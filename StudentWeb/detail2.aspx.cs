using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentWeb.ServiceReference1;
using RtfPipe;
using System.IO;
using System.Web.UI.HtmlControls;
namespace StudentWeb
{
    public partial class detail2 : System.Web.UI.Page
    {
        StudInfo st = null;
        String exeriseid = null;
        List<RadioButtonList> lrb = new List<RadioButtonList>();
        protected void Page_Load(object sender, EventArgs e)
        {
            ////
            Global gb = Session["gb"] as Global; ;
            paramst pp = gb.pp;
            Label1.Text = Session["Lexserise"] as String;
            exeriseid = Session["Lexserise"] as String;
            st = Session["user"] as StudInfo;

            var questionQuery1 = from o in pp.context.exerDetail
                                 where o.lid == int.Parse(exeriseid) && o.typeq == 1
                                 orderby o.id
                                 select o;
            List<exerDetail> ell = questionQuery1.ToList<exerDetail>();


            int numm = 0;

            foreach (exerDetail eld in ell)
            {
                var questionQuery2 = from o in pp.context.TFQues
                                     where o.id == eld.qid
                                     select o;
                TFQues  mcq = questionQuery2.First<TFQues>();
                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                StreamReader rt = new StreamReader(mstream);
                RtfSource rs = new RtfSource(rt);
                int no = numm + 1;
                var html2 = Rtf.ToHtml(rs);
                //  viewDiv.InnerHtml  = no+"."+html2;
                HtmlGenericControl Div2 = new HtmlGenericControl();
                Div2.ID = "div" + numm;
                HtmlGenericControl Div3 = new HtmlGenericControl();
                Div3.ID = "div" + (numm + 1000);
                RadioButtonList rbl = new RadioButtonList();
                rbl.ID = "rbl" + numm;
                rbl.Items.Add("True");
                rbl.Items.Add("False");
             
                lrb.Add(rbl);
                if (numm % 2 == 0)
                    Div2.Style.Add("lcs", "OVERFLOW: auto; WIDTH: 400px; HEIGHT: 400px;color:blue;font-size:16px");
                else
                    Div2.Style.Add("lcs", "OVERFLOW: auto; WIDTH: 400px; HEIGHT: 400px;color: balck;font-size:16px");
                rbl.RepeatDirection = RepeatDirection.Horizontal;
                ///读取答案
                var questionQuery3 = from o in pp.context.studAnsw
                                     where o.did == eld.id && o.lid == eld.lid && o.stid == st.studentid
                                     select o;
                if (questionQuery3.Count<studAnsw>() > 0)
                {
                    if (questionQuery3.First<studAnsw>().answ2 != null)
                    {
                        string cs = "";
                       bool ics = (bool)questionQuery3.First<studAnsw>().answ2;
                        if (!ics ) cs = "False";
                        if (ics ) cs = "True";                       
                        rbl.SelectedValue = cs;
                    }
                }



                ////


                Div3.Controls.Add(rbl);
                Div2.InnerHtml = no + "." + html2;
                numm = numm + 1;
                //   rrtf.Add(richTextBox1.Rtf);
                viewDiv.Controls.Add(Div2);
                viewDiv.Controls.Add(Div3);
                //查询答案
                /*var q2 = from q in pp.context.studAnsw
                         where q.lid == eld.lid && q.stid == pp.st.studentid && q.did == eld.id
                         select q;
                studAnsw answ1 = null;
                #00FFFF
                if (q2.Count<studAnsw>() > 0) { answ1 = q2.First<studAnsw>(); Lmqansw.Add(answ1); }
                ell = questionQuery1.ToList<exerDetail>();
                */


            }
            ////
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String URLstr = null;
            URLstr = @"result2.aspx?";
            int i = 0;
            if (lrb != null && lrb.Count() != 0)
            {
                foreach (RadioButtonList trb in lrb)
                {
                    URLstr = URLstr + "r" + i + "=" + trb.SelectedValue + @"&&";
                    i++;
                }
            }

            Response.Redirect(URLstr);
        }
    }
}