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
    public partial class Editor : Form
    {
        bool Refreshing = false;

        Project.Sentence PreviewSentence;

        public Editor()
        {
            InitializeComponent();
            RefreshAll();
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
                InitialDirectory = SBAS.MainController.CurrentProject.fileStream.Name,
                ValidateNames = true,
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                SBAS.MainController.SaveProjectAs(saveFileDialog.FileName);
                RefreshAll();
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
            Application.Exit();
        }

        private void exitToStarterToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
                ClearAll();
                EditorPages.Enabled = true;

                Text = SBAS.MainController.CurrentProject.Name;

                RefreshStringPhraseTree();
                RefreshTagList();
                RefreshStringList();
                RefreshAudioTreeView();
                RefreshPreview();
            }
            else
            {
                ClearAll();
                EditorPages.Enabled = false;
            }
        }

        private void RefreshStringPhraseTree()
        {
            StringPhraseTree.Enabled = false;
            StringPhrasesPage.Enabled = false;
        }

        private void TrueRefreshStringPhraseTree()
        {
            Refreshing = true;
            StringPhraseTree.Nodes.Clear();
            StringWordsList.Items.Clear();
            StringIncompletePhrasesList.Items.Clear();
            StringMissingWordsList.Items.Clear();
            Update();

            SBAS.MainWindowController.ShowProgressViewer("Building Phrase Tree...", SBAS.MainController.CurrentProject.Strings.Count);
            int Counter = 0;
            foreach (Project.String str in SBAS.MainController.CurrentProject.Strings)
            {
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Analysing String: {0}", str.ID));
                if (str.GetChildren().Length != 0)
                {
                    if (str.GetParents().Length == 0)
                    {
                        TreeNode NewNode = new TreeNode()
                        {
                            Text = str.Text,
                            Name = str.ID,
                            Tag = str,
                        };
                        StringPhraseTree.Nodes.Add(NewNode);
                    }
                }
                else if (str.Words.Length == 1) StringWordsList.Items.Add(str);
                else
                {
                    StringIncompletePhrasesList.Items.Add(str);
                    foreach (string word in str.Words.Where(x => !StringMissingWordsList.Items.Contains(x) && !SBAS.MainController.CurrentProject.Strings.Any(y => y.Text.ToLower() == x))) StringMissingWordsList.Items.Add(word);
                }
                Update();
            }

            SBAS.MainWindowController.CloseProgressViewer();

            SBAS.MainWindowController.ShowProgressViewer("Building Phrase Tree...", StringPhraseTree.Nodes.Count);
            Counter = 0;

            foreach (TreeNode node in StringPhraseTree.Nodes)
            {
                Project.String str = (Project.String)node.Tag;
                SBAS.MainWindowController.UpdateProgress(Counter++, string.Format("Analysing String: {0}", str.ID));
                RefreshStringPhraseTreeRecursive(node, str.GetChildren());
            }

            SBAS.MainWindowController.CloseProgressViewer();

            void RefreshStringPhraseTreeRecursive(TreeNode CurrentNode, Project.String[] CurrentStrings)
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
                    Update();
                    RefreshStringPhraseTreeRecursive(NewNode, str.GetChildren());
                }
            }

            StringWordsList.Sorted = true;
            StringIncompletePhrasesList.Sorted = true;
            StringMissingWordsList.Sorted = true;
            StringPhrasesPage.Enabled = true;

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
            NewString.Enabled = true;

            Project.String[] SelectedStrings = GetSelectedStrings();

            if (SelectedStrings.Length > 0)
            {
                StringProperties.Enabled = true;

                if (SelectedStrings.Length > 1)
                {
                    StringID.Enabled = false;
                    StringID.Text = "Multiple Values";
                    StringText.Enabled = false;
                    StringText.Text = "Multiple Values";
                    StringActionGroup.Enabled = false;

                    List<string> Tags = SelectedStrings.SelectMany(x => x.Tags).Where(x => SelectedStrings.All(y => y.Tags.Contains(x))).Distinct().ToList();

                    StringTagList.Items.Synchronise(Tags);
                    StringTagList.Sorted = true;
                    StringAddTag.Enabled = true;
                    if (StringTagList.SelectedItems.Count > 0) StringRemoveTag.Enabled = true;
                }
                else
                {
                    StringActionGroup.Enabled = true;
                    Project.String SelectedString = SelectedStrings[0];
                    StringID.Enabled = true;
                    StringID.Text = SelectedString.ID;
                    StringText.Enabled = true;
                    StringText.Text = SelectedString.Text;

                    List<string> SelectedTags = StringTagList.SelectedItems.Cast<string>().ToList();
                    StringTagList.Items.Synchronise(SelectedString.Tags);
                    StringTagList.Sorted = true;
                    for (int i = 0; i < StringTagList.Items.Count; i++) StringTagList.SetSelected(i, SelectedTags.Contains(StringTagList.Items[i]) ? true : false);
                    StringAddTag.Enabled = true;
                    if (StringTagList.SelectedItems.Count > 0) StringRemoveTag.Enabled = true;

                    if (SelectedString.action.GetType() == typeof(Project.String.Action.Audio))
                    {
                        Project.String.Action.Audio LinkedAudio = (Project.String.Action.Audio)SelectedString.action;
                        StringAction.SelectedItem = "Audio File";
                        StringActionDelay.Value = 0;
                        StringActionDelay.Enabled = false;

                        StringAudioFile.Text = LinkedAudio.MainAudio != null ? LinkedAudio.MainAudio.FileName : String.Empty;
                        StringLinkAudioButton.Enabled = true;
                        StringAutoLinkAudio.Enabled = true;

                    }
                    else
                    {
                        StringAudioFile.Text = String.Empty;
                        StringLinkAudioButton.Enabled = false;
                        StringAutoLinkAudio.Enabled = false;

                        if (SelectedString.action.GetType() == typeof(Project.String.Action.Delay))
                        {
                            StringAction.SelectedItem = "Delay (ms):";
                            StringActionDelay.Value = ((Project.String.Action.Delay)SelectedString.action).DelayLength;
                            StringActionDelay.Enabled = true;
                        }
                        else
                        {
                            StringAction.SelectedItem = "Group";
                            StringActionDelay.Value = 0;
                            StringActionDelay.Enabled = false;
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

        private void RefreshAudioTreeView()
        {
            Refreshing = true;
            object SelectedFile = AudioTreeView.SelectedNode != null ? AudioTreeView.SelectedNode.Tag : null;

            Dictionary<Project.AudioFile, string> AudioPaths = SBAS.MainController.CurrentProject.AudioFiles.ToDictionary(x => x, x => x.FullDir);

            AudioTreeView.Synchronise(AudioPaths);

            AudioTreeView.SelectedNode = SelectedFile != null ? AudioTreeView.GetAllNodes().ToList().Find(x => x.Tag == SelectedFile) : null;

            if (AudioTreeView.Nodes.Count == 1)
            {
                TreeNode CurrentNode = AudioTreeView.Nodes[0];
                CurrentNode.Expand();

                while (CurrentNode.Nodes.Count == 1)
                {
                    CurrentNode = CurrentNode.Nodes[0];
                    CurrentNode.Expand();
                }
            }

            Refreshing = false;
            RefreshAudioProperties();
        }

        private void RefreshAudioProperties()
        {
            Refreshing = true;

            Project.AudioFile[] SelectedAudio = GetSelectedAudio();

            AudioFileLocation.Items.Clear();

            if (SelectedAudio.Length > 0)
            {
                AudioFileLocation.Enabled = true;
                AudioFileLocation.Items.Add("[Create New Data File]");
                foreach (Project.DataFile dat in SBAS.MainController.CurrentProject.DataFiles) AudioFileLocation.Items.Add(dat);

                if (SelectedAudio.Length > 1)
                {
                    if (SelectedAudio.All(x => x.OffsetL == SelectedAudio.First().OffsetL)) AudioOffsetL.Value = SelectedAudio.First().OffsetL;
                    else AudioOffsetL.Value = 0;

                    if (SelectedAudio.All(x => x.OffsetR == SelectedAudio.First().OffsetR)) AudioOffsetR.Value = SelectedAudio.First().OffsetR;
                    else AudioOffsetR.Value = 0;

                    if (SelectedAudio.All(x => x.AutoCroppingEnabled == SelectedAudio.First().AutoCroppingEnabled)) AudioAutoCropping.Checked = SelectedAudio.First().AutoCroppingEnabled;
                    else AudioAutoCropping.CheckState = CheckState.Indeterminate;

                    if (SelectedAudio.All(x => x.IsInDataFile == SelectedAudio.First().IsInDataFile))
                    {
                        if (SelectedAudio.First().IsInDataFile) {
                            Project.DataFile dataFile = SelectedAudio.First().GetDataFile();
                            if (SelectedAudio.All(x => x.GetDataFile() == dataFile)) AudioFileLocation.SelectedItem = dataFile;
                        }
                        else
                        {
                            AudioFileLocation.Items.Add("(External)");
                            AudioFileLocation.SelectedItem = "(External)";
                        }
                    }
                    else
                    {
                        AudioFileLocation.Items.Add("(Multiple)");
                        AudioFileLocation.SelectedItem = "(Multiple)";
                    }

                    AudioAutoCroppingLabel.Enabled = false;
                    AudioAutoCroppingLabel.Text = "Multiple Files Selected";
                }
                else
                {
                    Project.AudioFile SelectedFile = SelectedAudio.First();

                    if (SelectedFile.IsInDataFile) AudioFileLocation.SelectedItem = SelectedFile.GetDataFile();
                    else
                    {
                        AudioFileLocation.Items.Add("(External)");
                        AudioFileLocation.SelectedItem = "(External)";
                    }

                    AudioOffsetL.Value = SelectedFile.OffsetL;
                    AudioOffsetR.Value = SelectedFile.OffsetR;
                    AudioAutoCropping.Checked = SelectedFile.AutoCroppingEnabled;
                    AudioAutoCroppingLabel.Enabled = true;
                    AudioAutoCroppingLabel.Text = String.Format("Start: {0} End: {1}", SelectedFile.CroppingL - SelectedFile.OffsetL, SelectedFile.CroppingR - SelectedFile.OffsetR);
                }

                AudioExtractAudioFiles.Enabled = true;
                AudioExtractDataFile.Enabled = true;
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
                AudioExtractAudioFiles.Enabled = false;
                AudioExtractDataFile.Enabled = false;
                AudioAutoCroppingLabel.Enabled = false;
                AudioAutoCroppingLabel.Text = "No Files Selected";
                AudioFileLocation.Enabled = false;
            }

            AudioImportFiles.Enabled = true;
            AudioImportFolders.Enabled = true;
            AudioRecord.Enabled = true;
            Refreshing = false;
        }

        private void RefreshPreview()
        {
            PreviewSentence = new Project.Sentence(PreviewTextBox.Text, SBAS.MainController.CurrentProject);
            PreviewPlayButton.Enabled = PreviewSentence.Valid;
            PreviewSaveAudioButton.Enabled = PreviewSentence.Valid;

            if (PreviewSentence.Valid)
                PreviewProcessBox.Text = String.Join(" ", PreviewSentence.Strings.Select(x => x.Text));
            else // Show the index of the error and a basic idea of where the error is
                PreviewProcessBox.Text = $"Error processing sentence at index '{PreviewSentence.ErrorIndex}': {PreviewTextBox.Text.Substring(0, PreviewSentence.ErrorIndex)} !!";
        }

        private void ClearAll()
        {
            Refreshing = true;
            Text = "Editor";
            StringPhraseTree.Nodes.Clear();
            TagList.Items.Clear();
            StringList.Items.Clear();
            StringListFilter.Clear();
            StringID.Clear();
            StringText.Clear();
            StringTagList.Items.Clear();
            StringAction.SelectedItem = "";
            StringActionDelay.Value = 0;
            StringAudioFile.Clear();
            AudioTreeView.Nodes.Clear();
            PreviewProcessBox.Text = String.Empty;
            PreviewTextBox.Text = String.Empty;
            AudioFileLocation.Items.Clear();

            Refreshing = false;
        }

        #endregion

        #region Select Functions

        private Project.AudioFile[] GetSelectedAudio()
        {
            List<Project.AudioFile> ret = new List<Project.AudioFile>();

            if (AudioTreeView.SelectedNode != null)
            {
                if (AudioTreeView.SelectedNode.Tag == null)
                {
                    foreach (TreeNode node in AudioTreeView.SelectedNode.GetChildren())
                    {
                        if (node.Tag != null) ret.Add((Project.AudioFile)node.Tag);
                    }
                }
                else ret.Add((Project.AudioFile)AudioTreeView.SelectedNode.Tag);
            }

            return ret.ToArray();
        }

        private Project.String[] GetSelectedStrings()
        {
            if (StringViewPages.SelectedIndex == 0) return StringList.SelectedItems.Cast<Project.String>().ToArray();
            else
            {
                if (StringPhraseTree.Focused) return new Project.String[1] { (Project.String)StringPhraseTree.SelectedNode.Tag };
                else if (StringWordsList.Focused) return StringWordsList.SelectedItems.Cast<Project.String>().ToArray();
                else if (StringIncompletePhrasesList.Focused) return StringIncompletePhrasesList.SelectedItems.Cast<Project.String>().ToArray();
                else return new Project.String[0];
            }
        }

        #endregion

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
                RefreshStringPhraseTree();
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

        private void StringAddTag_Click(object sender, EventArgs e)
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
                RefreshStringPhraseTree();
            }
        }

        private void StringRemoveTag_Click(object sender, EventArgs e)
        {
            if (StringTagList.SelectedItem != null)
            {
                foreach (Project.String str in StringList.SelectedItems.Cast<Project.String>())
                {
                    str.Tags.Remove((string)StringTagList.SelectedItem);
                }
                RefreshTagList();
                RefreshStringList();
                RefreshStringProperties();
                RefreshStringPhraseTree();
            }
        }

        private void StringTagList_SelectedIndexChanged(object sender, EventArgs e)
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
                if ((string)StringAction.SelectedItem == "Audio File")
                {
                    ((Project.String)StringList.SelectedItem).action = new Project.String.Action.Audio();
                }
                else if ((string)StringAction.SelectedItem == "Delay (ms):")
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

        private void StringActionDelay_ValueChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                ((Project.String.Action.Delay)((Project.String)StringList.SelectedItem).action).DelayLength = (short)StringActionDelay.Value;
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

        private void AudioExtractAudioFiles_Click(object sender, EventArgs e)
        {

        }

        private void AudioExtractDataFile_Click(object sender, EventArgs e)
        {

        }

        private void AudioTreeViewNewFolder_Click(object sender, EventArgs e)
        {

        }

        private void AudioTreeViewDelete_Click(object sender, EventArgs e)
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

        private void AudioTreeViewRename_Click(object sender, EventArgs e)
        {

        }

        private void AudioTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!Refreshing)
            {
                if (e.Node.Tag != null) ((Project.AudioFile)e.Node.Tag).Play();
                RefreshAudioProperties();
            }
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
            foreach (Project.AudioFile audio in SelectedAudio) audio.CroppingR = (int)AudioOffsetR.Value;
        }

        private void AudioTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
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

        private void StringPhraseTreeGroup_MouseHover(object sender, EventArgs e)
        {
            if (!StringPhraseTree.Enabled)
            {
                StringPhraseTree.Enabled = true;
                TrueRefreshStringPhraseTree();
            }
        }

        private void StringViewPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (StringViewPages.SelectedIndex == 1 && !StringPhraseTree.Enabled)
            {
                StringPhraseTree.Enabled = true;
                TrueRefreshStringPhraseTree();
            }
        }

        private void PreviewPlayButton_Click(object sender, EventArgs e)
        {
            PreviewSentence.Play();
        }

        private void PreviewSaveAudioButton_Click(object sender, EventArgs e)
        {
            if (PreviewSentence.Valid) {
                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "WAV Audio Files (*.wav)|*.wav",
                    OverwritePrompt = true,
                    Title = "Save Sentence as Audio File...",
                    CheckFileExists = false,
                    InitialDirectory = SBAS.MainController.CurrentProject.fileStream.Name,
                    ValidateNames = true,
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    PreviewSentence.Save(saveFileDialog.FileName);
                }
            }
        }

        private void PreviewTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!Refreshing) RefreshPreview();
        }

        private void StringPhraseTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!Refreshing)
            {
                Refreshing = true;
                StringWordsList.ClearSelected();
                StringIncompletePhrasesList.ClearSelected();
                Refreshing = false;
                RefreshStringProperties();
            }
        }

        private void StringPhrasesRemaningList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                Refreshing = true;
                StringIncompletePhrasesList.ClearSelected();
                Refreshing = false;
                RefreshStringProperties();
            }
        }

        private void StringIncompletePhrasesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                Refreshing = true;
                StringWordsList.ClearSelected();
                Refreshing = false;
                RefreshStringProperties();
            }
        }

        private void AudioFileLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Refreshing)
            {
                if (AudioFileLocation.SelectedItem.GetType() == typeof(Project.DataFile))
                {
                    Project.DataFile DestinationFile = (Project.DataFile)AudioFileLocation.SelectedItem;
                    Project.AudioFile[] SelectedAudioFiles = GetSelectedAudio();
                    if (SelectedAudioFiles.Length > 0)
                    {
                        DestinationFile.Add(SelectedAudioFiles.ToArray());
                        RefreshAudioTreeView();
                    }
                }
                else if (AudioFileLocation.SelectedItem == "[Create New Data File]")
                {
                    Project.AudioFile[] SelectedAudioFiles = GetSelectedAudio();
                    if (SelectedAudioFiles.Length > 0)
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog()
                        {
                            Filter = "SBAS Data Files (*.sbd)|*.sbd",
                            OverwritePrompt = true,
                            Title = "Create new Data File...",
                            CheckFileExists = false,
                            InitialDirectory = SBAS.MainController.CurrentProject.fileStream.Name,
                            ValidateNames = true,
                        };

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            Project.DataFile NewDataFile = new Project.DataFile(saveFileDialog.FileName, SelectedAudioFiles);
                            SBAS.MainController.CurrentProject.DataFiles.Add(NewDataFile);
                            RefreshAudioTreeView();
                        }
                        else RefreshAudioProperties();
                    }
                }
            }
        }

        private void StringLinkAudioButton_Click(object sender, EventArgs e)
        {

        }

        private void StringAutoLinkAudio_Click(object sender, EventArgs e)
        {
            int LinkedFiles = 0;
            Project.String[] SelectedStrings = GetSelectedStrings();

            foreach (Project.String str in SelectedStrings)
            {
                Project.AudioFile LinkedFile = SBAS.MainController.CurrentProject.AudioFiles.Find(x => string.Concat(Path.GetFileNameWithoutExtension(x.FileName).GetWords()) == string.Concat(str.Words));

                if (LinkedFile != null && str.action.GetType() == typeof(Project.String.Action.Audio))
                {
                    ((Project.String.Action.Audio)str.action).MainAudio = LinkedFile;
                    ((Project.String.Action.Audio)str.action).MainAudioHash = LinkedFile.Hash;
                    LinkedFiles++;
                }
            }
        }

        private void ImportDataFileButton_Click(object sender, EventArgs e)
        {
            Importer importer = new Importer(true, false, "Please Select a Valid Data File...");
            importer.ShowDialog();
            RefreshAll();
        }

        private void RemoveAudioButton_Click(object sender, EventArgs e)
        {
            Project.AudioFile[] SelectedAudio = GetSelectedAudio();

            foreach (Project.AudioFile aud in SelectedAudio)
            {
                SBAS.MainController.CurrentProject.AudioFiles.Remove(aud);
                if (aud.IsInDataFile) aud.GetDataFile().Remove(aud);
                aud.fileStream.Dispose();
            }

            if (SelectedAudio.Length > 0) RefreshAll();
        }
    }
}
