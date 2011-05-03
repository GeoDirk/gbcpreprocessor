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


    }
}
