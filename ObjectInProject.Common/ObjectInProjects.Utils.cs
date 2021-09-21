using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ObjectInProject.Common
{
    public static class Utils
    {
        #region Read / Write To Text Files

        public static bool ReadFileLines(string filename, out List<string> lines, out string result)
        {
            string method = ":{" + MethodBase.GetCurrentMethod().Name + "}: ";
            string line;

            lines = null;
            result = "";

            try
            {
                if (!File.Exists(filename))
                {
                    return false;
                }

                lines = new List<string>();

                StreamReader file = new StreamReader(filename);
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }

                file.Close();

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool WriteLinesToFile(string filename, List<string> lines, out string result)
        {
            string method = ":{" + MethodBase.GetCurrentMethod().Name + "}: ";

            result = "";

            try
            {
                StreamWriter file = new StreamWriter(filename, true);
                foreach (string line in lines)
                {
                    file.WriteLine(line);
                }

                file.Close();

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        #endregion

        #region Read / Write To XML Files

        public static bool WriteToXmlFile<T>(string sFilePath, T objectToWrite, bool bAppend = false) where T : new()
        {
            TextWriter twTextWriter = null;

            try
            {
                var vSerializer = new XmlSerializer(typeof(T));
                twTextWriter = new StreamWriter(sFilePath, bAppend);
                vSerializer.Serialize(twTextWriter, objectToWrite);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
            finally
            {
                if (twTextWriter != null)
                {
                    twTextWriter.Close();
                }
            }
        }

        public static T ReadFromXmlFile<T>(string sFilePath) where T : new()
        {
            TextReader trTextReader = null;

            try
            {
                var vSerializer = new XmlSerializer(typeof(T));
                trTextReader = new StreamReader(sFilePath);

                return (T)vSerializer.Deserialize(trTextReader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return default(T);
            }
            finally
            {
                if (trTextReader != null)
                {
                    trTextReader.Close();
                }
            }
        }

        #endregion

        #region Editors string <==> enum

        public static string EditorToString(Editors editor)
        {
            string result = "";

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

                default:
                    result = "Notepad";
                    break;
            }

            return result;
        }

        public static Editors EditorToEnum(string editor)
        {
            Editors result = Editors.Unknown;

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

                default:
                    result = Editors.Unknown;
                    break;
            }

            return result;
        }

        #endregion

        public static int GetLineNumber(Exception ex)
        {
            try
            {
                var st = new StackTrace(ex, true);

                // Get the top stack frame
                var frame = st.GetFrame(0);

                // Get the line number from the stack frame
                return frame.GetFileLineNumber();
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            try
            {
                FieldInfo fi = value.GetType().GetField(value.ToString());

                DescriptionAttribute[] attributes =
                    (DescriptionAttribute[])fi.GetCustomAttributes(
                    typeof(DescriptionAttribute),
                    false);

                if (attributes != null &&
                    attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }
            catch (Exception e)
            {
                return "Error. " + e.Message;
            }
        }
    }
}
