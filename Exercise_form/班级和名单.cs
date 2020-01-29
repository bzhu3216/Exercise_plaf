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
    public partial class NameList : Form
    {
        private db_exerciseEntities context;
        private param pp;
        List<classinfo> cls;
        int selindex = -1;
        public NameList(param p)
        {
            InitializeComponent();
            pp = p;
            context = p.context;

        }

        private void 班级和名单_Load(object sender, EventArgs e)
        {
            var classQuery1 = from o in context.classinfo
                                 where o.teacher == pp.teacher.teacherid
                                 orderby o.addtime descending
                                 select o;
            cls = classQuery1.ToList<classinfo>();
            listBox1.DataSource = cls;
            listBox1.ValueMember = "classinfo1";
           // DataServiceCollection<mchoiceQues> mques = new DataServiceCollection<mchoiceQues>(questionQuery);





        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
              selindex = listBox1.SelectedIndex;
            //  comboBox1.Text=
           // classinfo co= cls[selindex];

        

        }
    }
}
