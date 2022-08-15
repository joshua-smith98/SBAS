namespace SBAS
{
    partial class Importer
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
            this.ImportButton = new System.Windows.Forms.Button();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.FileNumber = new System.Windows.Forms.Label();
            this.DataGroup = new System.Windows.Forms.GroupBox();
            this.AudioGroup = new System.Windows.Forms.GroupBox();
            this.GenerateText = new System.Windows.Forms.CheckBox();
            this.AutoCropping = new System.Windows.Forms.CheckBox();
            this.TagSubfolders = new System.Windows.Forms.CheckBox();
            this.CreateStrings = new System.Windows.Forms.CheckBox();
            this.SearchForNames = new System.Windows.Forms.Button();
            this.DataNamedLabel = new System.Windows.Forms.Label();
            this.DataGroup.SuspendLayout();
            this.AudioGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImportButton
            // 
            this.ImportButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.ImportButton.Location = new System.Drawing.Point(314, 219);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 23);
            this.ImportButton.TabIndex = 0;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // CancelButton1
            // 
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(233, 219);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(75, 23);
            this.CancelButton1.TabIndex = 1;
            this.CancelButton1.Text = "Cancel";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton1_Click);
            // 
            // FileNumber
            // 
            this.FileNumber.AutoSize = true;
            this.FileNumber.Location = new System.Drawing.Point(12, 9);
            this.FileNumber.Name = "FileNumber";
            this.FileNumber.Size = new System.Drawing.Size(157, 13);
            this.FileNumber.TabIndex = 2;
            this.FileNumber.Text = "X Files Selected in X Subfolders";
            // 
            // DataGroup
            // 
            this.DataGroup.Controls.Add(this.DataNamedLabel);
            this.DataGroup.Controls.Add(this.SearchForNames);
            this.DataGroup.Location = new System.Drawing.Point(204, 24);
            this.DataGroup.Name = "DataGroup";
            this.DataGroup.Size = new System.Drawing.Size(185, 189);
            this.DataGroup.TabIndex = 3;
            this.DataGroup.TabStop = false;
            this.DataGroup.Text = "Data Files";
            // 
            // AudioGroup
            // 
            this.AudioGroup.Controls.Add(this.GenerateText);
            this.AudioGroup.Controls.Add(this.AutoCropping);
            this.AudioGroup.Controls.Add(this.TagSubfolders);
            this.AudioGroup.Controls.Add(this.CreateStrings);
            this.AudioGroup.Location = new System.Drawing.Point(12, 25);
            this.AudioGroup.Name = "AudioGroup";
            this.AudioGroup.Size = new System.Drawing.Size(186, 188);
            this.AudioGroup.TabIndex = 4;
            this.AudioGroup.TabStop = false;
            this.AudioGroup.Text = "Audio Files";
            // 
            // GenerateText
            // 
            this.GenerateText.AutoSize = true;
            this.GenerateText.Checked = true;
            this.GenerateText.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GenerateText.Location = new System.Drawing.Point(29, 43);
            this.GenerateText.Name = "GenerateText";
            this.GenerateText.Size = new System.Drawing.Size(94, 17);
            this.GenerateText.TabIndex = 3;
            this.GenerateText.Text = "Generate Text";
            this.GenerateText.UseVisualStyleBackColor = true;
            // 
            // AutoCropping
            // 
            this.AutoCropping.AutoSize = true;
            this.AutoCropping.Checked = true;
            this.AutoCropping.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoCropping.Location = new System.Drawing.Point(7, 89);
            this.AutoCropping.Name = "AutoCropping";
            this.AutoCropping.Size = new System.Drawing.Size(140, 17);
            this.AutoCropping.TabIndex = 2;
            this.AutoCropping.Text = "Calculate Auto-Cropping";
            this.AutoCropping.UseVisualStyleBackColor = true;
            // 
            // TagSubfolders
            // 
            this.TagSubfolders.AutoSize = true;
            this.TagSubfolders.Checked = true;
            this.TagSubfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TagSubfolders.Location = new System.Drawing.Point(29, 66);
            this.TagSubfolders.Name = "TagSubfolders";
            this.TagSubfolders.Size = new System.Drawing.Size(151, 17);
            this.TagSubfolders.TabIndex = 1;
            this.TagSubfolders.Text = "Tag with Subfolder Names";
            this.TagSubfolders.UseVisualStyleBackColor = true;
            // 
            // CreateStrings
            // 
            this.CreateStrings.AutoSize = true;
            this.CreateStrings.Checked = true;
            this.CreateStrings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CreateStrings.Location = new System.Drawing.Point(7, 20);
            this.CreateStrings.Name = "CreateStrings";
            this.CreateStrings.Size = new System.Drawing.Size(92, 17);
            this.CreateStrings.TabIndex = 0;
            this.CreateStrings.Text = "Create Strings";
            this.CreateStrings.UseVisualStyleBackColor = true;
            this.CreateStrings.CheckedChanged += new System.EventHandler(this.CreateStrings_CheckedChanged);
            // 
            // SearchForNames
            // 
            this.SearchForNames.Location = new System.Drawing.Point(12, 21);
            this.SearchForNames.Name = "SearchForNames";
            this.SearchForNames.Size = new System.Drawing.Size(167, 23);
            this.SearchForNames.TabIndex = 7;
            this.SearchForNames.Text = "Search for Filenames in...";
            this.SearchForNames.UseVisualStyleBackColor = true;
            this.SearchForNames.Click += new System.EventHandler(this.button1_Click);
            // 
            // DataNamedLabel
            // 
            this.DataNamedLabel.AutoSize = true;
            this.DataNamedLabel.Location = new System.Drawing.Point(9, 47);
            this.DataNamedLabel.Name = "DataNamedLabel";
            this.DataNamedLabel.Size = new System.Drawing.Size(119, 13);
            this.DataNamedLabel.TabIndex = 8;
            this.DataNamedLabel.Text = "0 of 0 Filenames Linked";
            // 
            // Importer
            // 
            this.AcceptButton = this.ImportButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelButton1;
            this.ClientSize = new System.Drawing.Size(401, 254);
            this.ControlBox = false;
            this.Controls.Add(this.AudioGroup);
            this.Controls.Add(this.DataGroup);
            this.Controls.Add(this.FileNumber);
            this.Controls.Add(this.CancelButton1);
            this.Controls.Add(this.ImportButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "Importer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Import";
            this.Shown += new System.EventHandler(this.Importer_Shown);
            this.DataGroup.ResumeLayout(false);
            this.DataGroup.PerformLayout();
            this.AudioGroup.ResumeLayout(false);
            this.AudioGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button CancelButton1;
        private System.Windows.Forms.Label FileNumber;
        private System.Windows.Forms.GroupBox DataGroup;
        private System.Windows.Forms.GroupBox AudioGroup;
        private System.Windows.Forms.CheckBox AutoCropping;
        private System.Windows.Forms.CheckBox TagSubfolders;
        private System.Windows.Forms.CheckBox CreateStrings;
        private System.Windows.Forms.CheckBox GenerateText;
        private System.Windows.Forms.Button SearchForNames;
        private System.Windows.Forms.Label DataNamedLabel;
    }
}