using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class SearchedFilesList
    {
        #region Properties        

        public List<SearchedFile> Files
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public SearchedFilesList()
        {
            Files = new List<SearchedFile>();
        }

        public SearchedFilesList(List<SearchedFile> files)
        {
            Files = files;
        }

        #endregion

        #region Public Methods

        public bool AddFile(SearchedFile file, out string result)
        {
            result = string.Empty;

            try
            {
                if (file == null)
                {
                    result = "Trying To Add An Empty File";

                    return false;
                }

                if (Files == null)
                {
                    Files = new List<SearchedFile>();
                }

                Files.Add(file);

                return true;
            }
            catch (Exception e)
            {
                result = e.Message;

                return false;
            }
        }

        public bool GetLines(out List<ExtendedSearchedLine> lines, out string result)
        {
            result = string.Empty;

            lines = null;

            try
            {
                if (Files == null)
                {
                    result = "No Lines Found";

                    return false;
                }

                lines = new List<ExtendedSearchedLine>();
                foreach (SearchedFile file in Files)
                {
                    if ((file != null) && (file.Lines != null) && (file.Lines.Count > 0))
                    {
                        foreach (SearchedLine line in file.Lines)
                        {
                            lines.Add(new ExtendedSearchedLine(line.LineNumber, line.Line, file.Name, file.Path));
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

        #endregion
    }
}
