using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace SlnBatchBuildTool
{
    public class SlnBatchBuildController : ISlnBatchBuildController
    {
        private ISlnBatchBuildView _view;

        private SlnContainer _slns;

        private string _dataPath;

        private IBuilder _builder;

        public SlnBatchBuildController(ISlnBatchBuildView view)
        {
            this._view = view;

            if (_view != null)
            {
                _view.Controller = this;
            }

            _slns = new SlnContainer();

            _dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SlnBatchBuildTool", "Config", "Slns.mdat");

            ReadData();

            RefreshViewList();

            _builder = new Devenv();
        }

        public void AddSln()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,

                Title = "Please choose sln",

                Filter = "Sln file(*.sln)|*.sln"
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] files = dialog.FileNames;

                HandleSlnFile(files);
            }
        }

        public void RemoveSln()
        {
            if (_slns == null || _slns.slns == null)
            {
                return;
            }

            int[] seletectIndexArr = _view?.SelectedIndex;

            if (seletectIndexArr != null && seletectIndexArr.Length > 0)
            {
                int count = _slns.slns.Count;

                for (int i = count - 1; i >= 0; i--)
                {
                    if (seletectIndexArr.Contains(i))
                    {
                        _slns.slns.RemoveAt(i);
                    }
                }

                SaveData();

                RefreshViewList();
            }
        }

        public void Build()
        {
            if (_view != null)
            {
                _view.BuildContent = "Building...";

                _view.BuildEnabled = false;
            }

            if (_builder != null)
            {
                if (_builder.IsValid)
                {
                    if (_slns != null && _slns.slns != null)
                    {
                        List<int> indexList = new List<int>();

                        for (int i = 0; i < _slns.slns.Count; i++)
                        {
                            string sln = _slns.slns[i];

                            if (!string.IsNullOrEmpty(sln) && File.Exists(sln))
                            {
                                _builder.Build(sln);
                            }
                            else
                            {
                                indexList.Add(i);
                            }
                        }

                        if (indexList.Count > 0)
                        {
                            StringBuilder stringBuilder = new StringBuilder("Build done , following sln not existed : ");

                            for (int i = 0; i < indexList.Count; i++)
                            {
                                stringBuilder.Append("\r\n");

                                stringBuilder.Append(_slns.slns[i]);
                            }

                            MessageBox.Show(stringBuilder.ToString(), "Warning");
                        }
                        else
                        {
                            MessageBox.Show("Build done", "Tip");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Build tool is invalid");
                }
            }

            ResetBuild();
        }

        public void FocusSln()
        {
            if (_view != null)
            {
                int[] seletedArr = _view.SelectedIndex;

                if (seletedArr != null && seletedArr.Length > 0)
                {
                    int index = seletedArr[0];

                    string path = null;

                    if (TryGetPath(index, out path))
                    {
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            Process.Start("explorer.exe", string.Format("/select,{0}", path.Replace("/", "\\")));
                        }
                    }
                }
            }
        }

        public void HandleDragFile(string[] files)
        {
            HandleSlnFile(files);
        }

        private void HandleSlnFile(string[] files)
        {
            if (files != null && _slns != null && _slns.slns != null)
            {
                if (_view != null)
                {
                    _view.BuildContent = "Importing...";

                    _view.BuildEnabled = false;
                }

                string slnExt = ".sln";

                List<int> existedList = new List<int>();

                List<string> invalidDirs = new List<string>();

                for (int i = 0; i < files.Length; i++)
                {
                    string path = files[i];

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (Directory.Exists(path))
                        {
                            string dir = path;

                            //找到文件夹中第一个sln并输出
                            if (!FindSln(dir, out path))
                            {
                                invalidDirs.Add(dir);
                            }
                        }

                        if (File.Exists(path))
                        {
                            string ext = Path.GetExtension(path);

                            if (slnExt.Equals(ext, StringComparison.OrdinalIgnoreCase))
                            {
                                int index = _slns.slns.IndexOf(path);

                                if (index >= 0)
                                {
                                    existedList.Add(index);
                                }
                                else
                                {
                                    _slns.slns.Add(path);
                                }
                            }
                        }
                    }
                }

                SaveData();

                RefreshViewList();

                if (invalidDirs.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder("Following directory don't contains any sln files : ");

                    for (int i = 0; i < invalidDirs.Count; i++)
                    {
                        stringBuilder.Append("\r\n");

                        stringBuilder.Append(invalidDirs[i]);
                    }

                    MessageBox.Show(stringBuilder.ToString());
                }

                if (existedList.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder("Following sln existed : ");

                    for (int i = 0; i < existedList.Count; i++)
                    {
                        stringBuilder.Append("\r\n");

                        stringBuilder.Append(_slns.slns[existedList[i]]);
                    }

                    MessageBox.Show(stringBuilder.ToString(), "Warning");

                    _view.SelectedIndex = existedList.ToArray();
                }

                ResetBuild();
            }
        }

        private void RefreshViewList()
        {
            if (_view != null && _slns != null && _slns.slns != null)
            {
                string[] arr = new string[_slns.slns.Count];

                for (int i = 0; i < _slns.slns.Count; i++)
                {
                    string sln = _slns.slns[i];

                    if (!string.IsNullOrEmpty(sln))
                    {
                        string fileName = Path.GetFileName(sln);

                        arr[i] = fileName;
                    }
                }

                _view.ListItems = arr;
            }
        }

        private bool TryGetPath(int index, out string path)
        {
            bool res = false;

            path = null;

            if (index >= 0 && _slns != null && _slns.slns != null && _slns.slns.Count > index)
            {
                path = _slns.slns[index];

                res = true;
            }

            return res;
        }

        private void SaveData()
        {
            if (_slns != null)
            {
                if (!string.IsNullOrEmpty(_dataPath))
                {
                    string dir = Path.GetDirectoryName(_dataPath);

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    using (FileStream fileStream = new FileStream(_dataPath, FileMode.Create, FileAccess.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        formatter.Serialize(fileStream, _slns);
                    }
                }
            }
        }

        private void ReadData()
        {
            if (!string.IsNullOrEmpty(_dataPath) && File.Exists(_dataPath))
            {
                using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(_dataPath)))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    _slns = formatter.Deserialize(stream) as SlnContainer;
                }
            }
        }

        private bool FindSln(string dir, out string path)
        {
            bool res = false;

            path = null;

            string ext = "*.sln";

            if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
            {
                string[] files = Directory.GetFiles(dir, ext);

                if (files != null && files.Length > 0)
                {
                    path = files[0];

                    res = true;
                }
                else
                {
                    string[] dirs = Directory.GetDirectories(dir);

                    if (dirs != null && dirs.Length > 0)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            res = FindSln(dirs[i], out path);

                            if (res)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return res;
        }

        private void ResetBuild()
        {
            if (_view != null)
            {
                _view.BuildEnabled = true;

                _view.BuildContent = "Build";
            }
        }
    }
}
