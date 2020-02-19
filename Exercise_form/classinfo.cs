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
    public partial class classinfoF : Form
    {

       private db_exerciseEntities context;
       // private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        List<V_tea_course> lsc;
        List<classinfo> lsc2;
        int selindex = -1;
        param pp;
        public classinfoF(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;
        }
        public classinfoF()

        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void classinfo_Load(object sender, EventArgs e)
        {

            try
            {
                // Instantiate the DataServiceContext.
                //   context = new db_exerciseEntities(svcUri);

                // Define a LINQ query that returns Orders and 
                // Order_Details for a specific customer.
                // var questionQuery = from o in context.Orders.Expand("Order_Details")
                //                  where o.Customers.CustomerID == customerId
                //                 select o;
                listBox1.Items.Clear();
                comboBox1.Items.Clear();
                textBox1.Text = "";
                lsc = null;
                lsc2 = null;
                /*
               var questionQuery1 = from o in context.Course 
                                    select o ;
              
               lsc = questionQuery1.ToList();*/
                lsc = pp.ltea_c;

                comboBox1.DataSource = lsc;
                comboBox1.ValueMember = "CourseName";


                var questionQuery2 = from p in context.classinfo
                                     where p.teacher ==pp.teacher.teacherid 
                                     select p;

                lsc2 = questionQuery2.ToList();
                /*
                foreach (classinfo  cc in lsc2)
                {
                  listBox1.Items.Add(cc.classinfo1);
                }
                */
                listBox1.DataSource = lsc2;              

                listBox1.ValueMember = "classinfo1";
                comboBox2.Text = "";
                comboBox1.Text = "";
               
                // Make the DataServiceCollection<T> the binding source for the Grid.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }











        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_Click(object sender, EventArgs e)
        {
             selindex = listBox1.SelectedIndex;
            //  comboBox1.Text=
             textBox1.Text = lsc2[selindex].classinfo1;
            comboBox2.Text =comboBox2.Items[(int)(lsc2[selindex].finish)].ToString();
            int cid = (int)lsc2[selindex].courseid;
             foreach (V_tea_course  cc in lsc) {
                if ((int)cc.couseid  == cid) { comboBox1.Text = cc.CourseName;  }

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {  if (textBox1.Text != null && comboBox2.SelectedIndex >-1) { 
            classinfo ci = new classinfo();
            ci.classinfo1 = textBox1.Text;
                foreach (classinfo cinfo in lsc2) {

                    if (cinfo.classinfo1.Trim() == textBox1.Text.Trim()) { MessageBox.Show("已有同样的班级名称请改名!"); return; }   
                }
            ci.teacher = pp.teacher.teacherid;
                ci.finish = comboBox2.SelectedIndex;
            foreach (V_tea_course  cc in lsc)
            {
                if (comboBox1.Text == cc.CourseName) ci.courseid = cc.couseid ;

            }
                ci.addtime = System.DateTime.Now;
                context.AddToclassinfo(ci);
                context.SaveChanges();

            }
            listBox1.DataSource = null;
               var questionQuery2 = from p in context.classinfo
                                 where p.teacher == pp.teacher.teacherid
                                 select p;

            lsc2 = questionQuery2.ToList();
            // classinfo_Load(sender, e);
            listBox1.DataSource = lsc2;
            listBox1.ValueMember = "classinfo1";
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (selindex >=0)
            {
                String yname = lsc2[selindex].classinfo1.ToString();
                foreach (classinfo cinfo in lsc2)
                {

                    if (cinfo.classinfo1.Trim() == textBox1.Text.Trim() && yname.Trim()!= textBox1.Text.Trim()) { MessageBox.Show("已有同样的班级名称请改名!"); return; }
                }
                classinfo ci = lsc2[selindex] ;
                ci.classinfo1 = textBox1.Text;
              
                ci.teacher = pp.teacher.teacherid;
                ci.finish = comboBox2.SelectedIndex;
                foreach (V_tea_course  cc in lsc)
                {
                    if (comboBox1.Text == cc.CourseName) ci.courseid = cc.couseid ;

                }
                //  ci.addtime = System.DateTime.Now;
                context.UpdateObject(ci);
                context.SaveChanges();

            }

         






        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selindex >=0)
            {

                classinfo tcl = lsc2[selindex];
                var re = from o in pp.context.classExer
                         where o.cid == tcl.classid
                         select o;
                if (re.Count<classExer>() > 0)
                {
                    MessageBox.Show("已经有练习关联不能删除！先去除关联或联系管理员");

                }
                else
                {

                    pp.context.DeleteObject(tcl);
                    pp.context.SaveChanges();
                    
                }
            }

        }
  ////////////////////////      ////////////
    }
}
