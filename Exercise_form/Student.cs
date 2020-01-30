using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Exercise_form
{
    class Student:INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
       
        public string studentid { get; set; }
        public string name{ get; set; }
        public string pd { get; set; }
        public int courseid { get; set; }
        public int classid { get; set; }
        public int no { get; set; }

        public Student()
        {



        }

        public Student(String id,String name2,int no1,string pd1 ,int clsid,int cid)
        {
            studentid = id;
            name = name2;
            no = no1;
            pd = pd1;
            classid = clsid;
            courseid = cid;

        }
        //


    }
}
