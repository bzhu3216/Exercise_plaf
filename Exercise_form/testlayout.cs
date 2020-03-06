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
                    dgvr.Cells[0].Value = i+1;
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
                         where (o.name == textBox1.Text) && o.teacherid == pp.teacher.teacherid && o.pub == 3 &&o.courseid == lcs[comboBox7.SelectedIndex].couseid
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

            int irow = dataGridView1.CurrentRow.Index;
            if (irow>=0 && comboBox7.SelectedIndex>=0)
            {
                EditTestPaper mq = null;
                if (mq == null || mq.IsDisposed)
                {
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
            int irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0)
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    exerL yex = tlvedp[irow];
                    pp.context.DeleteObject(yex);
                    pp.context.SaveChanges();
                    //deteldell
                   // deldetail(yex);
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

            int irow = dataGridView1.CurrentRow.Index;
            if (irow >= 0) { 

                exerL yex = tlvedp[irow];
              if (dataGridView1.CurrentRow.Cells[2].Value .ToString() != "")
                {   if (!EXtools.isexitel(dataGridView1.CurrentRow.Cells[2].Value.ToString(), yex.courseid , pp))
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


        //////endcalsss
    }
}
