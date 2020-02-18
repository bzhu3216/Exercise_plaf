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
using System.IO;

namespace Exercise_student
{
    public partial class fdo : Form
    {
        paramst pp;
        exerL el;
        bool isBind = false;
        List<studAnsw> Lmqansw = new List<studAnsw>();
        int sel = -1;
        List<exerDetail> ell=null;
        Form1 ff = null;
        public fdo(Form1  f)
        {
            InitializeComponent();
            pp = f.pp;
            foreach (exerL tel in f.erl)
            {
                if (tel.id == f.sel2)
                    el = tel; 

            }
            ff = f;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fdo_Load(object sender, EventArgs e)
        {
            if (ff.c_l == 3) { dataGridView1.ReadOnly = true; dataGridView1.Columns[5].Visible = false; }
            if (ff.c_l == 2) { dataGridView1.ReadOnly = false; dataGridView1.Columns[5].Visible = true; }




        }

        ///////////////////////////////////////////////

        private void loadex(int a)
        {

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
                dataGridView1.Columns[3].Visible = true;

                ((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[3]).Items.Clear();
                    ((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[3]).Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "D"
            });


                ell = null;

                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid ==el.id && o.typeq==0
                                     select o;
               ell = questionQuery1.ToList<exerDetail>();

                int numm = 0;
            
                foreach (exerDetail eld in ell)
                {
                    var questionQuery2 = from o in pp.context.mchoiceQues
                                         where o.id == eld.qid
                                         select o;
                    mchoiceQues mcq = questionQuery2.First<mchoiceQues>();
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    //   rrtf.Add(richTextBox1.Rtf);
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    //查询答案
                    var q2 = from q in pp.context.studAnsw
                             where q.lid == eld.lid && q.stid == pp.st.studentid && q.did == eld.id 
                             select q;
                    studAnsw answ1 = null;

                    if (q2.Count<studAnsw>() > 0) { answ1 = q2.First<studAnsw>(); Lmqansw.Add(answ1); }


                    foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[2].Value = richTextBox1.Rtf;
                    dgvr.Cells[1].Value = mcq.id;
                    numm++;
                    dgvr.Cells[0].Value = numm;
                    string mqkey = null;
                    if (answ1 != null)
                    {
                        if (answ1.answ1 == 0) mqkey = "A";
                        if (answ1.answ1 == 1) mqkey = "B";
                        if (answ1.answ1 == 2) mqkey = "C";
                        if (answ1.answ1 == 3) mqkey = "D";
                    }
                    
                    dgvr.Cells[3].Value = mqkey;
                    int hh = (int)(richTextBox1.Rtf.Length / 10);
                    if (hh > 300) hh = 300;
                    dgvr.Height = hh;                 
                    this.dataGridView1.Rows.Add(dgvr);

                }

                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;

            }//end 0

            if (a == 1)

            {
                ((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[3]).Items.Clear();
                ((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[3]).Items.AddRange(new object[] {
            "True",
            "False"
            });
                dataGridView1.Columns[3].Visible = true;
                ell = null;
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 1
                                     select o;
                 ell = questionQuery1.ToList<exerDetail>();

                int numm = 0;

                foreach (exerDetail eld in ell)
                {
                    var questionQuery2 = from o in pp.context.TFQues
                                         where o.id == eld.qid
                                         select o;
                    TFQues mcq = questionQuery2.First<TFQues>();
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    //   rrtf.Add(richTextBox1.Rtf);
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    //查询答案
                    var q2 = from q in pp.context.studAnsw
                             where q.lid == eld.lid && q.stid == pp.st.studentid && q.did == eld.id
                             select q;
                    studAnsw answ1 = null;

                    if (q2.Count<studAnsw>() > 0) { answ1 = q2.First<studAnsw>(); Lmqansw.Add(answ1); }


                    foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[2].Value = richTextBox1.Rtf;
                    dgvr.Cells[1].Value = mcq.id;
                    numm++;
                    dgvr.Cells[0].Value = numm;
                    if (answ1 != null)
                     {
                        if (answ1.answ2 != null) { 
                    bool mqkey = (bool)answ1.answ2;

                    dgvr.Cells[3].Value = mqkey.ToString();
                        }
                    }
                    
                    int hh = (int)(richTextBox1.Rtf.Length / 8);
                    if (hh > 300) hh = 300;
                    dgvr.Height = hh;
                    this.dataGridView1.Rows.Add(dgvr);

                }

                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;


            }//end 1

//填空题预留
////
            if (a == 3)

            {
                dataGridView1.Columns[3].Visible = false;

                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[5].Visible = true;
                if (ff.c_l == 3) { dataGridView1.ReadOnly = true; dataGridView1.Columns[5].Visible = false; }
                if (ff.c_l == 2) { dataGridView1.ReadOnly = false; dataGridView1.Columns[5].Visible = true; }


                ell = null;

                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 3
                                     select o;
                ell = questionQuery1.ToList<exerDetail>();

                int numm = 0;

                foreach (exerDetail eld in ell)
                {
                    var questionQuery2 = from o in pp.context.SQues
                                         where o.id == eld.qid
                                         select o;
                    SQues mcq = questionQuery2.First<SQues>();
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    //   rrtf.Add(richTextBox1.Rtf);
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    //查询答案
                    var q2 = from q in pp.context.studAnsw
                             where q.lid == eld.lid && q.stid == pp.st.studentid && q.did == eld.id
                             select q;
                    studAnsw answ1 = null;
                    System.IO.MemoryStream ms = null;
                    if (q2.Count<studAnsw>() > 0) {
                        answ1 = q2.First<studAnsw>(); Lmqansw.Add(answ1);
                        //读取图片
                        Byte[] mybyte = answ1.answ3;
                        if(mybyte !=null)
                        ms = new System.IO.MemoryStream(mybyte);                     

                        //

                    }


                    foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[2].Value = richTextBox1.Rtf;
                    dgvr.Cells[1].Value = mcq.id;
                    numm++;
                    dgvr.Cells[0].Value = numm;
                    if(ms!=null)
                    dgvr.Cells[4].Value= Image.FromStream(ms);
                    int hh = (int)(richTextBox1.Rtf.Length / 8);
                    if (hh > 300) hh = 300;
                    dgvr.Height = hh;
                    this.dataGridView1.Rows.Add(dgvr);

                }


            }//end 3


            ////
            if (a == 4)

            {
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[5].Visible = true;
                if (ff.c_l == 3) { dataGridView1.ReadOnly = true; dataGridView1.Columns[5].Visible = false; }
                if (ff.c_l == 2) { dataGridView1.ReadOnly = false; dataGridView1.Columns[5].Visible = true; }

                ell = null;
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid == el.id && o.typeq == 4
                                     select o;
                ell = questionQuery1.ToList<exerDetail>();

                int numm = 0;

                foreach (exerDetail eld in ell)
                {
                    var questionQuery2 = from o in pp.context.AQues 
                                         where o.id == eld.qid
                                         select o;
                    AQues mcq = questionQuery2.First<AQues>();
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    //   rrtf.Add(richTextBox1.Rtf);
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    //查询答案
                    var q2 = from q in pp.context.studAnsw
                             where q.lid == eld.lid && q.stid == pp.st.studentid && q.did == eld.id
                             select q;
                    studAnsw answ1 = null;
                    System.IO.MemoryStream ms = null;
                    if (q2.Count<studAnsw>() > 0)
                    {
                        answ1 = q2.First<studAnsw>(); Lmqansw.Add(answ1);
                        //读取图片
                        Byte[] mybyte = answ1.answ3;
                        if (mybyte != null)
                            ms = new System.IO.MemoryStream(mybyte);

                        //

                    }


                    foreach (DataGridViewColumn c in this.dataGridView1.Columns)
                    {

                        dgvr.Cells.Add(c.CellTemplate.Clone() as DataGridViewCell);
                    }
                    dgvr.Cells[2].Value = richTextBox1.Rtf;
                    dgvr.Cells[1].Value = mcq.id;
                    numm++;
                    dgvr.Cells[0].Value = numm;
                    if (ms != null)
                        dgvr.Cells[4].Value = Image.FromStream(ms);
                    int hh = (int)(richTextBox1.Rtf.Length / 8);
                    if (hh > 300) hh = 300;
                    dgvr.Height = hh;
                    this.dataGridView1.Rows.Add(dgvr);

                }


            }//end 4


















        }

        /// /////////////////

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadex(listBox1.SelectedIndex);
            sel = listBox1.SelectedIndex;





        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
           // string msg = e.RowIndex.ToString() + e.ColumnIndex.ToString();
          //  MessageBox.Show(msg, "Cell State Changed");

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView1.CurrentCell.OwningColumn.Name == "Column4" && dataGridView1.CurrentCell.RowIndex != -1)
            {
                System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)e.Control;
               

                if (!isBind)
                {
                    cb.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                    isBind = true;
                }
            }
            else
            {
                isBind = false;
            }

        }
        ////

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
           // MessageBox.Show("dao1");
            int irow = dataGridView1.CurrentCell.RowIndex;
            int did = -1;
            if (ell != null) did = ell[irow].id;
            if(sel==0)
            savemqk(cb.SelectedIndex ,did);
            if (sel == 1)
            {
                bool key1 = false;
                if (cb.SelectedIndex == 0) key1 = true;
                saveTF(key1,did);
            }

            if (isBind)
            {
                cb.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                isBind = false;
            }

        }

        ////////////////////////////savemqkey

        private void savemqk(int key,int did2)
        {

            DataGridViewRow dv = this.dataGridView1.CurrentRow;
            int qid = int.Parse(dv.Cells[1].Value.ToString());
            // MessageBox.Show(dv.Cells[1].Value.ToString());
            studAnsw sansw = new studAnsw();
            sansw.answ1 = key;
            sansw.stid = pp.st.studentid;
            sansw.lid = el.id;
            sansw.did = did2;
            studAnsw sansw2 = null;
            var q2 = from q in pp.context.studAnsw
                     where q.lid == el.id && q.stid == pp.st.studentid && q.did == did2
                     select q;
            if(q2.Count<studAnsw>()>0)
            sansw2 = q2.First<studAnsw>();

            if (sansw2 == null)
            {
                pp.context.AddTostudAnsw(sansw);
                
            }
            else
            {
                sansw2.answ1 = key;
                pp.context.UpdateObject(sansw2);                    
                    
                    }
            pp.context.SaveChanges();

        }

        //////////////////////

        private void saveTF(bool key3,int did3)
        {

            DataGridViewRow dv = this.dataGridView1.CurrentRow;
            int qid = int.Parse(dv.Cells[1].Value.ToString());
            // MessageBox.Show(dv.Cells[1].Value.ToString());
            studAnsw sansw = new studAnsw();
            sansw.answ2 = key3;
            sansw.stid = pp.st.studentid;
            sansw.lid = el.id;
            sansw.did = did3;
            studAnsw sansw2 = null;

            try
            {
                var q3 = from q in pp.context.studAnsw
                         where q.lid == el.id && q.stid == pp.st.studentid && q.did == did3
                         select q;
                if (q3.Count<studAnsw>() > 0)
                    sansw2 = q3.First<studAnsw>();
                
            if (sansw2 == null)
            {
                pp.context.AddTostudAnsw(sansw);

            }
            else
            {
                sansw2.answ2 = key3;
                pp.context.UpdateObject(sansw2);

            }
            pp.context.SaveChanges();

            }
            catch (Exception e)

            {

                MessageBox.Show(e.Message);
            }

        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {


            int CIndex = e.ColumnIndex;
            if (CIndex == 5)
            {
                openFileDialog1.DefaultExt = ".jpg";
                openFileDialog1.Filter = "JPG file|*.jpg";
                String dirup = null;
                int irow = e.RowIndex;
                int did = -1;
                if (ell != null) did = ell[irow].id;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {                   
                    dirup = openFileDialog1.FileName;
                }

                if (dirup != null) {
                    System.IO.FileStream fs = null;


                   Piczip.CompressImage(dirup, @"c:\temp.jpg", 90, 120, true);
                    FileInfo ff = new FileInfo(@"c:\temp.jpg");
                    if (ff != null)
                    {

                       fs = new System.IO.FileStream(@"c:\temp.jpg", FileMode.Open, FileAccess.Read);
                    }
                    else
                    {
                        Piczip.CompressImage(dirup, @"c:\temp.jpg", 90, 120, true);
                        fs = new System.IO.FileStream(@"c:\temp.jpg", FileMode.Open, FileAccess.Read);
                    }
                  

                    //System.IO.FileStream fs = new System.IO.FileStream(dirup, FileMode.Open, FileAccess.Read);

                    // System.IO.MemoryStream fs = Piczip.CompressImage2(dirup, 80, 300, true);
                    /// if (fs.Length > 300 * 1024) { fs = Piczip.CompressImage2(dirup, 60, 300, true);MessageBox.Show(fs.Length.ToString()); }
                    // if (fs.Length > 300 * 1024) { fs = Piczip.CompressImage2(dirup, 40, 300, true); MessageBox.Show(fs.Length.ToString()); }
                    // if (fs.Length > 300 * 1024) { fs = Piczip.CompressImage2(dirup, 20, 300, true); MessageBox.Show(fs.Length.ToString()); }
                    // if (fs.Length > 300 * 1024) { fs = Piczip.CompressImage2(dirup, 10, 300, true); MessageBox.Show(fs.Length.ToString()); }
                    //声明Byte数组



                    if (fs.Length > 120 * 1024)
                    {
                        MessageBox.Show("请把图片处理成小于100k的再上传");
                    }
                    else
                    {
                       // MessageBox.Show(fs.Length.ToString() );
                        Byte[] mybyte = new byte[fs.Length];
                        //读取数据
                        fs.Read(mybyte, 0, mybyte.Length);                    
                        saveimg(mybyte, did);
                        loadex(sel);
                    }
                    fs.Close();
                }


            }
            if (CIndex == 4)
            {

                picZoom mq = null;
                    if (mq == null || mq.IsDisposed)
                {
                    DataGridViewRow dv = this.dataGridView1.CurrentRow;                   
                    mq = new picZoom();
                    mq.pictureBox1.Image = (Image)dv.Cells[4].Value;
                        mq.Show();
                    }
                    else
                    {  
                        mq.Activate();
                        mq.WindowState = FormWindowState.Normal;
                    }
                
             }



        }
        //////////////////////////////////////


        private void saveimg(Byte[]   key3, int did3)
        {

            DataGridViewRow dv = this.dataGridView1.CurrentRow;
            int qid = int.Parse(dv.Cells[1].Value.ToString());
            // MessageBox.Show(dv.Cells[1].Value.ToString());
            studAnsw sansw = new studAnsw();
            sansw.answ3 = key3;
            sansw.stid = pp.st.studentid;
            sansw.lid = el.id;
            sansw.did = did3;
            studAnsw sansw2 = null;

            try
            {
                var q3 = from q in pp.context.studAnsw
                         where q.lid == el.id && q.stid == pp.st.studentid && q.did == did3
                         select q;
                if (q3.Count<studAnsw>() > 0)
                    sansw2 = q3.First<studAnsw>();

                if (sansw2 == null)
                {
                    pp.context.AddTostudAnsw(sansw);

                }
                else
                {
                    sansw2.answ3 = null;
                    sansw2.answ3 = key3;
                    pp.context.UpdateObject(sansw2);

                }
                pp.context.SaveChanges();

            }
            catch (Exception e)

            {

                MessageBox.Show(e.Message);
            }

        }

        private void fdo_Shown(object sender, EventArgs e)
        {
            if (ff.c_l == 3) { dataGridView1.ReadOnly = true; dataGridView1.Columns[5].Visible = false; }
            if (ff.c_l == 2) { dataGridView1.ReadOnly = false; dataGridView1.Columns[5].Visible = true; }

        }












        //endsave




        ///////////////////////////////////////////////////

    }
}
