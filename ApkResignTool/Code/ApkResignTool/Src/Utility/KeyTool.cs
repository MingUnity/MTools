using System;
using System.Diagnostics;
using System.IO;

namespace ApkResignTool
{
    public class KeyTool
    {
        public string keyToolPath;

        public string alias;

        public string password;

        public void Create(Action<Result, string> callback = null)
        {
            Result result = CheckValid();

            string keystorePath = string.Empty;

            if (result == Result.Success)
            {
                string keystore = "android.keystore";

                keystorePath = string.Format("{0}/{1}", Environment.CurrentDirectory, keystore);

                if (File.Exists(keystorePath))
                {
                    result |= Result.KeystoreExisted;
                }
                else
                {
                    //创建keystore
                    string args = string.Format("-genkey -alias {0} -keyalg RSA -validity 36500 -keystore {2}  -keypass {1} -storepass {1}", alias, password, keystore);

                    RunKeyTool(args);

                    if (!File.Exists(keystorePath))
                    {
                        result |= Result.CreateFailed;
                    }
                }
            }

            callback?.Invoke(result, keystorePath);
        }

        private void RunKeyTool(string args)
        {
            using (Process process = new Process())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = keyToolPath;

                startInfo.Arguments = args;

                process.StartInfo = startInfo;

                process.Start();

                process.WaitForExit();

                process.Close();
            }
        }

        private Result CheckValid()
        {
            Result result = Result.Success;

            if (string.IsNullOrEmpty(keyToolPath) || !File.Exists(keyToolPath))
            {
                result |= Result.NotExistKeyTool;
            }

            if (string.IsNullOrEmpty(alias))
            {
                result |= Result.EmptyAlias;
            }

            if (string.IsNullOrEmpty(password))
            {
                result |= Result.EmptyPassword;
            }
            else if (password.Length < 6)
            {
                result |= Result.LittlePassword;
            }

            return result;
        }

        [Flags]
        public enum Result
        {
            /// <summary>
            /// 成功
            /// </summary>
            Success = 0,

            /// <summary>
            /// keytool不存在
            /// </summary>
            NotExistKeyTool = 1,

            /// <summary>
            /// 空别名
            /// </summary>
            EmptyAlias = 2,

            /// <summary>
            /// 空密码
            /// </summary>
            EmptyPassword = 4,

            /// <summary>
            /// 密码过短
            /// </summary>
            LittlePassword = 8,

            /// <summary>
            /// 已存在秘钥
            /// </summary>
            KeystoreExisted = 16,

            /// <summary>
            /// 创建失败
            /// </summary>
            CreateFailed = 32
        }
    }
}
