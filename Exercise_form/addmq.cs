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
    public partial class addmq : Form
    {
        private db_exerciseEntities context;
       // private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        param pp;
        List<V_tea_course > lcs = null;
        int cid=-1;
        public addmq(param p)
        {
            InitializeComponent();
            pp = p;
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (checkem())
            {


                mchoiceQues mcq = new mchoiceQues();
                mcq.answ = comboBox4.SelectedIndex + 1;
                mcq.con = Convert.ToInt16(comboBox2.Text);
                mcq.diff = Convert.ToInt16(comboBox3.Text);
                mcq.objective = Convert.ToInt16(comboBox1.Text);
                mcq.courseid = cid;
                mcq.teacherid = pp.teacher.teacherid;
                ////////////write richtext

                System.IO.MemoryStream mstream = new System.IO.MemoryStream();
                this.rquestion.SaveFile(mstream, RichTextBoxStreamType.RichText);
                //将流转换成数组
                //  byte[] bWrite = mstream.ToArray();
                mcq.question = mstream.ToArray();

               if(mcq.question.Length  <pp.maxsize )
                { 
                    context.AddTomchoiceQues(mcq);
                    //////end write richtext

                    context.SaveChanges();
                    rquestion.Text = "";
                    comboBox4.Text = "";
                }
                else
                {
                    MessageBox.Show("请使用小一点的图片，建议不使用");
                }



            }
            else
            {

                MessageBox.Show("把数据填完整");
            }










        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rquestion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            rquestion.SaveFile(@"d:\a.rtf");
        }

        private void addmq_Load(object sender, EventArgs e)
        {

            context =pp.context ;
            /*  var questionQuery = from o in context.Course 
                                  select o;
              lcs = questionQuery.ToList<Course>();*/
            lcs = pp.ltea_c;
            comboBox5.DataSource = lcs;

            comboBox5.ValueMember = "CourseName";

            comboBox5.Text = "";



        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numobjective=0;
            int con=0;
            int diff=0;

            foreach (V_tea_course  cc in lcs)
                if (comboBox5.Text == cc.CourseName)
                {
                    numobjective=(int)cc.numobjective ;
                    con=(int)cc.numcontent ;
                    diff=(int)cc.diff;
                    cid = (int)cc.couseid ;
                }
            comboBox1.Items.Clear();
            for (int i = 0; i < numobjective; i++) comboBox1.Items.Add(i+1);
            comboBox2.Items.Clear();
            for (int i = 0; i < con; i++) comboBox2.Items.Add(i + 1);
            comboBox3.Items.Clear();
            for (int i = 0; i < diff; i++) comboBox3.Items.Add(i + 1);


        }

        private bool checkem()
        {
            bool vv = true;
            if (comboBox1.Text == "") vv = false;
            if (comboBox2.Text == "") vv = false;
            if (comboBox3.Text == "") vv = false;
            if (comboBox4.Text == "") vv = false;
            if (comboBox5.Text == "") vv = false;
            if (rquestion.Text == "") vv = false;
            return vv;
        }
    }
}
