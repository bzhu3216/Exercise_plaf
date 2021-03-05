using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentWeb.ServiceReference1;
using System.Configuration;
namespace StudentWeb
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
