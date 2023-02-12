using ObjectInProject.Common;
using System.Collections.Generic;
using System;

namespace ObjectInProject.Search
{
    public static class SearchListOfFiles
    {
        #region Public Methods

        public static bool SearchInListOfFiles(List<FileOrigin> files,
                                               List<string> tokens,
                                               SearchLogic searchLogic,
                                               bool caseSensitive,
                                               out SearchedFilesList searchedFilesList,
                                               out string result)
        {
            result = string.Empty;

            searchedFilesList = null;

            try
            {
                if ((files == null) || (files.Count == 0))
                {
                    result = "Files List Is Null Or Empty";

                    return false;
                }

                searchedFilesList = new SearchedFilesList();

                foreach (FileOrigin file in files)
                {
                    if (!SearchFile.SearchInFile(file, tokens, searchLogic, caseSensitive, out SearchedFile searchedFile, out result))
                    {
                        //  some Audit

                        continue;
                    }

                    if (!searchedFilesList.AddFile(searchedFile, out result))
                    {
                        //  some Audit
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion
    }
}
