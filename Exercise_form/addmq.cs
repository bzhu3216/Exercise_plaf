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
    public partial class addmq : Form
    {
        private db_exerciseEntities context;
        private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        public addmq()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

           // try
           // {
                // Instantiate the DataServiceContext.
                context = new db_exerciseEntities(svcUri);

                mchoiceQues mcq= new mchoiceQues();
                mcq.answ = comboBox4.SelectedIndex + 1;
                mcq.con = Convert.ToInt16(comboBox2.Text);
                mcq.diff = Convert.ToInt16(comboBox3.Text);
                mcq.objective = Convert.ToInt16(comboBox1.Text);
                ////////////write richtext

                System.IO.MemoryStream mstream = new System.IO.MemoryStream();
              this.rquestion.SaveFile(mstream, RichTextBoxStreamType.RichText);
                  //将流转换成数组
                //  byte[] bWrite = mstream.ToArray();
                 mcq.question = mstream.ToArray();
                context.AddTomchoiceQues(mcq);
                //////end write richtext

                context.SaveChanges();

                // Make the DataServiceCollection<T> the binding source for the Grid.
          //  }
          //  catch (Exception ex)
           // {
           //     MessageBox.Show(ex.ToString());
           // }









        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void rquestion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            rquestion.SaveFile(@"d:\a.rtf");
        }
    }
}
