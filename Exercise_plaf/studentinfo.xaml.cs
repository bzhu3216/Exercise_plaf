using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Diagnostics;

namespace Exercise_plaf
{
    /// <summary>
    /// studentinfo.xaml 的交互逻辑
    /// </summary>
    public partial class studentinfo : Window
    {
        ObservableCollection<Student> studentlist;

        public studentinfo()
        {
            InitializeComponent();
        }

        

        private void Button_Click(object sender, RoutedEventArgs e)
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
                studentlist=DB_exceltool.searchstubyclassid(1);


                if (studentlist.Count != 0)
                  
                // ((this.FindName("DG1")) as DataGrid).ItemsSource = peopleList;
                DG1.ItemsSource = studentlist;
               // DB_exceltool.savestudents(1);

            }
            //end 导入
        }

        private void DG1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
