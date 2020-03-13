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
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
namespace Exercise_form
{
    public partial class TaskList : Form
    {
        public param pp;
       //public List<class_student> lcsl = null;
        public List<classinfo> lclinfo = null;
        public List<classExer> lce = null;
        public List<exerL> ler = new List<exerL>();
        public int sel1 = -1;
        public int sel2 = -1;
        public TaskList(param p)
        {
            InitializeComponent();
            pp = p;
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            lclinfo=tlcin();
            listBox1.DataSource = lclinfo;
            listBox1.ValueMember = "classinfo1";
            //   dataGridView1.AutoGenerateColumns = false;
            listBox1.SelectedIndex = -1;





        }
        /////////////////////////////////////////////////////////////////////////

        private List<classinfo> tlcin()
        {
            List<classinfo> tlcin2 = null;

            var q1 = from o in pp.context.classinfo
                     where o.teacher == pp.teacher.teacherid && o.finish==0
                     orderby  o.addtime   descending 
                     select o;
            if (q1.Count<classinfo>() > 0)
            {
                tlcin2 = q1.ToList<classinfo>();

            }


            return tlcin2;

        }
        ////////////////////////////
        private List<classExer> tlce(classinfo tcl)
        {
            List<classExer> tlce2 = null;
            var q2 = from o in pp.context.classExer
                     where o.cid == tcl.classid
                     select o;
            if (q2.Count<classExer>() > 0)
            {
                tlce2 = q2.ToList<classExer>();

            }
            

            return tlce2;

        }
        private void tlel(classinfo tcl)
        {
            
            lce = tlce(tcl);
            ler.Clear() ;
            if (lce != null){
                foreach (classExer ce in lce)
                {

                    var q1 = from o in pp.context.exerL
                             where o.id == ce.eid 
                             select o;
                    if (q1.Count<exerL>() > 0)
                    {

                        exerL tel = q1.First<exerL>();
                        ler.Add(tel);

                    }

                }
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  if (dataGridView1.DataSource != null)
            // {
            //     DataTable dt = (DataTable)dataGridView1.DataSource;
            //    dt.Rows.Clear();
            //     dataGridView1.DataSource = dt;
            //  }
            /*
            sel2 = -1;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            sel1 = listBox1.SelectedIndex;
            tlel(lclinfo[sel1]);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ler;
            pp.updataccid =(int) lclinfo[sel1].courseid;

    */








        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 )
            {
                if (sel2 >= 0)
                {

                    mark mq = null;
                    if (mq == null || mq.IsDisposed)
                    {
                        mq = new mark(this);
                        //  mq.MdiParent = this;
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

                    MessageBox.Show("请选择一个作业");
                }

            }



        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sel2 = e.RowIndex;
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (sel1 >= 0 && sel2 >= 0)
            {


                toRTF(lclinfo[sel1], ler[sel2]);


            }








        }








        ////////////////////////////

        private void toRTF2(classinfo tci,exerL tel )
        {
              saveFileDialog1.DefaultExt = ".rtf";
              saveFileDialog1.Filter = "RTF file|*.rtf";
             String dirsave = null;
            List<View_student> tlvst = null;
            exerL tel1 = tel;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
             {

               string  localFilePath = saveFileDialog1.FileName.ToString();
                dirsave = localFilePath.Substring(0, localFilePath.LastIndexOf("\\")); 

             }

            var q5 = from o in pp.context.View_student
                     where o.cid == tci.classid
                     select o;

            if (q5.Count<View_student>() > 0) tlvst = q5.ToList<View_student>();
            List<exerDetail> led = null;
            var q11 = from o in pp.context.exerDetail
                      where o.lid == tel1.id
                      orderby o.typeq
                      select o;
            if (q11.Count<exerDetail>()>0 )
            {
                led = q11.ToList<exerDetail>();
            }
                foreach (View_student vst in tlvst )
               { 

                richTextBox1.Text = "";
                this.richTextBox2.Text = ""; 
                if (q11.Count<exerDetail>() > 0)
                {
                   // led = q11.ToList<exerDetail>();
                    foreach (exerDetail ed1 in led)
                    {
                        //get question
                        if (ed1.typeq == 0)
                        {

                            var q12 = from o in pp.context.mchoiceQues
                                      where o.id == ed1.qid
                                      select o;
                            mchoiceQues mcq = q12.First<mchoiceQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            richTextBox1.Text = "";
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            this.richTextBox2.AppendText(richTextBox1.Text);
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == vst.stid  && o.did == ed1.id
                                      select o;

                            studAnsw tsa = null;
                            String key1 = "Question not being attemped"; ;
                            if (q13.Count<studAnsw>()>0) 
                            {
                                tsa = q13.First<studAnsw>();
                                if (tsa.answ1 == 0) key1 = "A";
                                if (tsa.answ1 == 1) key1 = "B";
                                if (tsa.answ1 == 2) key1 = "C";
                                if (tsa.answ1 == 3) key1 = "D";
                            }
                            this.richTextBox2.AppendText("\n(" + key1 + ")");
                            this.richTextBox2.AppendText("\n_____________________________\n");

                        }
                        if (ed1.typeq == 1)
                        {

                            var q12 = from o in pp.context.TFQues
                                      where o.id == ed1.qid
                                      select o;
                            TFQues mcq = q12.First<TFQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            richTextBox1.Text = "";
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            this.richTextBox2.AppendText(richTextBox1.Text);
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == vst.stid  && o.did == ed1.id
                                      select o;

                            studAnsw tsa = null;
                            String key1 = "Question not being attemped"; ;
                            if (q13.Count<studAnsw>() > 0)
                            {
                                tsa = q13.First<studAnsw>();
                                if (tsa.answ2 == true) key1 = "True";
                                if (tsa.answ2 == false) key1 = "False";
                            }
                            this.richTextBox2.AppendText("\n(" + key1 + ")");
                            this.richTextBox2.AppendText("\n_____________________________\n");

                        }

                        if (ed1.typeq == 2)
                        {



                        }

                        if (ed1.typeq == 3)
                        {

                            var q12 = from o in pp.context.SQues
                                      where o.id == ed1.qid
                                      select o;
                            SQues mcq = q12.First<SQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            this.richTextBox2.AppendText(richTextBox1.Text);
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == vst.stid  && o.did == ed1.id
                                      select o;


                            if (q13.Count<studAnsw>() > 0)
                            {
                                studAnsw tsa = q13.First<studAnsw>();
                                //  String key1 = null;
                                // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                Byte[] mybyte = tsa.answ3;
                                System.IO.MemoryStream ms = null;
                                if (mybyte != null)
                                    ms = new System.IO.MemoryStream(mybyte);
                                Image im = Image.FromStream(ms);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                Clipboard.SetDataObject(im, false);
                                richTextBox2.Paste();
                                this.richTextBox2.AppendText("\n_____________________________\n");
                            }
                            else
                            {
                                this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                this.richTextBox2.AppendText("\n_____________________________\n");

                            }

                        }
                        //////////////////////////////////////end3
                        if (ed1.typeq == 4)
                        {

                            var q12 = from o in pp.context.AQues
                                      where o.id == ed1.qid
                                      select o;
                            AQues mcq = q12.First<AQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            this.richTextBox2.AppendText(richTextBox1.Text);
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == vst.stid && o.did == ed1.id
                                      select o;

                            if (q13.Count<studAnsw>() > 0)
                            {
                                studAnsw tsa = q13.First<studAnsw>();
                                //  String key1 = null;
                                // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                Byte[] mybyte = tsa.answ3;
                                System.IO.MemoryStream ms = null;
                                if (mybyte != null)
                                    ms = new System.IO.MemoryStream(mybyte);
                                Image im = Image.FromStream(ms);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                Clipboard.SetDataObject(im, false);
                                richTextBox2.Paste();
                                this.richTextBox2.AppendText("\n_____________________________\n");
                            }
                            else
                            {
                                this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                this.richTextBox2.AppendText("\n_____________________________\n");

                            }

                        }

////end4


                    }



                }

                //////////////////savertf//////////////////////
                //  saveFileDialog1.DefaultExt = ".rtf";
                //   saveFileDialog1.Filter = "RTF file|*.rtf";

                //  if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                // {
                String stsavepath = null;
                stsavepath = dirsave + @"/" + vst.stid + vst.stname + ".rtf";

                richTextBox2.SaveFile(stsavepath);

                // }
                //////////////////////







            }
        }

        //////////////////////////////
        private void toRTF(classinfo tci, exerL tel)
        {
            saveFileDialog1.DefaultExt = ".docx";
            saveFileDialog1.Filter = "Word file|*.docx";
            String dirsave = null;
            List<View_student> tlvst = null;
            exerL tel1 = tel;
           
            var q5 = from o in pp.context.View_student
                     where o.cid == tci.classid
                     select o;

            if (q5.Count<View_student>() > 0) tlvst = q5.ToList<View_student>();
            List<exerDetail> led = null;
            var q11 = from o in pp.context.exerDetail
                      where o.lid == tel1.id
                      orderby o.typeq
                      select o;
            if (q11 != null)
            {

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = saveFileDialog1.FileName.ToString();
                    dirsave = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                    foreach (View_student vst in tlvst)
                  {
                        List<int> numofquestion = new List<int>(5);
                        for (int i = 0; i < 5; i++) numofquestion.Add(0);
                        int biaoti = 1;
                        richTextBox1.Text = "";
                this.richTextBox2.Text = "";
                 led = q11.ToList<exerDetail>();                
                        Document doc = new Document();
                        Section s = doc.AddSection();
                        led = q11.ToList<exerDetail>();
                        foreach (exerDetail ed1 in led)
                        {
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
                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
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

                                // Paragraph para1 = s.AddParagraph();
                                //para1.AppendRTF(richTextBox2.Rtf);
                                if (numofquestion[0] == 0)
                                {
                                    Paragraph para3 = s.AddParagraph();
                                    para3.AppendText(biaoti + ".选择题");
                                    biaoti++;
                                }
                                Paragraph para1 = s.AddParagraph();
                                numofquestion[0] = numofquestion[0] + 1;
                                para1.AppendRTF(numofquestion[0] + "." + richTextBox2.Rtf);

                            }
                            if (ed1.typeq == 1)
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
                                          where o.stid == vst.stid && o.did == ed1.id
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
                                // Paragraph para1 = s.AddParagraph();
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
                                          where o.stid == vst.stid && o.did == ed1.id
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
                                    //  Paragraph para1 = s.AddParagraph();
                                    //para1.AppendRTF(richTextBox2.Rtf);

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
                                          where o.stid == vst.stid && o.did == ed1.id
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
                                    //  para1.AppendRTF(richTextBox2.Rtf);
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


                                }

                            }

                            ////end4

                        }

                        //////////////////savedoc//////////////////////

                        String stsavepath = null;
                        stsavepath = dirsave + @"/" + vst.stid + vst.stname + ".docx";

                        // richTextBox2.SaveFile(saveFileDialog1.FileName);
                        try
                        {

                            doc.SaveToFile(stsavepath, FileFormat.Docx2013);
                        }
                        catch (Exception Err)
                        {
                            MessageBox.Show("WORD文件保存操作失败！" + Err.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }//end eachstudent

                    MessageBox.Show("文档生成结束！");




                }
            }
       
}

        private void button3_Click(object sender, EventArgs e)
        {
            //需要验证以及批改完，传入课程目标
            //

            

            if (sel1 >= 0 && sel2 >= 0)
            {

                var q1 = from o in pp.context.studAnsw
                         where o.mark<0 && o.lid == ler[sel2].id
                         orderby o.stid
                         select o;
              
                if (q1.Count<studAnsw>()>0)
                {
          
                    MessageBox.Show("还有作业没批好!");
                    return;
                }
                else
                { 
                /////////////////////////
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

                }

            }


        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            sel1 = listBox1.SelectedIndex;
            if (sel1 >= 0) {
            sel2 = -1;
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();           
            tlel(lclinfo[sel1]);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ler;
            pp.updataccid = (int)lclinfo[sel1].courseid;
            }

        }

        //////////////////////////////
        /////
        //      ////////////////////////////////////////////////////////////////////////////
    }
}
