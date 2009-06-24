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
using System.Xml;

namespace GBC_USFM_Preprocessor
{
    public partial class frmCharReplacer : Form
    {
        frmMain _fMain = null;
        DataTable _dt = new DataTable("charHTML");

        public DataTable charHTMLdatatable
        {
            get { return _dt; }
            set { _dt = value; }
        }
        public frmCharReplacer(frmMain f)
        {
            //save a ref back to the main form
            _fMain = f;
            //save a ref to this form in the main form
            _fMain.fCharForm = this;
            InitializeComponent();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            //check to make sure that they really want to do this
            if (MessageBox.Show("Are you sure you want to clear everything?", "Clear Grid", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }
            _dt.Rows.Clear();
            gFonts.Refresh();
        }

        private void cmdOpenFile_Click(object sender, EventArgs e)
        {
            //check to make sure that the user actually picked something
            openFileDialog1.Filter = "Filter Files|*.xml|All Files|*.*";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            List<cCharReplacements> oChars = ReadXMLCharFile(openFileDialog1.FileName);

            //use list to add new rows to the datatable
            foreach (cCharReplacements oC in oChars)
            {
                //add the data to the dataset
                DataRow dr = _dt.NewRow();
                dr[0] = oC.letter;
                dr[1] = "&" + oC.htmlcode;
                _dt.Rows.Add(dr);
            }
            gFonts.Refresh();
        }

        /// <summary>
        /// Read in the XML Char/HTML replacement file
        /// </summary>
        /// <param name="sFilename">filename path</param>
        /// <returns>list of cCharReplacements</returns>
        private List<cCharReplacements> ReadXMLCharFile(string sFilename)
        {
            List<cCharReplacements> oChars = new List<cCharReplacements>();
            //check to see if the file exists
            if (! System.IO.File.Exists(sFilename))
            {
                return oChars;
            }

            //read in the xml document
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                //see if file exists
                xmlDoc.Load(sFilename);
            }
            catch (Exception)
            {
                return oChars;
            }

            //start reading the file in
            XmlTextReader reader = new XmlTextReader(sFilename);
            cCharReplacements oChar = new cCharReplacements("", "");
            while (reader.Read())
            {
                string sNodeName = "";
                switch (reader.NodeType)
                {
                    case XmlNodeType.XmlDeclaration: //starting xml line
                        break;
                    case XmlNodeType.Whitespace: //line end or whitespace
                        break;
                    case XmlNodeType.Element: // The node is an element.
                        sNodeName = reader.Name.ToString();
                        switch (sNodeName)
                        {
                            case "htmlcodes":
                                break;
                            case "item":
                                oChar = new cCharReplacements("", "");
                                break;
                            case "schar":
                                reader.Read();
                                oChar.letter = reader.Value.ToString();
                                break;
                            case "code":
                                reader.Read();
                                oChar.htmlcode = reader.Value.ToString();
                                break;
                            default:
                                break;
                        }

                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        //based upon the tag, do the correct action
                        sNodeName = reader.Name.ToString();
                        switch (sNodeName)
                        {
                            case "schar":
                                oChar.letter = reader.Value.ToString();
                                break;
                            case "code":
                                oChar.htmlcode = reader.Value.ToString();
                                break;
                            default:
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        //add this element to collection
                        if (reader.Name == "item")
                        {
                            //add the data to the dataset
                            oChars.Add(oChar);
                        }
                        break;
                }
            }
            return oChars;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCharReplacer_Load(object sender, EventArgs e)
        {
            //create datatable for grid
            DataColumn dc;
            //Add columns
            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "Character";
            dc.AutoIncrement = false;
            dc.ReadOnly = false;
            dc.Unique = false;
            _dt.Columns.Add(dc);

            dc = new DataColumn();
            dc.DataType = typeof(string);
            dc.ColumnName = "HTLM Code";
            dc.AutoIncrement = false;
            dc.ReadOnly = false;
            dc.Unique = false;
            // Add the Column to the DataColumnCollection.
            _dt.Columns.Add(dc);

            gFonts.DataSource = _dt;
        }

        private void cmdSaveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "xml";
            saveFileDialog1.Filter = "XML file (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.Title = "Where do you want to save the file?";
            // Show the dialog and process the result
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //write out the XML codes
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog1.FileName))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine("<htmlcodes>");
                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        sw.WriteLine("\t<item>");
                        sw.WriteLine("\t\t<schar>" + _dt.Rows[i][0].ToString() + "</schar>");
                        //rip off the '&' character if present
                        string s = _dt.Rows[i][1].ToString();
                        if (s.StartsWith("&"))
                        {
                            s = s.Substring(1);
                        }
                        sw.WriteLine("\t\t<code>" + s + "</code>");
                        sw.WriteLine("\t</item>");
                    }
                    sw.WriteLine("</htmlcodes>");
                }

            }
        }

        private void frmCharReplacer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //pass back the datatable to the main form
            _fMain.charHTMLdatatable = _dt;
        }
    }


}
