﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace SlnBatchBuildTool
{
    /// <summary>
    /// 编译工具
    /// </summary>
    public class Devenv : IBuilder
    {
        private string _path;

        /// <summary>
        /// 异步编译队列
        /// </summary>
        private Queue<string> _asyncBuildQueue;

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

        public void ConcurrentBuild(string[] slns, int maxConcurrentCount = 4)
        {
            if (slns != null && slns.Length > 0)
            {
                _asyncBuildQueue = new Queue<string>(slns);

                RunTask(maxConcurrentCount);
            }

            _asyncBuildQueue.Clear();

            _asyncBuildQueue = null;
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
            string result = string.Empty;

            GetPathBySxs(out result);

            return result;
        }

        private bool GetPathByAppPaths(out string path)
        {
            bool res = false;

            path = null;

            RegistryKey regKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\devenv.exe");

            if (regKey != null)
            {
                path = regKey.GetValue(string.Empty).ToString();

                regKey.Close();

                res = true;
            }

            return res;
        }

        private bool GetPathBySxs(out string path)
        {
            bool res = false;

            path = null;

            RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, Environment.Is64BitOperatingSystem ? RegistryView.Registry64 : RegistryView.Registry32);

            string registryPath = @"SOFTWARE\Wow6432Node\Microsoft\VisualStudio\SxS\VS7";

            Dictionary<string, string> vsPaths = ReadRegistryInfo(key, registryPath);

            string highestVSdevenvPath = string.Empty;

            if (vsPaths != null && vsPaths.Any())
            {
                int tempVersion = 0;

                foreach (KeyValuePair<string, string> kvp in vsPaths)
                {
                    string devenvExePath = Path.Combine(kvp.Value, @"Common7\IDE\devenv.exe");

                    if (File.Exists(devenvExePath))
                    {
                        int currentVersion = Convert.ToInt32(kvp.Key.Split('.')[0]);

                        if (currentVersion > tempVersion)
                        {
                            tempVersion = currentVersion;

                            highestVSdevenvPath = devenvExePath;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(highestVSdevenvPath))
                {
                    path = highestVSdevenvPath;

                    res = true;
                }
            }

            return res;
        }

        private Dictionary<string, string> ReadRegistryInfo(RegistryKey registryKey, string registryInfoPath)
        {
            if (registryKey == null || string.IsNullOrEmpty(registryInfoPath))
            {
                return null;
            }

            try
            {
                RegistryKey rsg = registryKey.OpenSubKey(registryInfoPath, false);

                if (rsg != null)
                {
                    var keyNameArray = rsg?.GetValueNames();

                    Dictionary<string, string> result = new Dictionary<string, string>();

                    foreach (var name in keyNameArray)
                    {
                        string keyValue = (string)rsg.GetValue(name);

                        result.Add(name, keyValue);
                    }

                    rsg.Close();

                    return result;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private void RunTask(int maxConcurrentCount)
        {
            if (maxConcurrentCount <= 0)
            {
                maxConcurrentCount = 1;
            }

            int curConcurrentCount = 0;

            while (_asyncBuildQueue.Count > 0 || curConcurrentCount > 0)
            {
                if (_asyncBuildQueue.Count > 0 && curConcurrentCount < maxConcurrentCount)
                {
                    string sln = string.Empty;

                    lock (_asyncBuildQueue)
                    {
                        sln = _asyncBuildQueue.Dequeue();
                    }

                    curConcurrentCount++;

                    AsyncBuild(sln, () =>
                    {
                        curConcurrentCount--;
                    });
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

        private void AsyncBuild(string sln, Action callback = null)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Build(sln);

                callback?.Invoke();
            });
        }
    }
}
