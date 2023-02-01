using General.Common;
using System;
using System.Collections.Generic;

namespace ObjectInProject.Common
{
    [Serializable]
    public class SearchProjects : EventArgs
    {       
        #region Properties

        public int ActiveSearchProjectIndex
        {
            get;
            set;
        }

        public int ActiveSearchProjectRealIndex
        {
            get;
            set;
        }

        public List<SearchProject> SearchProjectsList
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public SearchProjects()
        {
            SearchProjectsList = new List<SearchProject>();

            ActiveSearchProjectIndex = Constants.NONE;
        }

        #endregion

        #region Private Methods

        private int GetIndex()
        {
            try
            {
                if (SearchProjectsList == null)
                {
                    SearchProjectsList = new List<SearchProject>();
                }

                int maximumIndex = 0;

                for (int i = 0; i < SearchProjectsList.Count; i++)
                {
                    if (SearchProjectsList[i].Index > maximumIndex)
                    {
                        maximumIndex = SearchProjectsList[i].Index;
                    }
                }

                return maximumIndex + 1;
            }
            catch (Exception)
            {
                return Constants.NONE;
            }
        }

        #endregion

        #region Public Methods

        public bool Add(SearchProject searchProject, out int searchProjectIndex, out string result)
        {
            searchProjectIndex = Constants.NONE;

            result = string.Empty;

            try
            {
                if (SearchProjectsList == null)
                {
                    result = "Search Projects List Is Null Or Empty";

                    return false;
                }

                if (!Exists(searchProject.Name, out searchProjectIndex))
                {
                    searchProjectIndex = GetIndex();
                    searchProject.Index = searchProjectIndex;
                    SearchProjectsList.Add(searchProject);

                    return true;
                }

                result = $"Search Project '{searchProject.Name}' Already Exist";

                return false;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool Exists(string searchProjectName, out int searchProjectIndex)
        {
            searchProjectIndex = Constants.NONE;

            if (SearchProjectsList == null)
            {
                return false;
            }

            for (int i = 0; i < SearchProjectsList.Count; i++)
            {
                if (SearchProjectsList[i].Name.ToLower() == searchProjectName.ToLower())
                {
                    searchProjectIndex = i;

                    return true;
                }
            }

            return false;
        }

        public bool Exists(int searchProjectIndex, out int realIndex)
        {
            realIndex = Constants.NONE;

            if (SearchProjectsList == null)
            {
                return false;
            }

            for (int i = 0; i < SearchProjectsList.Count; i++)
            {
                if (SearchProjectsList[i].Index == searchProjectIndex)
                {
                    realIndex = i;

                    return true;
                }
            }

            return false;
        }

        public bool GetByName(string searchProjectName, out SearchProject searchProject, out string result)
        {
            result = string.Empty;

            searchProject = null;

            if (string.IsNullOrEmpty(searchProjectName))
            {
                result = "Search Project Name Is Null Or Empty";

                return false;
            }

            int searchProjetIndex;
            if (!Exists(searchProjectName, out searchProjetIndex))
            {
                result = $"Search Project '{searchProjectName}' Does Not Exist";

                return false;
            }

            searchProject = SearchProjectsList[searchProjetIndex];

            return true;
        }

        public bool Update(SearchProject searchProject, out string result)
        {
            result = string.Empty;

            try
            {
                if (SearchProjectsList == null)
                {
                    result = "Search Projects List Is Null";

                    return false;
                }

                if (searchProject == null)
                {
                    result = "Search Project To Update Is Null";

                    return false;
                }

                int realIndex;
                if (!Exists(searchProject.Index, out realIndex))
                {
                    result = $"Search Project With Index [{searchProject.Index}] Does Not Exist";

                    return false;
                }

                if (realIndex == Constants.NONE)
                {
                    result = "Index Not Found";

                    return false;
                }

                SearchProjectsList[realIndex] = searchProject;

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool Delete(string searchProjectName, out string result)
        {
            result = string.Empty;

            try
            {
                if (SearchProjectsList == null)
                {
                    result = "Search Projects List Is Null";

                    return false;
                }

                if (string.IsNullOrEmpty(searchProjectName))
                {
                    result = "Search Project Name To Delete Is Null";

                    return false;
                }

                int realIndex;
                if (!Exists(searchProjectName, out realIndex))
                {
                    result = $"Search Project With Name [{searchProjectName}] Does Not Exist";

                    return false;
                }

                if (realIndex == Constants.NONE)
                {
                    result = "Index Not Found";

                    return false;
                }

                SearchProjectsList.RemoveAt(realIndex);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool DeleteAll(SearchProjectType searchProjectType, out string result)
        {
            result = string.Empty;

            try
            {
                if (SearchProjectsList == null)
                {
                    result = "Search Projects List Is Null";

                    return false;
                }

                if (searchProjectType == SearchProjectType.Unknown)
                {
                    SearchProjectsList.Clear();
                }
                else
                {
                    List<int> indexesToBeRemoved = new List<int>();
                    for (int i = 0; i < SearchProjectsList.Count; i++)
                    {
                        if (SearchProjectsList[i].Type == searchProjectType)
                        {
                            indexesToBeRemoved.Add(i);
                        }
                    }

                    indexesToBeRemoved.Reverse();
                    for (int i = 0; i < indexesToBeRemoved.Count; i++)
                    {
                        SearchProjectsList.RemoveAt(i);
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

        public bool SetAsActive(string searchProjectName, out string result)
        {
            result = string.Empty;

            try
            {
                if (SearchProjectsList == null)
                {
                    result = "Search Projects List Is Null Or Empty";

                    return false;
                }

                int searchProjectRealIndex;
                if (Exists(searchProjectName, out searchProjectRealIndex))
                {
                    ActiveSearchProjectRealIndex = searchProjectRealIndex;
                    ActiveSearchProjectIndex = SearchProjectsList[searchProjectRealIndex].Index;

                    return true;
                }

                result = $"Search Project '{searchProjectName}' Does Not Exist";

                return false;
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
