using ObjectInProject.Common;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace ObjectInProject.Search
{
    public class SearchListOfFiles
    {
        #region Events

        public event AuditMessage Message;

        #endregion

        #region Public Methods

        public bool SearchInListOfFiles(List<FileOrigin> files,
                                        List<string> tokens,
                                        SearchLogic searchLogic,
                                        bool caseSensitive,
                                        out SearchedFilesList searchedFilesList,
                                        out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

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
                        Audit($"Failed Searching In File[{file.Path}]. {result}", method, LINE(), AuditSeverity.Warning);

                        continue;
                    }

                    if ((searchedFile != null) && (searchedFile.Lines != null) && (searchedFile.Lines.Count > 0))
                    {
                        if (!searchedFilesList.AddFile(searchedFile, out result))
                        {
                            Audit($"Failed Adding File[{searchedFile.Path}]. {result}", method, LINE(), AuditSeverity.Warning);
                        }
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

        #region Events Handlers

        public void OnMessage(string message, string method, string module, int line, AuditSeverity auditSeverity)
        {
            Message?.Invoke(message, method, module, line, auditSeverity);
        }

        #endregion

        #region Audit

        private void Audit(string message, string method, string module, int line, AuditSeverity auditSeverity)
        {
            OnMessage(message, method, module, line, auditSeverity);
        }

        private void Audit(string message, string method, int line, AuditSeverity auditSeverity)
        {
            string module = "Search List Of Files";

            Audit(message, method, module, line, auditSeverity);
        }

        public static int LINE([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }

        #endregion
    }
}
