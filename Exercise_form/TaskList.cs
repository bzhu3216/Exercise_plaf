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
        public List<class_student> lcsl = null;
        public List<classinfo> lclinfo = null;
        public List<classExer> lce = null;
        public List<exerL> ler = null;
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
        private List<exerL> tlel()
        {
            List<exerL> tlel2 = null;

            




            return tlel2;

        }








        ////////////////////////////


        //      ////////////////////////////////////////////////////////////////////////////
    }
}
