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
    public partial class TaskList : Form
    {
        public param pp;
       //public List<class_student> lcsl = null;
        public List<classinfo> lclinfo = null;
        public List<classExer> lce = null;
        public List<exerL> ler = new List<exerL>();
        public int sel1 = -1;
        public int sel2 = -1;
        public TaskList(param p)
        {
            InitializeComponent();
            pp = p;
        }

        private void TaskList_Load(object sender, EventArgs e)
        {
            lclinfo=tlcin();
            listBox1.DataSource = lclinfo;
            listBox1.ValueMember = "classinfo1";
         //   dataGridView1.AutoGenerateColumns = false;





        }
        /////////////////////////////////////////////////////////////////////////

        private List<classinfo> tlcin()
        {
            List<classinfo> tlcin2 = null;

            var q1 = from o in pp.context.classinfo
                     where o.teacher == pp.teacher.teacherid
                     orderby  o.addtime   descending 
                     select o;
            if (q1.Count<classinfo>() > 0)
            {
                tlcin2 = q1.ToList<classinfo>();

            }


            return tlcin2;

        }
        ////////////////////////////
        private List<classExer> tlce(classinfo tcl)
        {
            List<classExer> tlce2 = null;
            var q2 = from o in pp.context.classExer
                     where o.cid == tcl.classid
                     select o;
            if (q2.Count<classExer>() > 0)
            {
                tlce2 = q2.ToList<classExer>();

            }
            

            return tlce2;

        }
        private void tlel(classinfo tcl)
        {
            
            lce = tlce(tcl);
            ler.Clear() ;
            if (lce != null){
                foreach (classExer ce in lce)
                {

                    var q1 = from o in pp.context.exerL
                             where o.id == ce.eid 
                             select o;
                    if (q1.Count<exerL>() > 0)
                    {

                        exerL tel = q1.First<exerL>();
                        ler.Add(tel);

                    }

                }
            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  if (dataGridView1.DataSource != null)
            // {
            //     DataTable dt = (DataTable)dataGridView1.DataSource;
            //    dt.Rows.Clear();
            //     dataGridView1.DataSource = dt;
            //  }
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            sel1 = listBox1.SelectedIndex;
            tlel(lclinfo[sel1]);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = ler;
          




          



        }

        private void button1_Click(object sender, EventArgs e)
        {
            mark mq = null;
            if (mq == null || mq.IsDisposed)
            {
                mq = new mark(this);
              //  mq.MdiParent = this;
                mq.Show();
            }
            else
            {
                mq.Activate();
                mq.WindowState = FormWindowState.Normal;
            }






        }








        ////////////////////////////


        //      ////////////////////////////////////////////////////////////////////////////
    }
}
