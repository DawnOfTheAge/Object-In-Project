using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using General.Common;

namespace ObjectInProject.Common
{ 
    [Serializable]
    public class Solution
    {
        public Solution()
        {
            Projects = new List<Project>();
        }

        public string Name
        {
            get;
            set;
        }

        public List<Project> Projects
        {
            get;
            set;
        }
    }

    [Serializable]
    public class Project
    {
        public Project()
        {
            Files = new List<CsFile>();
        }

        public int Id
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public List<CsFile> Files
        {
            get;
            set;
        }
    }

    [Serializable]
    public class CsFile
    {
        public CsFile()
        {
            Lines = new List<string>();
        }

        public string Name
        {
            get;
            set;
        }

        public List<string> Lines
        {
            get;
            set;
        }
    }  
}
