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
    public partial class compare : Form
    {
        List<exerL> ell;
        V_tea_course vcc;
        param pp;


        public compare(List<exerL> ell1,V_tea_course vcc1,param p)
        {
            ell = ell1;
            vcc = vcc1;
            pp = p;
            InitializeComponent();
        }

        private void compare_Load(object sender, EventArgs e)
        {

            listBox1.DataSource = ell;
            listBox1.ValueMember = "name";
            gengrid();

        }

        private void button1_Click(object sender, EventArgs e)
        {




        }

        //////////////////////////

        private List<exerDetail> compareel(exerL e1,exerL e2, ref int  num)
         {
            num = 0;
            List<exerDetail> led = new List<exerDetail>();
            List<exerDetail> led1 = null;
            List<exerDetail> led2 = null;
            var q1 = from o in pp.context.exerDetail
                     where o.lid == e1.id
                     select o;
            if (q1.Count() > 0) led1 = q1.ToList<exerDetail>();
            var q2 = from o in pp.context.exerDetail
                     where o.lid == e2.id
                     select o;
            if (q2.Count() > 0) led2 = q2.ToList<exerDetail>();
            foreach (exerDetail te1 in led1)
            {
                foreach (exerDetail te2 in led2)
                {
                    if(te1.typeq == te1.typeq && te1.qid ==te2.qid )
                    {
                        num = num + 1;
                        if (te1.score > te2.score)
                            led.Add(te2);
                        else
                            led.Add(te1);
                    }
                }
            }  
            return led;
        }


        private void gengrid()
        {
            int num = ell.Count();

            DataTable dt = new DataTable();//建立个数据表
            for (int i = 0; i < num; i++)
            {
                dt.Columns.Add(new DataColumn((i+1).ToString(), typeof(int)));//在表中添加int类型的列
               


            }
            DataRow dr;//行
            for (int i = 0; i < num; i++)
            {
                dr = dt.NewRow();
               // dr["id"] = i;
               // dr["Name"] = "Name" + i;
                dt.Rows.Add(dr);//在表的对象的行里添加此行
            }
            
            dataGridView2.DataSource = dt;
            for (int i = 0; i < num; i++)
                dataGridView2.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;






        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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

        /////////////////////////////



    }//endclass
}
