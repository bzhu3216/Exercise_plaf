using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise_form.ServiceReference1;
namespace Exercise_form
{
   public class param
    {
        public db_exerciseEntities context;
        private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        public teacherinfo teacher=new teacherinfo();
        public int exerl1 = -1;
       
        public param()
        {
            context = new db_exerciseEntities(svcUri);
          
            
        }

    }
}
