using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace GBC_USFM_Preprocessor
{
    public partial class frmSetBookOrder : Form
    {
        private frmMain _fMain;
        private ArrayList sFullClipboardText = new ArrayList();

        public frmSetBookOrder(frmMain f)
        {
            this._fMain = f;
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            string[] filePaths = System.IO.Directory.GetFiles(_fMain.USFM_FilesPath, "*." + _fMain.USFM_FilesExt);

            sFullClipboardText.Clear();
            sFullClipboardText.Add("//Source Directory Path: " + _fMain.USFM_FilesPath + @"\*." + _fMain.USFM_FilesExt + "\n");

            cUtils c = new cUtils();
            c.ExtractBookNames(filePaths, ref lvOutput, _fMain.USFM_FilesEncoding, txtBookNameTag.Text, sFullClipboardText);
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            using (TextWriter writer = File.CreateText(_fMain.USFM_FilesPath + @"\usfm_book_order." + _fMain.USFM_FilesExt + ".txt"))
            {
                writer.WriteLine("--GBC_USFM_Preprocessor Book Order File--");
                for (int i = 0; i < lvOutput.Items.Count; i++)
                {
                    writer.WriteLine(lvOutput.Items[i].SubItems[2].Text);
                }
            }

 
        }
    }
}
