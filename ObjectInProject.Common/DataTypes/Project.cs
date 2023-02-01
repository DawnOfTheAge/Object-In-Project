using System.Collections.Generic;

namespace ObjectInProject.Common
{
    public class Project
    {        
        #region Properties

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

        #endregion

        #region Constructor

        public Project()
        {
            Files = new List<CsFile>();
        } 

        #endregion
    }
}
