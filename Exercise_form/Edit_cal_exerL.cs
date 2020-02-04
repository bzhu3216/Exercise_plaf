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
using System.Data.Services.Client;

namespace Exercise_form
{
    public partial class Edit_cal_exerL : Form
    {
        
        param pp;
        int lid = -1;
        List<classinfo> Lcs = null;
        public Edit_cal_exerL(int lid2,param p)
        {
            InitializeComponent();
            lid = lid2;
            pp = p;
           
        }

        private void Edit_cal_exerL_Load(object sender, EventArgs e)
        {
              dateTimePicker1.Value= System.DateTime.Now;
              dateTimePicker2.Value = dateTimePicker1.Value.AddDays(9);
               exerL el = getexerL(lid);
               Lcs = getclasslist(el);
              listBox1.DataSource = Lcs;
              listBox1.ValueMember = "classinfo1";

        }






        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(9);
        }

        private void button1_Click(object sender, EventArgs e)
        {





        }


        //////////////////////////



        public List<classinfo> getclasslist(exerL el)
        {
            List<classinfo> lcl = null;
            int cid = el.courseid;
            var q1 = from o in pp.context .classinfo
                     where o.courseid == cid && o.teacher == pp.teacher.teacherid
                     select o;
            lcl = q1.ToList<classinfo>();
            return lcl;
        }

        public exerL getexerL(int lid)
        {
            exerL el = null;

            var q1 = from o in pp.context.exerL
                     where o.id == lid
                     select o;
            el = q1.First<exerL>();
            return el;
        }

        ///////////////////////////////


    }
}
