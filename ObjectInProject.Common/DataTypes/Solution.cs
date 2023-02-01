using System.Collections.Generic;

namespace ObjectInProject.Common
{
    public class Solution
    {        
        #region Properties

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

        #endregion

        #region Constructor

        public Solution()
        {
            Projects = new List<Project>();
        }

        #endregion
    }
}
