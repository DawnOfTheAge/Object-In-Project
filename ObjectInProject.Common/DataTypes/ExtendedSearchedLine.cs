using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class ExtendedSearchedLine : SearchedLine
    {
        #region Properties

        public string Path
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public ExtendedSearchedLine()
        {
        }

        public ExtendedSearchedLine(int lineNumber, string line, string name, string path) : base(lineNumber, line)
        {
            Name = name;
            Path = path;
        }

        #endregion
    }
}
