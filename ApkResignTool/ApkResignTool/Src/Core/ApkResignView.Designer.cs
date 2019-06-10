namespace ApkResignTool
{
    partial class ApkResignView
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtKeysotre = new System.Windows.Forms.TextBox();
            this.btnKeystorePicker = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAlias = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtApk = new System.Windows.Forms.TextBox();
            this.btnApk = new System.Windows.Forms.Button();
            this.btnSign = new System.Windows.Forms.Button();
            this.btnSdkDir = new System.Windows.Forms.Button();
            this.txtJdkDir = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddDll = new System.Windows.Forms.Button();
            this.btnCreateKeystore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Keystore";
            // 
            // txtKeysotre
            // 
            this.txtKeysotre.Location = new System.Drawing.Point(91, 72);
            this.txtKeysotre.Name = "txtKeysotre";
            this.txtKeysotre.ReadOnly = true;
            this.txtKeysotre.Size = new System.Drawing.Size(355, 21);
            this.txtKeysotre.TabIndex = 0;
            this.txtKeysotre.TabStop = false;
            // 
            // btnKeystorePicker
            // 
            this.btnKeystorePicker.Location = new System.Drawing.Point(453, 70);
            this.btnKeystorePicker.Name = "btnKeystorePicker";
            this.btnKeystorePicker.Size = new System.Drawing.Size(32, 23);
            this.btnKeystorePicker.TabIndex = 2;
            this.btnKeystorePicker.TabStop = false;
            this.btnKeystorePicker.Text = "...";
            this.btnKeystorePicker.UseVisualStyleBackColor = true;
            this.btnKeystorePicker.Click += new System.EventHandler(this.btnKeystorePicker_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(91, 121);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(355, 21);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TabStop = false;
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "Alias";
            // 
            // txtAlias
            // 
            this.txtAlias.Location = new System.Drawing.Point(91, 168);
            this.txtAlias.Name = "txtAlias";
            this.txtAlias.Size = new System.Drawing.Size(355, 21);
            this.txtAlias.TabIndex = 2;
            this.txtAlias.TabStop = false;
            this.txtAlias.Leave += new System.EventHandler(this.txtAlias_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "Apk";
            // 
            // txtApk
            // 
            this.txtApk.Location = new System.Drawing.Point(91, 213);
            this.txtApk.Name = "txtApk";
            this.txtApk.ReadOnly = true;
            this.txtApk.Size = new System.Drawing.Size(355, 21);
            this.txtApk.TabIndex = 9;
            this.txtApk.TabStop = false;
            // 
            // btnApk
            // 
            this.btnApk.Location = new System.Drawing.Point(453, 211);
            this.btnApk.Name = "btnApk";
            this.btnApk.Size = new System.Drawing.Size(32, 23);
            this.btnApk.TabIndex = 10;
            this.btnApk.TabStop = false;
            this.btnApk.Text = "...";
            this.btnApk.UseVisualStyleBackColor = true;
            this.btnApk.Click += new System.EventHandler(this.btnApk_Click);
            // 
            // btnSign
            // 
            this.btnSign.Location = new System.Drawing.Point(192, 264);
            this.btnSign.Name = "btnSign";
            this.btnSign.Size = new System.Drawing.Size(156, 23);
            this.btnSign.TabIndex = 11;
            this.btnSign.TabStop = false;
            this.btnSign.Text = "Sign";
            this.btnSign.UseVisualStyleBackColor = true;
            this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
            // 
            // btnSdkDir
            // 
            this.btnSdkDir.Location = new System.Drawing.Point(453, 23);
            this.btnSdkDir.Name = "btnSdkDir";
            this.btnSdkDir.Size = new System.Drawing.Size(32, 23);
            this.btnSdkDir.TabIndex = 14;
            this.btnSdkDir.TabStop = false;
            this.btnSdkDir.Text = "...";
            this.btnSdkDir.UseVisualStyleBackColor = true;
            this.btnSdkDir.Click += new System.EventHandler(this.btnSdkDir_Click);
            // 
            // txtJdkDir
            // 
            this.txtJdkDir.Location = new System.Drawing.Point(91, 25);
            this.txtJdkDir.Name = "txtJdkDir";
            this.txtJdkDir.ReadOnly = true;
            this.txtJdkDir.Size = new System.Drawing.Size(355, 21);
            this.txtJdkDir.TabIndex = 12;
            this.txtJdkDir.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "JdkDir";
            // 
            // btnAddDll
            // 
            this.btnAddDll.Font = new System.Drawing.Font("宋体", 12F);
            this.btnAddDll.Location = new System.Drawing.Point(491, 211);
            this.btnAddDll.Name = "btnAddDll";
            this.btnAddDll.Size = new System.Drawing.Size(27, 23);
            this.btnAddDll.TabIndex = 16;
            this.btnAddDll.TabStop = false;
            this.btnAddDll.Text = "+";
            this.btnAddDll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddDll.UseVisualStyleBackColor = true;
            this.btnAddDll.Click += new System.EventHandler(this.btnAddDll_Click);
            // 
            // btnCreateKeystore
            // 
            this.btnCreateKeystore.Font = new System.Drawing.Font("宋体", 12F);
            this.btnCreateKeystore.Location = new System.Drawing.Point(491, 70);
            this.btnCreateKeystore.Name = "btnCreateKeystore";
            this.btnCreateKeystore.Size = new System.Drawing.Size(27, 23);
            this.btnCreateKeystore.TabIndex = 17;
            this.btnCreateKeystore.TabStop = false;
            this.btnCreateKeystore.Text = "+";
            this.btnCreateKeystore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCreateKeystore.UseVisualStyleBackColor = true;
            this.btnCreateKeystore.Click += new System.EventHandler(this.btnCreateKeystore_Click);
            // 
            // ApkResignView
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 316);
            this.Controls.Add(this.btnCreateKeystore);
            this.Controls.Add(this.btnAddDll);
            this.Controls.Add(this.btnSdkDir);
            this.Controls.Add(this.txtJdkDir);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSign);
            this.Controls.Add(this.btnApk);
            this.Controls.Add(this.txtApk);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAlias);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnKeystorePicker);
            this.Controls.Add(this.txtKeysotre);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ApkResignView";
            this.Text = "ApkResigner";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ApkResignView_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ApkResignView_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeysotre;
        private System.Windows.Forms.Button btnKeystorePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAlias;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtApk;
        private System.Windows.Forms.Button btnApk;
        private System.Windows.Forms.Button btnSign;
        private System.Windows.Forms.Button btnSdkDir;
        private System.Windows.Forms.TextBox txtJdkDir;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddDll;
        private System.Windows.Forms.Button btnCreateKeystore;
    }
}

