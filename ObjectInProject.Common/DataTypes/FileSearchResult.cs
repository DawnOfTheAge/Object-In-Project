namespace ObjectInProject.Common
{
    public class FileSearchResult
    {
        #region Properties

        public int Line
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public FileSearchResult(int line)
        {
            Line = line;
        } 

        #endregion
    }
}
