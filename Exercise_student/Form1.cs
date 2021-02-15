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

namespace Exercise_student
{
    public partial class Form1 : Form
    {
        public paramst pp;
        public List<class_student> lcsl = null;
        public List<classinfo> lclinfo = null;
        public List<classExer> lce = null;
        public List<exerL> erl = null;
        int sel1 = -1;
        public int sel2 = -1;
        public int selid = -1;
        public int c_l = 0;
        List<Object> ltemp = null;
        public Form1(paramst p)
        {
            InitializeComponent();
            pp = p;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lcsl = getcsl(pp.st.studentid);
            lclinfo = getclassinfo(lcsl);
            comboBox1.DataSource = lclinfo;
            comboBox1.ValueMember = "classinfo1";
          //  comboBox1.Text = "";
           




        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        //////////////////////////////////


        private List<class_student> getcsl(String sid)
        {
            List<class_student> csl = null;
            var q1 = from c in pp.context.class_student
                     where c.studentid==sid
                     select c;
            csl = q1.ToList<class_student>();
            return csl;
        }

        //
        private List<classinfo> getclassinfo(List<class_student> tlcs)
        {
            List<classinfo> tcsl = null;
            List<classinfo> tcsl2 = null;
            try
            {
                var q1 = from o in pp.context.classinfo
                         where o.finish ==0
                         select o;
                tcsl2 = q1.ToList<classinfo>();
                var qtcsl = tcsl2.Where(p => tlcs.Any(c => c.classid == p.classid));
                tcsl = qtcsl.ToList<classinfo>();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }


            return tcsl;
        }
        ///////

        private List<classExer> getclexerl(classinfo tcin)
        {
            List<classExer> tel = null;
            var q1 = from o in pp.context.classExer
                     where o.cid == tcin.classid
            select o;

            tel = q1.ToList();         

            return tel;
        }


        /// //////////
        private List<exerL > getexerl2(classinfo tcin)
        {
            List<exerL> tel =null;
            var  q1 = from o in pp.context.exerL 
                         where o.courseid ==tcin.courseid 
                         select o;
            tel = q1.ToList();
            return tel;
        }






        /// ////////////

        private void button1_Click(object sender, EventArgs e)

        {
            if (c_l == 0) { MessageBox.Show("请点击列表最前面选择一个作业"); return; }
            if (c_l == 1) { MessageBox.Show("The time limit is not up!");return; }
            sel2 = -1;
           if (c_l == 3) { MessageBox.Show("The due time has expired. You can only view your submission."); ; }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    selid = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    sel2 = i;
                }
            }
            if (sel2 >=0)
            {
                fdo mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new fdo(this);
                    mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }
            else

            {
                MessageBox.Show("请点击列表最前面选择一个作业");

            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex >= 0)
            {
                sel1 = comboBox1.SelectedIndex;
                classinfo cin = lclinfo[sel1];
                lce = getclexerl(cin);
                erl = getexerl2(cin);


                //  persons.Join(cities, p => p.CityID, c => c.ID, (p, c) => new { PersonName = p.Name, CityName = c.Name });

              var q1=  lce.Join(erl, p => p.eid, c =>c.id , (p, c) => new { eid = p.eid , ename = c.name ,stime=p.starttime ,etime=p.endtime });

                ltemp =q1.OrderBy(s=>s.stime).ToList<Object>() ;
                dataGridView1.DataSource = ltemp;

              

                

            }





        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            password mq = null;
            if (mq == null || mq.IsDisposed)
            {
                mq = new password(pp);
                mq.ShowDialog(); 
            }
            else
            {
                mq.Activate();
                mq.WindowState = FormWindowState.Normal;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //导出联习

            if (sel2 >= 0)
            {
                // sel1 = comboBox1.SelectedIndex;
                // classinfo cin = lclinfo[sel1];
                //lce = getclexerl(cin);
                // erl = getexerl2(cin);
                //  persons.Join(cities, p => p.CityID, c => c.ID, (p, c) => new { PersonName = p.Name, CityName = c.Name });
                // var q1 = lce.Join(erl, p => p.eid, c => c.id, (p, c) => new { eid = p.eid, ename = c.name, stime = p.starttime, etime = p.endtime });
                //////////
                List<int> numofquestion = new List<int>(5);
                for (int i = 0; i < 5; i++) numofquestion.Add(0);
                int biaoti = 1;
                /////////
                richTextBox2.Text = "";
                int selid3 = int.Parse(dataGridView1.Rows[sel2].Cells[0].Value.ToString());
                exerL  tel1 = null;
                tel1 = erl.Where(o => o.id  == selid3).First();
                //exerL tel1 = erl[sel2];
                List<exerDetail> led = null;
                var q11 = from o in pp.context.exerDetail
                          where o.lid == tel1.id
                          orderby o.typeq, o.id
                          select o;
                if (q11 != null)
                {

                    saveFileDialog1.DefaultExt = ".docx";
                    saveFileDialog1.Filter = "WORD file|*.docx";

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        Document doc = new Document();
                        Section s = doc.AddSection();
                         


                        led = q11.ToList<exerDetail>();
                    foreach (exerDetail ed1 in led)
                    {
                        //get question
                        if (ed1.typeq == 0)
                        {
                                this.richTextBox2.Rtf = null;
                            var q12 = from o in pp.context.mchoiceQues
                                      where o.id == ed1.qid
                                      select o;
                            mchoiceQues mcq = q12.First<mchoiceQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            richTextBox1.Text = "";
                            this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                          //  this.richTextBox2.AppendText(richTextBox1.Rtf);
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == pp.st.studentid  && o.did == ed1.id
                                      select o;

                            studAnsw tsa = null;
                            String key1 = "Question not being attemped"; ;
                            if (q13.Count<studAnsw>() > 0)
                            {
                                tsa = q13.First<studAnsw>();
                                if (tsa.answ1 == 0) key1 = "A";
                                if (tsa.answ1 == 1) key1 = "B";
                                if (tsa.answ1 == 2) key1 = "C";
                                if (tsa.answ1 == 3) key1 = "D";
                            }
                            this.richTextBox2.AppendText("\n(" + key1 + ")_____________________________\n");
                           // this.richTextBox2.AppendText("_____________________________\n");
                                
                               
                                if (numofquestion[0] == 0)
                                {
                                    Paragraph para3 = s.AddParagraph();
                                    para3.AppendText(biaoti + ".选择题");
                                    biaoti++;
                                }
                                Paragraph para1 = s.AddParagraph();
                                numofquestion[0] = numofquestion[0] + 1;
                                para1.AppendRTF(numofquestion[0]+"."+richTextBox2.Rtf );


                            }
                        if (ed1.typeq ==1)
                        {
                                this.richTextBox2.Rtf = null;
                                var q12 = from o in pp.context.TFQues
                                      where o.id == ed1.qid
                                      select o;
                            TFQues mcq = q12.First<TFQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            richTextBox1.Text = "";
                            this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                           // this.richTextBox2.AppendText(richTextBox1.Rtf);
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == pp.st.studentid && o.did == ed1.id
                                      select o;

                            studAnsw tsa = null;
                            String key1 = "Question not being attemped"; ;
                            if (q13.Count<studAnsw>() > 0)
                            {
                                tsa = q13.First<studAnsw>();
                                if (tsa.answ2 == true) key1 = "True";
                                if (tsa.answ2 == false) key1 = "False";
                            }
                                this.richTextBox2.AppendText("\n(" + key1 + ")_____________________________\n");
                                // this.richTextBox2.AppendText("\n_____________________________\n");
                                // Section s = doc.AddSection();
                                //  Paragraph para1 = s.AddParagraph();
                                // para1.AppendRTF(richTextBox2.Rtf);

                                if (numofquestion[1] == 0)
                                {
                                    Paragraph para3 = s.AddParagraph();
                                    para3.AppendText(biaoti + ".判断题");
                                    biaoti++;
                                }
                                Paragraph para1 = s.AddParagraph();
                                numofquestion[1] = numofquestion[1] + 1;
                                para1.AppendRTF(numofquestion[1] + "." + richTextBox2.Rtf);

                            }

                        if (ed1.typeq == 2)
                        {



                        }

                        if (ed1.typeq == 3)
                        {
                                this.richTextBox2.Rtf = null;

                                var q12 = from o in pp.context.SQues
                                      where o.id == ed1.qid
                                      select o;
                            SQues mcq = q12.First<SQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            //this.richTextBox2.AppendText(richTextBox1.Rtf );
                            //get student answervar
                             var q13 = from o in pp.context.studAnsw
                                      where o.stid == pp.st.studentid && o.did == ed1.id
                                      select o;
                            if (q13.Count<studAnsw>() > 0)
                            {
                                studAnsw tsa = q13.First<studAnsw>();
                                //  String key1 = null;
                                // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                Byte[] mybyte = tsa.answ3;
                               // MessageBox.Show(mybyte.Length.ToString());
                                System.IO.MemoryStream ms = null;
                                if (mybyte != null)
                                    ms = new System.IO.MemoryStream(mybyte);
                                Image im = Image.FromStream(ms);
                                    int w = im.Size.Width;
                                    int h = im.Size.Height;
                                    //Section s = doc.AddSection();
                                    // Paragraph para1 = s.AddParagraph();
                                    // para1.AppendRTF(richTextBox2.Rtf);


                                    if (numofquestion[3] == 0)
                                    {
                                        Paragraph para3 = s.AddParagraph();
                                        para3.AppendText(biaoti + ".简答题");
                                        biaoti++;
                                    }
                                    Paragraph para1 = s.AddParagraph();
                                    numofquestion[3] = numofquestion[3] + 1;
                                    para1.AppendRTF(numofquestion[3] + "." + richTextBox2.Rtf);

                                    //  para1.AppendPicture(im); 
                                    Paragraph para2 = s.AddParagraph();                                   
                                    DocPicture picture = para2.AppendPicture(im);
                                    //设置图片大小         

                                    if (w < 450)
                                    {
                                        picture.Width = w;
                                        picture.Height = h;
                                    }
                                    else
                                    {
                                        picture.Width = 450;
                                        picture.Height = h*450/w;
                                        if (h * 450 / w>450) picture.Height = 450;

                                    }
                                }
                            else
                            {
                                this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                    // Paragraph para1 = s.AddParagraph();
                                    // para1.AppendRTF(richTextBox2.Rtf);

                                    if (numofquestion[3] == 0)
                                    {
                                        Paragraph para3 = s.AddParagraph();
                                        para3.AppendText(biaoti + ".简答题");
                                        biaoti++;
                                    }
                                    Paragraph para1 = s.AddParagraph();
                                    numofquestion[3] = numofquestion[3] + 1;
                                    para1.AppendRTF(numofquestion[3] + "." + richTextBox2.Rtf);

                                }

                        }
                        //////////////////////////////////////end3
                        if (ed1.typeq == 4)
                        {
                                this.richTextBox2.Rtf = null;

                                var q12 = from o in pp.context.AQues 
                                          where o.id == ed1.qid
                                          select o;
                                AQues mcq = q12.First<AQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                //this.richTextBox2.AppendText(richTextBox1.Rtf );
                                //get student answervar
                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == pp.st.studentid && o.did == ed1.id
                                          select o;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    studAnsw tsa = q13.First<studAnsw>();
                                    //  String key1 = null;
                                    // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                    //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                    Byte[] mybyte = tsa.answ3;
                                    // MessageBox.Show(mybyte.Length.ToString());
                                    System.IO.MemoryStream ms = null;
                                    if (mybyte != null)
                                        ms = new System.IO.MemoryStream(mybyte);
                                    Image im = Image.FromStream(ms);
                                    int w = im.Size.Width;
                                    int h = im.Size.Height;
                                    //Section s = doc.AddSection();
                                    // Paragraph para1 = s.AddParagraph();
                                    // para1.AppendRTF(richTextBox2.Rtf);

                                    if (numofquestion[4] == 0)
                                    {
                                        Paragraph para3 = s.AddParagraph();
                                        para3.AppendText(biaoti + ".分析题");
                                        biaoti++;
                                    }
                                    Paragraph para1 = s.AddParagraph();
                                    numofquestion[4] = numofquestion[4] + 1;
                                    para1.AppendRTF(numofquestion[4] + "." + richTextBox2.Rtf);

                                    //  para1.AppendPicture(im); 
                                    Paragraph para2 = s.AddParagraph();
                                    DocPicture picture = para2.AppendPicture(im);
                                    //设置图片大小         

                                    if (w < 450)
                                    {
                                        picture.Width = w;
                                        picture.Height = h;
                                    }
                                    else
                                    {
                                        picture.Width = 450;
                                        picture.Height = h * 450 / w;
                                        if (h * 450 / w > 450) picture.Height = 450;

                                    }
                                }
                                else
                                {
                                    this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                    this.richTextBox2.AppendText("\n_____________________________\n");
                                    // Paragraph para1 = s.AddParagraph();
                                    //para1.AppendRTF(richTextBox2.Rtf);
                                    if (numofquestion[4] == 0)
                                    {
                                        Paragraph para3 = s.AddParagraph();
                                        para3.AppendText(biaoti + ".分析题");
                                        biaoti++;
                                    }
                                    Paragraph para1 = s.AddParagraph();
                                    numofquestion[4] = numofquestion[4] + 1;
                                    para1.AppendRTF(numofquestion[4] + "." + richTextBox2.Rtf);

                                }

                            }

                            ////end4



                        }



               

                //////////////////savertf//////////////////////
                

                    // richTextBox2.SaveFile(saveFileDialog1.FileName);
                    try
                    {
                       
                            doc.SaveToFile(saveFileDialog1.FileName, FileFormat.Docx2013);
                            MessageBox.Show("WORD文件已生成！");
                        }
                    catch (Exception Err)
                    {
                        MessageBox.Show("WORD文件保存操作失败！" + Err.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    }
                }
                //////////////////////




            }
            else
            {
                MessageBox.Show("Please select an exercise!");
            }





        }

        private void dataGridView1_RowHeaderCellChanged(object sender, DataGridViewRowEventArgs e)
        { 
           
        }

        private void dataGridView1_MultiSelectChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            c_l = 2;
            DateTime now1=System.DateTime.Now;
           int selid3 = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            classExer ext = null;
            ext = lce.Where(o => o.eid == selid3).First();  
            if (now1 < ext.starttime) c_l = 1;
            if (now1 > ext.endtime ) c_l = 3;
            // MessageBox.Show(e.RowIndex.ToString());
            //   MessageBox.Show(lce[e.RowIndex].starttime.ToString());
            sel2 = e.RowIndex;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

           shiyan mq = null;
            if (mq == null || mq.IsDisposed)
            {
                mq = new shiyan(pp);
                mq.Show();
            }
            else
            {
                mq.Activate();
                mq.WindowState = FormWindowState.Normal;
            }



        }
    }
}
