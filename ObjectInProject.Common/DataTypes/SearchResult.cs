using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class SearchResult
    {
        public SearchResult(string solution, string project, string file, int line, string fullPath)
        {
            Solution = solution;
            Project = project;
            File = file;
            Line = line;
            FullPath = fullPath;
        }

        public SearchResult(string directory, string file, int line, string fullPath)
        {
            Directoty = directory;
            File = file;
            Line = line;
            FullPath = fullPath;
        }

        public string Solution
        {
            get;
            set;
        }

        public string Project
        {
            get;
            set;
        }

        public string File
        {
            get;
            set;
        }

        public string FullPath
        {
            get;
            set;
        }

        public int Line
        {
            get;
            set;
        }

        public string Directoty
        {
            get;
            set;
        }
    }
}
