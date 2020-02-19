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
    public partial class UPmq : Form
    {
        param pp;
        List<V_tea_course> lvtc = null;
        int qid = -1;
        int pagesize = 20;
        int pageNum = 0;
        public UPmq(param p,int qid)
        {
            InitializeComponent();
            pp = p;
            lvtc = pp.ltea_c;

        }

        private void UPmq_Load(object sender, EventArgs e)
        {

            comboBox1.DataSource = lvtc;
            comboBox1.ValueMember = "CourseName";
            comboBox1.Text = "";
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            if (qid != -1 && !(pp.teacher.teacherid.Equals("1536")))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
            }
          //  loadcom();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        //////////////////////////////////////////////////

        private void loadcom()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            int numc = lvtc[comboBox1.SelectedIndex].numcontent;
            int numo = lvtc[comboBox1.SelectedIndex].numobjective;
            int numd = lvtc[comboBox1.SelectedIndex].diff;
            for (int i = 1; i <= numc;i++)
            {
                comboBox2.Items.Add(i.ToString()); 
            }
            for (int i = 1; i <= numo; i++)
            {
                comboBox3.Items.Add(i.ToString());
            }

            for (int i = 1; i <= numd; i++)
            {
                comboBox4.Items.Add(i.ToString());
            }



        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadcom();
        }
        /////////////////////

        //loadq

        private void display(int a, bool bb)
        {
           
                        
           
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

                    var questionQuery3 = (from o in pp.context.mchoiceQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (o.courseid == lvtc[comboBox1.SelectedIndex].couseid )
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
                    var questionQuery3 = (from o in pp.context.TFQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                        && (o.courseid == lvtc[comboBox1.SelectedIndex].couseid)
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    List<TFQues> lmq = questionQuery3.ToList<TFQues>();
                    this.dataGridView1.RowTemplate.Height = 100;
                    foreach (TFQues mcq in lmq)
                    {
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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

                //startSques
                if (a == 3)
                {
                    // comboBox1.Text = comboBox1.Items[a].ToString();
                    comboBox5.Text = "5";
                    var questionQuery3 = (from o in pp.context.SQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                        && (o.courseid == lvtc[comboBox1.SelectedIndex].couseid)
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    List<SQues> lmq = questionQuery3.ToList<SQues>();
                    this.dataGridView1.RowTemplate.Height = 100;
                    foreach (SQues mcq in lmq)
                    {
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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
                //endSques


                //startAques
                if (a == 4)
                {
                    // comboBox1.Text = comboBox1.Items[a].ToString();
                    comboBox5.Text = "10";
                    var questionQuery3 = (from o in pp.context.AQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                        && (o.courseid == lvtc[comboBox1.SelectedIndex].couseid)
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    List<AQues> lmq = questionQuery3.ToList<AQues>();
                    this.dataGridView1.RowTemplate.Height = 100;
                    foreach (AQues mcq in lmq)
                    {
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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
                //endAques





            }




            ///endloadw




            //////////////////////////////////////endclass
        }
}
