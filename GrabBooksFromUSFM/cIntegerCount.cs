using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GBC_USFM_Preprocessor
{
    public class cIntegerCount
    {
        List<Point> oPoints = new List<Point>();

        public cIntegerCount()
        {

        }

        public void ParseVerseForNumbers(string sTmp)
        {
            string s = sTmp;
            //remove the first verse number
            if (IsNumeric(s[0].ToString()))
            {
                if (s.IndexOf(" ") > 0)
                {
                    s = s.Substring(s.IndexOf(" ")).Trim();
                }
                else
                {
                    s = "";
                }
            }

            //remove any commas
            s = s.Replace(",", "");

            Regex r = new Regex(@"(?<!\S)\d+(?!\S)", RegexOptions.IgnoreCase);
            foreach (Match m in r.Matches(s))
            {
                Console.WriteLine(m.Groups["num"]);

                //look to see if this a used before

            }



        }

        public Boolean IsNumeric(string stringToTest)
        {
            int result;
            return int.TryParse(stringToTest, out result);
        }
    }


    // Point Class to handle Map Functions
    public class Point
    {
        // Private data members
        private int x;
        private int y;

        // Default Constructor
        public Point()
        {
            // Set the internal values
            this.x = 0;
            this.y = 0;
        }

        // Standard Constructor
        public Point(int X, int Y)
        {
            // Set the internal values
            this.x = X;
            this.y = Y;
        }


        // Provide another equivalent of an assignment operator
        public void Set(int X, int Y)
        { 
            // Set the internal values
            this.x = X;
            this.y = Y;
        }

        // Publically accessible properties
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }       
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }


    }

}
