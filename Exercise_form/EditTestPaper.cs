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
       // List<mchoiceQues> lmq = null;
        List<exerDetail> ell = null;
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
                    List<mchoiceQues> lmq = null;
                    var questionQuery3 = (from o in pp.context.mchoiceQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (o.courseid == vcc.couseid)
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
                            dgvr.Cells[1].Value = mcq.objective ;
                            dgvr.Cells[2].Value = richTextBox1.Rtf;
                            dgvr.Cells[0].Value = mcq.id ;
                            dgvr.Cells[0].ToolTipText = mcq.objective + @"/" + mcq.con;
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
                        if (pageNum < 0) pageNum = 0;
                        else
                            display(comboBox1.SelectedIndex, false);

                    }
                }
                ///////////////////////////////end0
                if (a == 1)
                {
                    // comboBox1.Text = comboBox1.Items[a].ToString();
                    comboBox5.Text = "1";
                    List<TFQues> lmq = null;
                    var questionQuery3 = (from o in pp.context.TFQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (o.courseid == vcc.couseid)
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    if (questionQuery3.Count() > 0)
                    {
                        lmq = questionQuery3.ToList<TFQues>();
                        dataGridView1.AutoGenerateColumns = false;
                        foreach (TFQues mcq in lmq)
                        {
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            DataGridViewRow dgvr = new DataGridViewRow();
                            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {
                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[1].Value = mcq.objective;
                            dgvr.Cells[2].Value = richTextBox1.Rtf;
                            dgvr.Cells[0].Value = mcq.id;
                            // dgvr.Cells[0].Value = mcq.id + "|" + mcq.objective;
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
                        if (pageNum < 0) pageNum = 0;
                        else
                            display(comboBox1.SelectedIndex, false);

                    }
                }
                ////            ///////////////////////////////end1
                if (a == 2&& el.pub==3)
                {
                    // comboBox1.Text = comboBox1.Items[a].ToString();
                    comboBox5.Text = "1";
                    List<eQues> lmq = null;
                    var questionQuery3 = (from o in pp.context.eQues 
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (o.courseid == vcc.couseid)
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    if (questionQuery3.Count() > 0)
                    {
                        lmq = questionQuery3.ToList<eQues>();
                        dataGridView1.AutoGenerateColumns = false;
                        foreach (eQues mcq in lmq)
                        {
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            DataGridViewRow dgvr = new DataGridViewRow();
                            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {
                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[1].Value = mcq.objective;
                            dgvr.Cells[2].Value = richTextBox1.Rtf;
                            dgvr.Cells[0].Value = mcq.id;
                            //dgvr.Cells[0].Value = mcq.id + "|" + mcq.objective;
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
                        if (pageNum < 0) pageNum = 0;
                        else
                            display(comboBox1.SelectedIndex, false);

                    }
                }
                ////            ///////////////////////////////end2
                if (a == 3)
                {
                    // comboBox1.Text = comboBox1.Items[a].ToString();
                    comboBox5.Text = "5";
                    List<SQues> lmq = null;
                    var questionQuery3 = (from o in pp.context.SQues 
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (o.courseid == vcc.couseid)
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    if (questionQuery3.Count() > 0)
                    {
                        lmq = questionQuery3.ToList<SQues>();
                        dataGridView1.AutoGenerateColumns = false;
                        foreach (SQues mcq in lmq)
                        {
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            DataGridViewRow dgvr = new DataGridViewRow();
                            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {
                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[1].Value = mcq.objective;
                            dgvr.Cells[2].Value = richTextBox1.Rtf;
                            dgvr.Cells[0].Value = mcq.id;
                           // dgvr.Cells[0].Value = mcq.id + "|" + mcq.objective;
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
                        if (pageNum < 0) pageNum = 0;
                        else
                            display(comboBox1.SelectedIndex, false);

                    }
                }
                ////            ///////////////////////////////end3
                if (a == 4)
                {
                    // comboBox1.Text = comboBox1.Items[a].ToString();
                    comboBox5.Text = "10";
                    List<AQues> lmq = null;
                    var questionQuery3 = (from o in pp.context.AQues
                                          where (b1 || o.objective == c1)
                                       && (b2 || o.con == c2)
                                       && (b3 || o.diff == c3)
                                       && (o.courseid == vcc.couseid)
                                          select o).Skip(pageNum * pagesize).Take(pagesize);
                    if (questionQuery3.Count() > 0)
                    {
                        lmq = questionQuery3.ToList<AQues>();
                        dataGridView1.AutoGenerateColumns = false;
                        foreach (AQues mcq in lmq)
                        {
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            DataGridViewRow dgvr = new DataGridViewRow();
                            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {
                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[1].Value = mcq.objective;
                            dgvr.Cells[2].Value = richTextBox1.Rtf;
                            dgvr.Cells[0].Value = mcq.id;
                           // dgvr.Cells[0].Value = mcq.id + "|" + mcq.objective;
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
                        if (pageNum < 0) pageNum = 0;
                        else
                            display(comboBox1.SelectedIndex, false);

                    }
                }
                ////            ///////////////////////////////end4




            }
            markselected();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pageNum = 0;
            display(comboBox1.SelectedIndex, true);
            reload2(comboBox1.SelectedIndex);
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
                edl.lid = el.id ;
                edl.qid = selid;
                edl.lorder = dataGridView2.RowCount + 1;
                edl.score = int.Parse(comboBox5.Text);
                edl.typeq = comboBox1.SelectedIndex;
                saveExerD(edl);
                reload2(comboBox1.SelectedIndex);
               

            }

        }
        ///////////////////////////////

        private void reload2(int a)
        {
            if (dataGridView2.DataSource != null)
            {
                DataTable dt = (DataTable)dataGridView2.DataSource;
                dt.Rows.Clear();
                dataGridView2.DataSource = dt;
            }
            else

            {
                dataGridView2.Rows.Clear();

            }
            if (a == 0)
            {
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 0
                                     orderby o.lorder 
                                     select o;
                ell  = questionQuery1.ToList<exerDetail>();
                //add lorder
                int fflag = 0;
                foreach (exerDetail tel in ell)
                {
                    if (tel.lorder == null) fflag = 1;

                }

                if (fflag == 1)
                {
                    int ilorder = 1;
                    foreach (exerDetail tel in ell)
                    {
                        tel.lorder = ilorder; ;
                        pp.context.UpdateObject(tel);

                        ilorder++;

                    }

                    pp.context.SaveChanges();
                }

                //end lorder             
                foreach (exerDetail tel in ell)
                {
                    var questionQuery2 = from o in pp.context.mchoiceQues
                                         where o.id == tel.qid
                                         select o;
                    if (questionQuery2.Count() > 0)
                    {
                        mchoiceQues mcq = questionQuery2.First<mchoiceQues>();
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                       // dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        foreach (DataGridViewColumn c in this.dataGridView2.Columns)
                        {

                            dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        dgvr.Cells[2].Value = richTextBox1.Rtf;
                        dgvr.Cells[1].Value = mcq.objective ;
                        dgvr.Cells[0].Value = mcq.id;                        
                        int hh = (int)(richTextBox1.Rtf.Length / 8);
                        if (hh > 300) hh = 300;
                        dgvr.Height = hh;
                        this.dataGridView2.Rows.Add(dgvr);
                    }
                    else
                    {
                        MessageBox.Show("有问题联系管理员"+ tel.qid.ToString());
                     }

                }
            }//end0
            if (a == 1)
            {
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 1
                                     orderby o.lorder
                                     select o;
                ell = questionQuery1.ToList<exerDetail>();
                //add lorder
                int fflag = 0;
                foreach (exerDetail tel in ell)
                {
                    if (tel.lorder == null) fflag = 1;

                }
                
                if (fflag == 1)
                {
                    int ilorder = 1;
                    foreach (exerDetail tel in ell)
                    {
                        tel.lorder = ilorder; ;
                        pp.context.UpdateObject(tel);

                        ilorder++;

                    }

                    pp.context.SaveChanges();
                }

                //end lorder


                foreach (exerDetail tel in ell)
                {
                    var questionQuery2 = from o in pp.context.TFQues 
                                         where o.id == tel.qid
                                         select o;
                    if (questionQuery2.Count() > 0)
                    {
                        TFQues mcq = questionQuery2.First<TFQues>();
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        // dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        foreach (DataGridViewColumn c in this.dataGridView2.Columns)
                        {

                            dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        dgvr.Cells[2].Value = richTextBox1.Rtf;
                        dgvr.Cells[1].Value = mcq.objective;
                        dgvr.Cells[0].Value = mcq.id;
                        int hh = (int)(richTextBox1.Rtf.Length / 8);
                        if (hh > 300) hh = 300;
                        dgvr.Height = hh;
                        this.dataGridView2.Rows.Add(dgvr);
                    }
                    else
                    {
                        MessageBox.Show("有问题联系管理员" + tel.qid.ToString());
                    }

                }
            }
            //end1
            if(a == 2&&el.pub==3)
            {
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 2
                                     orderby o.lorder
                                     select o;
                ell = questionQuery1.ToList<exerDetail>();
                //add lorder
                int fflag = 0;
                foreach (exerDetail tel in ell)
                {
                    if (tel.lorder == null) fflag = 1;

                }

                if (fflag == 1)
                {
                    int ilorder = 1;
                    foreach (exerDetail tel in ell)
                    {
                        tel.lorder = ilorder; ;
                        pp.context.UpdateObject(tel);

                        ilorder++;

                    }

                    pp.context.SaveChanges();
                }

                //end lorder   
                foreach (exerDetail tel in ell)
                {
                    var questionQuery2 = from o in pp.context.eQues 
                                         where o.id == tel.qid
                                         select o;
                    if (questionQuery2.Count() > 0)
                    {
                        eQues mcq = questionQuery2.First<eQues>();
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        // dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        foreach (DataGridViewColumn c in this.dataGridView2.Columns)
                        {

                            dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        dgvr.Cells[2].Value = richTextBox1.Rtf;
                        dgvr.Cells[1].Value = mcq.objective;
                        dgvr.Cells[0].Value = mcq.id;
                        int hh = (int)(richTextBox1.Rtf.Length / 8);
                        if (hh > 300) hh = 300;
                        dgvr.Height = hh;
                        this.dataGridView2.Rows.Add(dgvr);
                    }
                    else
                    {
                        pp.context.DeleteObject(tel);
                        pp.context.SaveChanges();

                        MessageBox.Show("有问题联系管理员" + tel.qid.ToString());
                    }

                }
            }
            //end2
            if (a == 3)
            {
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 3
                                     orderby o.lorder
                                     select o;
                ell = questionQuery1.ToList<exerDetail>();
                //add lorder
                int fflag = 0;
                foreach (exerDetail tel in ell)
                {
                    if (tel.lorder == null) fflag = 1;

                }

                if (fflag == 1)
                {
                    int ilorder = 1;
                    foreach (exerDetail tel in ell)
                    {
                        tel.lorder = ilorder; ;
                        pp.context.UpdateObject(tel);

                        ilorder++;

                    }

                    pp.context.SaveChanges();
                }

                //end lorder   
                foreach (exerDetail tel in ell)
                {
                    var questionQuery2 = from o in pp.context.SQues 
                                         where o.id == tel.qid
                                         select o;
                    if (questionQuery2.Count() > 0)
                    {
                        SQues mcq = questionQuery2.First<SQues>();
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        // dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        foreach (DataGridViewColumn c in this.dataGridView2.Columns)
                        {

                            dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        dgvr.Cells[2].Value = richTextBox1.Rtf;
                        dgvr.Cells[1].Value = mcq.objective;
                        dgvr.Cells[0].Value = mcq.id;
                        int hh = (int)(richTextBox1.Rtf.Length / 8);
                        if (hh > 300) hh = 300;
                        dgvr.Height = hh;
                        this.dataGridView2.Rows.Add(dgvr);
                    }
                    else
                    {
                        MessageBox.Show("有问题联系管理员" + tel.qid.ToString());
                    }

                }
            }
            //end3

            if (a == 4)
            {
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 4
                                     orderby o.lorder
                                     select o;
                ell = questionQuery1.ToList<exerDetail>();
                //add lorder
                int fflag = 0;
                foreach (exerDetail tel in ell)
                {
                    if (tel.lorder == null) fflag = 1;

                }

                if (fflag == 1)
                {
                    int ilorder = 1;
                    foreach (exerDetail tel in ell)
                    {
                        tel.lorder = ilorder; ;
                        pp.context.UpdateObject(tel);

                        ilorder++;

                    }

                    pp.context.SaveChanges();
                }

                //end lorder   
                foreach (exerDetail tel in ell)
                {
                    var questionQuery2 = from o in pp.context.AQues 
                                         where o.id == tel.qid
                                         select o;
                    if (questionQuery2.Count() > 0)
                    {
                        AQues mcq = questionQuery2.First<AQues>();
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                        this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                        //   rrtf.Add(richTextBox1.Rtf);
                        DataGridViewRow dgvr = new DataGridViewRow();
                        // dataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                        foreach (DataGridViewColumn c in this.dataGridView2.Columns)
                        {

                            dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                        }
                        dgvr.Cells[2].Value = richTextBox1.Rtf;
                        dgvr.Cells[1].Value = mcq.objective;
                        dgvr.Cells[0].Value = mcq.id;
                        int hh = (int)(richTextBox1.Rtf.Length / 8);
                        if (hh > 300) hh = 300;
                        dgvr.Height = hh;
                        this.dataGridView2.Rows.Add(dgvr);
                    }
                    else
                    {
                        MessageBox.Show("有问题联系管理员" + tel.qid.ToString());
                    }

                }
            }
            //end4


            markselected();

        }//end sub

        ////////////////////////////

        private void markselected()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                String tsid = dataGridView1.Rows[i].Cells[0].Value.ToString();
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.White;
                for (int j = 0; j < dataGridView2.Rows.Count; j++)
                {
                    
                    String tsid2=dataGridView2.Rows[j].Cells[0].Value.ToString();
                    if (tsid == tsid2) { dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Red; break; }
                }

                //  if (dataGridView1.Rows[i].Selected == true)
                //  {
                //    selid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                //  }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int selid = -1;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Selected == true)
                {
                    selid = int.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
            }
            if (selid != -1)
            {
                exerDetail eed = ell.Find(x => x.qid == selid);
                pp.context.DeleteObject(eed);
                pp.context.SaveChanges();
                reload2(comboBox1.SelectedIndex);
                ////                Update lorder
                int i = 0;
                foreach (exerDetail e1 in ell)
                {
                    i = i + 1;
                    e1.lorder = i;
                    pp.context.UpdateObject(e1); 

                }
                pp.context.SaveChanges();


                ////////



            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                display(comboBox1.SelectedIndex, false);
                reload2(comboBox1.SelectedIndex);
            }
            else
                MessageBox.Show("请先选择题型");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex >= 0)
            {
                display(comboBox1.SelectedIndex, false);
                reload2(comboBox1.SelectedIndex);
            }
            else
                MessageBox.Show("请先选择题型");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex >= 0)
            {
                display(comboBox1.SelectedIndex, false);
                reload2(comboBox1.SelectedIndex);
            }
            else
                MessageBox.Show("请先选择题型");
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            int selid = -1;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Selected == true)
                {
                    selid = int.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
            }
            if (selid != -1)
            {

                exerDetail eed = ell.Find(x => x.qid == selid);
                comboBox5.Text = eed.score.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selid = -1;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Selected == true)
                {
                    selid = int.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
            }
            if (selid != -1)
            {
                try
                {
                    exerDetail eed = ell.Find(x => x.qid == selid);
                    eed.score = int.Parse(comboBox5.Text);
                    pp.context.UpdateObject(eed);
                    pp.context.SaveChanges();
                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.ToString()); 
                }





            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Exercise_Summary mq = null;
            


            if (mq == null || mq.IsDisposed)
            {
                //   pp.vdlword = null;
                //  pp.elword = null;

                mq = new Exercise_Summary(pp);
                mq.textBox1.Text = EXtools.toSummary(el, vcc, pp);
                mq.ShowDialog();
                // mq.Show();
            }
            else
            {
                mq.Activate();
                mq.WindowState = FormWindowState.Normal;
            }

        }


        //////////////////////////////////////////
        private void changeorder(int id1, int id2)
        {



        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int selid = -1;
            selid = dataGridView2.CurrentRow.Index;
            if (selid > 0)
            {
                /* foreach (exerDetail e1 in ell)
                 {
                     i = i + 1;
                     e1.lorder = i;
                    

                 }*/
             if (ell[selid].lorder != null) { 
                int temp = (int) (ell[selid].lorder);
                ell[selid].lorder = ell[selid - 1].lorder;
                ell[selid - 1].lorder = temp;
                pp.context.UpdateObject(ell[selid]);
                pp.context.UpdateObject(ell[selid-1]);
                pp.context.SaveChanges();
                reload2(comboBox1.SelectedIndex);
                }

            }












        }

        private void button8_Click(object sender, EventArgs e)
        {


            int selid = -1;
            selid = dataGridView2.CurrentRow.Index;
            if (selid < dataGridView2.RowCount - 1)
            {
                /* foreach (exerDetail e1 in ell)
                 {
                     i = i + 1;
                     e1.lorder = i;
                    

                 }*/
                if (ell[selid].lorder != null)
                {
                    int temp = (int)(ell[selid].lorder);
                    ell[selid].lorder = ell[selid + 1].lorder;
                    ell[selid + 1].lorder = temp;
                    pp.context.UpdateObject(ell[selid]);
                    pp.context.UpdateObject(ell[selid + 1]);
                    pp.context.SaveChanges();
                  
                    reload2(comboBox1.SelectedIndex);
                }

            }
        }

        //////////////////////////////////////////

    }
}
