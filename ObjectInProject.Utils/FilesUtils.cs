using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System;

namespace ObjectInProject.Utils
{
    public static class FilesUtils
    {
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
    }
}
