﻿namespace Exercise_form
{
    partial class MianWindows
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.课程管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.班级管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.班级管理ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.班级名单ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.题库管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.输入题目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.填空题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.判断题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.简单题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.分析题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改土木ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.习题管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.习题生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批改习题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出成绩ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出习题ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.用户管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.课程管理ToolStripMenuItem,
            this.班级管理ToolStripMenuItem,
            this.题库管理ToolStripMenuItem,
            this.习题管理ToolStripMenuItem,
            this.用户管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(826, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 课程管理ToolStripMenuItem
            // 
            this.课程管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加课程ToolStripMenuItem,
            this.修改课程ToolStripMenuItem});
            this.课程管理ToolStripMenuItem.Name = "课程管理ToolStripMenuItem";
            this.课程管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.课程管理ToolStripMenuItem.Text = "课程管理";
            // 
            // 添加课程ToolStripMenuItem
            // 
            this.添加课程ToolStripMenuItem.Name = "添加课程ToolStripMenuItem";
            this.添加课程ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.添加课程ToolStripMenuItem.Text = "添加课程";
            // 
            // 修改课程ToolStripMenuItem
            // 
            this.修改课程ToolStripMenuItem.Name = "修改课程ToolStripMenuItem";
            this.修改课程ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改课程ToolStripMenuItem.Text = "修改课程";
            // 
            // 班级管理ToolStripMenuItem
            // 
            this.班级管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.班级管理ToolStripMenuItem1,
            this.班级名单ToolStripMenuItem});
            this.班级管理ToolStripMenuItem.Name = "班级管理ToolStripMenuItem";
            this.班级管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.班级管理ToolStripMenuItem.Text = "班级管理";
            // 
            // 班级管理ToolStripMenuItem1
            // 
            this.班级管理ToolStripMenuItem1.Name = "班级管理ToolStripMenuItem1";
            this.班级管理ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.班级管理ToolStripMenuItem1.Text = "班级信息";
            this.班级管理ToolStripMenuItem1.Click += new System.EventHandler(this.班级管理ToolStripMenuItem1_Click);
            // 
            // 班级名单ToolStripMenuItem
            // 
            this.班级名单ToolStripMenuItem.Name = "班级名单ToolStripMenuItem";
            this.班级名单ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.班级名单ToolStripMenuItem.Text = "班级名单";
            this.班级名单ToolStripMenuItem.Click += new System.EventHandler(this.班级名单ToolStripMenuItem_Click);
            // 
            // 题库管理ToolStripMenuItem
            // 
            this.题库管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.输入题目ToolStripMenuItem,
            this.修改土木ToolStripMenuItem});
            this.题库管理ToolStripMenuItem.Name = "题库管理ToolStripMenuItem";
            this.题库管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.题库管理ToolStripMenuItem.Text = "题库管理";
            // 
            // 输入题目ToolStripMenuItem
            // 
            this.输入题目ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择题ToolStripMenuItem,
            this.填空题ToolStripMenuItem,
            this.判断题ToolStripMenuItem,
            this.简单题ToolStripMenuItem,
            this.分析题ToolStripMenuItem});
            this.输入题目ToolStripMenuItem.Name = "输入题目ToolStripMenuItem";
            this.输入题目ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.输入题目ToolStripMenuItem.Text = "输入题目";
            // 
            // 选择题ToolStripMenuItem
            // 
            this.选择题ToolStripMenuItem.Name = "选择题ToolStripMenuItem";
            this.选择题ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.选择题ToolStripMenuItem.Text = "选择题";
            this.选择题ToolStripMenuItem.Click += new System.EventHandler(this.选择题ToolStripMenuItem_Click);
            // 
            // 填空题ToolStripMenuItem
            // 
            this.填空题ToolStripMenuItem.Name = "填空题ToolStripMenuItem";
            this.填空题ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.填空题ToolStripMenuItem.Text = "填空题";
            // 
            // 判断题ToolStripMenuItem
            // 
            this.判断题ToolStripMenuItem.Name = "判断题ToolStripMenuItem";
            this.判断题ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.判断题ToolStripMenuItem.Text = "判断题";
            this.判断题ToolStripMenuItem.Click += new System.EventHandler(this.判断题ToolStripMenuItem_Click);
            // 
            // 简单题ToolStripMenuItem
            // 
            this.简单题ToolStripMenuItem.Name = "简单题ToolStripMenuItem";
            this.简单题ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.简单题ToolStripMenuItem.Text = "简答题";
            this.简单题ToolStripMenuItem.Click += new System.EventHandler(this.简单题ToolStripMenuItem_Click);
            // 
            // 分析题ToolStripMenuItem
            // 
            this.分析题ToolStripMenuItem.Name = "分析题ToolStripMenuItem";
            this.分析题ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.分析题ToolStripMenuItem.Text = "分析题";
            this.分析题ToolStripMenuItem.Click += new System.EventHandler(this.分析题ToolStripMenuItem_Click);
            // 
            // 修改土木ToolStripMenuItem
            // 
            this.修改土木ToolStripMenuItem.Name = "修改土木ToolStripMenuItem";
            this.修改土木ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改土木ToolStripMenuItem.Text = "修改题目";
            // 
            // 习题管理ToolStripMenuItem
            // 
            this.习题管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.习题生成ToolStripMenuItem,
            this.批改习题ToolStripMenuItem,
            this.导出成绩ToolStripMenuItem,
            this.导出习题ToolStripMenuItem});
            this.习题管理ToolStripMenuItem.Name = "习题管理ToolStripMenuItem";
            this.习题管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.习题管理ToolStripMenuItem.Text = "习题管理";
            // 
            // 习题生成ToolStripMenuItem
            // 
            this.习题生成ToolStripMenuItem.Name = "习题生成ToolStripMenuItem";
            this.习题生成ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.习题生成ToolStripMenuItem.Text = "习题生成";
            this.习题生成ToolStripMenuItem.Click += new System.EventHandler(this.习题生成ToolStripMenuItem_Click);
            // 
            // 批改习题ToolStripMenuItem
            // 
            this.批改习题ToolStripMenuItem.Name = "批改习题ToolStripMenuItem";
            this.批改习题ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.批改习题ToolStripMenuItem.Text = "批改习题";
            // 
            // 导出成绩ToolStripMenuItem
            // 
            this.导出成绩ToolStripMenuItem.Name = "导出成绩ToolStripMenuItem";
            this.导出成绩ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.导出成绩ToolStripMenuItem.Text = "导出成绩";
            // 
            // 导出习题ToolStripMenuItem
            // 
            this.导出习题ToolStripMenuItem.Name = "导出习题ToolStripMenuItem";
            this.导出习题ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.导出习题ToolStripMenuItem.Text = "导出习题";
            // 
            // 用户管理ToolStripMenuItem
            // 
            this.用户管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.登录ToolStripMenuItem,
            this.修改密码ToolStripMenuItem});
            this.用户管理ToolStripMenuItem.Name = "用户管理ToolStripMenuItem";
            this.用户管理ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.用户管理ToolStripMenuItem.Text = "用户管理";
            // 
            // 登录ToolStripMenuItem
            // 
            this.登录ToolStripMenuItem.Name = "登录ToolStripMenuItem";
            this.登录ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.登录ToolStripMenuItem.Text = "登录";
            this.登录ToolStripMenuItem.Click += new System.EventHandler(this.登录ToolStripMenuItem_Click);
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            // 
            // MianWindows
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 531);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MianWindows";
            this.Text = "MianWindows";
            this.Load += new System.EventHandler(this.MianWindows_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 课程管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加课程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改课程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 班级管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 题库管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 输入题目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 填空题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 判断题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 简单题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 分析题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改土木ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 习题管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 习题生成ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批改习题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出成绩ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出习题ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 班级管理ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 班级名单ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 用户管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
    }
}