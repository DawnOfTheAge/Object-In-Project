using System.IO;
using System.Xml.Serialization;
using System;

namespace ObjectInProject.Utils
{
    public static class XmlUtils
    {
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
    }
}
