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
    /// class to hold one item that represents a letter/html code representation
    /// </summary>
    public class cCharReplacements
    {
        private string _letter = "";
        private string _htmlcode = "";

        public string letter
        {
            get { return _letter; }
            set { _letter = value; }
        }
        public string htmlcode
        {
            get { return _htmlcode; }
            set { _htmlcode = value; }
        }

        public cCharReplacements(string s, string html)
        {
            _letter = s;
            _htmlcode = html;
        }

    }
}
