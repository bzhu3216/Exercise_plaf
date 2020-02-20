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
        int sel1 = -1;
        int sel2 = -1;
        List<classinfo> Lcs = null;
        List<classinfo> Lcs2 = null;
        List<classinfo> Lcs3 = null;
        exerL el;
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
               el = getexerL(lid);
               Lcs = getclasslist(el);            
             
            Lcs2 = getclasslin2(el);
            listBox2.DataSource = Lcs2;
            listBox2.ValueMember = "classinfo1";
            var qq = Lcs.Except(Lcs2);
            Lcs3 = qq.ToList();
            listBox1.DataSource = Lcs3;
            listBox1.ValueMember = "classinfo1";




        }






        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(9);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                sel1= listBox1.SelectedIndex;
                classExer ce = new classExer();
                ce.cid = Lcs3[sel1].classid;
                ce.eid = lid;
                ce.starttime = dateTimePicker1.Value;
                ce.endtime= dateTimePicker2.Value;
                pp.context.AddToclassExer(ce);
                pp.context.SaveChanges();               

            }

            Edit_cal_exerL_Load(sender, e);







        }


       



        public List<classinfo> getclasslist(exerL el)
        {
            List<classinfo> lcl = null;
            int cid = el.courseid;
            var q1 = from o in pp.context .classinfo
                     where o.courseid == cid && o.teacher == pp.teacher.teacherid
                     select o;
            if (q1.Count<classinfo>()>0) lcl = q1.ToList<classinfo>();

            return lcl;
        }
        public List<classinfo> getclasslin2(exerL el)
        {
            List<classinfo> lcl = null;
            int cl = el.id;
            int cc = el.courseid;
            var q2 = from cc1 in pp.context.classExer
                     where cc1.eid == cl
                     select cc1;
            List <classExer>  lce = q2.ToList<classExer>(); 
            var q1 = from o in pp.context.classinfo  
                     where (o.courseid==cc) && (o.teacher == pp.teacher.teacherid) 
                     select o;
            List<classinfo> lcl2 = q1.ToList<classinfo>();
            try
            {  if (lcl2 != null && lce != null)
                {
                    var lcl3 = lcl2.Where(s1 => lce.Any(c1 => s1.classid == c1.cid));
                    lcl = lcl3.ToList<classinfo>();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message );

            }
           
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                sel2 = listBox2.SelectedIndex;                
               int cid = Lcs2[sel2].classid;
                int eid = lid;
                var q4= from o in pp.context.classExer
                                 where o.cid == cid && o.eid == eid
                                 select o;
                classExer eed = q4.First<classExer>( );
                pp.context.DeleteObject(eed);
                pp.context.SaveChanges();
                



            }

            Edit_cal_exerL_Load(sender, e);




        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Edit_cal_exerL_Shown(object sender, EventArgs e)
        {
        }

        ///////////////////////////////


    }
}
