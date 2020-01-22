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
        private classinfo fm1;
        public MianWindows()
        {
            InitializeComponent();
        }

        private void 班级管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (fm1 == null || fm1.IsDisposed)
            {
                fm1 = new classinfo();
                fm1.MdiParent = this;
                fm1.Show();
            }
            else
            {
                fm1.Activate();
                fm1.WindowState = FormWindowState.Normal;
            }


        }
    }
}
