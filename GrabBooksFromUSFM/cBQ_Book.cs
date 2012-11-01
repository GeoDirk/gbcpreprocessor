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

namespace GBC_USFM_Preprocessor
{
    /// <summary>
    /// Structure to hold one book of the bible
    /// </summary>
    public class cBQ_Book
    {
        private string _sBookName = "";
        private List<cBQ_Chapter> _oChapters = new List<cBQ_Chapter>();
        private string _sChLabels = "";

        public string sChLabels
        {
            get { return _sChLabels; }
            set { _sChLabels = value; }
        }

        /// <summary>
        /// Set/Get the book chapters
        /// </summary>
        public List<cBQ_Chapter> oChapters
        {
            get { return _oChapters; }
            set { _oChapters = value; }
        }

        /// <summary>
        /// Set/Get the book's name (i.e., Mark, Luke, Psalms...)
        /// </summary>
        public string sBookName
        {
            get { return _sBookName; }
            set { _sBookName = value; }
        }

        /// <summary>
        /// Return the number of Chapters in the book
        /// </summary>
        public int GetNumberOfChapters
        {
            get { return _oChapters.Count; }
        }

        /// <summary>
        /// Add a new chapter to the book
        /// </summary>
        /// <param name="c"></param>
        public void AddChapter(cBQ_Chapter c)
        {
            _oChapters.Add(c);
        }
    }

    /// <summary>
    /// Structure to hold one chapter's worth of verses
    /// </summary>
    public class cBQ_Chapter
    {
        private ArrayList _verses = new ArrayList();
        private string _sChapterNumber = "";
        private ArrayList _footnotes = new ArrayList();
        private ArrayList _crossrefs = new ArrayList();

        public ArrayList Crossrefs
        {
            get { return _crossrefs; }
            set { _crossrefs = value; }
        }

        public ArrayList Footnotes
        {
            get { return _footnotes; }
            set { _footnotes = value; }
        }

        //class initilizer
        public cBQ_Chapter(int iChapterNum)
        {
            _sChapterNumber = iChapterNum.ToString();
        }

        /// <summary>
        /// The chapter number
        /// </summary>
        public string sChapterNumber
        {
            get { return _sChapterNumber; }
            set { _sChapterNumber = value; }
        }

        /// <summary>
        /// set/get the arraylist of verses for this chapter
        /// </summary>
        public ArrayList Verses
        {
          get { return _verses; }
          set { _verses = value; }
        }

        /// <summary>
        /// return the verse count for this chapter
        /// </summary>
        public int VerseCount
        {
            get { return _verses.Count; }
        }

        /// <summary>
        /// Add in a new verse
        /// </summary>
        /// <param name="s"></param>
        public void AddVerse (string s)
        {
            _verses.Add(s);
        }
        /// <summary>
        /// Add in a new footnote
        /// //find if the verse has a footnote, strip it out, and return a clean verse
        /// </summary>
        /// <param name="s"></param>
        public string AddFootnote(string s, ref int fnum)
        {
            string sVerse = "";
            string sFootnt = "";
            if (s.Contains("<n>"))
            {
                do
                {
                    sVerse = s.Substring(0, s.IndexOf("<n>"));
                    s = s.Substring(s.IndexOf("<n>") + 4);
                    sVerse = sVerse + "<ftnt>" + s.Substring(s.IndexOf("</n>") + 4);
                    sFootnt = "<p>" + s.Substring(0, s.IndexOf("</n>"))+ "</p>";
                    //find place to insert closing </b> tag
                    string sTmp = sFootnt.Substring(6);
                    int iIns = sTmp.IndexOf(" ") + 6;
                    sFootnt = sFootnt.Insert(iIns, "</b>");
                    
                    //replace <ftnt> with the link to footnote
                    //find the position of text between <b> tags in the sFootnt
                    sTmp = sFootnt.Substring(sFootnt.IndexOf("b>") + 2);
                    sTmp = sTmp.Substring(0, sTmp.IndexOf("</b"));

                    sVerse = sVerse.Replace("<ftnt>", "<a id=\"vrs" + sTmp + "\"></a><a href=\"#ftnt" + sTmp + "\"><b> <sup>" + fnum + "</sup></b></a>");
                    s = sVerse;
                    //add anchor and link to footnote
                    sFootnt = sFootnt.Replace("</b>", "</a></b><a id=\"ftnt" + sTmp + "\"></a>");
                    sFootnt = sFootnt.Replace("<b>", "<a href=\"#vrs" + sTmp + "\"><sup>" + fnum + "</sup> ");
                    _footnotes.Add(sFootnt);
                    fnum++;
                } while (s.Contains("<n>"));
            }
            else
            {
                sVerse = s;
            }
            
            return sVerse;
        }

        public string AddCrossRef(string s, ref int ltr, int chnum)
        {
            string sVerse = "";
            string sCrsrf = "";
            if (s.Contains("<c>"))
            {
                do
                {
                    sVerse = s.Substring(0, s.IndexOf("<c>"));
                    s = s.Substring(s.IndexOf("<c>") + 4);
                    sVerse = sVerse + "<crltr>" + s.Substring(s.IndexOf("</c>") + 5);
                    sCrsrf = "<p>" + s.Substring(0, s.IndexOf("</c>")) + "</p>";
                    //find place to insert closing </b> tag
                    string sTmp = sCrsrf.Substring(6);                    
                    int iIns = sTmp.IndexOf(" ") + 6;
                    sCrsrf = sCrsrf.Insert(iIns, "</a></b><a id=\"crrf" + chnum.ToString() + Convert.ToChar(ltr).ToString() + "\"></a>");
                    sCrsrf = sCrsrf.Insert(6, "<a href=\"#vrs" + chnum.ToString() + Convert.ToChar(ltr).ToString() + "\"><sup>" + Convert.ToChar(ltr).ToString() + "</sup> ");
                    _crossrefs.Add(sCrsrf);
                    //replace <crltr> with the link to footnote
                    sVerse = sVerse.Replace("<crltr>", "<a id=\"vrs" + chnum.ToString() + Convert.ToChar(ltr).ToString() + "\"></a><a href=\"#crrf" + chnum.ToString() + Convert.ToChar(ltr).ToString().ToLower() + "\"><sup>" + Convert.ToChar(ltr).ToString() + "</sup></a>");
                    ltr++;
                    if (ltr > 97+25)
                    {
                        ltr = 65;
                    }
                    s = sVerse;
                } while (s.Contains("<c>"));
            }
            else
            {
                sVerse = s;
            }

            return sVerse;
        }
    }


    

    /// <summary>
    /// Structure to hold the BibleQuotes INI file
    /// </summary>
    public class cBQ_IniStructure
    {
        private string _PathName = "";
        private string _FullName = "";
        private string _HTMLName = "";

        public string HTMLName
        {
            get { return _HTMLName; }
            set { _HTMLName = value; }
        }
        private string _ShortName = "";
        private int _ChapterQty = 0;

        public string PathName
        {
            get { return _PathName; }
            set { _PathName = value; }
        }

        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        public string ShortName
        {
            get { return _ShortName; }
            set { _ShortName = value; }
        }

        public int ChapterQty
        {
            get { return _ChapterQty; }
            set { _ChapterQty = value; }
        }
    }


}
