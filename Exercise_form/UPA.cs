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
    public partial class UPA : Form
    {
        param pp;
        List<V_tea_course> lvtc = null;
        int qid = -1;
        int pagesize = 10;
        int pageNum = 0;
     
        List<AQues> lTF = null;
        AQues ctf = null;
        public UPA(param p,int qid)
        {
            InitializeComponent();
            pp = p;
            lvtc = pp.ltea_c;
            this.qid = qid;
        }

        private void UPA_Load(object sender, EventArgs e)
        {

            comboBox1.DataSource = lvtc;
            comboBox1.ValueMember = "CourseName";
            comboBox1.Text = "";
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            if (qid != -1)
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                textBox1.Text = qid.ToString();
                int countvc = lvtc.Count;

                for (int kk = 0; kk < countvc; kk++)
                {

                    if (lvtc[kk].couseid == pp.updataccid) { comboBox1.SelectedIndex = kk; comboBox1.Text = lvtc[kk].CourseName; }
                }

                pageNum = 0;
                display(2, true);

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
            pageNum = 1;
            loadcom();
        }
        /////////////////////

        //loadq

        private void display(int a, bool bb)
        {


            int c0 = -1;
            int c1 = -1;
                int c2 = -1;
                int c3 = -1;
            bool b0 = false;
            bool b1 = false;
                bool b2 = false;
                bool b3 = false;
            if (textBox1 .Text != "")
                c0 = int.Parse(textBox1.Text);
            else
                b0 = true;
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
                if (a == 2)
                {
                  lTF = null;
                ctf = null;
                 var questionQuery3 = (from o in pp.context.AQues
                                       where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (b0 || o.id == c0)
                                       && (o.courseid == lvtc[comboBox1.SelectedIndex].couseid )
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                if (questionQuery3.Count<AQues>() > 0)
                {      
                       lTF = questionQuery3.ToList<AQues>();


                    foreach (AQues mcq in lTF)
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

                        //////////////////////////////
                        /////////////////////////////////////////////

                    }
                }

                else
                {
                    pageNum--;
                    MessageBox.Show("没有了啊！"); 
                }


            }


                
               

            





            }

        private void button2_Click(object sender, EventArgs e)
        {
            pageNum++;
            display(2, true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pageNum = 0;         
            if (comboBox1.SelectedIndex > 0)
                display(2, true);
            else
                MessageBox.Show("请选择课程！");
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

        private void splitContainer1_Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pageNum == 0) { display(2, true); MessageBox.Show("已经到最前"); }
            if (pageNum > 0)
            {
                pageNum--;
                display(2, true);
            }
            else
            {
                //MessageBox.Show("已经到最前");
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
           
           // MessageBox.Show("sdfds");
            cmq = lmq[e.RowIndex];
            System.IO.MemoryStream mstream = new System.IO.MemoryStream(cmq.question, false);
            this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
            string key = "";
            if (cmq.answ == 1) key = "A";
            if (cmq.answ == 2) key = "B";
            if (cmq.answ == 3) key = "C";
            if (cmq.answ == 4) key = "D";
            comboBox5.Text = key;
            */

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //
            // MessageBox.Show("sdfds");
            ctf = lTF[e.RowIndex];
            System.IO.MemoryStream mstream = new System.IO.MemoryStream(ctf.question, false);
            this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
            System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(ctf.answ , false);
            this.richTextBox3.LoadFile(mstream2, RichTextBoxStreamType.RichText);
            loadcom2();
            comboBox8.Text = ctf.con.ToString();
            comboBox7.Text = ctf.objective.ToString();
            comboBox6.Text = ctf.diff.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (ctf != null)
            {
                ctf.diff = int.Parse(comboBox6.Text);
                ctf.con = int.Parse(comboBox8.Text);
                ctf.objective = int.Parse(comboBox7.Text);
                System.IO.MemoryStream mstream2 = new System.IO.MemoryStream();
                richTextBox3.SaveFile(mstream2, RichTextBoxStreamType.RichText);
                //将流转换成数组
                //  byte[] bWrite = mstream.ToArray();
                ctf.answ = mstream2.ToArray();

                System.IO.MemoryStream mstream = new System.IO.MemoryStream();
                richTextBox2 .SaveFile(mstream, RichTextBoxStreamType.RichText);
                //将流转换成数组
                //  byte[] bWrite = mstream.ToArray();
                ctf.question = mstream.ToArray();
                pp.context.UpdateObject(ctf);
                pp.context.SaveChanges();
              
                int irow = dataGridView1.CurrentRow.Index;               
                dataGridView1.Rows.Clear();
                display(2, false);
                dataGridView1.CurrentCell = dataGridView1.Rows[irow].Cells[0]; 


            }


        }

        ///endloadw
        private void loadcom2()
        {
            int numobjective = 0;
            int con = 0;
            int diff = 0;

            foreach (V_tea_course cc in lvtc)
                if (comboBox1.Text == cc.CourseName)
                {
                    numobjective = (int)cc.numobjective;
                    con = (int)cc.numcontent;
                    diff = (int)cc.diff;
                    // cid = (int)cc.couseid;
                }
            comboBox7.Items.Clear(); comboBox8.Items.Clear(); comboBox6.Items.Clear();
            for (int i = 0; i < numobjective; i++) comboBox7.Items.Add(i + 1);
            for (int i = 0; i < con; i++) comboBox8.Items.Add(i + 1);
            for (int i = 0; i < diff; i++) comboBox6.Items.Add(i + 1);

        }




        //////////////////////////////////////endclass
    }
}
