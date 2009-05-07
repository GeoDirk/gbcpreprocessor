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

namespace GBC_USFM_Preprocessor
{
    /// <summary>
    /// Structure to hold the results for the search for long words
    /// </summary>
    public class cLongWords
    {
        private string _sWord = "";
        private string _sFileName = "";
        private int _iTotalHits = 0;

        public string sWord
        {
            get { return _sWord; }
            set { _sWord = value; }
        }

        public int iTotalHits
        {
            get { return _iTotalHits; }
            set { _iTotalHits = value; }
        }

        public string sFileName
        {
            get { return _sFileName; }
            set { _sFileName = value; }
        }


    }
}
