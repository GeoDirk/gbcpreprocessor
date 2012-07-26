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
using System.Text;
using System.Collections;
using System.IO;

namespace GBC_USFM_Preprocessor
{
    public class cUtils
    {
        /// <summary>
        /// This function will iterate through the files in a directory and obtain
        /// a list of all the various extensions for the filenames
        /// </summary>
        /// <param name="sDir">directory to scan</param>
        /// <returns>list of all the extensions from that directory</returns>
        public static ArrayList GetFileExtensionList(string sDir)
        {
            ArrayList sList = new ArrayList();
            if (sDir == "")
            {
                return sList;
            }

            string[] filePaths = System.IO.Directory.GetFiles(sDir, "*.*");
            foreach (string item in filePaths)
            {
                //pull out the extension information so we can rename
                //the output file differently
                FileInfo fi = new FileInfo(item);
                string sExt = fi.Extension;

                bool bFound = false;
                for (int i = 0; i < sList.Count; i++)
                {
                    //check to see if already in the list
                    if (sExt.ToLower() == sList[i].ToString())
                    {
                        bFound = true;
                        break;
                    }
                }
                if (!bFound)
                {
                    //add the new extension to the list in lower case
                    sList.Add(sExt.ToLower());
                }
            }
            return sList;
        }


        internal static bool IsNumeric(object ObjectToTest)
        {
            if (ObjectToTest == null)
            {
                return false;
            }
            else
            {
                double OutValue;
                return double.TryParse(ObjectToTest.ToString().Trim(),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.CurrentCulture,
                    out OutValue);
            }
        }

        //overload for normal Listview
        internal ArrayList ExtractBookNames(string[] filePaths, ref System.Windows.Forms.ListView lvOutput, string sEncoding, 
            string sBookNameTag, ArrayList sFullClipboardText)
        {
            lvOutput.Items.Clear();
            foreach (string item in filePaths)
            {

                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);

                //Set Codepage
                StreamReader sr;
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
                    if (line != null)
                    {
                        if (line.StartsWith(sBookNameTag + " "))
                        {
                            FileInfo fi = new FileInfo(item.ToString());

                            //add to string
                            sFullClipboardText.Add("//filename:" + fi.Name + "\n");
                            sFullClipboardText.Add("Book: " + line.Substring(sBookNameTag.Length).Trim() + "\n");
                            //add to listview
                            System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem();
                            li.Text = line.Substring(3);
                            li.SubItems.Add(file.Name.Substring(file.Name.LastIndexOf("\\") + 1));
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
            return sFullClipboardText;
        }

        //overload for Drag & Drop Listview
        internal ArrayList ExtractBookNames(string[] filePaths, ref DragNDrop.DragAndDropListView lvOutput, string sEncoding,
            string sBookNameTag, ArrayList sFullClipboardText)
        {
            lvOutput.Items.Clear();
            int iCount = 1;
            foreach (string item in filePaths)
            {

                //open each book up and get the bookname (ie. Genesis)
                FileStream file = new FileStream(item, FileMode.OpenOrCreate, FileAccess.Read);

                //Set Codepage
                StreamReader sr;
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
                    if (line != null)
                    {
                        if (line.StartsWith(sBookNameTag + " "))
                        {
                            FileInfo fi = new FileInfo(item.ToString());

                            //add to string
                            sFullClipboardText.Add("//filename:" + fi.Name + "\n");
                            sFullClipboardText.Add("Book: " + line.Substring(sBookNameTag.Length).Trim() + "\n");
                            //add to listview
                            System.Windows.Forms.ListViewItem li = new System.Windows.Forms.ListViewItem();
                            li.Text = iCount.ToString();
                            li.SubItems.Add(line.Substring(3));
                            li.SubItems.Add(file.Name.Substring(file.Name.LastIndexOf("\\") + 1));
                            lvOutput.Items.Add(li);
                            break;
                        }
                    }
                } while (line != null);
                // Close StreamReader
                sr.Close();
                // Close file
                file.Close();
                iCount++;
            }
            return sFullClipboardText;
        }




        internal void LoadBookOrder(ref DragNDrop.DragAndDropListView lvOutput, string sEncoding, string file)
        {
            lvOutput.Items.Clear();
            if (System.IO.File.Exists(file))
            {
                
            
                //populate ListView1 using txt tile
                int iCount = 1;

                //Set Codepage
                StreamReader sr;
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
                        lvOutput.Items.Add(li);
                        iCount++;
                        //break;
                    }
                } while (line != null);
                sr.Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("File " + file + "doesn't exist");
            }
        }
    }


}
