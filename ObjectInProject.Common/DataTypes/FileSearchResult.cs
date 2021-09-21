using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class FileSearchResult
    {
        public FileSearchResult(int line)
        {
            Line = line;
        }

        public int Line
        {
            get;
            set;
        }
    }
}
