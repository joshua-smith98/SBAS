using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Security.Cryptography;
using NAudio;
using NAudio.Utils;
using NAudio.Wave;
using NAudio.FileFormats;
using NAudio.MediaFoundation;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace SBAS
{
    public class Project : IDisposable
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
                            NewString.action = new String.Action.Delay() {
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
                    AudioFile NewAudioFile = new AudioFile() {
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

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public FileStream fileStream;

        public string Name;
        public List<String> Strings = new List<String>();
        public List<AudioFile> AudioFiles = new List<AudioFile>();
        public List<DataFile> DataFiles = new List<DataFile>();
        public List<Line> Lines = new List<Line>();
        public List<Line.Stop> Stops { get { return Lines.SelectMany(x => x.Stops).ToList(); } }
        DataFile MainDataFile;

        public Project(string Dir, string Name)
        {
            this.Name = Name;
            SaveFile(Dir);
            MainDataFile = new DataFile()
            {
                Dir = Dir,
                ParentProject = this,
                fileStream = File.Open(Dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite),
            };
            DataFiles.Add(MainDataFile);
            SaveFile(Dir);
            MainDataFile.Save();
        }

        protected Project()
        {

        }

        internal void InitialiseAll()
        {
            fileStream.Seek(0, SeekOrigin.Begin);

            Dictionary<String, AudioFile> UnlinkedStrings = new Dictionary<String, AudioFile>();

            int Counter = 0;
            SBAS.MainWindowController.ShowProgressViewer("Initialising Project...", DataFiles.Count);
            foreach (DataFile dat in DataFiles)
            {
                dat.Initialise();
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Initialising Data File: {0}", dat.Dir));
            }
            SBAS.MainWindowController.CloseProgressViewer();

            Counter = 0;
            SBAS.MainWindowController.ShowProgressViewer("Initialising Project...", AudioFiles.Count);
            foreach (AudioFile aud in AudioFiles)
            {
                aud.Initialise();
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Initialising Audio: {0}", aud.FileName));
            }
            SBAS.MainWindowController.CloseProgressViewer();

            Counter = 0;
            SBAS.MainWindowController.ShowProgressViewer("Initialising Project...", AudioFiles.Count);
            foreach (AudioFile aud in AudioFiles)
            {
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Checking Audio Integrity: {0}", aud.FileName));
                aud.fileStream.Position = 0;
                string NewHash =  BitConverter.ToString(MD5.Create().ComputeHash(aud.fileStream)).Replace("-", "").ToLowerInvariant();
                aud.fileStream.Position = 0;
                if (NewHash != aud.Hash)
                {
                    foreach (String str in Strings.Where(x => x.action.GetType() == typeof(String.Action.Audio) && ((String.Action.Audio)x.action).MainAudioHash == aud.Hash)) ((String.Action.Audio)str.action).MainAudioHash = NewHash;
                    aud.Hash = NewHash;
                }
            }
            SBAS.MainWindowController.CloseProgressViewer();

            SBAS.MainWindowController.ShowProgressViewer("Initialising Project...", Strings.Count);
            Counter = 0;
            foreach (String str in Strings)
            {
                str.Initialise();
                if (str.action.GetType() == typeof(String.Action.Audio))
                {
                    String.Action.Audio Action = (String.Action.Audio)str.action;
                    if (Action.MainAudio == null && Action.MainAudioHash != AudioFile.NullHash) UnlinkedStrings.Add(str, null);
                }

                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Initialising String: {0}", str.ID));
            }
            SBAS.MainWindowController.CloseProgressViewer();

            SBAS.MainWindowController.ShowProgressViewer("Initialising Project...", Lines.Count);
            Counter = 0;
            foreach (Line line in Lines)
            {
                line.Initialise();
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Initialising Line: {0}", line.Name));
            }
            SBAS.MainWindowController.CloseProgressViewer();

            if (UnlinkedStrings.Count > 0)
            {
                Dictionary<String, AudioFile> UnlinkedStringsB = new Dictionary<String, AudioFile>();
                foreach (KeyValuePair<String, AudioFile> pair in UnlinkedStrings) UnlinkedStringsB.Add(pair.Key, pair.Value);

                foreach (String str in UnlinkedStringsB.Keys)
                {
                    if (AudioFiles.Select(x => Path.GetFileNameWithoutExtension(x.FullDir)).Contains(str.ID)) UnlinkedStrings[str] = AudioFiles.Find(x => Path.GetFileNameWithoutExtension(x.FullDir) == str.ID);
                }

                int FoundFiles = UnlinkedStrings.Values.Count(x => x != null);
                bool Continue = false;

                if (FoundFiles == 0 && MessageBox.Show(string.Format("{0} Strings have (somehow) lost their attatched Audio Files. Unfortunately these cannot be recovered. Do you want to remove the voided attachments? (This is irreversable!)", UnlinkedStrings.Count), "Detached Audio Files", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    foreach (String str in UnlinkedStrings.Keys) ((String.Action.Audio)str.action).MainAudioHash = AudioFile.NullHash;
                }
                else if (FoundFiles != 0)
                {
                    if (MessageBox.Show(string.Format("{0} Strings have (somehow) lost their attatched Audio Files. Would you like to automatically relink {1} similar audio files?", UnlinkedStrings.Count, FoundFiles), "Detached Audio Files", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        foreach (KeyValuePair<String, AudioFile> Pair in UnlinkedStrings)
                        {
                            ((String.Action.Audio)Pair.Key.action).MainAudio = Pair.Value;
                            ((String.Action.Audio)Pair.Key.action).MainAudioHash = Pair.Value.Hash;
                        }
                    }

                    if (FoundFiles != UnlinkedStrings.Count && MessageBox.Show(string.Format("Do you want to remove the remaining {0} voided attachments? (This is irreversable!)", UnlinkedStrings.Count - FoundFiles), "Detached Audio Files", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        foreach (String str in UnlinkedStrings.Where(x => x.Value == null).ToDictionary(x => x.Key, x => x.Value).Keys) ((String.Action.Audio)str.action).MainAudioHash = AudioFile.NullHash;
                    }
                }

            }
        }

        public class String
        {
            public class Action
            {
                public class Audio : Action
                {
                    internal string MainAudioHash = AudioFile.NullHash;
                    internal string EndAudioHash = AudioFile.NullHash;
                    public AudioFile MainAudio;
                    public AudioFile EndAudio;

                    public bool AllowPhrasing = true;

                    public Audio()
                    {
                        value = 0;
                    }

                    public Audio(AudioFile MainAudio)
                    {
                        value = 0;
                        this.MainAudio = MainAudio;
                        MainAudioHash = MainAudio.Hash;
                    }
                }

                public class Delay : Action
                {
                    public short DelayLength;

                    public Delay()
                    {
                        value = 2;
                    }
                }

                internal short value;
            }

            public static class Actions
            {
                public static readonly Action Audio = new Action() { value = 0 };
                public static readonly Action Group = new Action() { value = 1 };
                public static readonly Action Delay = new Action() { value = 2 };
            }

            public string ID;
            public string Text;
            public string[] Words { get { return Text.GetWords(); } }
            public List<string> Tags = new List<string>();
            public Action action;

            internal Project ParentProject;

            internal String() { }

            internal void Initialise()
            {
                if (ParentProject.Strings.Exists(x => x.ID == ID && x != this)) throw new DuplicateStringException();

                if (action.value == 0)
                {
                    AudioFile FoundAudio = ParentProject.AudioFiles.Find(x => x.Hash == ((Action.Audio)action).MainAudioHash);
                    if (FoundAudio != null) ((Action.Audio)action).MainAudio = FoundAudio;

                    FoundAudio = ParentProject.AudioFiles.Find(x => x.Hash == ((Action.Audio)action).EndAudioHash);
                    if (FoundAudio != null) ((Action.Audio)action).EndAudio = FoundAudio;
                }
            }

            public String[] GetChildren()
            {
                List<String> ret = ParentProject.Strings.Where(x => x != this && Words.Length > x.Words.Length).ToList();
                //string tempstring = string.Concat(Words);
                string[] tempwords = Words;
                bool Found = false;
                List<String> newret = new List<String>();

                while (true)
                {
                    //String[] FoundStrings = ret.Where(x => tempstring.StartsWith(string.Concat(x.Words))).ToArray();
                    String[] FoundStrings = ret.Where(x => string.Concat(tempwords.Reverse().Take(x.Words.Length).Reverse()) == string.Concat(x.Words)).ToArray();
                    if (FoundStrings.Length == 0) break;
                    else
                    {
                        //String FoundString = FoundStrings.OrderBy(x => x.Words.Length).Last();
                        String FoundString = FoundStrings.ToList().Find(x => string.Concat(tempwords.Reverse().Take(x.Words.Length).Reverse()) == string.Concat(x.Words) && !FoundStrings.Any(y => y != x && string.Concat(y.Words) == string.Concat(x.Words) && y.Words.Length < x.Words.Length));
                        //tempstring = tempstring.Remove(0, FoundString.Text.Length);
                        tempwords = tempwords.Reverse().Skip(FoundString.Words.Length).Reverse().ToArray();
                        newret.Add(FoundString);

                        if (tempwords.Length == 0)
                        {
                            if (string.Concat(newret.ToArray().Reverse().Select(x => string.Concat(x.Words).ToLower())) == string.Concat(Words))
                            {
                                Found = true;
                            }
                            break;
                        }
                    }
                }

                if (Found) return newret.ToArray().Reverse().ToArray();
                else return new List<String>().ToArray();
            }

            public String[] GetParents()
            {
                return ParentProject.Strings.Where(x => x.Text.ToLowerInvariant().Contains(Text.ToLowerInvariant()) && x.GetChildren().Contains(this)).ToArray();
            }

            public void Play()
            {
                GetAudioFile().Play();
            }

            public AudioFile GetAudioFile()
            {
                if (action.value == Actions.Audio.value)
                {
                    return ((Action.Audio)action).MainAudio;
                }
                else if (action.value == Actions.Group.value)
                {
                    String[] children = GetChildren();
                    List<AudioFile> audioFiles = new List<AudioFile>();

                    foreach (String str in children)
                    {
                        audioFiles.Add(str.GetAudioFile());
                    }

                    return AudioFile.Combine(audioFiles.ToArray());
                }
                else if (action.value == Actions.Delay.value)
                {
                    AudioFile Delay = AudioFile.CreateNullAudioFile();
                    Delay.CroppingL = -((Action.Delay)action).DelayLength;
                    return Delay;
                }
                else return null;
            }

            public override string ToString()
            {
                return ID;
            }
        }

        public class DataFile
        {
            #region Data File Format
            /* SBAS Data File (*.sbd / *.sbp) Format
             * 
             * --METADATA--
             * HEADER:              Offset: --  Length: 8   Type:   String  Value:  SBASDATA
             * DATAOFFSET:          Offset: +8  Length: 4   Type:   Int32   Value:  --
             * NUMFILES:            Offset: +12 Length: 4   Type:   Int32   Value:  --
             * FILE1HASH:           Offset: +16 Length: 32  Type:   String  Value:  --
             * FILE1OFFSET:         Offset: +48 Length: 8   Type:   Int64   Value:  --
             * FILE1LENGTH:         Offset: +56 Length: 8   Type:   Int64   Value:  --
             * ...cont for file2...
             * Straight audio files are strung together at the offsets above
             * 
            */
            #endregion

            public FileStream fileStream;
            internal string Dir;
            internal long Offset = 0;
            internal long Length
            {
                get
                {
                    return fileStream.Length - Offset;
                }
            }

            internal Project ParentProject;

            public List<AudioFile> AudioFiles = new List<AudioFile>();

            internal DataFile() { }

            public DataFile(string dir, AudioFile[] audioFiles)
            {
                Dir = dir;
                List<DataFile> SaveList = new List<DataFile>();
                foreach (AudioFile audioFile in audioFiles)
                {
                    if (audioFile.IsInDataFile)
                    {
                        DataFile dataFile = audioFile.GetDataFile();
                        if (!SaveList.Contains(dataFile)) SaveList.Add(dataFile);
                        audioFile.GetDataFile().Remove(audioFile, true, true);
                    }
                    audioFile.FullDir = string.Concat(Dir, @"\", audioFile.FileName);
                    audioFile.IsInDataFile = true;
                    AudioFiles.Add(audioFile);
                }
                foreach (DataFile dat in SaveList) dat.Save();
                fileStream = File.Open(Dir, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                Save();
            }

            internal void Initialise()
            {
                long DataOffset;
                int NumFiles;

                fileStream = File.Open(Dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(fileStream);
                Offset = br.FindFirst("SBASDATA");
                if (Offset == -1) throw new InvalidFileException();

                fileStream.Seek(Offset + 8, SeekOrigin.Begin);
                DataOffset = Offset + br.ReadInt32();
                NumFiles = br.ReadInt32();

                for (int i = 0; i < NumFiles; i++)
                {
                    string hash = br.ReadChars(32).MakeString();
                    long offset = Offset + br.ReadInt64();
                    long length = br.ReadInt64();
                    AudioFile FoundFile = ParentProject?.AudioFiles.Find(x => x.Hash == hash);
                    if (FoundFile != null)
                    {
                        FoundFile.Offset = offset;
                        FoundFile.Length = length;
                        FoundFile.IsInDataFile = true;
                        FoundFile.fileStream = new MemoryStream();
                        File.Open(Dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite).CopyBytes(FoundFile.fileStream, FoundFile.Offset, FoundFile.Length);
                        AudioFiles.Add(FoundFile);
                    }
                    else
                    {
                        AudioFile NewAudioFile = new AudioFile()
                        {
                            Hash = hash,
                            Offset = offset,
                            Length = length,
                            IsInDataFile = true,
                            fileStream = new MemoryStream(),
                        };
                        File.Open(Dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite).CopyBytes(NewAudioFile.fileStream, NewAudioFile.Offset, NewAudioFile.Length);
                        AudioFiles.Add(NewAudioFile);
                    }
                }
            }

            public void Add(AudioFile audioFile)
            {
                if (audioFile.IsInDataFile) audioFile.GetDataFile().Remove(audioFile, true, false);
                audioFile.FullDir = string.Concat(Dir, @"\", audioFile.FileName);
                audioFile.IsInDataFile = true;
                AudioFiles.Add(audioFile);
                Save();
            }

            public void Add(AudioFile[] audioFiles)
            {
                List<DataFile> SaveList = new List<DataFile>();
                foreach (AudioFile audioFile in audioFiles)
                {
                    if (audioFile.IsInDataFile)
                    {
                        DataFile dataFile = audioFile.GetDataFile();
                        if (!SaveList.Contains(dataFile)) SaveList.Add(dataFile);
                        audioFile.GetDataFile().Remove(audioFile, true, true);
                    }
                    audioFile.FullDir = string.Concat(Dir, @"\", audioFile.FileName);
                    audioFile.IsInDataFile = true;
                    AudioFiles.Add(audioFile);
                }
                foreach (DataFile dat in SaveList) dat.Save();
                Save();
            }

            public void Remove(AudioFile audioFile)
            {
                if (AudioFiles.Contains(audioFile))
                {
                    AudioFiles.Remove(audioFile);
                    audioFile.fileStream.Dispose();
                }
                Save();
            }

            public void Remove(AudioFile[] audioFiles)
            {
                foreach (AudioFile audioFile in audioFiles)
                {
                    if (AudioFiles.Contains(audioFile))
                    {
                        AudioFiles.Remove(audioFile);
                        audioFile.fileStream.Dispose();
                    }
                }
                Save();
            }

            private void Remove(AudioFile audioFile, bool DoNotDispose, bool DoNotSave)
            {
                if (AudioFiles.Contains(audioFile))
                {
                    AudioFiles.Remove(audioFile);
                    if (!DoNotDispose) audioFile.fileStream.Dispose();
                }
                if (!DoNotSave) Save();
            }

            internal void Save()
            {
                if (Offset > 0)
                {
                    fileStream.Position = 0;
                    MemoryStream FileBuffer = new MemoryStream();
                    fileStream.CopyTo(FileBuffer, Offset);

                    fileStream.Dispose();
                    fileStream = File.Open(Dir, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

                    FileBuffer.WriteTo(fileStream);
                    FileBuffer.Dispose();
                }
                else
                {
                    fileStream.Dispose();
                    fileStream = File.Open(Dir, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                }

                int DataOffset = 16 + (AudioFiles.Count * 48);
                int NumFiles = AudioFiles.Count;
                fileStream.Write(Encoding.ASCII.GetBytes("SBASDATA"), 0, 8);
                fileStream.Write(BitConverter.GetBytes(DataOffset), 0, 4);
                fileStream.Write(BitConverter.GetBytes(NumFiles), 0, 4);

                long CurrentAudioOffset = DataOffset;

                foreach (AudioFile aud in AudioFiles)
                {
                    string Hash = aud.Hash;
                    long Offset = CurrentAudioOffset;
                    long Length = aud.fileStream.Length;
                    fileStream.Write(Encoding.ASCII.GetBytes(Hash), 0, 32);
                    fileStream.Write(BitConverter.GetBytes(Offset), 0, 8);
                    aud.Offset = Offset;
                    fileStream.Write(BitConverter.GetBytes(Length), 0, 8);

                    CurrentAudioOffset += Length;
                }

                SBAS.MainWindowController.ShowProgressViewer(string.Format("Saving Data File: {0}", Path.GetFileName(Dir)), AudioFiles.Count);
                int Counter = 0;
                foreach (AudioFile aud in AudioFiles)
                {
                    SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Copying Audio File: {0}", aud.FileName));
                    aud.fileStream.Position = 0;
                    aud.fileStream.CopyTo(fileStream);
                    aud.fileStream.Position = 0;
                }

                SBAS.MainWindowController.CloseProgressViewer();
                fileStream.Flush();
                fileStream.Dispose();
                fileStream = File.Open(Dir, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            }

            public override string ToString()
            {
                return Path.GetFileName(Dir);
            }
        }

        public class AudioFile
        {
            public MemoryStream fileStream;
            public WaveFormat waveFormat;
            internal string Hash;
            public const string NullHash = "00000000000000000000000000000000";
            public long Offset;
            public long Length;

            internal bool AutoCropping = false;
            public bool AutoCroppingEnabled { get { return AutoCropping; } }
            public long CroppingL
            {
                get
                {
                    if (AutoCropping)
                    {
                        return (long)((AutoCroppingL + ManCroppingL) / (waveFormat.SampleRate / 1000f));
                    }
                    else
                    {
                        return (long)((ManCroppingL) / (waveFormat.SampleRate / 1000f));
                    }
                }
                set
                {
                    ManCroppingL = (long)(value * (waveFormat.SampleRate / 1000f));
                }
            }
            public long CroppingR
            {
                get
                {
                    if (AutoCropping)
                    {
                        return (long)((AutoCroppingR + ManCroppingR) / (waveFormat.SampleRate / 1000f));
                    }
                    else
                    {
                        return (long)((ManCroppingR) / (waveFormat.SampleRate / 1000f));
                    }
                }
                set
                {
                    ManCroppingR = (long)(value * (waveFormat.SampleRate / 1000f));
                }
            }

            public long CroppingLSamples
            {
                get
                {
                    if (AutoCropping)
                    {
                        return AutoCroppingL + ManCroppingL;
                    }
                    else
                    {
                        return ManCroppingL;
                    }
                }
                set
                {
                    ManCroppingL = value;
                }
            }
            public long CroppingRSamples
            {
                get
                {
                    if (AutoCropping)
                    {
                        return AutoCroppingR + ManCroppingR;
                    }
                    else
                    {
                        return ManCroppingR;
                    }
                }
                set
                {
                    ManCroppingR = value;
                }
            }
            long AutoCroppingL = 0;
            long AutoCroppingR = 0;
            internal long ManCroppingL = 0;
            internal long ManCroppingR = 0;
            public long OffsetL { get { return (long)(ManCroppingL / (waveFormat.SampleRate / 1000f)); } }
            public long OffsetR { get { return (long)(ManCroppingR / (waveFormat.SampleRate / 1000f)); } }
            const float AutoCropThreshold = 0.00085f;
            const short AutoCropResolution = 100;

            internal Project ParentProject;

            public string FullDir;
            public string Directory { get { return Path.GetDirectoryName(FullDir); } }
            public string FileName { get { return Path.GetFileName(FullDir); } }
            public string Extension { get { return Path.GetExtension(FullDir); } }

            public bool IsInDataFile;

            internal AudioFile() { }

            public AudioFile(string Dir, Project parentProject)
            {
                FileStream filestream = File.Open(Dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                filestream.Seek(0, SeekOrigin.Begin);
                fileStream = new MemoryStream();
                filestream.CopyTo(fileStream);
                Offset = 0;
                Length = filestream.Length;
                fileStream.Position = 0;
                Hash = BitConverter.ToString(MD5.Create().ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
                fileStream.Position = 0;
                IsInDataFile = false;
                FullDir = filestream.Name;
                waveFormat = new WaveFileReader(fileStream).WaveFormat;
                fileStream.Position = 0;
                ParentProject = parentProject;
            }

            public AudioFile(Stream filestream, WaveFormat waveformat, Project parentProject = null)
            {
                fileStream = new MemoryStream();
                filestream.Seek(0, SeekOrigin.Begin);
                filestream.CopyTo(fileStream);
                Offset = 0;
                Length = filestream.Length;
                fileStream.SetLength(Length);
                Hash = BitConverter.ToString(MD5.Create().ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
                fileStream.Position = 0;
                IsInDataFile = false;
                FullDir = null;
                waveFormat = waveformat;
                fileStream.Seek(0, SeekOrigin.Begin);
                ParentProject = parentProject;
            }

            internal void Initialise()
            {
                if (fileStream == null)
                {
                    fileStream = new MemoryStream();
                    File.Open(FullDir, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite).CopyBytes(fileStream, Offset, Length);
                    if (Hash == null) Hash = BitConverter.ToString(MD5.Create().ComputeHash(fileStream)).Replace("-", "").ToLowerInvariant();
                }
                fileStream.Position = 0;
                waveFormat = new WaveFileReader(fileStream).WaveFormat;
                fileStream.Position = 0;
                if (AutoCropping) CalculateAutoCropping();
            }

            public static AudioFile CreateNullAudioFile()
            {
                AudioFile ret = new AudioFile();

                ret.fileStream = new MemoryStream();
                RawSourceWaveStream EmptyFile = new RawSourceWaveStream(new MemoryStream(), new WaveFormat(44100, 16, 1));
                WaveFileWriter.WriteWavFileToStream(ret.fileStream, EmptyFile);
                ret.Offset = 0;
                ret.Length = ret.fileStream.Length;
                ret.Hash = BitConverter.ToString(MD5.Create().ComputeHash(ret.fileStream)).Replace("-", "").ToLowerInvariant();
                ret.IsInDataFile = false;
                ret.FullDir = null;
                ret.waveFormat = new WaveFormat(44100, 16, 1);
                ret.fileStream.Seek(0, SeekOrigin.Begin);

                return ret;
            }

            public void EnableAutoCropping()
            {
                CalculateAutoCropping();
                AutoCropping = true;
            }

            public void DisableAutoCropping()
            {
                AutoCropping = false;
            }

            public void Play()
            {
                fileStream.Seek(0, SeekOrigin.Begin);
                new Audio.MemoryPlayer(fileStream).PlayAsync();
            }

            public static AudioFile Combine(AudioFile[] audioFiles, bool autoCropping = true)
            {
                SBAS.MainWindowController.ShowProgressViewer("Rendering Audio...", audioFiles.Length * 2);
                int Counter = 0;
                audioFiles[0].fileStream.Seek(0, SeekOrigin.Begin);
                WaveFormat Format = new WaveFileReader(audioFiles[0].fileStream).WaveFormat;
                List<AudioFile> audioFiles2 = new List<AudioFile>();
                foreach (AudioFile audioFile in audioFiles)
                {
                    SBAS.MainWindowController.UpdateProgress(Counter++, "Converting");
                    audioFile.fileStream.Seek(0, SeekOrigin.Begin);
                    WaveFileReader reader = new WaveFileReader(audioFile.fileStream);
                    MediaFoundationResampler resampler = new MediaFoundationResampler(reader, Format);
                    MemoryStream NewWave = new MemoryStream();
                    WaveFileWriter.WriteWavFileToStream(NewWave, resampler);
                    AudioFile newAudioFile = new AudioFile(NewWave, Format);
                    newAudioFile.ManCroppingL = audioFile.ManCroppingL;
                    newAudioFile.ManCroppingR = audioFile.ManCroppingR;
                    newAudioFile.AutoCropping = audioFile.AutoCropping;
                    newAudioFile.Initialise();
                    audioFiles2.Add(newAudioFile);
                }
                Array CombinedSamples = Array.CreateInstance(typeof(float[]), audioFiles2.Select(x => x.Length - x.CroppingLSamples - x.CroppingRSamples).Sum() * 2);
                long CroppingVal = 0;
                long CurrentOffset = 0;
                foreach (AudioFile audioFile in audioFiles2)
                {
                    SBAS.MainWindowController.UpdateProgress(Counter++, "Compiling");
                    audioFile.fileStream.Position = 0;
                    WaveFileReader WfR = new WaveFileReader(audioFile.fileStream);
                    if (audioFile != audioFiles2.First())
                    {
                        CroppingVal += audioFile.CroppingLSamples;
                        CurrentOffset -= CroppingVal;

                        for (long i = 0; i < CroppingVal; i++)
                        {
                            float[] NewSample = WfR.ReadNextSampleFrame();
                            if (NewSample == null) NewSample = Enumerable.Repeat(0f, Format.Channels).ToArray();

                            float[] OldSample = (float[])CombinedSamples.GetValue(CurrentOffset);
                            if (OldSample == null) OldSample = Enumerable.Repeat(0f, Format.Channels).ToArray();

                            float[] CombinedSample = NewSample.Zip(OldSample, (x, y) => (x + y)).ToArray();
                            CombinedSamples.SetValue(CombinedSample, CurrentOffset);
                            CurrentOffset++;
                        }
                    }
                    for (long i = 0; i < WfR.SampleCount - CroppingVal; i++)
                    {
                        float[] Sample = WfR.ReadNextSampleFrame();
                        if (Sample == null) Sample = Enumerable.Repeat(0f, Format.Channels).ToArray();
                        CombinedSamples.SetValue(Sample, CurrentOffset);
                        CurrentOffset++;
                    }
                    CroppingVal = audioFile.CroppingRSamples;
                }

                SBAS.MainWindowController.CloseProgressViewer();

                MemoryStream retStream = new MemoryStream();
                MemoryStream retWave = new MemoryStream();
                WaveFileWriter Writer = new WaveFileWriter(retStream, Format);
                for (long i = 0; i < CombinedSamples.LongLength; i++)
                {
                    float[] Sample = (float[])CombinedSamples.GetValue(i);
                    if (Sample != null) Writer.WriteSamples(Sample, 0, Sample.Length);
                }
                retStream.Seek(0, SeekOrigin.Begin);
                RawSourceWaveStream retRaw = new RawSourceWaveStream(retStream, Format);
                retRaw.Seek(0, SeekOrigin.Begin);
                WaveFileWriter.WriteWavFileToStream(retWave, retRaw);

                AudioFile ret = new AudioFile(retWave, Format) { AutoCropping = autoCropping };
                ret.Initialise();
                return ret;
            }

            internal void CalculateAutoCropping()
            {
                fileStream.Seek(0, SeekOrigin.Begin);

                WaveFileReader WfR = new WaveFileReader(fileStream);
                float Peak = 0;

                for (long i = 0; i < WfR.SampleCount; i++)
                {
                    float Sample = Math.Abs(WfR.ReadNextSampleFrame().Average());
                    if (Sample != 0 && Sample >= Peak) Peak = Sample;
                }

                WfR.Position = 0;

                bool LFinished = false;
                bool RFinished = false;
                for (long i = 0; i < WfR.SampleCount; i++)
                {
                    float Sample = Math.Abs(WfR.ReadNextSampleFrame().Average());
                    if (i % AutoCropResolution == 0) {
                        if (!LFinished && Sample / Peak > AutoCropThreshold)
                        {
                            LFinished = true;
                            AutoCroppingL = i;
                        }
                        else if (!RFinished && Sample / Peak < AutoCropThreshold)
                        {
                            RFinished = true;
                            AutoCroppingR = (WfR.SampleCount) - i;
                        }
                        else if (RFinished && Sample / Peak > AutoCropThreshold) RFinished = false;
                    }
                }
            }

            public DataFile GetDataFile()
            {
                if (IsInDataFile) return ParentProject.DataFiles.Find(x => x.AudioFiles.Contains(this));
                else return null;
            }

            public override string ToString()
            {
                return FileName;
            }
        }

        public class Sentence
        {
            public List<String> Strings = new List<String>();
            public string Source;
            Project ParentProject;
            public bool Valid = true;
            public int ErrorIndex = 0;

            public Sentence(string sentence, Project parentProject)
            {
                ParentProject = parentProject;
                Source = sentence;
                List<string> Words = sentence.GetWords().ToList();
                while (Words.Count > 0 && !ParentProject.Strings.Select(x => x.Text.ToLowerInvariant()).Any(x => string.Concat(Words).EndsWith(x)))
                {
                    Words.RemoveAt(Words.Count - 1);
                }

                List<String> ret = ParentProject.Strings.Where(x => Words.Count > x.Words.Length).ToList();
                string tempstring = string.Concat(Words);
                bool Found = false;
                List<String> newret1 = new List<String>();
                List<String> newret2 = new List<String>();
                Dictionary<String, int> BannedStrings = new Dictionary<String, int>();
                int Attempts = 0;

                while (true)
                {
                    String[] FoundStrings = ret.Where(x =>tempstring.EndsWith(string.Concat(x.Words)) && (!BannedStrings.Keys.Contains(x) && !BannedStrings.Any(y => y.Key == x && y.Value == ret.FindIndex(z => z == x)))).ToArray();
                    if (FoundStrings.Length == 0)
                    {
                            if (newret1.Count == 0 || Attempts > 3) break;
                            Attempts++;
                            BannedStrings.Add(newret1.ToArray().Reverse().Last(), newret1.Count);
                            newret1.Clear();
                            tempstring = string.Concat(Words);
                    }
                    else
                    {
                        String FoundString = FoundStrings.OrderBy(x => x.Words.Length).Last();
                        int FoundStringLength = string.Concat(FoundString.Words).Length;
                        tempstring = tempstring.Remove(tempstring.Length - FoundStringLength, FoundStringLength);
                        newret1.Add(FoundString);

                        if (tempstring.Length == 0)
                        {
                            if (string.Concat(newret1.Select(x => string.Concat(x.Words).ToLower()).Reverse()) == string.Concat(Words))
                            {
                                Found = true;
                            }
                            break;
                        }
                    }
                }

                while (true)
                {
                    String[] FoundStrings = ret.Where(x => tempstring.StartsWith(string.Concat(x.Words)) && (!BannedStrings.Keys.Contains(x) && !BannedStrings.Any(y => y.Key == x && y.Value == ret.FindIndex(z => z == x)))).ToArray();
                    if (FoundStrings.Length == 0)
                    {
                        if (newret2.Count == 0 || Attempts > 3) break;
                        Attempts++;
                        BannedStrings.Add(newret2.Last(), newret2.Count);
                        newret2.Clear();
                        tempstring = string.Concat(Words);
                    }
                    else
                    {
                        String FoundString = FoundStrings.OrderBy(x => x.Words.Length).Last();
                        int FoundStringLength = string.Concat(FoundString.Words).Length;
                        tempstring = tempstring.Remove(tempstring.Length - FoundStringLength, FoundStringLength);
                        newret2.Add(FoundString);

                        if (tempstring.Length == 0)
                        {
                            if (string.Concat(newret2.Select(x => string.Concat(x.Words).ToLower())) == string.Concat(Words))
                            {
                                Found = true;
                            }
                            break;
                        }
                    }
                }

                if (Found)
                {
                    if (newret1.Count > 0 && newret2.Count == 0) Strings = newret1.ToArray().Reverse().ToList();
                    else if(newret2.Count > 0 && newret1.Count == 0) Strings = newret2;
                    else if (newret1.Select(x => x.Words.Length).Average() > newret2.Select(x => x.Words.Length).Average()) Strings = newret1.ToArray().Reverse().ToList();
                    else Strings = newret2;
                }
                else
                {
                    Valid = false;
                    ErrorIndex = string.Concat(Words).Length - tempstring.Length;
                }
            }

            public void Save(string dir)
            {
                if (Valid)
                {
                    List<AudioFile> audioFiles = new List<AudioFile>();
                    foreach (String str in Strings) audioFiles.Add(str.GetAudioFile());
                    AudioFile MainAudio = AudioFile.Combine(audioFiles.ToArray());
                    FileStream SavedAudio = File.Open(dir, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    MainAudio.fileStream.Position = 0;
                    MainAudio.fileStream.CopyTo(SavedAudio);
                    SavedAudio.Dispose();
                }
            }

            public void Play()
            {
                if (Valid)
                {
                    List<AudioFile> audioFiles = new List<AudioFile>();
                    foreach (String str in Strings) audioFiles.Add(str.GetAudioFile());
                    AudioFile MainAudio = AudioFile.Combine(audioFiles.ToArray());
                    MainAudio.Play();
                }
            }
        }

        public class Line
        {
            public class Stop
            {
                public String Name;
                public Project ParentProject;

                public Stop(String Name, Project ParentProject)
                {
                    this.Name = Name;
                    this.ParentProject = ParentProject;
                }

                internal Stop() { }

                public Line GetParentLine()
                {
                    return ParentProject.Lines.Find(x => x.Stops.Contains(this));
                }

                public override string ToString()
                {
                    return Name.ID;
                }
            }

            public string Name;
            public Project ParentProject;
            public List<Stop> Stops = new List<Stop>();

            public Line BranchesFrom;
            internal string BranchesFromStr;
            public Stop BranchesAt;
            internal string BranchesAtStr;
            public Line ConnectsTo;
            internal string ConnectsToStr;
            public Stop ConnectsAt;
            internal string ConnectsAtStr;

            public Line(string Name, Project ParentProject)
            {
                this.Name = Name;
                this.ParentProject = ParentProject;
            }

            internal Line() { }

            internal void Initialise()
            {
                if (BranchesFromStr != null) BranchesFrom = ParentProject.Lines.Find(x => x.Name == BranchesFromStr);
                if (ConnectsToStr != null) ConnectsTo = ParentProject.Lines.Find(x => x.Name == ConnectsToStr);

                if (BranchesAtStr != null) BranchesAt = ParentProject.Stops.Find(x => x.Name.ID == BranchesAtStr);
                if (ConnectsAtStr != null) ConnectsAt = ParentProject.Stops.Find(x => x.Name.ID == ConnectsAtStr);
            }

            public String GetLineString()
            {
                return ParentProject.Strings.Find(x => x.Text == Name);
            }

            public Line[] GetBranches()
            {
                return ParentProject.Lines.Where(x => x.BranchesFrom == this || x.ConnectsTo == this).ToArray();
            }

            public void MoveStopUp(Stop stop)
            {
                if (!Stops.Contains(stop)) return;
                if (Stops.IndexOf(stop) == 0) return;

                int StopIndex = Stops.IndexOf(stop);
                Stops.Remove(stop);
                Stops.Insert(StopIndex - 1, stop);
            }

            public void MoveStopDown(Stop stop)
            {
                if (!Stops.Contains(stop)) return;
                if (Stops.IndexOf(stop) == Stops.Count - 1) return;

                int StopIndex = Stops.IndexOf(stop);
                Stops.Remove(stop);
                Stops.Insert(StopIndex + 1, stop);
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public void Save()
        {
            //DataFile LinkedDataFile = DataFiles.Find(x => x.fileStream.Name == fileStream.Name);

            string Dir = fileStream.Name;
            string TempDir = System.String.Concat(Dir, ".temp");
            File.Copy(Dir, TempDir);

            SaveFile(Dir);

            /*if (LinkedDataFile != null)
            {
                FileStream fs = File.Open(TempDir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                fs.CopyBytes(fileStream, LinkedDataFile.Offset, LinkedDataFile.Length);
                fs.Close();
            }*/

            File.Delete(TempDir);

        }

        public void SaveAs(string NewDir)
        {
            //if (File.Exists(NewDir)) throw new FileNotFoundException();

            //DataFile LinkedDataFile = DataFiles.Find(x => x.fileStream.Name == fileStream.Name);

            SaveFile(NewDir);
            MainDataFile.Dir = NewDir;
            MainDataFile.Initialise();

            /*if (LinkedDataFile != null)
            {
                FileStream fs = File.Open(Dir, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                fs.CopyBytes(fileStream, LinkedDataFile.Offset, LinkedDataFile.Length);
                fs.Close();
            }*/
        }

        void SaveFile(string Dir)
        {
            MemoryStream DataFile = new MemoryStream();
            if (fileStream != null)
            {
                fileStream.Position = 0;
                BinaryReader br = new BinaryReader(fileStream);

                long DataOffset = br.FindFirst("SBASDATA");
                if (DataOffset != -1)
                {
                    fileStream.Position = DataOffset;
                    fileStream.CopyTo(DataFile);
                }
                else fileStream.Position = 0;
            }
            fileStream = File.Open(Dir, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);

            BinaryWriter bw = new BinaryWriter(fileStream);

            List<byte> Header = new List<byte>();
            Header.AddRange(Encoding.ASCII.GetBytes("SBASPROJ"));
            Header.AddRange(BitConverter.GetBytes((short)3));
            Header.AddRange(BitConverter.GetBytes((short)Name.Length));
            Header.AddRange(Encoding.ASCII.GetBytes(Name));
            int StringTableOffset = Header.Count + 18;
            short StringTableRows = (short)Strings.Count;
            int AudioTableOffset;
            short AudioTableRows = (short)AudioFiles.Count;
            int DataFileTableOffset;
            short DataFileTableRows = (short)DataFiles.Count;
            int LineTableOffset;
            short LineTableRows = (short)Lines.Count;

            List<byte> StringTable = new List<byte>();
            StringTable.AddRange(Encoding.ASCII.GetBytes("SBASSTRT"));
            foreach (String str in Strings)
            {
                StringTable.AddRange(BitConverter.GetBytes((short)str.ID.Length));
                StringTable.AddRange(Encoding.ASCII.GetBytes(str.ID));
                StringTable.AddRange(BitConverter.GetBytes((short)str.Text.Length));
                StringTable.AddRange(Encoding.ASCII.GetBytes(str.Text));
                StringTable.AddRange(BitConverter.GetBytes((short)str.Tags.Count));
                foreach (string tag in str.Tags)
                {
                    StringTable.AddRange(BitConverter.GetBytes((short)tag.Length));
                    StringTable.AddRange(Encoding.ASCII.GetBytes(tag));
                }
                StringTable.AddRange(BitConverter.GetBytes(str.action.value));
                if (str.action.value == String.Actions.Delay.value) StringTable.AddRange(BitConverter.GetBytes(((String.Action.Delay)str.action).DelayLength));
                else if (str.action.value == String.Actions.Audio.value)
                {
                    StringTable.AddRange(Encoding.ASCII.GetBytes(((String.Action.Audio)str.action).MainAudioHash));
                    StringTable.AddRange(Encoding.ASCII.GetBytes(((String.Action.Audio)str.action).EndAudioHash));
                    StringTable.AddRange(BitConverter.GetBytes(((String.Action.Audio)str.action).AllowPhrasing));
                }
            }

            List<byte> AudioTable = new List<byte>();
            AudioTable.AddRange(Encoding.ASCII.GetBytes("SBASAUDT"));
            foreach (AudioFile aud in AudioFiles)
            {
                AudioTable.AddRange(BitConverter.GetBytes(aud.FullDir.Length));
                AudioTable.AddRange(Encoding.ASCII.GetBytes(aud.FullDir));
                AudioTable.AddRange(BitConverter.GetBytes(aud.Offset));
                AudioTable.AddRange(BitConverter.GetBytes(aud.Length));
                AudioTable.AddRange(Encoding.ASCII.GetBytes(aud.Hash));
                AudioTable.AddRange(BitConverter.GetBytes(aud.AutoCropping));
                AudioTable.AddRange(BitConverter.GetBytes(aud.ManCroppingL));
                AudioTable.AddRange(BitConverter.GetBytes(aud.ManCroppingR));
            }

            List<byte> DataFileTable = new List<byte>();
            DataFileTable.AddRange(Encoding.ASCII.GetBytes("SBASDATT"));
            foreach (DataFile dat in DataFiles)
            {
                DataFileTable.AddRange(BitConverter.GetBytes(dat.fileStream.Name.Length));
                DataFileTable.AddRange(Encoding.ASCII.GetBytes(dat.fileStream.Name));
            }

            List<byte> LineTable = new List<byte>();
            LineTable.AddRange(Encoding.ASCII.GetBytes("SBASLINT"));
            foreach (Line line in Lines)
            {
                LineTable.AddRange(BitConverter.GetBytes((short)line.Name.Length));
                LineTable.AddRange(Encoding.ASCII.GetBytes(line.Name));

                if (line.BranchesFrom != null)
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)line.BranchesFrom.Name.Length));
                    LineTable.AddRange(Encoding.ASCII.GetBytes(line.BranchesFrom.Name));
                }
                else
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)0));
                }

                if (line.BranchesAt != null)
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)line.BranchesAt.Name.ID.Length));
                    LineTable.AddRange(Encoding.ASCII.GetBytes(line.BranchesAt.Name.ID));
                }
                else
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)0));
                }

                if (line.ConnectsTo != null)
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)line.ConnectsTo.Name.Length));
                    LineTable.AddRange(Encoding.ASCII.GetBytes(line.ConnectsTo.Name));
                }
                else
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)0));
                }

                if (line.ConnectsAt != null)
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)line.ConnectsAt.Name.ID.Length));
                    LineTable.AddRange(Encoding.ASCII.GetBytes(line.ConnectsAt.Name.ID));
                }
                else
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)0));
                }

                LineTable.AddRange(BitConverter.GetBytes((short)line.Stops.Count));

                foreach (Line.Stop stop in line.Stops)
                {
                    LineTable.AddRange(BitConverter.GetBytes((short)stop.Name.ID.Length));
                    LineTable.AddRange(Encoding.ASCII.GetBytes(stop.Name.ID));
                }
            }

            AudioTableOffset = StringTableOffset + StringTable.Count;
            DataFileTableOffset = AudioTableOffset + AudioTable.Count;
            LineTableOffset = DataFileTableOffset + DataFileTable.Count;

            Header.AddRange(BitConverter.GetBytes(StringTableOffset));
            Header.AddRange(BitConverter.GetBytes(StringTableRows));
            Header.AddRange(BitConverter.GetBytes(AudioTableOffset));
            Header.AddRange(BitConverter.GetBytes(AudioTableRows));
            Header.AddRange(BitConverter.GetBytes(DataFileTableOffset));
            Header.AddRange(BitConverter.GetBytes(DataFileTableRows));
            Header.AddRange(BitConverter.GetBytes(LineTableOffset));
            Header.AddRange(BitConverter.GetBytes(LineTableRows));

            bw.Write(Header.ToArray());
            bw.Write(StringTable.ToArray());
            bw.Write(AudioTable.ToArray());
            bw.Write(DataFileTable.ToArray());
            bw.Write(LineTable.ToArray());

            if (MainDataFile != null) MainDataFile.Offset = fileStream.Length;

            if (DataFile.Length > 0) DataFile.WriteTo(fileStream);
            DataFile.Dispose();

            fileStream.SetLength(fileStream.Position);
        }

        public override string ToString()
        {
            return fileStream != null ? Path.GetFileName(fileStream.Name) : Name;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                handle.Dispose();

                int Counter = 0;
                SBAS.MainWindowController.ShowProgressViewer("Closing Project...", AudioFiles.Count + DataFiles.Count);

                foreach (AudioFile aud in AudioFiles)
                {
                    aud.fileStream.Dispose();
                    SBAS.MainWindowController.UpdateProgress(Counter++, "Cleaning up Audio Files");
                }

                foreach (DataFile dat in DataFiles)
                {
                    dat.fileStream.Dispose();
                    SBAS.MainWindowController.UpdateProgress(Counter++, "Cleaning up Data Files");
                }

                SBAS.MainWindowController.CloseProgressViewer();
            }

            disposed = true;
        }
    }
}
