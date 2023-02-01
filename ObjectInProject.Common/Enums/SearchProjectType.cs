using System.ComponentModel;

namespace ObjectInProject.Common
{
    public enum SearchProjectType
    {
        [Description("Unknown Project")]
        Unknown = -1,

        [Description("Directories Project")]
        DirectoriesProject,

        [Description("Solutions Project")]
        SolutionsProject
    }
}
