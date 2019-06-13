using Microsoft.Win32;
using System.Diagnostics;
using System.IO;

namespace SlnBatchBuildTool
{
    /// <summary>
    /// 编译工具
    /// </summary>
    public class Devenv : IBuilder
    {
        private string _path;

        /// <summary>
        /// 有效性
        /// </summary>
        public bool IsValid
        {
            get
            {
                bool res = false;

                if (!string.IsNullOrEmpty(_path) && File.Exists(_path))
                {
                    res = true;
                }

                return res;
            }
        }

        public Devenv()
        {
            _path = GetPath();
        }

        /// <summary>
        /// 编译
        /// </summary>
        public void Build(string sln)
        {
            if (!string.IsNullOrEmpty(sln))
            {
                string args = string.Format("\"{0}\" /Build \"Release\"", sln);

                Run(args);
            }
        }

        private void Run(string args)
        {
            using (Process vs = new Process())
            {
                ProcessStartInfo startinfo = new ProcessStartInfo();

                startinfo.FileName = _path;

                startinfo.Arguments = args;

                vs.StartInfo = startinfo;

                vs.Start();

                vs.WaitForExit();

                vs.Close();
            }
        }

        private string GetPath()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\devenv.exe");

            string result = regKey.GetValue(string.Empty).ToString();

            regKey.Close();

            return result;
        }
    }
}
