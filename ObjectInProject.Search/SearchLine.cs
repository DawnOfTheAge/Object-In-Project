using ObjectInProject.Common;
using System.Collections.Generic;
using System;

namespace ObjectInProject.Search
{
    public static class SearchLine
    {
        #region Public Methods

        public static bool SearchInLine(string line,
                                        List<string> tokens,
                                        SearchLogic searchLogic,
                                        bool caseSensitive,
                                        out string result)
                {
                    try
                    {
                        switch (searchLogic)
                        {
                            case SearchLogic.And:
                                return SearchInLineAnd(line, tokens, caseSensitive, out result);

                            case SearchLogic.Or:
                                return SearchInLineOr(line, tokens, caseSensitive, out result);

                            default:
                                result = $"Wrong Search Logic Parameter - '{searchLogic}'";
                                break;
                        }

                        return false;
                    }
                    catch (Exception e)
                    {
                        result = e.Message;

                        return false;
                    }
                }

        #endregion

        #region Private Methods

        private static bool SearchInLineAnd(string line, List<string> tokens, bool caseSensitive, out string result)
        {
            result = string.Empty;

            try
            {
                if ((tokens == null) || (tokens.Count == 0))
                {
                    return false;
                }

                if (!caseSensitive)
                {
                    line = line.ToLower();
                }

                bool success = true;

                foreach (string token in tokens)
                {
                    string currentToken = token;

                    if (!caseSensitive)
                    {
                        currentToken = currentToken.ToLower();
                    }

                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        bool contains = line.Contains(currentToken);
                        success &= contains;
                    }
                }

                return success;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private static bool SearchInLineOr(string line, List<string> tokens, bool caseSensitive, out string result)
        {
            result = string.Empty;

            try
            {
                if ((tokens == null) || (tokens.Count == 0))
                {
                    return false;
                }

                if (!caseSensitive)
                {
                    line = line.ToLower();
                }

                bool success = false;

                foreach (string token in tokens)
                {
                    string currentToken = token;

                    if (!caseSensitive)
                    {
                        currentToken = currentToken.ToLower();
                    }

                    if (!string.IsNullOrEmpty(currentToken))
                    {
                        bool contains = line.Contains(currentToken);
                        success |= contains;
                    }
                }

                return success;
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
