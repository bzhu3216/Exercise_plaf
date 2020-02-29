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
using System.Configuration;
namespace Exercise_form
{
   
    public partial class addA : Form
    {
        private db_exerciseEntities context;
        param pp;
        List<V_tea_course > lcs = null;
        int cid = -1;
       // long maxsize = 0;
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
           // String maxstr = ConfigurationManager.AppSettings["maxsize"].ToString();
           // maxsize = long.Parse(maxstr);


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
           // MessageBox.Show((richTextBox1.Rtf.Length).ToString());
            if (checkem())
            {
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
                if ((mcq.question.Length+mcq.answ.Length ) < pp.maxsize)
                {
                    context.AddToAQues(mcq);
                    //////end write richtext
                    context.SaveChanges();
                    rquestion.Text = "";
                    richTextBox1.Text = "";
                }
                else
                {
                    MessageBox.Show("请使用小一点的图片，建议不使用");
                }

            }
            else
            {
                MessageBox.Show("数据填完整"); 
            }


        }


        private bool checkem()
        {
            bool vv = true;
            if (comboBox1.Text == "") vv = false;
            if (comboBox2.Text == "") vv = false;
            if (comboBox3.Text == "") vv = false;
           // if (comboBox4.Text == "") vv = false;
            if (comboBox2.Text == "") vv = false;
            if (rquestion.Text == "") vv = false;
            return vv;
        }


    }
}
