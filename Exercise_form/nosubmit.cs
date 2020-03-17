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
    public partial class nosubmit : Form
    {
        param pp;
        classinfo cl;
        exerL el;
        List<class_student> lcs;
        public nosubmit(param pp1,classinfo cl1,exerL el1)
        {
            InitializeComponent();
            pp = pp1;
            cl = cl1;
            el = el1;
        }

        private void nosubmit_Load(object sender, EventArgs e)
        {
            lcs=EXtools.nosubmit(pp, cl, el);
            textBox1.Text = el.name;
            listBox1.DataSource = lcs;
            listBox1.ValueMember = "studentid";

        }
    }
}
