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
    public partial class NOMark : Form
    { param pp;
        exerL el;
        List<View_student> lstv;
        public NOMark(param p, exerL el2, List<View_student> lstv1)
        {
            InitializeComponent();
            pp = p;
            el = el2;
            lstv = lstv1;

        }

        private void NOMark_Load(object sender, EventArgs e)
        {

            var q1 = from o in pp.context.studAnsw
                         // where  o.mark==-100 && o.did ==el.id   
                     where o.mark < 0 &&o.lid  == el.id 
                     orderby o.stid 
                     select o;

            List<studAnsw> lst = null;
            if (q1.Count<studAnsw>()> 0)
            {

                lst = q1.ToList<studAnsw>();
                List<studAnsw> lst2 = null;

                var q2 = from o in lst
                         join staa in lstv on o.stid equals staa.stid
                         select o;

                if (q2.Count<studAnsw>() > 0)
                {
                    lst2= q2.ToList<studAnsw>();
                }

                

                    dataGridView1.DataSource = lst2; 



            }
            else
                MessageBox.Show("Congratulations!");




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
