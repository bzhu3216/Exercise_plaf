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
            dgvr.Cells[0].Value = vel.qid;
            //  dgvr.Cells[1].Value = stA.answ2;
            //  dgvr.Cells[2].Value = stA.mark;
            System.IO.MemoryStream ms = null;
            Byte[] mybyte = stA.answ3;
            if (mybyte != null)
                ms = new System.IO.MemoryStream(mybyte);
            if (ms != null)
                dgvr.Cells[3].Value = Image.FromStream(ms);
            //   ((System.Windows.Forms.DataGridViewComboBoxColumn)dataGridView1.Columns[4]).Items.Clear();


            if (stA.mark >= 0)
                dgvr.Cells[4].Value = stA.mark.ToString();
            // else
            //   dgvr.Cells[4].Value = "0";
            int hh = (int)ms.Length / 250;
            //MessageBox.Show(hh.ToString()); 
            if (hh > 350) hh = 350;
            dgvr.Height = hh;
            this.dataGridView1.Rows.Add(dgvr);





        }
    }
}
