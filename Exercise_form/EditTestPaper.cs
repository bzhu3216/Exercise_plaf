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
    public partial class EditTestPaper : Form
    {
        param pp;
        exerL el = null;
        V_tea_course vcc = null;
        int pagesize = 15;
        int pageNum = 0;
        List<mchoiceQues> lmq = null;
        // V_tea_course vc = null;
        public EditTestPaper(param p,exerL el1, V_tea_course vcc1)
        {
            pp = p;
            el = el1;
            
           vcc = vcc1;
            InitializeComponent();
        }

        private void EditTestPaper_Load(object sender, EventArgs e)
        {
            this.Text = el.id.ToString();


        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                  e.RowBounds.Location.Y,
                                  dataGridView1.RowHeadersWidth,
                                  e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                  e.RowBounds.Location.Y,
                                  dataGridView1.RowHeadersWidth,
                                  e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dataGridView1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        private void saveExerD(exerDetail exl)
        {   // context.StudInfo1Set
            // exerDetail etemp = null;
            var q1 = from o in pp.context.exerDetail
                     where o.lid == exl.lid && o.qid == exl.qid && o.typeq == exl.typeq
                     select o;


            if (q1.Count<exerDetail>() == 0)

            {
                pp.context.AddToexerDetail(exl);
                pp.context.SaveChanges();
            }
            else
            {


                MessageBox.Show("该题目已经存在！");

            }



        }
        /// <summary>
        /// /////////////////////
        private void display2(int a)
        {





        }
        /// </summary>
        /// <param name="a"></param>
        /// <param name="bb"></param>
        /////////////////////////////
        private void display(int a, bool bb)
        {
            int numobjective = -1;
            int con = -1;
            int diff = -1;
           
            if (vcc != null)
            {
                numobjective = (int)vcc.numobjective;
                con = (int)vcc.numcontent;
                diff = (int)vcc.diff;
                if (bb)
                {
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
                    lmq = null;
                    var questionQuery3 = (from o in pp.context.mchoiceQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (o.courseid == vcc.couseid )
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    if (questionQuery3.Count() > 0)
                    {
                        lmq = questionQuery3.ToList<mchoiceQues>();
                        dataGridView1.AutoGenerateColumns = false;                        
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
                           // dgvr.Cells[0].Style.BackColor = Color.Yellow;
                            int hh = (int)(richTextBox1.Rtf.Length / 6);
                            if (hh > 300) hh = 300;
                            dgvr.Height = hh;
                            this.dataGridView1.Rows.Add(dgvr);
                        }

                        



                    }

                    else
                    {
                        MessageBox.Show("End of  Question List!");
                        pageNum--;
                        display(comboBox1.SelectedIndex, false);

                    }


                  /*  foreach (mchoiceQues mcq in lmq)
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

                    }*/


                }  
  
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            display(comboBox1.SelectedIndex, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pageNum++;
            display(comboBox1.SelectedIndex,false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pageNum == 0) { display(comboBox1.SelectedIndex, false); ; MessageBox.Show("已经到最前"); }
            if (pageNum > 0)
            {
                pageNum--;
                display(comboBox1.SelectedIndex, false);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int selid = -1;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    selid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
            if (selid != -1)
            {
                exerDetail edl = new exerDetail();
                edl.lid = pp.exerl1;
                edl.qid = selid;
                edl.score = int.Parse(comboBox5.Text);
                edl.typeq = comboBox1.SelectedIndex;
                saveExerD(edl);
                //reloadd2(comboBox1.SelectedIndex);

            }

        }
        ///////////////////////////////



        ////////////////////////////




    }
}
