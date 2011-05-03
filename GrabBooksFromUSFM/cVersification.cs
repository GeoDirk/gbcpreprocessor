using System;
using System.Collections.Generic;
using System.Text;

namespace GBC_USFM_Preprocessor
{
    /// <summary>
    /// Stores the versification strings
    /// </summary>
    public class cVersification
    {
        private string _sFileName = "";
        private string _sChapNum = "";
        private string _sVerseNum = "";
        private string _sMessage = "";

        /// <summary>
        /// Class initialization
        /// </summary>
        /// <param name="FileName">Filename</param>
        /// <param name="ChapNum">chapter number</param>
        /// <param name="VerseNum">verse number</param>
        public cVersification(string FileName, string ChapNum, string VerseNum, string Message)
        {
            _sFileName = FileName;
            _sChapNum = ChapNum;
            _sVerseNum = VerseNum;
            _sMessage = Message;
        }

        /// <summary>
        /// The filename of the problem book
        /// </summary>
        public string sFileName
        {
            get { return _sFileName; }
            set { _sFileName = value; }
        }

        /// <summary>
        /// chapter number
        /// </summary>
        public string sChapNum
        {
            get { return _sChapNum; }
            set { _sChapNum = value; }
        }

        /// <summary>
        /// verse number
        /// </summary>
        public string sVerseNum
        {
            get { return _sVerseNum; }
            set { _sVerseNum = value; }
        }

        /// <summary>
        /// message to send out
        /// </summary>
        public string sMessage
        {
            get { return _sMessage; }
            set { _sMessage = value; }
        }

    }
}
