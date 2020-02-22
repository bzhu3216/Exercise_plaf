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
    public partial class Exercise_Summary : Form
    {
        param pp;
        public Exercise_Summary(param p)
        {
            InitializeComponent();
            pp = p;
        }

        private void Exercise_Summary_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {  if (checkBox1.Checked) pp.keyneed = true;
            EXtools tool = new EXtools();
            tool.toword(pp);





        }
    }
}
