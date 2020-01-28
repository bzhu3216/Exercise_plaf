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
        List<Course> lsc;
        List<classinfo> lsc2;
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
                var questionQuery1 = from o in context.Course 
                                    select o ;
              
               lsc = questionQuery1.ToList();
                foreach (Course cc in lsc)
                {
                    comboBox1.Items.Add(cc.CourseName);
                }
                var questionQuery2 = from p in context.classinfo
                                     select p;

                lsc2 = questionQuery2.ToList();
                foreach (classinfo  cc in lsc2)
                {
                  listBox1.Items.Add(cc.classinfo1);
                }


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
            int selindex = listBox1.SelectedIndex;
            //  comboBox1.Text=
             textBox1.Text = lsc2[selindex].classinfo1;
            int cid = (int)lsc2[selindex].courseid;
             foreach (Course cc in lsc) {
                if ((int)cc.id == cid) comboBox1.Text = cc.CourseName;

            }



        }

        private void button1_Click(object sender, EventArgs e)
        {
            classinfo ci = new classinfo();





        }
    }
}
