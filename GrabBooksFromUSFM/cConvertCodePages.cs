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

/*
    some code based upon unlicensed material from Emad Awad
    http://www.codeproject.com/KB/files/CodePage_File_Converter.aspx
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace GBC_USFM_Preprocessor
{
    public class cConvertCodePages
    {
        /// <summary>
        /// Structure to hold a codepage number and description
        /// </summary>
        public struct CodePage
        {
            public int iCodePage;
            public string strName;
            public string strDisplayName;
            public CodePage(int i, string n, string d)
            {
                iCodePage = i;
                strName = n;
                strDisplayName = d;
            }

            public int CodePageNum
            {
                get { return iCodePage; }
            }

            public string DisplayName
            {
                get { return strName + " - " + strDisplayName; }
            }
        }

        /// <summary>
        /// Listing of all the codepages on a Windows system
        /// </summary>
        /// <returns></returns>
        public static CodePage[] ConstructCodePageArray()
        {
            return new CodePage[] { 
                new CodePage(37 	   ,"IBM037"                     ,"IBM EBCDIC (US-Canada)"),
                new CodePage(437       ,"IBM437"                     ,"OEM United States"),
                new CodePage(500       ,"IBM500"                     ,"IBM EBCDIC (International)"),
                new CodePage(708       ,"ASMO-708"                   ,"Arabic (ASMO 708)"),
                new CodePage(720       ,"DOS-720"                    ,"Arabic (DOS)"),
                new CodePage(737       ,"ibm737"                     ,"Greek (DOS)"),
                new CodePage(775       ,"ibm775"                     ,"Baltic (DOS)"),
                new CodePage(850       ,"ibm850"                     ,"Western European (DOS)"),
                new CodePage(852       ,"ibm852"                     ,"Central European (DOS)"),
                new CodePage(855       ,"IBM855"                     ,"OEM Cyrillic"),
                new CodePage(857       ,"ibm857"                     ,"Turkish (DOS)"),
                new CodePage(858       ,"IBM00858"                   ,"OEM Multilingual Latin I"),
                new CodePage(860       ,"IBM860"                     ,"Portuguese (DOS)"),
                new CodePage(861       ,"ibm861"                     ,"Icelandic (DOS)"),
                new CodePage(862       ,"DOS-862"                    ,"Hebrew (DOS)"),
                new CodePage(863       ,"IBM863"                     ,"French Canadian (DOS)"),
                new CodePage(864       ,"IBM864"                     ,"Arabic (864)"),
                new CodePage(865       ,"IBM865"                     ,"Nordic (DOS)"),
                new CodePage(866       ,"cp866"                      ,"Cyrillic (DOS)"),
                new CodePage(869       ,"ibm869"                     ,"Greek, Modern (DOS)"),
                new CodePage(870       ,"IBM870"                     ,"IBM EBCDIC (Multilingual Latin-2)"),
                new CodePage(874       ,"windows-874"                ,"Thai (Windows)"),
                new CodePage(875       ,"cp875"                      ,"IBM EBCDIC (Greek Modern)"),
                new CodePage(932       ,"shift_jis"                  ,"Japanese (Shift-JIS)"),
                new CodePage(936       ,"gb2312"                     ,"Chinese Simplified (GB2312)"),
                new CodePage(949       ,"ks_c_5601-1987"             ,"Korean"),
                new CodePage(950       ,"big5"                       ,"Chinese Traditional (Big5)"),
                new CodePage(1026      ,"IBM1026"                    ,"IBM EBCDIC (Turkish Latin-5)"),
                new CodePage(1047      ,"IBM01047"                   ,"IBM Latin-1"),
                new CodePage(1140      ,"IBM01140"                   ,"IBM EBCDIC (US-Canada-Euro)"),
                new CodePage(1141      ,"IBM01141"                   ,"IBM EBCDIC (Germany-Euro)"),
                new CodePage(1142      ,"IBM01142"                   ,"IBM EBCDIC (Denmark-Norway-Euro)"),
                new CodePage(1143      ,"IBM01143"                   ,"IBM EBCDIC (Finland-Sweden-Euro)"),
                new CodePage(1144      ,"IBM01144"                   ,"IBM EBCDIC (Italy-Euro)"),
                new CodePage(1145      ,"IBM01145"                   ,"IBM EBCDIC (Spain-Euro)"),
                new CodePage(1146      ,"IBM01146"                   ,"IBM EBCDIC (UK-Euro)"),
                new CodePage(1147      ,"IBM01147"                   ,"IBM EBCDIC (France-Euro)"),
                new CodePage(1148      ,"IBM01148"                   ,"IBM EBCDIC (International-Euro)"),
                new CodePage(1149      ,"IBM01149"                   ,"IBM EBCDIC (Icelandic-Euro)"),
                new CodePage(1200      ,"utf-16"                     ,"Unicode"),
                new CodePage(1201      ,"unicodeFFFE"                ,"Unicode (Big-Endian)"),
                new CodePage(1250      ,"windows-1250"               ,"Central European (Windows)"),
                new CodePage(1251      ,"windows-1251"               ,"Cyrillic (Windows)"),
                new CodePage(1252      ,"Windows-1252"               ,"Western European (Windows)"),
                new CodePage(1253      ,"windows-1253"               ,"Greek (Windows)"),
                new CodePage(1254      ,"windows-1254"               ,"Turkish (Windows)"),
                new CodePage(1255      ,"windows-1255"               ,"Hebrew (Windows)"),
                new CodePage(1256      ,"windows-1256"               ,"Arabic (Windows)"),
                new CodePage(1257      ,"windows-1257"               ,"Baltic (Windows)"),
                new CodePage(1258      ,"windows-1258"               ,"Vietnamese (Windows)"),
                new CodePage(1361      ,"Johab"                      ,"Korean (Johab)"),
                new CodePage(10000     ,"macintosh"                  ,"Western European (Mac)"),
                new CodePage(10001     ,"x-mac-japanese"             ,"Japanese (Mac)"),
                new CodePage(10002     ,"x-mac-chinesetrad"          ,"Chinese Traditional (Mac)"),
                new CodePage(10003     ,"x-mac-korean"               ,"Korean (Mac)"),
                new CodePage(10004     ,"x-mac-arabic"               ,"Arabic (Mac)"),
                new CodePage(10005     ,"x-mac-hebrew"               ,"Hebrew (Mac)"),
                new CodePage(10006     ,"x-mac-greek"                ,"Greek (Mac)"),
                new CodePage(10007     ,"x-mac-cyrillic"             ,"Cyrillic (Mac)"),
                new CodePage(10008     ,"x-mac-chinesesimp"          ,"Chinese Simplified (Mac)"),
                new CodePage(10010     ,"x-mac-romanian"             ,"Romanian (Mac)"),
                new CodePage(10017     ,"x-mac-ukrainian"            ,"Ukrainian (Mac)"),
                new CodePage(10021     ,"x-mac-thai"                 ,"Thai (Mac)"),
                new CodePage(10029     ,"x-mac-ce"                   ,"Central European (Mac)"),
                new CodePage(10079     ,"x-mac-icelandic"            ,"Icelandic (Mac)"),
                new CodePage(10081     ,"x-mac-turkish"              ,"Turkish (Mac)"),
                new CodePage(10082     ,"x-mac-croatian"             ,"Croatian (Mac)"),
                new CodePage(20000     ,"x-Chinese-CNS"              ,"Chinese Traditional (CNS)"),
                new CodePage(20001     ,"x-cp20001"                  ,"TCA Taiwan"),
                new CodePage(20002     ,"x-Chinese-Eten"             ,"Chinese Traditional (Eten)"),
                new CodePage(20003     ,"x-cp20003"                  ,"IBM5550 Taiwan"),
                new CodePage(20004     ,"x-cp20004"                  ,"TeleText Taiwan"),
                new CodePage(20005     ,"x-cp20005"                  ,"Wang Taiwan"),
                new CodePage(20105     ,"x-IA5"                      ,"Western European (IA5)"),
                new CodePage(20106     ,"x-IA5-German"               ,"German (IA5)"),
                new CodePage(20107     ,"x-IA5-Swedish"              ,"Swedish (IA5)"),
                new CodePage(20108     ,"x-IA5-Norwegian"            ,"Norwegian (IA5)"),
                new CodePage(20127     ,"us-ascii"                   ,"US-ASCII"),
                new CodePage(20261     ,"x-cp20261"                  ,"T.61"),
                new CodePage(20269     ,"x-cp20269"                  ,"ISO-6937"),
                new CodePage(20273     ,"IBM273"                     ,"IBM EBCDIC (Germany)"),
                new CodePage(20277     ,"IBM277"                     ,"IBM EBCDIC (Denmark-Norway)"),
                new CodePage(20278     ,"IBM278"                     ,"IBM EBCDIC (Finland-Sweden)"),
                new CodePage(20280     ,"IBM280"                     ,"IBM EBCDIC (Italy)"),
                new CodePage(20284     ,"IBM284"                     ,"IBM EBCDIC (Spain)"),
                new CodePage(20285     ,"IBM285"                     ,"IBM EBCDIC (UK)"),
                new CodePage(20290     ,"IBM290"                     ,"IBM EBCDIC (Japanese katakana)"),
                new CodePage(20297     ,"IBM297"                     ,"IBM EBCDIC (France)"),
                new CodePage(20420     ,"IBM420"                     ,"IBM EBCDIC (Arabic)"),
                new CodePage(20423     ,"IBM423"                     ,"IBM EBCDIC (Greek)"),
                new CodePage(20424     ,"IBM424"                     ,"IBM EBCDIC (Hebrew)"),
                new CodePage(20833     ,"x-EBCDIC-KoreanExtended"    ,"IBM EBCDIC (Korean Extended)"),
                new CodePage(20838     ,"IBM-Thai"                   ,"IBM EBCDIC (Thai)"),
                new CodePage(20866     ,"koi8-r"                     ,"Cyrillic (KOI8-R)"),
                new CodePage(20871     ,"IBM871"                     ,"IBM EBCDIC (Icelandic)"),
                new CodePage(20880     ,"IBM880"                     ,"IBM EBCDIC (Cyrillic Russian)"),
                new CodePage(20905     ,"IBM905"                     ,"IBM EBCDIC (Turkish)"),
                new CodePage(20924     ,"IBM00924"                   ,"IBM Latin-1"),
                new CodePage(20932     ,"EUC-JP"                     ,"Japanese (JIS 0208-1990 and 0212-1990)"),
                new CodePage(20936     ,"x-cp20936"                  ,"Chinese Simplified (GB2312-80)"),
                new CodePage(20949     ,"x-cp20949"                  ,"Korean Wansung"),
                new CodePage(21025     ,"cp1025"                     ,"IBM EBCDIC (Cyrillic Serbian-Bulgarian)"),
                new CodePage(21866     ,"koi8-u"                     ,"Cyrillic (KOI8-U)"),
                new CodePage(28591     ,"iso-8859-1"                 ,"Western European (ISO)"),
                new CodePage(28592     ,"iso-8859-2"                 ,"Central European (ISO)"),
                new CodePage(28593     ,"iso-8859-3"                 ,"Latin 3 (ISO)"),
                new CodePage(28594     ,"iso-8859-4"                 ,"Baltic (ISO)"),
                new CodePage(28595     ,"iso-8859-5"                 ,"Cyrillic (ISO)"),
                new CodePage(28596     ,"iso-8859-6"                 ,"Arabic (ISO)"),
                new CodePage(28597     ,"iso-8859-7"                 ,"Greek (ISO)"),
                new CodePage(28598     ,"iso-8859-8"                 ,"Hebrew (ISO-Visual)"),
                new CodePage(28599     ,"iso-8859-9"                 ,"Turkish (ISO)"),
                new CodePage(28603     ,"iso-8859-13"                ,"Estonian (ISO)"),
                new CodePage(28605     ,"iso-8859-15"                ,"Latin 9 (ISO)"),
                new CodePage(29001     ,"x-Europa"                   ,"Europa"),
                new CodePage(38598     ,"iso-8859-8-i"               ,"Hebrew (ISO-Logical)"),
                new CodePage(50220     ,"iso-2022-jp"                ,"Japanese (JIS)"),
                new CodePage(50221     ,"csISO2022JP"                ,"Japanese (JIS-Allow 1 byte Kana)"),
                new CodePage(50222     ,"iso-2022-jp"                ,"Japanese (JIS-Allow 1 byte Kana - SO/SI) 	"),
                new CodePage(50225     ,"iso-2022-kr"                ,"Korean (ISO)"),
                new CodePage(50227     ,"x-cp50227"                  ,"Chinese Simplified (ISO-2022)"),
                new CodePage(51932     ,"euc-jp"                     ,"Japanese (EUC)"),
                new CodePage(51936     ,"EUC-CN"                     ,"Chinese Simplified (EUC)"),
                new CodePage(51949     ,"euc-kr"                     ,"Korean (EUC)"),
                new CodePage(52936     ,"hz-gb-2312"                 ,"Chinese Simplified (HZ)"),
                new CodePage(54936     ,"GB18030"                    ,"Chinese Simplified (GB18030)"),
                new CodePage(57002     ,"x-iscii-de"                 ,"ISCII Devanagari"),
                new CodePage(57003     ,"x-iscii-be"                 ,"ISCII Bengali"),
                new CodePage(57004     ,"x-iscii-ta"                 ,"ISCII Tamil"),
                new CodePage(57005     ,"x-iscii-te"                 ,"ISCII Telugu"),
                new CodePage(57006     ,"x-iscii-as"                 ,"ISCII Assamese"),
                new CodePage(57007     ,"x-iscii-or"                 ,"ISCII Oriya"),
                new CodePage(57008     ,"x-iscii-ka"                 ,"ISCII Kannada"),
                new CodePage(57009     ,"x-iscii-ma"                 ,"ISCII Malayalam"),
                new CodePage(57010     ,"x-iscii-gu"                 ,"ISCII Gujarati"),
                new CodePage(57011     ,"x-iscii-pa"                 ,"ISCII Punjabi"),
                new CodePage(65000     ,"utf-7"                      ,"Unicode (UTF-7)"),
                new CodePage(65001     ,"utf-8"                      ,"Unicode (UTF-8)"),
                new CodePage(65005     ,"utf-32"                     ,"Unicode (UTF-32)"),
                new CodePage(65006     ,"utf-32BE"                   ,"Unicode (UTF-32 Big-Endian)")
            };
        }


        /// <summary>
        /// Reads in one file using the specified codepage and outputs another file with a different codepage
        /// </summary>
        /// <param name="sFileInPath">input file path</param>
        /// <param name="sFileOutPath">output file path</param>
        /// <param name="iCodePageIn">codepage number of the input file</param>
        /// <param name="iCodePageOut">codepage number of the output file</param>
        public static void ConvertFileToCodePageFile(string sFileInPath, string sFileOutPath, int iCodePageIn, int iCodePageOut)
        {
            //read in the file to convert
            StreamReader inStream = new StreamReader(sFileInPath, Encoding.GetEncoding(iCodePageIn));
            //create the file to output to
            StreamWriter outStream = new StreamWriter(sFileOutPath, false, Encoding.GetEncoding(iCodePageOut));

            //set an input buffer
            char [] chBuffer = new char[4096];
            int iCount ;
            //read into the buffer
            try
            {
                while ((iCount = inStream.Read(chBuffer, 0, 4096)) > 0)
                {
                    //write out the converted buffer
                    outStream.Write(chBuffer, 0, iCount);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            //close the reader/writer
            inStream.Close();
            outStream.Close();
        }

        /// <summary>
        /// BROKEN:DO NOT USE:Converts a string to a file using the specified codepage
        /// </summary>
        /// <param name="sIn">String to be converted</param>
        /// <param name="sFileOutPath">path of the file to write</param>
        /// <param name="iCodePageIn">codepage of the incoming string - should be utf8 (65001)</param>
        /// <param name="iCodePageOut">codepage of the outgoing string</param>
        public static void ConvertStringToCodePageFile(string sIn, string sFileOutPath, int iCodePageIn, int iCodePageOut)
        {
            //encode the incoming string to bytes in the right codepage
            byte[] asciiBytes = Encoding.GetEncoding(iCodePageIn).GetBytes(sIn);
            //convert from one codepage to another
            byte[] outBytes = Encoding.Convert(Encoding.GetEncoding(iCodePageIn), Encoding.GetEncoding(iCodePageOut), asciiBytes);
            //convert the bytes to a character array so we can dump using stringwriter
            char[] outChar = new char[outBytes.Length];
            for (int i = 0; i < outBytes.Length; i++)
            {
                outChar[i] = (char)outBytes[i];
            }
            //write out the converted text to a file
            using (StreamWriter outStream = new StreamWriter(sFileOutPath, false))
            {
                outStream.Write(outChar);
                outStream.Flush();
                outStream.Close();
            }
        }

        /// <summary>
        /// BROKEN:DO NOT USE:Converts a string to another string using the specified codepage
        /// </summary>
        /// <param name="sIn">String to be converted</param>
        /// <param name="iCodePageIn">codepage of the incoming string - should be utf8 (65001)</param>
        /// <param name="iCodePageOut">codepage of the outgoing string</param>
        public static string ConvertStringToCodePageString(string sIn, int iCodePageIn, int iCodePageOut)
        {
            //encode the incoming string to bytes in the right codepage
            byte[] asciiBytes = Encoding.GetEncoding(iCodePageIn).GetBytes(sIn);
            //convert from one codepage to another
            byte[] outBytes = Encoding.Convert(Encoding.GetEncoding(iCodePageIn), Encoding.GetEncoding(iCodePageOut), asciiBytes);
            //convert the bytes to a character array so we can dump using stringwriter
            char[] outChar = new char[outBytes.Length];
            for (int i = 0; i < outBytes.Length; i++)
            {
                outChar[i] = (char)outBytes[i];
            }
            //convert chars to string
            string s = new string(outChar);
            return s;
        }

        /// <summary>
        /// Convert a file from a codepage to utf8 and save
        /// 
        /// from the MSDN website
        /// http://msdn.microsoft.com/en-us/library/cc488003.aspx
        /// </summary>
        /// <param name="sFileIn">Input file to parse</param>
        /// <param name="encoding">Encoding parameter for the input file</param>
        public static void ConvertFileToUTF8File(string sFileIn, string sFileOut, Encoding encoding, bool bBOM)
        {
            // Specify the code page to correctly interpret byte values
            byte[] codePageValues = System.IO.File.ReadAllBytes(sFileIn);

            // Same content is now encoded as UTF-16
            string unicodeValues = encoding.GetString(codePageValues);

            if (bBOM)
            {
                // Same content is stored as UTF-8
                File.WriteAllText(sFileOut, unicodeValues, Encoding.UTF8);
            }
            else
            {
                // Save file as UTF-8 without BOM
                Encoding utf8WithoutBom = new UTF8Encoding(false);  //false indicates no BOM
                TextWriter tw = new StreamWriter(sFileOut, false, utf8WithoutBom);
                tw.Write(unicodeValues);
                tw.Close();
            }


        }

    }
}
