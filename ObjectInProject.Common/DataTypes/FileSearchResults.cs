using System;
using System.Collections.Generic;

namespace ObjectInProject.Common
{
    public class FileSearchResults
    {
        #region Data Members

        private List<FileSearchResult> Results; 

        #endregion

        #region Constructor

        public FileSearchResults()
        {
            Results = new List<FileSearchResult>();
        } 

        #endregion

        #region Public Methods

        public bool AddFileSearchResult(FileSearchResult fileSearchJobResult)
        {
            try
            {
                Results.Add(fileSearchJobResult);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<FileSearchResult> GetFileSearchResults()
        {
            return Results;
        } 

        #endregion
    }
}
