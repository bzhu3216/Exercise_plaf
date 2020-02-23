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
    public partial class adde : Form
    {
        private db_exerciseEntities context;
        param pp;
        List<V_tea_course> lcs = null;
        int cid = -1;
        public adde(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;
        }

        private void adde_Load(object sender, EventArgs e)
        {
            context = pp.context;           
            lcs = pp.ltea_c;
            comboBox5.DataSource = lcs;
            comboBox5.ValueMember = "CourseName";
            comboBox5.Text = "";
            comboBox4.SelectedIndex = 0;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

            int numobjective = 0;
            int con = 0;
            int diff = 0;

            foreach (V_tea_course cc in lcs)
                if (comboBox5.Text == cc.CourseName)
                {
                    numobjective = (int)cc.numobjective;
                    con = (int)cc.numcontent;
                    diff = (int)cc.diff;
                    cid = (int)cc.couseid;
                }
            comboBox1.Items.Clear();
            for (int i = 0; i < numobjective; i++) comboBox1.Items.Add(i + 1);
            comboBox2.Items.Clear();
            for (int i = 0; i < con; i++) comboBox2.Items.Add(i + 1);
            comboBox3.Items.Clear();
            for (int i = 0; i < diff; i++) comboBox3.Items.Add(i + 1);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0) textBox1.Text = "I:       ";
            if (comboBox4.SelectedIndex == 1) textBox1.Text = "I:       II:";
            if (comboBox4.SelectedIndex == 2) textBox1.Text = "I:        II:        III:         ";
        }

        private void button1_Click(object sender, EventArgs e)
        {  if (checkem())
            {
                eQues mcq = new eQues();
                // mcq.answ = comboBox4.SelectedIndex + 1;
                mcq.con = Convert.ToInt16(comboBox2.Text);
                mcq.diff = Convert.ToInt16(comboBox3.Text);
                mcq.objective = Convert.ToInt16(comboBox1.Text);
                mcq.courseid = cid;
                mcq.teacherid = pp.teacher.teacherid;
                mcq.emnum = comboBox4.SelectedIndex + 1;
                ////////////write richtext

                System.IO.MemoryStream mstream = new System.IO.MemoryStream();
                this.rquestion.SaveFile(mstream, RichTextBoxStreamType.RichText);
                mcq.question = mstream.ToArray();
                mcq.answ = textBox1.Text;              
                    context.AddToeQues(mcq);
                    context.SaveChanges();
                    rquestion.Text = "";

                
            }
            else
            {
                MessageBox.Show("Please complete the data"); 
            }


        }


        private bool checkem()
        {
            bool vv= true;
            if (comboBox1.Text == "") vv = false;
            if (comboBox2.Text == "") vv = false;
            if (comboBox3.Text == "") vv = false;
            if (comboBox4.Text == "") vv = false;
            if (comboBox2.Text == "") vv = false;
            return vv;
        }





    }
}
