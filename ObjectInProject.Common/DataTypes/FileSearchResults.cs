using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class FileSearchResults
    {
        private List<FileSearchResult> Results;

        public FileSearchResults()
        {
            Results = new List<FileSearchResult>();
        }

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
    }
}
