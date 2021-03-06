using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentWeb
{
    public class extime
    {

        public int eid { get; set; }
        public string ename { get; set; }
        public DateTime?  stime { get; set; }
        public DateTime? etime { get; set; }

        public extime(int eid1,String name1,DateTime? st,DateTime? et)
        {
            eid = eid1;
            ename = name1;
            stime = st;
            etime = et;


        }


    }
}