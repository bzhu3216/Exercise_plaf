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

        {  if (c_l == 1) { MessageBox.Show("The time limit is not up!");return; }
            sel2 = -1;
           if (c_l == 3) { MessageBox.Show("The due time has expired. You can only view your submission."); ; }

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Selected == true)
                {
                    sel2 = int.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
            if (sel2 != -1)
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

                ltemp =q1.ToList<Object>();
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
                richTextBox2.Text = "";
                
                exerL tel1 = erl[sel2];
                List<exerDetail> led = null;
                var q11 = from o in pp.context.exerDetail
                          where o.lid == tel1.id
                          orderby o.typeq 
                          select o;
                if (q11 != null)
                {
                    led = q11.ToList<exerDetail>();
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
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            this.richTextBox2.AppendText(richTextBox1.Text );
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == pp.st.studentid && o.did == ed1.id
                                      select o;

                            studAnsw tsa = q13.First<studAnsw>();
                            String key1 = null;
                            if (tsa.answ1 == 0) key1 = "A";
                            if (tsa.answ1 == 1) key1 = "B";
                            if (tsa.answ1 == 2) key1 = "C";
                            if (tsa.answ1 == 3) key1 = "D";
                            this.richTextBox2.AppendText("\n(" + key1 + ")");
                            this.richTextBox2.AppendText("_____________________________\n");

                        }
                        if (ed1.typeq == 1)
                        {

                            var q12 = from o in pp.context.TFQues 
                                      where o.id == ed1.qid
                                      select o;
                            TFQues mcq = q12.First<TFQues>();
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                            this.richTextBox2.AppendText(richTextBox1.Text);
                            //get student answervar


                            var q13 = from o in pp.context.studAnsw
                                      where o.stid == pp.st.studentid && o.did == ed1.id
                                      select o;

                            studAnsw tsa = q13.First<studAnsw>();
                            String key1 = null;
                            if (tsa.answ2 == true) key1 = "True";
                            if (tsa.answ2 == false) key1 = "False";
                            
                            this.richTextBox2.AppendText("\n(" + key1 + ")");
                            this.richTextBox2.AppendText("_____________________________\n");

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
                                      where o.stid == pp.st.studentid && o.did == ed1.id
                                      select o;

                            studAnsw tsa = q13.First<studAnsw>();
                          //  String key1 = null;
                            System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                            this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                            this.richTextBox2.AppendText("\n_____________________________\n");
                            this.richTextBox2.AppendText(richTextBox1.Text);
                            this.richTextBox2.AppendText("\n_____________________________\n");

                        }







                    }



                }





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
            if (now1 < lce[e.RowIndex].starttime) c_l = 1;
            if (now1 > lce[e.RowIndex].endtime ) c_l = 3;
            // MessageBox.Show(e.RowIndex.ToString());
            //   MessageBox.Show(lce[e.RowIndex].starttime.ToString());
            sel2 = e.RowIndex;


        }
    }
}
