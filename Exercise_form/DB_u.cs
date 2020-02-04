using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exercise_form.ServiceReference1;
using System.Data.Services.Client;

namespace Exercise_form
{
   public  class DB_u
    {
        private db_exerciseEntities context;
        param pp;
        public DB_u(param p)
        {
            pp = p;
            context = p.context;
        }

        public List<classinfo> getclasslist(exerL el)
        {   
            List<classinfo> lcl = null;
            int cid = el.courseid;
            var q1 = from o in context.classinfo
                     where o.courseid == cid && o.teacher==pp.teacher.teacherid 
                     select o;
            lcl = q1.ToList<classinfo>();
            return lcl;    
          }



    }
}
