using System.Collections.Generic;

namespace ObjectInProject.Common
{
    public class CsFile
    {        
        #region Properties

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

        #endregion

        #region Constructor

        public CsFile()
        {
            Lines = new List<string>();
        }

        #endregion
    }
}
