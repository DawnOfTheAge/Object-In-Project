using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public class TagObject
    {
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

        public TagObjectType TagType { get; set; }
        public object Object { get; set; }
    }
}
