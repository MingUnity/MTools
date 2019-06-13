using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApkResignTool
{
    [Serializable]
    public struct ApkResignModel
    {
        public string keystore;

        public string password;

        public string alias;

        public string jdkDir;

        public string apk;
    }
}
