using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFDBservice
{
    public class testfind
    {

        public int Addstudent()
        {
            try
            {
              

                return 1;
            }
            //catch (DbEntityValidationException ex)
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }


    }
}