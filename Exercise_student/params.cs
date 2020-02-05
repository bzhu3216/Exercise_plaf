using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exercise_student.ServiceExer;
namespace Exercise_student
{
   public class param
    {
        public db_exerciseEntities context;
        private Uri svcUri = new Uri("http://localhost:1800/WcfDataServicequestion.svc");
        public StudInfo st;
        
        public param()
        {
            context = new db_exerciseEntities(svcUri);
            st = new StudInfo();

        }

    }
}
