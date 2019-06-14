using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApkResignTool
{
    public class WinRar : ICompressTool
    {
        private string _winRarExe;

        public WinRar()
        {
            _winRarExe = GetWinRarPath();
        }

        public void Compress(string[] srcs, string destZipFile, Action<bool, string> callback = null)
        {
            bool success = false;

            string tip = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(_winRarExe) && File.Exists(_winRarExe))
                {
                    string dir = Path.GetDirectoryName(destZipFile);

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string shellArguments = string.Format("a -ep1 -ibck -scul -r0 -iext -- \"{0}\" ", destZipFile);

                    if (srcs != null)
                    {
                        for (int i = 0; i < srcs.Length; i++)
                        {
                            string src = srcs[i];

                            if (!string.IsNullOrEmpty(src))
                            {
                                if (Directory.Exists(src) || File.Exists(src))
                                {
                                    shellArguments = string.Format("{0} \"{1}\"", shellArguments, src);
                                }
                            }
                        }
                    }

                    RunWinRar(shellArguments);

                    success = true;

                }
                else
                {
                    tip = "不存在winRar程序";
                }
            }
            catch
            {
                tip = "压缩报错";
            }

            callback?.Invoke(success, tip);
        }

        public void Delete(string zipFile, string deletePath, Action<bool, string> callback = null)
        {
            bool success = false;

            string tip = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(_winRarExe) && File.Exists(_winRarExe))
                {
                    if (!string.IsNullOrEmpty(zipFile) && File.Exists(zipFile))
                    {
                        string shellArguments = string.Format("d -ibck \"{0}\" \"{1}\"", zipFile, deletePath);

                        RunWinRar(shellArguments);

                        success = true;
                    }
                    else
                    {
                        tip = "不存在源压缩文件";
                    }
                }
                else
                {
                    tip = "不存在winRar程序";
                }
            }
            catch
            {
                tip = "压缩报错";
            }

            callback?.Invoke(success, tip);
        }

        public void UnCompress(string zipFile, string destDir, Action<bool, string> callback = null)
        {
            bool success = false;

            string tip = string.Empty;

            try
            {
                if (!string.IsNullOrEmpty(_winRarExe) && File.Exists(_winRarExe))
                {
                    if (!string.IsNullOrEmpty(zipFile) && File.Exists(zipFile))
                    {
                        //组合出需要shell的完整格式  
                        string shellArguments = string.Format("x -ibck -o+ \"{0}\" \"{1}\\\"", zipFile, destDir);

                        RunWinRar(shellArguments);

                        success = true;
                    }
                    else
                    {
                        tip = "源文件不存在";
                    }
                }
                else
                {
                    tip = "不存在winRar程序";
                }
            }
            catch
            {
                tip = "解压报错";
            }

            callback?.Invoke(success, tip);
        }

        private string GetWinRarPath()
        {
            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");

            string result = regKey.GetValue(string.Empty).ToString();

            regKey.Close();

            return result;
        }

        private void RunWinRar(string args)
        {
            using (Process unrar = new Process())
            {
                ProcessStartInfo startinfo = new ProcessStartInfo();

                startinfo.FileName = _winRarExe;

                startinfo.Arguments = args;  
                
                unrar.StartInfo = startinfo;

                unrar.Start();

                unrar.WaitForExit();

                unrar.Close();
            }
        }
    }
}
