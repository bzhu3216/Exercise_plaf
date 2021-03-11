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
    public partial class Edit_cal_exp : Form
    {
        
        param pp;       
        List<V_tea_course> lvtc = null;
        int cid = -1; 
        int sel1 = -1;
        int sel2 = -1;
        int sel3 = -1;
        List<classinfo> Lcs = null;
        List<exp_q> Lexp1 = null;
        List<View_class_exp> Lexp2 = null;
        List<exp_q> Lexp3 = null;
   
        public Edit_cal_exp(param p)
        {
            InitializeComponent();
            pp = p;
            lvtc = pp.ltea_c;
        
           
        }

        private void Edit_cal_exerL_Load(object sender, EventArgs e)
        {
            comboBox3.DataSource = lvtc;
            comboBox3.ValueMember = "CourseName";
           // comboBox3.Text = "";

            //dispalylist();
        }

        private void DisplayHScroll()
        {
            // Make sure no items are displayed partially.
            listBox1.IntegralHeight = true;          
            listBox1.HorizontalScrollbar = true;            
            Graphics g = listBox1.CreateGraphics();            
            int hzSize = (int)g.MeasureString(listBox1.Items[listBox1.Items.Count - 1].ToString(), listBox1.Font).Width;            
            listBox1.HorizontalExtent = hzSize;
            //2
            listBox2.IntegralHeight = true;
            listBox2.HorizontalScrollbar = true;
            Graphics g2 = listBox2.CreateGraphics();
            int hzSize2 = (int)g2.MeasureString(listBox2.Items[listBox2.Items.Count - 1].ToString(), listBox2.Font).Width;
            listBox2.HorizontalExtent = hzSize2;
            //3
            listBox3.IntegralHeight = true;
            listBox3.HorizontalScrollbar = true;
            Graphics g3 = listBox3.CreateGraphics();
            int hzSize3 = (int)g3.MeasureString(listBox3.Items[listBox3.Items.Count - 1].ToString(), listBox3.Font).Width;
            listBox3.HorizontalExtent = hzSize3;
        }




        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(9);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sel1 >= 0 && sel2 >= 0)
            {
                Exercise_form.ServiceReference1.classExp mcq = new classExp();
                mcq.attach = checkBox1.Checked;
                mcq.maxatta = int.Parse(comboBox2.Text);
                mcq.maxfile = int.Parse(comboBox1.Text);
                mcq.starttime = dateTimePicker1.Value;
                mcq.endtime = dateTimePicker2.Value;
                mcq.cid = ((classinfo)Lcs[sel1]).classid;
                mcq.expid = ((exp_q)Lexp1[sel2]).idexp;
               

                try
                {
                    pp.context.AddToclassExp(mcq);
                    pp.context.SaveChanges();
                }
                catch
                {
                    pp.context.DeleteObject(mcq);
                    MessageBox.Show("关联已经存在");
                }

            }
            else
            {
                MessageBox.Show("请选择班级和实验");
            }

            displayexp2();




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

            if (listBox3.SelectedIndex >= 0)
            {

                int cid2 = Lexp2[sel3].classid;
                int expid2 = Lexp2[sel3].expid;
                var q4 = from o in pp.context.classExp
                         where o.cid == cid2 && o.expid == expid2
                         select o;
                classExp eed = q4.First<classExp>();
                pp.context.DeleteObject(eed);
                pp.context.SaveChanges();

                displayexp2();




            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            sel1 = listBox1.SelectedIndex;
            displayexp2();
        }

        private void Edit_cal_exerL_Shown(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0)
            {
                int cid2 = Lexp2[sel3].classid;
                int expid2 = Lexp2[sel3].expid;
                var q4 = from o in pp.context.classExp
                         where o.cid == cid2 && o.expid == expid2
                         select o;
                classExp eed = q4.First<classExp>();

                eed.attach = checkBox1.Checked;
                eed.maxatta = int.Parse(comboBox2.Text);
                eed.maxfile = int.Parse(comboBox1.Text);
                eed.starttime = dateTimePicker1.Value;
                eed.endtime = dateTimePicker2.Value;
                pp.context.UpdateObject(eed) ;
                pp.context.SaveChanges();

                dispalylist();
            }

           

        
            else
            {

                MessageBox.Show("please select a list");
            }
            



        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {


            sel2 = listBox2.SelectedIndex;
            /*
            if (listBox2.SelectedIndex >= 0)
            {
                classExer ce = null;
                var q1 = from o in pp.context.classExer
                         where o.cid == Lcs2[listBox2.SelectedIndex].classid && o.eid == lid
                         select o;
                ce = q1.First<classExer>();
                dateTimePicker1.Value = (System.DateTime )ce.starttime ;
                dateTimePicker2.Value = (System.DateTime)ce.endtime ;
                sel2 = listBox2.SelectedIndex;


            }

    */




        }
        ///////////////////////////////////////


        private void dispalylist()
        {
            /*
            dateTimePicker1.Value = System.DateTime.Now;
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(9);
           // el = getexerL(lid);
            Lcs = getclasslist(el);
            Lcs2 = getclasslin2(el);
            listBox2.DataSource = Lcs2;
            listBox2.ValueMember = "classinfo1";
            var qq = Lcs.Except(Lcs2);
            Lcs3 = qq.ToList();
            listBox1.DataSource = Lcs3;
            listBox1.ValueMember = "classinfo1";
          // if (sel1 >= 0) listBox1.SelectedIndex = sel1;
          //  if (sel2 >= 0) listBox2.SelectedIndex = sel2;
          */





        }

        private void listBox2_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            if (listBox2.SelectedIndex >= 0)
            {
                classExer ce = null;
                var q1 = from o in pp.context.classExer
                         where o.cid == Lcs2[listBox2.SelectedIndex].classid && o.eid == lid
                         select o;
                ce = q1.First<classExer>();
                dateTimePicker1.Value = (System.DateTime)ce.starttime;
                dateTimePicker2.Value = (System.DateTime)ce.endtime;
                sel2 = listBox2.SelectedIndex;


            }*/
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox3.SelectedIndex >=0)cid = (int)lvtc[comboBox3.SelectedIndex].couseid;
            displayclass();
            displayexp();
            displayexp2();

        }
        //

        private void displayclass()
        {   
            var q1 = from o in pp.context.classinfo
                     where o.courseid == cid && o.teacher==pp.teacher.teacherid && o.finish==0
                     select o;
            if (q1.Count() > 0)
            {
                Lcs = q1.ToList<classinfo>();
                listBox1.DataSource = Lcs;
                listBox1.ValueMember = "classinfo1";
            }

            
        }

        private void displayexp()
        {
            var q1 = from o in pp.context.exp_q
                     where o.courseid == cid 
                     orderby o.con
                     select o;
            if (q1.Count() > 0)
            {
                Lexp1 = q1.ToList<exp_q>();
                listBox2.DataSource = Lexp1;
                listBox2.ValueMember = "exname";
              
            }

            
        }


        //




        private void displayexp2()
        {
            listBox3.DataSource = null;
            listBox3.Items.Clear();
            Lexp2 = null;
         // List<View_class_exp> Lexp21 = null;
         ;
            if (sel1 >= 0) { 
            var q1 = from o in pp.context.View_class_exp
                     where o.classid == ((classinfo)Lcs[sel1]).classid
                     orderby o.con
                     select o;
            if (q1.Count() > 0)
            {
                Lexp2 = q1.ToList<View_class_exp>();
                

            }


        }
           // listBox3.ValueMember = "exname";
            listBox3.DisplayMember = "exname";
            listBox3.DataSource = Lexp2;
            
        }

        






        private void listBox1_Click(object sender, EventArgs e)
        {
            sel1 = listBox1.SelectedIndex;
            displayexp2();
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            sel3 = listBox3.SelectedIndex;

            //

            if (listBox3.SelectedIndex >= 0)
            {
                int cid2 = Lexp2[sel3].classid;
                int expid2 = Lexp2[sel3].expid;
                var q4 = from o in pp.context.classExp
                         where o.cid == cid2 && o.expid == expid2
                         select o;
                classExp eed = q4.First<classExp>();

                // eed.attach = checkBox1.Checked;
                //eed.maxatta = int.Parse(comboBox2.Text);
                //eed.maxfile = int.Parse(comboBox1.Text);
                //eed.starttime = dateTimePicker1.Value;
                //eed.endtime = dateTimePicker2.Value;
                checkBox1.Checked = (bool)eed.attach;
                comboBox2.Text = eed.maxatta.ToString();
                comboBox1.Text= eed.maxfile.ToString();
                dateTimePicker1.Value = (DateTime)eed.starttime;
                dateTimePicker2.Value = (DateTime)eed.endtime;

            }

            //




        }
        ///////////////////////////////


    }
}
