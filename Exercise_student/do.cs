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
    public partial class fdo : Form
    {
        paramst pp;
        exerL el;
        bool isBind = false;

        public fdo(Form1  f)
        {
            InitializeComponent();
            pp = f.pp;
            foreach (exerL tel in f.erl)
            {
                if (tel.id == f.sel2)
                    el = tel; 

            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fdo_Load(object sender, EventArgs e)
        {





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
                var questionQuery1 = from o in pp.context.exerDetail
                                     where o.lid ==el.id && o.typeq==0
                                     select o;
               List<exerDetail> ell = questionQuery1.ToList<exerDetail>();

                int numm = 0;
                foreach (exerDetail el in ell)
                {
                    var questionQuery2 = from o in pp.context.mchoiceQues
                                         where o.id == el.qid
                                         select o;
                    mchoiceQues mcq = questionQuery2.First<mchoiceQues>();
                    System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                    this.richTextBox1.LoadFile(mstream, RichTextBoxStreamType.RichText);
                    //   rrtf.Add(richTextBox1.Rtf);
                    DataGridViewRow dgvr = new DataGridViewRow();
                    dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    //查询答案
                    var q2 = from q in pp.context.studAnsw
                             where q.lid == el.lid && q.stid == pp.st.studentid && q.did == mcq.id
                             select q;
                    studAnsw answ1 = null;

                    if (q2.Count<studAnsw>() > 0) answ1 = q2.First<studAnsw>(); 


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


            }




        }

/// /////////////////

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            loadex(listBox1.SelectedIndex);







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
            savemqk(cb.SelectedIndex );

            if (isBind)
            {
                cb.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                isBind = false;
            }

        }

        ////////////////////////////savemqkey

        private void savemqk(int key)
        {

            DataGridViewRow dv = this.dataGridView1.CurrentRow;
            int qid = int.Parse(dv.Cells[1].Value.ToString());
            // MessageBox.Show(dv.Cells[1].Value.ToString());
            studAnsw sansw = new studAnsw();
            sansw.answ1 = key;
            sansw.stid = pp.st.studentid;
            sansw.lid = el.id;
            sansw.did = qid;
            pp.context.AddTostudAnsw(sansw);
            pp.context.SaveChanges();


        }



        //endsave




        ///////////////////////////////////////////////////

    }
}
