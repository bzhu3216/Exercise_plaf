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
    public partial class exerList : Form
    {
        public exerList()
        {
            InitializeComponent();
        }

        private void exerList_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            EditererList  mq = null;
            if (mq == null || mq.IsDisposed)
            {
                mq = new EditererList();
                mq.ShowDialog();
               // mq.Show();
            }
            else
            {
                mq.Activate();
                mq.WindowState = FormWindowState.Normal;
            }





        }
    }
}
