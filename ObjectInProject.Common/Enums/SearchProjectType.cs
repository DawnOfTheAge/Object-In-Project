using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
