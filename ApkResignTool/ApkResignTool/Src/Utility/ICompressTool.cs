using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApkResignTool
{
    /// <summary>
    /// 解压缩工具接口
    /// </summary>
    public interface ICompressTool
    {
        void Compress(string[] srcs, string destZipFile, Action<bool, string> callback = null);

        void UnCompress(string zipFile, string destDir, Action<bool, string> callback = null);

        void Delete(string zipFile, string deletePath, Action<bool, string> callback = null);
    }
}
