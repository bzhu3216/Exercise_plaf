using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise_form.ServiceReference1;
namespace Exercise_form
{
    public partial class Login : Form
    {
        param pp;
        public Login(param p)
        {
            InitializeComponent();
            pp = p;
           
        }
        public Login()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            var questionQuery = from o in pp.context.teacherinfo
                                where (o.teacherid==textBox1.Text) &&  (o.pd== textBox2.Text)
                                select o;

            if (questionQuery.Count<teacherinfo>() != 0)

            {
                pp.teacher = questionQuery.First<teacherinfo>();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong password or username is not existed!");

            }


        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
