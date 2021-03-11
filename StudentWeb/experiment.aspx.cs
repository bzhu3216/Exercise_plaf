using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using StudentWeb.ServiceReference1;
using System.IO;
using System.Web.UI.HtmlControls;

namespace StudentWeb
{
    public partial class experiment : System.Web.UI.Page
    {
        string expid = null;
        paramst pp = null;
        exp_q qew = null;
        View_class_exp vce = null;
        StudInfo st = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            expid = Session["expid"] as String;
            Global gb = Session["gb"] as Global; ; ;
            vce = Session["vce"] as View_class_exp;
            st = Session["user"] as StudInfo;
            pp = gb.pp;
            if (expid != null)
            {

                var questionQuery2 = from o in pp.context.exp_q
                                     where (o.idexp == int.Parse(expid))

                                     select o;

                if (questionQuery2.Count<exp_q>() > 0)
                {
                    qew = questionQuery2.First<exp_q>();
                }
                labcontrol();

            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
          
            byte[] bytes = new byte[(int)qew.expdoc.Length];
            bytes = qew.expdoc;
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(qew.docfilename, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void labcontrol()
        {
            DateTime dtnow = DateTime.Now;
            DateTime stime = (DateTime)vce.starttime;
            DateTime etime = (DateTime)vce.endtime;
            if (dtnow < stime || dtnow > etime) { Button3.Enabled = false; Button4.Enabled = false; Button3.Text  = "到期无法上传"; Button4.Text = "到期无法上传"; ; }
            if (qew.attachment == null) { Button2.Enabled = false; Label5.Text = "无补充材料"; }
            else
            {
                Button2.Enabled = true; Label5.Text = "点击下载补充材料";

            }
            if (!(bool)vce.attach) { Button4.Enabled = false; Button4.Text = "无需上传实验附件"; Button6.Enabled = false; Button6.Text = "无实验附件下载"; }

            var questionQuery3 = from o in pp.context.studreport
                                 where o.expid == vce.expid && o.stid == st.studentid
                                 select o;

            if (questionQuery3.Count<studreport>() > 0)
            {
                studreport tst = questionQuery3.First<studreport>();
                if(tst.atta == null){
                    Button6.Enabled = false;
                    Button6.Text = "实验附件未上传";
                }


                
            }
            else
            {
                Button5.Enabled = false;
                Button5.Text = "实验报告未上传";
                Button6.Enabled = false;
                Button6.Text = "实验附件未上传";

            }



        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            byte[] bytes = new byte[(int)qew.expdoc.Length];
            bytes = qew.attachment;
            Response.ContentType = "application/octet-stream";
            //通知浏览器下载文件而不是打开
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(qew.attachmentname, System.Text.Encoding.UTF8));
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            HtmlInputFile tfb = File1;
            HttpPostedFile file = tfb.PostedFile;
           
                Stream objFile;
                objFile = file.InputStream;
                BinaryReader objReader = new BinaryReader(objFile);
                //读取文件内容
                byte[] byteFile = objReader.ReadBytes((int)objFile.Length);
                int size = (int)vce.maxfile;
                if (objFile.Length > size*1024 * 1024) Response.Write(@"<script>window.alert('上传文件要小于"+size+@"M')</script>");

                else
                {
                    if (file.FileName == "" || file.ContentLength == 0)
                    {
                        Response.Write("<script>window.alert('请选择文件')</script>");
                    }
                    else
                    { 
                    var questionQuery3 = from o in pp.context.studreport
                                         where o.expid  == vce.expid && o.stid ==st.studentid 
                                         select o;

                    if (questionQuery3.Count<studreport>() > 0)
                    {
                        studreport tst = questionQuery3.First<studreport>();
                        if (tst.rep != null)
                        {
                            tst.rep  = byteFile;
                            tst.fname = file.FileName;
                            pp.context.UpdateObject(tst);
                            pp.context.SaveChanges();
                            Response.AddHeader("Refresh", "0");
                        }

                    }
                    else
                    {
                        studreport tst = new studreport();
                        tst.expid= vce.expid;                        
                        tst.stid = st.studentid;
                        tst.fname = file.FileName;
                        tst.rep = byteFile;
                        pp.context.AddTostudreport(tst);
                        pp.context.SaveChanges();
                        Response.AddHeader("Refresh", "0");
                    }

                }
                }
            }

        protected void Button4_Click(object sender, EventArgs e)
        {

            HtmlInputFile tfb = File2;
            HttpPostedFile file = tfb.PostedFile;

            Stream objFile;
            objFile = file.InputStream;
            BinaryReader objReader = new BinaryReader(objFile);
            //读取文件内容
            byte[] byteFile = objReader.ReadBytes((int)objFile.Length);
            int size = (int)vce.maxatta;
            if (objFile.Length > size * 1024 * 1024) Response.Write(@"<script>window.alert('上传文件要小于" + size + @"M')</script>");

            else
            {
                if (file.FileName == "" || file.ContentLength == 0)
                {
                    Response.Write("<script>window.alert('请选择文件')</script>");
                }
                else
                {
                    var questionQuery3 = from o in pp.context.studreport
                                         where o.expid == vce.expid && o.stid == st.studentid
                                         select o;

                    if (questionQuery3.Count<studreport>() > 0)
                    {
                        studreport tst = questionQuery3.First<studreport>();
                      //  if (true)
                        //{
                            tst.atta = byteFile;
                            tst.aname = file.FileName;
                            pp.context.UpdateObject(tst);
                            pp.context.SaveChanges();
                            Response.AddHeader("Refresh", "0");
                       // }

                    }
                    else
                    {
                      
                        Response.Write("<script>window.alert('请先上传实验报告')</script>");


                    }

                }
            }





        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            var questionQuery3 = from o in pp.context.studreport
                                 where o.expid == vce.expid && o.stid == st.studentid
                                 select o;

            if (questionQuery3.Count<studreport>() > 0)
            {
                studreport tst = questionQuery3.First<studreport>();
                if (tst.rep != null)
                {

                    byte[] bytes = new byte[(int)tst.rep.Length];
                    bytes = tst.rep;
                    Response.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(tst.fname, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }



            }
            




        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            var questionQuery3 = from o in pp.context.studreport
                                 where o.expid == vce.expid && o.stid == st.studentid
                                 select o;

            if (questionQuery3.Count<studreport>() > 0)
            {
                studreport tst = questionQuery3.First<studreport>();
                if (tst.atta  != null)
                {

                    byte[] bytes = new byte[(int)tst.rep.Length];
                    bytes = tst.atta;
                    Response.ContentType = "application/octet-stream";
                    //通知浏览器下载文件而不是打开
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(tst.aname, System.Text.Encoding.UTF8));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }



            }
        }
    }//endclass
}