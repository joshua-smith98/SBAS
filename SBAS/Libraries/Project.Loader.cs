using System.IO;
using System.Text;

namespace SBAS
{
    public partial class Project
    {
        public class Loader : Project
        {
            BinaryReader br;
            public Loader(string Dir, bool Initialise = true)
            {
                fileStream = File.Open(Dir, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                br = new BinaryReader(fileStream, Encoding.ASCII);

                if (br.ReadChars(8).MakeString() != "SBASPROJ") throw new InvalidFileException();

                switch (br.ReadInt16())
                {
                    case 0:
                        LoadV0(Initialise);
                        break;
                    case 1:
                        LoadV1(Initialise);
                        break;
                    case 2:
                        LoadV2(Initialise);
                        break;
                    case 3:
                        LoadV3(Initialise);
                        break;
                    default:
                        throw new InvalidFileException();
                }
            }

            void LoadV0(bool init)
            {
                #region Project File V0 Format
                /* SBAS Project File (*.sbp) V0 Format & Layout
                 * 
                 * --METADATA--
                 * HEADER:              Offset: 0   Length: 8   Type:   String  Value:  SBASPROJ
                 * FILEVERSION:         Offset: 8   Length: 2   Type:   Int16   Value:  0
                 * PROJECTNAMELENGTH:   Offset: 10  Length: 2   Type:   Int16   Value:  --
                 * PROJECTNAME:         Offset: 12  Length: -   Type:   String  Value:  --
                 * STRINGTABLEOFFSET:   Offset: --  Length: 4   Type:   Int32   Value:  --
                 * STRINGTABLEROWS:     Offset: +4  Length: 2   Type:   Int16   Value:  --
                 * AUDIOTABLEOFFSET:    Offset: +6  Length: 4   Type:   Int32   Value:  --
                 * AUDIOTABLEROWS:      Offset: +10 Length: 2   Type:   Int16   Value:  --
                 * DATAFILETABLEOFFSET: Offset: +12 Length: 4   Type:   Int32   Value:  --
                 * DATAFILETABLEROWS:   Offset: +16 Length: 2   Type:   Int16   Value:  --
                 * --STRING TABLE--
                 * HEADER:              Offset: +22 Length: 8   Type:   String  Value:  SBASSTRT
                 * STRINGIDLENGTH:      Offset: +30 Length: 2   Type:   Int16   Value:  --
                 * STRINGID:            Offset: +32 Length: -   Type:   String  Value:  --
                 * STRINGTEXTLENGTH:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTEXT:          Offset: +2  Length: -   Type:   String  Value:  --
                 * STRINGTAGNUM:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1Length:    Offset: +2  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1:          Offset: +4  Length: -   Type:   String  Value:  --
                 * STRINGTAG2Length:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG2:          Offset: +2  Length: -   Type:   String  Value:  --
                 * etc...
                 * STRINGACTION:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGDELAY:         Offset: +2  Length: 2   Type:   Int16   Value:  -- //Only if STRINGACTION == 2
                 * STRINGAUDIO1MD5:     Offset: +2  Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGAUDIO2MD5:     Offset: +34 Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGENABLEPHRASES: Offset: --  Length: 1   Type:   Bool    Value:  -- //Only if STRINGACTION == 0
                 * ...cont to next string...
                 * --AUDIO TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASAUDT
                 * AUDIODIRLENGTH:      Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * AUDIODIR:            Offset: +12 Length: -   Type:   String  Value:  --
                 * AUDIOOFFSET:         Offset: --  Length: 8   Type:   Int64   Value:  --
                 * AUDIOLENGTH:         Offset: +8  Length: 8   Type:   Int64   Value:  --
                 * AUDIOMD5:            Offset: +16 Length: 32  Type:   String  Value:  --
                 * ...cont to next audio file...
                 * --DATA FILE TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASDATT
                 * DATAFILEDIRLENGTH:   Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * DATAFILEDIR:         Offset: +12 Length: -   Type:   String  Value:  --
                 * ...cont to next data file...
                 * --DATA FILE--
                 * See data file format
                 * 
                */
                #endregion

                int CharCount;
                int StringTableOffset;
                short StringTableRows;
                int AudioTableOffset;
                short AudioTableRows;
                int DataFileTableOffset;
                short DataFileTableRows;

                fileStream.Seek(10, SeekOrigin.Begin);

                CharCount = br.ReadInt16();
                Name = br.ReadChars(CharCount).MakeString();
                StringTableOffset = br.ReadInt32();
                StringTableRows = br.ReadInt16();
                AudioTableOffset = br.ReadInt32();
                AudioTableRows = br.ReadInt16();
                DataFileTableOffset = br.ReadInt32();
                DataFileTableRows = br.ReadInt16();

                if (br.ReadChars(8).MakeString() != "SBASSTRT") throw new InvalidFileException();

                for (short i = 0; i < StringTableRows; i++)
                {
                    String NewString = new String() { ParentProject = this };
                    short TagNum;
                    short action;

                    CharCount = br.ReadInt16();
                    NewString.ID = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    NewString.Text = br.ReadChars(CharCount).MakeString();
                    TagNum = br.ReadInt16();
                    for (short j = 0; j < TagNum; j++)
                    {
                        CharCount = br.ReadInt16();
                        NewString.Tags.Add(br.ReadChars(CharCount).MakeString());
                    }

                    action = br.ReadInt16();

                    switch (action)
                    {
                        case 0:
                            NewString.action = new String.Action.Audio()
                            {
                                MainAudioHash = br.ReadChars(32).MakeString(),
                                EndAudioHash = br.ReadChars(32).MakeString(),
                                AllowPhrasing = br.ReadBoolean(),
                            };
                            break;
                        case 1:
                            NewString.action = String.Actions.Group;
                            break;
                        case 2:
                            NewString.action = new String.Action.Delay()
                            {
                                DelayLength = br.ReadInt16(),
                            };
                            break;
                        default:
                            throw new InvalidFileException();
                    }

                    Strings.Add(NewString);
                }

                if (br.ReadChars(8).MakeString() != "SBASAUDT") throw new InvalidFileException();

                for (short i = 0; i < AudioTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    AudioFile NewAudioFile = new AudioFile()
                    {
                        ParentProject = this,
                        FullDir = br.ReadChars(CharCount).MakeString(),
                        Offset = br.ReadInt64(),
                        Length = br.ReadInt64(),
                        Hash = br.ReadChars(32).MakeString(),
                    };

                    AudioFiles.Add(NewAudioFile);
                }

                if (br.ReadChars(8).MakeString() != "SBASDATT") throw new InvalidFileException();

                for (short i = 0; i < DataFileTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    DataFile NewDataFile = new DataFile()
                    {
                        ParentProject = this,
                        Dir = br.ReadChars(CharCount).MakeString(),
                    };
                    DataFiles.Add(NewDataFile);
                }

                if (init) InitialiseAll();
            }

            void LoadV1(bool init)
            {
                #region Project File V1 Format
                /* SBAS Project File (*.sbp) V1 Format & Layout
                 * 
                 * --METADATA--
                 * HEADER:              Offset: 0   Length: 8   Type:   String  Value:  SBASPROJ
                 * FILEVERSION:         Offset: 8   Length: 2   Type:   Int16   Value:  1
                 * PROJECTNAMELENGTH:   Offset: 10  Length: 2   Type:   Int16   Value:  --
                 * PROJECTNAME:         Offset: 12  Length: -   Type:   String  Value:  --
                 * STRINGTABLEOFFSET:   Offset: --  Length: 4   Type:   Int32   Value:  --
                 * STRINGTABLEROWS:     Offset: +4  Length: 2   Type:   Int16   Value:  --
                 * AUDIOTABLEOFFSET:    Offset: +6  Length: 4   Type:   Int32   Value:  --
                 * AUDIOTABLEROWS:      Offset: +10 Length: 2   Type:   Int16   Value:  --
                 * DATAFILETABLEOFFSET: Offset: +12 Length: 4   Type:   Int32   Value:  --
                 * DATAFILETABLEROWS:   Offset: +16 Length: 2   Type:   Int16   Value:  --
                 * --STRING TABLE--
                 * HEADER:              Offset: +22 Length: 8   Type:   String  Value:  SBASSTRT
                 * STRINGIDLENGTH:      Offset: +30 Length: 2   Type:   Int16   Value:  --
                 * STRINGID:            Offset: +32 Length: -   Type:   String  Value:  --
                 * STRINGTEXTLENGTH:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTEXT:          Offset: +2  Length: -   Type:   String  Value:  --
                 * STRINGTAGNUM:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1Length:    Offset: +2  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1:          Offset: +4  Length: -   Type:   String  Value:  --
                 * STRINGTAG2Length:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG2:          Offset: +2  Length: -   Type:   String  Value:  --
                 * etc...
                 * STRINGACTION:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGDELAY:         Offset: +2  Length: 2   Type:   Int16   Value:  -- //Only if STRINGACTION == 2
                 * STRINGAUDIO1MD5:     Offset: +2  Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGAUDIO2MD5:     Offset: +34 Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGENABLEPHRASES: Offset: --  Length: 1   Type:   Bool    Value:  -- //Only if STRINGACTION == 0
                 * ...cont to next string...
                 * --AUDIO TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASAUDT
                 * AUDIODIRLENGTH:      Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * AUDIODIR:            Offset: +12 Length: -   Type:   String  Value:  --
                 * AUDIOOFFSET:         Offset: --  Length: 8   Type:   Int64   Value:  --
                 * AUDIOLENGTH:         Offset: +8  Length: 8   Type:   Int64   Value:  --
                 * AUDIOMD5:            Offset: +8  Length: 32  Type:   String  Value:  --
                 * AUDIOAUTOCROPPING    Offset: +32 Length: 2   Type    Bool    Value:  --
                 * ...cont to next audio file...
                 * --DATA FILE TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASDATT
                 * DATAFILEDIRLENGTH:   Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * DATAFILEDIR:         Offset: +12 Length: -   Type:   String  Value:  --
                 * ...cont to next data file...
                 * --DATA FILE--
                 * See data file format
                 * 
                */
                #endregion

                int CharCount;
                int StringTableOffset;
                short StringTableRows;
                int AudioTableOffset;
                short AudioTableRows;
                int DataFileTableOffset;
                short DataFileTableRows;

                fileStream.Seek(10, SeekOrigin.Begin);

                CharCount = br.ReadInt16();
                Name = br.ReadChars(CharCount).MakeString();
                StringTableOffset = br.ReadInt32();
                StringTableRows = br.ReadInt16();
                AudioTableOffset = br.ReadInt32();
                AudioTableRows = br.ReadInt16();
                DataFileTableOffset = br.ReadInt32();
                DataFileTableRows = br.ReadInt16();

                if (br.ReadChars(8).MakeString() != "SBASSTRT") throw new InvalidFileException();

                SBAS.MainWindowController.ShowProgressViewer("Loading Project...", StringTableRows + AudioTableRows + DataFileTableRows);

                for (short i = 0; i < StringTableRows; i++)
                {
                    String NewString = new String() { ParentProject = this };
                    short TagNum;
                    short action;

                    CharCount = br.ReadInt16();
                    NewString.ID = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    NewString.Text = br.ReadChars(CharCount).MakeString();
                    TagNum = br.ReadInt16();
                    for (short j = 0; j < TagNum; j++)
                    {
                        CharCount = br.ReadInt16();
                        NewString.Tags.Add(br.ReadChars(CharCount).MakeString());
                    }

                    action = br.ReadInt16();

                    switch (action)
                    {
                        case 0:
                            NewString.action = new String.Action.Audio()
                            {
                                MainAudioHash = br.ReadChars(32).MakeString(),
                                EndAudioHash = br.ReadChars(32).MakeString(),
                                AllowPhrasing = br.ReadBoolean(),
                            };
                            break;
                        case 1:
                            NewString.action = String.Actions.Group;
                            break;
                        case 2:
                            NewString.action = new String.Action.Delay()
                            {
                                DelayLength = br.ReadInt16(),
                            };
                            break;
                        default:
                            throw new InvalidFileException();
                    }

                    SBAS.MainWindowController.UpdateProgress(i, "Loading Audio");

                    Strings.Add(NewString);
                }

                if (br.ReadChars(8).MakeString() != "SBASAUDT") throw new InvalidFileException();

                for (short i = 0; i < AudioTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    AudioFile NewAudioFile = new AudioFile()
                    {
                        ParentProject = this,
                        FullDir = br.ReadChars(CharCount).MakeString(),
                        Offset = br.ReadInt64(),
                        Length = br.ReadInt64(),
                        Hash = br.ReadChars(32).MakeString(),
                        AutoCropping = br.ReadBoolean(),
                    };

                    SBAS.MainWindowController.UpdateProgress(StringTableRows + i, "Loading Audio");

                    AudioFiles.Add(NewAudioFile);
                }

                if (br.ReadChars(8).MakeString() != "SBASDATT") throw new InvalidFileException();

                for (short i = 0; i < DataFileTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    DataFile NewDataFile = new DataFile()
                    {
                        ParentProject = this,
                        Dir = br.ReadChars(CharCount).MakeString(),
                    };

                    SBAS.MainWindowController.UpdateProgress(StringTableRows + AudioTableRows + i, "Loading Data Files");

                    DataFiles.Add(NewDataFile);
                }

                SBAS.MainWindowController.CloseProgressViewer();

                if (init) InitialiseAll();
            }

            void LoadV2(bool init)
            {
                #region Project File V2 Format
                /* SBAS Project File (*.sbp) V2 Format & Layout
                 * 
                 * --METADATA--
                 * HEADER:              Offset: 0   Length: 8   Type:   String  Value:  SBASPROJ
                 * FILEVERSION:         Offset: 8   Length: 2   Type:   Int16   Value:  2
                 * PROJECTNAMELENGTH:   Offset: 10  Length: 2   Type:   Int16   Value:  --
                 * PROJECTNAME:         Offset: 12  Length: -   Type:   String  Value:  --
                 * STRINGTABLEOFFSET:   Offset: --  Length: 4   Type:   Int32   Value:  --
                 * STRINGTABLEROWS:     Offset: +4  Length: 2   Type:   Int16   Value:  --
                 * AUDIOTABLEOFFSET:    Offset: +6  Length: 4   Type:   Int32   Value:  --
                 * AUDIOTABLEROWS:      Offset: +10 Length: 2   Type:   Int16   Value:  --
                 * DATAFILETABLEOFFSET: Offset: +12 Length: 4   Type:   Int32   Value:  --
                 * DATAFILETABLEROWS:   Offset: +16 Length: 2   Type:   Int16   Value:  --
                 * --STRING TABLE--
                 * HEADER:              Offset: +22 Length: 8   Type:   String  Value:  SBASSTRT
                 * STRINGIDLENGTH:      Offset: +30 Length: 2   Type:   Int16   Value:  --
                 * STRINGID:            Offset: +32 Length: -   Type:   String  Value:  --
                 * STRINGTEXTLENGTH:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTEXT:          Offset: +2  Length: -   Type:   String  Value:  --
                 * STRINGTAGNUM:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1Length:    Offset: +2  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1:          Offset: +4  Length: -   Type:   String  Value:  --
                 * STRINGTAG2Length:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG2:          Offset: +2  Length: -   Type:   String  Value:  --
                 * etc...
                 * STRINGACTION:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGDELAY:         Offset: +2  Length: 2   Type:   Int16   Value:  -- //Only if STRINGACTION == 2
                 * STRINGAUDIO1MD5:     Offset: +2  Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGAUDIO2MD5:     Offset: +34 Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGENABLEPHRASES: Offset: --  Length: 1   Type:   Bool    Value:  -- //Only if STRINGACTION == 0
                 * ...cont to next string...
                 * --AUDIO TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASAUDT
                 * AUDIODIRLENGTH:      Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * AUDIODIR:            Offset: +12 Length: -   Type:   String  Value:  --
                 * AUDIOOFFSET:         Offset: --  Length: 8   Type:   Int64   Value:  --
                 * AUDIOLENGTH:         Offset: +8  Length: 8   Type:   Int64   Value:  --
                 * AUDIOMD5:            Offset: +8  Length: 32  Type:   String  Value:  --
                 * AUDIOAUTOCROPPING    Offset: +32 Length: 2   Type:   Bool    Value:  --
                 * AUDIOOFFSETL         Offset: +2  Length: 8   Type:   Int64   Value:  --
                 * AUDIOOFFSETR         Offset: +8  Length: 8   Type:   Int64   Value:  --
                 * ...cont to next audio file...
                 * --DATA FILE TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASDATT
                 * DATAFILEDIRLENGTH:   Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * DATAFILEDIR:         Offset: +12 Length: -   Type:   String  Value:  --
                 * ...cont to next data file...
                 * --DATA FILE--
                 * See data file format
                 * 
                */
                #endregion

                int CharCount;
                int StringTableOffset;
                short StringTableRows;
                int AudioTableOffset;
                short AudioTableRows;
                int DataFileTableOffset;
                short DataFileTableRows;

                fileStream.Seek(10, SeekOrigin.Begin);

                CharCount = br.ReadInt16();
                Name = br.ReadChars(CharCount).MakeString();
                StringTableOffset = br.ReadInt32();
                StringTableRows = br.ReadInt16();
                AudioTableOffset = br.ReadInt32();
                AudioTableRows = br.ReadInt16();
                DataFileTableOffset = br.ReadInt32();
                DataFileTableRows = br.ReadInt16();

                if (br.ReadChars(8).MakeString() != "SBASSTRT") throw new InvalidFileException();

                SBAS.MainWindowController.ShowProgressViewer("Loading Project...", StringTableRows + AudioTableRows + DataFileTableRows);

                for (short i = 0; i < StringTableRows; i++)
                {
                    String NewString = new String() { ParentProject = this };
                    short TagNum;
                    short action;

                    CharCount = br.ReadInt16();
                    NewString.ID = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    NewString.Text = br.ReadChars(CharCount).MakeString();
                    TagNum = br.ReadInt16();
                    for (short j = 0; j < TagNum; j++)
                    {
                        CharCount = br.ReadInt16();
                        NewString.Tags.Add(br.ReadChars(CharCount).MakeString());
                    }

                    action = br.ReadInt16();

                    switch (action)
                    {
                        case 0:
                            NewString.action = new String.Action.Audio()
                            {
                                MainAudioHash = br.ReadChars(32).MakeString(),
                                EndAudioHash = br.ReadChars(32).MakeString(),
                                AllowPhrasing = br.ReadBoolean(),
                            };
                            break;
                        case 1:
                            NewString.action = String.Actions.Group;
                            break;
                        case 2:
                            NewString.action = new String.Action.Delay()
                            {
                                DelayLength = br.ReadInt16(),
                            };
                            break;
                        default:
                            throw new InvalidFileException();
                    }

                    SBAS.MainWindowController.UpdateProgress(i, "Loading Audio");

                    Strings.Add(NewString);
                }

                if (br.ReadChars(8).MakeString() != "SBASAUDT") throw new InvalidFileException();

                for (short i = 0; i < AudioTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    AudioFile NewAudioFile = new AudioFile()
                    {
                        ParentProject = this,
                        FullDir = br.ReadChars(CharCount).MakeString(),
                        Offset = br.ReadInt64(),
                        Length = br.ReadInt64(),
                        Hash = br.ReadChars(32).MakeString(),
                        AutoCropping = br.ReadBoolean(),
                        ManCroppingL = br.ReadInt64(),
                        ManCroppingR = br.ReadInt64(),
                    };

                    SBAS.MainWindowController.UpdateProgress(StringTableRows + i, "Loading Audio");

                    AudioFiles.Add(NewAudioFile);
                }

                if (br.ReadChars(8).MakeString() != "SBASDATT") throw new InvalidFileException();

                for (short i = 0; i < DataFileTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    DataFile NewDataFile = new DataFile()
                    {
                        ParentProject = this,
                        Dir = br.ReadChars(CharCount).MakeString(),
                    };

                    SBAS.MainWindowController.UpdateProgress(StringTableRows + AudioTableRows + i, "Loading Data Files");

                    DataFiles.Add(NewDataFile);
                    if (NewDataFile.Dir == fileStream.Name) MainDataFile = NewDataFile;
                }

                SBAS.MainWindowController.CloseProgressViewer();

                if (init) InitialiseAll();
            }

            void LoadV3(bool init)
            {
                #region Project File V3 Format
                /* SBAS Project File (*.sbp) V3 Format & Layout
                 * 
                 * --METADATA--
                 * HEADER:              Offset: 0   Length: 8   Type:   String  Value:  SBASPROJ
                 * FILEVERSION:         Offset: 8   Length: 2   Type:   Int16   Value:  3
                 * PROJECTNAMELENGTH:   Offset: 10  Length: 2   Type:   Int16   Value:  --
                 * PROJECTNAME:         Offset: 12  Length: -   Type:   String  Value:  --
                 * STRINGTABLEOFFSET:   Offset: --  Length: 4   Type:   Int32   Value:  --
                 * STRINGTABLEROWS:     Offset: +4  Length: 2   Type:   Int16   Value:  --
                 * AUDIOTABLEOFFSET:    Offset: +6  Length: 4   Type:   Int32   Value:  --
                 * AUDIOTABLEROWS:      Offset: +10 Length: 2   Type:   Int16   Value:  --
                 * DATAFILETABLEOFFSET: Offset: +12 Length: 4   Type:   Int32   Value:  --
                 * DATAFILETABLEROWS:   Offset: +16 Length: 2   Type:   Int16   Value:  --
                 * LINETABLEOFFSET:     Offset: +18 Length: 4   Type:   Int32   Value:  --
                 * LINETABLEROWS:       Offset: +22 Length: 2   Type:   Int16   Value:  --
                 * 
                 * --STRING TABLE--
                 * HEADER:              Offset: +24 Length: 8   Type:   String  Value:  SBASSTRT
                 * STRINGIDLENGTH:      Offset: +32 Length: 2   Type:   Int16   Value:  --
                 * STRINGID:            Offset: +34 Length: -   Type:   String  Value:  --
                 * STRINGTEXTLENGTH:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTEXT:          Offset: +2  Length: -   Type:   String  Value:  --
                 * STRINGTAGNUM:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1Length:    Offset: +2  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG1:          Offset: +4  Length: -   Type:   String  Value:  --
                 * STRINGTAG2Length:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGTAG2:          Offset: +2  Length: -   Type:   String  Value:  --
                 * etc...
                 * STRINGACTION:        Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STRINGDELAY:         Offset: +2  Length: 2   Type:   Int16   Value:  -- //Only if STRINGACTION == 2
                 * STRINGAUDIO1MD5:     Offset: +2  Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGAUDIO2MD5:     Offset: +34 Length: 32  Type:   String  Value:  -- //Only if STRINGACTION == 0
                 * STRINGENABLEPHRASES: Offset: --  Length: 1   Type:   Bool    Value:  -- //Only if STRINGACTION == 0
                 * ...cont to next string...
                 * --AUDIO TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASAUDT
                 * AUDIODIRLENGTH:      Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * AUDIODIR:            Offset: +12 Length: -   Type:   String  Value:  --
                 * AUDIOOFFSET:         Offset: --  Length: 8   Type:   Int64   Value:  --
                 * AUDIOLENGTH:         Offset: +8  Length: 8   Type:   Int64   Value:  --
                 * AUDIOMD5:            Offset: +8  Length: 32  Type:   String  Value:  --
                 * AUDIOAUTOCROPPING    Offset: +32 Length: 2   Type:   Bool    Value:  --
                 * AUDIOOFFSETL         Offset: +2  Length: 8   Type:   Int64   Value:  --
                 * AUDIOOFFSETR         Offset: +8  Length: 8   Type:   Int64   Value:  --
                 * ...cont to next audio file...
                 * --DATA FILE TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASDATT
                 * DATAFILEDIRLENGTH:   Offset: +8  Length: 4   Type:   Int32   Value:  --
                 * DATAFILEDIR:         Offset: +12 Length: -   Type:   String  Value:  --
                 * ...cont to next data file...
                 * --LINE TABLE--
                 * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASLINT
                 * NAMELENGTH:          Offset: +8  Length: 2   Type:   Int16   Value:  --
                 * NAME:                Offset: +10 Length: --  Type:   String  Value:  --
                 * BRANCHFROMLENGTH:    Offset: --  Length: 2   Type:   Int16   Value:  --
                 * BRANCHFROM:          Offset: +2  Length: --  Type:   String  Value:  --
                 * BRANCHATLENGTH:      Offset: --  Length: 2   Type:   Int16   Value:  --
                 * BRANCHFROM:          Offset: +2  Length: --  Type:   String  Value:  --
                 * CONNECTTOLENGTH:     Offset: --  Length: 2   Type:   Int16   Value:  --
                 * CONNECTTO:           Offset: +2  Length: --  Type:   String  Value:  --
                 * CONNECTATLENGTH:     Offset: --  Length: 2   Type:   Int16   Value:  --
                 * CONNECTAT:           Offset: +2  Length: --  Type:   String  Value:  --
                 * STOPTABLEROWS:       Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STOPNAMELENGTH:      Offset: --  Length: 2   Type:   Int16   Value:  --
                 * STOPNAME:            Offset: +2  Length: --  Type:   String  Value:  --
                 * ...cont to next stop...
                 * ...cont to next line...
                 * 
                 * --DATA FILE--
                 * See data file format
                 * 
                */
                #endregion

                int CharCount;
                int StringTableOffset;
                short StringTableRows;
                int AudioTableOffset;
                short AudioTableRows;
                int DataFileTableOffset;
                short DataFileTableRows;
                int LineTableOffset;
                short LineTableRows;

                fileStream.Seek(10, SeekOrigin.Begin);

                CharCount = br.ReadInt16();
                Name = br.ReadChars(CharCount).MakeString();
                StringTableOffset = br.ReadInt32();
                StringTableRows = br.ReadInt16();
                AudioTableOffset = br.ReadInt32();
                AudioTableRows = br.ReadInt16();
                DataFileTableOffset = br.ReadInt32();
                DataFileTableRows = br.ReadInt16();
                LineTableOffset = br.ReadInt32();
                LineTableRows = br.ReadInt16();

                if (br.ReadChars(8).MakeString() != "SBASSTRT") throw new InvalidFileException();

                SBAS.MainWindowController.ShowProgressViewer("Loading Project...", StringTableRows + AudioTableRows + DataFileTableRows + LineTableRows);

                for (short i = 0; i < StringTableRows; i++)
                {
                    String NewString = new String() { ParentProject = this };
                    short TagNum;
                    short action;

                    CharCount = br.ReadInt16();
                    NewString.ID = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    NewString.Text = br.ReadChars(CharCount).MakeString();
                    TagNum = br.ReadInt16();
                    for (short j = 0; j < TagNum; j++)
                    {
                        CharCount = br.ReadInt16();
                        NewString.Tags.Add(br.ReadChars(CharCount).MakeString());
                    }

                    action = br.ReadInt16();

                    switch (action)
                    {
                        case 0:
                            NewString.action = new String.Action.Audio()
                            {
                                MainAudioHash = br.ReadChars(32).MakeString(),
                                EndAudioHash = br.ReadChars(32).MakeString(),
                                AllowPhrasing = br.ReadBoolean(),
                            };
                            break;
                        case 1:
                            NewString.action = String.Actions.Group;
                            break;
                        case 2:
                            NewString.action = new String.Action.Delay()
                            {
                                DelayLength = br.ReadInt16(),
                            };
                            break;
                        default:
                            throw new InvalidFileException();
                    }

                    SBAS.MainWindowController.UpdateProgress(i, "Loading Audio");

                    Strings.Add(NewString);
                }

                if (br.ReadChars(8).MakeString() != "SBASAUDT") throw new InvalidFileException();

                for (short i = 0; i < AudioTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    AudioFile NewAudioFile = new AudioFile()
                    {
                        ParentProject = this,
                        FullDir = br.ReadChars(CharCount).MakeString(),
                        Offset = br.ReadInt64(),
                        Length = br.ReadInt64(),
                        Hash = br.ReadChars(32).MakeString(),
                        AutoCropping = br.ReadBoolean(),
                        ManCroppingL = br.ReadInt64(),
                        ManCroppingR = br.ReadInt64(),
                    };

                    SBAS.MainWindowController.UpdateProgress(StringTableRows + i, "Loading Audio");

                    AudioFiles.Add(NewAudioFile);
                }

                if (br.ReadChars(8).MakeString() != "SBASDATT") throw new InvalidFileException();

                for (short i = 0; i < DataFileTableRows; i++)
                {
                    CharCount = br.ReadInt32();
                    DataFile NewDataFile = new DataFile()
                    {
                        ParentProject = this,
                        Dir = br.ReadChars(CharCount).MakeString(),
                    };

                    SBAS.MainWindowController.UpdateProgress(StringTableRows + AudioTableRows + i, "Loading Data Files");

                    DataFiles.Add(NewDataFile);
                    if (NewDataFile.Dir == fileStream.Name) MainDataFile = NewDataFile;
                }

                if (br.ReadChars(8).MakeString() != "SBASLINT") throw new InvalidFileException();

                for (short i = 0; i < LineTableRows; i++)
                {
                    string Name;
                    string BranchFrom;
                    string BranchAt;
                    string ConnectTo;
                    string ConnectAt;
                    int StopTableRows;

                    CharCount = br.ReadInt16();
                    Name = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    BranchFrom = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    BranchAt = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    ConnectTo = br.ReadChars(CharCount).MakeString();
                    CharCount = br.ReadInt16();
                    ConnectAt = br.ReadChars(CharCount).MakeString();
                    StopTableRows = br.ReadInt16();

                    Line NewLine = new Line()
                    {
                        ParentProject = this,
                        Name = Name,
                        BranchesFromStr = BranchFrom,
                        BranchesAtStr = BranchAt,
                        ConnectsToStr = ConnectTo,
                        ConnectsAtStr = ConnectAt
                    };

                    for (short j = 0; j < StopTableRows; j++)
                    {
                        CharCount = br.ReadInt16();
                        string StopName = br.ReadChars(CharCount).MakeString();
                        Line.Stop NewStop = new Line.Stop()
                        {
                            ParentProject = this,
                            Name = Strings.Find(x => x.ID == StopName)
                        };

                        NewLine.Stops.Add(NewStop);
                    }

                    SBAS.MainWindowController.UpdateProgress(StringTableRows + AudioTableRows + i, "Loading Lines");

                    Lines.Add(NewLine);
                }

                SBAS.MainWindowController.CloseProgressViewer();

                if (init) InitialiseAll();
            }
        }
    }
}