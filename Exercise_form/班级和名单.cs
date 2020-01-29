using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Exercise_form.ServiceReference1;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Diagnostics;
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

        private void button1_Click(object sender, EventArgs e)
        {


            //导入学生名单
            String dirup = null;
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".xlsx";
            ofd.Filter = "xlsx file|*.xlsx";
            if (ofd.ShowDialog() == true)
            {
                dirup = ofd.FileName;
                DB_exceltool.getstudentsfromexcel(dirup);
                DB_exceltool.savestudents(1);

                // studentlist = DB_exceltool.studentList;
                studentlist = DB_exceltool.searchstubyclassid(1);


                if (studentlist.Count != 0)

                    // ((this.FindName("DG1")) as DataGrid).ItemsSource = peopleList;
                    DG1.ItemsSource = studentlist;
                // DB_exceltool.savestudents(1);

            }
            //end 导入



        }
    }
}
