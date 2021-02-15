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
    public partial class shiyan : Form
    {
        paramst pp;
        classinfo clin;
        StudInfo stin;
        List<View_class_exp> lvce;
        public shiyan(paramst p, classinfo clin1, StudInfo stin1)
        {
            InitializeComponent();
            pp = p;
            clin = clin1;
            stin = stin1;
        }

        private void shiyan_Load(object sender, EventArgs e)
        {

            var questionQuery = from o in pp.context.View_class_exp
                                where (o.classid==clin.classid)
                                orderby o.con
                                select o;
            if (questionQuery.Count() > 0) lvce = questionQuery.ToList<View_class_exp>();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = lvce;




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
