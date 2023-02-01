namespace ObjectInProject.Common
{
    public class TagObject
    {        
        #region Properties

        public TagObjectType TagType { get; set; }

        public object Object { get; set; } 

        #endregion

        #region Constructors

        public TagObject(TagObjectType type)
        {
            TagType = type;
        }

        public TagObject(TagObjectType type, object tagObject)
        {
            TagType = type;
            Object = tagObject;
        }

        public TagObject(TagObjectType type, object tagObject, SearchProjectType projectType)
        {
            TagType = type;
            Object = tagObject;
        }

        #endregion
    }
}
