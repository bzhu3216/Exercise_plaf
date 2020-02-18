using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dirup = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dirup = openFileDialog1.FileName;
            }

            if (dirup != null)
            {


                Exercise_student.Piczip.CompressImage(dirup, @"c:\temp.jpg", 90, 1200, true);



            }
        }
    }
}
