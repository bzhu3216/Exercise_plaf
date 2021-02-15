using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise_student.ServiceExer;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using System.IO;

namespace Exercise_student
{
    public partial class shiyan : Form
    {
        paramst pp;
        classinfo clin;
        StudInfo stin;
        List<View_class_exp> lvce;
        int selexp = -1;
        public shiyan(paramst p, classinfo clin1, StudInfo stin1)
        {
            InitializeComponent();
            pp = p;
            clin = clin1;
            stin = stin1;
        }

        private void shiyan_Load(object sender, EventArgs e)
        {

            var questionQuery = from o in pp.context.View_class_exp
                                where (o.classid==clin.classid)
                                orderby o.con
                                select o;
            if (questionQuery.Count() > 0) lvce = questionQuery.ToList<View_class_exp>();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = lvce;




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

        private void button1_Click(object sender, EventArgs e)
        {

            if (selexp >= 0) {
                View_class_exp vp = lvce[selexp];
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                    string attaDirectory = saveFileDialog1.FileName;
                    var questionQuery2 = from o in pp.context.exp_q
                                         where (o.idexp == vp.expid)

                                         select o;

                    if (questionQuery2.Count<exp_q>() > 0)
                    {
                        exp_q qew = questionQuery2.First<exp_q>();
                   






                     }




                }

            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.RowIndex.ToString());
            selexp = e.RowIndex;
            getstate();
        }

        ////////////////////////////////////

        private void getstate()
        {
            if (selexp >= 0)
            {
                View_class_exp vp = lvce[selexp];                
                DateTime dt = DateTime.Now;
                if (dt > vp.starttime && vp.endtime > dt) {
                    button3.Enabled = true;
                    button4.Enabled = (bool)vp.attach;
                }


                var questionQuery = from o in pp.context.studreport
                                    where (o.classid == clin.classid && o.stid== stin.studentid && o.expid==vp.expid )
                                   
                                    select o;

                if (questionQuery.Count<studreport>() > 0) button5.Enabled = true;
                else button5.Enabled = false;
                var questionQuery2= from o in pp.context.exp_q
                                    where (o.idexp== vp.expid)

                                    select o;

                if (questionQuery2.Count<exp_q>() > 0)
                {
                    exp_q qew = questionQuery2.First<exp_q>();
                   if(qew.attachment!=null) button2.Enabled = true;
                   else button2.Enabled = false;

                }


            }
            else
                MessageBox.Show("请选择表格中的行");



        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        ///////////////////////////////////////


    }
}
