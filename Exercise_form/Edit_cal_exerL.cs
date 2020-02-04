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
    public partial class Edit_cal_exerL : Form
    {
        public Edit_cal_exerL()
        {
            InitializeComponent();
        }

        private void Edit_cal_exerL_Load(object sender, EventArgs e)
        {
              dateTimePicker1.Value= System.DateTime.Now;
              dateTimePicker2.Value = dateTimePicker1.Value.AddDays(9);
         
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(9);
        }
    }
}
