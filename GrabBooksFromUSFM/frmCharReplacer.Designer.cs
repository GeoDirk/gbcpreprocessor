namespace GBC_USFM_Preprocessor
{
    partial class frmCharReplacer
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
            this.gFonts = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdSaveFile = new System.Windows.Forms.Button();
            this.cmdOpenFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.gFonts)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gFonts
            // 
            this.gFonts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gFonts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gFonts.Location = new System.Drawing.Point(0, 0);
            this.gFonts.Name = "gFonts";
            this.gFonts.Size = new System.Drawing.Size(346, 403);
            this.gFonts.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdClear);
            this.panel1.Controls.Add(this.cmdClose);
            this.panel1.Controls.Add(this.cmdSaveFile);
            this.panel1.Controls.Add(this.cmdOpenFile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 403);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 55);
            this.panel1.TabIndex = 1;
            // 
            // cmdClear
            // 
            this.cmdClear.Location = new System.Drawing.Point(249, 3);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(94, 23);
            this.cmdClear.TabIndex = 3;
            this.cmdClear.Text = "Clear All";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(249, 29);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(94, 23);
            this.cmdClose.TabIndex = 2;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdSaveFile
            // 
            this.cmdSaveFile.Location = new System.Drawing.Point(3, 29);
            this.cmdSaveFile.Name = "cmdSaveFile";
            this.cmdSaveFile.Size = new System.Drawing.Size(217, 23);
            this.cmdSaveFile.TabIndex = 1;
            this.cmdSaveFile.Text = "Save Char Definition File";
            this.cmdSaveFile.UseVisualStyleBackColor = true;
            this.cmdSaveFile.Click += new System.EventHandler(this.cmdSaveFile_Click);
            // 
            // cmdOpenFile
            // 
            this.cmdOpenFile.Location = new System.Drawing.Point(3, 3);
            this.cmdOpenFile.Name = "cmdOpenFile";
            this.cmdOpenFile.Size = new System.Drawing.Size(217, 23);
            this.cmdOpenFile.TabIndex = 0;
            this.cmdOpenFile.Text = "Open Existing Char Definition File";
            this.cmdOpenFile.UseVisualStyleBackColor = true;
            this.cmdOpenFile.Click += new System.EventHandler(this.cmdOpenFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmCharReplacer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 458);
            this.Controls.Add(this.gFonts);
            this.Controls.Add(this.panel1);
            this.Name = "frmCharReplacer";
            this.Text = "Character Replacer to HTML Code";
            this.Load += new System.EventHandler(this.frmCharReplacer_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCharReplacer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gFonts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gFonts;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdSaveFile;
        private System.Windows.Forms.Button cmdOpenFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button cmdClear;
    }
}