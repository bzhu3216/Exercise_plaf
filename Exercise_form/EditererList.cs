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
    public partial class EditererList : Form
    {
        private db_exerciseEntities context;
        param pp;
       // List<Course> lcs = null;
      //  List<exerL> el = null;
       // int cid = -1;
        public EditererList(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;


        }

        private void EditererList_Load(object sender, EventArgs e)
        {

        }
    }
}
