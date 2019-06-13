namespace SlnBatchBuildTool
{
    partial class SlnBatchBuildView
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
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listSlns = new System.Windows.Forms.ListBox();
            this.btnReduce = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(12, 118);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(233, 25);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.TabStop = false;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(251, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(21, 21);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "+";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listSlns
            // 
            this.listSlns.AllowDrop = true;
            this.listSlns.FormattingEnabled = true;
            this.listSlns.ItemHeight = 12;
            this.listSlns.Location = new System.Drawing.Point(12, 12);
            this.listSlns.Name = "listSlns";
            this.listSlns.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listSlns.Size = new System.Drawing.Size(233, 100);
            this.listSlns.TabIndex = 3;
            this.listSlns.TabStop = false;
            this.listSlns.DragDrop += new System.Windows.Forms.DragEventHandler(this.listSlns_DragDrop);
            this.listSlns.DragEnter += new System.Windows.Forms.DragEventHandler(this.listSlns_DragEnter);
            this.listSlns.DoubleClick += new System.EventHandler(this.listSlns_DoubleClick);
            // 
            // btnReduce
            // 
            this.btnReduce.Location = new System.Drawing.Point(251, 91);
            this.btnReduce.Name = "btnReduce";
            this.btnReduce.Size = new System.Drawing.Size(21, 21);
            this.btnReduce.TabIndex = 4;
            this.btnReduce.TabStop = false;
            this.btnReduce.Text = "-";
            this.btnReduce.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.btnReduce.UseVisualStyleBackColor = true;
            this.btnReduce.Click += new System.EventHandler(this.btnReduce_Click);
            // 
            // SlnBatchBuildView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 162);
            this.Controls.Add(this.btnReduce);
            this.Controls.Add(this.listSlns);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnBuild);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SlnBatchBuildView";
            this.Text = "SlnBatchBuildTool";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listSlns;
        private System.Windows.Forms.Button btnReduce;
    }
}

