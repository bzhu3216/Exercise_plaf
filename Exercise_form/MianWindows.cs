using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise_form
{
    public partial class MianWindows : Form
    {
        private classinfoF fm1;
        private param pp = new param();
        public MianWindows()
        {
            InitializeComponent();
        }

        private void 班级管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (fm1 == null || fm1.IsDisposed)
            {
                fm1 = new classinfoF(pp);
                fm1.MdiParent = this;
                fm1.Show();
            }
            else
            {
                fm1.Activate();
                fm1.WindowState = FormWindowState.Normal;
            }


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MianWindows_Load(object sender, EventArgs e)
        {

        }

        private void 登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login fm2=null;
            if (fm2 == null || fm2.IsDisposed)
            {
                fm2 = new Login(pp);
                fm2.MdiParent = this;
                fm2.Show();
            }
            else
            {
                fm2.Activate();
                fm2.WindowState = FormWindowState.Normal;
            }



        }

        private void 班级名单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NameList fm2 = null;
            if (fm2 == null || fm2.IsDisposed)
            {
                fm2 = new NameList(pp);
                fm2.MdiParent = this;
                fm2.Show();
            }
            else
            {
                fm2.Activate();
                fm2.WindowState = FormWindowState.Normal;
            }

        }
    }
}
