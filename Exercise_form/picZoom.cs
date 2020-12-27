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
    public partial class picZoom : Form
    {
        public picZoom()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            


        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            this.Height = 2 * this.Height;
            this.Width = 2 * this.Width;
        }

        private void picZoom_Load(object sender, EventArgs e)
        {

        }
    }
}
