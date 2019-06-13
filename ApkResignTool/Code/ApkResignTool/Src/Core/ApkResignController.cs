using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace ApkResignTool
{
    public class ApkResignController : IApkResignController
    {
        #region Variable

        private IApkResignView _view;

        private ICompressTool _compressTool;

        private ApkResignModel _model;

        private string _modelPath;

        private string _emptyMetaInfPath;

        #endregion

        #region Public Func

        public IApkResignView View
        {
            get
            {
                return _view;
            }
            set
            {
                _view = value;

                if (_view != null)
                {
                    _view.Controller = this;
                }

                ReadModel();

                Setup();
            }
        }

        public ApkResignController(ICompressTool compressTool)
        {
            this._compressTool = compressTool;

            _modelPath = string.Format("{0}/{1}/Config/ApkResignModel.bytes", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);

            string dir = Path.GetDirectoryName(_modelPath);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            _emptyMetaInfPath = string.Format("{0}/{1}/Config/META-INF", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);

            if (Directory.Exists(_emptyMetaInfPath))
            {
                Directory.Delete(_emptyMetaInfPath, true);
            }

            Directory.CreateDirectory(_emptyMetaInfPath);
        }

        /// <summary>
        /// 选择keystore
        /// </summary>
        public void PickKeystore()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false,

                Title = "Please choose keystore",

                Filter = "Keystore file(*.keystore)|*.keystore"
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string keystore = dialog.FileName;

                _model.keystore = keystore;

                _model.password = string.Empty;

                _model.alias = string.Empty;

                Setup();

                SaveModel();
            }
        }

        /// <summary>
        /// 保存密码
        /// </summary>
        public void SavePassword()
        {
            _model.password = _view.Password;

            SaveModel();
        }

        /// <summary>
        /// 保存别名
        /// </summary>
        public void SaveAlias()
        {
            _model.alias = _view.Alias;

            SaveModel();
        }

        /// <summary>
        /// 选择JDK目录
        /// </summary>
        public void PickJdkDir()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog()
            {
                Description = "Please choose JDK directory",

                ShowNewFolderButton = false
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string jdkDir = dialog.SelectedPath;

                string signer = GetSigner(jdkDir);

                if (File.Exists(signer))
                {
                    _view.JdkDir = jdkDir;

                    _model.jdkDir = jdkDir;

                    SaveModel();
                }
                else
                {
                    MessageBox.Show("JDK directory is wrong!", "Warning");
                }
            }
        }

        /// <summary>
        /// 选择Apk文件
        /// </summary>
        public void PickApk()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false,

                Title = "Please choose apk",

                Filter = "Apk file(*.apk)|*.apk"
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string apk = dialog.FileName;

                _view.Apk = apk;

                _model.apk = apk;

                SaveModel();
            }
        }

        /// <summary>
        /// 处理拖拽文件
        /// </summary>
        public void HandleDragFile(string[] filePaths)
        {
            if (filePaths != null)
            {
                List<string> dlls = new List<string>();

                for (int i = 0; i < filePaths.Length; i++)
                {
                    string filePath = filePaths[i];

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        if (File.Exists(filePath))
                        {
                            string ext = Path.GetExtension(filePath);

                            if (".apk".Equals(ext, StringComparison.OrdinalIgnoreCase))
                            {
                                _view.Apk = filePath;

                                _model.apk = filePath;
                            }
                            else if (".dll".Equals(ext, StringComparison.OrdinalIgnoreCase))
                            {
                                dlls.Add(filePath);
                            }
                            else if (".keystore".Equals(ext, StringComparison.OrdinalIgnoreCase))
                            {
                                _view.Keystore = filePath;

                                _model.keystore = filePath;
                            }
                        }
                        else if (Directory.Exists(filePath))
                        {
                            string signer = GetSigner(filePath);

                            if (File.Exists(signer))
                            {
                                _view.JdkDir = filePath;

                                _model.jdkDir = filePath;
                            }
                        }
                    }
                }

                SaveModel();

                if (dlls != null && dlls.Count > 0)
                {
                    ReplaceDll(dlls.ToArray());
                }
            }
        }

        /// <summary>
        /// 签名
        /// </summary>
        public void Sign()
        {
            InvalidEnum valid = CheckValid();

            if (valid == InvalidEnum.Valid)
            {
                _view.Tip = "Signing...";

                _view.SignEnabled = false;

                ApkSigner signer = new ApkSigner()
                {
                    emptyMetaInfPath = _emptyMetaInfPath,

                    alias = _model.alias,

                    apk = _model.apk,

                    jarSignerPath = GetSigner(_model.jdkDir),

                    keystore = _model.keystore,

                    password = _model.password,

                    _compressTool = new WinRar()
                };

                signer.Sign((success, path) =>
                {
                    ResetViewTip();

                    if (success)
                    {
                        OpenDestDir(path);

                        MessageBox.Show("Sign done", "Tip");
                    }
                    else
                    {
                        MessageBox.Show("Sign failed", "Warning");
                    }
                });
            }
            else
            {
                StringBuilder wrongtip = new StringBuilder("Wrong :");

                if (valid.HasFlag(InvalidEnum.Keystore))
                {
                    wrongtip.Append(" [keystore not exists]");
                }

                if (valid.HasFlag(InvalidEnum.Password))
                {
                    wrongtip.Append(" [password is empty]");
                }

                if (valid.HasFlag(InvalidEnum.Alias))
                {
                    wrongtip.Append(" [alias is empty]");
                }

                if (valid.HasFlag(InvalidEnum.JdkDir))
                {
                    wrongtip.Append(" [jdkdir wrong]");
                }

                if (valid.HasFlag(InvalidEnum.Apk))
                {
                    wrongtip.Append(" [apk not exists]");
                }

                MessageBox.Show(wrongtip.ToString(), "Warning");
            }
        }

        /// <summary>
        /// 添加dll
        /// </summary>
        public void AddDll()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,

                Title = "Please choose dll",

                Filter = "Dll file(*.dll)|*.dll"
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] fileNames = dialog.FileNames;

                ReplaceDll(fileNames);
            }
        }

        /// <summary>
        /// 创建keystore
        /// </summary>
        public void CreateKeystore()
        {
            KeyTool keyTool = new KeyTool()
            {
                keyToolPath = GetKeyTool(_model.jdkDir),

                alias = _model.alias,

                password = _model.password
            };

            keyTool.Create(CreateKeystoreResult);
        }

        #endregion

        #region Private Func

        /// <summary>
        /// 装载数据
        /// </summary>
        private void Setup()
        {
            if (_view != null)
            {
                _view.Keystore = _model.keystore;

                _view.Password = _model.password;

                _view.Alias = _model.alias;

                _view.JdkDir = _model.jdkDir;

                _view.Apk = _model.apk;
            }
        }

        /// <summary>
        /// 读取数据
        /// </summary>
        private void ReadModel()
        {
            if (!string.IsNullOrEmpty(_modelPath) && File.Exists(_modelPath))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read))
                {
                    _model = (ApkResignModel)formatter.Deserialize(stream);
                }
            }
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveModel()
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream stream = new FileStream(_modelPath, FileMode.Create))
            {
                formatter.Serialize(stream, _model);
            }
        }

        /// <summary>
        /// 获取签名器
        /// </summary>
        private string GetSigner(string jdkDir)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(jdkDir) && Directory.Exists(jdkDir))
            {
                result = string.Format("{0}/bin/jarsigner.exe", jdkDir);
            }

            return result;
        }

        /// <summary>
        /// 获取秘钥工具
        /// </summary>
        private string GetKeyTool(string jdkDir)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(jdkDir) && Directory.Exists(jdkDir))
            {
                result = string.Format("{0}/bin/keytool.exe", jdkDir);
            }

            return result;
        }

        /// <summary>
        /// 检查有效性
        /// </summary>
        private InvalidEnum CheckValid()
        {
            InvalidEnum result = InvalidEnum.Valid;

            bool existKeystore = !string.IsNullOrEmpty(_model.keystore) && File.Exists(_model.keystore);

            bool existPwd = !string.IsNullOrEmpty(_model.password);

            bool existAlias = !string.IsNullOrEmpty(_model.alias);

            bool existJdkDir = !string.IsNullOrEmpty(_model.jdkDir) && File.Exists(GetSigner(_model.jdkDir));

            bool existApk = !string.IsNullOrEmpty(_model.apk) && File.Exists(_model.apk);

            if (!existKeystore)
            {
                result |= InvalidEnum.Keystore;
            }

            if (!existPwd)
            {
                result |= InvalidEnum.Password;
            }

            if (!existAlias)
            {
                result |= InvalidEnum.Alias;
            }

            if (!existJdkDir)
            {
                result |= InvalidEnum.JdkDir;
            }

            if (!existApk)
            {
                result |= InvalidEnum.Apk;
            }

            return result;
        }

        /// <summary>
        /// 替换dll
        /// </summary>
        private void ReplaceDll(string[] dlls)
        {
            if (!string.IsNullOrEmpty(_model.apk) && File.Exists(_model.apk))
            {
                if (dlls != null && dlls.Length > 0)
                {
                    _view.Tip = "Replacing dll...";

                    _view.SignEnabled = false;

                    string replaceDir = string.Format("{0}/{1}/assets", Path.GetTempPath(), Application.ProductName);

                    if (Directory.Exists(replaceDir))
                    {
                        Directory.Delete(replaceDir, true);
                    }

                    string tempDllDir = string.Format("{0}/bin/Data/Managed", replaceDir);

                    if (!Directory.Exists(tempDllDir))
                    {
                        Directory.CreateDirectory(tempDllDir);
                    }

                    for (int i = 0; i < dlls.Length; i++)
                    {
                        string dll = dlls[i];

                        if (!string.IsNullOrEmpty(dll) && File.Exists(dll))
                        {
                            string dllName = Path.GetFileName(dll);

                            string copyDestPath = string.Format("{0}/{1}", tempDllDir, dllName);

                            File.Copy(dll, copyDestPath, true);
                        }
                    }

                    _compressTool?.Compress(new string[] { replaceDir }, _model.apk, (success, tip) =>
                    {
                        ResetViewTip();

                        if (success)
                        {
                            MessageBox.Show("Replace dll done", "Tip");
                        }
                        else
                        {
                            MessageBox.Show(tip, "Warning");
                        }
                    });
                }
            }
            else
            {
                MessageBox.Show("Apk not exists", "Warning");
            }
        }

        /// <summary>
        /// 重置提示
        /// </summary>
        private void ResetViewTip()
        {
            _view.Tip = "Sign";

            _view.SignEnabled = true;
        }

        /// <summary>
        /// 打开目标文件目录
        /// </summary>
        private void OpenDestDir(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                Process.Start("explorer.exe", string.Format("/select,{0}", path.Replace("/", "\\")));
            }
        }

        /// <summary>
        /// 创建秘钥结果
        /// </summary>
        private void CreateKeystoreResult(KeyTool.Result result, string path)
        {
            if (result == KeyTool.Result.Success)
            {
                OpenDestDir(path);

                _view.Keystore = path;

                _model.keystore = path;

                SaveModel();

                MessageBox.Show("Create keystore done", "Tip");
            }
            else if (result == KeyTool.Result.KeystoreExisted)
            {
                DialogResult dialogResult = MessageBox.Show("Keystore existed, Override ?", "Tip", MessageBoxButtons.OKCancel);

                if (dialogResult == DialogResult.OK)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    CreateKeystore();
                }
            }
            else
            {
                StringBuilder failTip = new StringBuilder("Wrong :");

                if (result.HasFlag(KeyTool.Result.NotExistKeyTool))
                {
                    failTip.Append(" [jdkDir not exist]");
                }

                if (result.HasFlag(KeyTool.Result.EmptyAlias))
                {
                    failTip.Append(" [alias is empty]");
                }

                if (result.HasFlag(KeyTool.Result.EmptyPassword))
                {
                    failTip.Append(" [password is empty]");
                }

                if (result.HasFlag(KeyTool.Result.CreateFailed))
                {
                    failTip.Append(" [create failed]");
                }

                if (result.HasFlag(KeyTool.Result.LittlePassword))
                {
                    failTip.Append(" [password at least 6]");
                }

                MessageBox.Show(failTip.ToString(), "Warning");
            }
        }

        #endregion

        #region Enum

        [Flags]
        private enum InvalidEnum
        {
            /// <summary>
            /// 有效
            /// </summary>
            Valid = 0,

            Keystore = 1,

            Password = 2,

            Alias = 4,

            JdkDir = 8,

            Apk = 16
        }

        #endregion
    }
}
