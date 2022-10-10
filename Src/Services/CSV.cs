using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CP_Dev_Tools.Src.Services
{
    class CSV : List<string[]>
    {
        protected string csv = string.Empty;
        protected string seperator = ";";

        /// <summary>
        /// Creates a CSV class instance that contains the information feed by the user.
        /// </summary>
        /// <param name="csv"> contanis the contents of the csv file to be parsed </param>
        /// <param name="seperator"> represents the dilimitor used during parsing ( standard ; ) </param>
        public CSV( string csv, string seperator = ";" )
        {
            this.csv = csv;
            this.seperator = seperator;
            read();
        }

        public CSV( string csv, char seperator = ';' )
        {
            this.csv = csv;
            this.seperator = seperator.ToString();
            read();
        }

        public CSV( string csv )
        {
            this.csv = csv;
            this.seperator = ";";
            read();
        }

        internal void read()
        {
            foreach (string line in Regex.Split(this.csv, System.Environment.NewLine)
                                         .ToList()
                                         .Where(s => !string.IsNullOrEmpty(s)))
            {
                string[] values = Regex.Split(line, this.seperator);
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                this.Add(values);
            }
        }
    }
}
