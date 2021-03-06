﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentWeb.ServiceReference1;
using RtfPipe;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Reflection;

namespace StudentWeb
{
    public partial class detail : System.Web.UI.Page
    {   StudInfo st = null;
        String exeriseid = null;
        List<extime> ltemp = null;
        List<RadioButtonList> lrb = new List<RadioButtonList>();
        protected void Page_Load(object sender, EventArgs e)
        {

            Global gb = Session["gb"] as Global; ;
            Label1.Text = Session["Lexserise"] as String;
            exeriseid = Session["Lexserise"] as String;
            st = Session["user"] as StudInfo;
            ///////////////////////////////////////////////////////////
            ltemp= Session["ltemp"] as List<extime>;
            DateTime dtnow = DateTime.Now;
            // DateTime stime = new DateTime();
            // DateTime etime = new DateTime();
            //"{ eid = 3164, ename = 20-21绪论, stime = 2020/10/12 0:00:00, etime = 2020/11/6 0:00:00 }
            bool benable = true;
            foreach (extime o in ltemp)
            {   
                int lexid = o.eid;
                if (lexid ==int.Parse(exeriseid))
                {
                    DateTime stime = (DateTime)o.stime;
                    DateTime etime = (DateTime)o.etime;
                    if (dtnow < stime || dtnow > etime) benable = false;
                }
              }
            ////////////////////////////////////

            var questionQuery1 = from o in gb.pp.context.exerDetail
                                 where o.lid == int.Parse(exeriseid) && o.typeq == 0
                                 orderby o.id
                                 select o;
            List<exerDetail> ell = questionQuery1.ToList<exerDetail>();


            int numm = 0;

            foreach (exerDetail eld in ell)
            {  
                var questionQuery2 = from o in gb.pp.context.mchoiceQues 
                                     where o.id == eld.qid
                                     select o;
                mchoiceQues mcq = questionQuery2.First<mchoiceQues>();
                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                StreamReader rt = new StreamReader(mstream);
                RtfSource rs = new RtfSource(rt);
                int no = numm + 1;
                var html2 = Rtf.ToHtml(rs);
                //  viewDiv.InnerHtml  = no+"."+html2;
                HtmlGenericControl Div2 = new HtmlGenericControl();
                Div2.ID = "div"+ numm;
                HtmlGenericControl Div3 = new HtmlGenericControl();
                Div3.ID = "div" + (numm+1000);
                RadioButtonList rbl = new RadioButtonList();
                rbl.ID = "rbl" + numm;
                rbl.Items.Add("A");
                rbl.Items.Add("B");
                rbl.Items.Add("C");
                rbl.Items.Add("D");
                lrb.Add(rbl);
                if ( numm%2==0)
                    Div2.Style.Add("lcs", "OVERFLOW: auto; WIDTH: 400px; HEIGHT: 400px;color:blue;font-size:16px");
               else
                    Div2.Style.Add("lcs", "OVERFLOW: auto; WIDTH: 400px; HEIGHT: 400px;color: balck;font-size:16px");
                rbl.RepeatDirection = RepeatDirection.Horizontal;
                ///读取答案
                var questionQuery3 = from o in gb.pp.context.studAnsw
                                     where o.did== eld.id && o.lid == eld.lid && o.stid==st.studentid
                                     select o;
                if (questionQuery3.Count<studAnsw>() > 0)
                {
                    if (questionQuery3.First<studAnsw>().answ1 != null)
                    {
                        string cs = "";
                        int ics = (int)questionQuery3.First<studAnsw>().answ1;
                        if (ics == 0) cs = "A";
                        if (ics == 1) cs = "B";
                        if (ics == 2) cs = "C";
                        if (ics == 3) cs = "D";
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
            if (!benable) Button1.Enabled = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            




            String URLstr = null;
            URLstr = @"result.aspx?";
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