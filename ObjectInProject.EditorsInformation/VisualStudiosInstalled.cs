using General.Common;
using General.Log;
using ObjectInProject.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ObjectInProject.EditorsInformation
{
    public static class VisualStudiosInstalled
    {
        #region Local Constants

        private static string module = MethodBase.GetCurrentMethod().DeclaringType.Name;

        #endregion

        #region Constants

        public const string NOTEPAD_STRING = "notepad";
        public const string NOTEPAD_PLUS_PLUS_STRING = "notepad++";

        #endregion

        #region Data Members

        private static string m_vsWhereFullPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\vswhere.exe";

        #endregion

        #region Public Methods

        public static bool GetVisualStudiosInstalled(out List<EditorInformation> lVsStutio, out string result)
        {
            lVsStutio = null;
            result = string.Empty;

            try
            {
                #region get all existing visual studio paths using the vsWhere.exe

                if (!vsWhereExists())
                {
                    result = "vswhere.exe missing";

                    return false;
                }

                Process process = new Process();
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.FileName = m_vsWhereFullPath;
                startInfo.Arguments = "-all -legacy -property installationPath";

                process.StartInfo = startInfo;
                process.Start();

                string standardOutput = process.StandardOutput.ReadToEnd();
                string[] paths = standardOutput.Split(new[] { "\r\n" }, StringSplitOptions.None);

                #endregion

                if ((paths != null) && (paths.Length > 0))
                {
                    lVsStutio = new List<EditorInformation>();

                    if (NotepadExists())
                    {
                        lVsStutio.Add(new EditorInformation($"{NOTEPAD_STRING}", Common.Editors.Notepad, true));
                    }

                    if (NotepadPlusPlusExists())
                    {
                        lVsStutio.Add(new EditorInformation($"{NOTEPAD_PLUS_PLUS_STRING}", Common.Editors.NotepadPlusPlus, true));
                    }

                    foreach (string path in paths)
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            string finalPath;

                            if (path.Substring(path.Length - 1, 1) != @"\")
                            {
                                finalPath = $@"{path}\";
                            }
                            else
                            {
                                finalPath = path;
                            }

                            finalPath = $@"{finalPath}Common7\IDE\devenv.exe";

                            Editors editor = InquirePath(finalPath, out result);

                            if (editor != Editors.Unknown)
                            {
                                EditorInformation editorInformation = new EditorInformation(finalPath, editor, File.Exists(finalPath));

                                lVsStutio.Add(editorInformation);
                            }
                        }
                    }

                    return true;
                }

                result = "No Paths Found";

                return false;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }
        
        public  static bool OpenFileAtLine(string file, int line, Editors editor, out string result)
        {
            result = string.Empty;

            try
            {
                if (!File.Exists(file))
                {
                    result = $"File '{file}' Does Not Exist";

                    return false;
                }

                switch (editor)
                {
                    case Editors.Notepad:
                        if (!OpenWithNotepad(file, line, out result))
                        {
                            result = $"Failed Openning File '{file}' With '{Utils.EditorToString(editor)}'";

                            return false;
                        }
                        break;

                    case Editors.NotepadPlusPlus:
                        if (!OpenWithNotepadPlusPlus(file, line, out result))
                        {
                            result = $"Failed Openning File '{file}' With '{Utils.EditorToString(editor)}'";
                         
                            return false;
                        }
                        break;

                    case Editors.VisualStudio2005:
                    case Editors.VisualStudio2010:
                    case Editors.VisualStudio2013:
                    case Editors.VisualStudio2012:
                    case Editors.VisualStudio2017:
                    case Editors.VisualStudio2019:
                        if (!OpenWithVisualStudio(file, line, editor, out result))
                        {
                            result = $"Failed Openning File '{file}' With '{Utils.EditorToString(editor)}'";

                            return false;
                        }
                        break;

                    default:
                        result = $"Wrong Editor '{Utils.EditorToString(editor)}'";

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

        public static bool NotepadExists()
        {
            string result;

            int processId;

            if (!StartNotepad(out processId, out result))
            {
                return false;
            }
            else
            {
                if (processId != ObjectInProjectConstants.NONE)
                {
                    KillProcess(processId, out result);
                }
                else
                {
                    return false;
                }

                return true;
            }
        }

        public static bool NotepadPlusPlusExists()
        {
            string result;

            int processId;

            if (!StartNotepadPlusPlus(out processId, out result))
            {
                return false;
            }
            else
            {
                if (processId != ObjectInProjectConstants.NONE)
                {
                    KillProcess(processId, out result);
                }
                else
                {
                    return false;
                }

                return true;
            }
        }

        #endregion

        #region Notepad        
        
        private static bool StartNotepad(out int processId, out string result)
        {
            result = string.Empty;

            processId = ObjectInProjectConstants.NONE;

            try
            {
                Process notepadProcess = Process.Start($"{VisualStudiosInstalled.NOTEPAD_STRING}");

                processId = notepadProcess.Id;

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }
        
        private static bool OpenWithNotepad(string file, int line, out string result)
        {
            result = "";

            try
            {
                if (!File.Exists(file))
                {
                    result = $"File '{file}' Does Not Exist";

                    return false;
                }

                Process.Start("notepad.exe", file);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Notepad++

        private static bool StartNotepadPlusPlus(out int processId, out string result)
        {
            result = string.Empty;

            processId = ObjectInProjectConstants.NONE;

            try
            {
                Process notepadPlusPlusProcess = Process.Start($"{VisualStudiosInstalled.NOTEPAD_PLUS_PLUS_STRING}");

                processId = notepadPlusPlusProcess.Id;

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }
        
        private static bool OpenWithNotepadPlusPlus(string file, int line, out string result)
        {
            result = string.Empty;

            try
            {
                if (!File.Exists(file))
                {
                    result = $"File '{file}' Does Not Exist";

                    return false;
                }

                string arguments = $"\"{file}\" -n{line}";
                Process.Start("notepad++", arguments);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Visual Studio

        private static Editors InquirePath(string finalPath, out string result)
        {
            result = string.Empty;

            try
            {
                if (finalPath.IndexOf("2019") != Constants.NONE)
                {
                    return Editors.VisualStudio2019;
                }

                if (finalPath.IndexOf("2017") != Constants.NONE)
                {
                    return Editors.VisualStudio2017;
                }

                if (finalPath.IndexOf("8.0") != Constants.NONE)
                {
                    return Editors.VisualStudio2005;
                }

                if (finalPath.IndexOf("9.0") != Constants.NONE)
                {
                    return Editors.VisualStudio2008;
                }

                if (finalPath.IndexOf("10.0") != Constants.NONE)
                {
                    return Editors.VisualStudio2010;
                }

                if (finalPath.IndexOf("11.0") != Constants.NONE)
                {
                    return Editors.VisualStudio2012;
                }

                if (finalPath.IndexOf("12.0") != Constants.NONE)
                {
                    return Editors.VisualStudio2013;
                }

                if (finalPath.IndexOf("14.0") != Constants.NONE)
                {
                    return Editors.VisualStudio2015;
                }

                result = "No visual studio found";

                return Editors.Unknown;
            }
            catch (Exception e)
            {
                result = e.Message;

                return Editors.Notepad;
            }
        }

        private static bool vsWhereExists()
        {
            try
            {
                if (File.Exists(m_vsWhereFullPath))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool OpenWithVisualStudio(string file, int line, Editors editor, out string result)
        {
            #region Data Members

            string method = MethodBase.GetCurrentMethod().Name;

            object visualStudio;
            object ops;
            object window;
            object selection;

            #endregion

            result = string.Empty;

            try
            {
                if (!File.Exists(file))
                {
                    result = $"File '{file}' Does Not Exist";

                    return false;
                }

                switch (editor)
                {
                    case Editors.VisualStudio2005:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.9.0");
                        break;

                    case Editors.VisualStudio2010:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.10.0");
                        break;

                    case Editors.VisualStudio2012:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.11.0");
                        break;

                    case Editors.VisualStudio2013:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.12.0");
                        break;

                    case Editors.VisualStudio2015:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE.14.0");
                        break;

                    case Editors.VisualStudio2017:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE");
                        break;

                    case Editors.VisualStudio2019:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE");
                        break;

                    case Editors.VisualStudio2022:
                        visualStudio = Marshal.GetActiveObject("VisualStudio.DTE");
                        break;

                    default:
                        result = "Unknown editor type";
                        return false;
                }

                ops = visualStudio.GetType().InvokeMember("ItemOperations", BindingFlags.GetProperty, null, visualStudio, null);
                window = ops.GetType().InvokeMember("OpenFile", BindingFlags.InvokeMethod, null, ops, new object[] { file });
                selection = window.GetType().InvokeMember("Selection", BindingFlags.GetProperty, null, window, null);
                selection.GetType().InvokeMember("GotoLine", BindingFlags.InvokeMethod, null, selection, new object[] { line, true });

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        private static bool KillProcess(int processId, out string result)
        {
            result = string.Empty;

            try
            {
                Process[] processes = Process.GetProcesses();

                foreach (Process process in processes)
                {
                    if (process.Id == processId)
                    {
                        process.Kill();

                        return true;
                    }
                }

                result = $"No Process Found For Process ID [{processId}]";

                return false;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #region Audit

        private static void Audit(string message, string method, AuditSeverity auditSeverity, int line)
        {
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            string fileName = Log.FILE();

            Log.Audit(message, fileName, assemblyName, module, method, auditSeverity, line);
        }

        #endregion
    }
}

