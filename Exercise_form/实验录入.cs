using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exercise_form
{
    public partial class shiya : Form
    {
        public shiya()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            ////////////////////////////

            OpenFileDialog dialog = new OpenFileDialog();
            
            dialog.Filter = "Word97-2003 files(*.doc)|*.doc|Word2007-2010 files (*.docx)|*.docx|All files (*.*)|*.*";
           
            dialog.Title = "Select a DOC file";
           
            dialog.Multiselect = false;
            
            dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\Data");
           



            DialogResult result = dialog.ShowDialog();
          



            if (result == DialogResult.OK)
                
            {
                
                try

                {
                    
                    //Load DOC document from file.

                    this.docViewer1.LoadFromFile(dialog.FileName);
                    
                }
               
                catch (Exception ex)

                {
                   
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
               
            }


                ///////////////////////////


            }
        }
}
