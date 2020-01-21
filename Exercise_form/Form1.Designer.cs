namespace Exercise_form
{
    partial class Form1
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
            this.quesView = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ques = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.objective = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.con = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.quesView)).BeginInit();
            this.SuspendLayout();
            // 
            // quesView
            // 
            this.quesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.quesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.ques,
            this.objective,
            this.con});
            this.quesView.Location = new System.Drawing.Point(12, 117);
            this.quesView.Name = "quesView";
            this.quesView.RowTemplate.Height = 23;
            this.quesView.Size = new System.Drawing.Size(862, 443);
            this.quesView.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(484, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "题号";
            this.id.Name = "id";
            // 
            // ques
            // 
            this.ques.DataPropertyName = "question";
            this.ques.HeaderText = "问题";
            this.ques.Name = "ques";
            // 
            // objective
            // 
            this.objective.DataPropertyName = "objective";
            this.objective.HeaderText = "目标";
            this.objective.Name = "objective";
            // 
            // con
            // 
            this.con.DataPropertyName = "con";
            this.con.HeaderText = "章节";
            this.con.Name = "con";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 572);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.quesView);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.quesView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView quesView;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ques;
        private System.Windows.Forms.DataGridViewTextBoxColumn objective;
        private System.Windows.Forms.DataGridViewTextBoxColumn con;
    }
}

