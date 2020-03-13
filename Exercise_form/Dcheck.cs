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

namespace Exercise_form
{
    public partial class Dcheck : Form
    {
        param pp;
        exerL el;
        public Dcheck(param p,exerL eel)
        {
            pp = p;
            el = eel;
            InitializeComponent();
        }

        private void Dcheck_Load(object sender, EventArgs e)
        {
            textBox1.Text = el.id.ToString();
            dataGridView1.DataSource = EXtools.checkd(pp,el.id );



        }
    }
}
