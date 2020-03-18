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
    public partial class nosubmit : Form
    {
        param pp;
        classinfo cl;
        exerL el;
        List<class_student> lcs;
        public nosubmit(param pp1,classinfo cl1,exerL el1)
        {
            InitializeComponent();
            pp = pp1;
            cl = cl1;
            el = el1;
        }

        private void nosubmit_Load(object sender, EventArgs e)
        {
            lcs=EXtools.nosubmit(pp, cl, el);
            textBox1.Text = el.name;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1 .DataSource = lcs;
           
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
    }
}
