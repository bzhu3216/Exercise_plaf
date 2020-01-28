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
    public partial class Uptatamcq : Form
    {


        private db_exerciseEntities context;
        private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        public Uptatamcq()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
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
                                    where o.id==5
                                    select o;


                mchoiceQues mcq = questionQuery.First<mchoiceQues>();
                comboBox1.Text = (mcq.objective).ToString();
                comboBox2.Text = (mcq.con).ToString();
                comboBox3.Text = (mcq.diff ).ToString();
                comboBox4.Text = (mcq.answ).ToString();

                ////////////read richtext

                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                this.rquestion.LoadFile(mstream, RichTextBoxStreamType.RichText);

                //////end read richtext


                // Make the DataServiceCollection<T> the binding source for the Grid.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void Uptatamcq_Load(object sender, EventArgs e)
        {

        }
    }
}
