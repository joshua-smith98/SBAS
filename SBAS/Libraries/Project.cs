using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SBAS
{
    public partial class Project : IDisposable
    {

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
                string NewHash = BitConverter.ToString(MD5.Create().ComputeHash(aud.fileStream)).Replace("-", "").ToLowerInvariant();
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
