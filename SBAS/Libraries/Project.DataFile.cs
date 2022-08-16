using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SBAS
{
    public partial class Project
    {
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
    }
}
