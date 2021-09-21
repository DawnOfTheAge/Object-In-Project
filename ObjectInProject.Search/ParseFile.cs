using General.Common;
using Microsoft.Build.Construction;
using ObjectInProject.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Search
{
    public static class ParseFile
    {
        public static  bool ParseSolutionFile(string solutionFilename, out List<string> projectFiles, out string result)
        {
            result = string.Empty;

            projectFiles = null;

            try
            {
                SolutionFile solutionFile = SolutionFile.Parse(solutionFilename);

                if ((solutionFile == null) || (solutionFile.ProjectsInOrder == null) || (solutionFile.ProjectsInOrder.Count == 0))
                {
                    result = "Solution File Is Null Or Empty";

                    return false;
                }

                projectFiles = new List<string>();
                foreach (var currentProjectFile in solutionFile.ProjectsInOrder)
                {
                    projectFiles.Add(currentProjectFile.AbsolutePath);
                }

                return true;
            }
            catch (Exception e)
            {
                result = $"Parse Solution File Text Error. {e.Message}";

                return false;
            }
        }

        public static bool ParseProjectFile(string projectFile, out List<string> codeFiles, out string result)
        {
            result = string.Empty;

            codeFiles = null;

            try
            {
                if (!string.IsNullOrEmpty(projectFile))
                {
                    if (!File.Exists(projectFile))
                    {
                        result = $"File '{projectFile}' Does Not Exist";                        

                        return false;
                    }
                                        
                    string projectFileExtension = Path.GetExtension(projectFile);
                    
                    switch (projectFileExtension.ToLower())
                    {
                        case ObjectInProjectConstants.CS_PROJECT_FILE_EXTENSION:
                            return ParseProjectCsFile(projectFile, out codeFiles, out result);

                        case ObjectInProjectConstants.CPP_PROJECT_FILE_EXTENSION:
                            return ParseProjectCppFile(projectFile, out codeFiles, out result);

                        default:
                            result = $"Wrong Project Type '{projectFileExtension}'";

                            return false;
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

        private static bool ParseProjectCsFile(string projectFile, out List<string> csFiles, out string result)
        {
            result = string.Empty;

            csFiles = null;

            try
            {
                if (!File.Exists(projectFile))
                {
                    result = $"File '{projectFile}' Does Not Exist";

                    return false;
                }

                string projectFilePath = Path.GetDirectoryName(projectFile);
                string projectFileData = File.ReadAllText(projectFile);

                if (string.IsNullOrEmpty(projectFileData))
                {
                    result = "Project File Is Null Or Empty";

                    return false;
                }

                bool goOn = true;

                int startFileIndex;
                int endFileIndex;
                int projectFileTextIndex = 0;

                csFiles = new List<string>();

                while (goOn)
                {
                    startFileIndex = projectFileData.IndexOf(ObjectInProjectConstants.CS_PROPERTY, projectFileTextIndex);
                    if (startFileIndex == Constants.NONE)
                    {
                        break;
                    }

                    endFileIndex = projectFileData.IndexOf(ObjectInProjectConstants.CS_FILE_EXTENSION, startFileIndex);
                    if (endFileIndex == Constants.NONE)
                    {
                        break;
                    }

                    csFiles.Add(projectFilePath + "\\" + projectFileData.Substring(startFileIndex + (ObjectInProjectConstants.CS_PROPERTY.Length + 1),
                                                                                   endFileIndex - startFileIndex - (ObjectInProjectConstants.CS_PROPERTY.Length - ObjectInProjectConstants.CS_FILE_EXTENSION.Length + 1)));

                    projectFileTextIndex = endFileIndex + 3;

                    if (projectFileTextIndex >= projectFileData.Length)
                    {
                        goOn = false;
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

        private static bool ParseProjectCppFile(string projectFile, out List<string> cppFiles, out string result)
        {
            result = string.Empty;

            cppFiles = null;

            try
            {
                if (!File.Exists(projectFile))
                {
                    result = $"File '{projectFile}' Does Not Exist";

                    return false;
                }

                string projectFilePath = Path.GetDirectoryName(projectFile);
                string projectFileData = File.ReadAllText(projectFile);

                if (string.IsNullOrEmpty(projectFileData))
                {
                    result = "Project File Is Null Or Empty";

                    return false;
                }

                bool goOn = true;

                int startFileIndex;
                int endFileIndex;
                int projectFileTextIndex = 0;

                cppFiles = new List<string>();

                while (goOn)
                {
                    startFileIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY, projectFileTextIndex);
                    if (startFileIndex == Constants.NONE)
                    {
                        break;
                    }

                    endFileIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_INCLUDE_FILE_EXTENSION, startFileIndex);
                    if (endFileIndex == Constants.NONE)
                    {
                        break;
                    }

                    cppFiles.Add(projectFilePath + "\\" + projectFileData.Substring(startFileIndex + (ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY.Length + 1),
                                                                                    endFileIndex - startFileIndex - (ObjectInProjectConstants.CPP_INCLUDE_FILE_PROPERTY.Length - ObjectInProjectConstants.CPP_INCLUDE_FILE_EXTENSION.Length + 1)));

                    projectFileTextIndex = endFileIndex + 3;

                    if (projectFileTextIndex >= projectFileData.Length)
                    {
                        goOn = false;
                    }
                }

                goOn = true;
                while (goOn)
                {
                    startFileIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY, projectFileTextIndex);
                    if (startFileIndex == Constants.NONE)
                    {
                        break;
                    }

                    endFileIndex = projectFileData.IndexOf(ObjectInProjectConstants.CPP_SOURCE_FILE_EXTENSION, startFileIndex);
                    if (endFileIndex == Constants.NONE)
                    {
                        break;
                    }

                    cppFiles.Add(projectFilePath + "\\" + projectFileData.Substring(startFileIndex + (ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY.Length + 1),
                                                                                    endFileIndex - startFileIndex - (ObjectInProjectConstants.CPP_SOURCE_FILE_PROPERTY.Length - ObjectInProjectConstants.CPP_SOURCE_FILE_EXTENSION.Length + 1)));

                    projectFileTextIndex = endFileIndex + 3;

                    if (projectFileTextIndex >= projectFileData.Length)
                    {
                        goOn = false;
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
    }
}
