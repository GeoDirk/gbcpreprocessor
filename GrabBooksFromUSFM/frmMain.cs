/*
Copyright 2009 Mission Aviation Fellowship
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace GBC_USFM_Preprocessor
{
    public partial class frmMain : Form
    {
        ArrayList sFullClipboardText = new ArrayList();
        List<cTagCount> oTags = new List<cTagCount>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //add in version number
            lblVersion.Text = "Matches GBC Version: " 
                + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString()
                + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString()
                + "." + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Build.ToString()
                + "     GBC_USFM_Preprocessor Revision: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
            
            //add in the encodings into the dropdown
            EncodingInfo[] codes = Encoding.GetEncodings();

            for (int i = 0; i < codes.GetUpperBound(0); i++)
            {
                cboEncoding.Items.Add(codes[i].CodePage + " - " + codes[i].DisplayName);
            }

            //get the last settings from the registry
            cboExt.Text = cRegistry.GetStringRegistryValue("extension", "ptx");
            txtDir.Text = cRegistry.GetStringRegistryValue("directory", "");
            txtBQBibleNameFull.Text = cRegistry.GetStringRegistryValue("BQBibleNameFull", "");
            txtBQBibleNameShort.Text = cRegistry.GetStringRegistryValue("BQBibleNameShort", "");
            txtBQCopyright.Text = cRegistry.GetStringRegistryValue("BQCopyright", "");
            txtBQAlphabet.Text = cRegistry.GetStringRegistryValue("BQAlphabet", "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz");
            txtBQChapterSign.Text = cRegistry.GetStringRegistryValue("BQChapterSign", "<h4>");
            txtBQVerseSign.Text = cRegistry.GetStringRegistryValue("BQVerseSign", "<sup>");
            txtBQ_BooknameTag.Text = cRegistry.GetStringRegistryValue("BQ_BooknameTag", @"\h");

            if (cRegistry.GetStringRegistryValue("fullcomments", "false") == "false")
            {
                radioDropComments.Checked = true;
            }
            //folderBrowserDialog1.SelectedPath = txtDir.Text;


            //reset the progress bar to zero
            progressBar1.Value = 0;

            //add in the BibleQuote Export dropdown information
            InitializeCodePageLists();

            //add in the font information
            cboBQFontCharSet.Items.Add("0 -- ANSI_CHARSET  (ANSI)");
            cboBQFontCharSet.Items.Add("1 -- DEFAULT_CHARSET  (Default font)");
            cboBQFontCharSet.Items.Add("2 -- SYMBOL_CHARSET  (Standard)");
            cboBQFontCharSet.Items.Add("77 -- MAC_CHARSET  (Macintosh)");
            cboBQFontCharSet.Items.Add("128 -- SHIFTJIS_CHARSET  (Japanese)");
            cboBQFontCharSet.Items.Add("129 -- HANGEUL_CHARSET  (Korean)");
            cboBQFontCharSet.Items.Add("130 -- JOHAB_CHARSET  (Korean)");
            cboBQFontCharSet.Items.Add("134 -- GB2312_CHARSET  (Simplified)");
            cboBQFontCharSet.Items.Add("136 -- CHINESEBIG5_CHARSET  (Traditional)");
            cboBQFontCharSet.Items.Add("161 -- GREEK_CHARSET  (Greek)");
            cboBQFontCharSet.Items.Add("162 -- TURKISH_CHARSET  (Turkish)");
            cboBQFontCharSet.Items.Add("163 -- VIETNAMESE_CHARSET  (Vietnamese)");
            cboBQFontCharSet.Items.Add("177 -- HEBREW_CHARSET  (Hebrew)");
            cboBQFontCharSet.Items.Add("178 -- ARABIC_CHARSET  (Arabic)");
            cboBQFontCharSet.Items.Add("186 -- BALTIC_CHARSET  (Baltic)");
            cboBQFontCharSet.Items.Add("204 -- RUSSIAN_CHARSET  (Cyrillic)");
            cboBQFontCharSet.Items.Add("222 -- THAI_CHARSET  (Thai)");
            cboBQFontCharSet.Items.Add("238 -- EASTEUROPE_CHARSET  (Includes)");
            cboBQFontCharSet.Items.Add("255 -- OEM_CHARSET  (Depends)");
        }


        /// <summary>
        /// Create the entries for the codepage dropdown
        /// </summary>
        private void InitializeCodePageLists()
        {
            cboBQCodePage.DataSource = cConvertCodePages.ConstructCodePageArray();
            cboBQCodePage.DisplayMember = "DisplayName";
            cboBQCodePage.ValueMember = "CodePageNum";
        }


        /// <summary>
        /// Browse for a directory
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtDir.Text = folderBrowserDialog1.SelectedPath;
            }

            //get the list of file extensions from that directory
            ArrayList sExt = cUtils.GetFileExtensionList(folderBrowserDialog1.SelectedPath);
            cboExt.Items.Clear();
            for (int i = 0; i < sExt.Count; i++)
            {
                string sExt2 = sExt[i].ToString();
                cboExt.Items.Add(sExt2.Substring(1));
            }
            if (cboExt.Items.Count > 0)
	        {
                cboExt.Text = cboExt.Items[0].ToString();        		 
	        }
        }


        /// <summary>
        /// Grab the booknames from the USFM files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdProcess_Click(object sender, EventArgs e)
        {
            string[] filePaths = System.IO.Directory.GetFiles(txtDir.Text, "*." + cboExt.Text);
            lvOutput.Items.Clear();
            sFullClipboardText.Clear();
            sFullClipboardText.Add("//Source Directory Path: " + txtDir.Text + @"\*." + cboExt.Text + "\n");

            foreach (string item in filePaths)
            {

                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);

                //Set Codepage
                StreamReader sr;
                if (cboEncoding.Text == "")
                {
                    sr = new StreamReader(file, Encoding.UTF8, false);
                }
                else
                {
                    string sEncoding = cboEncoding.Text;
                    sEncoding = sEncoding.Substring(0, sEncoding.IndexOf(" -")).Trim();
                    sr = new StreamReader(file, Encoding.GetEncoding(Convert.ToInt16(sEncoding)), false);
                }
                // Create a new stream to read from a file
                // Read contents of file into a string
                string line = "";
                do
                {
                    try
                    {
                        //read in a line
                        line = sr.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        //problem with read - set line to null
                        Console.WriteLine(ex.Message);
                        line = null;
                    }
                    if (line != null)
                    {
                        if (line.StartsWith(txtBookNameTag.Text + " "))
                        {
                            FileInfo fi = new FileInfo(item.ToString());

                            //add to string
                            sFullClipboardText.Add("//filename:" + fi.Name + "\n");
                            sFullClipboardText.Add("Book: " + line.Substring(txtBookNameTag.Text.Length).Trim() + "\n");
                            //add to listview
                            ListViewItem li = new ListViewItem();
                            li.Text = line.Substring(3);
                            li.SubItems.Add(file.Name.Substring(file.Name.LastIndexOf("\\")+1));
                            lvOutput.Items.Add(li);
                            break;
                        }
                    }
                } while (line != null);
                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();
            }

            //take out comments
            ProcessText();
            MessageBox.Show("Completed");
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save the settings to the registry
            cRegistry.SetStringRegistryValue("extension", cboExt.Text);
            cRegistry.SetStringRegistryValue("directory", txtDir.Text);
            if (radioKeepComments.Checked)
            {
                cRegistry.SetStringRegistryValue("fullcomments", "true");
            }
            else
            {
                cRegistry.SetStringRegistryValue("fullcomments", "false");
            }

            //BibleQuote Export Tab Settings
            cRegistry.SetStringRegistryValue("BQBibleNameFull", txtBQBibleNameFull.Text);
            cRegistry.SetStringRegistryValue("BQBibleNameShort", txtBQBibleNameShort.Text);
            cRegistry.SetStringRegistryValue("BQCopyright", txtBQCopyright.Text);
            cRegistry.SetStringRegistryValue("BQAlphabet", txtBQAlphabet.Text);
            cRegistry.SetStringRegistryValue("BQChapterSign", txtBQChapterSign.Text);
            cRegistry.SetStringRegistryValue("BQVerseSign", txtBQVerseSign.Text);
            cRegistry.SetStringRegistryValue("BQ_BooknameTag", txtBQ_BooknameTag.Text);
        }

        /// <summary>
        /// removes comments from the output and replaces the text in the
        /// output with the parsed text
        /// </summary>
        private void ProcessText()
        {
            string s = "";
            bool bKeepComments = false;

            txtOutput.Clear();
            if (radioKeepComments.Checked == true)
            {
                bKeepComments = true;
            }

            foreach (string sLine in sFullClipboardText)
            {
                if (!sLine.StartsWith("//"))
                {
                    s += sLine;
                }
                else if (bKeepComments)
                {
                    s += sLine;
                }
            }

            txtOutput.Text = s;
        }

        /// <summary>
        /// Convert the USFM files from their codepage to UTF8
        /// file format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConvertToUTF_Click(object sender, EventArgs e)
        {
            //make sure that the file extensions are not the same
            if (txtOutFileExtension.Text == cboExt.Text)
            {
                MessageBox.Show("Specify a different file extension");
                return;
            }
            
            this.Cursor = Cursors.WaitCursor;

            //get the file encoding to convert to
            Encoding encoding;
            if (cboEncoding.Text == "")
            {
                encoding = Encoding.UTF8;
            }
            else
            {
                string sEncoding = cboEncoding.Text;
                sEncoding = sEncoding.Substring(0, sEncoding.IndexOf(" -")).Trim();
                encoding = Encoding.GetEncoding(Convert.ToInt16(sEncoding));
            }

            //iterate through the files in the directory
            string[] filePaths = System.IO.Directory.GetFiles(txtDir.Text, "*." + cboExt.Text);
            foreach (string item in filePaths)
            {
                //pull out the extension information so we can rename
                //the output file differently
                FileInfo fi = new FileInfo(item);
                string sExt = fi.Extension;
                string sFileOut = fi.DirectoryName + @"\" + fi.Name.Substring(0,fi.Name.Length - fi.Extension.Length) + "." + txtOutFileExtension.Text;

                //convert from ANSI to UTF8 format
                cConvertCodePages.ConvertFileToUTF8File(item, sFileOut, encoding);
            }
            this.Cursor = Cursors.Default;

            MessageBox.Show("Conversion Completed");
        }

        private void radioKeepComments_CheckedChanged(object sender, EventArgs e)
        {
            ProcessText();
        }

        private void radioDropComments_CheckedChanged(object sender, EventArgs e)
        {
            ProcessText();
        }

        /// <summary>
        /// Look through the USFM files for non-standard USFM codes that need processing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdScanTags_Click(object sender, EventArgs e)
        {
            ArrayList sBadFileNames = new ArrayList();
            bool bProblem = false;
            this.Cursor = Cursors.WaitCursor;
            gridExtraTags.Columns.Clear();
            oTags.Clear();

            //get all the standard USFM tags that we don't process
            ArrayList sNormalTags = GetNormalTags();

            //get all the USFM files to process
            string[] filePaths = System.IO.Directory.GetFiles(txtDir.Text, "*." + cboExt.Text);

            //iterate through each file
            foreach (string item in filePaths)
            {
                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);
                FileInfo fi = new FileInfo(file.Name);

                //Set Codepage
                StreamReader sr = new StreamReader(file, Encoding.UTF8, false);
                StringBuilder sb = new StringBuilder();

                //split the text at the chapter tag as we don't need the header information
                bProblem = false;
                string line = "";
                try
                {
                    // Create a new stream to read from a file
                    // Read contents of file into a string
                    line = sr.ReadToEnd();
                    line = line.Substring(line.IndexOf("\\c "));
                }
                catch (Exception)
                {
                    sBadFileNames.Add(fi.Name);
                    bProblem = true;
                }

                if (!bProblem)
                {

                    try
                    {
                        //Regex regexObj = new Regex(@"(?:\\.[\w]?[^\d\s]?)");
                        Regex regexObj = new Regex(@"(\\.?\w+\*?)");
                        Match matchResults = regexObj.Match(line);
                        while (matchResults.Success)
                        {
                            // matched text: matchResults.Value
                            // match start: matchResults.Index
                            // match length: matchResults.Length
                            //matchResults = matchResults.NextMatch();
                            string sMatch = matchResults.Value;
                            ////remove anything to the right of the * char
                            //if (sMatch.IndexOf("*") > -1)
                            //{
                            //    sMatch = sMatch.Substring(0,sMatch.IndexOf("*"));
                            //}

                            //remove numbers or * characters
                            sMatch = sMatch.Replace("*", "");
                            //replace the numbers
                            for (int i = 0; i < 10; i++)
                            {
                                sMatch = sMatch.Replace(i.ToString(), "");
                            }

                            if (!CheckTagType(sNormalTags, sMatch))
                            {
                                if (sMatch != "\\r")
                                {
                                    Console.WriteLine(sMatch);
                                }
                                AddToExceptionList(ref oTags, sMatch, fi.Name);
                            }
                            matchResults = matchResults.NextMatch();
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("ERROR: " + ex.Message);
                        throw;
                    }
                }
                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();
            }

            //append the results to the datagrid
            DataTable dt = new DataTable();
            dt.Columns.Add("Tag");
            dt.Columns.Add("Times Used");

            //add in the error rows to the datatable
            for (int i = 0; i < oTags.Count; i++)
            {
                DataRow dr;
                dr = dt.NewRow();
                // Then add the new row to the collection.
                dr["Tag"] = oTags[i].sTagName;
                dr["Times Used"] = oTags[i].iTagCount.ToString();
                dt.Rows.Add(dr);
            }
            //bind rows to the datasource
            gridExtraTags.DataSource = dt;

            //create a new dropdown column and add it to the grid
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();
            col.DataPropertyName = "ProcessMethod";
            col.HeaderText = "Process Method";
            col.DropDownWidth = 300;
            col.Width = 300;
            col.MaxDropDownItems = 5;
            col.FlatStyle = FlatStyle.Flat;
            //add in the items of text
            col.Items.AddRange(
                "--IGNORE TAG--",
                "Remove Singular Tag - remove tag only",
                "Remove Singular Tag - remove tag and remainder of line",
                "Remove Double Marker Tags (Only) - leave text in between",
                "Remove Double Marker Tags (Full) - remove text in between"
                );
            //append on dropdown to grid
            gridExtraTags.Columns.Add(col);

            if (gridExtraTags.Rows.Count > 0)
            {
                btnProcessExtraTags.Enabled = true;
                lblOutputExtension.Enabled = true;
                txtOutputTagExtension.Enabled = true;
            }
            else
            {
                btnProcessExtraTags.Enabled = false;
                lblOutputExtension.Enabled = false;
                txtOutputTagExtension.Enabled = false;
            }
            this.Cursor = Cursors.Default;
            if (sBadFileNames.Count > 0)
            {
                string sTmp = "";
                for (int i = 0; i < sBadFileNames.Count; i++)
                {
                    sTmp += "\n" + sBadFileNames[i].ToString();
                }
                MessageBox.Show("Problems with the following files: \n" + sTmp + "\n\nThe file(s) do not appear to be valid USFM bible files. \n Rename their extension to something else");                
            }
            else
            {
                MessageBox.Show("Completed");
            }
        }

        /// <summary>
        /// Found a bad tag - we need to process it
        /// </summary>
        /// <param name="oTags"></param>
        /// <param name="sTagToCheck"></param>
        /// <param name="sFilename"></param>
        private void AddToExceptionList(ref List<cTagCount> oTags, string sTagToCheck, string sFilename)
        {
            bool bFound = false;
            //look to see if the tag already exists
            for (int i = 0; i < oTags.Count; i++)
            {
                if (sTagToCheck == oTags[i].sTagName)
                {
                    //increment the total tag count
                    oTags[i].iTagCount++;
                    bFound = true;
                    break;
                }
            }
            if (!bFound)
            {
                //add in new tag
                cTagCount c = new cTagCount();
                c.iTagCount = 1;
                c.sTagName = sTagToCheck;
                oTags.Add(c);
            }

            //check to see if in the filelist for this record
            bFound = false;
            for (int i = 0; i < oTags[oTags.Count -1].sFileNames.Count; i++)
            {
                if (oTags[oTags.Count -1].sFileNames[i].ToString() == sFilename)
                {
                    bFound = true;
                    break;
                }
            }
            if (!bFound)
            {
                //add in new filename to tag
                oTags[oTags.Count - 1].sFileNames.Add(sFilename);
            }
        }

        /// <summary>
        /// The list of official USFM tags
        /// </summary>
        /// <returns></returns>
        private ArrayList GetNormalTags()
        {
            //list of standard USFM markers
            string[] sMarker = { 
                "!$","//","\\add","\\b","\\bd","\\bdit","\\bk","\\c","\\ca","\\cd",
                "\\cl","\\cls","\\cp","\\dc","\\em","\\f","\\fdc","\\fe","\\fig",
                "\\fk","\\fl","\\fm","\\fp","\\fq","\\fqa","\\fr","\\ft","\\fv",
                "\\it","\\k","\\li","\\lit","\\m","\\mi","\\mt","\\ms","\\nb","\\nd","\\ndx",
                "\\no","\\ord","\\p","\\pb","\\pc","\\ph","\\pi","\\pm","\\pmc",
                "\\pmo","\\pmr","\\pn","\\pr","\\pro","\\q","\\qa","\\qac","\\qc",
                "\\qm","\\qr","\\qs","\\qt","\\s","\\sc","\\sig","\\sls","\\tl","\\v",
                "\\va","\\vp","\\w","\\wg","\\wh","\\wj","\\x","\\xdc","\\xk",
                "\\xo","\\xq","\\xt"
            };
            ArrayList s = new ArrayList();
            for (int i = 0; i < sMarker.Length; i++)
            {
                s.Add(sMarker[i]);
            }
            return s;
        }

        /// <summary>
        /// Check to see if the tag is a standard USFM tag or not
        /// </summary>
        /// <param name="sNormalTags"></param>
        /// <param name="sTagToCheck"></param>
        /// <returns></returns>
        private bool CheckTagType(ArrayList sNormalTags, string sTagToCheck)
        {
            for (int i = 0; i < sNormalTags.Count; i++)
            {
                if (sTagToCheck == sNormalTags[i].ToString())
                {
                    //standard USFM tag
                    return true;
                }
            }
            //not a USFM tag
            return false;
        }

        private void txtOutFileExtension_TextChanged(object sender, EventArgs e)
        {
            //make sure that the file extensions are not the same
            if (txtOutFileExtension.Text == cboExt.Text)
            {
                txtOutFileExtension.ForeColor = Color.Red;
            }
            else
            {
                txtOutFileExtension.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Process the extra (non-standard) USFM tags in the files using the method 
        /// specified.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnProcessExtraTags_Click(object sender, EventArgs e)
        {
            //make sure that the file extensions are not the same
            if (txtOutputTagExtension.Text == cboExt.Text)
            {
                MessageBox.Show("Specify a different file extension");
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            //iterate through each file looking for tags to fix
            //get all the USFM files to process
            string[] filePaths = System.IO.Directory.GetFiles(txtDir.Text, "*." + cboExt.Text);

            //iterate through each file
            foreach (string item in filePaths)
            {
                FileInfo fi = new FileInfo(item);

                //check to see if the file needs tag fixes
                bool bNeedsRepair = false;
                foreach (cTagCount  oT in oTags)
                {
                    for (int i = 0; i < oT.sFileNames.Count; i++)
                    {
                        if (oT.sFileNames[i].ToString() == fi.Name)
                        {
                            bNeedsRepair = true;
                            break;
                        }
                    }
                }

                if (!bNeedsRepair)
                {
                    try
                    {
                        //just to do a file copy to the new extension
                        File.Copy(item, fi.FullName.Substring(0, fi.FullName.LastIndexOf(".")) + "." + txtOutFileExtension.Text, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
                else
                {
                    //open each book up and get the bookname (ie. Genesis)
                    FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);

                    //Set Codepage
                    StreamReader sr = new StreamReader(file, Encoding.UTF8, false);
                    StringBuilder sb = new StringBuilder();

                    // Create a new stream to read from a file
                    // Read contents of file into a string
                    string line = "";
                    try
                    {
                        line = sr.ReadToEnd();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }

                    //loop through the tags to see if they are in there
                    for (int i = 0; i < gridExtraTags.RowCount; i++)
                    {
                        string sTag = gridExtraTags.Rows[i].Cells[0].Value.ToString();
                        string sSwitch = "--IGNORE TAG--";
                        if (gridExtraTags.Rows[i].Cells[2].Value != null)
                        {
                            sSwitch = gridExtraTags.Rows[i].Cells[2].Value.ToString();
                        }

                        switch (sSwitch)
                        {
                            case "--IGNORE TAG--":
                                break;
                            case "Remove Singular Tag - remove tag only":
                                line = ProcessSingularTag(line, sTag);
                                break;
                            case "Remove Singular Tag - remove tag and remainder of line":
                                line = ProcessSingularTagFullLine(line, sTag);
                                break;
                            case "Remove Double Marker Tags (Only) - leave text in between":
                                line = ProcessDoubleTag(line, sTag);
                                break;
                            case "Remove Double Marker Tags (Full) - remove text in between":
                                line = ProcessDoubleTagFull(line, sTag);
                                break;
                            default:
                                break;
                        }

                    }

                    //write out the processed file
                    using (StreamWriter sw = new StreamWriter(fi.FullName.Substring(0, fi.FullName.LastIndexOf(".")) + "." + txtOutFileExtension.Text))
                    {
                        sw.Write(line);
                        sw.Flush();
                        sw.Close();
                    }
                    
                }
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// Function to remove a double tag (start & end tag) along with
        /// all the text between
        /// </summary>
        /// <param name="line"></param>
        /// <param name="sTag"></param>
        /// <returns></returns>
        private string ProcessDoubleTagFull(string line, string sTag)
        {
            bool bBreakOut = false;
            //look for each occurance of the tag
            while (line.IndexOf(sTag) > -1 && !bBreakOut)
            {
                int iTagStart = line.IndexOf(sTag);
                //find the next line start
                int iLineEnd = line.IndexOf(sTag + "*", iTagStart);
                if (iLineEnd > -1)
                {
                    line = line.Remove(iTagStart, iLineEnd - iTagStart + sTag.Length + 1);
                }
                else
                {
                    //cannot find end tag...break out of loop
                    bBreakOut = true;
                }
                    
            }
            return line;
        }

        /// <summary>
        /// Remove the tag and the remainder of the line behind it
        /// </summary>
        /// <param name="line"></param>
        /// <param name="sTag"></param>
        /// <returns></returns>
        private string ProcessSingularTagFullLine(string line, string sTag)
        {
            bool bBreakOut = false;
            //look for each occurance of the tag
            while (line.IndexOf(sTag) > -1 && !bBreakOut)
            {
                int iTagStart = line.IndexOf(sTag);
                //find the next line start
                int iLineEnd = line.IndexOf("\n", iTagStart);
                if (iLineEnd > -1)
                {
                    line = line.Remove(iTagStart, iLineEnd - iTagStart);
                }
                else
                {
                    bBreakOut = true;
                }
            }
            return line;
        }

        /// <summary>
        /// Just remove the tag from the row
        /// </summary>
        /// <param name="line"></param>
        /// <param name="sTag"></param>
        /// <returns></returns>
        private string ProcessSingularTag(string line, string sTag)
        {
            ////removes start tag with number and trailing space
            Regex reg = new Regex(@"\" + sTag + @"[0-9]\s");
            line = reg.Replace(line, "");
            //removes start tag with number
            reg = new Regex(@"\" + sTag + @"[0-9]");
            line = reg.Replace(line, "");
            return line;
        }

        /// <summary>
        /// Remove a double tag (start & end tag) but leave the text
        /// remaining between the tags
        /// </summary>
        /// <param name="line"></param>
        /// <param name="sTag"></param>
        /// <returns></returns>
        private string ProcessDoubleTag(string line, string sTag)
        {
            line = line.Replace(sTag + "*", ""); //remove end tag first
            line = line.Replace(sTag + " ", ""); //removes start tag with trailing space
            line = line.Replace(sTag, ""); //removes start tag
            return line;
        }

        private void txtOutputTagExtension_TextChanged(object sender, EventArgs e)
        {
            //make sure that the file extensions are not the same
            if (txtOutputTagExtension.Text == cboExt.Text)
            {
                txtOutputTagExtension.ForeColor = Color.Red;
            }
            else
            {
                txtOutputTagExtension.ForeColor = Color.Black;
            }
        }

        /// <summary>
        /// Show a tooltip of the files that have this tag on grid mouseover
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridExtraTags_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            string sFileNames = "";
            // Ignore any invalid RowIndex values.
            if (e.RowIndex != -1)
            {
                string sTag = gridExtraTags.Rows[e.RowIndex].Cells[0].Value.ToString();
                //find the tag
                foreach (cTagCount  oT in oTags)
                {
                    if (oT.sTagName == sTag)
                    {
                        for (int i = 0; i < oT.sFileNames.Count; i++)
                        {
                            sFileNames += oT.sFileNames[i].ToString() + "\r\n";
                        }
                    }
                }
                lblDisplayTag.Text = "Tag: " + sTag;
                txtDisplayTag.Text = sFileNames;
            }
        }

        /// <summary>
        /// Function to find all the words greater than the specified length
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdFindAllLongWords_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            bool bProblem = false;
            List<cLongWords> c = new List<cLongWords>();

            //get all the USFM files to process
            string[] filePaths = System.IO.Directory.GetFiles(txtDir.Text, "*." + cboExt.Text);

            //iterate through each file
            foreach (string item in filePaths)
            {
                bProblem = false;
                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);
                FileInfo fi = new FileInfo(file.Name);
                string sFilename = fi.Name;

                //Set Codepage
                StreamReader sr = new StreamReader(file, Encoding.UTF8, false);

                // Create a new stream to read from a file
                // Read contents of file into a string
                string line = "";
                try
                {
                    //read in the entire file
                    line = sr.ReadToEnd();
                    //split the text at the chapter tag as we don't need the header information
                    line = line.Substring(line.IndexOf("\\c "));
                }
                catch (Exception)
                {
                    bProblem = true;
                }

                if (!bProblem)
                {
                    //regex the string length using the method specified by the user
                    try
                    {
                        string sPattern = "";
                        if (rbWordsOnly.Checked)
                        {
                            sPattern = @"\w{" + updownLongWords.Value.ToString() + ",}";
                        }
                        else if (rbWordsAndHyphens.Checked)
                        {
                            sPattern = @"\w*-\w{" + updownLongWords.Value.ToString() + ",}";
                        }
                        else
                        {
                            sPattern = @"\S{" + updownLongWords.Value.ToString() + ",}";
                        }
                        Regex regexObj = new Regex(sPattern);
                        Match matchResults = regexObj.Match(line);
                        while (matchResults.Success)
                        {
                            //check to see if this word has already been found
                            string sTmp = matchResults.Value;
                            bool bFound = false;
                            foreach (cLongWords lw in c)
                            {
                                if (sTmp == lw.sWord && lw.sFileName == sFilename)
                                {
                                    //word already there for this file...increment hits
                                    lw.iTotalHits++;
                                    bFound = true;
                                    break;
                                }
                            }
                            if (!bFound)
                            {
                                //add in a new word
                                cLongWords l = new cLongWords();
                                l.sWord = sTmp;
                                l.sFileName = sFilename;
                                l.iTotalHits = 1;
                                c.Add(l);
                            }

                            matchResults = matchResults.NextMatch();
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine("ERROR: " + ex.Message);
                        throw;
                    }


                }
                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();
            }

            //parse out data for the clipboard
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Words Greater than " + updownLongWords.Value.ToString() + " characters");
            sb.AppendLine("Filename\tWord\tTotal Hits\tWord Length");
            foreach (cLongWords lw in c)
            {
                sb.AppendLine(lw.sFileName + "\t" + lw.sWord + "\t" + lw.iTotalHits.ToString() + "\t" + lw.sWord.Length.ToString());                
            }

            //dump to the clipboard
            this.Cursor = Cursors.Default;            
            Clipboard.Clear();
            try
            {
                Clipboard.SetText(sb.ToString());
                MessageBox.Show("Results placed in clipboard");
            }
            catch (Exception)
            {
                MessageBox.Show("Error copying results to clipboard","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Start the process of exporting the files into BibleQuote format
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGenerateBQExport_Click(object sender, EventArgs e)
        {
            //check to see if all the fields have been filled in
            if (BQExportValidation() == true)
            {
                MessageBox.Show("Please fill in required fields");
                return;
            }

            //give the user a chance to abort.
            if (MessageBox.Show("Use of this function will overwrite all .htm files in your selected directory", "Continue?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            //change the screen cursor
            this.Cursor = Cursors.WaitCursor;

            bool bProblem = false;
            bool bChapterZero = false;
            //process each file
            //get all the USFM files to process
            string[] filePaths = System.IO.Directory.GetFiles(txtDir.Text, "*." + cboExt.Text);
            List<cBQ_IniStructure> oIniFile = new List<cBQ_IniStructure>();

            //calculate the number of files we are going to parse so we can set
            //the progress bar max
            progressBar1.Value = 0;
            progressBar1.Maximum = filePaths.Length;

            //iterate through each file
            foreach (string item in filePaths)
            {
                bProblem = false;
                //create a book object to hold the chapters / verses
                cBQ_Book oBook = new cBQ_Book();

                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);
                FileInfo fi = new FileInfo(file.Name);
                string sFilename = fi.Name;
                string sFileOutName = fi.FullName.Substring(0, fi.FullName.LastIndexOf(".")) + ".htm";

                //Set Codepage
                StreamReader sr;
                sr = new StreamReader(file, Encoding.UTF8, false);

                // Create a new stream to read from a file
                // Read contents of file into a string
                string line = "";
                line = sr.ReadToEnd();
                
                //split the text at the chapter tag as we don't need the header information
                try
                {
                    //split the file based upon the chapter tags
                    string[] sChapters = Regex.Split(line, @"\\c ", RegexOptions.Multiline);
                    for (int i = 0; i < sChapters.Length; i++)
                    {
                        string sSection = sChapters[i];
                        if (i == 0)
                        {
                            //prechapter text that could be in the introduction

                            //get the bible header
                            int iStart = sSection.IndexOf(txtBQ_BooknameTag.Text);
                            int iEnd;
                            if (iStart > 0)
                            {
                                iStart += 3; //add in for the \h tag and space themselves
                                iEnd = sSection.IndexOf("\r\n", iStart);
                                if (iEnd > iStart)
	                            {
                                    oBook.sBookName = sSection.Substring(iStart, iEnd - iStart);
	                            }
                            }

                            //look for any intro paragraphs
                            if (sSection.IndexOf("\\ip ") > 0)
                            {
                                //create a Chapter 0 chapter
                                cBQ_Chapter oChap = new cBQ_Chapter(0);
                                //grab all the \ip lines of text
                                try
                                {
                                    Regex regexObj = new Regex(@"^.*(\\ip ).*$\r?\n?", RegexOptions.Multiline);
                                    Match matchResults = regexObj.Match(sSection);
                                    while (matchResults.Success)
                                    {
                                        oChap.AddVerse(ParseHeaderTags(matchResults.Value));
                                        oChap.AddVerse("<br/>");
                                        oChap.AddVerse("<br/>");
                                        matchResults = matchResults.NextMatch();
                                    }
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine("ERROR: " + ex.Message);
                                    throw;
                                }
                                try
                                {
                                    //check for toc of each bible chapter //io1 and nested in it //io2
                                    //todo replace <ol> and <li> in the code below with actual numbering
                                    //Regex regexObj = new Regex(@"^.*(\\io1 ).*$\r?\n?", RegexOptions.Multiline);
                                    Regex regexObj = new Regex(@"^.*((\\io1 ).*$\r?\n?)|((\\io2 ).*$\r?\n?)", RegexOptions.Multiline);
                                    Match matchResults = regexObj.Match(sSection);
                                    oChap.AddVerse("<ol>");
                                    bool needUL = true;
                                    while (matchResults.Success)
                                    {

                                        if (matchResults.Value.Substring(0, 4) == @"\io1")
                                        {

                                            oChap.AddVerse(Regex.Replace(matchResults.Value, @"\\io1 ", "<li>", RegexOptions.None));
                                            matchResults = matchResults.NextMatch();
                                            needUL = true;
                                        }
                                        else
                                        {
                                            if (needUL)
                                            {
                                                oChap.AddVerse("<ol>");
                                            }

                                            oChap.AddVerse(Regex.Replace(matchResults.Value, @"\\io2 ", "<li>", RegexOptions.None));
                                            matchResults = matchResults.NextMatch();
                                            if (matchResults.Length > 4)
                                            {
                                                if (matchResults.Value.Substring(0, 4) == @"\io1")
                                                {
                                                    oChap.AddVerse("</ol>");
                                                    needUL = true;
                                                }
                                                else
                                                {
                                                    needUL = false;
                                                }
                                            }
                                            else
                                            {
                                                oChap.AddVerse("</ol>");
                                            }

                                        }

                                    }
                                    oChap.AddVerse("</ol>");
                                }
                                catch (ArgumentException ex)
                                {
                                    Console.WriteLine("ERROR: " + ex.Message);
                                    throw;
                                }
                                
                                //if anything was added then add this to the overall book
                                if (oChap.VerseCount > 0)
                                {
                                    bChapterZero = true;
                                    oBook.AddChapter(oChap);
                                }
                            }
                            
                        }
                        else
                        {
                            //normal bible chapters
                            cBQ_Chapter oChap = new cBQ_Chapter(i);
                            //grab all the \ip lines of text
                            try
                            {
                                //todo add in the \s tag processing
                                //Regex regexObj = new Regex(@"^.*((\\v ).*$\r?\n?)|((\\s).*$\r?\n?)|((\\q1).*$\r?\n?)|((\\q2).*$\r?\n?)", RegexOptions.Multiline);
                                Regex regexObj = new Regex(@"^.*((\\[a-z][0-9]).*$\r?\n?)|((\\[a-z]).*$\r?\n?)", RegexOptions.Multiline);
                                Match matchResults = regexObj.Match(sSection);
                                while (matchResults.Success)
                                {
                                    //if it's a heading
                                    if (matchResults.Value.Substring(0,2) == @"\s")
                                    {
                                    
                                        //if it's an \s tag
                                        string sTemp = matchResults.Value;
                                        sTemp = sTemp.Replace(@"\s", "").Trim();
                                        //put in extra breaks
                                        oChap.AddVerse("<br/>");
                                        oChap.AddVerse("! <br/><b>" + sTemp + "</b>");
                                        oChap.AddVerse("<br/>");

                                    }
                                    else if (matchResults.Value.Substring(0,3) == @"\ms")
                                    {
                                        //poetry \ms tags
                                        string sTemp = matchResults.Value;
                                        sTemp = sTemp.Replace(@"\ms", "").Trim();
                                        //put in extra breaks
                                        oChap.AddVerse("<br/>");
                                        oChap.AddVerse("! <br/><b>" + sTemp + "</b>");
                                        oChap.AddVerse("<br/>");

                                    }
                                    
                                    //if it's a regular verse
                                    else if (matchResults.Value.Substring(0,2) == @"\v")
                                    {
                                        string s = ParseVerseTags(matchResults.Value);
                                        if (s != String.Empty)
                                        {
                                            oChap.AddVerse(s);
                                        }
                                        
                                    }
                                    else
                                    {
                                        //if it's everything else
                                        string s = ParseVerseTags(matchResults.Value);
                                        if (s != String.Empty)
                                        {
                                            oChap.AddVerse("<br/>");
                                            oChap.AddVerse(s);
                                        }
                                        else
                                        {
                                            oChap.AddVerse("<br/>");
                                        }
                                        
                                    }
                                    

                                    matchResults = matchResults.NextMatch();
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("ERROR: " + ex.Message);
                                throw;
                            }
                            //add in the chapter to the book object
                            oBook.AddChapter(oChap);
                        }
                    }

                }
                catch (Exception ex)
                {
                    bProblem = true;
                    Console.WriteLine("ERROR: " + ex.Message);
                    throw;
                }

                if (!bProblem)
                {

                }

                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();

                //dump out the book to a file
                ExportBQFile(sFileOutName, oBook);

                //create a new ini entry
                cBQ_IniStructure oIni = new cBQ_IniStructure();
                oIni.PathName = fi.Name.Substring(0,fi.Name.LastIndexOf(".")) + ".htm";
                oIni.FullName = oBook.sBookName;
                oIni.ChapterQty = oBook.GetNumberOfChapters;

                //add into the ini collection
                oIniFile.Add(oIni);

                //iterate the progress bar
                progressBar1.Value++;
                this.Refresh();
            }
            //dump out the setup.ini file to a computer
            ExportBQiniFile(oIniFile, bChapterZero);
            
            //reset the cursor
            this.Cursor = Cursors.Default;
            MessageBox.Show("Completed Conversion");
        }

        private string ParseFootnote(string sFtnt)
        {
            sFtnt = cUSFM_Utilities.ProcessOtherTags(sFtnt);
            return sFtnt;
        }

        /// <summary>
        /// Export the Bible Quotes ini file
        /// </summary>
        /// <param name="oIniFile"></param>
        private void ExportBQiniFile(List<cBQ_IniStructure> oIniFile, bool bChapZero)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CodePage = " + cboBQCodePage.SelectedValue.ToString());
            if (rbL2R.Checked == true)
            {
                sb.AppendLine("BiDiMode = LR");
            }
            else
            {
                sb.AppendLine("BiDiMode = RL");
            }
            sb.AppendLine("DesiredFontCharset = " + cboBQFontCharSet.Text.Substring(0, cboBQFontCharSet.Text.IndexOf(" ")));
            sb.AppendLine("BibleName = " + txtBQBibleNameFull.Text);
            sb.AppendLine("BibleShortName = " + txtBQBibleNameShort.Text);
            if (rbBQIsBible_Y.Checked)
            {
                sb.AppendLine("Bible = Y");
            }
            else
            {
                sb.AppendLine("Bible = N");
            }
            if (rbBQ_OT_Y.Checked)
            {
                sb.AppendLine("OldTestament = Y");
            }
            else
            {
                sb.AppendLine("OldTestament = N");
            }
            if (rbBQ_NT_Y.Checked)
            {
                sb.AppendLine("NewTestament = Y");
            }
            else
            {
                sb.AppendLine("NewTestament = N");
            }
            if (rbBQApocrypha_Y.Checked)
            {
                sb.AppendLine("Apocrypha = Y");
            }
            else
            {
                sb.AppendLine("Apocrypha = N");
            }
            sb.AppendLine("StrongNumbers = N");
            sb.AppendLine("Greek = N");
            sb.AppendLine("Alphabet = " + txtBQAlphabet.Text);
            sb.AppendLine("ChapterSign = " + txtBQChapterSign.Text);
            sb.AppendLine("VerseSign = " + txtBQVerseSign.Text);
            if (bChapZero)
            {
                sb.AppendLine("ChapterZero = Y");
            }
            else
            {
                sb.AppendLine("ChapterZero = N");
            }
            sb.AppendLine("BookQty = " + oIniFile.Count.ToString());
            sb.AppendLine("NoForcedLineBreaks = Y");
            sb.AppendLine("");

            foreach (cBQ_IniStructure oIni in oIniFile)
            {
                sb.AppendLine("PathName = " + oIni.PathName);
                sb.AppendLine("FullName = " + oIni.FullName);
                sb.AppendLine("ShortName = " + oIni.ShortName);
                sb.AppendLine("ChapterQty = " + oIni.ChapterQty.ToString());
                sb.AppendLine("");
            }

            //write out the ini to a UTF8 file
            using (StreamWriter sw = new StreamWriter(txtDir.Text + @"\setup.ini", false, Encoding.UTF8))
            {
                sw.Write(sb);
                sw.Flush();
                sw.Close();
            }
        }

        /// <summary>
        /// Export the BibleQuote htm file
        /// </summary>
        /// <param name="sFileOutName"></param>
        /// <param name="oBook"></param>
        private void ExportBQFile(string sFileOutName, cBQ_Book oBook)
        {
            StringBuilder sb = new StringBuilder();
            List<cBQ_Chapter> oChapters = oBook.oChapters;
            sb.AppendLine("<HTML>");
            sb.AppendLine("<HEAD>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html\">");
            sb.AppendLine("<link href=\"..\\common.css\" type=text/css rel=stylesheet>");
            sb.AppendLine("<link href=\"directory.css\" type=text/css rel=stylesheet>");
            sb.AppendLine("<TITLE>" + oBook.sBookName + "</TITLE>");
            sb.AppendLine("</HEAD>");
            sb.AppendLine("<BODY>");
            sb.AppendLine("<H1>" + oBook.sBookName + "</H1>");

            string sChapTagStart = txtBQChapterSign.Text;
            string sChapTagEnd = "</" + txtBQChapterSign.Text.Substring(1);
            string sVerseStart = txtBQVerseSign.Text;
            string sVerseEnd = "</" + txtBQVerseSign.Text.Substring(1);

            //process the chapters and verses
            foreach (cBQ_Chapter c in oChapters)
            {
                int iCount = 0;
                //chapter number header
                sb.AppendLine(sChapTagStart + oBook.sBookName + " " + c.sChapterNumber + sChapTagEnd);

                //append each verse
                ArrayList oVerse = c.Verses;
                
                for (int i = 0; i < oVerse.Count; i++)
                {
                    if (c.sChapterNumber == "0")
                    {
                        //chapter information section
                        sb.AppendLine(sVerseStart + sVerseEnd + oVerse[i].ToString());
                    }
                    else
                    {
                        
                        //regular bible chapter
                        string sTmp = oVerse[i].ToString().Trim();
                        //pull out the verse number from the front of the verse
                        if (sTmp != String.Empty)
                        {
                            if (sTmp.Substring(0, 1) == "!")
                            {
                                //this is a section header \s
                                sb.AppendLine(sTmp.Substring(2));
                                iCount = 0;
                            }
                            else if (sTmp == "<br/>")
                            {
                                //keep track of the <br/> tags, and don't let there be more than 2
                                iCount++;
                                if (iCount<3)
                                {
                                    sb.AppendLine("<br/>");
                                }
                                
                            }
                            else
                            {
                                //when it's a normal verse
                                try
                                {
                                    //will error out if missing any verse text, if there is just a tag without text following it
                                    string sVerseNum = sTmp.Substring(0, sTmp.IndexOf(" "));
                                    sb.AppendLine(sVerseStart + sVerseNum + sVerseEnd + sTmp.Substring(sVerseNum.Length, sTmp.Length - sVerseNum.Length));
                                    iCount = 0;
                                }
                                catch (Exception ex)
                                {
                                    //sb.AppendLine("<br/>");
                                    Console.WriteLine("ERROR: " + ex.Message);
                                    //throw;
                                }
                            }
                        }
                        
                        
                    }
                }
                ////add a couple of empty lines
                //sb.AppendLine("<sup></sup>");
                //sb.AppendLine("<sup></sup>");
                //insert footnotes at the end of each chapter
                ArrayList oFtnote = c.Footnotes;
                for (int i = 0; i < oFtnote.Count; i++)
                {
                    
                    string sFTmp = oFtnote[i].ToString().Trim();
                    //pull out the verse number from the front of the verse
                    if (sFTmp != String.Empty)
                    {

                        try
                        {
                            //will error out if missing any verse text, if there is just a tag without text following it
                            string sVerseNum = sFTmp.Substring(9);
                            sVerseNum = sVerseNum.Substring(0,sVerseNum.IndexOf(@"\"));
                            sb.AppendLine(sVerseStart + sVerseEnd + "<b>" + sVerseNum + "</b>" + sFTmp.Substring(sFTmp.IndexOf("*") + 1) + "<br/>");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine("");
                            Console.WriteLine("ERROR: " + ex.Message);
                            //throw;
                        }

                    }
                }

            }

            //closing tags
            sb.AppendLine("</BODY>");
            sb.AppendLine("</HTML>");

            //write out the BQ page to a UTF8 temp file
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + @"\tmp.txt", false, Encoding.UTF8))
            {
                sw.Write(sb);
                sw.Flush();
                sw.Close();
            }
            //convert file to correct codepage
            cConvertCodePages.ConvertFileToCodePageFile(Application.StartupPath + @"\tmp.txt", sFileOutName, 65001, (int)cboBQCodePage.SelectedValue);

            /*
             * This code doesn't work
             */
            //write out the BQ file in the new codepage format
            //cConvertCodePages.ConvertStringToCodePageFile(sb.ToString(), sFileOutName, 65001, (int)cboBQCodePage.SelectedValue);
            //string sTemp = cConvertCodePages.ConvertStringToCodePageString(sb.ToString(), 65001, (int)cboBQCodePage.SelectedValue);
        }


        /// <summary>
        /// Parse out the normal verse tags
        /// </summary>
        /// <param name="sVerse"></param>
        /// <returns></returns>
        private string ParseVerseTags(string sVerse)
        {
            sVerse = cUSFM_Utilities.RemoveDoubleMarkerTags(sVerse);
            sVerse = cUSFM_Utilities.RemoveDoubleMarkerTagsFull(sVerse);
            sVerse = cUSFM_Utilities.RemoveSingularMarkerTags(sVerse);
            sVerse = cUSFM_Utilities.RemoveVerseNumbering(sVerse);
            sVerse = cUSFM_Utilities.ProcessOtherTags(sVerse);
            return sVerse;
        }

        /// <summary>
        /// parse the ip tags out of the verses
        /// </summary>
        /// <param name="sVerse"></param>
        /// <returns></returns>
        private string ParseHeaderTags(string sVerse)
        {
            try
            {
                sVerse = Regex.Replace(sVerse, @"\\ip ", "", RegexOptions.None);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                throw;
            }
            
            return sVerse;
        }

        /// <summary>
        /// Check data entry fields to make sure that the required ones
        /// are filled in fully
        /// </summary>
        /// <returns>true if everything validates</returns>
        private bool BQExportValidation()
        {
            bool bRet = false;
            errorProvider1.Clear();
            if (txtBQBibleNameFull.Text == string.Empty)
            {
                errorProvider1.SetError(txtBQBibleNameFull, "Need value");
                bRet = true;
            }
            if (txtBQBibleNameShort.Text == string.Empty)
            {
                errorProvider1.SetError(txtBQBibleNameShort, "Need value");
                bRet = true;
            }
            if (txtBQCopyright.Text == string.Empty)
            {
                errorProvider1.SetError(txtBQCopyright, "Need value");
                bRet = true;
            }
            if (txtBQAlphabet.Text == string.Empty)
            {
                errorProvider1.SetError(txtBQAlphabet, "Need value");
                bRet = true;
            }
            if (txtBQChapterSign.Text == string.Empty)
            {
                errorProvider1.SetError(txtBQChapterSign, "Need value");
                bRet = true;
            }
            if (txtBQVerseSign.Text == string.Empty)
            {
                errorProvider1.SetError(txtBQVerseSign, "Need value");
                bRet = true;
            }
            if (cboBQCodePage.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboBQCodePage, "Need value");
                bRet = true;
            }
            if (cboBQFontCharSet.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboBQFontCharSet, "Need value");
                bRet = true;
            }

            return bRet;
        }

        /// <summary>
        /// Copy the bible book names to the clipboard in the format
        /// the user has specified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCopyToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            if (txtOutput.Text != string.Empty)
            {
                Clipboard.SetText(txtOutput.Text);
            }
        }

        private void cmdVersificationFind_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            bool bProblem = false;
            List<cVersification> oVersificationList = new List<cVersification>();

            //get all the USFM files to process
            string[] filePaths = System.IO.Directory.GetFiles(txtDir.Text, "*." + cboExt.Text);

            //iterate through each file
            foreach (string item in filePaths)
            {
                bProblem = false;
                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);
                FileInfo fi = new FileInfo(file.Name);
                string sFilename = fi.Name;

                //Set Codepage
                StreamReader sr = new StreamReader(file, Encoding.UTF8, false);

                // Create a new stream to read from a file
                // Read contents of file into a string
                string line = "";
                try
                {
                    //read in the entire file
                    line = sr.ReadToEnd();
                    //split the text at the chapter tag as we don't need the header information
                    line = line.Substring(line.IndexOf("\\c "));
                }
                catch (Exception)
                {
                    bProblem = true;
                }

                if (!bProblem)
                {
                    //bust the book up into chapters
                    string[] sSplitChar = new string[]{"\\c"};
                    string[] sChapters = line.Split(sSplitChar, StringSplitOptions.RemoveEmptyEntries);

                    //iterate through each chapter
                    for (int i = 0; i < sChapters.Length; i++)
                    {
                        string[] sSplitVerse = new string[] { "\\v" };
                        string[] sVerses = sChapters[i].Split(sSplitVerse, StringSplitOptions.RemoveEmptyEntries);
                        
                        //rip out the verse numbers
                        ArrayList alVerses = new ArrayList();
                        for (int j = 0; j < sVerses.Length; j++)
                        {
                            sVerses[j] = RipOutVerseNumber(sVerses[j]);
                            //remove blank verses
                            if (sVerses[j] != string.Empty)
                            {
                                alVerses.Add(sVerses[j].ToString());
                            }
                        }

                        //look for gaps
                        int iPrevVerse = 1;
                        for (int j = 0; j < alVerses.Count; j++)
                        {
                            if (Convert.ToString(iPrevVerse) != alVerses[j].ToString())
                            {
                                //found versification problem here
                                //Console.WriteLine("Versification Issue\tBook: " + sFilename + "\tChapter: " + (i + 1).ToString() + "\tVerse: " + iPrevVerse.ToString());
                                cVersification oV = new cVersification(sFilename, (i + 1).ToString(), iPrevVerse.ToString());
                                oVersificationList.Add(oV);
                            }
                            iPrevVerse = Convert.ToInt16(alVerses[j].ToString()) + 1;
                        }
                    }


                }
                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();
            }

            //parse out data for the clipboard
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Versification Issues");
            sb.AppendLine("Filename\tChapter\tVerse");
            foreach (cVersification lw in oVersificationList)
            {
                sb.AppendLine(lw.sFileName + "\t" + lw.sChapNum + "\t" + lw.sVerseNum);
            }

            //dump to the clipboard
            this.Cursor = Cursors.Default;
            Clipboard.Clear();
            try
            {
                Clipboard.SetText(sb.ToString());
                MessageBox.Show("Results placed in clipboard");
            }
            catch (Exception)
            {
                MessageBox.Show("Error copying results to clipboard", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private string RipOutVerseNumber(string p)
        {
            //rip out the verse number - will have a space after the number
            p = p.TrimStart();

            //look for a normal number followed by a space then the verse text
            if (p.IndexOf(" " ) == -1)
            {
                return "";
            }
            string sTmp = p.Substring(0, p.IndexOf(" "));
            double result;
            if (double.TryParse(sTmp, out result))
            {
                //valid number
                return sTmp;
            }
            else
            {
                //not a valid verse number
                return "";
            }
        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

    }
}
