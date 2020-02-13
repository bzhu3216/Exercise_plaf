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
    { param pp = null;
        TaskList tl = null;
        public List<class_student> lcsl = null;
        classinfo clinfo = null;
        List<View_student> lstv = null;
        exerL el = null;
        //List<mchoiceQues> Lmq = null;
        List<View_detai_exerL> ltvdl= null;
        public mark(TaskList tl1)
        {
            InitializeComponent();
            tl = tl1;
            clinfo = tl.lclinfo[tl.sel1];
            pp = tl.pp;
            lstv=getstudent2(clinfo);
            el = tl1.ler[tl.sel2];
            ltvdl = getmqbylnio(el);
          
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void mark_Load(object sender, EventArgs e)
        {
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.DataSource = lstv;
            
            


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
                lcsl2=q1.ToList<class_student>();
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
                    
                     where o.cid  == tcinfo.classid
                     select o;

             if (q1.Count<View_student>()>0)
                lst2 = q1.ToList<View_student>();

            return lst2;

        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = dataGridView2.RowCount;
            //DataGridViewRow row = dataGridView1.sle
            int i= 0;
            if(dataGridView2.CurrentRow!=null)
             i = dataGridView2.CurrentRow.Index;   
            if(i<count-1)        
            dataGridView2.CurrentCell = dataGridView2.Rows[i+1].Cells[0]; 




        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int count = dataGridView2.RowCount;
            //DataGridViewRow row = dataGridView1.sle
            int i = 0;
            if (dataGridView2.CurrentRow != null)
                i = dataGridView2.CurrentRow.Index;
            if (i >0)
                dataGridView2.CurrentCell = dataGridView2.Rows[i - 1].Cells[0];
        }

        //////////


        private List<View_detai_exerL> getmqbylnio(exerL  texl)
        {
            List<View_detai_exerL> ltvdl2 = new List<View_detai_exerL>();
            var q1 = from o in pp.context.View_detai_exerL 

                     where o.id == texl.id
                     select o;

            if (q1.Count<View_detai_exerL>() > 0)
                ltvdl2= q1.ToList<View_detai_exerL>();

            return ltvdl2;

        }

        private void dataGridView2_RowHeaderCellChanged(object sender, DataGridViewRowEventArgs e)
        {


           

        }

        private void dataGridView2_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
         
    




        }

        private void showdatagrid()
        {  
            View_student vst= lstv[dataGridView2.CurrentRow.Index];
            int qtype = listBox1.SelectedIndex;
            if(qtype==0)
            {
                //ltvdl
                var q0 = ltvdl.Where(o => o.typeq == 0);
                List<View_detai_exerL> ltvdl0 = q0.ToList<View_detai_exerL>();







            }





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







        //////        ////////////////////////////////////////
    }
}
