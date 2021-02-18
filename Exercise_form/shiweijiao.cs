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


namespace Exercise_form
{
    public partial class shiweijiao : Form
    {
        param pp;
        classinfo cl;
        View_class_exp vce;
        studreport cstrep = null;
        List<View_class_student> lstwei = new List<View_class_student>();
        List<View_class_student> lstall = null;
        public shiweijiao(param pp1, classinfo cl1, View_class_exp vce1)
        {
            InitializeComponent();
            pp = pp1;
            cl = cl1;
            vce = vce1;

        }

        private void shiweijiao_Load(object sender, EventArgs e)
        {
            var questionQuery1 = from o in pp.context.View_class_student
                                 where o.classid == cl.classid
                                 orderby o.classno
                                 select o;

            if (questionQuery1.Count<View_class_student>() > 0)
            {
                lstall = questionQuery1.ToList<View_class_student>();
                lstwei.Clear();

                List<stu> lstu = null;
                var questionQuery2 = from o in pp.context.studreport
                                     where o.classid == cl.classid &&  o.expid == vce.expid
                                     select  new stu(o.stid);

                if (questionQuery2.Count() > 0)
                {
                    lstu = questionQuery2.ToList<stu>();

                    foreach (View_class_student vst in lstall)
                    {
                        bool wflag = true;
                        foreach (stu st1 in lstu)
                        {
                            if (vst.studentid == st1.studentid)
                            { wflag = false; continue; }
                        }
                        if (wflag) lstwei.Add(vst);
                    }
                    //endeach
                }// endquestionQuery2
                else
                    lstwei = lstall;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.DataSource = lstwei;


            }




        }
        ///
        private void getrep()
        {
           
      
            
            
                  


        }
        ///



    }
}
