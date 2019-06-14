using System;
using System.Windows.Forms;

namespace ReferenceReplaceTool
{
    public partial class ReferenceReplacerView : Form, IRefReplacerView
    {
        private IRefReplacerController _controller;

        public IRefReplacerController Controller
        {
            get
            {
                return _controller;
            }
            set
            {
                _controller = value;
            }
        }

        public int[] CsprojSelectedIndex
        {
            get
            {
                int[] res = null;

                ListBox.SelectedIndexCollection collection = listCsproj.SelectedIndices;

                if (collection != null)
                {
                    res = new int[collection.Count];

                    collection.CopyTo(res, 0);
                }

                return res;
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        listCsproj.SetSelected(value[i], true);
                    }
                }
            }
        }

        public string[] CsprojListItems
        {
            set
            {
                if (value != null)
                {
                    listCsproj.Items.Clear();

                    listCsproj.Items.AddRange(value);
                }
            }
        }

        public int[] DllSelectedIndex
        {
            get
            {
                int[] res = null;

                ListBox.SelectedIndexCollection collection = listDll.SelectedIndices;

                if (collection != null)
                {
                    res = new int[collection.Count];

                    collection.CopyTo(res, 0);
                }

                return res;
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        listDll.SetSelected(value[i], true);
                    }
                }
            }
        }

        public string[] DllListItems
        {
            set
            {
                if (value != null)
                {
                    listDll.Items.Clear();

                    listDll.Items.AddRange(value);
                }
            }
        }

        public bool ReplaceEnabled
        {
            get
            {
                return btnReplace.Enabled;
            }
            set
            {
                btnReplace.Enabled = value;
            }
        }

        public string ReplaceContent
        {
            get
            {
                return btnReplace.Text;
            }
            set
            {
                btnReplace.Text = value;
            }
        }

        public ReferenceReplacerView()
        {
            InitializeComponent();
        }

        private void btnReplace_Click(object sender, System.EventArgs e)
        {
            _controller?.ReplaceRef();
        }

        private void btnAddCsproj_Click(object sender, EventArgs e)
        {
            _controller?.AddCsproj();
        }

        private void btnReduceCsproj_Click(object sender, EventArgs e)
        {
            _controller?.ReduceCsproj();
        }

        private void btnAddDll_Click(object sender, EventArgs e)
        {
            _controller?.AddDll();
        }

        private void btnReduceDll_Click(object sender, EventArgs e)
        {
            _controller?.ReduceDll();
        }

        private void listCsproj_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragEnter(e);
        }

        private void listCsproj_DragDrop(object sender, DragEventArgs e)
        {
            _controller?.HandleCsprojDrag(GetDragDropFiles(e));
        }

        private void listDll_DragEnter(object sender, DragEventArgs e)
        {
            HandleDragEnter(e);
        }

        private void listDll_DragDrop(object sender, DragEventArgs e)
        {
            _controller?.HandleDllDrag(GetDragDropFiles(e));
        }

        private void HandleDragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private string[] GetDragDropFiles(DragEventArgs e)
        {
            string[] files = null;

            Array arr = (Array)e.Data.GetData(DataFormats.FileDrop);

            if (arr != null)
            {
                files = new string[arr.Length];

                for (int i = 0; i < arr.Length; i++)
                {
                    files[i] = arr.GetValue(i).ToString();
                }
            }

            return files;
        }

        private void listCsproj_DoubleClick(object sender, EventArgs e)
        {
            _controller?.FocusCsproj();
        }

        private void listDll_DoubleClick(object sender, EventArgs e)
        {
            _controller?.FocusDll();
        }

        private void listCsproj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e != null && e.KeyCode == Keys.Delete)
            {
                _controller?.ReduceCsproj();
            }
        }

        private void listDll_KeyDown(object sender, KeyEventArgs e)
        {
            if (e != null && e.KeyCode == Keys.Delete)
            {
                _controller?.ReduceDll();
            }
        }
    }
}
