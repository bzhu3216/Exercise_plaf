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
using System.IO;

namespace Exercise_form
{
    public partial class shiayanpigai : Form
    {
        param pp;     
        List<V_tea_course> lvtc = null;
        List<classinfo> lcl = null;
       // List<classExp> lce = null;
        List<View_class_exp> lvce=null;
        int sel1 = -1;
        public shiayanpigai(param p)
        {
            InitializeComponent();
            pp = p;
            lvtc = p.ltea_c;
        }

        private void shiayanpigai_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = lvtc;
            comboBox1.ValueMember  = "CourseName";
          //  lcl = getclasslist();
            //listBox1.DataSource = lcl;
          //  listBox1.DisplayMember = "classinfo";



        }
        //getclasses

        private List<classinfo> getclasslist()
        {
            List<classinfo> templcl = null;
           // List<classExp> templce = null;
            /*var questionQuery3 = from o in pp.context.classExp
                                 where (o.cid == lvtc[comboBox1.SelectedIndex].CourseName && lvtc[comboBox1.SelectedIndex].teacherid==pp.teacher.teacherid)
                                 select o;
            if (questionQuery3.Count<classExp>() > 0)
            {
                templce = questionQuery3.ToList<classExp>();
                lce = templce;
            }
            if (templce == null) return null;
            foreach (classExp ice in templce)
            {
                var questionQuery1 = from o in pp.context.classinfo
                                     where (o.classid==ice.cid && o.finish==0)
                                     select o;
                if (questionQuery1.Count<classinfo>() > 0) templcl.Add(questionQuery1.First<classinfo>());
            }*/
            var questionQuery1 = from o in pp.context.classinfo
                                 where (o.courseid == lvtc[comboBox1.SelectedIndex].couseid && o.finish == 0  && o.teacher== pp.teacher.teacherid)
                                 orderby o.addtime
                                 select o;
            if (questionQuery1.Count<classinfo>() > 0) templcl= questionQuery1.ToList<classinfo>();

            return templcl;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvce = null;
            sel1 = -1;
            lcl = getclasslist();            
            listBox1.DisplayMember  = "classinfo1";
            listBox1.DataSource = lcl;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvce = null;
            dataGridView1.DataSource  = null;
            dataGridView1.Rows.Clear();

            if (listBox1.SelectedIndex >= 0) {
                classinfo clin = lcl[listBox1.SelectedIndex];

            var questionQuery = from o in pp.context.View_class_exp
                                where (o.classid == clin.classid)
                                orderby o.con
                                select o;
            if (questionQuery.Count() > 0) lvce = questionQuery.ToList<View_class_exp>();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = lvce;
            }


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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         // MessageBox.Show(sel1.ToString());
            if (sel1 >= 0 && listBox1.SelectedIndex>=0) {
            shiyanpgui mq = null;
            if (mq == null || mq.IsDisposed)
            {
                mq = new shiyanpgui(pp,lcl[listBox1.SelectedIndex], lvce[sel1]);
                // mq.MdiParent = this;
                mq.Show();
            }
            else
            {
                mq.Activate();
                mq.WindowState = FormWindowState.Normal;
            }
            }
            else
            MessageBox.Show(sel1.ToString()+"请选择实验");
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            sel1 = e.RowIndex;
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
           // sel1 = -1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sel1 >= 0 && listBox1.SelectedIndex >= 0)
            {
                shiweijiao mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new shiweijiao(pp, lcl[listBox1.SelectedIndex], lvce[sel1]);
                    // mq.MdiParent = this;
                    mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else
                MessageBox.Show(sel1.ToString() + "请选择实验");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sel1 >= 0 && listBox1.SelectedIndex >= 0)
            {
                shiweipi mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new shiweipi(pp, lcl[listBox1.SelectedIndex], lvce[sel1]);
                    // mq.MdiParent = this;
                    mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else
                MessageBox.Show(sel1.ToString() + "请选择实验");
        }

        private void button2_Click(object sender, EventArgs e)
        {
           

         if (sel1 >= 0 && listBox1.SelectedIndex >= 0)
            {
                
                exportreport(lcl[listBox1.SelectedIndex], lvce[sel1]);
            }
            else
                MessageBox.Show(sel1.ToString() + "请选择实验");
        }


  


        ////////////export all report
        private void exportreport(classinfo cl1, View_class_exp vst1)
        {
            String dirsave = null;
            List<View_class_student> lstwei = new List<View_class_student>();
            List<View_class_student> lstall = null;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = saveFileDialog1.FileName.ToString();
                dirsave = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
            }

         

            List<studreport> lstrep=null;
                var questionQuery2 = from o in pp.context.studreport
                                     where o.classid == cl1.classid && o.expid == vst1.expid
                                     select o;

            if (questionQuery2.Count() > 0)
            {
                lstrep = questionQuery2.ToList<studreport>();

                foreach (studreport qew in lstrep)
                {
                    string attaDirectory = dirsave + @"\"+qew.stid + EXtools.getstuname(pp, qew.stid);
                    String repname = qew.fname;
                    if (repname[repname.Length - 1] == 'x' || repname[repname.Length - 1] == 'X')
                        attaDirectory = attaDirectory + @".docx";
                    else
                        attaDirectory = attaDirectory + @".doc";

                    Byte[] Files = qew.rep;
                    BinaryWriter bw = new BinaryWriter(File.Open(attaDirectory, FileMode.OpenOrCreate));
                    bw.Write(Files);
                    bw.Close();
                    ///导出附件
                    ///
                    if(qew.aname!=null && qew.atta != null)
                    { 
                       string attaDirectory2 = dirsave + @"\" + qew.stid + EXtools.getstuname(pp, qew.stid);
                       attaDirectory2= attaDirectory2+@".rar";
                        Byte[] Files2 = qew.atta;
                        BinaryWriter bw2 = new BinaryWriter(File.Open(attaDirectory2, FileMode.OpenOrCreate));
                        bw2.Write(Files2);
                        bw2.Close();


                    }





                }

            }
            else
                MessageBox.Show("没有报告可以导出");


            }


        /////////////////







    }
}
