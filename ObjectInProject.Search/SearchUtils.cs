using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using General.Common;
using General.Common.Utils;
using General.Log;
using ObjectInProject.Common;

namespace ObjectInProject.Search
{
    public static class SearchUtils
    {
        #region Local Constants

        private static string module = MethodBase.GetCurrentMethod().DeclaringType.Name;

        #endregion

        public static bool FindToken(string token,
                                     SearchProject configuration,
                                     out List<SearchResult> serachResults,
                                     out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            List<string> fileTypeFilterList;

            int fileCounter = 0;

            #endregion

            result = string.Empty;
            serachResults = null;

            try
            {
                #region Check For Problems

                if (configuration == null)
                {
                    result = "Configuration Object Is Null";

                    return false;
                }

                if (!StringUtils.Split(configuration.FileTypeFilter, ';', out fileTypeFilterList, out result))
                {
                    return false;
                }

                if ((fileTypeFilterList == null) || (fileTypeFilterList.Count == 0))
                {
                    result = "No File Type Filter";

                    return false;
                }

                #endregion

                serachResults = new List<SearchResult>();

                if (!configuration.CaseSensitive)
                {
                    token = token.ToLower();
                }

                switch (configuration.Type)
                {
                    case SearchProjectType.SolutionsProject:
                        #region Solutions / Projects search

                        for (int solutionIndex = 0; solutionIndex < configuration.SolutionsInformation.Count; solutionIndex++)
                        {
                            Solution solution = configuration.SolutionsInformation[solutionIndex];

                            if (configuration.NoTests)
                            {
                                if (solution.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                {
                                    continue;
                                }
                            }

                            if (solution.Projects != null)
                            {
                                for (int projectIndex = 0; projectIndex < solution.Projects.Count; projectIndex++)
                                {
                                    Project project = solution.Projects[projectIndex];

                                    if (configuration.NoTests)
                                    {
                                        if (project.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                        {
                                            continue;
                                        }
                                    }

                                    if (project.Files != null)
                                    {
                                        for (int fileIndex = 0; fileIndex < project.Files.Count; fileIndex++)
                                        {
                                            CsFile csFile = project.Files[fileIndex];

                                            if (configuration.NoTests)
                                            {
                                                if (csFile.Name.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                                {
                                                    continue;
                                                }
                                            }

                                            fileCounter++;

                                            if (csFile.Lines != null)
                                            {
                                                for (int lineIndex = 0; lineIndex < csFile.Lines.Count; lineIndex++)
                                                {
                                                    string line = csFile.Lines[lineIndex];

                                                    if (!configuration.CaseSensitive)
                                                    {
                                                        line = line.ToLower();
                                                    }

                                                    if (line.IndexOf(token) != Constants.NONE)
                                                    {
                                                        SearchResult searchResult = new SearchResult(Path.GetFileName(solution.Name),
                                                                                                     Path.GetFileName(project.Name),
                                                                                                     Path.GetFileName(csFile.Name),
                                                                                                     lineIndex + 1,
                                                                                                     csFile.Name);

                                                        serachResults.Add(searchResult);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                        return true;

                    case SearchProjectType.DirectoriesProject:
                        #region All Files search

                        if ((configuration.Workspace == null) || (configuration.Workspace.Count == 0))
                        {
                            result = "No Directories Defined For Search";

                            return false;
                        }

                        for (int directoryIndex = 0; directoryIndex < configuration.Workspace.Count; directoryIndex++)
                        {
                            string currentDirectory = configuration.Workspace[directoryIndex];

                            if (!Directory.Exists(currentDirectory))
                            {
                                Audit($"Directory '{currentDirectory}' Does Not Exist", method, Log.LINE(), AuditSeverity.Information);

                                continue;
                            }

                            if (configuration.NoTests)
                            {
                                if (currentDirectory.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                {
                                    Audit($"Directory '{currentDirectory}' Has 'test' In It", method, Log.LINE(), AuditSeverity.Information);

                                    continue;
                                }
                            }

                            foreach (string currentFileExtension in fileTypeFilterList)
                            {
                                List<string> allFiles = Directory.GetFiles(currentDirectory, 
                                                                           currentFileExtension, 
                                                                           SearchOption.AllDirectories).ToList();

                                if ((allFiles == null) || (allFiles.Count == 0))
                                {
                                    continue;
                                }

                                Audit($"For Extension[{currentFileExtension}] {allFiles.Count} Files Found", 
                                      method, 
                                      Log.LINE(), 
                                      AuditSeverity.Information);

                                //SearchInListOfFiles(allFiles, )

                                //foreach (string currentFile in allFiles)
                                //{
                                //    if (configuration.NoTests)
                                //    {
                                //        if (currentFile.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != Constants.NONE)
                                //        {
                                //            Audit($"File '{currentFile}' Has 'test' In It", method, Log.LINE(), AuditSeverity.Information);

                                //            continue;
                                //        }
                                //    }



                                //    //List<string> lines;

                                //    //if (Utils.ReadFileLines(currentFile, out lines, out result))
                                //    //{
                                //    //    fileCounter++;

                                //    //    if (lines != null)
                                //    //    {
                                //    //        for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
                                //    //        {
                                //    //            string line = lines[lineIndex];

                                //    //            if (!configuration.CaseSensitive)
                                //    //            {
                                //    //                line = line.ToLower();
                                //    //            }

                                //    //            if (line.IndexOf(text) != Constants.NONE)
                                //    //            {
                                //    //                SearchResult searchResult = new SearchResult(Path.GetDirectoryName(currentFile),
                                //    //                                                             Path.GetFileName(currentFile),
                                //    //                                                             lineIndex + 1,
                                //    //                                                             currentFile);

                                //    //                serachResults.Add(searchResult);
                                //    //            }
                                //    //        }
                                //    //    }
                                //    //}
                                //}
                            }
                        }

                        #endregion
                        return true;

                    default:
                        return false;
                }
            }
            catch (Exception e)
            {
                result = e.Message;

                Audit(result, method, Log.LINE(), AuditSeverity.Error);

                return false;
            }
        }

        public static bool FindTokens(List<string> tokens,
                                      SearchProject configuration,
                                      out List<SearchResult> searchResults, 
                                      out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            List<string> fileTypeFilterList;

            #endregion

            result = string.Empty;

            searchResults = null;

            try
            {
                #region Check For Problems

                if (configuration == null)
                {
                    result = "Configuration Is Null";

                    return false;
                }

                if (!StringUtils.Split(configuration.FileTypeFilter, ';', out fileTypeFilterList, out result))
                {
                    return false;
                }

                if ((fileTypeFilterList == null) || (fileTypeFilterList.Count == 0))
                {
                    result = "No File Type Filter";

                    return false;
                }

                if ((tokens == null) || (tokens.Count == 0))
                {
                    result = "No Text To Search";

                    return false;
                }

                #endregion

                SearchedFilesList searchedFilesList;
                List<FileOrigin> listOfFiles;

                if (!GetFilesList(configuration.Type, configuration.Workspace, configuration.FileTypeFilter, out listOfFiles, out result))
                {
                    return false;
                }

                if (!SearchInListOfFiles(listOfFiles, tokens, configuration.Logic, configuration.CaseSensitive, out searchedFilesList, out result))
                { 
                    return false;
                }


                if (!GetSearchResults(searchedFilesList, out searchResults, out result))
                {
                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private static bool GetSearchResults(SearchedFilesList searchedFilesList, out List<SearchResult> searchResults, out string result)
        {
            result = string.Empty;

            searchResults = null;

            try
            {
                if ((searchedFilesList == null) || (searchedFilesList.Files == null) || (searchedFilesList.Files.Count == 0))
                {
                    result = "No Results";

                    return false;
                }

                searchResults = new List<SearchResult>();
                foreach (SearchedFile searchedFile in searchedFilesList.Files)
                {
                    List<SearchResult> currentSearchResults;
                    if (!searchedFile.GetSearchResults(out currentSearchResults, out result))
                    {
                        continue;
                    }

                    searchResults.AddRange(searchResults);
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private static bool GetFilesList(SearchProjectType type, 
                                         List<string> paths, 
                                         string searchPattern, 
                                         out List<FileOrigin> listOfFiles, 
                                         out string result)
        {
            result = string.Empty;

            listOfFiles = null;

            try
            {
                switch (type)
                {
                    case SearchProjectType.DirectoriesProject:
                        if (!GetPathsFilesList(paths, searchPattern, out listOfFiles, out result))
                        {
                            return false;
                        }
                        break;

                    case SearchProjectType.SolutionsProject:
                        if (!GetSolutionshsFilesList(paths, out listOfFiles, out result))
                        {
                            return false;
                        }
                        break;

                    default:
                        result = $"Unknown Project Type '{type}'";
                        return false;
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #region Files For Soulution Project

        private static bool GetSolutionshsFilesList(List<string> solutions, out List<FileOrigin> listOfFiles, out string result)
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

                    List<string> projectFiles;
                    if (!ParseFile.ParseSolutionFile(solution, out projectFiles, out result))
                    {
                        continue;
                    }

                    if ((projectFiles == null) || (projectFiles.Count == 0))
                    {
                        continue;
                    }

                    listOfFiles = new List<FileOrigin>();
                    List<string> codeFiles;
                    foreach (string projectFile in projectFiles)
                    {
                        if (File.Exists(projectFile))
                        {
                            if (!ParseFile.ParseProjectFile(projectFile, out codeFiles, out result))
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

        #region Files For Directory Project 

        private static bool GetPathsFilesList(List<string> paths, string searchPattern, out List<FileOrigin> listOfFiles, out string result)
        {
            result = string.Empty;

            listOfFiles = null;

            try
            {
                if ((paths == null) || (paths.Count == 0))
                {
                    result = "No Paths To Search";

                    return false;
                }


                listOfFiles = new List<FileOrigin>();
                foreach (string path in paths)
                {
                    List<FileOrigin> currentListOfFiles;
                    if (!GetPathFilesList(path, searchPattern, out currentListOfFiles, out result))
                    { 
                    }

                    if ((currentListOfFiles == null) || (currentListOfFiles.Count == 0))
                    {
                        continue;
                    }

                    listOfFiles.AddRange(currentListOfFiles);
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private static bool GetPathFilesList(string path, string searchPattern, out List<FileOrigin> listOfFiles, out string result)
        {
            result = string.Empty;

            listOfFiles = null;

            try
            {
                if (!Directory.Exists(path))
                {
                    result = $"Path '{path}' Does Not Exist";

                    return false;    
                }

                List<string> files = Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories).ToList();

                if ((files == null) || (files.Count == 0))
                {
                    result = "No Files Found";

                    return false;
                }

                foreach (string file in files)
                {
                    if (File.Exists(file))
                    {
                        listOfFiles.Add(new FileOrigin(path, file));
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

        #region Search In List Of Files

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
                    SearchedFile searchedFile;
                    if (!SearchInFile(file, tokens, searchLogic, caseSensitive, out searchedFile, out result))
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

        #region Search File

        public static bool SearchInFile(FileOrigin file, 
                                        List<string> tokens, 
                                        SearchLogic searchLogic, 
                                        bool caseSensitive, 
                                        out SearchedFile searchedFile, 
                                        out string result)
        {
            result = string.Empty;

            searchedFile = null;

            try
            {
                if (!File.Exists(file.Path))
                {
                    result = $"File '{file.Path}' Does Not Exist";

                    return false;
                }

                List<string> lines;

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

                if (Utils.ReadFileLines(file.Path, out lines, out result))
                {
                    if (lines != null)
                    {
                        int lineNumber = 1;
                        foreach (string line in lines)
                        {
                            if (SearchInLine(line, tokens, searchLogic, caseSensitive, out result))
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

        #region Search Line

        public static bool SearchInLine(string line, List<string> tokens, SearchLogic searchLogic, bool caseSensitive, out string result)
        {
            result = string.Empty;

            try
            {
                switch (searchLogic)
                {
                    case SearchLogic.And:
                        return SearchInLineAnd(line, tokens, caseSensitive, out result);

                    case SearchLogic.Or:
                        return SearchInLineOr(line, tokens, caseSensitive, out result);

                    default:
                        result = $"Wrong Search Logic Parameter - '{searchLogic}'";
                        break;
                }

                return false;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private static bool SearchInLineAnd(string line, List<string> tokens, bool caseSensitive, out string result)
        {            
            result = string.Empty;

            try
            {
                if ((tokens == null) || (tokens.Count == 0))
                {
                    return false;
                }

                if (!caseSensitive)
                {
                    line = line.ToLower();
                }

                bool success = true;

                foreach (string token in tokens)
                {
                    string currentToken = token;

                    if (!caseSensitive)
                    {
                        currentToken = currentToken.ToLower();
                    }

                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        bool contains = line.Contains(currentToken);
                        success &= contains;
                    }
                }

                return success;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private static bool SearchInLineOr(string line, List<string> tokens, bool caseSensitive, out string result)
        {            
            result = string.Empty;

            try
            {
                if ((tokens == null) || (tokens.Count == 0))
                {
                    return false;
                }

                if (!caseSensitive)
                {
                    line = line.ToLower();
                }

                bool success = false;

                foreach (string token in tokens)
                {
                    string currentToken = token;

                    if (!caseSensitive)
                    {
                        currentToken = currentToken.ToLower();
                    }

                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        bool contains = line.Contains(currentToken);
                        success |= contains;
                    }
                }

                return success;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Audit

        private static void Audit(string message, string method, int line, AuditSeverity auditSeverity)
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string fileName = Log.FILE();

            Log.Audit(message, fileName, assemblyName, module, method, auditSeverity, line);
        }

        #endregion
    }
}
