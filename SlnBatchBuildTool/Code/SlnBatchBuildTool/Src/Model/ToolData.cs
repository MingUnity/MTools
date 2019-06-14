using System;
using System.Collections.Generic;

namespace SlnBatchBuildTool
{
    /// <summary>
    /// 工具数据
    /// </summary>
    [Serializable]
    public class ToolData
    {
        public List<string> slns = new List<string>();

        public int maxConcurrentCount = 4;
    }
}
