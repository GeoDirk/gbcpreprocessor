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
    /// Class to hold the results of the extra tags and their count
    /// </summary>
    public class cTagCount
    {
        private int _iTagCount;
        private string _sTagName;
        ArrayList _sFileNames = new ArrayList();

        /// <summary>
        /// The total number of times that the tag has been hit
        /// </summary>
        public int iTagCount
        {
            get { return _iTagCount; }
            set { _iTagCount = value; }
        }

        /// <summary>
        /// The tag name (i.e., \h,\v...)
        /// </summary>
        public string sTagName
        {
            get { return _sTagName; }
            set { _sTagName = value; }
        }

        /// <summary>
        /// The filenames to process
        /// </summary>
        public ArrayList sFileNames
        {
            get { return _sFileNames; }
            set { _sFileNames = value; }
        }
    
    }
}
