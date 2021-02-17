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
            this.saveEPLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.githubSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtEditor = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.txtFilename = new System.Windows.Forms.TextBox();
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
            this.saveEPLToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.githubSourceToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(600, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // loadEPLToolStripMenuItem
            // 
            this.loadEPLToolStripMenuItem.Name = "loadEPLToolStripMenuItem";
            this.loadEPLToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.loadEPLToolStripMenuItem.Text = "Load file";
            this.loadEPLToolStripMenuItem.Click += new System.EventHandler(this.loadEPLToolStripMenuItem_Click);
            // 
            // saveEPLToolStripMenuItem
            // 
            this.saveEPLToolStripMenuItem.Name = "saveEPLToolStripMenuItem";
            this.saveEPLToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.saveEPLToolStripMenuItem.Text = "Save bitmap";
            this.saveEPLToolStripMenuItem.Click += new System.EventHandler(this.saveEPLToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.saveFileToolStripMenuItem.Text = "Save file";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
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
            this.panel1.Size = new System.Drawing.Size(600, 50);
            this.panel1.TabIndex = 3;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "EPL",
            "ZPL"});
            this.cmbType.Location = new System.Drawing.Point(305, 50);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(93, 21);
            this.cmbType.TabIndex = 5;
            // 
            // txtEditor
            // 
            this.txtEditor.Location = new System.Drawing.Point(305, 75);
            this.txtEditor.Multiline = true;
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.Size = new System.Drawing.Size(280, 275);
            this.txtEditor.TabIndex = 4;
            this.txtEditor.TextChanged += new System.EventHandler(this.txtEditor_TextChanged);
            this.txtEditor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEditor_KeyDown);
            // 
            // txtFilename
            // 
            this.txtFilename.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilename.Location = new System.Drawing.Point(420, 50);
            this.txtFilename.Name = "txtFilename";
            this.txtFilename.ReadOnly = true;
            this.txtFilename.Size = new System.Drawing.Size(165, 20);
            this.txtFilename.TabIndex = 6;
            this.txtFilename.Text = "untitled";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 360);
            this.Controls.Add(this.txtFilename);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.txtEditor);
            this.Controls.Add(this.pbLabel);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "EPL2Bitmap";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.TextBox txtEditor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFilename;
    }
}

