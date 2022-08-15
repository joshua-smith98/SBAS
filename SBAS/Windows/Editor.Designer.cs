namespace SBAS
{
    partial class Editor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToStarterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditorContainer = new System.Windows.Forms.Panel();
            this.EditorPages = new System.Windows.Forms.TabControl();
            this.AudioPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AudioTreeView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.AudioExtractAudioFiles = new System.Windows.Forms.Button();
            this.AudioExtractDataFile = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.AudioAutoCropping = new System.Windows.Forms.CheckBox();
            this.AudioAutoCroppingLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.AudioOffsetL = new System.Windows.Forms.NumericUpDown();
            this.AudioOffsetR = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.AudioFileLocation = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.AudioImportFiles = new System.Windows.Forms.Button();
            this.AudioImportFolders = new System.Windows.Forms.Button();
            this.AudioRecord = new System.Windows.Forms.Button();
            this.ImportDataFileButton = new System.Windows.Forms.Button();
            this.RemoveAudioButton = new System.Windows.Forms.Button();
            this.StringsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.StringProperties = new System.Windows.Forms.TableLayoutPanel();
            this.StringMetadataGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.StringID = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.StringText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.StringAddTag = new System.Windows.Forms.Button();
            this.StringRemoveTag = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.StringTagList = new System.Windows.Forms.ListBox();
            this.StringActionGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel16 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel17 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.StringAction = new System.Windows.Forms.ComboBox();
            this.StringActionDelay = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel18 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.StringAudioFile = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel19 = new System.Windows.Forms.TableLayoutPanel();
            this.StringLinkAudioButton = new System.Windows.Forms.Button();
            this.StringAutoLinkAudio = new System.Windows.Forms.Button();
            this.StringViewPages = new System.Windows.Forms.TabControl();
            this.StringTagsPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TagList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel20 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel21 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.StringListFilter = new System.Windows.Forms.TextBox();
            this.StringList = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel22 = new System.Windows.Forms.TableLayoutPanel();
            this.NewString = new System.Windows.Forms.Button();
            this.DeleteString = new System.Windows.Forms.Button();
            this.StringPhrasesPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.StringsPhraseTree = new System.Windows.Forms.GroupBox();
            this.StringPhraseTree = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.StringWordsList = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.StringIncompletePhrasesList = new System.Windows.Forms.ListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.StringMissingWordsList = new System.Windows.Forms.ListBox();
            this.PreviewPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel23 = new System.Windows.Forms.TableLayoutPanel();
            this.PreviewPlayButton = new System.Windows.Forms.Button();
            this.PreviewSaveAudioButton = new System.Windows.Forms.Button();
            this.PreviewTextBox = new System.Windows.Forms.TextBox();
            this.PreviewProcessBox = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.EditorContainer.SuspendLayout();
            this.EditorPages.SuspendLayout();
            this.AudioPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AudioOffsetL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AudioOffsetR)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.StringsPage.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.StringProperties.SuspendLayout();
            this.StringMetadataGroup.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.StringActionGroup.SuspendLayout();
            this.tableLayoutPanel16.SuspendLayout();
            this.tableLayoutPanel17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StringActionDelay)).BeginInit();
            this.tableLayoutPanel18.SuspendLayout();
            this.tableLayoutPanel19.SuspendLayout();
            this.StringViewPages.SuspendLayout();
            this.StringTagsPage.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel20.SuspendLayout();
            this.tableLayoutPanel21.SuspendLayout();
            this.tableLayoutPanel22.SuspendLayout();
            this.StringPhrasesPage.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.StringsPhraseTree.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.PreviewPage.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel23.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(927, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeProjectToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToStarterToolStripMenuItem,
            this.exitToDesktopToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open Project";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save Project";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save Project as";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToStarterToolStripMenuItem
            // 
            this.exitToStarterToolStripMenuItem.Name = "exitToStarterToolStripMenuItem";
            this.exitToStarterToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToStarterToolStripMenuItem.Text = "Exit to Starter";
            this.exitToStarterToolStripMenuItem.Click += new System.EventHandler(this.exitToStarterToolStripMenuItem_Click);
            // 
            // exitToDesktopToolStripMenuItem
            // 
            this.exitToDesktopToolStripMenuItem.Name = "exitToDesktopToolStripMenuItem";
            this.exitToDesktopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToDesktopToolStripMenuItem.Text = "Exit to Desktop";
            this.exitToDesktopToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // EditorContainer
            // 
            this.EditorContainer.Controls.Add(this.EditorPages);
            this.EditorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorContainer.Location = new System.Drawing.Point(0, 24);
            this.EditorContainer.Name = "EditorContainer";
            this.EditorContainer.Size = new System.Drawing.Size(927, 482);
            this.EditorContainer.TabIndex = 1;
            // 
            // EditorPages
            // 
            this.EditorPages.Controls.Add(this.AudioPage);
            this.EditorPages.Controls.Add(this.StringsPage);
            this.EditorPages.Controls.Add(this.PreviewPage);
            this.EditorPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorPages.Location = new System.Drawing.Point(0, 0);
            this.EditorPages.Name = "EditorPages";
            this.EditorPages.SelectedIndex = 0;
            this.EditorPages.Size = new System.Drawing.Size(927, 482);
            this.EditorPages.TabIndex = 0;
            // 
            // AudioPage
            // 
            this.AudioPage.BackColor = System.Drawing.SystemColors.Control;
            this.AudioPage.Controls.Add(this.tableLayoutPanel1);
            this.AudioPage.Location = new System.Drawing.Point(4, 22);
            this.AudioPage.Name = "AudioPage";
            this.AudioPage.Padding = new System.Windows.Forms.Padding(3);
            this.AudioPage.Size = new System.Drawing.Size(919, 456);
            this.AudioPage.TabIndex = 0;
            this.AudioPage.Text = "Audio";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.5345F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.4655F));
            this.tableLayoutPanel1.Controls.Add(this.AudioTreeView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(913, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // AudioTreeView
            // 
            this.AudioTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioTreeView.FullRowSelect = true;
            this.AudioTreeView.HideSelection = false;
            this.AudioTreeView.Location = new System.Drawing.Point(226, 3);
            this.AudioTreeView.Name = "AudioTreeView";
            this.AudioTreeView.Size = new System.Drawing.Size(684, 444);
            this.AudioTreeView.TabIndex = 0;
            this.AudioTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AudioTreeView_AfterSelect);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.AudioExtractAudioFiles, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.AudioExtractDataFile, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel6, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.AudioRecord, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.ImportDataFileButton, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.RemoveAudioButton, 0, 9);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(217, 444);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // AudioExtractAudioFiles
            // 
            this.AudioExtractAudioFiles.AutoSize = true;
            this.AudioExtractAudioFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioExtractAudioFiles.Location = new System.Drawing.Point(3, 3);
            this.AudioExtractAudioFiles.Name = "AudioExtractAudioFiles";
            this.AudioExtractAudioFiles.Size = new System.Drawing.Size(211, 24);
            this.AudioExtractAudioFiles.TabIndex = 0;
            this.AudioExtractAudioFiles.Text = "Extract Audio File(s)";
            this.AudioExtractAudioFiles.UseVisualStyleBackColor = true;
            this.AudioExtractAudioFiles.Click += new System.EventHandler(this.AudioExtractAudioFiles_Click);
            // 
            // AudioExtractDataFile
            // 
            this.AudioExtractDataFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioExtractDataFile.Location = new System.Drawing.Point(3, 33);
            this.AudioExtractDataFile.Name = "AudioExtractDataFile";
            this.AudioExtractDataFile.Size = new System.Drawing.Size(211, 24);
            this.AudioExtractDataFile.TabIndex = 1;
            this.AudioExtractDataFile.Text = "Extract Whole Data File";
            this.AudioExtractDataFile.UseVisualStyleBackColor = true;
            this.AudioExtractDataFile.Click += new System.EventHandler(this.AudioExtractDataFile_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel3.Controls.Add(this.AudioAutoCropping, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.AudioAutoCroppingLabel, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 63);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(211, 24);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // AudioAutoCropping
            // 
            this.AudioAutoCropping.AutoSize = true;
            this.AudioAutoCropping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioAutoCropping.Location = new System.Drawing.Point(3, 3);
            this.AudioAutoCropping.Name = "AudioAutoCropping";
            this.AudioAutoCropping.Size = new System.Drawing.Size(99, 18);
            this.AudioAutoCropping.TabIndex = 0;
            this.AudioAutoCropping.Text = "Auto Cropping";
            this.AudioAutoCropping.UseVisualStyleBackColor = true;
            this.AudioAutoCropping.CheckedChanged += new System.EventHandler(this.AudioAutoCropping_CheckedChanged);
            // 
            // AudioAutoCroppingLabel
            // 
            this.AudioAutoCroppingLabel.AutoSize = true;
            this.AudioAutoCroppingLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.AudioAutoCroppingLabel.Location = new System.Drawing.Point(123, 0);
            this.AudioAutoCroppingLabel.Name = "AudioAutoCroppingLabel";
            this.AudioAutoCroppingLabel.Size = new System.Drawing.Size(85, 24);
            this.AudioAutoCroppingLabel.TabIndex = 1;
            this.AudioAutoCroppingLabel.Text = "No File Selected";
            this.AudioAutoCroppingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.AudioOffsetL, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.AudioOffsetR, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 93);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(211, 27);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Playback Offset (ms):";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AudioOffsetL
            // 
            this.AudioOffsetL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioOffsetL.Location = new System.Drawing.Point(122, 3);
            this.AudioOffsetL.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.AudioOffsetL.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.AudioOffsetL.Name = "AudioOffsetL";
            this.AudioOffsetL.Size = new System.Drawing.Size(40, 20);
            this.AudioOffsetL.TabIndex = 1;
            this.AudioOffsetL.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // AudioOffsetR
            // 
            this.AudioOffsetR.Location = new System.Drawing.Point(168, 3);
            this.AudioOffsetR.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.AudioOffsetR.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.AudioOffsetR.Name = "AudioOffsetR";
            this.AudioOffsetR.Size = new System.Drawing.Size(40, 20);
            this.AudioOffsetR.TabIndex = 2;
            this.AudioOffsetR.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.71698F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.28302F));
            this.tableLayoutPanel5.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.AudioFileLocation, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 126);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(211, 28);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 28);
            this.label2.TabIndex = 0;
            this.label2.Text = "Location:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AudioFileLocation
            // 
            this.AudioFileLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioFileLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AudioFileLocation.FormattingEnabled = true;
            this.AudioFileLocation.Location = new System.Drawing.Point(65, 3);
            this.AudioFileLocation.Name = "AudioFileLocation";
            this.AudioFileLocation.Size = new System.Drawing.Size(143, 21);
            this.AudioFileLocation.TabIndex = 1;
            this.AudioFileLocation.SelectedIndexChanged += new System.EventHandler(this.AudioFileLocation_SelectedIndexChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.AudioImportFiles, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.AudioImportFolders, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 352);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(211, 29);
            this.tableLayoutPanel6.TabIndex = 5;
            // 
            // AudioImportFiles
            // 
            this.AudioImportFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioImportFiles.Location = new System.Drawing.Point(3, 3);
            this.AudioImportFiles.Name = "AudioImportFiles";
            this.AudioImportFiles.Size = new System.Drawing.Size(99, 23);
            this.AudioImportFiles.TabIndex = 0;
            this.AudioImportFiles.Text = "Import File(s)";
            this.AudioImportFiles.UseVisualStyleBackColor = true;
            this.AudioImportFiles.Click += new System.EventHandler(this.AudioImportFiles_Click);
            // 
            // AudioImportFolders
            // 
            this.AudioImportFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioImportFolders.Location = new System.Drawing.Point(108, 3);
            this.AudioImportFolders.Name = "AudioImportFolders";
            this.AudioImportFolders.Size = new System.Drawing.Size(100, 23);
            this.AudioImportFolders.TabIndex = 1;
            this.AudioImportFolders.Text = "Import Folder";
            this.AudioImportFolders.UseVisualStyleBackColor = true;
            this.AudioImportFolders.Click += new System.EventHandler(this.AudioImportFolders_Click);
            // 
            // AudioRecord
            // 
            this.AudioRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AudioRecord.Location = new System.Drawing.Point(3, 387);
            this.AudioRecord.Name = "AudioRecord";
            this.AudioRecord.Size = new System.Drawing.Size(211, 24);
            this.AudioRecord.TabIndex = 6;
            this.AudioRecord.Text = "Record Audio";
            this.AudioRecord.UseVisualStyleBackColor = true;
            // 
            // ImportDataFileButton
            // 
            this.ImportDataFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ImportDataFileButton.Location = new System.Drawing.Point(3, 322);
            this.ImportDataFileButton.Name = "ImportDataFileButton";
            this.ImportDataFileButton.Size = new System.Drawing.Size(211, 24);
            this.ImportDataFileButton.TabIndex = 7;
            this.ImportDataFileButton.Text = "Import Data File(s)";
            this.ImportDataFileButton.UseVisualStyleBackColor = true;
            this.ImportDataFileButton.Click += new System.EventHandler(this.ImportDataFileButton_Click);
            // 
            // RemoveAudioButton
            // 
            this.RemoveAudioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RemoveAudioButton.Location = new System.Drawing.Point(3, 417);
            this.RemoveAudioButton.Name = "RemoveAudioButton";
            this.RemoveAudioButton.Size = new System.Drawing.Size(211, 24);
            this.RemoveAudioButton.TabIndex = 8;
            this.RemoveAudioButton.Text = "Remove Audio File(s)";
            this.RemoveAudioButton.UseVisualStyleBackColor = true;
            this.RemoveAudioButton.Click += new System.EventHandler(this.RemoveAudioButton_Click);
            // 
            // StringsPage
            // 
            this.StringsPage.BackColor = System.Drawing.SystemColors.Control;
            this.StringsPage.Controls.Add(this.tableLayoutPanel7);
            this.StringsPage.Location = new System.Drawing.Point(4, 22);
            this.StringsPage.Name = "StringsPage";
            this.StringsPage.Padding = new System.Windows.Forms.Padding(3);
            this.StringsPage.Size = new System.Drawing.Size(919, 456);
            this.StringsPage.TabIndex = 1;
            this.StringsPage.Text = "Strings";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 278F));
            this.tableLayoutPanel7.Controls.Add(this.StringProperties, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.StringViewPages, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(913, 450);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // StringProperties
            // 
            this.StringProperties.ColumnCount = 1;
            this.StringProperties.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.StringProperties.Controls.Add(this.StringMetadataGroup, 0, 0);
            this.StringProperties.Controls.Add(this.StringActionGroup, 0, 1);
            this.StringProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringProperties.Location = new System.Drawing.Point(638, 3);
            this.StringProperties.Name = "StringProperties";
            this.StringProperties.RowCount = 2;
            this.StringProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.StringProperties.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
            this.StringProperties.Size = new System.Drawing.Size(272, 444);
            this.StringProperties.TabIndex = 0;
            // 
            // StringMetadataGroup
            // 
            this.StringMetadataGroup.Controls.Add(this.tableLayoutPanel12);
            this.StringMetadataGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringMetadataGroup.Location = new System.Drawing.Point(3, 3);
            this.StringMetadataGroup.Name = "StringMetadataGroup";
            this.StringMetadataGroup.Size = new System.Drawing.Size(266, 313);
            this.StringMetadataGroup.TabIndex = 0;
            this.StringMetadataGroup.TabStop = false;
            this.StringMetadataGroup.Text = "Metadata";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 1;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel13, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel14, 0, 1);
            this.tableLayoutPanel12.Controls.Add(this.tableLayoutPanel15, 0, 4);
            this.tableLayoutPanel12.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel12.Controls.Add(this.StringTagList, 0, 3);
            this.tableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel12.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 5;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(260, 294);
            this.tableLayoutPanel12.TabIndex = 0;
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 2;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.StringID, 1, 0);
            this.tableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel13.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(254, 27);
            this.tableLayoutPanel13.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 27);
            this.label3.TabIndex = 0;
            this.label3.Text = "ID: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StringID
            // 
            this.StringID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringID.Location = new System.Drawing.Point(48, 3);
            this.StringID.Name = "StringID";
            this.StringID.Size = new System.Drawing.Size(203, 20);
            this.StringID.TabIndex = 1;
            this.StringID.TextChanged += new System.EventHandler(this.StringID_TextChanged);
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.ColumnCount = 2;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.StringText, 1, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(254, 27);
            this.tableLayoutPanel14.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 27);
            this.label4.TabIndex = 0;
            this.label4.Text = "String: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StringText
            // 
            this.StringText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringText.Location = new System.Drawing.Point(48, 3);
            this.StringText.Name = "StringText";
            this.StringText.Size = new System.Drawing.Size(203, 20);
            this.StringText.TabIndex = 1;
            this.StringText.TextChanged += new System.EventHandler(this.StringText_TextChanged);
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Controls.Add(this.StringAddTag, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.StringRemoveTag, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(3, 262);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(254, 29);
            this.tableLayoutPanel15.TabIndex = 2;
            // 
            // StringAddTag
            // 
            this.StringAddTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringAddTag.Location = new System.Drawing.Point(3, 3);
            this.StringAddTag.Name = "StringAddTag";
            this.StringAddTag.Size = new System.Drawing.Size(121, 23);
            this.StringAddTag.TabIndex = 0;
            this.StringAddTag.Text = "Add";
            this.StringAddTag.UseVisualStyleBackColor = true;
            this.StringAddTag.Click += new System.EventHandler(this.StringAddTag_Click);
            // 
            // StringRemoveTag
            // 
            this.StringRemoveTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringRemoveTag.Location = new System.Drawing.Point(130, 3);
            this.StringRemoveTag.Name = "StringRemoveTag";
            this.StringRemoveTag.Size = new System.Drawing.Size(121, 23);
            this.StringRemoveTag.TabIndex = 1;
            this.StringRemoveTag.Text = "Remove";
            this.StringRemoveTag.UseVisualStyleBackColor = true;
            this.StringRemoveTag.Click += new System.EventHandler(this.StringRemoveTag_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(254, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Tags:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StringTagList
            // 
            this.StringTagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringTagList.FormattingEnabled = true;
            this.StringTagList.Location = new System.Drawing.Point(3, 84);
            this.StringTagList.Name = "StringTagList";
            this.StringTagList.Size = new System.Drawing.Size(254, 172);
            this.StringTagList.TabIndex = 4;
            this.StringTagList.SelectedIndexChanged += new System.EventHandler(this.StringTagList_SelectedIndexChanged);
            // 
            // StringActionGroup
            // 
            this.StringActionGroup.Controls.Add(this.tableLayoutPanel16);
            this.StringActionGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringActionGroup.Location = new System.Drawing.Point(3, 322);
            this.StringActionGroup.Name = "StringActionGroup";
            this.StringActionGroup.Size = new System.Drawing.Size(266, 119);
            this.StringActionGroup.TabIndex = 1;
            this.StringActionGroup.TabStop = false;
            this.StringActionGroup.Text = "Action";
            // 
            // tableLayoutPanel16
            // 
            this.tableLayoutPanel16.ColumnCount = 1;
            this.tableLayoutPanel16.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel17, 0, 0);
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel18, 0, 1);
            this.tableLayoutPanel16.Controls.Add(this.tableLayoutPanel19, 0, 2);
            this.tableLayoutPanel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel16.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel16.Name = "tableLayoutPanel16";
            this.tableLayoutPanel16.RowCount = 3;
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel16.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel16.Size = new System.Drawing.Size(260, 100);
            this.tableLayoutPanel16.TabIndex = 0;
            // 
            // tableLayoutPanel17
            // 
            this.tableLayoutPanel17.ColumnCount = 3;
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel17.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel17.Controls.Add(this.StringAction, 1, 0);
            this.tableLayoutPanel17.Controls.Add(this.StringActionDelay, 2, 0);
            this.tableLayoutPanel17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel17.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel17.Name = "tableLayoutPanel17";
            this.tableLayoutPanel17.RowCount = 1;
            this.tableLayoutPanel17.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel17.Size = new System.Drawing.Size(254, 27);
            this.tableLayoutPanel17.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "Action: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StringAction
            // 
            this.StringAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StringAction.FormattingEnabled = true;
            this.StringAction.Items.AddRange(new object[] {
            "Audio File",
            "Delay (ms):",
            "Group"});
            this.StringAction.Location = new System.Drawing.Point(63, 3);
            this.StringAction.Name = "StringAction";
            this.StringAction.Size = new System.Drawing.Size(125, 21);
            this.StringAction.TabIndex = 1;
            this.StringAction.SelectedIndexChanged += new System.EventHandler(this.StringAction_SelectedIndexChanged);
            // 
            // StringActionDelay
            // 
            this.StringActionDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringActionDelay.Location = new System.Drawing.Point(194, 3);
            this.StringActionDelay.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.StringActionDelay.Name = "StringActionDelay";
            this.StringActionDelay.Size = new System.Drawing.Size(57, 20);
            this.StringActionDelay.TabIndex = 2;
            this.StringActionDelay.ValueChanged += new System.EventHandler(this.StringActionDelay_ValueChanged);
            // 
            // tableLayoutPanel18
            // 
            this.tableLayoutPanel18.ColumnCount = 2;
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 67F));
            this.tableLayoutPanel18.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel18.Controls.Add(this.StringAudioFile, 1, 0);
            this.tableLayoutPanel18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel18.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel18.Name = "tableLayoutPanel18";
            this.tableLayoutPanel18.RowCount = 1;
            this.tableLayoutPanel18.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel18.Size = new System.Drawing.Size(254, 27);
            this.tableLayoutPanel18.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 27);
            this.label7.TabIndex = 0;
            this.label7.Text = "Audio File: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StringAudioFile
            // 
            this.StringAudioFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringAudioFile.Location = new System.Drawing.Point(70, 3);
            this.StringAudioFile.Name = "StringAudioFile";
            this.StringAudioFile.ReadOnly = true;
            this.StringAudioFile.Size = new System.Drawing.Size(181, 20);
            this.StringAudioFile.TabIndex = 1;
            // 
            // tableLayoutPanel19
            // 
            this.tableLayoutPanel19.ColumnCount = 2;
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Controls.Add(this.StringLinkAudioButton, 0, 0);
            this.tableLayoutPanel19.Controls.Add(this.StringAutoLinkAudio, 1, 0);
            this.tableLayoutPanel19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel19.Location = new System.Drawing.Point(3, 69);
            this.tableLayoutPanel19.Name = "tableLayoutPanel19";
            this.tableLayoutPanel19.RowCount = 1;
            this.tableLayoutPanel19.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel19.Size = new System.Drawing.Size(254, 29);
            this.tableLayoutPanel19.TabIndex = 2;
            // 
            // StringLinkAudioButton
            // 
            this.StringLinkAudioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringLinkAudioButton.Location = new System.Drawing.Point(3, 3);
            this.StringLinkAudioButton.Name = "StringLinkAudioButton";
            this.StringLinkAudioButton.Size = new System.Drawing.Size(121, 23);
            this.StringLinkAudioButton.TabIndex = 0;
            this.StringLinkAudioButton.Text = "Select Audio File";
            this.StringLinkAudioButton.UseVisualStyleBackColor = true;
            this.StringLinkAudioButton.Click += new System.EventHandler(this.StringLinkAudioButton_Click);
            // 
            // StringAutoLinkAudio
            // 
            this.StringAutoLinkAudio.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringAutoLinkAudio.Location = new System.Drawing.Point(130, 3);
            this.StringAutoLinkAudio.Name = "StringAutoLinkAudio";
            this.StringAutoLinkAudio.Size = new System.Drawing.Size(121, 23);
            this.StringAutoLinkAudio.TabIndex = 1;
            this.StringAutoLinkAudio.Text = "Auto-Link Audio File";
            this.StringAutoLinkAudio.UseVisualStyleBackColor = true;
            this.StringAutoLinkAudio.Click += new System.EventHandler(this.StringAutoLinkAudio_Click);
            // 
            // StringViewPages
            // 
            this.StringViewPages.Controls.Add(this.StringTagsPage);
            this.StringViewPages.Controls.Add(this.StringPhrasesPage);
            this.StringViewPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringViewPages.Location = new System.Drawing.Point(3, 3);
            this.StringViewPages.Name = "StringViewPages";
            this.StringViewPages.SelectedIndex = 0;
            this.StringViewPages.Size = new System.Drawing.Size(629, 444);
            this.StringViewPages.TabIndex = 1;
            this.StringViewPages.SelectedIndexChanged += new System.EventHandler(this.StringViewPages_SelectedIndexChanged);
            // 
            // StringTagsPage
            // 
            this.StringTagsPage.BackColor = System.Drawing.SystemColors.Control;
            this.StringTagsPage.Controls.Add(this.tableLayoutPanel9);
            this.StringTagsPage.Location = new System.Drawing.Point(4, 22);
            this.StringTagsPage.Name = "StringTagsPage";
            this.StringTagsPage.Padding = new System.Windows.Forms.Padding(3);
            this.StringTagsPage.Size = new System.Drawing.Size(621, 418);
            this.StringTagsPage.TabIndex = 0;
            this.StringTagsPage.Text = "Tags";
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.groupBox2, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(615, 412);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TagList);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(301, 406);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tags";
            // 
            // TagList
            // 
            this.TagList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TagList.FormattingEnabled = true;
            this.TagList.Location = new System.Drawing.Point(3, 16);
            this.TagList.Name = "TagList";
            this.TagList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.TagList.Size = new System.Drawing.Size(295, 387);
            this.TagList.TabIndex = 0;
            this.TagList.SelectedIndexChanged += new System.EventHandler(this.TagList_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel20);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(310, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 406);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Strings";
            // 
            // tableLayoutPanel20
            // 
            this.tableLayoutPanel20.ColumnCount = 1;
            this.tableLayoutPanel20.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel21, 0, 0);
            this.tableLayoutPanel20.Controls.Add(this.StringList, 0, 1);
            this.tableLayoutPanel20.Controls.Add(this.tableLayoutPanel22, 0, 2);
            this.tableLayoutPanel20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel20.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel20.Name = "tableLayoutPanel20";
            this.tableLayoutPanel20.RowCount = 3;
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel20.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel20.Size = new System.Drawing.Size(296, 387);
            this.tableLayoutPanel20.TabIndex = 0;
            // 
            // tableLayoutPanel21
            // 
            this.tableLayoutPanel21.ColumnCount = 2;
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel21.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel21.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel21.Controls.Add(this.StringListFilter, 1, 0);
            this.tableLayoutPanel21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel21.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel21.Name = "tableLayoutPanel21";
            this.tableLayoutPanel21.RowCount = 1;
            this.tableLayoutPanel21.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel21.Size = new System.Drawing.Size(290, 27);
            this.tableLayoutPanel21.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 27);
            this.label8.TabIndex = 0;
            this.label8.Text = "Filter:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StringListFilter
            // 
            this.StringListFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringListFilter.Location = new System.Drawing.Point(44, 3);
            this.StringListFilter.Name = "StringListFilter";
            this.StringListFilter.Size = new System.Drawing.Size(243, 20);
            this.StringListFilter.TabIndex = 1;
            this.StringListFilter.TextChanged += new System.EventHandler(this.StringListFilter_TextChanged);
            // 
            // StringList
            // 
            this.StringList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringList.FormattingEnabled = true;
            this.StringList.Location = new System.Drawing.Point(3, 36);
            this.StringList.Name = "StringList";
            this.StringList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.StringList.Size = new System.Drawing.Size(290, 313);
            this.StringList.TabIndex = 1;
            this.StringList.SelectedIndexChanged += new System.EventHandler(this.StringList_SelectedIndexChanged);
            // 
            // tableLayoutPanel22
            // 
            this.tableLayoutPanel22.ColumnCount = 2;
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel22.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel22.Controls.Add(this.NewString, 0, 0);
            this.tableLayoutPanel22.Controls.Add(this.DeleteString, 1, 0);
            this.tableLayoutPanel22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel22.Location = new System.Drawing.Point(3, 355);
            this.tableLayoutPanel22.Name = "tableLayoutPanel22";
            this.tableLayoutPanel22.RowCount = 1;
            this.tableLayoutPanel22.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel22.Size = new System.Drawing.Size(290, 29);
            this.tableLayoutPanel22.TabIndex = 2;
            // 
            // NewString
            // 
            this.NewString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewString.Location = new System.Drawing.Point(3, 3);
            this.NewString.Name = "NewString";
            this.NewString.Size = new System.Drawing.Size(139, 23);
            this.NewString.TabIndex = 0;
            this.NewString.Text = "New";
            this.NewString.UseVisualStyleBackColor = true;
            this.NewString.Click += new System.EventHandler(this.NewStringButton_Click);
            // 
            // DeleteString
            // 
            this.DeleteString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeleteString.Location = new System.Drawing.Point(148, 3);
            this.DeleteString.Name = "DeleteString";
            this.DeleteString.Size = new System.Drawing.Size(139, 23);
            this.DeleteString.TabIndex = 1;
            this.DeleteString.Text = "Delete";
            this.DeleteString.UseVisualStyleBackColor = true;
            this.DeleteString.Click += new System.EventHandler(this.DeleteStringButton_Click);
            // 
            // StringPhrasesPage
            // 
            this.StringPhrasesPage.BackColor = System.Drawing.SystemColors.Control;
            this.StringPhrasesPage.Controls.Add(this.tableLayoutPanel10);
            this.StringPhrasesPage.Location = new System.Drawing.Point(4, 22);
            this.StringPhrasesPage.Name = "StringPhrasesPage";
            this.StringPhrasesPage.Padding = new System.Windows.Forms.Padding(3);
            this.StringPhrasesPage.Size = new System.Drawing.Size(621, 418);
            this.StringPhrasesPage.TabIndex = 1;
            this.StringPhrasesPage.Text = "Phrases";
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tableLayoutPanel10.Controls.Add(this.StringsPhraseTree, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.tableLayoutPanel11, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(615, 412);
            this.tableLayoutPanel10.TabIndex = 0;
            // 
            // StringsPhraseTree
            // 
            this.StringsPhraseTree.Controls.Add(this.StringPhraseTree);
            this.StringsPhraseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringsPhraseTree.Location = new System.Drawing.Point(3, 3);
            this.StringsPhraseTree.Name = "StringsPhraseTree";
            this.StringsPhraseTree.Size = new System.Drawing.Size(408, 406);
            this.StringsPhraseTree.TabIndex = 0;
            this.StringsPhraseTree.TabStop = false;
            this.StringsPhraseTree.Text = "Phrase Tree";
            // 
            // StringPhraseTree
            // 
            this.StringPhraseTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringPhraseTree.Location = new System.Drawing.Point(3, 16);
            this.StringPhraseTree.Name = "StringPhraseTree";
            this.StringPhraseTree.Size = new System.Drawing.Size(402, 387);
            this.StringPhraseTree.TabIndex = 0;
            this.StringPhraseTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.StringPhraseTree_AfterSelect);
            this.StringPhraseTree.DoubleClick += new System.EventHandler(this.StringPhraseTreeGroup_MouseHover);
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 1;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.groupBox4, 0, 1);
            this.tableLayoutPanel11.Controls.Add(this.groupBox5, 0, 2);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(417, 3);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 3;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(195, 406);
            this.tableLayoutPanel11.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.StringWordsList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(189, 129);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Words";
            // 
            // StringWordsList
            // 
            this.StringWordsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringWordsList.FormattingEnabled = true;
            this.StringWordsList.Location = new System.Drawing.Point(3, 16);
            this.StringWordsList.Name = "StringWordsList";
            this.StringWordsList.Size = new System.Drawing.Size(183, 110);
            this.StringWordsList.TabIndex = 0;
            this.StringWordsList.SelectedIndexChanged += new System.EventHandler(this.StringPhrasesRemaningList_SelectedIndexChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.StringIncompletePhrasesList);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(3, 138);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(189, 129);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Incomplete Phrases";
            // 
            // StringIncompletePhrasesList
            // 
            this.StringIncompletePhrasesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringIncompletePhrasesList.FormattingEnabled = true;
            this.StringIncompletePhrasesList.Location = new System.Drawing.Point(3, 16);
            this.StringIncompletePhrasesList.Name = "StringIncompletePhrasesList";
            this.StringIncompletePhrasesList.Size = new System.Drawing.Size(183, 110);
            this.StringIncompletePhrasesList.TabIndex = 0;
            this.StringIncompletePhrasesList.SelectedIndexChanged += new System.EventHandler(this.StringIncompletePhrasesList_SelectedIndexChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.StringMissingWordsList);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(3, 273);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(189, 130);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Missing Words";
            // 
            // StringMissingWordsList
            // 
            this.StringMissingWordsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StringMissingWordsList.FormattingEnabled = true;
            this.StringMissingWordsList.Location = new System.Drawing.Point(3, 16);
            this.StringMissingWordsList.Name = "StringMissingWordsList";
            this.StringMissingWordsList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.StringMissingWordsList.Size = new System.Drawing.Size(183, 111);
            this.StringMissingWordsList.TabIndex = 0;
            // 
            // PreviewPage
            // 
            this.PreviewPage.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewPage.Controls.Add(this.tableLayoutPanel8);
            this.PreviewPage.Location = new System.Drawing.Point(4, 22);
            this.PreviewPage.Name = "PreviewPage";
            this.PreviewPage.Size = new System.Drawing.Size(919, 456);
            this.PreviewPage.TabIndex = 2;
            this.PreviewPage.Text = "Preview";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel23, 0, 2);
            this.tableLayoutPanel8.Controls.Add(this.PreviewTextBox, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.PreviewProcessBox, 0, 1);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(919, 456);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // tableLayoutPanel23
            // 
            this.tableLayoutPanel23.ColumnCount = 3;
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel23.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel23.Controls.Add(this.PreviewPlayButton, 2, 0);
            this.tableLayoutPanel23.Controls.Add(this.PreviewSaveAudioButton, 1, 0);
            this.tableLayoutPanel23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel23.Location = new System.Drawing.Point(3, 423);
            this.tableLayoutPanel23.Name = "tableLayoutPanel23";
            this.tableLayoutPanel23.RowCount = 1;
            this.tableLayoutPanel23.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel23.Size = new System.Drawing.Size(913, 30);
            this.tableLayoutPanel23.TabIndex = 0;
            // 
            // PreviewPlayButton
            // 
            this.PreviewPlayButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewPlayButton.Enabled = false;
            this.PreviewPlayButton.Location = new System.Drawing.Point(816, 3);
            this.PreviewPlayButton.Name = "PreviewPlayButton";
            this.PreviewPlayButton.Size = new System.Drawing.Size(94, 24);
            this.PreviewPlayButton.TabIndex = 0;
            this.PreviewPlayButton.Text = "Play Audio";
            this.PreviewPlayButton.UseVisualStyleBackColor = true;
            this.PreviewPlayButton.Click += new System.EventHandler(this.PreviewPlayButton_Click);
            // 
            // PreviewSaveAudioButton
            // 
            this.PreviewSaveAudioButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewSaveAudioButton.Enabled = false;
            this.PreviewSaveAudioButton.Location = new System.Drawing.Point(716, 3);
            this.PreviewSaveAudioButton.Name = "PreviewSaveAudioButton";
            this.PreviewSaveAudioButton.Size = new System.Drawing.Size(94, 24);
            this.PreviewSaveAudioButton.TabIndex = 1;
            this.PreviewSaveAudioButton.Text = "Save Audio";
            this.PreviewSaveAudioButton.UseVisualStyleBackColor = true;
            this.PreviewSaveAudioButton.Click += new System.EventHandler(this.PreviewSaveAudioButton_Click);
            // 
            // PreviewTextBox
            // 
            this.PreviewTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewTextBox.Location = new System.Drawing.Point(3, 3);
            this.PreviewTextBox.Multiline = true;
            this.PreviewTextBox.Name = "PreviewTextBox";
            this.PreviewTextBox.Size = new System.Drawing.Size(913, 204);
            this.PreviewTextBox.TabIndex = 1;
            this.PreviewTextBox.TextChanged += new System.EventHandler(this.PreviewTextBox_TextChanged);
            // 
            // PreviewProcessBox
            // 
            this.PreviewProcessBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewProcessBox.Location = new System.Drawing.Point(3, 213);
            this.PreviewProcessBox.Multiline = true;
            this.PreviewProcessBox.Name = "PreviewProcessBox";
            this.PreviewProcessBox.ReadOnly = true;
            this.PreviewProcessBox.Size = new System.Drawing.Size(913, 204);
            this.PreviewProcessBox.TabIndex = 2;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 506);
            this.Controls.Add(this.EditorContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Editor";
            this.Text = "Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.EditorContainer.ResumeLayout(false);
            this.EditorPages.ResumeLayout(false);
            this.AudioPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AudioOffsetL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AudioOffsetR)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.StringsPage.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.StringProperties.ResumeLayout(false);
            this.StringMetadataGroup.ResumeLayout(false);
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.tableLayoutPanel15.ResumeLayout(false);
            this.StringActionGroup.ResumeLayout(false);
            this.tableLayoutPanel16.ResumeLayout(false);
            this.tableLayoutPanel17.ResumeLayout(false);
            this.tableLayoutPanel17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StringActionDelay)).EndInit();
            this.tableLayoutPanel18.ResumeLayout(false);
            this.tableLayoutPanel18.PerformLayout();
            this.tableLayoutPanel19.ResumeLayout(false);
            this.StringViewPages.ResumeLayout(false);
            this.StringTagsPage.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel20.ResumeLayout(false);
            this.tableLayoutPanel21.ResumeLayout(false);
            this.tableLayoutPanel21.PerformLayout();
            this.tableLayoutPanel22.ResumeLayout(false);
            this.StringPhrasesPage.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.StringsPhraseTree.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.PreviewPage.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel23.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToStarterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToDesktopToolStripMenuItem;
        private System.Windows.Forms.Panel EditorContainer;
        private System.Windows.Forms.TabControl EditorPages;
        private System.Windows.Forms.TabPage AudioPage;
        private System.Windows.Forms.TabPage StringsPage;
        private System.Windows.Forms.TabPage PreviewPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView AudioTreeView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button AudioExtractAudioFiles;
        private System.Windows.Forms.Button AudioExtractDataFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.CheckBox AudioAutoCropping;
        private System.Windows.Forms.Label AudioAutoCroppingLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown AudioOffsetL;
        private System.Windows.Forms.NumericUpDown AudioOffsetR;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox AudioFileLocation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button AudioImportFiles;
        private System.Windows.Forms.Button AudioImportFolders;
        private System.Windows.Forms.Button AudioRecord;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TableLayoutPanel StringProperties;
        private System.Windows.Forms.GroupBox StringMetadataGroup;
        private System.Windows.Forms.GroupBox StringActionGroup;
        private System.Windows.Forms.TabControl StringViewPages;
        private System.Windows.Forms.TabPage StringTagsPage;
        private System.Windows.Forms.TabPage StringPhrasesPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.GroupBox StringsPhraseTree;
        private System.Windows.Forms.TreeView StringPhraseTree;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox StringWordsList;
        private System.Windows.Forms.ListBox TagList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox StringID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox StringText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox StringTagList;
        private System.Windows.Forms.Button StringAddTag;
        private System.Windows.Forms.Button StringRemoveTag;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel16;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel17;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox StringAction;
        private System.Windows.Forms.NumericUpDown StringActionDelay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel18;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox StringAudioFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel19;
        private System.Windows.Forms.Button StringLinkAudioButton;
        private System.Windows.Forms.Button StringAutoLinkAudio;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel20;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel21;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox StringListFilter;
        private System.Windows.Forms.ListBox StringList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel22;
        private System.Windows.Forms.Button NewString;
        private System.Windows.Forms.Button DeleteString;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel23;
        private System.Windows.Forms.Button PreviewPlayButton;
        private System.Windows.Forms.Button PreviewSaveAudioButton;
        private System.Windows.Forms.TextBox PreviewTextBox;
        private System.Windows.Forms.TextBox PreviewProcessBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox StringIncompletePhrasesList;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListBox StringMissingWordsList;
        private System.Windows.Forms.Button ImportDataFileButton;
        private System.Windows.Forms.Button RemoveAudioButton;
    }
}