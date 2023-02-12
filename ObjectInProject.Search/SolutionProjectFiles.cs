using ObjectInProject.Common;
using System.Collections.Generic;
using System.IO;
using System;

namespace ObjectInProject.Search
{
    public static class SolutionProjectFiles
    {
        #region Public Methods

        public static bool GetSolutionsFilesList(List<string> solutions, out List<FileOrigin> listOfFiles, out string result)
        {
            result = string.Empty;

            listOfFiles = null;

            try
            {
                if ((solutions == null) || (solutions.Count == 0))
                {
                    result = "No Solutions To Search";

                    return false;
                }

                foreach (string solution in solutions)
                {
                    if (!File.Exists(solution))
                    {
                        continue;
                    }

                    if (!ParseFile.ParseSolutionFile(solution, out List<string> projectFiles, out result))
                    {
                        continue;
                    }

                    if ((projectFiles == null) || (projectFiles.Count == 0))
                    {
                        continue;
                    }

                    listOfFiles = new List<FileOrigin>();
                    foreach (string projectFile in projectFiles)
                    {
                        if (File.Exists(projectFile))
                        {
                            if (!ParseFile.ParseProjectFile(projectFile, out List<string> codeFiles, out result))
                            {
                                //  Some Audit
                            }
                            else
                            {
                                if ((codeFiles != null) && (codeFiles.Count > 0))
                                {
                                    foreach (string codeFile in codeFiles)
                                    {
                                        if (File.Exists(codeFile))
                                        {
                                            listOfFiles.Add(new FileOrigin(Path.GetFileName(solution),
                                                                           Path.GetFileName(projectFile),
                                                                           codeFile));
                                        }
                                    }
                                }
                            }
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
    }
}
