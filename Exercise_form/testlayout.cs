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
    public partial class testlayout : Form
    {
        param pp;
        List<V_tea_course> lcs = null;
        List<exerL> tlvedp = null;
        public testlayout(param p)
        {
            InitializeComponent();
            pp = p;

            /*var questionQuery = from o in context.Course
                                select o;
            lcs = questionQuery.ToList<Course>();
            
            */
            lcs = pp.ltea_c;
            comboBox7.DataSource = lcs;
            comboBox7.ValueMember = "CourseName";


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer2_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void testlayout_Load(object sender, EventArgs e)
        {






        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex >= 0)
            {
                pp.updataccid = lcs[comboBox7.SelectedIndex].couseid;
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                int numobjective = lcs[comboBox7.SelectedIndex].numobjective;
                int numcon = lcs[comboBox7.SelectedIndex].numcontent;

                // dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                for (int i = 0; i < numobjective; i++)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in this.dataGridView2.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[0].Value = i + 1;
                    this.dataGridView2.Rows.Add(dgvr);
                }
                for (int i = 0; i < numcon; i++)
                {
                    DataGridViewRow dgvr = new DataGridViewRow();
                    foreach (DataGridViewColumn c in this.dataGridView3.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[0].Value = i + 1;
                    this.dataGridView3.Rows.Add(dgvr);
                }
                updatalist();
            }
            else
            {

                MessageBox.Show("怎么没选择课程？");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (comboBox7.SelectedIndex >= 0 && textBox1.Text != "")
            {
                var q1 = from o in pp.context.exerL
                         where (o.name == textBox1.Text) && o.teacherid == pp.teacher.teacherid && o.pub == 3 && o.courseid == lcs[comboBox7.SelectedIndex].couseid
                         select 0;
                if (q1.Count() <= 0)
                {

                    exerL mcq = new exerL();
                    // mcq.answ = comboBox4.SelectedIndex + 1;

                    mcq.courseid = lcs[comboBox7.SelectedIndex].couseid;
                    mcq.teacherid = pp.teacher.teacherid;
                    mcq.name = textBox1.Text;
                    mcq.pub = 3;
                    ////////////write richtext
                    pp.context.AddToexerL(mcq);
                    pp.context.SaveChanges();
                    textBox1.Text = "";
                    updatalist();

                }
                else
                    MessageBox.Show("同名试卷已经存在");
            }
            else

                MessageBox.Show("怎么没选择课程？或没写名称");

        }

        private void updatalist()
        {

            tlvedp = null;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = null;
            var q1 = from o in pp.context.exerL
                     where o.courseid == lcs[comboBox7.SelectedIndex].couseid && o.pub == 3 && o.teacherid == pp.teacher.teacherid
                     select o;
            if (q1.Count<exerL>() > 0) tlvedp = q1.ToList<exerL>();
            dataGridView1.DataSource = tlvedp;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int irow = -1;
            if (dataGridView1.CurrentRow != null)
                irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0 && comboBox7.SelectedIndex >= 0)
            {
                //pp.vdlword=
                EditTestPaper mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    pp.elword = tlvedp[irow];
                    pp.vdlword = lcs[comboBox7.SelectedIndex];
                    mq = new EditTestPaper(pp, tlvedp[irow], lcs[comboBox7.SelectedIndex]);
                    mq.ShowDialog();
                    // mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                MessageBox.Show("请选择练习！or 课程");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int irow = -1;
            if (dataGridView1.CurrentRow != null)
                irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0)
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    exerL yex = tlvedp[irow];
                    pp.context.DeleteObject(yex);
                    pp.context.SaveChanges();
                    //deteldell
                    deldetail(yex);
                    updatalist();



                }
                else
                {

                    MessageBox.Show("请选择删除的试卷");
                }
            }



        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null) {
                int irow = dataGridView1.CurrentRow.Index;
                if (irow >= 0) {

                    exerL yex = tlvedp[irow];
                    if (dataGridView1.CurrentRow.Cells[2].Value.ToString() != "")
                    { if (!EXtools.isexitel(dataGridView1.CurrentRow.Cells[2].Value.ToString(), yex.courseid, pp))
                        {
                            yex.name = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                            pp.context.UpdateObject(yex);
                            pp.context.SaveChanges();
                            updatalist();
                        }
                        else
                        {
                            MessageBox.Show("名称已经存在");
                        }

                    }
                    else
                    {
                        MessageBox.Show("请datagrid中输入新名称");
                    }


                }
                else
                {

                    MessageBox.Show("请选择书卷");
                }

            }
        }
        ////
        private void deldetail(exerL s)
        {
            var q1 = from o in pp.context.exerDetail
                     where o.lid == s.id
                     select o;
            if (q1.Count<exerDetail>() > 0)
            {
                List<exerDetail> tlerd = q1.ToList<exerDetail>();
                foreach (exerDetail iexd in tlerd)
                {
                    pp.context.DeleteObject(iexd);

                }

                pp.context.SaveChanges();

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Exercise_Summary mq = null;
            int irow = -1;
            if (dataGridView1.CurrentRow != null)
                irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0 && comboBox7.SelectedIndex >= 0)
            {

                if (mq == null || mq.IsDisposed)
                {
                    //   pp.vdlword = null;
                    //  pp.elword = null;

                    mq = new Exercise_Summary(pp);
                    mq.textBox1.Text = EXtools.toSummary(tlvedp[irow], lcs[comboBox7.SelectedIndex], pp);
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

        private void button7_Click(object sender, EventArgs e)
        {
            // pp.updataccid = lcs[comboBox7.SelectedIndex].couseid;
            List<exerL> comel = new List<ServiceReference1.exerL>();

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (((bool?)(dataGridView1.Rows[i].Cells[0].Value)) == true)
                {
                    comel.Add(tlvedp[i]);
                }

            }
            if (comel.Count() > 8 || comel.Count() < 2)
                MessageBox.Show("只支持选择2-8份");
            else
            {
                compare mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    //   pp.vdlword = null;
                    //  pp.elword = null;

                    mq = new compare(comel, lcs[comboBox7.SelectedIndex], pp);

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

        private void button1_Click(object sender, EventArgs e)
        {   if (!checke()) { MessageBox.Show("有参数为空");return; }
            autogen(true, int.Parse(comboBox1.Text ));
        }



        public void autogen(bool flag,int nums)
            {

            if (flag)
            {
                List<int> objectives = new List<int>();
                int p = -1;int sump = 0;
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                {   if (dataGridView2.Rows[i].Cells[1].Value != null)
                        p = int.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
                    else
                        p = 0;
                    objectives.Add(p);
                    sump = sump + p;
                    
                }
                if (sump != 100) { MessageBox.Show("Sum must be 100");return; }
                List<exerL> ell = new List<ServiceReference1.exerL>();
                for (int i = 0; i < nums; i++)
                { exerL el= addexerL(textBox1.Text + i);
                    if (el != null) ell.Add(el);
                    else
                        return;

                }
                updatalist();
                textBox1.Text = "";





            }
            else
            { }






            
            
            }

        private bool checke()
        {
          bool  result = true;
            if (comboBox1.SelectedIndex < 0 || comboBox1.Text == "") result = false;
            if (comboBox2.SelectedIndex < 0 || comboBox2.Text == "") result = false;
            if (comboBox3.SelectedIndex < 0 || comboBox3.Text == "") result = false;
            if (comboBox4.SelectedIndex < 0 || comboBox4.Text == "") result = false;
            if (comboBox5.SelectedIndex < 0 || comboBox5.Text == "") result = false;
            if (comboBox6.SelectedIndex < 0 || comboBox6.Text == "") result = false;
            if (comboBox7.SelectedIndex < 0 || comboBox7.Text == "") result = false;
            if(textBox1.Text=="") result = false;
            if (textBox2.Text == "") result = false;
            if (textBox3.Text == "") result = false;
            if (textBox4.Text == "") result = false;
            if (textBox5.Text == "") result = false;
            if (textBox6.Text == "") result = false;
            return result;
        }

        //
        private exerL addexerL(string name)
        {
            exerL rel = null;
            var q1 = from o in pp.context.exerL
                     where (o.name == textBox1.Text) && o.teacherid == pp.teacher.teacherid && o.pub == 3 && o.courseid == lcs[comboBox7.SelectedIndex].couseid
                     select 0;
            if (q1.Count() <= 0)
            {

                exerL mcq = new exerL();
                // mcq.answ = comboBox4.SelectedIndex + 1;
                mcq.courseid = lcs[comboBox7.SelectedIndex].couseid;
                mcq.teacherid = pp.teacher.teacherid;
                mcq.name = name;
                mcq.pub = 3;
                ////////////write richtext
                pp.context.AddToexerL(mcq);
                pp.context.SaveChanges();
                rel = mcq;
            }
            else
                MessageBox.Show("同名试卷已经存在");
            return rel;
        }
        /////

        private void addexerdetail(List<exerDetail> ell1,List<int> obctivep,List<int> typenum,List<int>  typescore )
        {
           







        }




        //////endcalsss
    }
    }
