namespace SBAS
{
    partial class Player
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
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToStarterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToDesktopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.LinesTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.LinesGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.NewLine = new System.Windows.Forms.Button();
            this.DeleteLine = new System.Windows.Forms.Button();
            this.LineList = new System.Windows.Forms.ListBox();
            this.LinePropGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.LineName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BranchFrom = new System.Windows.Forms.ComboBox();
            this.BranchAt = new System.Windows.Forms.ComboBox();
            this.ConnectTo = new System.Windows.Forms.ComboBox();
            this.ConnectAt = new System.Windows.Forms.ComboBox();
            this.StopsGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.NewStop = new System.Windows.Forms.Button();
            this.DeleteStop = new System.Windows.Forms.Button();
            this.StopList = new System.Windows.Forms.ListBox();
            this.StopPropGroup = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.StopName = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.StopMoveUpButton = new System.Windows.Forms.Button();
            this.StopMoveDownButton = new System.Windows.Forms.Button();
            this.ScriptsTab = new System.Windows.Forms.TabPage();
            this.PreviewTab = new System.Windows.Forms.TabPage();
            this.ReverseStops = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.LinesTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.LinesGroup.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.LinePropGroup.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.StopsGroup.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.StopPropGroup.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(828, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
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
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            this.newProjectToolStripMenuItem.Click += new System.EventHandler(this.newProjectToolStripMenuItem_Click);
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_Click);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project As";
            this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_Click);
            // 
            // closeProjectToolStripMenuItem
            // 
            this.closeProjectToolStripMenuItem.Name = "closeProjectToolStripMenuItem";
            this.closeProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.closeProjectToolStripMenuItem.Text = "Close Project";
            this.closeProjectToolStripMenuItem.Click += new System.EventHandler(this.closeProjectToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // exitToStarterToolStripMenuItem
            // 
            this.exitToStarterToolStripMenuItem.Name = "exitToStarterToolStripMenuItem";
            this.exitToStarterToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToStarterToolStripMenuItem.Text = "Exit to Starter";
            this.exitToStarterToolStripMenuItem.Click += new System.EventHandler(this.exitToStarterToolStripMenuItem_Click);
            // 
            // exitToDesktopToolStripMenuItem
            // 
            this.exitToDesktopToolStripMenuItem.Name = "exitToDesktopToolStripMenuItem";
            this.exitToDesktopToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToDesktopToolStripMenuItem.Text = "Exit to Desktop";
            this.exitToDesktopToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.LinesTab);
            this.tabControl1.Controls.Add(this.ScriptsTab);
            this.tabControl1.Controls.Add(this.PreviewTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(828, 444);
            this.tabControl1.TabIndex = 1;
            // 
            // LinesTab
            // 
            this.LinesTab.BackColor = System.Drawing.SystemColors.Control;
            this.LinesTab.Controls.Add(this.tableLayoutPanel1);
            this.LinesTab.Location = new System.Drawing.Point(4, 22);
            this.LinesTab.Name = "LinesTab";
            this.LinesTab.Padding = new System.Windows.Forms.Padding(3);
            this.LinesTab.Size = new System.Drawing.Size(820, 418);
            this.LinesTab.TabIndex = 1;
            this.LinesTab.Text = "Lines";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.LinesGroup, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.LinePropGroup, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.StopsGroup, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.StopPropGroup, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(814, 412);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // LinesGroup
            // 
            this.LinesGroup.Controls.Add(this.tableLayoutPanel2);
            this.LinesGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LinesGroup.Location = new System.Drawing.Point(3, 3);
            this.LinesGroup.Name = "LinesGroup";
            this.LinesGroup.Size = new System.Drawing.Size(197, 406);
            this.LinesGroup.TabIndex = 0;
            this.LinesGroup.TabStop = false;
            this.LinesGroup.Text = "Lines";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.LineList, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(191, 387);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.NewLine, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.DeleteLine, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 355);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(185, 29);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // NewLine
            // 
            this.NewLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewLine.Location = new System.Drawing.Point(3, 3);
            this.NewLine.Name = "NewLine";
            this.NewLine.Size = new System.Drawing.Size(86, 23);
            this.NewLine.TabIndex = 0;
            this.NewLine.Text = "New";
            this.NewLine.UseVisualStyleBackColor = true;
            this.NewLine.Click += new System.EventHandler(this.NewLine_Click);
            // 
            // DeleteLine
            // 
            this.DeleteLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeleteLine.Location = new System.Drawing.Point(95, 3);
            this.DeleteLine.Name = "DeleteLine";
            this.DeleteLine.Size = new System.Drawing.Size(87, 23);
            this.DeleteLine.TabIndex = 1;
            this.DeleteLine.Text = "Delete";
            this.DeleteLine.UseVisualStyleBackColor = true;
            this.DeleteLine.Click += new System.EventHandler(this.DeleteLine_Click);
            // 
            // LineList
            // 
            this.LineList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineList.FormattingEnabled = true;
            this.LineList.Location = new System.Drawing.Point(3, 3);
            this.LineList.Name = "LineList";
            this.LineList.Size = new System.Drawing.Size(185, 346);
            this.LineList.TabIndex = 1;
            this.LineList.SelectedIndexChanged += new System.EventHandler(this.LineList_SelectedIndexChanged);
            // 
            // LinePropGroup
            // 
            this.LinePropGroup.Controls.Add(this.tableLayoutPanel6);
            this.LinePropGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LinePropGroup.Location = new System.Drawing.Point(206, 3);
            this.LinePropGroup.Name = "LinePropGroup";
            this.LinePropGroup.Size = new System.Drawing.Size(197, 406);
            this.LinePropGroup.TabIndex = 1;
            this.LinePropGroup.TabStop = false;
            this.LinePropGroup.Text = "Line Properties";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.label5, 0, 7);
            this.tableLayoutPanel6.Controls.Add(this.BranchFrom, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.BranchAt, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.ConnectTo, 0, 6);
            this.tableLayoutPanel6.Controls.Add(this.ConnectAt, 0, 8);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 10;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(191, 387);
            this.tableLayoutPanel6.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.LineName, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(185, 27);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // LineName
            // 
            this.LineName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LineName.Location = new System.Drawing.Point(58, 3);
            this.LineName.Name = "LineName";
            this.LineName.Size = new System.Drawing.Size(124, 20);
            this.LineName.TabIndex = 0;
            this.LineName.TextChanged += new System.EventHandler(this.LineName_TextChanged);
            this.LineName.Validated += new System.EventHandler(this.LineName_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "North Connection:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "From:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Location = new System.Drawing.Point(3, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 25);
            this.label4.TabIndex = 3;
            this.label4.Text = "South Connection:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Location = new System.Drawing.Point(3, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 25);
            this.label5.TabIndex = 4;
            this.label5.Text = "From:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BranchFrom
            // 
            this.BranchFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BranchFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BranchFrom.FormattingEnabled = true;
            this.BranchFrom.Location = new System.Drawing.Point(3, 61);
            this.BranchFrom.Name = "BranchFrom";
            this.BranchFrom.Size = new System.Drawing.Size(185, 21);
            this.BranchFrom.TabIndex = 5;
            this.BranchFrom.SelectedIndexChanged += new System.EventHandler(this.BranchFrom_SelectedIndexChanged);
            // 
            // BranchAt
            // 
            this.BranchAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BranchAt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BranchAt.FormattingEnabled = true;
            this.BranchAt.Location = new System.Drawing.Point(3, 111);
            this.BranchAt.Name = "BranchAt";
            this.BranchAt.Size = new System.Drawing.Size(185, 21);
            this.BranchAt.TabIndex = 6;
            this.BranchAt.SelectedIndexChanged += new System.EventHandler(this.BranchAt_SelectedIndexChanged);
            // 
            // ConnectTo
            // 
            this.ConnectTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConnectTo.FormattingEnabled = true;
            this.ConnectTo.Location = new System.Drawing.Point(3, 161);
            this.ConnectTo.Name = "ConnectTo";
            this.ConnectTo.Size = new System.Drawing.Size(185, 21);
            this.ConnectTo.TabIndex = 7;
            this.ConnectTo.SelectedIndexChanged += new System.EventHandler(this.ConnectTo_SelectedIndexChanged);
            // 
            // ConnectAt
            // 
            this.ConnectAt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectAt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConnectAt.FormattingEnabled = true;
            this.ConnectAt.Location = new System.Drawing.Point(3, 211);
            this.ConnectAt.Name = "ConnectAt";
            this.ConnectAt.Size = new System.Drawing.Size(185, 21);
            this.ConnectAt.TabIndex = 8;
            this.ConnectAt.SelectedIndexChanged += new System.EventHandler(this.ConnectAt_SelectedIndexChanged);
            // 
            // StopsGroup
            // 
            this.StopsGroup.Controls.Add(this.tableLayoutPanel4);
            this.StopsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopsGroup.Location = new System.Drawing.Point(409, 3);
            this.StopsGroup.Name = "StopsGroup";
            this.StopsGroup.Size = new System.Drawing.Size(197, 406);
            this.StopsGroup.TabIndex = 2;
            this.StopsGroup.TabStop = false;
            this.StopsGroup.Text = "Stops";
            this.StopsGroup.Enter += new System.EventHandler(this.StopsGroup_Enter);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.StopList, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(191, 387);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.NewStop, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.DeleteStop, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 355);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(185, 29);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // NewStop
            // 
            this.NewStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NewStop.Location = new System.Drawing.Point(3, 3);
            this.NewStop.Name = "NewStop";
            this.NewStop.Size = new System.Drawing.Size(86, 23);
            this.NewStop.TabIndex = 0;
            this.NewStop.Text = "New";
            this.NewStop.UseVisualStyleBackColor = true;
            this.NewStop.Click += new System.EventHandler(this.NewStop_Click);
            // 
            // DeleteStop
            // 
            this.DeleteStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DeleteStop.Location = new System.Drawing.Point(95, 3);
            this.DeleteStop.Name = "DeleteStop";
            this.DeleteStop.Size = new System.Drawing.Size(87, 23);
            this.DeleteStop.TabIndex = 1;
            this.DeleteStop.Text = "Delete";
            this.DeleteStop.UseVisualStyleBackColor = true;
            this.DeleteStop.Click += new System.EventHandler(this.DeleteStop_Click);
            // 
            // StopList
            // 
            this.StopList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopList.FormattingEnabled = true;
            this.StopList.Location = new System.Drawing.Point(3, 3);
            this.StopList.Name = "StopList";
            this.StopList.Size = new System.Drawing.Size(185, 346);
            this.StopList.TabIndex = 1;
            this.StopList.SelectedIndexChanged += new System.EventHandler(this.StopList_SelectedIndexChanged);
            // 
            // StopPropGroup
            // 
            this.StopPropGroup.Controls.Add(this.tableLayoutPanel8);
            this.StopPropGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopPropGroup.Location = new System.Drawing.Point(612, 3);
            this.StopPropGroup.Name = "StopPropGroup";
            this.StopPropGroup.Size = new System.Drawing.Size(199, 406);
            this.StopPropGroup.TabIndex = 3;
            this.StopPropGroup.TabStop = false;
            this.StopPropGroup.Text = "Stop Properties";
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel10, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.ReverseStops, 0, 2);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 4;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 33F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(193, 387);
            this.tableLayoutPanel8.TabIndex = 0;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.StopName, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(187, 27);
            this.tableLayoutPanel9.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 27);
            this.label6.TabIndex = 0;
            this.label6.Text = "Name:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StopName
            // 
            this.StopName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.StopName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.StopName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopName.FormattingEnabled = true;
            this.StopName.Location = new System.Drawing.Point(58, 3);
            this.StopName.Name = "StopName";
            this.StopName.Size = new System.Drawing.Size(126, 21);
            this.StopName.TabIndex = 1;
            this.StopName.SelectedIndexChanged += new System.EventHandler(this.StopName_SelectedIndexChanged);
            this.StopName.TextUpdate += new System.EventHandler(this.StopName_TextUpdate);
            this.StopName.Validating += new System.ComponentModel.CancelEventHandler(this.StopName_Validating);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.StopMoveUpButton, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.StopMoveDownButton, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(3, 36);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(187, 29);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // StopMoveUpButton
            // 
            this.StopMoveUpButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopMoveUpButton.Location = new System.Drawing.Point(3, 3);
            this.StopMoveUpButton.Name = "StopMoveUpButton";
            this.StopMoveUpButton.Size = new System.Drawing.Size(87, 23);
            this.StopMoveUpButton.TabIndex = 0;
            this.StopMoveUpButton.Text = "Move Up";
            this.StopMoveUpButton.UseVisualStyleBackColor = true;
            this.StopMoveUpButton.Click += new System.EventHandler(this.StopMoveUpButton_Click);
            // 
            // StopMoveDownButton
            // 
            this.StopMoveDownButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.StopMoveDownButton.Location = new System.Drawing.Point(96, 3);
            this.StopMoveDownButton.Name = "StopMoveDownButton";
            this.StopMoveDownButton.Size = new System.Drawing.Size(88, 23);
            this.StopMoveDownButton.TabIndex = 1;
            this.StopMoveDownButton.Text = "Move Down";
            this.StopMoveDownButton.UseVisualStyleBackColor = true;
            this.StopMoveDownButton.Click += new System.EventHandler(this.StopMoveDownButton_Click);
            // 
            // ScriptsTab
            // 
            this.ScriptsTab.BackColor = System.Drawing.SystemColors.Control;
            this.ScriptsTab.Location = new System.Drawing.Point(4, 22);
            this.ScriptsTab.Name = "ScriptsTab";
            this.ScriptsTab.Padding = new System.Windows.Forms.Padding(3);
            this.ScriptsTab.Size = new System.Drawing.Size(820, 418);
            this.ScriptsTab.TabIndex = 0;
            this.ScriptsTab.Text = "Scripts";
            // 
            // PreviewTab
            // 
            this.PreviewTab.BackColor = System.Drawing.SystemColors.Control;
            this.PreviewTab.Location = new System.Drawing.Point(4, 22);
            this.PreviewTab.Name = "PreviewTab";
            this.PreviewTab.Padding = new System.Windows.Forms.Padding(3);
            this.PreviewTab.Size = new System.Drawing.Size(820, 418);
            this.PreviewTab.TabIndex = 2;
            this.PreviewTab.Text = "Preview";
            // 
            // ReverseStops
            // 
            this.ReverseStops.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReverseStops.Location = new System.Drawing.Point(3, 71);
            this.ReverseStops.Name = "ReverseStops";
            this.ReverseStops.Size = new System.Drawing.Size(187, 24);
            this.ReverseStops.TabIndex = 2;
            this.ReverseStops.Text = "Reverse Stops";
            this.ReverseStops.UseVisualStyleBackColor = true;
            this.ReverseStops.Click += new System.EventHandler(this.ReverseStops_Click);
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(828, 468);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Player";
            this.Text = "Router";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.LinesTab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.LinesGroup.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.LinePropGroup.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.StopsGroup.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.StopPropGroup.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToStarterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToDesktopToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ScriptsTab;
        private System.Windows.Forms.TabPage LinesTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox LinesGroup;
        private System.Windows.Forms.TabPage PreviewTab;
        private System.Windows.Forms.GroupBox LinePropGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button NewLine;
        private System.Windows.Forms.Button DeleteLine;
        private System.Windows.Forms.ListBox LineList;
        private System.Windows.Forms.GroupBox StopsGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button NewStop;
        private System.Windows.Forms.Button DeleteStop;
        private System.Windows.Forms.ListBox StopList;
        private System.Windows.Forms.GroupBox StopPropGroup;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.TextBox LineName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox BranchFrom;
        private System.Windows.Forms.ComboBox BranchAt;
        private System.Windows.Forms.ComboBox ConnectTo;
        private System.Windows.Forms.ComboBox ConnectAt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox StopName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Button StopMoveUpButton;
        private System.Windows.Forms.Button StopMoveDownButton;
        private System.Windows.Forms.Button ReverseStops;
    }
}