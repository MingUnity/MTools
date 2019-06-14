namespace ReferenceReplaceTool
{
    partial class ReferenceReplacerView
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
            this.listCsproj = new System.Windows.Forms.ListBox();
            this.listDll = new System.Windows.Forms.ListBox();
            this.btnReplace = new System.Windows.Forms.Button();
            this.btnReduceCsproj = new System.Windows.Forms.Button();
            this.btnAddCsproj = new System.Windows.Forms.Button();
            this.btnReduceDll = new System.Windows.Forms.Button();
            this.btnAddDll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listCsproj
            // 
            this.listCsproj.AllowDrop = true;
            this.listCsproj.FormattingEnabled = true;
            this.listCsproj.ItemHeight = 12;
            this.listCsproj.Location = new System.Drawing.Point(12, 12);
            this.listCsproj.Name = "listCsproj";
            this.listCsproj.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listCsproj.Size = new System.Drawing.Size(233, 100);
            this.listCsproj.TabIndex = 0;
            this.listCsproj.TabStop = false;
            this.listCsproj.DragDrop += new System.Windows.Forms.DragEventHandler(this.listCsproj_DragDrop);
            this.listCsproj.DragEnter += new System.Windows.Forms.DragEventHandler(this.listCsproj_DragEnter);
            this.listCsproj.DoubleClick += new System.EventHandler(this.listCsproj_DoubleClick);
            // 
            // listDll
            // 
            this.listDll.AllowDrop = true;
            this.listDll.FormattingEnabled = true;
            this.listDll.ItemHeight = 12;
            this.listDll.Location = new System.Drawing.Point(12, 137);
            this.listDll.Name = "listDll";
            this.listDll.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listDll.Size = new System.Drawing.Size(233, 100);
            this.listDll.TabIndex = 1;
            this.listDll.TabStop = false;
            this.listDll.DragDrop += new System.Windows.Forms.DragEventHandler(this.listDll_DragDrop);
            this.listDll.DragEnter += new System.Windows.Forms.DragEventHandler(this.listDll_DragEnter);
            this.listDll.DoubleClick += new System.EventHandler(this.listDll_DoubleClick);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(12, 249);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(233, 21);
            this.btnReplace.TabIndex = 2;
            this.btnReplace.TabStop = false;
            this.btnReplace.Text = "Replace";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // btnReduceCsproj
            // 
            this.btnReduceCsproj.Location = new System.Drawing.Point(251, 91);
            this.btnReduceCsproj.Name = "btnReduceCsproj";
            this.btnReduceCsproj.Size = new System.Drawing.Size(21, 21);
            this.btnReduceCsproj.TabIndex = 6;
            this.btnReduceCsproj.TabStop = false;
            this.btnReduceCsproj.Text = "-";
            this.btnReduceCsproj.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnReduceCsproj.UseVisualStyleBackColor = true;
            this.btnReduceCsproj.Click += new System.EventHandler(this.btnReduceCsproj_Click);
            // 
            // btnAddCsproj
            // 
            this.btnAddCsproj.Location = new System.Drawing.Point(251, 12);
            this.btnAddCsproj.Name = "btnAddCsproj";
            this.btnAddCsproj.Size = new System.Drawing.Size(21, 21);
            this.btnAddCsproj.TabIndex = 5;
            this.btnAddCsproj.TabStop = false;
            this.btnAddCsproj.Text = "+";
            this.btnAddCsproj.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnAddCsproj.UseVisualStyleBackColor = true;
            this.btnAddCsproj.Click += new System.EventHandler(this.btnAddCsproj_Click);
            // 
            // btnReduceDll
            // 
            this.btnReduceDll.Location = new System.Drawing.Point(251, 216);
            this.btnReduceDll.Name = "btnReduceDll";
            this.btnReduceDll.Size = new System.Drawing.Size(21, 21);
            this.btnReduceDll.TabIndex = 8;
            this.btnReduceDll.TabStop = false;
            this.btnReduceDll.Text = "-";
            this.btnReduceDll.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnReduceDll.UseVisualStyleBackColor = true;
            this.btnReduceDll.Click += new System.EventHandler(this.btnReduceDll_Click);
            // 
            // btnAddDll
            // 
            this.btnAddDll.Location = new System.Drawing.Point(251, 137);
            this.btnAddDll.Name = "btnAddDll";
            this.btnAddDll.Size = new System.Drawing.Size(21, 21);
            this.btnAddDll.TabIndex = 7;
            this.btnAddDll.TabStop = false;
            this.btnAddDll.Text = "+";
            this.btnAddDll.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnAddDll.UseVisualStyleBackColor = true;
            this.btnAddDll.Click += new System.EventHandler(this.btnAddDll_Click);
            // 
            // ReferenceReplacerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 282);
            this.Controls.Add(this.btnReduceDll);
            this.Controls.Add(this.btnAddDll);
            this.Controls.Add(this.btnReduceCsproj);
            this.Controls.Add(this.btnAddCsproj);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.listDll);
            this.Controls.Add(this.listCsproj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReferenceReplacerView";
            this.Text = "RefReplacer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listCsproj;
        private System.Windows.Forms.ListBox listDll;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReduceCsproj;
        private System.Windows.Forms.Button btnAddCsproj;
        private System.Windows.Forms.Button btnReduceDll;
        private System.Windows.Forms.Button btnAddDll;
    }
}

