using ObjectInProject.Common;

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
    }
}
