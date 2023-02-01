using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System;

namespace ObjectInProject.Utils
{
    public static class GeneralUtils
    {
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
