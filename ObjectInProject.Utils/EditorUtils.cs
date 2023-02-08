using ObjectInProject.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ObjectInProject.Utils
{
    public static class EditorUtils
    {
        public static string EditorToString(Editors editor)
        {
            string result;

            switch (editor)
            {
                case Editors.NotepadPlusPlus:
                    result = "Notepad++";
                    break;

                case Editors.VisualStudio2005:
                    result = "Visual Studio 2005";
                    break;

                case Editors.VisualStudio2010:
                    result = "Visual Studio 2010";
                    break;

                case Editors.VisualStudio2012:
                    result = "Visual Studio 2012";
                    break;

                case Editors.VisualStudio2013:
                    result = "Visual Studio 2013";
                    break;

                case Editors.VisualStudio2015:
                    result = "Visual Studio 2015";
                    break;

                case Editors.VisualStudio2017:
                    result = "Visual Studio 2017";
                    break;

                case Editors.VisualStudio2019:
                    result = "Visual Studio 2019";
                    break;

                case Editors.VisualStudio2022:
                    result = "Visual Studio 2022";
                    break;

                default:
                    result = "Notepad";
                    break;
            }

            return result;
        }

        public static Editors EditorToEnum(string editor)
        {
            Editors result;

            switch (editor)
            {
                case "Notepad":
                    result = Editors.Notepad;
                    break;

                case "Notepad++":
                case "NotepadPlusPlus":
                    result = Editors.NotepadPlusPlus;
                    break;

                case "Visual Studio 2005":
                case "VisualStudio2005":
                    result = Editors.VisualStudio2005;
                    break;

                case "Visual Studio 2010":
                case "VisualStudio2010":
                    result = Editors.VisualStudio2010;
                    break;

                case "Visual Studio 2012":
                case "VisualStudio2012":
                    result = Editors.VisualStudio2012;
                    break;

                case "Visual Studio 2013":
                case "VisualStudio2013":
                    result = Editors.VisualStudio2013;
                    break;

                case "Visual Studio 2015":
                case "VisualStudio2015":
                    result = Editors.VisualStudio2015;
                    break;

                case "Visual Studio 2017":
                case "VisualStudio2017":
                    result = Editors.VisualStudio2017;
                    break;

                case "Visual Studio 2019":
                case "VisualStudio2019":
                    result = Editors.VisualStudio2019;
                    break;

                case "Visual Studio 2022":
                case "VisualStudio2022":
                    result = Editors.VisualStudio2022;
                    break;

                default:
                    result = Editors.Unknown;
                    break;
            }

            return result;
        }

        public static bool FindVisualStudioPaths(out List<string> visualStudioPaths, out string result)
        {
            result = string.Empty;
            visualStudioPaths = null;

            try
            {
                visualStudioPaths = Directory.GetFiles(@"c:\program files", "devenv.exe", SearchOption.AllDirectories).ToList();

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool VisualStudioPath(Editors visualStudioVersion, out string visualStudioPath, out string result)
        {
            result = string.Empty;
            visualStudioPath = string.Empty;

            try
            {
                switch (visualStudioVersion)
                {
                    case Editors.Notepad:
                    case Editors.NotepadPlusPlus:
                        result = $"'{visualStudioVersion}' Not A Visual Studio Version";
                        
                         return false;

                    case Editors.VisualStudio2005:
                        visualStudioPath = @"C:\Program Files\Microsoft Visual Studio 8\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2008:
                        visualStudioPath = @"C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2010:
                        visualStudioPath = @"C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2012:
                        visualStudioPath = @"C:\Program Files (x86)\Microsoft Visual Studio 11.0\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2013:
                        visualStudioPath = @"C:\Program Files (x86)\Microsoft Visual Studio 12.0\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2015:
                        visualStudioPath = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2017:
                        visualStudioPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2019:
                        visualStudioPath = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\IDE\devenv.exe";
                        break;

                    case Editors.VisualStudio2022:
                        visualStudioPath = @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe";
                        break;

                    default:
                        result = $"No Path Found For {visualStudioVersion}";

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
    }
}
