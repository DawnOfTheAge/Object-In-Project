using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class SearchedLine
    {
        #region Properties

        public int LineNumber
        {
            get;
            set;
        }

        public string Line
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public SearchedLine()
        {
        }

        public SearchedLine(int lineNumber, string line)
        {
            LineNumber = lineNumber;
            Line = line;
        }

        #endregion
    }
}
