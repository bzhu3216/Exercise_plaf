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
using Exercise_plaf.Servicestu;
//using Exercise_DAL;

namespace Exercise_plaf
{
    class DB_exceltool
    {

        public static ObservableCollection<Student> studentList=new ObservableCollection<Student>();
        //////////////////////////addstudent form excel
        public static void getstudentsfromexcel(String updir)
        {
           
            string strFileName = updir;
            object missing = System.Reflection.Missing.Value;
            Excel.Application excel = new Excel.Application();//lauch excel application
            if (excel == null)
            {
                // MessageBox.Show("没安装EXCEL？");
                string messageBoxText = "没安装EXCEL？";
                string caption = "提示";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                //显示消息框              
                MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            }
            else
            {
                excel.Visible = false; excel.UserControl = true;
                // 以只读的形式打开EXCEL文件
                Excel.Workbook wb = excel.Application.Workbooks.Open(strFileName, missing, true, missing, missing, missing,
                  missing, missing, missing, true, missing, missing, missing, missing, missing);
                //取得第一个工作薄
                Excel.Worksheet ws = (Excel.Worksheet)wb.Worksheets.get_Item(1);
                //取得总记录行数   (包括标题列)
                int rowsint = ws.UsedRange.Cells.Rows.Count; //得到行数
                                                             //int columnsint = mySheet.UsedRange.Cells.Columns.Count;//得到列数

                //取得数据范围区域 (不包括标题列)
                Excel.Range rng0 = ws.Cells.get_Range("a3", "c152");
                Excel.Range rng1 = ws.Cells.get_Range("c3", "c152"); //id
                Excel.Range rng2 = ws.Cells.get_Range("d3", "d152"); //MANE
                object[,] noss= (object[,])rng0.Value2;
                object[,] studentidss = (object[,])rng1.Value2;   //get range's value
                object[,] namess = (object[,])rng2.Value2;


                for (int i = 1; i <= 150; i++)

                {
                    {
                        if (studentidss[i, 1] != null)
                            studentList.Add(new Student(studentidss[i, 1].ToString(),namess[i,1].ToString(), int.Parse(noss[i,1].ToString()) ));
                    }

                }
                
            }

            excel.Quit(); excel = null;
            Process[] procs = Process.GetProcessesByName("EXCEL");
            foreach (Process pro in procs)
            {
                pro.Kill();//没有更好的方法,只有杀掉进程
            }
            GC.Collect();
        }
        ////endfromexcel//////////////////

        /////save student to db

        public static void savestudents(int classid)
        {

            Service_stuClient serviceDB = new Service_stuClient();
            List<Exercise_DAL.class_student> c_studl = new List<Exercise_DAL.class_student>();
            List<Exercise_DAL.StudInfo1> studentl = new List<Exercise_DAL.StudInfo1>();

            foreach (Student st in studentList)
            {
                Exercise_DAL.StudInfo1 sttemp = new Exercise_DAL.StudInfo1();
                sttemp.studentid = st.studentid;
                sttemp.name = st.name;
                sttemp.pd = "11111111";
                 Exercise_DAL.class_student c_st = new Exercise_DAL.class_student();
                c_st.classid = classid;
                c_st.studentid = st.studentid;
                c_st.classno = st.no;
                c_studl.Add(c_st);
                studentl.Add(sttemp);
            }
            serviceDB.Addstu(c_studl, studentl);
            ///////////////////////////////////////////////


        }
        ///endsave

        /////////////////////////begin search
        public static ObservableCollection<Student> searchstubyclassid(int classid2)

        {
            ObservableCollection<Student> studentList2 = new ObservableCollection<Student>();

            List<Exercise_DAL.Studenttemp> stl = new List<Exercise_DAL.Studenttemp>();
            Service_stuClient serviceDB = new Service_stuClient();
            stl = serviceDB.searchstubyclassid(classid2);

            foreach (Exercise_DAL.Studenttemp stt in stl)
            {
                studentList2.Add(new Student(stt.studentid, stt.name, stt.no));


            }


            return studentList2;

        }









//////end search

    }//endclass











}

