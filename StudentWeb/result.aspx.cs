using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentWeb.ServiceReference1;
using System.IO;

namespace StudentWeb
{
    public partial class result : System.Web.UI.Page
    {
        StudInfo st = null;
        String exeriseid = null;
        List<exerDetail> ell;
        protected void Page_Load(object sender, EventArgs e)
        {

           // String str= Request.QueryString["r0"];
           // Label1.Text = str;
            List<String> keystrl = new List<string>();
            //Global gb = new Global();
           Global gb = Session["gb"] as Global; ;
            paramst pp = gb.pp;           
            exeriseid = Session["Lexserise"] as String;
            st = Session["user"] as StudInfo;

            var questionQuery1 = from o in pp.context.exerDetail
                                 where o.lid == int.Parse(exeriseid) && o.typeq == 0
                                 orderby o.id
                                 select o;
           ell = questionQuery1.ToList<exerDetail>();
            for(int i=0;i<ell.Count();i++)
            {
                String index = "r" + i;
                String str = Request.QueryString[index];
                keystrl.Add(str);

            }


            int num = 0;
            foreach (exerDetail eld in ell)
            {
                
                ///更新答案
                var questionQuery3 = from o in pp.context.studAnsw
                                     where o.did == eld.id && o.lid == eld.lid && o.stid == st.studentid
                                     select o;
                if (questionQuery3.Count<studAnsw>() > 0)
                {
                   // if (questionQuery3.First<studAnsw>().answ1 != null)
                   // {
                        int cs = -1;
                        studAnsw stanser = questionQuery3.First<studAnsw>();
                        if (keystrl[num] == "A") cs = 0;
                        if (keystrl[num] == "B") cs = 1;
                        if (keystrl[num] == "C") cs = 2;
                        if (keystrl[num] == "D") cs = 3;
                        if (cs != -1) stanser.answ1 = cs;
                        pp.context.UpdateObject(stanser);

                  //  }
                }
                else
                {
                    if (keystrl[num] != null) { 
                    int cs = -1;                   
                    if (keystrl[num] == "A") cs = 0;
                    if (keystrl[num] == "B") cs = 1;
                    if (keystrl[num] == "C") cs = 2;
                    if (keystrl[num] == "D") cs = 3;                   
                    studAnsw tst = new studAnsw();
                    if (cs != -1) tst.answ1 = cs;
                    tst.did = eld.id;
                    tst.lid = eld.lid;
                    tst.stid = st.studentid;                   
                    pp.context.AddTostudAnsw(tst);
                    pp.context.SaveChanges();
                    }

                }
                num++;
            }//endeach
            pp.context.SaveChanges();





        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.opener=null;window.close();</script>");
        }
    }
}