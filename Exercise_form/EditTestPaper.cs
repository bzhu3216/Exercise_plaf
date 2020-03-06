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
    public partial class EditTestPaper : Form
    {
        param pp;
        exerL el = null;
       // V_tea_course vc = null;
        public EditTestPaper(param p,exerL el1)
        {
            pp = p;
            el = el1;
            
          //  vc = vc1;
            InitializeComponent();
        }

        private void EditTestPaper_Load(object sender, EventArgs e)
        {
            this.Text = el.id.ToString();


        }
    }
}
