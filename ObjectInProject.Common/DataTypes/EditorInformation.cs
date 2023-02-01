namespace ObjectInProject.Common
{
    public class EditorInformation
    {        
        #region Properties

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

        #endregion

        #region Constructor

        public EditorInformation(string path, Editors editor, bool exists)
        {
            Path = path;
            Editor = editor;
            Exists = exists;
        } 

        #endregion
    }
}
