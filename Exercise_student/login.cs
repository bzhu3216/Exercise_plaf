using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise_student.ServiceExer;

namespace Exercise_student
{
    public partial class login : Form
    {
        paramst pp;
        public login()
        {
            InitializeComponent();
            pp = new Exercise_student.paramst();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var questionQuery = from o in pp.context.StudInfoes 
                                where (o.studentid  == textBox1.Text) && (o.pd == maskedTextBox1.Text)
                                select o;

            if (questionQuery.Count<StudInfo>() != 0)

            {
                pp.st = questionQuery.First<StudInfo>();
                this.Hide();
                Form1 mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new Form1(pp);
                    mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("Wrong password or username is not existed!");

            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
