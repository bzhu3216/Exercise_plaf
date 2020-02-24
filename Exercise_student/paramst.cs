using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise_student.ServiceExer;
using System.Configuration;
namespace Exercise_student
{
   public class paramst
    {
        public db_exerciseEntities context;
       // private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        public StudInfo st;
        
        public paramst()
        {
            String WCFIPstr = ConfigurationManager.AppSettings["WCFIP"].ToString();
            context = new db_exerciseEntities(new Uri(WCFIPstr));
           // context = new db_exerciseEntities(svcUri);
            st = new StudInfo();

            


        }

    }
}
