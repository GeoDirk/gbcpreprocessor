using System;
using System.Collections.Generic;
using System.Text;

namespace GBC_USFM_Preprocessor
{
    public class cVerseHolder
    {
        private string _sVerse = "";
        private string _sVerseNum = "";
        private string _sExtraText = "";


        public string sVerse
        {
            get { return _sVerse; }
            set { _sVerse = value; }
        }

        public string sVerseNum
        {
            get { return _sVerseNum; }
            set { _sVerseNum = value; }
        }

        public string sExtraText
        {
            get { return _sExtraText; }
            set { _sExtraText = value; }
        }

        public static List<cVerseHolder> CheckForVersification(cVerseHolder vhFirst, cVerseHolder vhSecond, ref List<cVersification> oVL, string sFilename, string sChapter, string sReplacement, bool bVerseRangeNotification)
        {
            List<cVerseHolder> oVH = new List<cVerseHolder>();
            cVerseHolder vhTemp = new cVerseHolder();
            //check if there is a dash in the verse numbering
            //if so append them all to the oVH list
            //find the difference between the numbers
            int iV1 = -1;
            int iV2 = -1;
            int iDifference = -1;
            if (vhFirst._sVerseNum.IndexOf("-") > -1)
            {
                //find each verse number and the difference
                iV1 = Convert.ToInt16(vhFirst._sVerseNum.Substring(0, vhFirst._sVerseNum.IndexOf("-")));
                iV2 = Convert.ToInt16(vhFirst._sVerseNum.Substring(vhFirst._sVerseNum.IndexOf("-") + 1));
                iDifference = iV2 - iV1;
                //report versification: range of verses
                cVersification oV = new cVersification(sFilename, sChapter, iV1.ToString(), "Verse range detected [" + vhFirst._sVerseNum.ToString() + "]");
                oVL.Add(oV);
                //create a function that will look for the trailing tags only (like \p at the end of the line)
                vhFirst = CheckForTrailingTags(vhFirst);
                //add in first of the range
                vhTemp = new cVerseHolder();
                vhTemp._sVerseNum = iV1.ToString();
                vhTemp._sVerse = vhFirst._sVerse;

                //add in the verse marker on the lead verse
                if (iDifference > 0 && bVerseRangeNotification)
                {
                    {
                        vhTemp._sVerse = "{" + iV1.ToString() + "-" + (iV1 + iDifference).ToString() + "} " + vhTemp._sVerse;
                    }
                }
                oVH.Add(vhTemp);

                //add in verse range verses
                for (int i = 0; i < iDifference; i++)
                {
                    vhTemp = new cVerseHolder();
                    vhTemp._sVerseNum = (iV1 + i + 1).ToString();
                    vhTemp._sVerse = sReplacement;
                    //last verse in the set
                    if (i == iDifference-1)
                    {
                        vhTemp.sExtraText = "\r\n" + vhFirst._sExtraText;
                    }
                    oVH.Add(vhTemp);                  
                }

            }
            else
            {
                //add in vhFirst
                oVH.Add(vhFirst);
            }

            ReportEmptyVerses(vhFirst, oVL, sFilename, sChapter, sReplacement);

            //find the difference in verse numbers between the last element in oVH and vhSecond
            //but first check to make sure that vhSecond doesn't contain a dash in it
            //if so we only want the first number before dash
            string vNum = vhSecond._sVerseNum;
            if (vNum.IndexOf("-") > -1)
            {
                //find each verse number
                vNum = vhSecond._sVerseNum.Substring(0, vhSecond._sVerseNum.IndexOf("-"));
            }
            if (vNum != "" && oVH[oVH.Count - 1]._sVerseNum != "")
            {
                iDifference = Convert.ToInt16(vNum) - Convert.ToInt16(oVH[oVH.Count - 1]._sVerseNum);
                if (iDifference > 1)
                {
                    //todo report versification: missing verses

                    //append verses
                    for (int i = 0; i < iDifference - 1; i++)
                    {
                        vhTemp = new cVerseHolder();
                        vhTemp._sVerseNum = (Convert.ToInt16(vhFirst._sVerseNum) + i + 1).ToString();
                        vhTemp._sVerse = sReplacement;
                        oVH.Add(vhTemp);
                        cVersification oV = new cVersification(sFilename, sChapter, (Convert.ToInt16(vhFirst._sVerseNum) + i + 1).ToString(), "Missing verse(s)");
                        oVL.Add(oV);
                    }
                }
                //this is if the verse number was mistyped and it's less than or the same as the prev verse
                if (iDifference <= 0)
                {
                    cVersification oV = new cVersification(sFilename, sChapter, vhFirst.sVerseNum + "/" + vhSecond.sVerseNum, "PROBLEM: Verse number " + vhSecond.sVerseNum + " is following verse " + vhFirst.sVerseNum);
                    oVL.Add(oV);
                }
            }
            //We don't add the second one in, because we'll do it the next time we run this function
            //then in will come in as the first one

            return oVH;
        }

        private static cVerseHolder CheckForTrailingTags(cVerseHolder vhFirst)
        {
            cVerseHolder vhTemp = new cVerseHolder();
            vhTemp.sVerseNum = vhFirst._sVerseNum;
            string sVerse = "";
            string sExtra = "";
            //split on new lines
            string[] sSplitChar = new string[] { "\r\n" };
            string[] sLines = vhFirst._sVerse.Split(sSplitChar, StringSplitOptions.RemoveEmptyEntries);
            bool bFoundVerse = false;
            //iterate through each line backwards
            for (int i = sLines.Length-1; i >= 0; i--)
            {
                if (bFoundVerse)
                {
                    sVerse = sLines[i] + "\r\n" + sVerse;
                }
                else
                {
                    string s = sLines[i].Trim();
                    string[] sSplitCh = new string[] { " " };
                    string[] sWords = s.Split(sSplitCh, StringSplitOptions.RemoveEmptyEntries);
                    bool bNonTag = false;
                    foreach (string sTemp in sWords)
                    {
                        if (sTemp != String.Empty)
                        {
                            if (!sTemp.StartsWith("\\"))
                            {
                                bNonTag = true;
                                break;
                            }
                        }
                    }
                    if (bNonTag)
                    {
                        //this is a real verse then
                        sVerse = sLines[i];
                        bFoundVerse = true;
                    }
                    else 
                    {
                        //tags only detected
                        sExtra = sLines[i] + "\r\n" + sExtra;
                    }
                    
                }
                
            }

            vhTemp.sVerse = sVerse;
            if (sExtra.Length > 0)
            {
                sExtra = sExtra.Substring(0, sExtra.Length - 2);
                vhTemp.sExtraText = sExtra;
            }

            return vhTemp;
        }

        private static void ReportEmptyVerses(cVerseHolder vhFirst, List<cVersification> oVL, string sFilename, string sChapter, string sReplacement)
        {
            //report empty verses
            if (vhFirst.sVerse == "")
            {
                // report versification: no text in the verse
                if (vhFirst.sExtraText == "")
                {
                    vhFirst.sVerse =  sReplacement + "\r";

                    cVersification oV = new cVersification(sFilename, sChapter, vhFirst.sVerseNum.ToString(), "No actual verse text detected; empty verse");
                    oVL.Add(oV);
                }
                else
                {
                    vhFirst.sExtraText.Trim();
                    vhFirst.sVerse = sReplacement + "\r";
                    cVersification oV = new cVersification(sFilename, sChapter, vhFirst.sVerseNum.ToString(), "No actual verse text detected; only tag information detected [" + vhFirst.sExtraText + "]");
                    oVL.Add(oV);
                }



            }
        }

        public static List<cVerseHolder> CheckLastOneForVersification(cVerseHolder vhFirst, ref List<cVersification> oVL, string sFilename, string sChapter, string sReplacement, bool bVerseRangeNotification)
        {
            List<cVerseHolder> oVH = new List<cVerseHolder>();
            cVerseHolder vhTemp = new cVerseHolder();
            //check if there is a dash in the verse numbering
            //if so append them all to the oVH list
            //find the difference between the numbers
            int iV1 = -1;
            int iV2 = -1;
            int iDifference = -1;
            if (vhFirst._sVerseNum.IndexOf("-") > -1)
            {
                //find each verse number and the difference
                iV1 = Convert.ToInt16(vhFirst._sVerseNum.Substring(0, vhFirst._sVerseNum.IndexOf("-")));
                iV2 = Convert.ToInt16(vhFirst._sVerseNum.Substring(vhFirst._sVerseNum.IndexOf("-") + 1));
                iDifference = iV2 - iV1;
                //todo report versification: range of verses
                cVersification oV = new cVersification(sFilename, sChapter, iV1.ToString(), "Verse range detected [" + vhFirst._sVerseNum.ToString() + "]");
                oVL.Add(oV);
                //add in first of the range
                vhTemp = new cVerseHolder();
                vhTemp._sVerseNum = iV1.ToString();
                vhTemp._sVerse = vhFirst._sVerse;
                vhTemp._sExtraText = vhFirst._sExtraText;

                //add in the verse marker on the lead verse
                if (iDifference > 0 && bVerseRangeNotification)
                {
                    {
                        vhTemp._sVerse = "{" + iV1.ToString() + "-" + (iV1 + iDifference).ToString() + "} " + vhTemp._sVerse;
                    }
                }
                oVH.Add(vhTemp);

                //add in additional verse range verses
                for (int i = 0; i < iDifference; i++)
                {
                    vhTemp = new cVerseHolder();
                    vhTemp._sVerseNum = (iV1 + i + 1).ToString();
                    vhTemp._sVerse = sReplacement;
                    oVH.Add(vhTemp);
                }
            }
            else
            {
                ReportEmptyVerses(vhFirst, oVL, sFilename, sChapter, sReplacement);
                //add in vhFirst
                oVH.Add(vhFirst);
            }
            

            return oVH;

        }
    }
}
