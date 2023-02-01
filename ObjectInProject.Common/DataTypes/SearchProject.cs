using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ObjectInProject.Common
{
    [Serializable]
    public class SearchProject
    {        
        #region Properties

        public int Index
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Editor
        {
            get;
            set;
        }

        public List<string> Workspace
        {
            get;
            set;
        }

        [XmlIgnore]
        public List<Solution> SolutionsInformation
        {
            get;
            set;
        }

        public string FileTypeFilter
        {
            get; set;
        }

        public bool CaseSensitive
        {
            get;
            set;
        }

        public bool NoTests
        {
            get;
            set;
        }

        public string SearchAndDelimiter
        {
            get;
            set;
        }

        public string SearchOrDelimiter
        {
            get;
            set;
        }

        public SearchProjectType Type
        {
            get;
            set;
        }

        public SearchLogic Logic
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public SearchProject()
        {
            Index = ObjectInProjectConstants.NONE;

            Workspace = new List<string>();
            SolutionsInformation = new List<Solution>();
            Editor = string.Empty;
            Type = SearchProjectType.SolutionsProject;
            Logic = SearchLogic.And;
        }

        public SearchProject(int index)
        {
            Index = index;

            Workspace = new List<string>();
            SolutionsInformation = new List<Solution>();
            Editor = string.Empty;
            Type = SearchProjectType.SolutionsProject;
            Logic = SearchLogic.And;
        }

        public SearchProject(int index, string name)
        {
            Index = index;

            Name = name;
            Workspace = new List<string>();
            SolutionsInformation = new List<Solution>();
            Editor = string.Empty;
            Type = SearchProjectType.SolutionsProject;
            Logic = SearchLogic.And;
        }

        #endregion
    }
}
