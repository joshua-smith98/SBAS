using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SBAS
{
    public partial class EditorOLD : Form
    {
        bool Refreshing = false;

        public EditorOLD()
        {
            InitializeComponent();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "SBAS Projects (*.sbp)|*.sbp",
                OverwritePrompt = true,
                Title = "Create New SBAS Project...",
                CheckFileExists = false,
                InitialDirectory = Directory.GetCurrentDirectory(),
                ValidateNames = true,
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SBAS.MainController.NewProject(saveFileDialog.FileName, Path.GetFileNameWithoutExtension(saveFileDialog.FileName));
                RefreshAll();
            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "SBAS Projects (*.sbp)|*.sbp",
                CheckFileExists = true,
                Multiselect = false,
                Title = "Open SBAS Project File...",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SBAS.MainController.OpenProject(openFileDialog.FileName);
                RefreshAll();
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SBAS.MainController.SaveProject();
        }

        private void saveProjectAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "SBAS Projects (*.sbp)|*.sbp",
                OverwritePrompt = true,
                Title = "Save SBAS Project As...",
                CheckFileExists = false,
                InitialDirectory = Directory.GetCurrentDirectory(),
                ValidateNames = true,
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SBAS.MainController.SaveProjectAs(saveFileDialog.FileName);
            }
        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes to current project?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes) SBAS.MainController.SaveProject();

            if (result != DialogResult.Cancel)
            {
                SBAS.MainController.CloseProject();
                SBAS.MainController.ProjectClosed.WaitOne();
                RefreshAll();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes to current project?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes) SBAS.MainController.SaveProject();

            if (result != DialogResult.Cancel)
            {
                Application.Exit();
            }
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Save changes to current project?", "Save Changes?", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes) SBAS.MainController.SaveProject();

            if (result != DialogResult.Cancel)
            {
                Application.Exit();
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region Refresh Functions

        private void RefreshAll()
        {
            if (SBAS.MainController.CurrentProject != null)
            {
                SBASEditorContent.Enabled = true;

                ProjectName.Text = SBAS.MainController.CurrentProject.Name;

                RefreshDataFileList();
                RefreshStringTree();
                RefreshTagList();
                RefreshStringList();
                RefreshAudioBrowser();
            }
            else
            {
                ClearAll();
                SBASEditorContent.Enabled = false;
            }
        }

        private void RefreshDataFileList()
        {
            Refreshing = true;
            if (SBAS.MainController.CurrentProject.DataFiles.Count > 0)
            {
                List<Project.DataFile> SelectedFiles = new List<Project.DataFile>();
                List<Project.DataFile> ReferenceList = DataFileList.Items.Cast<Project.DataFile>().ToList();
                SelectedFiles.AddRange(DataFileList.SelectedItems.Cast<Project.DataFile>());

                DataFileList.Items.Synchronise(SBAS.MainController.CurrentProject.DataFiles);

                /*if (SBAS.MainController.CurrentProject.DataFiles.Exists(x => !ReferenceList.Contains(x)))
                {
                    List<Project.DataFile> NewFiles = SBAS.MainController.CurrentProject.DataFiles.FindAll(x => !ReferenceList.Contains(x));

                    DataFileList.Items.AddRange(NewFiles.ToArray());
                }

                if (ReferenceList.Exists(x => !SBAS.MainController.CurrentProject.DataFiles.Contains(x)))
                {
                    List<Project.DataFile> OldFiles = ReferenceList.FindAll(x => !SBAS.MainController.CurrentProject.DataFiles.Contains(x));
                    foreach (Project.DataFile dat in OldFiles) DataFileList.Items.Remove(dat);
                }*/

                DataFileList.Sorted = true;

                for (int i = 0; i < DataFileList.Items.Count; i++) DataFileList.SetSelected(i, SelectedFiles.Contains(DataFileList.Items[i]) ? true : false);

            }
            else DataFileList.Items.Clear();

            Refreshing = false;

            RefreshDataFileListButtons();
        }

        private void RefreshDataFileListButtons()
        {
            Refreshing = true;
            ProjectNewDataFile.Enabled = true;
            ProjectImportDataFile.Enabled = true;

            if (DataFileList.SelectedItems.Count > 0)
            {
                ProjectDeleteDataFile.Enabled = true;

                if (DataFileList.SelectedItems.Count > 1)
                {
                    ProjectMoveDataFile.Enabled = false;
                    ProjectRenameDataFile.Enabled = false;
                }
                else
                {
                    ProjectMoveDataFile.Enabled = true;
                    ProjectRenameDataFile.Enabled = true;
                }
            }
            else
            {
                ProjectDeleteDataFile.Enabled = false;
                ProjectMoveDataFile.Enabled = false;
                ProjectRenameDataFile.Enabled = false;
            }
            Refreshing = false;
        }

        private void RefreshStringTree()
        {
            StringTree.Enabled = false;
        }

        private void TrueRefreshStringTree()
        {
            Refreshing = true;
            StringTree.Nodes.Clear();

            SBAS.MainWindowController.ShowProgressViewer("Refreshing String Tree...", SBAS.MainController.CurrentProject.Strings.Count);
            List<Project.String> InitialStrings = new List<Project.String>();
            int Counter = 0;
            foreach (Project.String str in SBAS.MainController.CurrentProject.Strings)
            {
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Analysing String: {0}", str.ID));
                if (str.GetParents().Length == 0 && str.GetChildren().Length != 0) InitialStrings.Add(str);
            }

            SBAS.MainWindowController.CloseProgressViewer();

            SBAS.MainWindowController.ShowProgressViewer("Refreshing String Tree...", InitialStrings.Count);
            Counter = 0;
            foreach (Project.String str in InitialStrings)
            {
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Analysing String: {0}", str.ID));
                TreeNode NewNode = new TreeNode()
                {
                    Text = str.Text,
                    Name = str.ID,
                    Tag = str,
                };
                StringTree.Nodes.Add(NewNode);

                RefreshStringTreeRecursive(NewNode, str.GetChildren());
            }
            SBAS.MainWindowController.CloseProgressViewer();

            void RefreshStringTreeRecursive(TreeNode CurrentNode, Project.String[] CurrentStrings)
            {
                foreach (Project.String str in CurrentStrings)
                {
                    TreeNode NewNode = new TreeNode()
                    {
                        Text = str.Text,
                        Name = str.ID,
                        Tag = str,
                    };
                    CurrentNode.Nodes.Add(NewNode);
                    RefreshStringTreeRecursive(NewNode, str.GetChildren());
                }
            }
            Refreshing = false;
        }

        private void RefreshTagList()
        {
            Refreshing = true;
            if (SBAS.MainController.CurrentProject.Strings.Count > 0)
            {
                List<string> SelectedTags = TagList.SelectedItems.Cast<string>().ToList();
                List<string> CollatedTags = SBAS.MainController.CurrentProject.Strings.SelectMany(x => x.Tags).Distinct().ToList();
                CollatedTags.Add("[none]");
                CollatedTags.Add("[untagged]");

                TagList.Items.Synchronise(CollatedTags);

                TagList.Sorted = true;

                for (int i = 0; i < TagList.Items.Count; i++) TagList.SetSelected(i, SelectedTags.Contains(TagList.Items[i]) ? true : false);
            }
            else
            {
                TagList.Items.Clear();
            }
            Refreshing = false;

            RefreshStringList();
        }

        private void RefreshStringList()
        {
            Refreshing = true;
            if (SBAS.MainController.CurrentProject != null && SBAS.MainController.CurrentProject.Strings.Count > 0)
            {
                List<string> SelectedTags = TagList.SelectedItems.Cast<string>().ToList();
                List<Project.String> SelectedStrings = StringList.SelectedItems.Cast<Project.String>().ToList();
                List<Project.String> FilteredStrings = SBAS.MainController.CurrentProject.Strings;

                if (SelectedTags.Count > 0 && SelectedTags[0] == "[untagged]") FilteredStrings = FilteredStrings.Where(x => x.Tags.Count == 0).ToList();
                else if (SelectedTags.Count > 0 && SelectedTags[0] != "[none]") FilteredStrings = FilteredStrings.Where(x => SelectedTags.All(y => x.Tags.Contains(y)) && x.Tags.Count != 0).ToList();

                if (StringListFilter.Text != String.Empty) FilteredStrings = FilteredStrings.Where(x => x.ToString().ToLower().StartsWith(StringListFilter.Text.ToLower())).ToList();

                StringList.Items.Synchronise(FilteredStrings);
                StringList.RefreshSelected();

                StringList.Sorted = true;

                for (int i = 0; i < StringList.Items.Count; i++) StringList.SetSelected(i, SelectedStrings.Contains(StringList.Items[i]) ? true : false);

            }
            else StringList.Items.Clear();
            Refreshing = false;

            RefreshStringProperties();
        }

        private void RefreshStringProperties()
        {
            Refreshing = true;
            NewStringButton.Enabled = true;

            if (StringList.SelectedItems.Count > 0)
            {
                StringProperties.Enabled = true;

                if (StringList.SelectedItems.Count > 1)
                {
                    Project.String[] SelectedStrings = StringList.SelectedItems.Cast<Project.String>().ToArray();
                    StringID.Enabled = false;
                    StringID.Text = "Multiple Values";
                    StringText.Enabled = false;
                    StringText.Text = "Multiple Values";
                    StringActionGroup.Enabled = false;

                    List<string> Tags = SelectedStrings.SelectMany(x => x.Tags).Where(x => SelectedStrings.All(y => y.Tags.Contains(x))).Distinct().ToList();

                    StringTags.Items.Synchronise(Tags);
                    StringTags.Sorted = true;
                    StringNewTag.Enabled = true;
                    if (StringTags.SelectedItems.Count > 0) StringDeleteTag.Enabled = true;
                }
                else
                {
                    StringActionGroup.Enabled = true;
                    Project.String SelectedString = (Project.String)StringList.SelectedItems[0];
                    StringID.Enabled = true;
                    StringID.Text = SelectedString.ID;
                    StringText.Enabled = true;
                    StringText.Text = SelectedString.Text;

                    List<string> SelectedTags = StringTags.SelectedItems.Cast<string>().ToList();
                    StringTags.Items.Synchronise(SelectedString.Tags);
                    StringTags.Sorted = true;
                    for (int i = 0; i < StringTags.Items.Count; i++) StringTags.SetSelected(i, SelectedTags.Contains(StringTags.Items[i]) ? true : false);
                    StringNewTag.Enabled = true;
                    if (StringTags.SelectedItems.Count > 0) StringDeleteTag.Enabled = true;

                    if (SelectedString.action.GetType() == typeof(Project.String.Action.Audio))
                    {
                        Project.String.Action.Audio LinkedAudio = (Project.String.Action.Audio)SelectedString.action;
                        StringAction.SelectedItem = "Audio";
                        StringDelayLength.Value = 0;
                        StringDelayLength.Enabled = false;

                        StringMainAudio.Text = LinkedAudio.MainAudio != null ? LinkedAudio.MainAudio.FileName : String.Empty;
                        StringMainAudioLink.Enabled = true;
                        StringMainAudioAutoLink.Enabled = true;

                        StringEndAudio.Text = LinkedAudio.EndAudio != null ? LinkedAudio.EndAudio.FileName : String.Empty;
                        StringEndAudioLink.Enabled = true;
                        StringEndAudioUnlink.Enabled = LinkedAudio.EndAudio == null ? false : true;

                        StringAllowPhrasing.Enabled = true;
                        StringAllowPhrasing.Checked = LinkedAudio.AllowPhrasing;
                        if (LinkedAudio.AllowPhrasing) StringPhrasingLabel.Text = String.Format("Found {0} Sublabels in {1} Layers", 0, 0);
                        else StringPhrasingLabel.Text = "Phrasing Disabled";
                    }
                    else
                    {
                        StringMainAudio.Text = String.Empty;
                        StringMainAudioLink.Enabled = false;
                        StringMainAudioAutoLink.Enabled = false;

                        StringEndAudio.Text = String.Empty;
                        StringEndAudioLink.Enabled = false;
                        StringEndAudioUnlink.Enabled = false;

                        StringAllowPhrasing.Enabled = false;
                        StringPhrasingLabel.Text = "Phrasing Unavailable";

                        if (SelectedString.action.GetType() == typeof(Project.String.Action.Delay))
                        {
                            StringAction.SelectedItem = "Delay (ms)";
                            StringDelayLength.Value = ((Project.String.Action.Delay)SelectedString.action).DelayLength;
                            StringDelayLength.Enabled = true;
                            StringAllowPhrasing.Checked = false;
                        }
                        else
                        {
                            StringAction.SelectedItem = "Group";
                            StringDelayLength.Value = 0;
                            StringDelayLength.Enabled = false;
                            StringAllowPhrasing.Checked = true;
                        }
                    }
                }
            }
            else
            {
                StringProperties.Enabled = false;
            }
            Refreshing = false;
        }

        private void RefreshAudioBrowser()
        {
            Refreshing = true;
            object SelectedFile = AudioBrowser.SelectedNode != null ? AudioBrowser.SelectedNode.Tag : null;

            Dictionary<Project.AudioFile, string> AudioPaths = SBAS.MainController.CurrentProject.AudioFiles.ToDictionary(x => x, x => x.FullDir);

            AudioBrowser.Synchronise(AudioPaths);

            AudioBrowser.SelectedNode = SelectedFile != null ? AudioBrowser.GetAllNodes().ToList().Find(x => x.Tag == SelectedFile) : null;

            if (AudioBrowser.Nodes.Count == 1)
            {
                TreeNode CurrentNode = AudioBrowser.Nodes[0];
                CurrentNode.Expand();

                while (CurrentNode.Nodes.Count == 1)
                {
                    CurrentNode = CurrentNode.Nodes[0];
                    CurrentNode.Expand();
                }
            }

            Refreshing = false;
        }

        private void RefreshAudioProperties()
        {
            Refreshing = true;

            Project.AudioFile[] SelectedAudio = GetSelectedAudio();

            if (AudioBrowser.SelectedNode != null && SelectedAudio.Length > 0)
            {
                if (SelectedAudio.Length > 1)
                {
                    if (SelectedAudio.All(x => x.OffsetL == SelectedAudio.First().OffsetL)) AudioOffsetL.Value = SelectedAudio.First().OffsetL;
                    else AudioOffsetL.Value = 0;

                    if (SelectedAudio.All(x => x.OffsetR == SelectedAudio.First().OffsetR)) AudioOffsetR.Value = SelectedAudio.First().OffsetR;
                    else AudioOffsetR.Value = 0;

                    if (SelectedAudio.All(x => x.AutoCroppingEnabled == SelectedAudio.First().AutoCroppingEnabled)) AudioAutoCropping.Checked = SelectedAudio.First().AutoCroppingEnabled;
                    else AudioAutoCropping.CheckState = CheckState.Indeterminate;

                    AudioAutoCroppingVal.Enabled = false;
                    AudioAutoCroppingVal.Text = "Multiple Files Selected";
                }
                else
                {
                    Project.AudioFile SelectedFile = SelectedAudio.First();

                    AudioOffsetL.Value = SelectedFile.OffsetL;
                    AudioOffsetR.Value = SelectedFile.OffsetR;
                    AudioAutoCropping.Checked = SelectedFile.AutoCroppingEnabled;
                    AudioAutoCroppingVal.Enabled = true;
                    AudioAutoCroppingVal.Text = String.Format("Start: {0} End: {1}", SelectedFile.CroppingL - SelectedFile.ManCroppingL, SelectedFile.CroppingR - SelectedFile.ManCroppingR);
                }

                AudioExportFiles.Enabled = true;
                AudioExportDataFile.Enabled = true;
                AudioBrowserNewFolder.Enabled = true;
                AudioBrowserDelete.Enabled = true;
                AudioBrowserRename.Enabled = true;
                AudioOffsetL.Enabled = true;
                AudioOffsetR.Enabled = true;
                AudioAutoCropping.Enabled = true;
            }
            else
            {
                AudioOffsetL.Enabled = false;
                AudioOffsetL.Value = 0;
                AudioOffsetR.Enabled = false;
                AudioOffsetR.Value = 0;
                AudioAutoCropping.Enabled = false;
                AudioAutoCropping.Checked = false;
                AudioExportFiles.Enabled = false;
                AudioExportDataFile.Enabled = false;
                AudioBrowserNewFolder.Enabled = false;
                AudioBrowserDelete.Enabled = false;
                AudioBrowserRename.Enabled = false;
                AudioAutoCroppingVal.Enabled = false;
                AudioAutoCroppingVal.Text = "No Files Selected";
            }

            AudioImportFiles.Enabled = true;
            AudioImportFolders.Enabled = true;
            Refreshing = false;
        }

        private void ClearAll()
        {
            Refreshing = true;
            ProjectName.Clear();
            DataFileList.Items.Clear();
            StringTree.Nodes.Clear();
            TagList.Items.Clear();
            StringList.Items.Clear();
            StringListFilter.Clear();
            StringID.Clear();
            StringText.Clear();
            StringTags.Items.Clear();
            StringAction.SelectedItem = "";
            StringDelayLength.Value = 0;
            StringMainAudio.Clear();
            StringEndAudio.Clear();
            StringAllowPhrasing.Checked = false;
            StringPhrasingLabel.Text = "Phrasing Unavailable";
            AudioBrowser.Nodes.Clear();
            Refreshing = false;
        }

        #endregion

        #region Select Functions

        private Project.AudioFile[] GetSelectedAudio()
        {
            List<Project.AudioFile> ret = new List<Project.AudioFile>();

            if (AudioBrowser.SelectedNode != null)
            {
                if (AudioBrowser.SelectedNode.Tag == null)
                {
                    foreach (TreeNode node in AudioBrowser.SelectedNode.GetChildren())
                    {
                        if (node.Tag != null) ret.Add((Project.AudioFile)node.Tag);
                    }
                }
                else ret.Add((Project.AudioFile)AudioBrowser.SelectedNode.Tag);
            }

            return ret.ToArray();
        }

        #endregion

        private void ProjectName_TextChanged(object sender, EventArgs e)
        {
            if (ProjectName.Text != String.Empty) SBAS.MainController.CurrentProject.Name = ProjectName.Text;
        }

        private void ProjectNewDataFile_Click(object sender, EventArgs e)
        {

        }

        private void ProjectDeleteDataFile_Click(object sender, EventArgs e)
        {

        }

        private void ProjectImportDataFile_Click(object sender, EventArgs e)
        {

        }

        private void ProjectMoveDataFile_Click(object sender, EventArgs e)
        {

        }

        private void ProjectRenameDataFile_Click(object sender, EventArgs e)
        {

        }

        private void NewStringButton_Click(object sender, EventArgs e)
        {
            Namer name = new Namer(SBAS.MainController.CurrentProject.Strings.Select(x => x.ID).ToArray());
            name.ShowDialog();

            if (name.DialogResult == DialogResult.OK)
            {
                SBAS.MainController.NewString(name.NameResult);
                RefreshTagList();
                RefreshStringList();
                RefreshStringProperties();
                RefreshStringTree();
                if (StringList.SelectedItem != null) StringList.SetSelected(StringList.SelectedIndex, false);
                StringList.SelectedItem = SBAS.MainController.CurrentProject.Strings.Find(x => x.ID == name.NameResult);
            }
        }

        private void DeleteStringButton_Click(object sender, EventArgs e)
        {
            foreach (Project.String str in StringList.SelectedItems.Cast<Project.String>())
            {
                SBAS.MainController.CurrentProject.Strings.Remove(str);
            }
            RefreshAll();
        }

        private void StringList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Refreshing) return;
            RefreshStringProperties();
        }

        private void StringListFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshStringList();
        }

        private void StringID_TextChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                if (StringID.Text != String.Empty) ((Project.String)StringList.SelectedItem).ID = StringID.Text;
                RefreshStringList();
            }
        }

        private void StringText_TextChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                if (StringText.Text != String.Empty) ((Project.String)StringList.SelectedItem).Text = StringText.Text;
                RefreshStringList();
            }
        }

        private void StringNewTag_Click(object sender, EventArgs e)
        {
            ListNamer name = new ListNamer(SBAS.MainController.CurrentProject.Strings.SelectMany(x => x.Tags).Distinct().ToArray(), ((Project.String)StringList.SelectedItem).Tags.ToArray());
            name.ShowDialog();

            if (name.DialogResult == DialogResult.OK)
            {
                foreach (Project.String str in StringList.SelectedItems.Cast<Project.String>())
                {
                    str.Tags.Add(name.NameResult);
                }
                RefreshTagList();
                RefreshStringList();
                RefreshStringProperties();
                RefreshStringTree();
            }
        }

        private void StringDeleteTag_Click(object sender, EventArgs e)
        {
            if (StringTags.SelectedItem != null)
            {
                foreach (Project.String str in StringList.SelectedItems.Cast<Project.String>())
                {
                    str.Tags.Remove((string)StringTags.SelectedItem);
                }
                RefreshTagList();
                RefreshStringList();
                RefreshStringProperties();
                RefreshStringTree();
            }
        }

        private void StringTags_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TagList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                RefreshStringList();
            }
        }

        private void StringAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                if ((string)StringAction.SelectedItem == "Audio")
                {
                    ((Project.String)StringList.SelectedItem).action = new Project.String.Action.Audio();
                }
                else if ((string)StringAction.SelectedItem == "Delay (ms)")
                {
                    ((Project.String)StringList.SelectedItem).action = new Project.String.Action.Delay();
                }
                else
                {
                    ((Project.String)StringList.SelectedItem).action = Project.String.Actions.Group;
                }
                RefreshStringProperties();
            }
        }

        private void StringDelayLength_ValueChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                ((Project.String.Action.Delay)((Project.String)StringList.SelectedItem).action).DelayLength = (short)StringDelayLength.Value;
            }
        }

        private void StringAllowPhrasing_CheckedChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                ((Project.String.Action.Audio)((Project.String)StringList.SelectedItem).action).AllowPhrasing = StringAllowPhrasing.Checked;
                RefreshStringProperties();
            }
        }

        private void AudioImportFiles_Click(object sender, EventArgs e)
        {
            Importer importer = new Importer(false, false, "Pick a valid Audio File...", "wav");
            importer.ShowDialog();
            RefreshAll();
        }

        private void AudioImportFolders_Click(object sender, EventArgs e)
        {
            Importer importer = new Importer(false, true, "Pick a folder containing valid Audio Files...");
            importer.ShowDialog();
            RefreshAll();
        }

        private void AudioExportFiles_Click(object sender, EventArgs e)
        {

        }

        private void AudioExportDataFile_Click(object sender, EventArgs e)
        {

        }

        private void AudioBrowserNewFolder_Click(object sender, EventArgs e)
        {

        }

        private void AudioBrowserDelete_Click(object sender, EventArgs e)
        {
            Project.AudioFile[] SelectedAudio = GetSelectedAudio();

            if (SelectedAudio.Length > 0 && MessageBox.Show(String.Format("Are you sure you want to remove {0} Audio File{1}?", SelectedAudio.Length, SelectedAudio.Length > 1 ? "s" : ""), "Delete Audio", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (Project.AudioFile audio in SelectedAudio)
                {
                    SBAS.MainController.CurrentProject.AudioFiles.Remove(audio);
                }
            }

            RefreshAll();
        }

        private void AudioBrowserRename_Click(object sender, EventArgs e)
        {

        }

        private void AudioBrowser_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null) ((Project.AudioFile)e.Node.Tag).Play();
            RefreshAudioProperties();
        }

        private void AudioAutoCropping_CheckedChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                Project.AudioFile[] SelectedAudio = GetSelectedAudio();
                if (AudioAutoCropping.Checked)
                {
                    int Count = 0;
                    SBAS.MainWindowController.ShowProgressViewer("Processing Audio Files...", SelectedAudio.Length);
                    foreach (Project.AudioFile audio in SelectedAudio)
                    {
                        SBAS.MainWindowController.UpdateProgress(Count++, string.Format("Processing: {0}", audio.FileName));
                        audio.EnableAutoCropping();
                    }
                    SBAS.MainWindowController.CloseProgressViewer();
                }
                else if (!AudioAutoCropping.Checked)
                {
                    foreach (Project.AudioFile audio in SelectedAudio) audio.DisableAutoCropping();
                }

                RefreshAudioProperties();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (Refreshing) return;
            Project.AudioFile[] SelectedAudio = GetSelectedAudio();
            foreach (Project.AudioFile audio in SelectedAudio) audio.CroppingL = (int)AudioOffsetL.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (Refreshing) return;
            Project.AudioFile[] SelectedAudio = GetSelectedAudio();
            foreach (Project.AudioFile audio in SelectedAudio) audio.CroppingL = (int)AudioOffsetR.Value;
        }

        private void AudioBrowser_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag == null) e.CancelEdit = true;
            else
            {
                Project.AudioFile EditedFile = (Project.AudioFile)e.Node.Tag;
                if (!EditedFile.IsInDataFile) e.CancelEdit = true;
                e.CancelEdit = true; //Insert some code to change name if it's in a data file here
            }
        }

        private void previewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Previewer previewer = new Previewer();
            previewer.ShowDialog();
        }

        private void eXCEPTIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new Exception();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Recorder recorder = new Recorder();
            recorder.ShowDialog();
            RefreshAll();
        }

        private void StringTreeGroup_MouseHover(object sender, EventArgs e)
        {
            if (!StringTree.Enabled)
            {
                StringTree.Enabled = true;
                TrueRefreshStringTree();
            }
        }
    }
}
