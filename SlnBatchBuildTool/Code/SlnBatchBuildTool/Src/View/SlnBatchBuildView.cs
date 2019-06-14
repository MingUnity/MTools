using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SlnBatchBuildTool
{
    public partial class SlnBatchBuildView : Form, ISlnBatchBuildView
    {
        private ISlnBatchBuildController _controller;

        public ISlnBatchBuildController Controller
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

        public int[] SelectedIndex
        {
            get
            {
                int[] res = null;

                ListBox.SelectedIndexCollection collection = listSlns.SelectedIndices;

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
                        listSlns.SetSelected(value[i], true);
                    }
                }
            }
        }

        public string[] ListItems
        {
            set
            {
                if (value != null)
                {
                    listSlns.Items.Clear();

                    listSlns.Items.AddRange(value);
                }
            }
        }

        public bool BuildEnabled
        {
            get
            {
                return btnBuild.Enabled;
            }
            set
            {
                btnBuild.Enabled = value;
            }
        }

        public string BuildContent
        {
            get
            {
                return btnBuild.Text;
            }
            set
            {
                btnBuild.Text = value;
            }
        }

        public int MaxConcurrentCount
        {
            get
            {
                int res = 0;

                int.TryParse(txtConcurrentCount.Text, out res);

                return res;
            }
            set
            {
                txtConcurrentCount.Text = value.ToString();
            }
        }

        public SlnBatchBuildView()
        {
            InitializeComponent();
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            _controller?.Build();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _controller?.AddSln();
        }

        private void btnReduce_Click(object sender, EventArgs e)
        {
            _controller?.RemoveSln();
        }

        private void listSlns_DragEnter(object sender, DragEventArgs e)
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

        private void listSlns_DragDrop(object sender, DragEventArgs e)
        {
            Array arr = (Array)e.Data.GetData(DataFormats.FileDrop);

            if (arr != null)
            {
                string[] files = new string[arr.Length];

                for (int i = 0; i < arr.Length; i++)
                {
                    files[i] = arr.GetValue(i).ToString();
                }

                _controller?.HandleDragFile(files);
            }
        }

        private void listSlns_DoubleClick(object sender, EventArgs e)
        {
            _controller?.FocusSln();
        }

        private void txtConcurrentCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\b')//这是允许输入退格键  
            {
                if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字  
                {
                    e.Handled = true;
                }
            }
        }

        private void txtConcurrentCount_Leave(object sender, EventArgs e)
        {
            _controller?.SaveConcurrentCount();
        }

        private void listSlns_KeyDown(object sender, KeyEventArgs e)
        {
            if (e != null && e.KeyCode == Keys.Delete)
            {
                _controller?.RemoveSln();
            }
        }
    }
}
