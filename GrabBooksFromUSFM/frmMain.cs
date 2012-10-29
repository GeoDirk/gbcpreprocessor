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
using SharpEpub;
using System.Globalization;

namespace GBC_USFM_Preprocessor
{
    public partial class frmMain : Form
    {
        ArrayList sFullClipboardText = new ArrayList();
        List<cTagCount> oTags = new List<cTagCount>();
        string _sDigiStudyPath = "";
        frmCharReplacer _fChar = null;
        DataTable _charHTMLdatatable = null;
        bool _bDDtag = false;
        private enum ExtensionType
        {
            DirBrowser = 0,
            EnterButton = 1
        }

        public bool bDDtag
        {
            get { return _bDDtag; }
            set { _bDDtag = value; }
        }
        
        /// <summary>
        /// Used to set the reference back to the char form
        /// </summary>
        public frmCharReplacer fCharForm
        {
            get { return _fChar; }
            set { _fChar = value; }
        }
        /// <summary>
        /// used by the frmCharReplacer to update this form
        /// </summary>
        public DataTable charHTMLdatatable
        {
            get { return _charHTMLdatatable; }
            set { _charHTMLdatatable = value; }
        }

        public string USFM_FilesPath
        {
            get { return txtDir.Text; }
        }
        public string USFM_FilesExt
        {
            get { return cboExt.Text; }
        }
        public string USFM_FilesEncoding
        {
            get { return cboEncoding.Text; }
        }        

        
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
            txtReplacemetText.Text = cRegistry.GetStringRegistryValue("replacement", ". .");
            
            if (cRegistry.GetStringRegistryValue("fullcomments", "false") == "false")
            {
                radioDropComments.Checked = true;
            }
            //folderBrowserDialog1.SelectedPath = txtDir.Text;


            //reset the progress bar to zero
            progressBar1.Value = 0;
            progressBar2.Value = 0;

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

            //set the interface to the previous codepage/fontcharset
            string sPrevCodePage = cRegistry.GetStringRegistryValue("BQCodePage", "");
            string sPrevFontCharSet = cRegistry.GetStringRegistryValue("BQFontCharSet", "");

            for (int i = 0; i < cboBQCodePage.Items.Count; i++)
            {
                cConvertCodePages.CodePage oItem = (cConvertCodePages.CodePage)cboBQCodePage.Items[i];
                if (sPrevCodePage == (oItem.strName + " - " + oItem.strDisplayName))
                {
                    cboBQCodePage.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cboBQFontCharSet.Items.Count; i++)
            {
                if (sPrevFontCharSet == cboBQFontCharSet.Items[i].ToString())
                {
                    cboBQFontCharSet.SelectedIndex = i;
                    break;
                }
            }
            DataTable dtLangs = new DataTable();
            DataColumn dcLangCode = new DataColumn("DisplayName");
            DataColumn dcLangName = new DataColumn("Code");
            dtLangs.Columns.Add(dcLangCode);
            dtLangs.Columns.Add(dcLangName);
            //populate language codes for EPUB
            
            foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                dtLangs.Rows.Add(ci.EnglishName.ToString(),ci.TwoLetterISOLanguageName.ToString());
            }
            //sort datatable
            if (dtLangs.Rows.Count > 0)
            {
                //convert DataTable to DataView  
                DataView dv = dtLangs.DefaultView; 
                dv.Sort = "DisplayName";
                //save our newly ordered results back into our datatable  
                dtLangs = dv.ToTable();
            }
            DataRow dr = dtLangs.NewRow();
            dr[0] = "Select Language";
            dr[1] = "";
            dtLangs.Rows.InsertAt(dr, 0);
            cboEpubLanguage.DataSource = dtLangs;
            cboEpubLanguage.DisplayMember = "DisplayName";
            cboEpubLanguage.ValueMember = "Code";

            //find out if DigiStudy has been installed on this computer and get its path from
            //the registry
            _sDigiStudyPath = cRegistry.GetStringRegistryValue("SOFTWARE", "Mission Aviation Fellowship", "DigiStudy", "Path", "");
            if (_sDigiStudyPath != string.Empty)
            {
                lblExportTo.Text = "Exporting To: " + _sDigiStudyPath;
            }

            this.columnHeader3.Text = "Select All";
            lvBooks.Enabled = false;

            //fill out template for EPUB title page
            txtTitlePageInfo.Text = "<title>Cover Page Title here</title>"
                                + "</head>"
                                + "<body style=\"background-image: url(\'image/uzor.gif\'); background-repeat:no-repeat\">"
                                + "<div class=\"booktitle\"><h1>Bible Name here</h1></div>"
                                + "<div class=\"tpDescription\">Description here</div>"
                                + "<div class=\"tpPublisher\">Publisher's Name here</div>"
                                + "<div class=\"tpPublisher\">Year here</div>"
                                + "<div class=\"tpISBN\">ISBN here</div>"
                                + "<div class=\"tpISBN\">Optional Publishing Info here</div>"
                                + "<div class=\"tpWebsite\"><a href=\"http://www.youraddress.com\">www.youraddress.com</a></div>";
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
            else
            {
                return;
            }

            PopulateExtensionList(ExtensionType.DirBrowser);
        }

        private void PopulateExtensionList(ExtensionType e)
        {
            //get the list of file extensions from that directory
            string sPath = "";
            if (e == ExtensionType.DirBrowser)
            {
                sPath = folderBrowserDialog1.SelectedPath;
            }
            else
            {
                sPath = txtDir.Text;
            }
            ArrayList sExt = cUtils.GetFileExtensionList(sPath);
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

            sFullClipboardText.Clear();
            sFullClipboardText.Add("//Source Directory Path: " + txtDir.Text + @"\*." + cboExt.Text + "\n");

            cUtils c = new cUtils();
            c.ExtractBookNames(filePaths, ref lvOutput, cboEncoding.Text, txtBookNameTag.Text, sFullClipboardText);

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
            cRegistry.SetStringRegistryValue("BQCodePage", cboBQCodePage.Text);
            cRegistry.SetStringRegistryValue("BQFontCharSet", cboBQFontCharSet.Text);
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
        private void cmdConvertToUTF_WithoutBOM_Click(object sender, EventArgs e)
        {
            if (txtDir.Text != "")
            {
                folderBrowserDialog1.SelectedPath = txtDir.Text;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.Directory.Exists(folderBrowserDialog1.SelectedPath))
                {
                    ConvertFileToUTF(ckAddBOM.Checked, folderBrowserDialog1.SelectedPath);
                }

            }
            
        }

        private void ConvertFileToUTF(bool bBOM, string sOutputFolder)
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
                string sFileOut = sOutputFolder + @"\" + fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length) + "." + txtOutFileExtension.Text;

                //convert from ANSI to UTF8 format
                cConvertCodePages.ConvertFileToUTF8File(item, sFileOut, encoding, bBOM);
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

                            
                            //replace the numbers
                            for (int i = 0; i < 10; i++)
                            {
                                sMatch = sMatch.Replace(i.ToString(), "");
                            }

                            //check sMatch to see if it has a star
                            if (sMatch.Contains("*"))
                            {
                                bool bHasStar = false;
                                //function that checks if normal list also has a star for this tag
                                foreach (var tg in sNormalTags)
                                {
                                    if (tg.ToString() == sMatch.ToString())
                                    {
                                        bHasStar = true;
                                        break;
                                    }
                                }
                                if (!bHasStar)
                                {
                                    //add to error list
                                    AddToExceptionList(ref oTags, sMatch, fi.Name);
                                }
                            }

                            //remove * characters
                            sMatch = sMatch.Replace("*", "");

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
            
            //string[] sMarker = { 
            //    "!$","//","\\add","\\b","\\bd","\\bdit","\\bk","\\c","\\ca","\\cd",
            //    "\\cl","\\cls","\\cp","\\d","\\dc","\\em","\\f","\\fdc","\\fe","\\fig",
            //    "\\fk","\\fl","\\fm","\\fp","\\fq","\\fqa","\\fr","\\ft","\\fv",
            //    "\\it","\\k","\\li","\\lit","\\m","\\mi","\\mt","\\ms","\\nb","\\nd","\\ndx",
            //    "\\no","\\ord","\\p","\\pb","\\pc","\\ph","\\pi","\\pm","\\pmc",
            //    "\\pmo","\\pmr","\\pn","\\pr","\\pro","\\q","\\qa","\\qac","\\qc",
            //    "\\qm","\\qr","\\qs","\\qt","\\r","\\rq","\\s","\\sc","\\sig","\\sls","\\tl","\\v",
            //    "\\va","\\vp","\\w","\\wg","\\wh","\\wj","\\x","\\xdc","\\xk",
            //    "\\xo","\\xq","\\xt"
            //};

            List<string> sStandardTags = LoadTags();
            ArrayList s = new ArrayList();
            for (int i = 0; i < sStandardTags.Count; i++)
            {
                s.Add(sStandardTags[i]);
            }
            return s;

        }

        private List<string> LoadTags()
        {
            List<string> sTagList = new List<string>();

            cUSFM_Utilities.TagType tagType = cUSFM_Utilities.TagType.eUnknown;
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(System.Windows.Forms.Application.StartupPath + @"\taglist.txt");
            while ((line = file.ReadLine()) != null)
            {
                line = line.Trim();
                if (!line.StartsWith("/")) //comment line
                {
                    switch (line)
                    {
                        case "SECTION:LINE_REMOVAL_TAGS":
                            tagType = cUSFM_Utilities.TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:SINGULAR_NUMBER_TAGS":
                            tagType = cUSFM_Utilities.TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:SINGULAR_TAGS":
                            tagType = cUSFM_Utilities.TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:DOUBLE_TAGS":
                            tagType = cUSFM_Utilities.TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:DOUBLE_FULL_TAGS":
                            tagType = cUSFM_Utilities.TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:LIST_OF_STANDARD_USFM_TAGS":
                            tagType = cUSFM_Utilities.TagType.eUSFMstandardTag;
                            break;
                        default:
                            //process the following tag
                            if (line != "")
                            {
                                switch (tagType)
                                {
                                    case cUSFM_Utilities.TagType.eUnknown:
                                        break;
                                    case cUSFM_Utilities.TagType.eLineRemovalMarker:
                                        //do nothing
                                        break;
                                    case cUSFM_Utilities.TagType.eSingularNumberTag:
                                        //do nothing
                                        break;
                                    case cUSFM_Utilities.TagType.eSingularTag:
                                        //do nothing
                                        break;
                                    case cUSFM_Utilities.TagType.eDoubleTag:
                                        //do nothing
                                        break;
                                    case cUSFM_Utilities.TagType.eDoubleFullTag:
                                        //do nothing
                                        break;
                                    case cUSFM_Utilities.TagType.eUSFMstandardTag:
                                        sTagList.Add(line);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                    }
                }
            }

            file.Close();
            return sTagList;
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
                //if (sNormalTags[i].ToString().Contains("*"))
                //{
                //    sNormalTags[i].ToString().Replace("*", "");
                //}
                if (sTagToCheck == sNormalTags[i].ToString().Replace("*",""))
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
            if (sTag.EndsWith("*"))
            {
                //make it regex complatible
                sTag = sTag.Substring(0, sTag.Length - 1) + @"\*";
            }
            //add in needed start slash for regex
            sTag = @"\" + sTag;

            //removes start tag with number and trailing space
            line = Regex.Replace(line, sTag + @"[0-9]\s", "", RegexOptions.IgnoreCase);

            //removes start tag with number
            line = Regex.Replace(line, sTag + @"[0-9]", "", RegexOptions.IgnoreCase);

            //removes start tag without number and trailing space
            //line = Regex.Replace(line, sTag + @"\s", "", RegexOptions.IgnoreCase);

            //removes start tag without number and withoug trailing space
            line = Regex.Replace(line, sTag , "", RegexOptions.IgnoreCase);

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
            sb.AppendLine("Words Greater than or Equal to " + updownLongWords.Value.ToString() + " characters");
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

            //show the export path to the user
            string sExportPath = "";
            if (_sDigiStudyPath == "")
            {
                //doesn't have DigiStudy installed on their machine
                //so put in the source directory
                sExportPath = txtDir.Text;
            }
            else
            {
                //has DigiStudy installed - check for directory existance
                //and place the results into that directory

                //pull off last directory from source path
                string sDir = txtDir.Text.Substring(txtDir.Text.LastIndexOf('\\'),txtDir.Text.Length - txtDir.Text.LastIndexOf('\\'));
                if (sDir.StartsWith(@"\"))
                {
                    sDir = sDir.Substring(1);
                }
                if (!_sDigiStudyPath.EndsWith(@"\"))
                {
                    _sDigiStudyPath += @"\";
                }
                sExportPath = _sDigiStudyPath + sDir;
            }
            //add on trailing \ character
            if (!sExportPath.EndsWith(@"\"))
            {
                sExportPath += @"\";
            }
            lblExportTo.Text = "Exporting To: " + sExportPath;

            //create directory if it doesn't exist
            if (!Directory.Exists(sExportPath))
            {
                Directory.CreateDirectory(sExportPath);
            }

            //give the user a chance to abort.
            if (MessageBox.Show("Use of this function will overwrite all .htm files in your export directory", "Continue?", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            bool bOverwriteSetupFile = false;
            if (MessageBox.Show("Do you wish to overwrite the setup.ini file?", "Overwrite file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                bOverwriteSetupFile = true;
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
                string sFileOutName = sExportPath + fi.Name.Substring(0, fi.Name.LastIndexOf(".")) + ".htm";

#if DEBUG 

                if (sFileOutName == "C:\\Documents and Settings\\Admin\\Desktop\\DigiStudy\\CARSn\\04NUMCARS2.htm")
                
                {
                    //do something
                                        
                }
#endif

                //get the bookname before swapping out the replacement characters as we need
                //it for the DigiView listbox
                string line;
                string sBookName = "";
                //Set Codepage
                using (StreamReader srBookname = new StreamReader(item, Encoding.UTF8, false))
                {
                    while ((line = srBookname.ReadLine()) != null)
                    {
                        if (line.StartsWith(txtBQ_BooknameTag.Text))
                        {
                            sBookName = line;
                            sBookName = sBookName.Substring(sBookName.IndexOf(" ")).Trim();
                            break;
                        }
                    }
                }



                // Create a new stream to read from a file
                // Read contents of file into a string
                StreamReader sr = new StreamReader(item, Encoding.UTF8, false);
                line = sr.ReadToEnd();
                
                //swap out the funny characters for the HTML codes if needed
                if (chkCharacterReplacement.Checked)
                {
                    if (_fChar !=null)
                    {
                        //get the current datatable
                        _charHTMLdatatable = _fChar.charHTMLdatatable;
                    }

                    for (int i = 0; i < _charHTMLdatatable.Rows.Count; i++)
                    {
                        //loop through making the character changes
                        try
                        {
                            line = Regex.Replace(line, _charHTMLdatatable.Rows[i][0].ToString(), _charHTMLdatatable.Rows[i][1].ToString());
                        }
                        catch (ArgumentException)
                        {
                            // Syntax error in the regular expression
                        }
                    }


                }

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
                                    Regex regexObj = new Regex(@"^.*((\\ip ).*$\r?\n?)|((\\io[0-9] ).*$\r?\n?)", RegexOptions.Multiline);
                                    Match matchResults = regexObj.Match(sSection);
                                    while (matchResults.Success)
                                    {
                                        if (matchResults.Value.Substring(0,3) == @"\ip")
                                        {
                                            oChap.AddVerse(ParseHeaderTags(matchResults.Value));
                                            oChap.AddVerse("<br/>");
                                            oChap.AddVerse("<br/>");
                                            matchResults = matchResults.NextMatch();
                                        }
                                        else if (matchResults.Value.Substring(0,3) == @"\io")
                                        {
                                            //make table of contents for chapter 0 of each book
                                            int iIndentPrev = 0;
                                            int iCountFirstLvl = 0;
                                            while (matchResults.Success)
                                            {
                                                //check this line versus previous
                                                int iIndent = GetLevelOfIndentation(matchResults.Value, iIndentPrev);//gets current level, needs iPrev to catch ip's
                                                //if it's a deeper level slap <ol> at the end of the previous verse
                                                string sOL_Type = "type=\"I\"";
                                                
                                                if (iIndent == 2)
                                                {
                                                    sOL_Type = "type=\"1\"";
                                                }
                                                else if (iIndent == 3)
                                                {
                                                    sOL_Type = "type=\"i\"";
                                                }
                                                else
                                                {
                                                    iCountFirstLvl = iCountFirstLvl+1;
                                                    sOL_Type = "value=\"" + iCountFirstLvl + "\" type=\"I\"";
                                                    
                                                }
                                                if (iIndent > iIndentPrev)
                                                {
                                                    oChap.Verses[oChap.Verses.Count - 1] = oChap.Verses[oChap.Verses.Count - 1] + "<ol " + sOL_Type + ">";
                                                }
                                                //if it's previous level slap "</ol>" at the end of the previous verse
                                                else if (iIndent < iIndentPrev)
                                                {
                                                    oChap.Verses[oChap.Verses.Count - 1] = oChap.Verses[oChap.Verses.Count - 1] + "</ol>";
                                                }
                                                //do nothing if it's the same level
                                                else
                                                {

                                                }

                                                //process the level
                                                //replace io's with <li> tags
                                                if (matchResults.Value.Substring(0,3) == @"\io")
                                                {
                                                    oChap.AddVerse(Regex.Replace(matchResults.Value, @"\\io" + iIndent + " ", "<li>", RegexOptions.None));
                                                }
                                                //in case it has ip's in the middle of io's check how deep was the list
                                                else
                                                {
                                                    //if deeper than 1 then close ol tag
                                                    if (iIndentPrev - iIndent > 2)
                                                    {
                                                        oChap.AddVerse("</ol>");
                                                        oChap.AddVerse("</ol>");
                                                    }
                                                    else
                                                    {
                                                        oChap.AddVerse("</ol>");     
                                                    }
                                                    //insert actual line surrounded by breaks
                                                    oChap.AddVerse("<br/>");
                                                    oChap.AddVerse(ParseHeaderTags(matchResults.Value));
                                                    oChap.AddVerse("<br/>");
                                                }
                                                
                                                matchResults = matchResults.NextMatch();

                                                //set the variable for current indentation level
                                                iIndentPrev = iIndent;


                                            }
                                            oChap.AddVerse("</ol>");  
                                        }
                                        
                                    }
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
                            else //if chapter 0 is empty like in 2Sam, 2Ki, and 2Chron
                            {
                                cBQ_Chapter oChap = new cBQ_Chapter(0);
                                oChap.AddVerse("<br/>");
                                bChapterZero = true;
                                oBook.AddChapter(oChap);
                            }
                            
                        }
                        else 
                        {
                            #region normal bible chapters
                            cBQ_Chapter oChap = new cBQ_Chapter(i);
                            //grab all the \ip lines of text
                            try
                            {
                                bool bNewParagraph = false;//if new paragraph must be started
                                bool bPI = false;//if blockquote is open
                                bool bDTtag = false;//if dt tag was open
                                
                                //todo add in the \s tag processing
                                Regex regexObj = new Regex(@"^.*((\\[a-z][0-9]).*$\r?\n?)|((\\[a-z]).*$\r?\n?)", RegexOptions.Multiline);
                                Match matchResults = regexObj.Match(sSection);
                                while (matchResults.Success)
                                {
                                    
                                    //if it's a heading
                                    if (matchResults.Value.Substring(0,2) == @"\s")
                                    {
                                    
                                        //if it's an \s tag
                                        string sTemp = ParseVerseTags(matchResults.Value);
                                        //put in extra breaks
                                        string sPrevVerse = "";
                                        if (bPI)
                                        {
                                            //close \pi (blockquote) tag if it was open
                                            oChap.AddVerse("</blockquote>");
                                            //and set bPI to false to show it's been closed
                                            bPI = false;
                                            //there is no need for a <br> tag because blockquote takes some extra space before and after
                                        }
                                        if (oChap.Verses.Count > 0)
                                        {
                                            //find the number of the previous verse (if there are verses)
                                            sPrevVerse = oChap.Verses[oChap.Verses.Count - 1].ToString();
                                        }
                                        
                                        //if previous verse does not end with <h2> (if it's not a header) then add some breaks
                                        if (!sPrevVerse.EndsWith("</h2>"))
                                        {
                                            oChap.AddVerse("<br/>");
                                            oChap.AddVerse("<br/>");
                                        }
                                        //check if dt or dd was open and close it
                                        if (bDTtag)
                                        {
                                            oChap.AddVerse("<br/>");
                                            oChap.AddVerse("</dt>");
                                            bDTtag = false;
                                        }
                                        else if (bDDtag)
                                        {
                                            oChap.AddVerse("<br/>");
                                            oChap.AddVerse("</dd><dt></dt>");
                                            bDDtag = false;
                                        }
                                        
                                        oChap.AddVerse("! <b>" + sTemp + "</b>");
                                        oChap.AddVerse("<br/>");

                                    }
                                    //if it's a parallel ref
                                    else if (matchResults.Value.Substring(0, 2) == @"\r")
                                    {

                                        //if it's an \r tag
                                        string sTemp = matchResults.Value;
                                        sTemp = sTemp.Replace(@"\r", "").Trim();
                                        //put in extra breaks
                                        oChap.AddVerse("! <i>" + sTemp + "</i>");
                                        oChap.AddVerse("<br/>");

                                    }
                                    else if (matchResults.Value.Substring(0,3) == @"\ms")
                                    {
                                        //poetry \ms tags
                                        string sTemp = matchResults.Value;
                                        sTemp = sTemp.Replace(@"\ms", "").Trim();
                                        //put in extra breaks
                                        oChap.AddVerse("<br/>");
                                        oChap.AddVerse("! <h2>" + sTemp + "</h2>");

                                    }
                                    
                                    //if it's a regular verse
                                    else if (matchResults.Value.Substring(0,2) == @"\v")
                                    {
                                        string s = ParseVerseTags(matchResults.Value);
                                        if (s != String.Empty)
                                        {
                                            //check if it's a new paragraph
                                            if (bNewParagraph)
                                            {
                                                if (bDDtag)
                                                {
                                                    oChap.AddVerse("</dd><dt></dt>");
                                                    bDDtag = false;
                                                }
                                                s = s.Insert(s.IndexOf(" ")+1, "&nbsp;&nbsp;&nbsp;");
                                                bNewParagraph = false;
                                            }
                                            
                                            oChap.AddVerse(s);
                                            
                                        }
                                        
                                    }
                                    //check if it's a blockquote tag \pi
                                    else if (matchResults.Value.Substring(0, 3) == @"\pi")
                                    {
                                        //if one has already been open then close it
                                        if (bPI)
                                        {
                                            oChap.AddVerse("</blockquote>");
                                        }
                                        //indicate that you're openning one
                                        bPI = true;
                                        //parse the string, should come back the same
                                        string s = ParseVerseTags(matchResults.Value);
                                        //then replace the \pi in the beginning
                                        //s = s.Replace(@"\pi", "").Trim();
                                        //add a blockquote line
                                        //oChap.AddVerse("<blockquote>");
                                        //add actual text
                                        oChap.AddVerse(s);
                                    }
                                    else
                                    {
                                        //if it's everything else
                                        string s = ParseVerseTags(matchResults.Value);
                                        if (s != String.Empty)
                                        {
                                            //if it's an empty /p tag, make bNewParagraph true, 
                                            //so that we can slap in some &nbsp's in front of the next line
                                            if (s == "p")
                                            {
                                                bNewParagraph = true;
                                                oChap.AddVerse("<br/>");
                                                
                                            }
                                            else if (s == "<dt>")
                                            {
                                                if (bDTtag)
                                                {
                                                    oChap.AddVerse("</dt>");
                                                    bDTtag = false;
                                                }
                                                else if (bDDtag)
                                                {
                                                    oChap.AddVerse("</dd><dt></dt>");
                                                    bDDtag = false;
                                                }
                                                oChap.AddVerse("<br/>");
                                                oChap.AddVerse(s);
                                                bDTtag = true;

                                            }
                                            else if (s == "<dd>")
                                            {
                                                if (bDTtag)
                                                {
                                                    oChap.AddVerse("</dt>");
                                                    bDTtag = false;
                                                }
                                                else if (bDDtag)
                                                {
                                                    oChap.AddVerse("</dd><dt></dt>");
                                                    bDDtag = false;
                                                }
                                                oChap.AddVerse("<br/>");
                                                oChap.AddVerse(s);
                                                bDDtag = true;
                                            }
                                            else
                                            {
                                                if (bDTtag)
                                                {
                                                    oChap.AddVerse("</dt>");
                                                    bDTtag = false;
                                                }
                                                else if (bDDtag)
                                                {
                                                    oChap.AddVerse("</dd><dt></dt>");
                                                    bDDtag = false;
                                                }
                                                //mostly non-empty \p tags
                                                oChap.AddVerse("<br/>");
                                                oChap.AddVerse(s);  
                                            }
                                            
                                                                                     
                                        }
                                        else //this includes \b, \q1, \li1 and so on tags
                                        {
                                            if (bPI)
                                            {
                                                //close \pi (blockquote) tag if it was open
                                                oChap.AddVerse("</blockquote>");
                                                //and set bPI to false to show it's been closed
                                                bPI = false;
                                                //there is no need for a <br> tag because blockquote takes some extra space before and after
                                            }
                                            else if (!bDDtag)
                                            {
                                                
                                            }
                                            else
                                            {
                                                oChap.AddVerse("<br/>");
                                            }
                                            
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
#endregion
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

                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();

                //dump out the book to a file
                ExportBQFile(sFileOutName, oBook);

                //create a new ini entry
                cBQ_IniStructure oIni = new cBQ_IniStructure();
                oIni.PathName = fi.Name.Substring(0,fi.Name.LastIndexOf(".")) + ".htm";
                oIni.FullName = sBookName;  //the book name with all the unicode characters
                oIni.HTMLName = oBook.sBookName;  //the book name with HTML characters
                oIni.ChapterQty = oBook.GetNumberOfChapters;

                //add into the ini collection
                oIniFile.Add(oIni);

                //iterate the progress bar
                progressBar1.Value++;
                progressBar1.Refresh();
            }
            if (bOverwriteSetupFile)
            {
                //dump out the setup.ini file to a computer
                ExportBQiniFile(oIniFile, bChapterZero, sExportPath);
            }
            
            
            //reset the cursor
            this.Cursor = Cursors.Default;
            
            //check for DigiStudy's existance
            if (File.Exists(_sDigiStudyPath + "digistudy.exe"))
            {
                if (MessageBox.Show("Completed Conversion - Do you wish to start DigiStudy?", "Completed", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start(_sDigiStudyPath + "digistudy.exe");
                }
            }
            else
            {
                MessageBox.Show("Completed Conversion");
            }
        }

        private int GetLevelOfIndentation(string p, int iPrev)
        {
            int level;
            try
            {
                //get's the number from \io1 or \io2 or \io3 tag
                level = Convert.ToInt16(p.Substring(3, 1));
            }
            catch (Exception)
            {
                //catch an \ip tag in the middle of \io[n] tags
                //keeps level the same as previous one
                level = 0;
            }
            

            return level;
        } 


        /// <summary>
        /// Export the Bible Quotes ini file
        /// </summary>
        /// <param name="oIniFile"></param>
        private void ExportBQiniFile(List<cBQ_IniStructure> oIniFile, bool bChapZero, string sFilePath)
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
            sb.AppendLine("Copyright = " + txtBQCopyright.Text);

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
                sb.AppendLine("HTMLName = " + oIni.HTMLName);
                sb.AppendLine("ShortName = " + oIni.ShortName);
                sb.AppendLine("ChapterQty = " + oIni.ChapterQty.ToString());
                sb.AppendLine("");
            }

            //write out the ini to a UTF8 file
            using (StreamWriter sw = new StreamWriter(sFilePath + @"setup.ini", false, Encoding.UTF8))
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
                                //this is a section header \s or referece \r
                                sb.AppendLine(sVerseStart + sVerseEnd + sTmp.Substring(2));
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
                            //else if (sTmp == "<blockquote>")
                            //{
                            //    sb.AppendLine("<blockquote>");
                            //}
                            //else if (sTmp == "</blockquote>")
                            //{
                            //    sb.AppendLine("</blockquote>");
                            //}
                            //else if (sTmp == "<dt>")
                            //{
                            //    sb.AppendLine("<dt>");
                            //}
                            else if (sTmp.StartsWith("<"))
                            {
                                sb.AppendLine(sTmp);
                            }
                            else
                            {
                                //when it's a normal verse
                                try
                                {
                                    //will error out if missing any verse text, if there is just a tag without text following it
                                    string sVerseNum = sTmp.Substring(0, sTmp.IndexOf(" "));
                                    sb.AppendLine(sVerseStart + sVerseNum + sVerseEnd + sTmp.Substring(sVerseNum.Length, sTmp.Length - sVerseNum.Length).TrimStart());
                                    iCount = 0;
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("ERROR: " + ex.Message);
                                }
                            }
                        }
                        
                        
                    }
                }
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
            cUSFM_Utilities Util = new cUSFM_Utilities();
            sVerse = Util.RemoveDoubleMarkerTags(sVerse);
            sVerse = Util.RemoveDoubleMarkerTagsFull(sVerse);
            sVerse = Util.RemoveSingularMarkerTags(sVerse);
            sVerse = Util.RemoveVerseNumbering(sVerse, ref _bDDtag);
            sVerse = Util.ProcessOtherTags(sVerse);
            return sVerse;
        }

        /// <summary>
        /// Parse out the normal verse tags
        /// </summary>
        /// <param name="sVerse"></param>
        /// <returns></returns>
        private string ParseVerseTagsEPUB(string sVerse)
        {
            cUSFM_Utilities Util = new cUSFM_Utilities();
            //if (!Util.CheckIfWeKeepLine(sVerse))
            //{
            //    return "";
            //}
            sVerse = Util.RemoveDoubleMarkerTags(sVerse);
            sVerse = Util.RemoveDoubleMarkerTagsFull(sVerse);
            sVerse = Util.RemoveSingularMarkerTags(sVerse);
            sVerse = Util.RemoveVerseNumberingEPUB(sVerse);
            sVerse = Util.ProcessOtherTags(sVerse);
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
                sVerse = Regex.Replace(sVerse, @"\\ip ", "&nbsp;&nbsp;&nbsp;", RegexOptions.None);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                throw;
            }
            
            return sVerse;
        }

        /// <summary>
        /// parse the ip tags out of the verses for EPUB
        /// </summary>
        /// <param name="sVerse"></param>
        /// <returns></returns>
        private string ParseHeaderTagsEPUB(string sVerse)
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
            DetectAndFixVProblems(false);
            
        }

        private void DetectAndFixVProblems(bool bFix)
        {
            this.Cursor = Cursors.WaitCursor;
            bool bProblem = false;
            List<cVersification> oVersificationList = new List<cVersification>();
            int iOld = 0;//for file name
            bool bFineNumbDone = false;
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
                StringBuilder sbFileOut = new StringBuilder();
                // Create a new stream to read from a file
                // Read contents of file into a string
                string line = "";
                try
                {
                    //read in the entire file
                    line = sr.ReadToEnd();
                    sbFileOut.Append(line.Substring(0, line.IndexOf("\\c ")));
                    //split the text at the chapter tag as we don't need the header information
                    line = line.Substring(line.IndexOf("\\c "));
                }
                catch (Exception)
                {
                    bProblem = true;
                    // Close file
                    file.Close();
                }

                if (!bProblem)
                {
                    // Close file
                    file.Close();

                    //bust the book up into chapters
                    string[] sSplitChar = new string[] { "\\c " };
                    string[] sChapters = line.Split(sSplitChar, StringSplitOptions.RemoveEmptyEntries);

                    //iterate through each chapter
                    for (int i = 0; i < sChapters.Length; i++)
                    {
                        List<cVerseHolder> oVHOut = new List<cVerseHolder>();
                        //try if v tags exist in the file
                        try
                        {

                            //append everything between chapter number and first verse number
                            sbFileOut.Append("\\c " + sChapters[i].Substring(0, sChapters[i].IndexOf("\\v ")));
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                        
                        //split into verses
                        string[] sSplitVerse = new string[] { "\\v" };
                        string[] sVerses = sChapters[i].Split(sSplitVerse, StringSplitOptions.RemoveEmptyEntries);

                        List<cVerseHolder> oVH = new List<cVerseHolder>();
                        for (int j = 1; j < sVerses.Length; j++)  //start at verse 1
                        {
                            oVH.Add(RipOutVerseNumber(sVerses[j]));
                        }
                        List<cVerseHolder> oVHTemp = new List<cVerseHolder>();
                        for (int j = 0; j < oVH.Count - 1; j++)
                        {
                            oVHTemp = cVerseHolder.CheckForVersification(oVH[j], oVH[j + 1], ref oVersificationList, sFilename, (i + 1).ToString(), txtReplacemetText.Text, chkVerseRangeMarkers.Checked);
                            //
                            foreach (cVerseHolder verse in oVHTemp)
                            {
                                oVHOut.Add(verse);
                                //append fileOut
                                sbFileOut.Append("\\v " + verse.sVerseNum + " " + verse.sVerse + verse.sExtraText + "\r");
                            }

                        }

                        //deal with the last element in oVH
                        if (oVHTemp.Count > 0)//make sure oVHTemp is not empty
                        {
                            oVHTemp = cVerseHolder.CheckLastOneForVersification(oVH[oVH.Count - 1], ref oVersificationList, sFilename, (i + 1).ToString(), txtReplacemetText.Text, chkVerseRangeMarkers.Checked);
                            foreach (cVerseHolder verse in oVHTemp)
                            {
                                oVHOut.Add(verse);
                                //append fileOut
                                sbFileOut.Append("\n\\v " + verse.sVerseNum + " " + verse.sVerse + verse.sExtraText + "\r");
                            }
                        }
                        

                    }


                }
                // Close StreamReader
                sr.Close();
                
                if (bFix)//alter the file
                {
                    
                    //copy the file with a different extension
                    //check directory for files with .old extension
                    //check if .old has a - after it
                    //if it does, search through such files and find what's the highest number after the -
                    if (!bFineNumbDone)
                    {
                        iOld = CreateFileName(iOld, file);
                        bFineNumbDone = true;
                    }

                    string sPath = file.Name.Substring(0, file.Name.Length - fi.Extension.Length);
                    System.IO.File.Copy(file.Name, sPath + ".old-" + iOld.ToString());
                    //reopen the file and dump fileOut in it
                    // create a writer and open the file
                    TextWriter tw = new StreamWriter(file.Name);

                    // write the rest of the text lines
                    tw.Write(sbFileOut);

                    // close the stream
                    tw.Close();
                    
                    //System.IO.TextWriter 
                    //close file
                }
                
            }
            if (bFix)
            {
                MessageBox.Show("All files have been modified for versification issues. \nOld files names were given an extension .old-" + iOld.ToString());
            }
            //parse out data for the clipboard
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Versification Issues");
            sb.AppendLine("Filename\tChapter\tVerse");
            foreach (cVersification lw in oVersificationList)
            {
                sb.AppendLine(lw.sFileName + "\t" + lw.sChapNum + "\t" + lw.sVerseNum + "\t" + lw.sMessage);
                
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

        private static int CreateFileName(int iOld, FileStream file)
        {
            string[] sFiles = Directory.GetFiles(file.Name.Substring(0, file.Name.LastIndexOf("\\")), "*.old-*");
            int iPrev = -1;
            int iCurrent = -1;
            int iTemp = -1;
            foreach (string s in sFiles)
            {
                string sExt = s.Substring(s.LastIndexOf("."));
                sExt = sExt.Substring(5);

                if (sExt.Length > 0)
                {
                    if (cUtils.IsNumeric(sExt))
                    {
                        
                        iCurrent = Convert.ToInt16(sExt);
                        if (iCurrent > iPrev)
                        {
                            iTemp = iCurrent;

                        }
                        
                        iPrev = iCurrent;
                    }
                }
                
            }
            iOld = iTemp + 1;
            return iOld;
        }

        private cVerseHolder RipOutVerseNumber(string p)
        {
            cVerseHolder vh = new cVerseHolder();
            //rip out the verse number - will have a space after the number
            p = p.TrimStart();

            //look for a normal number followed by a space then the verse text
            //this is the case of a verse number followed by now verse text
            //and either white space or no space
            if (p.IndexOf("\r") < 4 && p.IndexOf("\r") != -1)
            {
                //take such line and one by one character strip out the number of the verse
                string sVnum = "";
                //check if there is a verse number
                foreach (char c in p)
                {
                    if (cUtils.IsNumeric(c))
                    {
                        sVnum = sVnum + c;
                    }
                    else
                    {
                        break;
                    }
                }
                vh.sVerseNum = sVnum;
                //rest of the text put in sExtraText field
                //remover verse number from the start of the p
                p = p.Substring(sVnum.Length);
                //create a string array by splitting on spaces
                string[] sSplitChar = new string[] { " " };
                string[] sText = p.Split(sSplitChar, StringSplitOptions.RemoveEmptyEntries);
                bool bFoundVerseText = false;
                //loop through each sText
                foreach (string s in sText)
                {
                    //first check to see if it's empty
                    if (s != String.Empty)
                    {
                        if (s != "\r\n")
                        {
                            string sT = s.Replace("\r\n", "");
                            //test to see if it starts with a \
                            if (!sT.StartsWith(@"\"))
                            {
                                //this is actual verse text
                                bFoundVerseText = true;
                                break;
                            }
                        }

                        
                    }
                }
                if (bFoundVerseText)
                {
                    vh.sVerse = p.Substring(sVnum.Length);
                }
                else
                {
                    if (p != "\r\n")
                    {
                        vh.sExtraText = p.Replace("\r\n", "").Trim();
                    }
                }
                return vh;                
            }

            string sTmp = p.Substring(0, p.IndexOf(" "));
            double result;
            if (double.TryParse(sTmp, out result))
            {
                //valid number
                vh.sVerseNum = sTmp;
                vh.sVerse = p.Substring(p.IndexOf(" ")).Trim();
                return vh;
            }
            else
            {
                if (sTmp.IndexOf('-') > -1)
                {
                    //verse range like 32-39
                    vh.sVerseNum = sTmp;
                    vh.sVerse = p.Substring(p.IndexOf(" ")).Trim();
                    return vh;
                }
                else
                {
                    //not a valid verse number
                    vh.sExtraText = p;
                    return vh;
                }
            }

        }

        private void cmdAbout_Click(object sender, EventArgs e)
        {
            frmAbout f = new frmAbout();
            f.ShowDialog();
        }

        private void chkCharacterReplacement_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCharacterReplacement.Checked)
            {
                frmCharReplacer f = new frmCharReplacer(this);
                f.Show();
            }
        }

        private void cmdReportAndFix_Click(object sender, EventArgs e)
        {
            if (txtReplacemetText.Text == "")
            {
                MessageBox.Show("You need to provide replacement text");
                txtReplacemetText.BackColor = Color.Red;
            }
            else
            {
                txtReplacemetText.BackColor = Color.White;
                DetectAndFixVProblems(true);
                cRegistry.SetStringRegistryValue("replacement", txtReplacemetText.Text);    
            }
            
        }

        private void txtDir_KeyDown(object sender, KeyEventArgs e)
        {
            //look for enter key (13)
            if (e.KeyValue == 13)
            {
                //trigger file extension refresh
                PopulateExtensionList(ExtensionType.EnterButton);
            }
        }

        private void txtReplacemetText_TextChanged(object sender, EventArgs e)
        {
            if (txtReplacemetText.Text.Length > 0)
            {
                txtReplacemetText.BackColor = Color.White;
            }
        }

        private void cmdGenerateEPUB_Click(object sender, EventArgs e)
        {
            //check to see if all the fields have been filled in
            if (EPUBExportValidation() == true)
            {
                MessageBox.Show("Please fill in required fields");
                return;
            }
            //show the export path to the user
            string sExportPath = sExportPath = txtDir.Text;

            //add on trailing \ character
            if (!sExportPath.EndsWith(@"\"))
            {
                sExportPath += @"\";
            }
            lblExport.Text = "Exporting To: " + sExportPath;

            bool bParagraphMode = chbParagraphMode.Checked;

            //create directory if it doesn't exist
            if (!Directory.Exists(sExportPath))
            {
                Directory.CreateDirectory(sExportPath);
            }

            //give the user a chance to abort.
            if (MessageBox.Show("Use of this function will overwrite all .htm files in your export directory", "Continue?", MessageBoxButtons.YesNo) == DialogResult.No)
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
            progressBar2.Value = 0;
            progressBar2.Maximum = lvBooks.CheckedItems.Count;
            int iOrder = 10;

            //iterate through each file
            foreach (System.Windows.Forms.ListViewItem item in lvBooks.CheckedItems)
            //foreach (string item in filePaths)
            {
                
                bProblem = false;
                //create a book object to hold the chapters / verses
                cBQ_Book oBook = new cBQ_Book();

                string sVerseStart = txtEPUBVerseTag.Text;
                string sVerseEnd = "</" + txtEPUBVerseTag.Text.Substring(1);

                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(sExportPath + item.SubItems[2].Text, FileMode.OpenOrCreate, FileAccess.Read);
                FileInfo fi = new FileInfo(file.Name);
                string sFilename = item.SubItems[2].Text;
                string sFileOutName = sExportPath + iOrder.ToString() + sFilename.Substring(0, sFilename.LastIndexOf(".")) + ".html";
                iOrder++;
                //get the bookname before swapping out the replacement characters as we need
                //it for the DigiView listbox
                string line;
                string sBookName = "";
                //Set Codepage
                using (StreamReader srBookname = new StreamReader(sExportPath + sFilename, Encoding.UTF8, false))
                {
                    while ((line = srBookname.ReadLine()) != null)
                    {
                        if (line.StartsWith(txtBQ_BooknameTag.Text))
                        {
                            sBookName = line;
                            sBookName = sBookName.Substring(sBookName.IndexOf(" ")).Trim();
                            break;
                        }
                    }
                }

                // Create a new stream to read from a file
                // Read contents of file into a string
                StreamReader sr = new StreamReader(sExportPath + sFilename, Encoding.UTF8, false);
                line = sr.ReadToEnd();

                //swap out the funny characters for the HTML codes if needed
                if (chkCharacterReplacement.Checked)
                {
                    if (_fChar != null)
                    {
                        //get the current datatable
                        _charHTMLdatatable = _fChar.charHTMLdatatable;
                    }

                    for (int i = 0; i < _charHTMLdatatable.Rows.Count; i++)
                    {
                        //loop through making the character changes
                        try
                        {
                            line = Regex.Replace(line, _charHTMLdatatable.Rows[i][0].ToString(), _charHTMLdatatable.Rows[i][1].ToString());
                        }
                        catch (ArgumentException)
                        {
                            // Syntax error in the regular expression
                        }
                    }
                }

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
                                    int iExceptionIndent = 0;
                                    string sExtraHeader = "";
                                    int iIndent = 0;
                                    Regex regexObj = new Regex(@"^.*((\\ip ).*$\r?\n?)|((\\io[0-9] ).*$\r?\n?)", RegexOptions.Multiline);
                                    Match matchResults = regexObj.Match(sSection);
                                    while (matchResults.Success)
                                    {
                                        
                                        if (matchResults.Value.Substring(0, 3) == @"\ip")
                                        {
                                            oChap.AddVerse("<p class=\"intro\">" + ParseHeaderTagsEPUB(matchResults.Value) + "</p>");
                                            
                                            matchResults = matchResults.NextMatch();
                                        }
                                        else if (matchResults.Value.Substring(0, 3) == @"\io")
                                        {
                                            //make table of contents for chapter 0 of each book
                                            int iIndentPrev = 0;
                                            //do this for complex TOC like in 1Ki
                                            if (iExceptionIndent > 0)
                                            {
                                                iIndentPrev = iExceptionIndent;
                                            }
                                            while (matchResults.Success)
                                            {
                                                if (matchResults.Value.Substring(0, 3) == @"\ip")
                                                {
                                                    sExtraHeader = "<p class=\"intro\">" + ParseHeaderTagsEPUB(matchResults.Value) + "</p>";
                                                    //grab next line to see what level it will be
                                                    matchResults = matchResults.NextMatch();
                                                    iIndent = GetLevelOfIndentation(matchResults.Value, iIndentPrev);//gets current level, needs iPrev to catch ip's
                                                    //slap in needed </ol> and </li> accroding to this next level of indent 
                                                    oChap.AddVerse("</li>");
                                                    for (int j = 0; j < iIndent-iIndentPrev; j++)
                                                    {
                                                        oChap.AddVerse("</ol></li>");
                                                    }
                                                    if ((iIndent-iIndentPrev)>0)
                                                    {
                                                        oChap.AddVerse("</ol>");
                                                    }
                                                    //only then insert extra header section
                                                    oChap.AddVerse(sExtraHeader);
                                                    iExceptionIndent = iIndentPrev;
                                                    break;
                                                }
                                                //check this line versus previous
                                                iIndent = GetLevelOfIndentation(matchResults.Value, iIndentPrev);//gets current level, needs iPrev to catch ip's

                                                //if it's a deeper level slap <ol> at the end of the previous verse
                                                
                                                if (iIndent > iIndentPrev)
                                                {
                                                    oChap.AddVerse("<ol>");                                                    
                                                }
                                                //if it's previous level slap "</ol>" at the end of the previous verse
                                                else if (iIndent < iIndentPrev)
                                                {
                                                    oChap.AddVerse("</li>");
                                                    for (int j = 0; j < iIndentPrev-iIndent; j++)
                                                    {
                                                        oChap.AddVerse("</ol></li>");
                                                    }

                                                }
                                                //do nothing if it's the same level
                                                else
                                                {
                                                    if (iExceptionIndent == 0)
                                                    {
                                                        oChap.Verses[oChap.Verses.Count - 1] = oChap.Verses[oChap.Verses.Count - 1] + "</li>";
                                                    }
                                                    else
                                                    {                                                        
                                                        iExceptionIndent = 0; //set it back to 0, so that the ending of the list is not messed up
                                                    }
                                                    
                                                }

                                                //process the level
                                                //replace io's with <li> tags
                                                if (matchResults.Value.Substring(0, 3) == @"\io")
                                                {
                                                    string sValue = "";
                                                    try
                                                    {
                                                        sValue = matchResults.Value.Replace("\r\n", "");
                                                    }
                                                    catch (Exception)
                                                    {                                                        
                                                        throw;
                                                    }
                                                    oChap.AddVerse(Regex.Replace(sValue, @"\\io" + iIndent + " ", "<li>", RegexOptions.None));
                                                }

                                                matchResults = matchResults.NextMatch();

                                                //set the variable for current indentation level
                                                iIndentPrev = iIndent;

                                            }
                                            //don't do it if it was a complex outline like 1Ki, 1Sa, Jn
                                            if (iExceptionIndent == 0)
                                            {
                                                oChap.AddVerse("</li>");
                                                for (int j = 1; j < iIndentPrev; j++)
                                                {
                                                    oChap.AddVerse("</ol></li>");
                                                }
                                                oChap.AddVerse("</ol>");
                                            }                                            
                                        }
                                    }
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
                            else //if chapter 0 is empty like in 2Sam, 2Ki, and 2Chron
                            {
                                cBQ_Chapter oChap = new cBQ_Chapter(0);
                                bChapterZero = true;
                                oBook.AddChapter(oChap);
                            }

                        }
                        else
                        {
                            #region normal bible chapters
                            cBQ_Chapter oChap = new cBQ_Chapter(i);
                            int ltr = 97;
                            int fnum = 1;
                            //grab all the \ip lines of text
                            try
                            {                                
                                bool bNewParagraph = false;//if new paragraph must be started
                                bool bPI = false;//if blockquote is open
                                //bool bDTtag = false;//if dt tag was open
                                if (bPI)
                                {
                                    //close \pi (blockquote) tag if it was open
                                    oChap.AddVerse("</blockquote>");
                                    //and set bPI to false to show it's been closed
                                    bPI = false;
                                    //there is no need for a <br> tag because blockquote takes some extra space before and after
                                }
                                //todo add in the \s tag processing
                                Regex regexObj = new Regex(@"^.*((\\[a-z][0-9]).*$\r?\n?)|((\\[a-z]).*$\r?\n?)", RegexOptions.Multiline);
                                Match matchResults = regexObj.Match(sSection);

                                while (matchResults.Success)
                                {                                    
                                    //if it's a heading
                                    if (matchResults.Value.Substring(0, 2) == @"\s")
                                    {                                        
                                        //if it's an \s tag
                                        string sTemp = ParseVerseTagsEPUB(matchResults.Value);
                                        //put in extra breaks
                                        string sPrevVerse = "";
                                        if (bPI)
                                        {
                                            //close \pi (blockquote) tag if it was open
                                            oChap.AddVerse("</blockquote>");
                                            //and set bPI to false to show it's been closed
                                            bPI = false;
                                            //there is no need for a <br> tag because blockquote takes some extra space before and after
                                        }
                                        if (oChap.Verses.Count > 0)
                                        {
                                            //find the number of the previous verse (if there are verses)
                                            sPrevVerse = oChap.Verses[oChap.Verses.Count - 1].ToString();
                                        }

                                        sTemp = oChap.AddFootnote(sTemp, ref fnum);
                                        sTemp = oChap.AddCrossRef(sTemp, ref ltr, i);
                                        oChap.AddVerse("! <h5>" + sTemp + "</h5>");
                                    }
                                    //if it's a parallel ref
                                    else if (matchResults.Value.Substring(0, 2) == @"\r")
                                    {
                                        //if it's an \r tag
                                        string sTemp = ParseVerseTagsEPUB(matchResults.Value);
                                        sTemp = sTemp.Replace(@"\r", "").Trim();
                                        //put in extra breaks
                                        oChap.AddVerse("! <h6>" + sTemp + "</h6>");
                                    }
                                    else if (matchResults.Value.Substring(0, 3) == @"\ms")
                                    {
                                        //poetry \ms tags
                                        string sTemp = matchResults.Value;
                                        sTemp = sTemp.Replace(@"\ms", "").Trim();
                                        //put in extra breaks
                                        oChap.AddVerse("! <h2>" + sTemp + "</h2>");
                                    }

                                    //if it's a regular verse
                                    else if (matchResults.Value.Substring(0, 2) == @"\v")
                                    {
                                        string s = ParseVerseTagsEPUB(matchResults.Value);
                                        if (s != String.Empty)
                                        {
                                            s = oChap.AddFootnote(s, ref fnum);
                                            s = oChap.AddCrossRef(s, ref ltr, i);
                                            if (bParagraphMode)
                                            {
                                                //paragraph
                                                if (!bNewParagraph)
                                                {
                                                    s = WrapVerse(sVerseStart, sVerseEnd, s);
                                                    oChap.AddVerse(s);    
                                                }
                                                else
                                                {
                                                    s = WrapVerse(sVerseStart, sVerseEnd, s);
                                                    oChap.AddVerse("<p class=\"prgr\">" + s);
                                                    bNewParagraph = false;
                                                }                                                
                                            }
                                            else
                                            {
                                                //line-by-line
                                                s = WrapVerse(sVerseStart, sVerseEnd, s);
                                                oChap.AddVerse("<p>" + s + "</p>");
                                            }                                            

                                        }

                                    }
                                    //check if it's a blockquote tag \pi
                                    else if (matchResults.Value.Substring(0, 3) == @"\pi")
                                    {
                                        //if one has already been open then close it
                                        if (bPI)
                                        {
                                            oChap.AddVerse("</blockquote>");
                                        }
                                        //indicate that you're openning one
                                        bPI = true;
                                        //parse the string, should come back the same
                                        string s = ParseVerseTagsEPUB(matchResults.Value);                                       
                                        //add actual text
                                        s = oChap.AddFootnote(s, ref fnum);
                                        s = oChap.AddCrossRef(s, ref ltr, i);
                                        oChap.AddVerse(s);
                                    }
                                    else
                                    {
                                        //if it's everything else
                                        string s = ParseVerseTagsEPUB(matchResults.Value);
                                        if (s != String.Empty)
                                        {
                                            //if it's an empty /p tag, make bNewParagraph true, 
                                            //so that we can slap in some &nbsp's in front of the next line
                                            if (s == "p")
                                            {
                                                bNewParagraph = true;
                                            }                                            
                                            else
                                            {
                                                //mostly non-empty \p tags
                                                if (s.StartsWith("<p") || s.StartsWith("<div"))
                                                {
                                                    s = oChap.AddFootnote(s, ref fnum);
                                                    s = oChap.AddCrossRef(s, ref ltr, i);
                                                    oChap.AddVerse(s.Trim());
                                                }
                                                else
                                                {
                                                    s = oChap.AddFootnote(s, ref fnum);
                                                    s = oChap.AddCrossRef(s, ref ltr, i);
                                                    oChap.AddVerse("<p>" + s.Trim() + "</p>");
                                                }                                                
                                            }
                                        }
                                        else //this includes \b, \q1, \li1 and so on tags
                                        {
                                            if (bPI)
                                            {
                                                //close \pi (blockquote) tag if it was open
                                                oChap.AddVerse("</blockquote>");
                                                //and set bPI to false to show it's been closed
                                                bPI = false;
                                                //there is no need for a <br> tag because blockquote takes some extra space before and after
                                            }
                                            else
                                            {
                                                oChap.AddVerse("<p></p>");
                                            }
                                        }

                                    }
                                    matchResults = matchResults.NextMatch();
                                }
                                if (bPI)
                                {
                                    //close \pi (blockquote) tag if it was open
                                    oChap.AddVerse("</blockquote>");
                                    //and set bPI to false to show it's been closed
                                    bPI = false;
                                    //there is no need for a <br> tag because blockquote takes some extra space before and after
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("ERROR: " + ex.Message);
                                throw;
                            }
                            #endregion
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

                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();
                
                //dump out the book to a file
                ExportToEPUBFile(sFileOutName, oBook, cboEpubLanguage.SelectedValue.ToString());

                //create a new ini entry
                cBQ_IniStructure oIni = new cBQ_IniStructure();
                oIni.PathName = sFilename.Substring(0, sFilename.LastIndexOf(".")) + ".html";
                oIni.FullName = sBookName;  //the book name with all the unicode characters
                oIni.HTMLName = oBook.sBookName;  //the book name with HTML characters
                oIni.ChapterQty = oBook.GetNumberOfChapters;

                //add into the ini collection
                oIniFile.Add(oIni);

                //iterate the progress bar
                progressBar2.Value++;
                progressBar2.Refresh();
            }

            CreateEpubTitlePage(sExportPath, cboEpubLanguage.SelectedValue.ToString());
            //put in the epub
            Epub epub = new Epub(sExportPath, TocOptions.ByTitleTag);
            epub.Metadata.Creator = "Creator";
            epub.Metadata.Language = cboEpubLanguage.Text;
            epub.Metadata.Title = txtFullName.Text;
            epub.Metadata.Date = DateTime.Now.ToShortDateString();
            epub.Structure.Directories.ImageFolder = "image";
            epub.Structure.Directories.CssFolder = "css";
            epub.DirectorySearchOption = SearchOption.AllDirectories;
            epub.BuildToFile(sExportPath + txtFullName.Text + ".epub");

            //reset the cursor
            this.Cursor = Cursors.Default;
            MessageBox.Show("The file is done");
        }

        private static string WrapVerse(string sVerseStart, string sVerseEnd, string s)
        {
            string sVerseNum = "";
            try
            {
                sVerseNum = s.Substring(0, s.IndexOf(" "));
            }
            catch (Exception)
            {
                sVerseNum = s.Trim();
            }

            s = s.Substring(sVerseNum.Length + 1);
            s = sVerseStart + sVerseNum + sVerseEnd + " " + s;
            return s;
        }

        private bool EPUBExportValidation()
        {
            bool bRet = false;

            errorProvider1.Clear();
            if (!lvBooks.Enabled)
            {
                errorProvider1.SetError(lvBooks, "Load in book list");
                bRet = true;
            }
            if (txtFullName.Text == string.Empty)
            {
                errorProvider1.SetError(txtFullName, "Need value");
                bRet = true;
            }
            if (cboEpubLanguage.Text == "Select Language")
            {
                errorProvider1.SetError(cboEpubLanguage, "Need value");
                bRet = true;
            }
            return bRet;
        }

        private void ExportToEPUBFile(string sFileOutName, cBQ_Book oBook, string sLang)
        {
            
            StringBuilder sb = new StringBuilder();
            List<cBQ_Chapter> oChapters = oBook.oChapters;
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\"");
            sb.AppendLine("\"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"" + sLang+ "\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"application/xhtml+xml; charset=utf-8\"/>");
            sb.AppendLine("<link rel=\"stylesheet\" href=\"css\\common.css\" type=\"text/css\"/>");
            sb.AppendLine("<title>" + oBook.sBookName + "</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.Append("<div class=\"chapterJump\">");//jump to chapter
            //add chapter links in the beginning of the book
            for (int i = 1; i < oChapters.Count; i++)
            {
                sb.Append("<a href=\"#chapter" + i + "\">" + i + "</a> ");
            }
            sb.Append("</div>");
            sb.AppendLine("<h1>" + oBook.sBookName + "</h1>");

            string sChapTagStart = txtEPUBChapterSign.Text;
            string sChapTagEnd = "</" + txtEPUBChapterSign.Text.Substring(1);            

            //process the chapters and verses
            foreach (cBQ_Chapter c in oChapters)
            {
                //chapter number header
                string chNumber = c.sChapterNumber;
                if (chNumber == "0")
                {
                    if (c.Verses.Count>0)
                    {
                        sb.AppendLine(sChapTagStart + "<a id=\"chapter" + c.sChapterNumber + "\"></a>" + txtIntroName.Text +  sChapTagEnd);
                    }                    
                }
                else
                {
                    sb.AppendLine(sChapTagStart + "<a id=\"chapter" + c.sChapterNumber + "\"></a>" + oBook.sBookName + " " + c.sChapterNumber + sChapTagEnd);
                }
                

                //append each verse
                ArrayList oVerse = c.Verses;

                for (int i = 0; i < oVerse.Count; i++)
                {
                    if (c.sChapterNumber == "0")
                    {
                        //chapter information section
                        sb.AppendLine(oVerse[i].ToString());
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
                                //this is a section header \s or referece \r
                                sb.AppendLine(sTmp.Substring(2));
                            }
                            else
                            {
                                //when it's a normal verse
                                try
                                {
                                    sb.AppendLine(sTmp);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("ERROR: " + ex.Message);
                                }
                            }
                        }


                    }
                    
                }
                //insert footnotes at the end of each chapter                
                ArrayList oFtnote = c.Footnotes;
                if (oFtnote.Count > 0)
                {
                    sb.AppendLine("<hr></hr>");
                }
                for (int i = 0; i < oFtnote.Count; i++)
                {
                    string sFTmp = oFtnote[i].ToString().Trim();                    
                    sb.AppendLine("<div class=\"ftnt\">" + sFTmp +"</div>");
                }

                //insert cross references at the end of each chapter                
                ArrayList oCrossRefs = c.Crossrefs;
                if (oCrossRefs.Count > 0)
                {
                    sb.AppendLine("<hr></hr>");
                }
                for (int i = 0; i < oCrossRefs.Count; i++)
                {
                    string sCTmp = oCrossRefs[i].ToString().Trim();
                    sb.AppendLine("<div class=\"ftnt\">" + sCTmp + "</div>");
                }
            }
            //closing tags
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            //write out the BQ page to a UTF8 temp file
            using (StreamWriter sw = new StreamWriter(sFileOutName, false, Encoding.UTF8))
            {
                sw.Write(sb);
                sw.Flush();
                sw.Close();
            }
        }

        private void CreateEpubTitlePage(string filePath, string sLang)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.1//EN\"");
            sb.AppendLine("\"http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd\">");
            sb.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"" + sLang + "\">");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta http-equiv=\"Content-Type\" content=\"application/xhtml+xml; charset=utf-8\"/>");
            sb.AppendLine("<link rel=\"stylesheet\" href=\"css\\common.css\" type=\"text/css\"/>");
            sb.AppendLine(txtTitlePageInfo.Text);
            //closing tags
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");
            //write out the BQ page to a UTF8 temp file
            using (StreamWriter sw = new StreamWriter(filePath + "00title.html", false, Encoding.UTF8))
            {
                sw.Write(sb);
                sw.Flush();
                sw.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void cmdSetBookOrder_Click(object sender, EventArgs e)
        {
            frmSetBookOrder f = new frmSetBookOrder(this);
            f.Show();
        }

        private void cmdLoadBookOrder_Click(object sender, EventArgs e)
        {
            //find out if \usfm_book_order.....txt file exists in the directory specified in txtDir \
            //and load file order into the DragNDropListView1
            //if not, provide instructions to create it and load it into the listview
            lvBooks.Items.Clear();
            lvBooks.Enabled = true;
            if (txtDir.Text != "")
            {
                if (System.IO.File.Exists(txtDir.Text + @"\usfm_book_order." + cboExt.Text + ".txt"))
                {
                    lblInstructions.Text = "";
                    label23.BringToFront();
                    label23.Text = "Reorder the books into the correct sequence, select the books you want to export if needed by clicking check boxes";
                    int iCount = 1;
                    //populate ListView1 using txt tile
                    //open each book up and get the bookname (ie. Genesis)
                    FileStream file = new FileStream(txtDir.Text + @"\usfm_book_order." + cboExt.Text + ".txt", FileMode.OpenOrCreate, FileAccess.Read);

                    //Set Codepage
                    StreamReader sr;
                    string sEncoding = this.USFM_FilesEncoding;
                    if (sEncoding == "")
                    {
                        sr = new StreamReader(file, Encoding.UTF8, false);
                    }
                    else
                    {
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
                        if (line != null && !line.StartsWith("--"))
                        {
                            //add to listview
                            System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem();
                            li.Text = iCount.ToString();
                            li.SubItems.Add(line.Substring(0, line.IndexOf("|")));
                            li.SubItems.Add(line.Substring(line.IndexOf("|") + 1));
                            lvBooks.Items.Add(li);
                            iCount++;
                            //break;
                        }
                    } while (line != null);
                    sr.Close();
                }
                else
                {
                    label23.Text = "";
                    lblInstructions.BringToFront();
                    lblInstructions.Text = "Attention: usfm_book_order." + cboExt.Text + ".txt file does not exist in this location for this file type. Please click [Set Book Order] button above to check book order and create this file. Then click this button again. Or check if you've selected correct file extension.";
                }
                
            }
        }

        private void lvBooks_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column.ToString() == "0")
            {
                try
                {
                    if (lvBooks.Items[0].Checked == false)
                    {
                        columnHeader3.Text = "Unselect All";
                        foreach (ListViewItem item in lvBooks.Items)
                        {
                            item.Checked = true;
                        }
                    }
                    else
                    {
                        columnHeader3.Text = "Select All";
                        foreach (ListViewItem item in lvBooks.Items)
                        {
                            item.Checked = false;
                        }
                    }
                }
                catch (Exception)
                {
                    
                }
                
                
            }
        }




    }
}
