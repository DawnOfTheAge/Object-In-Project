﻿using ObjectInProject.Common;
using ObjectInProject.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ObjectInProject.Search
{
    public class SearchUtils
    {
        #region Events

        public event AuditMessage Message;

        #endregion
        
        #region Public Methods

        public bool FindToken(string token,
                              SearchProject configuration,
                              out List<SearchResult> serachResults,
                              out string result)
        {

            string method = MethodBase.GetCurrentMethod().Name;

            int fileCounter = 0;

            serachResults = null;

            try
            {
                #region Check For Problems

                if (configuration == null)
                {
                    result = "Configuration Object Is Null";

                    return false;
                }

                if (!StringUtils.Split(configuration.FileTypeFilter, ';', out List<string> fileTypeFilterList, out result))
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
                                if (solution.Name.ToLower().Contains(ObjectInProjectConstants.TEST_STRING))
                                {
                                    continue;
                                }
                            }

                            if ((solution.Projects == null) || (solution.Projects.Count == 0))
                            {
                                continue;
                            }

                            for (int projectIndex = 0; projectIndex < solution.Projects.Count; projectIndex++)
                            {
                                Project project = solution.Projects[projectIndex];

                                if (configuration.NoTests)
                                {
                                    if (project.Name.ToLower().Contains(ObjectInProjectConstants.TEST_STRING))
                                    {
                                        continue;
                                    }
                                }

                                if ((project.Files == null) || (project.Files.Count == 0))
                                {
                                    continue;
                                }

                                for (int fileIndex = 0; fileIndex < project.Files.Count; fileIndex++)
                                {
                                    CsFile csFile = project.Files[fileIndex];

                                    if (configuration.NoTests)
                                    {
                                        if (csFile.Name.ToLower().Contains(ObjectInProjectConstants.TEST_STRING))
                                        {
                                            continue;
                                        }
                                    }

                                    fileCounter++;

                                    if ((csFile.Lines == null) || (csFile.Lines.Count == 0))
                                    {
                                        continue;
                                    }

                                    for (int lineIndex = 0; lineIndex < csFile.Lines.Count; lineIndex++)
                                    {
                                        string line = csFile.Lines[lineIndex];

                                        if (!configuration.CaseSensitive)
                                        {
                                            line = line.ToLower();
                                        }

                                        if (line.Contains(token))
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
                                Audit($"Directory '{currentDirectory}' Does Not Exist", method, LINE(), AuditSeverity.Information);

                                continue;
                            }

                            if (configuration.NoTests)
                            {
                                if (currentDirectory.ToLower().IndexOf(ObjectInProjectConstants.TEST_STRING) != ObjectInProjectConstants.NONE)
                                {
                                    Audit($"Directory '{currentDirectory}' Has 'test' In It", method, LINE(), AuditSeverity.Information);

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
                                      LINE(),
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

                Audit(result, method, LINE(), AuditSeverity.Error);

                return false;
            }
        }

        public bool FindTokens(List<string> tokens,
                               SearchProject configuration,
                               out List<SearchResult> searchResults,
                               out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            searchResults = null;

            try
            {
                #region Check For Problems

                if (configuration == null)
                {
                    result = "Configuration Is Null";

                    return false;
                }

                if (!StringUtils.Split(configuration.FileTypeFilter, ';', out List<string> fileTypeFilterList, out result))
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

                Audit($"Search Projet Name[{configuration.Name}] Type[{configuration.Type}]", method, LINE(), AuditSeverity.Information);

                if (!GetFilesList(configuration.Type,
                                  configuration.Workspace,
                                  configuration.FileTypeFilter,
                                  out List<FileOrigin> listOfFiles,
                                  out result))
                {
                    return false;
                }

                if ((listOfFiles == null) || (listOfFiles.Count == 0))
                {
                    Audit($"No Files Found", method, LINE(), AuditSeverity.Information);
                    
                    return true;
                }

                Audit($"{listOfFiles.Count} Files Found", method, LINE(), AuditSeverity.Information);


                Audit($"Tokens To Search[{string.Join(",", tokens)}] Logic[{configuration.Logic}] Case Sensitive[{configuration.CaseSensitive}]", 
                      method, 
                      LINE(), 
                      AuditSeverity.Information);

                SearchListOfFiles searchListOfFiles = new SearchListOfFiles();
                searchListOfFiles.Message += SearchListOfFiles_Message;
                if (!searchListOfFiles.SearchInListOfFiles(listOfFiles,
                                                           tokens,
                                                           configuration.Logic,
                                                           configuration.CaseSensitive,
                                                           out SearchedFilesList searchedFilesList,
                                                           out result))
                {
                    return false;
                }

                if ((searchedFilesList == null) || (searchedFilesList.Files == null) || (searchedFilesList.Files.Count == 0))
                {
                    Audit($"No Matching Files Found", method, LINE(), AuditSeverity.Information);

                    return true;
                }

                Audit($"{searchedFilesList.Files.Count} Matching Files Found", method, LINE(), AuditSeverity.Information);


                if (!GetSearchResults(searchedFilesList, out searchResults, out result))
                {
                    Audit($"Failed Getting Search Results. {result}", method, LINE(), AuditSeverity.Warning);

                    return false;
                }

                Audit($"{searchResults.Count} Matching Lines Found", method, LINE(), AuditSeverity.Information);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Private Methods

        private bool GetSearchResults(SearchedFilesList searchedFilesList, out List<SearchResult> searchResults, out string result)
        {
            string method = MethodBase.GetCurrentMethod().Name;

            result = string.Empty;

            searchResults = null;

            try
            {
                if ((searchedFilesList == null) ||
                    (searchedFilesList.Files == null) ||
                    (searchedFilesList.Files.Count == 0))
                {
                    result = "No Results";

                    return false;
                }

                searchResults = new List<SearchResult>();
                foreach (SearchedFile searchedFile in searchedFilesList.Files)
                {
                    if (!searchedFile.GetSearchResults(out List<SearchResult> currentSearchResults, out result))
                    {
                        Audit($"Failed Getting Search Results. {result}", method, LINE(), AuditSeverity.Warning);

                        continue;
                    }

                    if ((currentSearchResults != null) && (currentSearchResults.Count > 0))
                    {
                        searchResults.AddRange(currentSearchResults);
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

        private bool GetFilesList(SearchProjectType type,
                                  List<string> paths,
                                  string searchPattern,
                                  out List<FileOrigin> listOfFiles,
                                  out string result)
        {
            listOfFiles = null;

            try
            {
                switch (type)
                {
                    case SearchProjectType.DirectoriesProject:
                        if (!DirectoryProjectFiles.GetPathsFilesList(paths, searchPattern, out listOfFiles, out result))
                        {
                            return false;
                        }
                        break;

                    case SearchProjectType.SolutionsProject:
                        if (!SolutionProjectFiles.GetSolutionsFilesList(paths, out listOfFiles, out result))
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

        #endregion

        #region Events Handlers

        public void OnMessage(string message, string method, string module, int line, AuditSeverity auditSeverity)
        {
            Message?.Invoke(message, method, module, line, auditSeverity);
        }

        #endregion

        #region Audit

        private void SearchListOfFiles_Message(string message, string method, string module, int line, AuditSeverity auditSeverity)
        {
            Audit(message, method, line, auditSeverity);
        }

        private void Audit(string message, string method, string module, int line, AuditSeverity auditSeverity)
        {
            OnMessage(message, method, module, line, auditSeverity);
        }

        private void Audit(string message, string method, int line, AuditSeverity auditSeverity)
        {
            string module = "Search Utils";

            Audit(message, method, module, line, auditSeverity);
        }

        public static int LINE([System.Runtime.CompilerServices.CallerLineNumber] int lineNumber = 0)
        {
            return lineNumber;
        }

        #endregion
    }
}
