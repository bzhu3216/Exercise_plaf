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
    public partial class EditererList : Form
    {
        private db_exerciseEntities context;
        param pp;
        // List<Course> lcs = null;
        //  List<exerL> el = null;
        // int cid = -1;
        int pagesize =15;
        int pageNum = 0;
        public EditererList(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;


        }

        private void EditererList_Load(object sender, EventArgs e)
        {

            // display(0);





        }

        ///////////////start 

        private void display(int a,bool bb)
        {
            int numobjective = -1;
            int con = -1;
            int diff = -1;
            var questionQuery = from o in context.exerL
                                 where o.id == pp.exerl1
                                select o  ;
             
            exerL ell = questionQuery.First<exerL>();

            var questionQuery2 = from o in context.Course
                                where o.id == ell.courseid 
                                select o;
            Course  cc = questionQuery2.First<Course>();
            if (cc != null)
            { 
            numobjective = (int)cc.numobjective;
            con = (int)cc.numcontent;
            diff = (int)cc.diff;
                if (bb) { 
            comboBox2.Items.Clear();
            for (int i = 0; i < numobjective; i++) comboBox2.Items.Add(i + 1);
            comboBox3.Items.Clear();
            for (int i = 0; i < con; i++) comboBox3.Items.Add(i + 1);
            comboBox4.Items.Clear();
            for (int i = 0; i < diff; i++) comboBox4.Items.Add(i + 1);
                }
                int c1 = -1;
                int c2 = -1;
                int c3 = -1;
                bool b1 = false;
                bool b2 = false;
                bool b3 = false;
                if (comboBox2.Text != "")
                    c1 = int.Parse(comboBox2.Text);
                else
                    b1 = true;

                if (comboBox3.Text != "")
                    c2 = int.Parse(comboBox3.Text);
                else
                    b2 = true;

                if (comboBox4.Text != "")
                    c3 = int.Parse(comboBox4.Text);
                else
                    b3 = true;
                if (dataGridView1.DataSource != null)
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    dt.Rows.Clear();
                    dataGridView1.DataSource = dt;
                }
                else

                {
                    dataGridView1.Rows.Clear();

                }
                if (a == 0)
            {
               // comboBox1.Text = comboBox1.Items[a].ToString();
                comboBox5.Text = "1";
                   
                    var questionQuery3 = (from o in context.mchoiceQues 
                                         where (b1  || o.objective  == c1)
                                      && (b2 || o.con  == c2)
                                      && (b3 || o.diff  == c3)
                                       select o).Skip(pageNum * pagesize).Take(pagesize); 
                    List<mchoiceQues> lmq = questionQuery3.ToList<mchoiceQues>();

                   
                    foreach (mchoiceQues mcq in lmq)
                    {
                        
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                         DataGridViewRow dgvr = new DataGridViewRow();
                        dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                        {
                            dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        dgvr.Cells[1].Value = richTextBox1.Rtf;
                        dgvr.Cells[0].Value = mcq.id;
                        int hh = (int)(richTextBox1.Rtf.Length / 6);
                        if (hh > 300) hh = 300;
                        dgvr.Height = hh;
                        this.dataGridView1.Rows.Add(dgvr);

                    }
                                   

                    }

               
                /////end mq
                if (a == 1)
                {
                    // comboBox1.Text = comboBox1.Items[a].ToString();
                    comboBox5.Text = "1";                   
                    var questionQuery3 =( from o in context.TFQues 
                                         where (b1 || o.objective == c1)
                                      && (b2 || o.con == c2)
                                      && (b3 || o.diff == c3)
                                         select o).Skip(pageNum * pagesize).Take(pagesize); 
                    List<TFQues> lmq = questionQuery3.ToList<TFQues>();
                    this.dataGridView1.RowTemplate.Height = 100;                   
                    foreach (TFQues mcq in lmq)
                    {
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        dataGridView1.RowHeadersWidthSizeMode =  DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                        {
                           
                            dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        dgvr.Cells[1].Value = richTextBox1.Rtf;
                        dgvr.Cells[0].Value = mcq.id;                            
                                int hh = (int)(richTextBox1.Rtf.Length / 10);
                                if (hh > 300) hh = 300;
                                dgvr.Height = hh;                             
                            this.dataGridView1.Rows.Add(dgvr);                                              

                    }

                }
                ////endTF











            }






        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display(comboBox1.SelectedIndex,true );
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            display(comboBox1.SelectedIndex,false);
            comboBox2.Text = comboBox2.SelectedText;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            display(comboBox1.SelectedIndex, false);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            display(comboBox1.SelectedIndex, false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pageNum = pageNum + 1;
          
            display(comboBox1.SelectedIndex, false);




        }

        private void button4_Click(object sender, EventArgs e)
        {
             pageNum = pageNum - 1;
            if (pageNum < 0) pageNum = 0;
             display(comboBox1.SelectedIndex, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {













        }





        ///////////////end









    }
}
