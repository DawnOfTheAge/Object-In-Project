using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class FileOrigin
    {
        #region Properties

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

        public string RootDirectory
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public SearchProjectType ProjectType
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public FileOrigin(string rootDirectory, string path)
        {
            RootDirectory = rootDirectory;
            Path = path;
            ProjectType = SearchProjectType.DirectoriesProject;
        }

        public FileOrigin(string solution, string project, string path)
        {
            Solution = solution;
            Project = project;
            Path = path;
            ProjectType = SearchProjectType.SolutionsProject;
        }

        #endregion
    }
}
