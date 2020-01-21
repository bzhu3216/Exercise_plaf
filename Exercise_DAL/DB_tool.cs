using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_DAL
{
    public class DB_tool
    {

        public int Addstudent(StudInfo1 student)
        {
            try
            {
                Exercise_ERContainer cn = new Exercise_ERContainer();
               if( cn.StudInfo1Set.Find(student.studentid)==null)
                cn.StudInfo1Set.Add(student);
                cn.SaveChanges();
                return 1;
            }
            //catch (DbEntityValidationException ex)
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }
        ///////////////////endstuinfo



        //////////////begin add student-class

        public int Addstudenttoclass(class_student  c_stud)
        {
            try
            {
                Exercise_ERContainer cn = new Exercise_ERContainer();
                
                    cn.class_student .Add(c_stud);
                     cn.SaveChanges();
                return 1;
            }
            //catch (DbEntityValidationException ex)
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                 return 0;
            }
        }

        ///end-add student-class


        //begin add studentinfo&&student-class
        public int Addstudentandclass(List<class_student> c_studl, List<StudInfo1> studentl)
        {
            Exercise_ERContainer cn = new Exercise_ERContainer();


            System.Data.Entity.DbContextTransaction tr = cn.Database.BeginTransaction();
            try
            {
                foreach (StudInfo1 student in studentl)
                    if (cn.StudInfo1Set.Find(student.studentid) == null)
                        cn.StudInfo1Set.Add(student);                
                foreach (class_student c_stud in c_studl)      
                        cn.class_student.Add(c_stud);
                cn.SaveChanges();
                tr.Commit();
                return 1;
            }
            //catch (DbEntityValidationException ex)
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                tr.Rollback();
                return 0;
            }



        }


        //end add studentinfo&&student-class



        /////////////////////search class student by id

        public List<Studenttemp> searchstubyclassid(int classid)
         {
            List<Studenttemp> lstu = new List<Studenttemp>();
            Exercise_ERContainer cn = new Exercise_ERContainer();
            
            var query = from st in cn.StudInfo1Set join cs in cn.class_student on st.studentid equals cs.studentid where  cs.classid== classid
                         select new { sid = st.studentid, sname= st.name ,sno=cs.classno } ;                
            foreach (var result in query)
            {
                Studenttemp st2 = new Studenttemp();
                st2.studentid = result.sid ;
                st2.name = result.sname;
                st2.no = (int)result.sno;
                lstu.Add(st2);
            }
            return lstu;

         }



        /////////////////////////////////////end search class




















    }//endclass
}
