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
        ObservableCollection<Student> studentlist;
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
            studentlist = null;

            // Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.DefaultExt = ".xlsx";
            openFileDialog1.Filter = "xlsx file|*.xlsx";
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            { 

               dirup = openFileDialog1.FileName;
                DB_exceltool.getstudentsfromexcel2(dirup);
                DB_exceltool.savestudents(cls[selindex].classid );

                // studentlist = DB_exceltool.studentList;
                studentlist = DB_exceltool.searchstubyclassid(cls[selindex].classid);


                if (studentlist.Count != 0)

                    // ((this.FindName("DG1")) as DataGrid).ItemsSource = peopleList;
                    dataGridView1.DataSource  = studentlist;
                // DB_exceltool.savestudents(1);

            }
            //end 导入



        }

        private void button2_Click(object sender, EventArgs e)
        {

            //////
            studentlist = null;

            studentlist = DB_exceltool.searchstubyclassid(cls[selindex].classid);


            if (studentlist.Count != 0)

                // ((this.FindName("DG1")) as DataGrid).ItemsSource = peopleList;
                dataGridView1.DataSource = studentlist;




            //////





        }
    }
}
