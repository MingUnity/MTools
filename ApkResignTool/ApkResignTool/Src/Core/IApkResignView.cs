using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApkResignTool
{
    public interface IApkResignView
    {
        IApkResignController Controller { get; set; }

        string Keystore { get; set; }

        string Password { get; set; }

        string Alias { get; set; }

        string JdkDir { get; set; }

        string Apk { get; set; }

        string Tip { get; set; }

        bool SignEnabled { get; set; }
    }
}
