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
        //private ListViewColumnSorter lvwColumnSorter;

        public frmSetBookOrder(frmMain f)
        {
            this._fMain = f;
            InitializeComponent();
            // Create an instance of a ListView column sorter and assign it 
            // to the ListView control.
            //lvwColumnSorter = new ListViewColumnSorter();
            //this.lvOutput.ListViewItemSorter = lvwColumnSorter;
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
                    writer.WriteLine(lvOutput.Items[i].SubItems[1].Text + "|" + lvOutput.Items[i].SubItems[2].Text);
                }
            }
            MessageBox.Show("The order of the books was saved to " + _fMain.USFM_FilesPath + @"\usfm_book_order." + _fMain.USFM_FilesExt + ".txt");
            this.Close();
 
        }

        private void cmdLoadBookOrder_Click(object sender, EventArgs e)
        {
            sFullClipboardText.Clear();

            cUtils c = new cUtils();
            c.LoadBookOrder(ref lvOutput, _fMain.USFM_FilesEncoding, _fMain.USFM_FilesPath + @"\usfm_book_order." + _fMain.USFM_FilesExt + ".txt");
        }

        private void frmSetBookOrder_Load(object sender, EventArgs e)
        {

        }

        //private void lvOutput_ColumnClick(object sender, ColumnClickEventArgs e)
        //{
        //    // Determine if clicked column is already the column that is being sorted.
        //    if (e.Column == lvwColumnSorter.SortColumn)
        //    {
        //        // Reverse the current sort direction for this column.
        //        if (lvwColumnSorter.Order == SortOrder.Ascending)
        //        {
        //            lvwColumnSorter.Order = SortOrder.Descending;
        //        }
        //        else
        //        {
        //            lvwColumnSorter.Order = SortOrder.Ascending;
        //        }
        //    }
        //    else
        //    {
        //        // Set the column number that is to be sorted; default to ascending.
        //        lvwColumnSorter.SortColumn = e.Column;
        //        lvwColumnSorter.Order = SortOrder.Ascending;
        //    }

        //    // Perform the sort with these new sort options.
        //    this.lvOutput.Sort();
        //}



    }
}
