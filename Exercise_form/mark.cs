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
    public partial class mark : Form
    {
        param pp = null;
        TaskList tl = null;
        public List<class_student> lcsl = null;
        classinfo clinfo = null;
        List<View_student> lstv = null;
        exerL el = null;
        //List<mchoiceQues> Lmq = null;
        List<View_detai_exerL> ltvdl = null;
        List<stkey> qansw = new List<stkey>();
        List<stkey2> TFansw = new List<stkey2>();
        public mark(TaskList tl1)
        {
            InitializeComponent();
            tl = tl1;
            clinfo = tl.lclinfo[tl.sel1];
            pp = tl.pp;
            lstv = getstudent2(clinfo);
            el = tl1.ler[tl.sel2];
            ltvdl = getmqbylnio(el);

        }

        private void mark_Load(object sender, EventArgs e)
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = lstv;
         //   dataGridView2.CurrentCell = dataGridView2.Rows[0].Cells[0];
           listBox1.SelectedIndex = 3;
            




        }
        ////////////////////////

        private List<class_student> getclassstudent(classinfo tcinfo)
        {
            List<class_student> lcsl2 = new List<class_student>();

            var q1 = from o in pp.context.class_student
                     where o.classid == tcinfo.classid
                     select o;
            if (q1.Count<class_student>() > 0)
            {
                lcsl2 = q1.ToList<class_student>();
            }

            return lcsl2;

        }
        ////////////////////////////////////////

        private List<StudInfo> getstudent(classinfo tcinfo)
        {
            List<StudInfo> lst2 = new List<StudInfo>();
            List<class_student> temp_cs = getclassstudent(tcinfo);

            foreach (class_student cs in temp_cs)
            {
                StudInfo st = null;
                var q1 = from o in pp.context.StudInfoes
                         where o.studentid == cs.studentid
                         select o;
                if (q1.Count<StudInfo>() > 0) st = q1.First<StudInfo>();

                lst2.Add(st);

            }
            return lst2;

        }


        private List<View_student> getstudent2(classinfo tcinfo)
        {
            List<View_student> lst2 = new List<View_student>();

            var q1 = from o in pp.context.View_student

                     where o.cid == tcinfo.classid
                     select o;

            if (q1.Count<View_student>() > 0)
                lst2 = q1.ToList<View_student>();

            return lst2;

        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            showdatagrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = dataGridView2.RowCount;
            //DataGridViewRow row = dataGridView1.sle
            int i = 0;
            if (dataGridView2.CurrentRow != null)
                i = dataGridView2.CurrentRow.Index;
            if (i < count - 1)
                dataGridView2.CurrentCell = dataGridView2.Rows[i + 1].Cells[0];

            dataGridView1.Rows.Clear();
            showdatagrid();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int count = dataGridView2.RowCount;
            //DataGridViewRow row = dataGridView1.sle
            int i = 0;
            if (dataGridView2.CurrentRow != null)
                i = dataGridView2.CurrentRow.Index;
            if (i > 0)
                dataGridView2.CurrentCell = dataGridView2.Rows[i - 1].Cells[0];
            showdatagrid();
        }

        //////////


        private List<View_detai_exerL> getmqbylnio(exerL texl)
        {
            List<View_detai_exerL> ltvdl2 = new List<View_detai_exerL>();
            var q1 = from o in pp.context.View_detai_exerL

                     where o.id == texl.id
                     select o;

            if (q1.Count<View_detai_exerL>() > 0)
                ltvdl2 = q1.ToList<View_detai_exerL>();

            return ltvdl2;

        }

        private void dataGridView2_RowHeaderCellChanged(object sender, DataGridViewRowEventArgs e)
        {




        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
           // showdatagrid();





        }

        private void showdatagrid()
        {
            dataGridView1.Rows.Clear();
            View_student vst = lstv[dataGridView2.CurrentRow.Index];
            int qtype = listBox1.SelectedIndex;
            if (qtype == 0)
            {
                //ltvdl
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Columns[2].Visible = true;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                var q0 = ltvdl.Where(o => o.typeq == 0);
                List<View_detai_exerL> ltvdl0 = q0.ToList<View_detai_exerL>();
                foreach (View_detai_exerL vel in ltvdl0)
                {
                    if (vel.typeq == 0)
                    {
                        var q1 = from o in pp.context.studAnsw
                                 where o.did == vel.Expr1 && o.stid == vst.stid
                                 select o;
                        if (q1.Count() > 0)
                        {
                            studAnsw stA = q1.First<studAnsw>();
                            DataGridViewRow dgvr = new DataGridViewRow();
                           // dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[0].Value = stA.did;
                            String an = "";
                            if (stA.answ1 == 0) an = "A";
                            if (stA.answ1 == 1) an = "B";
                            if (stA.answ1 == 2) an = "C";
                            if (stA.answ1 == 3) an = "D";
                            dgvr.Cells[1].Value = an;
                            dgvr.Cells[2].Value = stA.mark;
                            this.dataGridView1.Rows.Add(dgvr);

                            /////////////////////////////

                        }
                        else
                        {
                            DataGridViewRow dgvr = new DataGridViewRow();
                            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[0].Value = vel.Expr1;
                            dgvr.Cells[1].Value = null;
                            dgvr.Cells[2].Value = 0;
                            this.dataGridView1.Rows.Add(dgvr);
                        }
                    }
                }
            }//end0

            ////////////////////////////////////////////////
            if (qtype == 1)
            {
                //ltvdl
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Columns[2].Visible = true;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                var q0 = ltvdl.Where(o => o.typeq == 1);
                List<View_detai_exerL> ltvdl0 = q0.ToList<View_detai_exerL>();
                foreach (View_detai_exerL vel in ltvdl0)
                {
                    if (vel.typeq ==1)
                    {
                        var q1 = from o in pp.context.studAnsw
                                 where o.did == vel.Expr1 && o.stid == vst.stid
                                 select o;
                        if (q1.Count() > 0)
                        {
                            studAnsw stA = q1.First<studAnsw>();
                            DataGridViewRow dgvr = new DataGridViewRow();
                            // dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[0].Value = stA.did;
                            dgvr.Cells[1].Value = stA.answ2;
                            dgvr.Cells[2].Value = stA.mark;
                            this.dataGridView1.Rows.Add(dgvr);

                            /////////////////////////////

                        }
                        else
                        {
                            DataGridViewRow dgvr = new DataGridViewRow();
                            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            dgvr.Cells[0].Value = vel.Expr1;
                            dgvr.Cells[1].Value = null;
                            dgvr.Cells[2].Value = 0;
                            this.dataGridView1.Rows.Add(dgvr);
                        }
                    }
                }
            }
            ////////////////////////////////////////////////

            ///////////////////////////////////////end2



            if (qtype ==3)
            {
                //ltvdl
                
                var q0 = ltvdl.Where(o => o.typeq == 3);
                List<View_detai_exerL> ltvdl0 = q0.ToList<View_detai_exerL>();
                foreach (View_detai_exerL vel in ltvdl0)
                {
                    if (vel.typeq == 3)
                    {
                        //((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[4]).Items.Clear();
                      //  for (int i = 0; i <= vel.score; i++)
                       // {
                            //  a.Add(i); 

                            //((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[4]).Items.Add(i.ToString());
                       // }
                        var q1 = from o in pp.context.studAnsw
                                 where o.did == vel.Expr1 && o.stid == vst.stid
                                 select o;
                        if (q1.Count() > 0)
                        {
                            studAnsw stA = q1.First<studAnsw>();
                            DataGridViewRow dgvr = new DataGridViewRow();
                            // dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            DataGridViewComboBoxCell dc = (DataGridViewComboBoxCell)dgvr.Cells[4];
                            dc.Items.Clear();
                            for (int i = 0; i <= vel.score; i++)
                            {
                                dc.Items.Add(i.ToString());
                            }
                            dgvr.Cells[0].Value = stA.did;
                            //  dgvr.Cells[1].Value = stA.answ2;
                            //  dgvr.Cells[2].Value = stA.mark;
                            System.IO.MemoryStream ms = null;
                            Byte[] mybyte = stA.answ3;
                            if (mybyte != null)
                                ms = new System.IO.MemoryStream(mybyte);
                            if (ms != null)
                                dgvr.Cells[3].Value = Image.FromStream(ms);
                           
                         
                            if (stA.mark >=0) { 
                            dgvr.Cells[4].Value = stA.mark.ToString ();
                        }
                          // else
                             //  dgvr.Cells[4].Value = "0";
                            int hh = (int)ms.Length / 250;
                            //MessageBox.Show(hh.ToString()); 
                            if (hh > 350) hh = 350;
                            dgvr.Height =hh; 
                            this.dataGridView1.Rows.Add(dgvr);

                            /////////////////////////////

                        }
                        else
                        {
                            DataGridViewRow dgvr = new DataGridViewRow();
                           

                            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            DataGridViewComboBoxCell dc = (DataGridViewComboBoxCell)dgvr.Cells[4];
                            dc.Items.Clear();
                            for (int i = 0; i <= vel.score; i++)
                            {
                                dc.Items.Add(i.ToString());
                            }
                            dgvr.Cells[0].Value = vel.Expr1;
                            //  dgvr.Cells[3].Value = null;
                           // ((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[4]).Items.Clear();
                           // List<int>  a = new List<int>();
                            
                             dgvr.Cells[4].Value  = "0";
                            this.dataGridView1.Rows.Add(dgvr);
                        }
                    }
                }
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;
            }











            /////////////////////////////            end3


            if (qtype == 4)
            {
                //ltvdl


                var q0 = ltvdl.Where(o => o.typeq == 4);
                List<View_detai_exerL> ltvdl0 = q0.ToList<View_detai_exerL>();
                foreach (View_detai_exerL vel in ltvdl0)
                {
                    if (vel.typeq == 4)
                    {
                       
                        var q1 = from o in pp.context.studAnsw
                                 where o.did == vel.Expr1 && o.stid == vst.stid
                                 select o;
                        if (q1.Count() > 0)
                        {
                           
                            studAnsw stA = q1.First<studAnsw>();
                            DataGridViewRow dgvr = new DataGridViewRow();
                            // dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            DataGridViewComboBoxCell dc = (DataGridViewComboBoxCell)dgvr.Cells[4];
                            dc.Items.Clear();
                            for (int i = 0; i <= vel.score; i++)
                            {
                                dc.Items.Add(i.ToString());
                            }
                            dgvr.Cells[0].Value = stA.did;
                            //  dgvr.Cells[1].Value = stA.answ2;
                            //  dgvr.Cells[2].Value = stA.mark;
                            System.IO.MemoryStream ms = null;
                            Byte[] mybyte = stA.answ3;
                            if (mybyte != null)
                                ms = new System.IO.MemoryStream(mybyte);
                            if (ms != null)
                                dgvr.Cells[3].Value = Image.FromStream(ms);
                         //   ((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[4]).Items.Clear();
                            

                            if (stA.mark >=0)
                                dgvr.Cells[4].Value = stA.mark.ToString();
                           // else
                             //   dgvr.Cells[4].Value = "0";
                            int hh = (int)ms.Length / 250;
                            //MessageBox.Show(hh.ToString()); 
                            if (hh > 350) hh = 350;
                            dgvr.Height = hh;
                            this.dataGridView1.Rows.Add(dgvr);

                            /////////////////////////////

                        }
                        else
                        {
                           

                            DataGridViewRow dgvr = new DataGridViewRow();
                            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                            foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                            {

                                dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                            }
                            DataGridViewComboBoxCell dc = (DataGridViewComboBoxCell)dgvr.Cells[4];
                            dc.Items.Clear();
                            for (int i = 0; i <= vel.score; i++)
                            {
                                dc.Items.Add(i.ToString());
                            }
                            dgvr.Cells[0].Value = vel.Expr1;
                            //  dgvr.Cells[3].Value = null;
                           // 
                            // List<int>  a = new List<int>();
                            
                            dgvr.Cells[4].Value = "0";
                            this.dataGridView1.Rows.Add(dgvr);
                        }
                    }
                }

                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;



            }











            /////////////////////////////            end4










        }       //end shows
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


        ///////////////////////////////////////////

        private void markmqandTF()
        {
            // View_student vst = lstv[dataGridView2.CurrentRow.Index];
            var q0 = ltvdl.Where(o => o.typeq == 0) ;
            List<View_detai_exerL> ltvdl0 = q0.ToList<View_detai_exerL>();
            foreach (View_detai_exerL vde in ltvdl0)
            {
                var q01 = from o in pp.context.mchoiceQues
                          where o.id == vde.qid
                          orderby o.id 
                          select new stkey{ qid=o.id,qkey=(int)o.answ, lid=(int)vde.Expr1};
                if (q01.Count() > 0)

                {
                    qansw.Add(q01.First());

                }          
  
               }
            ////////////////////////////////////////endmq

            var t1 = ltvdl.Where(o => o.typeq == 1);
            List<View_detai_exerL> ltvdl1 =t1.ToList<View_detai_exerL>();
            foreach (View_detai_exerL vde in ltvdl1)
            {
                var t11 = from o in pp.context.TFQues 
                          where o.id == vde.qid
                          orderby o.id
                          select  new stkey2{ qid = o.id, qkey =(bool)o.answ, lid = (int)vde.Expr1 };
                if (t11.Count() > 0)

                {
                   TFansw.Add(t11.First());

                }

            }








            //////////////////////////////////////////////////



            foreach (View_student vs in lstv)

            {
                //if (vs.stid== "20171113202") { 
               
                foreach (View_detai_exerL vel in ltvdl)
                {
                        if (vel.typeq == 0)
                        {
                            var q1 = from o in pp.context.studAnsw
                                     where o.did == vel.Expr1 && o.stid == vs.stid
                                     select o;
                            if (q1.Count() > 0)
                            {
                               studAnsw stA = q1.First<studAnsw>();
                              stkey bstk = qansw.Find(o => o.lid == stA.did && (o.qkey == stA.answ1 + 1));
                                if (bstk != null)
                                {
                                    stA.mark =(int) vel.score;
                                    pp.context.UpdateObject(stA);
                                   // pp.context.SaveChanges();

                                }
                                else
                                {
                                    stA.mark = 0;
                                    pp.context.UpdateObject(stA);
                                   // pp.context.SaveChanges();
                                }
                            }
                         }
                    ////////////////////////////////end 00
                    if (vel.typeq == 1)
                    {
                        var q1 = from o in pp.context.studAnsw
                                 where o.did == vel.Expr1 && o.stid == vs.stid
                                 select o;
                        if (q1.Count() > 0)
                        {
                            studAnsw stA = q1.First<studAnsw>();
                            stkey2 bstk = TFansw.Find(o => o.lid == stA.did && (o.qkey ==(bool)stA.answ2 ));
                            if (bstk != null)
                            {
                                stA.mark = (int)vel.score;
                                pp.context.UpdateObject(stA);
                                // pp.context.SaveChanges();

                            }
                            else
                            {
                                stA.mark = 0;
                                pp.context.UpdateObject(stA);
                                // pp.context.SaveChanges();
                            }
                        }
                    }



                    ///////////////////////////////////////////////////////////

                    pp.context.SaveChanges();

                   // }
                }//for each vel







            }  


















            }

        private void button4_Click(object sender, EventArgs e)
        {
            markmqandTF();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //  MessageBox.Show("ok"); 
            if (e.ColumnIndex == 4 && listBox1.SelectedIndex == 3)
            {
                studAnsw stA = null;
                int qid = -1;
                qid = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                string stid = lstv[dataGridView2.CurrentRow.Index].stid;
                //  var q2 = ltvdl.Where(o => o.Expr1 == qid && o.typeq == 3);
                var q3 = from o in pp.context.studAnsw
                         where o.did == qid &&o.stid== stid
                         select o;
                if (q3.Count<studAnsw>() > 0)
                {
                    stA = q3.First<studAnsw>();
                    stA.mark = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());

                    pp.context.UpdateObject(stA);
                    pp.context.SaveChanges();
                }
                //  MessageBox.Show("ok");

            }


            if (e.ColumnIndex == 4 && listBox1.SelectedIndex == 4)
            {
                studAnsw stA = null;
                int qid = -1;
                string stid = lstv[dataGridView2.CurrentRow.Index].stid;
                qid = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                //  var q2 = ltvdl.Where(o => o.Expr1 == qid && o.typeq == 3);
                var q3 = from o in pp.context.studAnsw
                         where o.did == qid && o.stid == stid
                         select o;
                if (q3.Count<studAnsw>() > 0)
                {
                    stA = q3.First<studAnsw>();
                    stA.mark = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());

                    pp.context.UpdateObject(stA);
                    pp.context.SaveChanges();
                }
                //  MessageBox.Show("ok");

            }





        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            showdatagrid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
           

                NOMark  mq = null;
                if (mq == null || mq.IsDisposed)
                {
                    mq = new NOMark(pp, el);
                    //  mq.MdiParent = this;
                    mq.Show();
                }
                else
                {
                    mq.Activate();
                    mq.WindowState = FormWindowState.Normal;
                }
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }




        //////        ////////////////////////////////////////
    }
}
