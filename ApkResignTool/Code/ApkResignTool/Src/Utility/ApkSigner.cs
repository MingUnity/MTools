using System;
using System.Diagnostics;
using System.IO;

namespace ApkResignTool
{
    public class ApkSigner
    {
        public string jarSignerPath;

        public string keystore;

        public string password;

        public string apk;

        public string alias;

        public string emptyMetaInfPath;

        public ICompressTool _compressTool;

        private readonly string METAINF = "META-INF";

        public void Sign(Action<bool, string> callback = null)
        {
            if (!string.IsNullOrEmpty(apk) && File.Exists(apk))
            {
                if (!string.IsNullOrEmpty(emptyMetaInfPath) && Directory.Exists(emptyMetaInfPath))
                {
                    string outputApk = GenerateSignedApkPath(apk);

                    if (!string.IsNullOrEmpty(outputApk) && File.Exists(outputApk))
                    {
                        File.Delete(outputApk);
                    }

                    _compressTool?.Compress(new string[] { emptyMetaInfPath }, apk, (aSuccess, aTip) =>
                    {
                        if (aSuccess)
                        {
                            _compressTool?.Delete(apk, METAINF, (bSuccess, bTip) =>
                            {
                                if (bSuccess)
                                {
                                    string args = string.Format("-verbose -keystore {0} -storepass {1} -signedjar {2} {3} {4}", keystore, password, outputApk, apk, alias);

                                    RunJarSigner(jarSignerPath, args, (cSuccess, cTip) =>
                                    {
                                        if (File.Exists(outputApk) && cSuccess)
                                        {
                                            callback?.Invoke(true, outputApk);
                                        }
                                        else
                                        {
                                            callback?.Invoke(false, string.Empty);
                                        }
                                    });
                                }
                                else
                                {
                                    callback?.Invoke(bSuccess, string.Empty);
                                }

                            });
                        }
                        else
                        {
                            callback?.Invoke(aSuccess, string.Empty);
                        }
                    });
                }
            }
        }

        private string GenerateSignedApkPath(string apk)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(apk))
            {
                string dir = Path.GetDirectoryName(apk);

                string fileName = Path.GetFileNameWithoutExtension(apk);

                string signedFileName = string.Format("{0}_signed.apk", fileName);

                result = string.Format("{0}/{1}", dir, signedFileName);
            }

            return result;
        }

        private void RunJarSigner(string signerPath, string args, Action<bool, string> callback = null)
        {
            if (!string.IsNullOrEmpty(signerPath) && File.Exists(signerPath))
            {
                using (Process process = new Process())
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    startInfo.FileName = signerPath;

                    startInfo.Arguments = args;

                    startInfo.WorkingDirectory = Path.GetDirectoryName(signerPath);

                    process.StartInfo = startInfo;

                    process.Start();

                    process.WaitForExit();

                    process.Close();

                    callback?.Invoke(true, string.Empty);
                }
            }
            else
            {
                callback?.Invoke(false, "JarSigner not exists");
            }
        }
    }
}
