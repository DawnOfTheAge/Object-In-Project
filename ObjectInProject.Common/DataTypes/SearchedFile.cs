using System;
using System.Collections.Generic;

namespace ObjectInProject.Common
{
    public class SearchedFile
    {
        private delegate bool GetSearchResultsDelegate(out List<SearchResult> searchResults, out string result);

        #region Properties

        public string Solution
        {
            get;
            set;
        }

        public string Project
        {
            get;
            set;
        }

        public string RootDirectory
        {
            get;
            set;
        }

        public string Path
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public List<SearchedLine> Lines
        {
            get;
            set;
        }

        #endregion

        #region Data Members

        private SearchProjectType _searchProjectType;

        #endregion

        #region Constructors        

        public SearchedFile(string rootDirectory, string name, string path)
        {
            RootDirectory = rootDirectory;
            Lines = new List<SearchedLine>();
            Path = path;
            Name = name;

            _searchProjectType = SearchProjectType.DirectoriesProject;
        }

        public SearchedFile(string solution, string project, string name, string path)
        {
            Solution = solution;
            Project = project;
            Lines = new List<SearchedLine>();
            Path = path;
            Name = name;

            _searchProjectType = SearchProjectType.SolutionsProject;
        }

        #endregion

        #region Public Methods

        public bool AddLine(SearchedLine line, out string result)
        {
            result = string.Empty;

            try
            {
                if (line == null)
                {
                    result = "Trying To Add An Empty Line";

                    return false;
                }

                if (Lines == null)
                {
                    Lines = new List<SearchedLine>();
                }

                Lines.Add(line);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool GetSearchResults(out List<SearchResult> searchResults, out string result)
        {
            result = string.Empty;

            searchResults = null;

            try
            {
                if (Lines == null)
                {
                    result = "No Results Found";

                    return false;
                }

                GetSearchResultsDelegate getSearchResults;

                switch (_searchProjectType)
                {
                    case SearchProjectType.DirectoriesProject:
                        getSearchResults = GetDirectoryProjectSearchResults;
                        break;

                    case SearchProjectType.SolutionsProject:
                        getSearchResults = GetSolutionProjectSearchResults;
                        break;

                    default:
                        result = $"Wrong Search Project Type '{_searchProjectType}'";

                        return false;
                }

                if (!getSearchResults(out searchResults, out result))
                {
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

        #endregion

        #region Private Methods

        private bool GetSolutionProjectSearchResults(out List<SearchResult> searchResults, out string result)
        {
            result = string.Empty;

            searchResults = null;

            try
            {
                if (Lines == null)
                {
                    result = "No Results Found";

                    return false;
                }

                searchResults = new List<SearchResult>();

                foreach (SearchedLine line in Lines)
                {
                    SearchResult searchResult = new SearchResult(Solution, Project, Name, line.LineNumber, Path);

                    searchResults.Add(searchResult);
                }

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        private bool GetDirectoryProjectSearchResults(out List<SearchResult> searchResults, out string result)
        {
            result = string.Empty;

            searchResults = null;

            try
            {
                if (Lines == null)
                {
                    result = "No Results Found";

                    return false;
                }

                searchResults = new List<SearchResult>();

                foreach (SearchedLine line in Lines)
                {
                    SearchResult searchResult = new SearchResult(RootDirectory, Name, line.LineNumber, Path);

                    searchResults.Add(searchResult);
                }

                return true;
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
