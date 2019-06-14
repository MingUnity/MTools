using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;

namespace ReferenceReplaceTool
{
    public class RefReplacerController : IRefReplacerController
    {
        private IRefReplacerView _view;

        private ToolData _data;

        private string _dataPath = string.Empty;

        public RefReplacerController(IRefReplacerView view)
        {
            this._view = view;

            if (_view != null)
            {
                _view.Controller = this;
            }

            _data = new ToolData();

            _dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "RefReplacer/Config/Ref.mdat");

            ReadData();

            RefreshCsprojView();

            RefreshDllView();
        }

        public void ReplaceRef()
        {
            if (_view != null)
            {
                _view.ReplaceContent = "Replacing...";

                _view.ReplaceEnabled = false;
            }

            if (_data != null)
            {
                for (int i = 0; i < _data.csprojList.Count; i++)
                {
                    string csprojFile = _data.csprojList[i];

                    HintReplacer replacer = new HintReplacer(csprojFile);

                    Dictionary<string, string> refDic = new Dictionary<string, string>();

                    for (int j = 0; j < _data.dllList.Count; j++)
                    {
                        string dll = _data.dllList[j];

                        refDic[Path.GetFileNameWithoutExtension(dll)] = dll;
                    }

                    replacer.ReplaceHintPath(refDic);
                }
            }

            ResetReplace();

            MessageBox.Show("Replace done", "Tip");
        }

        public void AddCsproj()
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = true,

                Title = "Please choose csproj",

                Filter = "Csproj file(*.csproj)|*.csproj"
            };

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string[] files = dialog.FileNames;

                HandleCsprojFile(files);
            }
        }

        public void ReduceCsproj()
        {
            if (_data == null || _data.csprojList == null)
            {
                return;
            }

            int[] seletectIndexArr = _view?.CsprojSelectedIndex;

            if (seletectIndexArr != null && seletectIndexArr.Length > 0)
            {
                int count = _data.csprojList.Count;

                for (int i = count - 1; i >= 0; i--)
                {
                    if (seletectIndexArr.Contains(i))
                    {
                        _data.csprojList.RemoveAt(i);
                    }
                }

                SaveData();

                RefreshCsprojView();
            }
        }

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
                string[] files = dialog.FileNames;

                HandleDllFile(files);
            }
        }

        public void ReduceDll()
        {
            if (_data == null || _data.dllList == null)
            {
                return;
            }

            int[] seletectIndexArr = _view?.DllSelectedIndex;

            if (seletectIndexArr != null && seletectIndexArr.Length > 0)
            {
                int count = _data.dllList.Count;

                for (int i = count - 1; i >= 0; i--)
                {
                    if (seletectIndexArr.Contains(i))
                    {
                        _data.dllList.RemoveAt(i);
                    }
                }

                SaveData();

                RefreshDllView();
            }
        }

        public void HandleCsprojDrag(string[] files)
        {
            HandleCsprojFile(files);
        }

        public void HandleDllDrag(string[] files)
        {
            HandleDllFile(files);
        }

        public void FocusCsproj()
        {
            if (_view != null)
            {
                int[] seletedArr = _view.CsprojSelectedIndex;

                if (seletedArr != null && seletedArr.Length > 0)
                {
                    int index = seletedArr[0];

                    string path = null;

                    if (TryGetPath(_data?.csprojList, index, out path))
                    {
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            Process.Start("explorer.exe", string.Format("/select,{0}", path.Replace("/", "\\")));
                        }
                    }
                }
            }
        }

        public void FocusDll()
        {
            if (_view != null)
            {
                int[] seletedArr = _view.DllSelectedIndex;

                if (seletedArr != null && seletedArr.Length > 0)
                {
                    int index = seletedArr[0];

                    string path = null;

                    if (TryGetPath(_data?.dllList, index, out path))
                    {
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            Process.Start("explorer.exe", string.Format("/select,{0}", path.Replace("/", "\\")));
                        }
                    }
                }
            }
        }

        private void ReadData()
        {
            if (File.Exists(_dataPath))
            {
                using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(_dataPath)))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    _data = formatter.Deserialize(stream) as ToolData;
                }
            }
        }

        private void SaveData()
        {
            if (_data != null)
            {
                string dir = Path.GetDirectoryName(_dataPath);

                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                using (FileStream stream = new FileStream(_dataPath, FileMode.Create, FileAccess.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();

                    formatter.Serialize(stream, _data);
                }
            }
        }

        private bool TryGetPath(List<string> list, int index, out string path)
        {
            bool res = false;

            path = null;

            if (index >= 0 && list != null && list.Count > index)
            {
                path = list[index];

                res = true;
            }

            return res;
        }

        private void RefreshCsprojView()
        {
            if (_view != null && _data != null)
            {
                if (_data.csprojList != null)
                {
                    string[] arr = new string[_data.csprojList.Count];

                    for (int i = 0; i < _data.csprojList.Count; i++)
                    {
                        string csproj = _data.csprojList[i];

                        if (!string.IsNullOrEmpty(csproj))
                        {
                            string fileName = Path.GetFileName(csproj);

                            arr[i] = fileName;
                        }
                    }

                    _view.CsprojListItems = arr;
                }
            }
        }

        private void RefreshDllView()
        {
            if (_view != null && _data != null)
            {
                if (_data.dllList != null)
                {
                    string[] arr = new string[_data.dllList.Count];

                    for (int i = 0; i < _data.dllList.Count; i++)
                    {
                        string dll = _data.dllList[i];

                        if (!string.IsNullOrEmpty(dll))
                        {
                            string fileName = Path.GetFileName(dll);

                            arr[i] = fileName;
                        }
                    }

                    _view.DllListItems = arr;
                }
            }
        }

        private void HandleDllFile(string[] files)
        {
            if (files != null && _data != null && _data.dllList != null)
            {
                if (_view != null)
                {
                    _view.ReplaceContent = "Importing...";

                    _view.ReplaceEnabled = false;
                }

                List<int> existedList = new List<int>();

                List<string> invalidDirs = new List<string>();

                List<string> searchedFileList = new List<string>();

                Action<string> AddAct = new Action<string>((path) =>
                {
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        string ext = Path.GetExtension(path);

                        if (".dll".Equals(ext, StringComparison.OrdinalIgnoreCase))
                        {
                            List<string> filenames = GetFileNameList(_data.dllList);

                            int index = filenames.IndexOf(Path.GetFileName(path));

                            if (index >= 0)
                            {
                                existedList.Add(index);
                            }
                            else
                            {
                                _data.dllList.Add(path);
                            }
                        }
                    }
                });

                for (int i = 0; i < files.Length; i++)
                {
                    string path = files[i];

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (Directory.Exists(path))
                        {
                            string[] searchedFiles = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);

                            if (searchedFiles != null && searchedFiles.Length > 0)
                            {
                                searchedFileList.AddRange(searchedFiles);
                            }
                            else
                            {
                                invalidDirs.Add(path);
                            }
                        }
                        else
                        {
                            AddAct.Invoke(path);
                        }
                    }
                }

                if (searchedFileList.Count > 0)
                {
                    for (int i = 0; i < searchedFileList.Count; i++)
                    {
                        AddAct.Invoke(searchedFileList[i]);
                    }
                }

                SaveData();

                RefreshDllView();

                ResetReplace();

                if (invalidDirs.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder("Following directory dones't contains any dll files : ");

                    for (int i = 0; i < invalidDirs.Count; i++)
                    {
                        stringBuilder.Append("\r\n");

                        stringBuilder.Append(invalidDirs[i]);
                    }

                    MessageBox.Show(stringBuilder.ToString());
                }

                if (existedList.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder("Following dll existed : ");

                    for (int i = 0; i < existedList.Count; i++)
                    {
                        stringBuilder.Append("\r\n");

                        stringBuilder.Append(Path.GetFileName(_data.dllList[existedList[i]]));
                    }

                    MessageBox.Show(stringBuilder.ToString(), "Warning");

                    _view.DllSelectedIndex = existedList.ToArray();
                }
            }
        }

        private void HandleCsprojFile(string[] files)
        {
            if (files != null && _data != null && _data.csprojList != null)
            {
                if (_view != null)
                {
                    _view.ReplaceContent = "Importing...";

                    _view.ReplaceEnabled = false;
                }

                List<int> existedList = new List<int>();

                List<string> invalidDirs = new List<string>();

                List<string> searchedFileList = new List<string>();

                Action<string> AddAct = new Action<string>((path) =>
                {
                    if (!string.IsNullOrEmpty(path) && File.Exists(path))
                    {
                        string ext = Path.GetExtension(path);

                        if (".csproj".Equals(ext, StringComparison.OrdinalIgnoreCase))
                        {
                            int index = _data.csprojList.IndexOf(path);

                            if (index >= 0)
                            {
                                existedList.Add(index);
                            }
                            else
                            {
                                _data.csprojList.Add(path);
                            }
                        }
                    }
                });

                for (int i = 0; i < files.Length; i++)
                {
                    string path = files[i];

                    if (!string.IsNullOrEmpty(path))
                    {
                        if (Directory.Exists(path))
                        {
                            string[] searchedFiles = Directory.GetFiles(path, "*.csproj", SearchOption.AllDirectories);

                            if (searchedFiles != null && searchedFiles.Length > 0)
                            {
                                searchedFileList.AddRange(searchedFiles);
                            }
                            else
                            {
                                invalidDirs.Add(path);
                            }
                        }
                        else
                        {
                            AddAct.Invoke(path);
                        }
                    }
                }

                if (searchedFileList.Count > 0)
                {
                    for (int i = 0; i < searchedFileList.Count; i++)
                    {
                        AddAct.Invoke(searchedFileList[i]);
                    }
                }

                SaveData();

                RefreshCsprojView();

                ResetReplace();

                if (invalidDirs.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder("Following directory dones't contains any csproj files : ");

                    for (int i = 0; i < invalidDirs.Count; i++)
                    {
                        stringBuilder.Append("\r\n");

                        stringBuilder.Append(invalidDirs[i]);
                    }

                    MessageBox.Show(stringBuilder.ToString());
                }

                if (existedList.Count > 0)
                {
                    StringBuilder stringBuilder = new StringBuilder("Following csproj existed : ");

                    for (int i = 0; i < existedList.Count; i++)
                    {
                        stringBuilder.Append("\r\n");

                        stringBuilder.Append(_data.csprojList[existedList[i]]);
                    }

                    MessageBox.Show(stringBuilder.ToString(), "Warning");

                    _view.CsprojSelectedIndex = existedList.ToArray();
                }
            }
        }

        private List<string> GetFileNameList(List<string> src)
        {
            List<string> res = null;

            if (src != null)
            {
                res = new List<string>();

                for (int i = 0; i < src.Count; i++)
                {
                    res.Add(Path.GetFileName(src[i]));
                }
            }

            return res;
        }

        private void ResetReplace()
        {
            if (_view != null)
            {
                _view.ReplaceEnabled = true;

                _view.ReplaceContent = "Replace";
            }
        }
    }
}
