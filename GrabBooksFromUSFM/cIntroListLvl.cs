using System;
using System.Collections.Generic;
using System.Text;

namespace GBC_USFM_Preprocessor
{
    class cIntroListLvl
    {
        int _iOL_lvl = 0;
        int _iOL_lvl1 = 0;
        int _iOL_lvl2 = 0;
        int _iOL_lvl3 = 0;
        
        public int iOL_lvl
        {
            get { return _iOL_lvl; }
            set { _iOL_lvl = value; }
        }
        public int iOL_lvl1
        {
            get { return _iOL_lvl1; }
            set { _iOL_lvl1 = value; }
        }
        public int iOL_lvl2
        {
            get { return _iOL_lvl2; }
            set { _iOL_lvl2 = value; }
        }
        public int iOL_lvl3
        {
            get { return _iOL_lvl3; }
            set { _iOL_lvl3 = value; }
        }
        
        public string CheckNewLvl(string sTag)
        {
            //determine if we need </ol> tags and how many
            //increment level (1/2/3)
            return "";
        }
    }
}
