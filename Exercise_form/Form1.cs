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
    public partial class Form1 : Form
    {


        private db_exerciseEntities context;
        private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        public Form1()
        {
            InitializeComponent();
        }
        



        private void Form1_Load(object sender, EventArgs e)
        {




        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Instantiate the DataServiceContext.
                context = new db_exerciseEntities(svcUri);

                // Define a LINQ query that returns Orders and 
                // Order_Details for a specific customer.
                // var questionQuery = from o in context.Orders.Expand("Order_Details")
                //                  where o.Customers.CustomerID == customerId
                //                 select o;
                var questionQuery = from o in context.mchoiceQues
                                       select new { o.id, o.question, o.objective, o.con };
                                    //select o;
                // Create an DataServiceCollection<T> based on 
                // execution of the LINQ query for Orders.
               // DataServiceCollection<mchoiceQues> mques = new DataServiceCollection<mchoiceQues>(questionQuery);
               // questionQuery.ToList();
             
                quesView.DataSource = questionQuery.ToList();



                // Make the DataServiceCollection<T> the binding source for the Grid.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }




        }

        private void quesView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
