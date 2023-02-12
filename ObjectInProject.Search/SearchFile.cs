using ObjectInProject.Common;
using ObjectInProject.Utils;
using System.Collections.Generic;
using System.IO;
using System;

namespace ObjectInProject.Search
{
    public static class SearchFile
    {
        #region Public Methods

        public static bool SearchInFile(FileOrigin file,
                                        List<string> tokens,
                                        SearchLogic searchLogic,
                                        bool caseSensitive,
                                        out SearchedFile searchedFile,
                                        out string result)
        {
            searchedFile = null;

            try
            {
                if (!File.Exists(file.Path))
                {
                    result = $"File '{file.Path}' Does Not Exist";

                    return false;
                }

                switch (file.ProjectType)
                {
                    case SearchProjectType.DirectoriesProject:
                        searchedFile = new SearchedFile(file.RootDirectory, Path.GetFileName(file.Path), file.Path);
                        break;

                    case SearchProjectType.SolutionsProject:
                        searchedFile = new SearchedFile(file.Solution, file.Project, Path.GetFileName(file.Path), file.Path);
                        break;

                    default:
                        result = $"Search Project Type Unknown '{file.ProjectType}'";

                        return false;
                }

                if (FilesUtils.ReadFileLines(file.Path, out List<string> lines, out result))
                {
                    if (lines != null)
                    {
                        int lineNumber = 1;
                        foreach (string line in lines)
                        {
                            if (SearchLine.SearchInLine(line, tokens, searchLogic, caseSensitive, out result))
                            {
                                if (!searchedFile.AddLine(new SearchedLine(lineNumber, line), out result))
                                {
                                    result = $"{Environment.NewLine}{result}";
                                }
                            }
                            else
                            {
                                result = $"{Environment.NewLine}{result}";
                            }

                            lineNumber++;
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
