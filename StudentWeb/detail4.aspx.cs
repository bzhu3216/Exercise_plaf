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
    public partial class detail4 : System.Web.UI.Page
    {
        StudInfo st = null;
        String exeriseid = null;
        List<RadioButtonList> lrb = new List<RadioButtonList>();
        List<HtmlInputFile> lfb = new List<HtmlInputFile>();
        List<exerDetail> ell = null;
        paramst pp = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ////
            Global gb = Session["gb"] as Global; ; ;
            pp = gb.pp;
            Label1.Text = Session["Lexserise"] as String;
            exeriseid = Session["Lexserise"] as String;
            st = Session["user"] as StudInfo;

            var questionQuery1 = from o in pp.context.exerDetail
                                 where o.lid == int.Parse(exeriseid) && o.typeq == 4
                                 orderby o.id
                                 select o;
            ell = questionQuery1.ToList<exerDetail>();
           



            int numm = 0;
            ///////////////////////////////////////////////////////////
            List<extime> ltemp = Session["ltemp"] as List<extime>;
            DateTime dtnow = DateTime.Now;
            // DateTime stime = new DateTime();
            // DateTime etime = new DateTime();
            //"{ eid = 3164, ename = 20-21绪论, stime = 2020/10/12 0:00:00, etime = 2020/11/6 0:00:00 }
            bool benable = true;
            foreach (extime o in ltemp)
            {
                int lexid = o.eid;
                if (lexid == int.Parse(exeriseid))
                {
                    DateTime stime = (DateTime)o.stime;
                    DateTime etime = (DateTime)o.etime;
                    if (dtnow < stime || dtnow > etime) benable = false;
                }
            }
            // if (!benable) Button1.Enabled = false;
            ////////////////////////////////////
            foreach (exerDetail eld in ell)
            {
                var questionQuery2 = from o in pp.context.AQues
                                     where o.id == eld.qid
                                     select o;
                AQues mcq = questionQuery2.First<AQues>();
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
                HtmlGenericControl Div4 = new HtmlGenericControl();
                Div4.ID = "pdiv" + numm;
                Button rbl = new Button ();
                if (!benable) rbl.Enabled = false;
                rbl.Click += new System.EventHandler(this.but_Click);
                rbl.CommandArgument = numm.ToString();
                HtmlInputFile  fbutton = new HtmlInputFile();
                fbutton.Accept = "image/*";
                lfb.Add(fbutton);
                // CommandArgument = "Descending"
                // OnCommand = "CommandBtn_Click"
                rbl.Text = "上传图片";
                rbl.ID = "rbl" + numm;
                Image Image1 = new Image();
                HtmlGenericControl Div5 = new HtmlGenericControl();
                Div5.InnerHtml= "</br>" +  "</br>";
                if (numm % 2 == 0)
                    Div2.Style.Add("lcs", "OVERFLOW: auto; WIDTH: 400px; HEIGHT: 400px;color:blue;font-size:16px");
                else
                    Div2.Style.Add("lcs", "OVERFLOW: auto; WIDTH: 400px; HEIGHT: 400px;color: balck;font-size:16px");
              
                ///读取答案
                var questionQuery3 = from o in pp.context.studAnsw
                                     where o.did == eld.id && o.lid == eld.lid && o.stid == st.studentid
                                     select o;
                if (questionQuery3.Count<studAnsw>() > 0)
                {
                    if (questionQuery3.First<studAnsw>().answ3 != null)
                    {

                        // bool ics = (bool)questionQuery3.First<studAnsw>().answ2;


                        //
                        //  Image himg = new Image();
                        // byte[] bytes = questionQuery3.First<studAnsw>().answ3;
                        // Response.BinaryWrite(bytes);
                        //  MemoryStream ms = new MemoryStream(bytes);
                        // himg = Image.FromStream(ms, true);
                        //  
                        byte[] bytes = questionQuery3.First<studAnsw>().answ3;
                        MemoryStream MStream = new MemoryStream(bytes);
                        string base64 = Convert.ToBase64String(MStream.ToArray());
                        Image1.ImageUrl = "data:image/png;base64," + base64;
                         Image1.ImageAlign = ImageAlign.Middle;
                       // Image1.Width = 600;
                        Image1.Width = Unit.Percentage(70);
                        Div4.Controls.Add(Image1 );
                       

                    }
                }



                ////

                Div3.Controls.Add(fbutton);
                Div3.Controls.Add(rbl);
                Div2.InnerHtml = "</br>"+no + "." + html2+"</br>";
                numm = numm + 1;
                //   rrtf.Add(richTextBox1.Rtf);
                viewDiv.Controls.Add(Div2);
               
                viewDiv.Controls.Add(Div4);
                viewDiv.Controls.Add(Div5);
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
        //


        protected void but_Click(object sender, EventArgs e)
        {

            Button rbl = (Button)sender;
            int indexp = int.Parse(rbl.CommandArgument);
            HtmlInputFile tfb = lfb[indexp];
            HttpPostedFile file = tfb.PostedFile;
            try
            {
                Stream objFile;
                objFile = file.InputStream;
                BinaryReader objReader = new BinaryReader(objFile);
                //读取文件内容
                byte[] byteFile = objReader.ReadBytes((int)objFile.Length);
                if (objFile.Length>350*1024) Response.Write("<script>window.alert('图片文件大于300k了')</script>");

                else
                {
                    if (file.FileName == "" || file.ContentLength == 0)
                    {
                        Response.Write("<script>window.alert('请选择文件')</script>");
                    }
                    else
                    {
                        exerDetail tel = ell[indexp];
                        var questionQuery3 = from o in pp.context.studAnsw
                                             where o.did == tel.id && o.lid == tel.lid && o.stid == st.studentid
                                             select o;
                        if (questionQuery3.Count<studAnsw>() > 0)
                        {
                            studAnsw tst = questionQuery3.First<studAnsw>();
                            if (tst.answ3 != null)
                            {
                                tst.answ3 = byteFile;
                                pp.context.UpdateObject(tst);
                                pp.context.SaveChanges();
                                Response.AddHeader("Refresh", "0");
                            }

                        }
                        else
                        {
                            studAnsw tst = new studAnsw();
                            tst.did = tel.id;
                            tst.lid = tel.lid;
                            tst.stid = st.studentid;
                            tst.answ3 = byteFile;
                            pp.context.AddTostudAnsw(tst);
                            pp.context.SaveChanges();
                            Response.AddHeader("Refresh", "0");

                        }
                    }





                }




                //文件扩展名
                //string strExtent = file.FileName.Substring(file.FileName.LastIndexOf("."));

            }
            catch (Exception ex)
            {
                Response.Write("<script>window.alert('请使用jpg文件')</script>");
            }


         //   Response.Write(@"<script>window.alert("+rbl+@")</script>");






        }
        //

    }
}