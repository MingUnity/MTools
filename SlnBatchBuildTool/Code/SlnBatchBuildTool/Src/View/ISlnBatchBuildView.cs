using System.Collections.Generic;

namespace SlnBatchBuildTool
{
    public interface ISlnBatchBuildView
    {
        ISlnBatchBuildController Controller { get; set; }

        /// <summary>
        /// 被选中的索引
        /// </summary>
        int[] SelectedIndex { get; set; }

        /// <summary>
        /// 列表元素
        /// </summary>
        string[] ListItems { set; }

        /// <summary>
        /// 编译可用
        /// </summary>
        bool BuildEnabled { get; set; }

        /// <summary>
        /// 编译按钮内容
        /// </summary>
        string BuildContent { get; set; }

        /// <summary>
        /// 最大并发数
        /// </summary>
        int MaxConcurrentCount { get; set; }
    }
}
