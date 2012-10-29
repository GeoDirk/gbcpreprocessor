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
using System.IO;

namespace GBC_USFM_Preprocessor
{
    /// <summary>
    /// Set of utilities that are used for processing out the standard USFM tags
    /// NOTE: this code should match the JAVA GoBibleCreator class
    /// </summary>
    public class cUSFM_Utilities
    {
        //tags to load in 
        private List<string> _sLineRemovalTags = new List<string>(); //{ "\\cl", "\\cp", "\\cd", "\\qa", "\\s", "\\ms", "\\mte", "\\mt", "\\mr", "\\sr", "\\sp" }
        private List<string> _sSingularNumberTags = new List<string>(); //{ "\\pi", "\\ph" };
        private List<string> _sDoubleTags = new List<string>(); //{ "\\qs", "\\qac", "\\add", "\\dc", "\\nd", "\\ord", "\\pn", "\\qt", "\\sig", "\\sls", "\\tl", "\\em", "\\bd", "\\it", "\\bdit", "\\no", "\\sc" };
        private List<string> _sSingularTags = new List<string>(); //{ "\\pmo", "\\pm", "\\pmc", "\\pmr", "\\mi", "\\nb", "\\cls", "\\pc", "\\pr", "\\qr", "\\qc", "\\pb", "\\b", "\\m", "\\p", "\\z" };
        //the list of markers that this applies to
        //note that order is important here so that \fm is processed
        //before \f
        private List<string> _sDoubleTagsFull = new List<string>(); //{ "\\ca", "\\va", "\\vp", "\\fe", "\\bk", "\\xdc", "\\fdc", "\\fm", "\\fig", "\\ndx", "\\pro", "\\wg", "\\wh", "\\w", "\\x" };

        public enum TagType
        {
            eUnknown,
            eLineRemovalMarker,
            eSingularTag,
            eSingularNumberTag,
            eDoubleTag,
            eDoubleFullTag,
            eUSFMstandardTag
        }

        //constructor call
        public cUSFM_Utilities()
        {
            //load in the tags from the TAG XML file
            LoadTags();
        }

        private void LoadTags()
        {
            //zero out the lists before loading
            _sLineRemovalTags.Clear();
            _sSingularNumberTags.Clear();
            _sDoubleTags.Clear();
            _sSingularTags.Clear();
            _sDoubleTagsFull.Clear();

            TagType tagType = TagType.eUnknown;
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
                            tagType = TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:SINGULAR_NUMBER_TAGS":
                            tagType = TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:SINGULAR_TAGS":
                            tagType = TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:DOUBLE_TAGS":
                            tagType = TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:DOUBLE_FULL_TAGS":
                            tagType = TagType.eLineRemovalMarker;
                            break;
                        case "SECTION:LIST_OF_STANDARD_USFM_TAGS":
                            tagType = TagType.eUSFMstandardTag;
                            break;
                        default:
                            //process the following tag
                            if (line != "")
                            {
                                switch (tagType)
                                {
                                    case TagType.eUnknown:
                                        break;
                                    case TagType.eLineRemovalMarker:
                                        _sLineRemovalTags.Add(line);
                                        break;
                                    case TagType.eSingularNumberTag:
                                        _sSingularNumberTags.Add(line);
                                        break;
                                    case TagType.eSingularTag:
                                        _sSingularTags.Add(line);
                                        break;
                                    case TagType.eDoubleTag:
                                        _sDoubleTags.Add(line);
                                        break;
                                    case TagType.eDoubleFullTag:
                                        _sDoubleTagsFull.Add(line);
                                        break;
                                    case TagType.eUSFMstandardTag:
                                        //do nothing
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
        }


        /*
         * Check to see if the line starts with certain
         * tags that allow us to delete the entire line
         * like '\cl A'
         * 
         * Return 'true' if we keep
         */
        public bool CheckIfWeKeepLine(string sTmp)
        {	
            bool bKeep = true;
            //loop through the markers removing them all from the text
            for (int i = 0; i < _sLineRemovalTags.Count; i++)
            {
                if (sTmp.StartsWith(_sLineRemovalTags[i]))
                {
                    return false;
                }
            }
            return bKeep;
        }


        /*
         * removes the preceeding chapter header information
         * prior to the start of the first verse
         */
        public static string RemoveChapterHeaders(string sVerse)
        {
            //find the first verse and remove everything before that
            int iStart = sVerse.IndexOf("\\v");
            sVerse = sVerse.Substring(iStart);
            return sVerse;
        }

        /*
         * This function takes a string and removes the USFM markers
         * which are singular (do not have and end tag).  Example
         * of this is the paragraph tag \p which is not needed
         */
        public string RemoveSingularMarkerTags(string sTmp)
        {		
            //for ones that are a tag and number '\q1'
            for (int i = 0; i < _sSingularNumberTags.Count; i++)
            {
                //replace tag with trailing number (trailing blank remove)
                //sTmp = sTmp.replaceAll("\\" + sTagNum[i] + "+[0-9] ", "");
                //replace tag with trailing number
                sTmp = sTmp.Replace("\\" + _sSingularNumberTags[i] + "+[0-9]", "");
                //replace tag without trailing number (trailing blank remove)
                sTmp = sTmp.Replace("\\" + _sSingularNumberTags[i] + " ", "");
                //replace tag without trailing number
                sTmp = sTmp.Replace("\\" + _sSingularNumberTags[i], "");
            }
            //the list of markers that this applies to
            //NOTE: you need four '\' to account for the \p string
            //as this is a regex command
            
            //loop through the markers removing them all from the text
            for (int i = 0; i < _sSingularTags.Count; i++)
            {
                //replace the marker tag with an empty string (trailing blank remove)
                //sTmp = sTmp.replaceAll("\\" + sMarker[i] + " ", "");
                //replace the marker tag with an empty string
                sTmp = sTmp.Replace("\\" + _sSingularTags[i], "");
            }
            
            //for ones that do not start with the slash
            string [] _sSpecial = {"!\\$", "//"};  //todo - check this in .NET as this is a Java regex
            for (int i = 0; i < _sSpecial.Length; i++)
            {
                //replace the marker tag with an empty string (trailing blank remove)
                sTmp = sTmp.Replace(_sSpecial[i] + " ", "");
                //replace the marker tag with an empty string
                sTmp = sTmp.Replace(_sSpecial[i], "");
            }
            
            //for ones that are a tag and number '\q1'
            string [] _sTagNum2 = {"\\li", "\\qm", "\\q"};
            for (int i = 0; i < _sTagNum2.Length; i++)
            {
                //replace tag with trailing number (trailing blank remove)
                //sTmp = sTmp.replaceAll("\\" + sTagNum2[i] + "+[0-9] ", "");
                //replace tag with trailing number
                sTmp = sTmp.Replace("\\" + _sTagNum2[i] + "+[0-9]", "");
                //replace tag without trailing number (trailing blank remove)
                //sTmp = sTmp.replaceAll("\\" + sTagNum2[i] + " ", "");
                //replace tag without trailing number
                //sTmp = sTmp.replaceAll("\\" + sTagNum2[i], "");
            }
            
            return sTmp;
        }

        /*
         * This function takes a string and removes the USFM markers
         * which are double (have start and end tags).  Example
         * of this is the bold tag \bd...\bd* in which we want to 
         * remove the tags only but keep the text between
         */
        public string RemoveDoubleMarkerTags(string sTmp)
        {		
            //loop through the markers removing them all from the text
            for (int i = 0; i < _sDoubleTags.Count; i++)
            {
                sTmp = sTmp.Replace("\\" + _sDoubleTags[i] + "\\*", "");
                //remove trailing blank from tag
                sTmp = sTmp.Replace("\\" + _sDoubleTags[i] + " ", "");                   
                sTmp = sTmp.Replace("\\" + _sDoubleTags[i], "");                   
            }
            return sTmp;
        }

        /*
         * This funtion finds the USFM marker for words of Jesus
         * and replaces them with the red lettering tag
         */
        public static string ReplaceWordsOfJesus(string sTmp, char sWJ, bool useRedLettering)
        {
            //replace the marker tag with the red lettering tag
            //NOTE: you must delete the * ending tag first before the
            //start tag otherwise you end up with only the * left
            if (useRedLettering)
            {
                //insert the red lettering character
                sTmp = sTmp.Replace("\\\\wj\\*", sWJ.ToString());
                sTmp = sTmp.Replace("\\\\wj", sWJ.ToString());
            }
            else
            {
                //just dump the tags
                sTmp = sTmp.Replace("\\\\wj\\*", "");
                sTmp = sTmp.Replace("\\\\wj", "");
            }
            return sTmp;
        }

        /*
         * This function takes a string and removes the USFM markers
         * which are double (have start and end tags).  Example
         * of this is the footnote tag \f...\f* in which we want to 
         * remove the tags and all the text between
         */
        public string RemoveDoubleMarkerTagsFull(string sTmp)
        {		
            //loop through the markers removing them all from the text
            for (int i = 0; i < _sDoubleTagsFull.Count; i++)
            {
                //find if the start tag is found
                while (sTmp.IndexOf(_sDoubleTagsFull[i]) != -1) 
                {
                    int iStart = sTmp.IndexOf(_sDoubleTagsFull[i]);
                    int iEnd = sTmp.IndexOf(_sDoubleTagsFull[i] + "*");
                    if (iStart != -1 && iEnd != -1)
                    {
                        //replace the marker tag with an empty string
                        String sFirstPart = sTmp.Substring(0, iStart);
                        String sSecondPart = sTmp.Substring(iEnd + _sDoubleTagsFull[i].Length + 1);
                        //insert a space if needed here
    //                    if(sFirstPart.endsWith(" ") || sSecondPart.startsWith(" "))
    //                    {
    //                        //blank exists between parts
    //                        sTmp = sFirstPart + sSecondPart;
    //                    }
    //                    else
    //                    {
    //                        //slap a blank between the parts
    //                        sTmp = sFirstPart + " " + sSecondPart;
    //                    }
                        sTmp = sFirstPart + sSecondPart;
                    }
                    else
                    {
                        //keep this from going into an endless loop
                        //if the end tag is missing
                        break;
                    }
                }
            }

            return sTmp;
        }

        /*
         * Drop the verse number from the start of the verse string
         */
        public string RemoveVerseNumbering(string sVerse, ref bool bDDTag)
        {
            //find the first verse and remove everything before that
            int iStart = sVerse.IndexOf(" ");
            if (iStart == -1)
            {
                //there is no space after the verse number which happens when
                //there are multiline verses and somebody is putting in blank
                //verse lines
                if (sVerse.Substring(0, 3) == "\\pi")
                {
                    sVerse = sVerse.Replace("\\pi", "<blockquote> ");
                }
                else if (sVerse.Substring(0, 2) == "\\p" || sVerse.Substring(0, 3) == "\\q1" || sVerse.Substring(0, 3) == "\\q2" || sVerse.Substring(0, 3) == "\\q3")
                {
                    //catch an empty p tag line or li or q tag
                    //because the next line should be indented
                    sVerse = "p";
                }
                else if (sVerse.Substring(0, 4) == "\\li1")
                {
                    sVerse = "<dt>";
                }
                else if (sVerse.Substring(0, 4) == "\\li2")
                {
                    sVerse = "<dd>";
                    
                }
                else
                {
                    sVerse = "";
                }
                
            }
            //plop in sup if first tag is not \\v
            //so that the first word doesn't look funny

            //if if's a paragraph start, or a quote1 or a list of the 1st level, add first level of indentation
            else if (sVerse.Substring(0, iStart) == "\\p" || sVerse.Substring(0, iStart) == "\\q1")
            {
                //this will also indent any dialogs, paragraph beginnings or lists
                sVerse = " &nbsp;&nbsp;&nbsp;" + sVerse.Substring(iStart);
            }
            //if it's a list or a quote of the second level add second level of indentation
            else if (sVerse.Substring(0, iStart) == "\\q2")
            {
                //this will also indent any dialogs, paragraph beginnings or lists
                sVerse = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + sVerse.Substring(iStart);
            }
            //if it's a quote of the third level - add third level of indentation
            else if (sVerse.Substring(0, iStart) == "\\q3")
            {
                //this will also indent any dialogs, paragraph beginnings or lists
                sVerse = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + sVerse.Substring(iStart);
            }
            else if (sVerse.Substring(0, iStart) == "\\li1")
            {
                sVerse = " <dt>" + sVerse.Substring(iStart) + "</dt>";
            }
            else if (sVerse.Substring(0, iStart) == "\\li2")
            {
                sVerse = " <dd>" + sVerse.Substring(iStart);
                bDDTag = true;
            }
            //if it only has a footnote or a selah then do nothing
            else if (sVerse.Substring(0, iStart) == "\\f" || sVerse.Substring(0, iStart) == "\\qs" || sVerse.Substring(0, iStart) == "\\pi")
            {
                //do nothing if the line starts with a footnote
            }
            //if it's something else (mostly all verse number \v tags and \s tags) 
            //just get rid of it including the white space after it
            else
            {
                sVerse = sVerse.Substring(iStart + 1);
            }
            return sVerse;
        }


        /*
         * Drop the verse number from the start of the verse string
         */
        public string RemoveVerseNumberingEPUB(string sVerse)
        {
            //find the first verse and remove everything before that
            int iStart = sVerse.IndexOf(" ");
            if (iStart == -1)
            {
                //there is no space after the verse number which happens when
                //there are multiline verses and somebody is putting in blank
                //verse lines
                if (sVerse.Substring(0, 3) == "\\pi")
                {
                    sVerse = sVerse.Replace("\\pi", "<blockquote> ");
                }
                else if (sVerse.Substring(0, 2) == "\\p" || sVerse.Substring(0, 3) == "\\q1" || sVerse.Substring(0, 3) == "\\q2" || sVerse.Substring(0, 3) == "\\q3")
                {
                    //catch an empty p tag line or li or q tag
                    //because the next line should be indented
                    sVerse = "p";
                }
                else if (sVerse.Substring(0, 4) == "\\li1")
                {
                    sVerse = "<p class=\"speech\">";
                }
                else if (sVerse.Substring(0, 4) == "\\li2")
                {
                    sVerse = "<p class=\"speech2\">";
                }
                else
                {
                    sVerse = "";
                }

            }
            //plop in sup if first tag is not \\v
            //so that the first word doesn't look funny

            //if if's a paragraph start, or a quote1 or a list of the 1st level, add first level of indentation
            else if (sVerse.Substring(0, iStart) == "\\p" || sVerse.Substring(0, iStart) == "\\q1")
            {
                //this will also indent any dialogs, paragraph beginnings or lists
                sVerse = "<p class=\"speech\">" + sVerse.Substring(iStart).Trim() + "</p>";
            }
            //if it's a list or a quote of the second level add second level of indentation
            else if (sVerse.Substring(0, iStart) == "\\q2")
            {
                //this will also indent any dialogs, paragraph beginnings or lists
                sVerse = "<p class=\"speech2\">" + sVerse.Substring(iStart) + "</p>";
            }
            //if it's a list or a quote of the second level add second level of indentation
            else if (sVerse.Substring(0, iStart) == "\\pi")
            {
                //this will also indent any dialogs, paragraph beginnings or lists
                sVerse = "<blockquote><p>" + sVerse.Substring(iStart).Trim() + "</p>";
            }
            //if it's a quote of the third level - add third level of indentation
            else if (sVerse.Substring(0, iStart) == "\\q3")
            {
                //this will also indent any dialogs, paragraph beginnings or lists
                //sVerse = " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + sVerse.Substring(iStart);
                sVerse = sVerse.Substring(iStart);
            }
            else if (sVerse.Substring(0, iStart) == "\\li1")
            {
                //sVerse = " <dt>" + sVerse.Substring(iStart) + "</dt>";
                sVerse = "<p class=\"speech\">" + sVerse.Substring(iStart) + "</p>";
            }
            else if (sVerse.Substring(0, iStart) == "\\li2")
            {
                //sVerse = " <dd>" + sVerse.Substring(iStart);
                sVerse = "<p class=\"speech2\">" + sVerse.Substring(iStart) + "</p>";
                //bDDTag = true;
            }
            //if it only has a footnote or a selah then do nothing
            else if (sVerse.Substring(0, iStart) == "\\f" || sVerse.Substring(0, iStart) == "\\qs" || sVerse.Substring(0, iStart) == "\\pi")
            {
                //do nothing if the line starts with a footnote
            }
            else if (sVerse.Substring(0, iStart) == "\\restore")
            {
                sVerse = ""; 
            }
            //if it's something else (mostly all verse number \v tags and \s tags) 
            //just get rid of it including the white space after it
            else
            {
                sVerse = sVerse.Substring(iStart + 1);
            }
            return sVerse;
        }
        

        public string ProcessOtherTags(string sTmp)
        {
            //deal with footnote tags
            sTmp = sTmp.Replace("\\f*", "</n>");
            sTmp = sTmp.Replace("\\f +", "<n>");
            sTmp = sTmp.Replace("\\f -", "<n>");
            sTmp = sTmp.Replace("\\f ?", "<n>");
            sTmp = sTmp.Replace("\\f *", "</n>");
            //sTmp = sTmp.Replace("\\fr*", "</b>");
            sTmp = sTmp.Replace("\\fr ", "<b>");
            sTmp = sTmp.Replace("\\fr", "<b>");
            sTmp = sTmp.Replace("\\fv*", "</sup> ");
            sTmp = sTmp.Replace("\\fv ", "<sup>");
            sTmp = sTmp.Replace("\\ft ", "");
            sTmp = sTmp.Replace("\\bdit*", "</i></b>");
            sTmp = sTmp.Replace("\\bdit", "<b><i>(");
            //cross references
            sTmp = sTmp.Replace("\\x*", "</c>");
            sTmp = sTmp.Replace("\\x +", "<c>");
            sTmp = sTmp.Replace("\\x -", "<c>");
            sTmp = sTmp.Replace("\\x ?", "<c>");
            sTmp = sTmp.Replace("\\x *", "</c>");
            sTmp = sTmp.Replace("\\xo ", "<b>");
            sTmp = sTmp.Replace("\\xo", "<b>");
            sTmp = sTmp.Replace("\\xt ", "");
            //deal with translator's additions
            sTmp = sTmp.Replace(" \\add* ", "</i>");
            sTmp = sTmp.Replace("\\add ", "<i>");
            //deal with transliterations
            sTmp = sTmp.Replace("\\tl*", "</i>");
            sTmp = sTmp.Replace("\\tl ", "<i>");
            //deal with Selah's that are located only in Psalms
            sTmp = sTmp.Replace("\\qs*", "</i>");
            sTmp = sTmp.Replace("\\qs", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>");
            sTmp = sTmp.Replace("\\pi ", "<blockquote> ");
            sTmp = sTmp.Replace("\\w*", "</i>");
            sTmp = sTmp.Replace("\\w ", "<i>");
            return sTmp;
        }


        
    }
}
