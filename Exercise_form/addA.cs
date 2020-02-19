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
   
    public partial class addA : Form
    {
        private db_exerciseEntities context;
        param pp;
        List<V_tea_course > lcs = null;
        int cid = -1;
        public addA(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;
        }

        private void addA_Load(object sender, EventArgs e)
        {

            context = pp.context;
            /* var questionQuery = from o in context.Course
                                 select o;
             lcs = questionQuery.ToList<Course>();*/
            lcs = pp.ltea_c;
            comboBox5.DataSource = lcs;

            comboBox5.ValueMember = "CourseName";

            comboBox5.Text = "";


        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            int numobjective = 0;
            int con = 0;
            int diff = 0;

            foreach (V_tea_course  cc in lcs)
                if (comboBox5.Text == cc.CourseName)
                {
                    numobjective = (int)cc.numobjective;
                    con = (int)cc.numcontent;
                    diff = (int)cc.diff;
                    cid = (int)cc.couseid ;
                }
            comboBox1.Items.Clear();
            for (int i = 0; i < numobjective; i++) comboBox1.Items.Add(i + 1);
            comboBox2.Items.Clear();
            for (int i = 0; i < con; i++) comboBox2.Items.Add(i + 1);
            comboBox3.Items.Clear();
            for (int i = 0; i < diff; i++) comboBox3.Items.Add(i + 1);




        }

        private void button1_Click(object sender, EventArgs e)
        {

            // try
            // {
            // Instantiate the DataServiceContext.


            AQues mcq = new AQues();
            // mcq.answ = comboBox4.SelectedIndex + 1;
            mcq.con = Convert.ToInt16(comboBox2.Text);
            mcq.diff = Convert.ToInt16(comboBox3.Text);
            mcq.objective = Convert.ToInt16(comboBox1.Text);
            mcq.courseid = cid;
            mcq.teacherid = pp.teacher.teacherid;
            ////////////write richtext

            System.IO.MemoryStream mstream = new System.IO.MemoryStream();
            this.rquestion.SaveFile(mstream, RichTextBoxStreamType.RichText);
            System.IO.MemoryStream mstream2 = new System.IO.MemoryStream();
            this.richTextBox1.SaveFile(mstream2, RichTextBoxStreamType.RichText);
            //将流转换成数组
            //  byte[] bWrite = mstream.ToArray();
            mcq.question = mstream.ToArray();
            mcq.answ = mstream2.ToArray(); ;

            if (comboBox5.Text != "")
            {
                context.AddToAQues(mcq);
                //////end write richtext

                context.SaveChanges();
                rquestion.Text = "";
                richTextBox1.Text = "";
            }
            // Make the DataServiceCollection<T> the binding source for the Grid.
            //  }
            //  catch (Exception ex)
            // {
            //     MessageBox.Show(ex.ToString());
            // }







        }
    }
}
