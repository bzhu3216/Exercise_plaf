using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise_form.ServiceReference1;
using System.Configuration;
namespace Exercise_form
{
   public class param
    {
        public db_exerciseEntities context;
       // private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        public teacherinfo teacher=new teacherinfo();
        public List<V_tea_course> ltea_c = null;
        public int exerl1 = -1;
        public V_tea_course  vdlword = null;//用户生成word参数传递
        public exerL elword = null;//用户生成word参数传递
        public bool keyneed = false;
        public int updataccid=-1;

        public param()
        {
            String WCFIPstr = ConfigurationManager.AppSettings["WCFIP"].ToString();
            context = new db_exerciseEntities(new Uri(WCFIPstr));

          
            
        }

    }
}
