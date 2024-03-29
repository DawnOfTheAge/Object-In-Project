﻿using ObjectInProject.Common;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

namespace ObjectInProject.Search
{
    public static class DirectoryProjectFiles
    {
        #region Public Methods

        public static bool GetPathsFilesList(List<string> paths, 
                                             string searchPatterns, 
                                             out List<FileOrigin> listOfFiles, 
                                             out string result)
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

                List<string> searchPatternsList = searchPatterns.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                listOfFiles = new List<FileOrigin>();
                foreach (string path in paths)
                {
                    if (!GetPathFilesList(path, searchPatternsList, out List<FileOrigin> currentListOfFiles, out result))
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

        #endregion

        #region Private Methods

        private static bool GetPathFilesList(string path, List<string> searchPatterns, out List<FileOrigin> listOfFiles, out string result)
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

                if ((searchPatterns == null) || (searchPatterns.Count == 0))
                {
                    result = "No Search Patterns";

                    return false;
                }
                
                List<string> files = new List<string>();
                foreach (string searchPattern in searchPatterns)
                {
                    files.AddRange(Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories).ToList());
                }

                if ((files == null) || (files.Count == 0))
                {
                    result = "No Files Found";

                    return false;
                }

                listOfFiles = new List<FileOrigin>();
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
    }
}
