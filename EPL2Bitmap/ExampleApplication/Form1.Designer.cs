namespace ExampleApplication
{
    partial class Form1
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
            this.pbLabel = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.loadEPLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadZPLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveEPLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLabel)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbLabel
            // 
            this.pbLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLabel.Location = new System.Drawing.Point(0, 50);
            this.pbLabel.Name = "pbLabel";
            this.pbLabel.Size = new System.Drawing.Size(300, 300);
            this.pbLabel.TabIndex = 1;
            this.pbLabel.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadEPLToolStripMenuItem,
            this.loadZPLToolStripMenuItem,
            this.saveEPLToolStripMenuItem,
            this.githubSourceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(585, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadEPLToolStripMenuItem
            // 
            this.loadEPLToolStripMenuItem.Name = "loadEPLToolStripMenuItem";
            this.loadEPLToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.loadEPLToolStripMenuItem.Text = "Load EPL";
            this.loadEPLToolStripMenuItem.Click += new System.EventHandler(this.loadEPLToolStripMenuItem_Click);
            // 
            // loadZPLToolStripMenuItem
            // 
            this.loadZPLToolStripMenuItem.Name = "loadZPLToolStripMenuItem";
            this.loadZPLToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.loadZPLToolStripMenuItem.Text = "Load ZPL";
            this.loadZPLToolStripMenuItem.Click += new System.EventHandler(this.loadZPLToolStripMenuItem_Click);
            // 
            // saveEPLToolStripMenuItem
            // 
            this.saveEPLToolStripMenuItem.Name = "saveEPLToolStripMenuItem";
            this.saveEPLToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.saveEPLToolStripMenuItem.Text = "Save bitmap";
            this.saveEPLToolStripMenuItem.Click += new System.EventHandler(this.saveEPLToolStripMenuItem_Click);
            // 
            // githubSourceToolStripMenuItem
            // 
            this.githubSourceToolStripMenuItem.Name = "githubSourceToolStripMenuItem";
            this.githubSourceToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.githubSourceToolStripMenuItem.Text = "Github source";
            this.githubSourceToolStripMenuItem.Click += new System.EventHandler(this.githubSourceToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Location = new System.Drawing.Point(-2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 50);
            this.panel1.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(306, 56);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(277, 135);
            this.textBox1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 349);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pbLabel);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "EPL2Bitmap";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pbLabel)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem loadEPLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEPLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem githubSourceToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem loadZPLToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
    }
}

