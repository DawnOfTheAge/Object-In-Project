using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectInProject.Common
{
    public static class ObjectInProjectConstants
    {
        public const int NONE = -1;
        
        public const string APPLICATION_NAME = "Object In Project Browser";

        public const string XML_EXTENSION = ".xml";
        public const string JSON_EXTENSION = ".json";
        public const string CS_FILE_EXTENSION = ".cs";
        public const string CPP_SOURCE_FILE_EXTENSION = ".cpp";
        public const string CPP_INCLUDE_FILE_EXTENSION = ".h";

        public const string CS_PROJECT_FILE_EXTENSION = ".csproj";
        public const string CPP_PROJECT_FILE_EXTENSION = ".vcxproj";

        public const string CS_PROPERTY = "Compile Include=";
        public const string CPP_SOURCE_FILE_PROPERTY = "ClCompile Include=";
        public const string CPP_INCLUDE_FILE_PROPERTY = "ClInclude Include=";

        public const string TEST_STRING = "test";

        public const string BATCH_FILE_NAME = "\\findVisualStudioPath.bat";

        public const string DEVENV_PATH_SUFFIX = "Common7\\IDE\\devenv.exe";
    }
}
