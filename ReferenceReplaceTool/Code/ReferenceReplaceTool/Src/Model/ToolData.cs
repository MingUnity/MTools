using System.Collections.Generic;
using System;

namespace ReferenceReplaceTool
{
    [Serializable]
    public class ToolData
    {
        public List<string> csprojList = new List<string>();

        public List<string> dllList = new List<string>();
    }
}
