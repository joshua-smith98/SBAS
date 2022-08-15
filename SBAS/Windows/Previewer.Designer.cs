namespace SBAS
{
    partial class Previewer
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
            this.TextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.PreviewerBar = new System.Windows.Forms.ToolStripProgressBar();
            this.PreviewerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.PlayButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.OptimiseButton = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBox
            // 
            this.TextBox.Location = new System.Drawing.Point(12, 12);
            this.TextBox.Multiline = true;
            this.TextBox.Name = "TextBox";
            this.TextBox.Size = new System.Drawing.Size(776, 201);
            this.TextBox.TabIndex = 0;
            this.TextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PreviewerBar,
            this.PreviewerStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // PreviewerBar
            // 
            this.PreviewerBar.Name = "PreviewerBar";
            this.PreviewerBar.Size = new System.Drawing.Size(100, 16);
            // 
            // PreviewerStatus
            // 
            this.PreviewerStatus.Name = "PreviewerStatus";
            this.PreviewerStatus.Size = new System.Drawing.Size(118, 17);
            this.PreviewerStatus.Text = "toolStripStatusLabel1";
            // 
            // PlayButton
            // 
            this.PlayButton.Enabled = false;
            this.PlayButton.Location = new System.Drawing.Point(680, 402);
            this.PlayButton.Name = "PlayButton";
            this.PlayButton.Size = new System.Drawing.Size(108, 23);
            this.PlayButton.TabIndex = 2;
            this.PlayButton.Text = "Play";
            this.PlayButton.UseVisualStyleBackColor = true;
            this.PlayButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(12, 220);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 176);
            this.textBox1.TabIndex = 4;
            // 
            // OptimiseButton
            // 
            this.OptimiseButton.Location = new System.Drawing.Point(565, 402);
            this.OptimiseButton.Name = "OptimiseButton";
            this.OptimiseButton.Size = new System.Drawing.Size(109, 23);
            this.OptimiseButton.TabIndex = 5;
            this.OptimiseButton.Text = "Optimise";
            this.OptimiseButton.UseVisualStyleBackColor = true;
            // 
            // Previewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OptimiseButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.PlayButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Previewer";
            this.Text = "Previewer";
            this.Shown += new System.EventHandler(this.Previewer_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar PreviewerBar;
        private System.Windows.Forms.ToolStripStatusLabel PreviewerStatus;
        private System.Windows.Forms.Button PlayButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button OptimiseButton;
    }
}