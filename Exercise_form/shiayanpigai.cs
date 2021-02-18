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
using Spire.Xls;

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
           // List<View_class_student> lstall = null;

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

        private void button4_Click(object sender, EventArgs e)
        {
            /*
             * 
             var q2 = from o in pp.context.Course
                                     where o.id == lclinfo[sel1].courseid
                                     select o;
                            //
                            int iobj = 0;
                            if (q2.Count<Course>() > 0)
                            {
                                    //

                                    saveFileDialog2.DefaultExt = ".xlsx";
                                    saveFileDialog2.Filter = "EXCEL file|*.xlsx";

                                    if (saveFileDialog2.ShowDialog() == DialogResult.OK)
                                    {
                                        //

                                        iobj = q2.First<Course>().numobjective;
                                        EXtools.toScore(lclinfo[sel1], ler[sel2], saveFileDialog2.FileName , pp, iobj);
                                    }
                            }
                            else
                            {
                                MessageBox.Show("未关联任何习题或有问题联系管理员!");
                            }
             * 
             */
            exportsum(lcl[listBox1.SelectedIndex], lvce[sel1], lvtc[comboBox1.SelectedIndex], 3);





        }
        //////////////////////////
        private void exportsum(classinfo cl1, View_class_exp vst1, V_tea_course vcourse, int poscol)
        {
            String dirsave = null;
            List<View_class_student> lstwei = new List<View_class_student>();
            // List<View_class_student> lstall = null;
            saveFileDialog2.DefaultExt = ".xlsx";
            saveFileDialog2.Filter = "EXCEL file|*.xlsx";
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = saveFileDialog2.FileName.ToString();
                // dirsave = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                dirsave = localFilePath;
            }

            //////////////////////excel

            Spire.Xls.Workbook wb = new Spire.Xls.Workbook();
            //清除默认的工作表
            wb.Worksheets.Clear();
            //添加一个工作表并指定表名
            Worksheet sheet = wb.Worksheets.Add("score");
             sheet.Range[2, 2].Text = "序号";
            sheet.Range[3, 2].Text = "分值";
            sheet.Range[4, 2].Text = "指标";
            sheet.Range[5, 1].Text = "学号";
            sheet.Range[5, 2].Text = "姓名";

            int intobjective =vcourse.numobjective;
            String expobjective = vst1.objective;
            String[] strexpobj = expobjective.Split('|'); 

            for (int i = 1; i <= intobjective+1; i++)
            {  if (i<intobjective+1)
                sheet.Range[4, poscol + i - 1].Text = i.ToString();
            else
                    sheet.Range[4, poscol + i - 1].Text ="已交";

            }
            int irow = 6;
            int icol = poscol;
            var questionQuery1 = from o in pp.context.View_class_student
                                 where o.classid == cl1.classid
                                 orderby o.classno
                                 select o;
            List<View_class_student> lstall=null;

               if (questionQuery1.Count<View_class_student>() > 0)   lstall = questionQuery1.ToList<View_class_student>();
               ///
               /// 
              if( lstall !=null)
            { 
               foreach (View_class_student itvst in lstall)
                  {
                    sheet.Range[irow , 1].Text = itvst.studentid;
                    sheet.Range[irow, 2].Text = itvst.name;
                  

                    var questionQuery2 = from o in pp.context.studreport
                                         where o.classid == cl1.classid && o.expid == vst1.expid && o.stid ==itvst.studentid 
                                         select o;

                    studreport isturep = null;
                    if (questionQuery2.Count<studreport>() > 0)
                    {
                        isturep = questionQuery2.First<studreport>();
                        String strmarks = isturep.score;
                        String[] strmark = strmarks.Split('|');
                        for (int i = 1; i <= intobjective+1; i++)
                        {
                            if (i < intobjective + 1)
                            { sheet.Range[irow, poscol + i - 1].Value2 = 0;
                                for (int j = 1; j < strexpobj.Length;j++)
                                {
                                    if (i == int.Parse(strexpobj[j])) sheet.Range[irow, poscol + i - 1].Value2 = int.Parse(strmark[j]);
                                }



                            }
                           // else
                               // sheet.Range[irow, poscol + i - 1].Text = "NO";

                        }


                    }
                    else
                    {
                        for (int i = 1; i <= intobjective + 1; i++)
                        {
                            if (i < intobjective + 1)
                                sheet.Range[irow, poscol + i - 1].Value2  =0;
                            else
                                sheet.Range[irow, poscol + i - 1].Text = "NO";

                        }


                    }




                    irow = irow + 1;

                }//foreach (View_class_stu
            }  /// end if( lstall !=null)
               ///






            sheet.AllocatedRange.AutoFitColumns();
            wb.SaveToFile(dirsave, ExcelVersion.Version2013);
            MessageBox.Show("excel生成好了");




        }
        /////////////////







    }//endclass
}
