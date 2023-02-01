using ObjectInProject.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ObjectInProject.Utils
{
    public static class StringUtils
    {
        public const int UNICODE_TO_HEBREW = 1264;

        public static string FindString(string[] lines, string text)
        {
            if ((lines == null) || (lines.Length == 0))
            {
                return string.Empty;
            }

            foreach (string line in lines)
            {
                if (line.IndexOf(text) != ObjectInProjectConstants.NONE)
                {
                    string result = line.Trim();

                    string[] subString = result.Split(':');

                    return subString[1].Trim();
                }
            }

            return string.Empty;
        }

        public static string InsertNewLineByIndex(string sOrigin, int iIndex)
        {
            return ReplaceSubStringByIndex(sOrigin, iIndex, "<NEWLINE>");
        }

        public static string ReplaceSubStringByIndex(string sOrigin, int iIndex, string sNewSubString)
        {
            #region Data Members

            string sPreString;
            string sPostString;

            #endregion

            try
            {
                if (iIndex >= sOrigin.Length)
                {
                    return "";
                }

                if (sNewSubString == "<NEWLINE>")
                {
                    sNewSubString = System.Environment.NewLine;
                }

                sPreString = sOrigin.Substring(0, iIndex);
                sPostString = sOrigin.Substring(iIndex + 1, sOrigin.Length - iIndex - 1);

                return sPreString + sNewSubString + sPostString;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static int GetNumberOfOccurences(string sWord, string sSubWord)
        {
            #region Data Members

            int count = 0;
            int i;

            #endregion

            for (i = 0; i < sWord.Length; i++)
            {
                if (sWord.Substring(i, 1) == sSubWord)
                {
                    count += 1;
                }
            }

            return count;
        }

        public static string GetNthSubString(string the_string, string sDelimiter, int index)
        {
            string[] sub_string;

            try
            {
                sub_string = the_string.Split(sDelimiter.ToCharArray());

                if (sub_string == null)
                {
                    return "";
                }

                if (index > sub_string.Length)
                {
                    return "";
                }

                return sub_string[index - 1];
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static int GetLastOccurencePosition(string s, string c)
        {
            #region Data Members

            int i;
            int iLength;
            int iGetLastOccurencePosition;

            #endregion

            iGetLastOccurencePosition = 0;
            iLength = s.Length;

            for (i = 0; i < iLength; i++)
            {
                if (s.Substring(i, 1) == c)
                {
                    iGetLastOccurencePosition = i;
                }
            }

            return iGetLastOccurencePosition;
        }

        public static int GetNthOccurencePosition(string s, string c, int index)
        {
            #region Data Members

            int i;
            int iLength;
            int iOccurenceCounter = 0;

            #endregion

            iLength = s.Length;

            for (i = 0; i < iLength; i++)
            {
                if (s.Substring(i, 1) == c)
                {
                    iOccurenceCounter += 1;
                    int iOccurencePosition = i;

                    if (iOccurenceCounter == index)
                    {
                        return iOccurencePosition;
                    }
                }
            }

            return -1;
        }

        public static string GetNthItem(string s, int number, string sDelimiter)
        {
            #region Data Members

            int i;
            int length;
            int front;
            int rear;
            int count;
            int iNumberOfOccurences;
            int iLastOccurencePosition;

            string sGetNthItem;
            string current_char;

            #endregion

            sGetNthItem = "";

            iNumberOfOccurences = GetNumberOfOccurences(s, sDelimiter);
            iLastOccurencePosition = GetLastOccurencePosition(s, sDelimiter);

            if (number == iNumberOfOccurences + 1)
            {
                sGetNthItem = s.Substring(iLastOccurencePosition, s.Length - iLastOccurencePosition);

                return sGetNthItem;
            }

            count = 0;
            length = s.Length;
            front = 0;

            try
            {
                for (i = 0; i < length; i++)
                {
                    current_char = s.Substring(i, 1);
                    if (current_char == sDelimiter)
                    {
                        rear = front;
                        front = i;
                        count++;
                        if (count == number)
                        {
                            sGetNthItem = s.Substring(rear + 1, front - rear - 1);
                            return sGetNthItem;
                        }
                    }
                }

                return sGetNthItem;
            }
            catch (Exception)
            {
                sGetNthItem = "";
                return sGetNthItem;
            }
        }

        public static bool GetAllSubStrings(string the_string, string sDelimiter, out string[] sSubString, out string sError)
        {
            sError = "";
            sSubString = null;

            try
            {
                sSubString = the_string.Split(sDelimiter.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

                if (sSubString == null)
                {
                    sError = "No Sub Strings For String[" + the_string + "] And Delimiter[" + sDelimiter + "]";

                    return false;
                }

                return true;
            }
            catch (Exception e)
            {
                sError = e.Message;

                return false;
            }
        }

        public static string[] GetAllSubStrings(string the_string, string sDelimiter)
        {
            try
            {
                return the_string.Split(sDelimiter.ToCharArray());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string FirstCharUpper(string word)
        {
            word = word.ToLower();
            word = word.Substring(0, 1).ToUpper() + word.Substring(1, word.Length - 1);

            return word;
        }

        public static bool Split(string text, char delimiter, out List<string> splits, out string result)
        {
            result = string.Empty;

            splits = new List<string>();

            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    result = "Nothing To Split. Null Or Empty.";

                    return false;
                }

                splits = text.Split(delimiter).ToList();

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool PrinterPageSplit(string line, bool removeDuplicates, out List<int> numbers, out string result)
        {
            result = string.Empty;

            numbers = new List<int>();

            try
            {
                if (string.IsNullOrEmpty(line))
                {
                    result = "Nothing To Split. Null Or Empty.";

                    return false;
                }

                List<string> splits = line.Split(',').ToList();

                foreach (string split in splits)
                {
                    string currentSplit = split.Trim();

                    if (string.IsNullOrEmpty(currentSplit))
                    {
                        continue;
                    }

                    if (currentSplit.Contains("-"))
                    {
                        List<string> fromTo = currentSplit.Split('-').ToList();
                        if (fromTo.Count != 2)
                        {
                            continue;
                        }

                        int from = int.TryParse(fromTo[0], out from) ? from : ObjectInProjectConstants.NONE;
                        int to = int.TryParse(fromTo[1], out to) ? to : ObjectInProjectConstants.NONE;

                        if ((from != ObjectInProjectConstants.NONE) && (to != ObjectInProjectConstants.NONE))
                        {
                            if (to >= from)
                            {
                                for (int i = from; i <= to; i++)
                                {
                                    numbers.Add(i);
                                }
                            }
                            else
                            {
                                for (int i = to; i <= from; i++)
                                {
                                    numbers.Add(i);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!int.TryParse(currentSplit, out int currentSplitNumber))
                        {
                            continue;
                        }

                        numbers.Add(currentSplitNumber);
                    }
                }

                if (removeDuplicates)
                {
                    numbers = numbers.Distinct().ToList();
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool NumbersToPrinterPage(List<int> numbers, out string line, out string result)
        {
            result = string.Empty;
            line = string.Empty;

            try
            {
                if ((numbers == null) || (numbers.Count == 0))
                {
                    result = "Numbers List Is Null Or Empty.";

                    return false;
                }

                numbers.Sort();

                List<List<int>> intervals = new List<List<int>>();

                foreach (int number in numbers)
                {
                    //  first number
                    if (intervals.Count == 0)
                    {
                        List<int> firstInterval = new List<int>() { number };
                        intervals.Add(firstInterval);

                        continue;
                    }

                    //  the number is in an interval OR 1 higher OR one lower
                    bool numberAdded = false;
                    for (int i = 0; i < intervals.Count && !numberAdded; i++)
                    {
                        int intervalMin = (intervals[i])[0];
                        int intervalMax = (intervals[i])[intervals[i].Count - 1];

                        if (((intervalMin <= number) && (number <= intervalMax)) ||
                            (number == (intervalMin - 1)) ||
                            (number == (intervalMax + 1)))
                        {
                            intervals[i].Add(number);
                            intervals[i].Sort();

                            numberAdded = true;
                        }
                    }

                    if (!numberAdded)
                    {
                        List<int> newInterval = new List<int>() { number };
                        intervals.Add(newInterval);
                    }
                    else
                    {
                        continue;
                    }
                }

                if (intervals.Count > 0)
                {
                    foreach (List<int> currentInterval in intervals)
                    {
                        string formerLine = string.IsNullOrEmpty(line) ? string.Empty : $"{line},";
                        switch (currentInterval.Count)
                        {
                            case 0:
                                break;

                            case 1:
                                line = $"{formerLine}{currentInterval[0]}";
                                break;

                            default:
                                line = $"{formerLine}{currentInterval[0]}-{currentInterval[currentInterval.Count - 1]}";
                                break;
                        }
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

        public static bool LinesFromText(string text, out List<string> lines, out string result)
        {
            result = string.Empty;

            lines = null;

            try
            {
                lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if ((lines == null) || (lines.Count == 0))
                {
                    result = "Text Is Null Or Empty";

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

        public static List<T> Intersect<T>(List<T> l1, List<T> l2)
        {
            try
            {
                if ((l1 == null) || (l1.Count == 0) || (l2 == null) || (l2.Count == 0))
                {
                    return null;
                }

                return l1.AsQueryable().Intersect(l2).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool CaseInsensitiveContains(List<string> list, string value)
        {
            if (list == null)
            {
                return false;
            }

            return list.Contains(value, StringComparer.OrdinalIgnoreCase);
        }

        public static bool CreateListWithCommas(List<string> list, out string listWithCommas, string quotationChar, out string result)
        {
            result = string.Empty;
            listWithCommas = string.Empty;

            try
            {
                if ((list == null) || (list.Count == 0))
                {
                    result = "List Is Empty";

                    return false;
                }

                foreach (string listItem in list)
                {
                    listWithCommas += string.Format("{0}{1}{0},", quotationChar, listItem);
                }

                listWithCommas = listWithCommas.Substring(0, listWithCommas.Length - 1);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static void SwapString(ref string s1, ref string s2)
        {
            string sSave;

            sSave = s1;
            s1 = s2;
            s2 = sSave;
        }

        public static string PreventDuplicateSpaces(string s)
        {
            #region Data Members

            int i;
            int iWordLength;
            string sLastCharacter = string.Empty;
            string sNewWord = string.Empty;

            #endregion

            try
            {
                iWordLength = s.Length;
                for (i = 0; i < iWordLength; i++)
                {
                    string sCharacter = s.Substring(i, 1);
                    if ((sLastCharacter == " ") && (sCharacter == " "))
                    {
                    }
                    else
                    {
                        sNewWord += sCharacter;
                        sLastCharacter = sCharacter;
                    }
                }

                return sNewWord.Trim();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool TrimManyToOne(string text, string delimiter, out string trimmedText, out string result)
        {
            result = string.Empty;
            trimmedText = string.Empty;

            try
            {
                trimmedText = text;
                while (trimmedText.Contains($"{delimiter}{delimiter}"))
                {
                    trimmedText = trimmedText.Replace($"{delimiter}{delimiter}", $"{delimiter}");
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool NumberOfCharInstancesInString(string text, char lookedUpChar, out int number, out string result)
        {
            result = string.Empty;
            number = ObjectInProjectConstants.NONE;

            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    result = "Text Is Null Or Emprt";

                    return false;
                }

                number = text.Count(currentChar => currentChar == lookedUpChar);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool NumberOfSubStringInstancesInString(string text, string lookedUpSubString, out int number, out string result)
        {
            result = string.Empty;
            number = ObjectInProjectConstants.NONE;

            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    result = "Text Is Null Or Emprt";

                    return false;
                }

                if (string.IsNullOrEmpty(lookedUpSubString))
                {
                    result = "Looked Up Sub String Is Null Or Emprt";

                    return false;
                }

                number = text.Split(new string[] { lookedUpSubString }, StringSplitOptions.None).Length - 1;

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static bool GetTextBetweenTwoSubStrings(string originalText, string startString, string endString, out string newText, out string result)
        {
            newText = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(originalText))
                {
                    result = "Original Text Is Null Or Emprt";

                    return false;
                }

                if (string.IsNullOrEmpty(startString))
                {
                    result = "Start String Is Null Or Emprt";

                    return false;
                }

                if (string.IsNullOrEmpty(endString))
                {
                    result = "End String Is Null Or Emprt";

                    return false;
                }

                if (!NumberOfSubStringInstancesInString(originalText, startString, out int numberOfStartStrings, out result))
                {
                    return false;
                }

                if (numberOfStartStrings != 1)
                {
                    result = $"Start Sub String '{startString}' Has {numberOfStartStrings} Occurences And Not Only 1";

                    return false;
                }

                if (!NumberOfSubStringInstancesInString(originalText, endString, out int numberOfEndStrings, out result))
                {
                    return false;
                }

                if (numberOfEndStrings != 1)
                {
                    result = $"End String '{endString}' Has {numberOfEndStrings} Occurences And Not Only 1";

                    return false;
                }

                int startIndex = originalText.IndexOf(startString) + startString.Length;
                int endIndex = originalText.IndexOf(endString);

                newText = originalText.Substring(startIndex, endIndex - startIndex);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public static string Strech(string s)
        {
            if (s.Length == 1)
            {
                return "0" + s;
            }
            else
            {
                return s;
            }
        }

        public static int GetFirstNumber(string sCommand, out int iPosition, out int iLength)
        {
            #region Data Members

            int iNumberStartPosition = -1;
            int i;
            int iNumber;

            bool bRc;

            string sNumber;

            #endregion

            if (string.IsNullOrEmpty(sCommand.Trim()))
            {
                iPosition = ObjectInProjectConstants.NONE;
                iLength = ObjectInProjectConstants.NONE;

                return ObjectInProjectConstants.NONE;
            }

            int iNumberEndPosition;
            for (i = 0; i < sCommand.Length; i++)
            {
                string sChar = sCommand.Substring(i, 1);

                bRc = Microsoft.VisualBasic.Information.IsNumeric(sChar);
                if (bRc)
                {
                    if (iNumberStartPosition == ObjectInProjectConstants.NONE)
                    {
                        iNumberStartPosition = i;
                    }
                }
                else
                {
                    if (iNumberStartPosition != -1)
                    {
                        iNumberEndPosition = i;

                        sNumber = sCommand.Substring(iNumberStartPosition, iNumberEndPosition - iNumberStartPosition);

                        try
                        {
                            iNumber = int.Parse(sNumber);
                        }
                        catch (Exception)
                        {
                            iNumber = ObjectInProjectConstants.NONE;
                        }

                        iPosition = iNumberStartPosition;
                        iLength = iNumberEndPosition - iNumberStartPosition;
                        return iNumber;
                    }
                }
            }

            if (iNumberStartPosition == ObjectInProjectConstants.NONE)
            {
                iPosition = -1;
                iLength = -1;

                return -1;
            }

            iNumberEndPosition = i;
            sNumber = sCommand.Substring(iNumberStartPosition, iNumberEndPosition - iNumberStartPosition);

            try
            {
                iNumber = int.Parse(sNumber);
            }
            catch (Exception)
            {
                iNumber = ObjectInProjectConstants.NONE;
            }

            iPosition = iNumberStartPosition;
            iLength = iNumberEndPosition - iNumberStartPosition;

            return iNumber;
        }

        public static void SwapByte(ref byte byteX, ref byte byteY)
        {
            byte byteTmp;

            byteTmp = byteX;
            byteX = byteY;
            byteY = byteTmp;
        }

        public static byte GetByte(byte[] buffer, int offset)
        {
            byte result;

            result = buffer[offset];

            return result;
        }

        public static int GetInteger(byte[] buffer, int offset)
        {
            int result;

            result = BitConverter.ToInt32(buffer, offset);

            return result;
        }

        public static int GetInteger16(byte[] buffer, int offset)
        {
            int result;

            result = BitConverter.ToInt16(buffer, offset);

            return result;
        }

        public static string GetString(byte[] buffer, int offset, int length)
        {
            #region Data Members

            string result = "";

            char c;
            int i;

            #endregion            

            if (buffer != null)
            {
                for (i = offset; i < (offset + length); i++)
                {
                    if (IsHebrewCharacter(buffer[i]))
                    {
                        c = Convert.ToChar(buffer[i] + UNICODE_TO_HEBREW);
                    }
                    else
                    {
                        c = Convert.ToChar(buffer[i]);
                    }

                    result += c.ToString();
                }
            }

            return result;
        }

        public static bool IsHebrewCharacter(byte bHeb)
        {
            //  from א to ת including the ך,ם,ן,ף,ץ
            if ((bHeb >= 224) && (bHeb <= 250))
            {
                return true;
            }

            return false;
        }

        public static void InsertByte(byte[] buffer, byte byte_to_insert, int offset)
        {
            if (buffer != null)
            {
                buffer[offset] = byte_to_insert;
            }
        }

        public static void InsertInteger(byte[] buffer, int integer_to_insert, int offset)
        {
            #region Data Members

            byte lsb;
            int ilsb;

            byte byte2;
            int ibyte2;

            byte byte3;
            int ibyte3;

            byte msb;
            long lmsb;

            #endregion

            if (buffer != null)
            {
                ilsb = integer_to_insert & 0x000000FF;
                lsb = (byte)ilsb;

                ibyte2 = integer_to_insert & 0x0000FF00;
                ibyte2 >>= 8;
                byte2 = (byte)ibyte2;

                ibyte3 = integer_to_insert & 0x00FF0000;
                ibyte3 >>= 16;
                byte3 = (byte)ibyte3;

                lmsb = integer_to_insert & 0xFF000000;
                lmsb >>= 24;
                msb = (byte)lmsb;

                buffer[offset] = lsb;
                buffer[offset + 1] = byte2;
                buffer[offset + 2] = byte3;
                buffer[offset + 3] = msb;
            }
        }

        public static void InsertString(byte[] buffer, string string_to_insert, int offset)
        {
            if (buffer != null)
            {
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] string_buffer = encoding.GetBytes(string_to_insert);

                InsertSubByteArray(buffer, string_buffer, offset);
            }
        }

        public static void InsertSubByteArray(byte[] buffer, byte[] buffer_to_insert, int offset)
        {
            #region Data Members

            int i;
            int j;

            #endregion

            if (buffer == null)
            {
                return;
            }

            if (buffer_to_insert == null)
            {
                return;
            }

            //  offset exceeds the buffer
            if (offset > buffer.Length)
            {
                return;
            }

            //  offset + length exceed the buffer
            if ((offset + buffer_to_insert.Length) > buffer.Length)
            {
                return;
            }

            j = 0;
            for (i = offset; i < offset + buffer_to_insert.Length; i++)
            {
                buffer[i] = buffer_to_insert[j];
                j++;
            }
        }

        public static byte[] GetSubByteArray(byte[] buffer, int offset, int length)
        {
            #region Data Members

            byte[] result;

            int i;
            int j;

            #endregion

            if (buffer == null)
            {
                return null;
            }

            //  offset exceeds the buffer
            if (offset > buffer.Length)
            {
                return null;
            }

            //  offset + length exceed the buffer
            if ((offset + length) > buffer.Length)
            {
                return null;
            }

            result = new byte[length];

            j = 0;
            for (i = offset; i < offset + length; i++)
            {
                result[j] = buffer[i];
                j++;
            }

            return result;
        }

        public static bool GetIpAddressBytes(string sAddress, out int[] Byte)
        {
            Byte = new int[4];

            for (int i = 0; i < 4; i++)
            {
                try
                {
                    string sByte = GetNthSubString(sAddress, ".", i + 1);
                    Byte[i] = int.Parse(sByte);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
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

        public static T GetValueFromDescription<T>(string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            return default;
        }

        public static Dictionary<T, string> GetEnumValuesAndDescrptions<T>()
        {
            Dictionary<T, string> enumValuesAndDescrptions = new Dictionary<T, string>();

            var enumValues = typeof(T).GetEnumValues();

            foreach (T value in enumValues)
            {
                MemberInfo memberInfo = typeof(T).GetMember(value.ToString()).First();

                var descriptionAttribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();

                if (descriptionAttribute != null)
                {
                    enumValuesAndDescrptions.Add(value, descriptionAttribute.Description);
                }
                else
                {
                    enumValuesAndDescrptions.Add(value, value.ToString());
                }
            }

            return enumValuesAndDescrptions;
        }
    }
}
