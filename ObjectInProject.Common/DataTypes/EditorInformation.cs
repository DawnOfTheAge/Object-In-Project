using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class EditorInformation
    {
        public EditorInformation(string path, Editors editor, bool exists)
        {
            Path = path;
            Editor = editor;
            Exists = exists;
        }

        public string Path
        {
            get;
            set;
        }

        public Editors Editor
        {
            get;
            set;
        }

        public bool Exists
        {
            get;
            set;
        }
    }
}
