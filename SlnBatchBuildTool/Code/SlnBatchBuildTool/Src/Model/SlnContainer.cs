using System;
using System.Collections.Generic;

namespace SlnBatchBuildTool
{
    /// <summary>
    /// sln工程数据
    /// </summary>
    [Serializable]
    public class SlnContainer
    {
        public List<string> slns = new List<string>();
    }
}
