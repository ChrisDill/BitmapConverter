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
            this.btnLoad = new System.Windows.Forms.Button();
            this.pbLabel = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbLabel)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(95, 29);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "&Load EPL file";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // pbLabel
            // 
            this.pbLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLabel.Location = new System.Drawing.Point(12, 111);
            this.pbLabel.Name = "pbLabel";
            this.pbLabel.Size = new System.Drawing.Size(300, 300);
            this.pbLabel.TabIndex = 1;
            this.pbLabel.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleName = "";
            this.btnSave.Location = new System.Drawing.Point(113, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 29);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save EPL bitmap";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Location = new System.Drawing.Point(-2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 93);
            this.panel1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 430);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pbLabel);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "EPL2Bitmap";
            ((System.ComponentModel.ISupportInitialize)(this.pbLabel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.PictureBox pbLabel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
    }
}

