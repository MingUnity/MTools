using System;
using System.Windows.Forms;

namespace ApkResignTool
{
    public partial class ApkResignView : Form, IApkResignView
    {
        private IApkResignController _controller;

        public IApkResignController Controller
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

        public ApkResignView()
        {
            InitializeComponent();
        }

        public string Keystore
        {
            get
            {
                return txtKeysotre.Text;
            }
            set
            {
                txtKeysotre.Text = value;
            }
        }

        public string Password
        {
            get
            {
                return txtPassword.Text;
            }
            set
            {
                txtPassword.Text = value;
            }
        }

        public string Alias
        {
            get
            {
                return txtAlias.Text;
            }
            set
            {
                txtAlias.Text = value;
            }
        }

        public string JdkDir
        {
            get
            {
                return txtJdkDir.Text;
            }
            set
            {
                txtJdkDir.Text = value;
            }
        }

        public string Apk
        {
            get
            {
                return txtApk.Text;
            }
            set
            {
                txtApk.Text = value;
            }
        }

        public string Tip
        {
            get
            {
                return btnSign.Text;
            }
            set
            {
                btnSign.Text = value;
            }
        }

        public bool SignEnabled
        {
            get
            {
                return btnSign.Enabled;
            }
            set
            {
                btnSign.Enabled = value;
            }
        }

        public bool ShowPassword
        {
            get
            {
                return txtPassword.PasswordChar == Char.MinValue;
            }
            set
            {
                txtPassword.PasswordChar = value ? Char.MinValue : '*';
            }
        }

        private void btnKeystorePicker_Click(object sender, EventArgs e)
        {
            _controller?.PickKeystore();
        }

        private void txtAlias_Leave(object sender, EventArgs e)
        {
            _controller?.SaveAlias();
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            _controller?.SavePassword();
        }

        private void btnApk_Click(object sender, EventArgs e)
        {
            _controller?.PickApk();
        }

        private void btnSdkDir_Click(object sender, EventArgs e)
        {
            _controller?.PickJdkDir();
        }

        private void ApkResignView_DragEnter(object sender, DragEventArgs e)
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

        private void ApkResignView_DragDrop(object sender, DragEventArgs e)
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

        private void btnSign_Click(object sender, EventArgs e)
        {
            _controller?.Sign();
        }

        private void btnAddDll_Click(object sender, EventArgs e)
        {
            _controller?.AddDll();
        }

        private void btnCreateKeystore_Click(object sender, EventArgs e)
        {
            _controller?.CreateKeystore();
        }

        private void btnShowPwd_Click(object sender, EventArgs e)
        {
            _controller?.ShowPassword();
        }
    }
}
