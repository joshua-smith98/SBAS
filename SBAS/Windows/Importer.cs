using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SBAS
{
    public partial class Importer : Form
    {
        bool IsFolder;
        bool FromDataFile;
        string AutoImport;
        bool QuickImport;
        Indexer.File[] Files;
        Indexer.Folder Folder;

        Project.AudioFile[] AudioFiles { get { return DataFiles.SelectMany(x => x.AudioFiles).ToArray(); } }
        List<Project.DataFile> DataFiles = new List<Project.DataFile>();
        int FoundFiles = 0;

        OpenFileDialog fileDialog = new OpenFileDialog() {
            CheckFileExists = true,
            CheckPathExists = true,
            InitialDirectory = Path.GetDirectoryName(SBAS.MainController.CurrentProject.fileStream.Name),
            Multiselect = true,
            RestoreDirectory = true,
        };
        FolderBrowserDialog folderDialog = new FolderBrowserDialog()
        {
            ShowNewFolderButton = true
        };

        public Importer(bool FromDataFile, bool IsFolder, string Description, string FileExtention = "", string autoImport = "", bool quickImport = false)
        {
            this.FromDataFile = FromDataFile;
            this.IsFolder = IsFolder;
            AutoImport = autoImport;
            QuickImport = quickImport;
            if (FromDataFile)
                fileDialog.Filter = "SBAS Data Files (*.sbd)|*.sbd";
            else
                fileDialog.Filter = String.Format("{0} files (*.{1})|*.{1}", FileExtention.ToUpper(), FileExtention.ToLower());
            fileDialog.Title = Description;
            folderDialog.Description = Description;
            InitializeComponent();

            if (FromDataFile)
            {
                TagSubfolders.Checked = false;
                TagSubfolders.Enabled = false;
            }
            else
            {
                DataGroup.Enabled = false;
                if (!IsFolder)
                {
                    TagSubfolders.Checked = false;
                    TagSubfolders.Enabled = false;
                }
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            if (FromDataFile && (FoundFiles >= AudioFiles.Length || MessageBox.Show(String.Format("Are you sure? {0} files will be unnamed!", AudioFiles.Length - FoundFiles), "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes))
            {
                foreach (Project.DataFile Dat in DataFiles)
                {
                    foreach (Project.AudioFile aud in Dat.AudioFiles)
                    {
                        if (aud.FullDir == String.Empty)
                        {
                            aud.FullDir = Path.Combine(Dat.Dir, aud.Hash);
                        }

                        aud.ParentProject = SBAS.MainController.CurrentProject;
                        SBAS.MainController.CurrentProject.AudioFiles.Add(aud);
                    }
                    Dat.ParentProject = SBAS.MainController.CurrentProject;
                    SBAS.MainController.CurrentProject.DataFiles.Add(Dat);
                    Dat.Initialise();
                    foreach (Project.AudioFile aud in Dat.AudioFiles) aud.Initialise();
                }

                Close();
            }
            else if (!FromDataFile)
            {
                Project.AudioFile CurrentFile;
                Project.String CurrentString;

                if (IsFolder)
                {
                    foreach (Indexer.File File in Folder.GetChildren(".wav"))
                    {
                        CurrentFile = new Project.AudioFile(File.Path, SBAS.MainController.CurrentProject);
                        if (AutoCropping.Checked) CurrentFile.EnableAutoCropping();
                        if (CreateStrings.Checked)
                        {
                            CurrentString = new Project.String()
                            {
                                ID = Path.GetFileNameWithoutExtension(CurrentFile.FullDir),
                                action = new Project.String.Action.Audio(CurrentFile),
                                ParentProject = SBAS.MainController.CurrentProject,
                                Tags = new List<string>(),
                            };

                            if (GenerateText.Checked)
                            {
                                CurrentString.Text = CurrentString.ID;
                            }

                            if (TagSubfolders.Checked)
                            {
                                CurrentString.Tags.AddRange(File.GetParents().Select(x => x.Path.Split('\\').Last()).Where(x => x != String.Empty));
                            }

                            SBAS.MainController.NewString(CurrentString);
                        }
                        SBAS.MainController.CurrentProject.AudioFiles.Add(CurrentFile);
                    }
                }
                else
                {
                    foreach (Indexer.File File in Files)
                    {
                        CurrentFile = new Project.AudioFile(File.Path, SBAS.MainController.CurrentProject);
                        if (AutoCropping.Checked) CurrentFile.EnableAutoCropping();
                        if (CreateStrings.Checked)
                        {
                            CurrentString = new Project.String()
                            {
                                ID = Path.GetFileNameWithoutExtension(CurrentFile.FullDir),
                                action = new Project.String.Action.Audio(CurrentFile),
                                ParentProject = SBAS.MainController.CurrentProject,
                                Tags = new List<string>(),
                            };

                            if (GenerateText.Checked)
                            {
                                CurrentString.Text = CurrentString.ID;
                            }

                            SBAS.MainController.NewString(CurrentString);
                        }

                        SBAS.MainController.CurrentProject.AudioFiles.Add(CurrentFile);
                    }
                }
                Close();
            }
        }

        private void CancelButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateStrings_CheckedChanged(object sender, EventArgs e)
        {
            if (!CreateStrings.Checked)
            {
                GenerateText.Checked = false;
                TagSubfolders.Checked = false;
                TagSubfolders.Enabled = false;
            }
            else if (IsFolder)
            {
                TagSubfolders.Enabled = true;
            }

            GenerateText.Enabled = CreateStrings.Checked;
        }

        private void Importer_Shown(object sender, EventArgs e)
        {
            if (FromDataFile)
            {
                if (fileDialog.ShowDialog() == DialogResult.Cancel) Close();
                else
                {
                    Files = fileDialog.FileNames.Select(x => new Indexer.File(x)).ToArray();

                    foreach (Indexer.File file in Files)
                    {
                        Project.DataFile NewDataFile = new Project.DataFile()
                        {
                            Dir = file.Path,
                        };
                        NewDataFile.Initialise();
                        DataFiles.Add(NewDataFile);
                    }

                    FileNumber.Text = String.Format("{0} Files Selected", AudioFiles.Length);
                    DataNamedLabel.Text = String.Format("0 of {0} Filenames Linked", AudioFiles.Length);
                }
            }
            else if (IsFolder)
            {
                if (folderDialog.ShowDialog() == DialogResult.Cancel) Close();
                else
                {
                    Folder = new Indexer.Folder(folderDialog.SelectedPath);
                    FileNumber.Text = String.Format("{0} Files Selected in {1} SubFolders", Folder.GetChildren(".wav").Length, Folder.GetSubFolders().Length);
                }
            }
            else if (File.Exists(AutoImport))
            {
                Files = new Indexer.File[1] { new Indexer.File(AutoImport) };
                FileNumber.Text = "Import Recorded File";
                if (QuickImport) ImportButton_Click(this, new EventArgs());
            }
            else
            {
                if (fileDialog.ShowDialog() == DialogResult.Cancel) Close();
                else
                {
                    Files = fileDialog.FileNames.Select(x => new Indexer.File(x)).ToArray();
                    FileNumber.Text = String.Format("{0} Files Selected", Files.Length);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ProjectFileDialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = Path.GetDirectoryName(SBAS.MainController.CurrentProject.fileStream.Name),
                Multiselect = false,
                RestoreDirectory = true,
                Filter = "SBAS Project Files (*.sbp)|*.sbp"
            };

            if (ProjectFileDialog.ShowDialog() == DialogResult.OK)
            {
                Project NewProject = new Project.Loader(ProjectFileDialog.FileName, false);
                SBAS.MainWindowController.ShowProgressViewer("Searching for Filenames...", NewProject.AudioFiles.Count);

                int Counter = 0;
                foreach (Project.AudioFile aud in NewProject.AudioFiles)
                {
                    Project.AudioFile LinkedFile = AudioFiles.ToList().Find(x => x.Hash == aud.Hash);
                    if (LinkedFile != null)
                    {
                        LinkedFile.FullDir = aud.FullDir;
                        LinkedFile.ManCroppingL = aud.ManCroppingL;
                        LinkedFile.ManCroppingR = aud.ManCroppingR;
                        DataNamedLabel.Text = String.Format("{0} of {1} Filenames Linked", FoundFiles++, AudioFiles.Length);
                    }
                    SBAS.MainWindowController.UpdateProgress(Counter++, "Searching for Filenames...");
                }
                SBAS.MainWindowController.CloseProgressViewer();
            }
        }
    }
}
