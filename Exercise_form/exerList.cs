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
using System.Data.Services.Client;

namespace Exercise_form
{
    public partial class exerList : Form
    {
        private db_exerciseEntities context;
        param pp;
        List<Course> lcs = null;
        List<exerL> el = null;
        int cid = -1;
        public exerList(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;
        }

        private void exerList_Load(object sender, EventArgs e)
        {
            context = pp.context;
            var questionQuery = from o in context.Course
                                select o;
            lcs = questionQuery.ToList<Course>();
            comboBox1.DataSource = lcs;

            comboBox1.ValueMember = "CourseName";

            comboBox1.Text = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();


        }

        private void button3_Click(object sender, EventArgs e)
        {

            EditererList  mq = null;
            if (mq == null || mq.IsDisposed)
            {
                mq = new EditererList(pp);
                mq.ShowDialog();
               // mq.Show();
            }
            else
            {
                mq.Activate();
                mq.WindowState = FormWindowState.Normal;
            }





        }

        private void button1_Click(object sender, EventArgs e)
        {

            // try
            // {
            // Instantiate the DataServiceContext.


            exerL  mcq = new exerL();
            // mcq.answ = comboBox4.SelectedIndex + 1;
            
            mcq.courseid  = cid;
            mcq.teacherid = pp.teacher.teacherid;
            mcq.name = textBox1.Text;
            mcq.pub  = false;
            ////////////write richtext


            if (comboBox1.Text != ""&& textBox1.Text != "")
            {
                context.AddToexerL(mcq);
               
                context.SaveChanges();
                textBox1.Text = "";
                updatalist();


            }


            // Make the DataServiceCollection<T> the binding source for the Grid.
            //  }
            //  catch (Exception ex)
            // {
            //     MessageBox.Show(ex.ToString());
            // }









        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numobjective = 0;
            int con = 0;
            int diff = 0;

            foreach (Course cc in lcs)
                if (comboBox1.Text == cc.CourseName)
                {
                    numobjective = (int)cc.numobjective;
                    con = (int)cc.numcontent;
                    diff = (int)cc.diff;
                    cid = (int)cc.id;
                }
            updatalist();

        }

        /////////////////////////////////my function
        private void  updatalist()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            context = pp.context;
            var questionQuery = from o in context.exerL 
                                where o.courseid ==cid 
                                select o;
            el = questionQuery.ToList<exerL>();

            foreach (exerL obel in el) {
                if (obel.pub == true)
                    listBox1.Items.Add(obel.name);
                if (obel.teacherid  == pp.teacher.teacherid )
                  listBox2.Items.Add(obel.name);



            }







        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                pp.exerl1 = el[listBox2.SelectedIndex].id;
                EditererList mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new EditererList(pp);
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








        ///////////////////////////////////end my function


    }
}
