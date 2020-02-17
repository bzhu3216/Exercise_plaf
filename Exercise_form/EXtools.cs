using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using System.Collections.ObjectModel;
using Microsoft.Win32;

using System.Collections;
using System.Diagnostics;
//using Exercise_plaf.Servicestu;
using System.Windows.Forms;
using Spire.Xls;
using Exercise_form.ServiceReference1;
using System.Data;

namespace Exercise_form
{
    class EXtools
    {


        ///////////////start score

        public static void toScore(classinfo tci, exerL tel, string dirst, param p)
        {

            Workbook wb = new Workbook();
            //清除默认的工作表
            wb.Worksheets.Clear();
            //添加一个工作表并指定表名
            Worksheet sheet = wb.Worksheets.Add("score");

            ////
            //创建一个DataTable
            /*
            DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("姓名");
            dt.Columns.Add("性别");
            dt.Columns.Add("出生日期");
            dt.Columns.Add("学历");
            dt.Columns.Add("联系电话");
            dt.Columns.Add("职务");
            dt.Columns.Add("工号");
            dt.Rows.Add("王伟", "男", "1990年2月10日", "本科", "13524756854", "销售", "0054");
            dt.Rows.Add("李宁", "男", "1985年6月8日", "大专", "13259863247", "销售", "0055");
            dt.Rows.Add("邓家佳", "女", "1989年11月25日", "本科", "13601540352", "销售", "0029");
            dt.Rows.Add("杜平安", "男", "1978年4月16日", "中专", "13352014060", "保安", "0036");
            dt.Rows.Add("唐静", "女", "1980年1月21日", "本科", "13635401489", "店长", "0010");

            //将DataTable数据写入工作表
            sheet.InsertDataTable(dt, true, 2, 1, true);      

            */
            


            param pp = p;
            String dirsave = dirst;
            List<View_student> tlvst = null;
            exerL tel1 = tel;
            var q5 = from o in pp.context.View_student
                     where o.cid == tci.classid
                     select o;

            if (q5.Count<View_student>() > 0) tlvst = q5.ToList<View_student>();   //得到所有学生
            List<exerDetail> led = null;
            var q11 = from o in pp.context.exerDetail
                      where o.lid == tel1.id
                      orderby o.typeq
                      select o;
           
            if (q11.Count<exerDetail>() > 0)
            {
                led = q11.ToList<exerDetail>();
                //以下生成表头
              //  dt.Columns.Add("学号");
               // dt.Columns.Add("姓名");
               
                //


               // sheet.Range[1, 2].NumberValue = 100;

            }
            else
            {
                MessageBox.Show("没有习题");
            }
            bool whead = false;
            sheet.Range[2, 2].Text = "序号";
            sheet.Range[3, 2].Text = "分值";
            sheet.Range[4, 2].Text = "指标";

            int irow = 5;
            int icol = 1;
            foreach (View_student vst in tlvst)
            {
                sheet.Range[irow, icol].Text = vst.stid;
                sheet.Range[irow, icol + 1].Text = vst.stname ;
                int dcol = 3;
                int n_mq =1;
                int n_TF =1;
               // int n_em = 1;
                int n_sq = 1;
                int n_aq = 1;
                foreach (exerDetail ed1 in led)
                {
                    if (ed1.typeq == 0)
                    {
                        var q12 = from o in pp.context.mchoiceQues
                                  where o.id == ed1.qid
                                  select o;
                        mchoiceQues mcq = q12.First<mchoiceQues>();
                        var q13 = from o in pp.context.studAnsw
                                  where o.stid == vst.stid && o.did == ed1.id
                                  select o;
                        studAnsw tsa = null;

                        if (q13.Count<studAnsw>() > 0)
                        {
                            tsa = q13.First<studAnsw>();

                        }

                        if(irow==4)
                        {
                            //write head
                            sheet.Range[2, dcol].NumberValue = n_mq;
                            sheet.Range[2, dcol].NumberFormat = "0"; 
                            sheet.Range[3, dcol].NumberValue = (int)ed1.score;
                            sheet.Range[3, dcol].NumberFormat = "0";
                            sheet.Range[4, dcol].NumberValue = (int)mcq.objective ;
                            sheet.Range[4, dcol].NumberFormat = "0";
                            n_mq = n_mq + 1; 

                        }

                        if (tsa == null)
                            sheet.Range[irow, dcol].NumberValue = 0;
                        else
                            sheet.Range[irow, dcol].NumberValue = (int)tsa.mark;
                        sheet.Range[irow , dcol].NumberFormat = "0";
                        dcol = dcol + 1;





                    }
                    if (ed1.typeq == 1)
                    {

                        var q12 = from o in pp.context.TFQues
                                  where o.id == ed1.qid
                                  select o;
                        TFQues mcq = q12.First<TFQues>();
                        System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);

                        // this.richTextBox2.AppendText(richTextBox1.Rtf);
                        //get student answervar


                        var q13 = from o in pp.context.studAnsw
                                  where o.stid == vst.stid && o.did == ed1.id
                                  select o;

                        studAnsw tsa = null;
                       
                        if (q13.Count<studAnsw>() > 0)
                        {
                            tsa = q13.First<studAnsw>();
                            
                        }

                        if (irow == 4)
                        {
                            //write head
                            sheet.Range[2, dcol].NumberValue = n_TF;
                            sheet.Range[2, dcol].NumberFormat = "0";
                            sheet.Range[3, dcol].NumberValue = (int)ed1.score;
                            sheet.Range[3, dcol].NumberFormat = "0";
                            sheet.Range[4, dcol].NumberValue = (int)mcq.objective;
                            sheet.Range[4, dcol].NumberFormat = "0";
                            n_TF = n_TF + 1;

                        }

                        if (tsa == null)
                            sheet.Range[irow, dcol].NumberValue = 0;
                        else
                            sheet.Range[irow, dcol].NumberValue = (int)tsa.mark;
                        sheet.Range[irow, dcol].NumberFormat = "0";
                        dcol = dcol + 1;

                    }

                    if (ed1.typeq == 2)
                    {



                    }

                    if (ed1.typeq == 3)
                    {


                        var q12 = from o in pp.context.SQues
                                  where o.id == ed1.qid
                                  select o;
                        SQues mcq = q12.First<SQues>();                       

                        var q13 = from o in pp.context.studAnsw
                                  where o.stid == vst.stid && o.did == ed1.id
                                  select o;
                        studAnsw tsa = null;
                        if (q13.Count<studAnsw>() > 0)
                        {
                           tsa = q13.First<studAnsw>();                           

                        }
                        if (irow == 4)
                        {
                            //write head
                            sheet.Range[2, dcol].NumberValue = n_sq;
                            sheet.Range[2, dcol].NumberFormat = "0";
                            sheet.Range[3, dcol].NumberValue = (int)ed1.score;
                            sheet.Range[3, dcol].NumberFormat = "0";
                            sheet.Range[4, dcol].NumberValue = (int)mcq.objective;
                            sheet.Range[4, dcol].NumberFormat = "0";
                            n_sq = n_sq + 1;

                        }

                        if (tsa == null)
                            sheet.Range[irow, dcol].NumberValue = 0;
                        else
                            sheet.Range[irow, dcol].NumberValue = (int)tsa.mark;
                        sheet.Range[irow, dcol].NumberFormat = "0";
                        dcol = dcol + 1;

                    }
                    //////////////////////////////////////end3
                    if (ed1.typeq == 4)
                    {


                        var q12 = from o in pp.context.AQues
                                  where o.id == ed1.qid
                                  select o;
                        AQues mcq = q12.First<AQues>();

                        var q13 = from o in pp.context.studAnsw
                                  where o.stid == vst.stid && o.did == ed1.id
                                  select o;
                        studAnsw tsa = null;
                        if (q13.Count<studAnsw>() > 0)
                        {
                            tsa = q13.First<studAnsw>();

                        }
                        if (irow == 4)
                        {
                            //write head
                            sheet.Range[2, dcol].NumberValue = n_sq;
                            sheet.Range[2, dcol].NumberFormat = "0";
                            sheet.Range[3, dcol].NumberValue = (int)ed1.score;
                            sheet.Range[3, dcol].NumberFormat = "0";
                            sheet.Range[4, dcol].NumberValue = (int)mcq.objective;
                            sheet.Range[4, dcol].NumberFormat = "0";
                            n_sq = n_sq + 1;

                        }
                        if (tsa == null)
                            sheet.Range[irow, dcol].NumberValue = 0;
                        else
                            sheet.Range[irow, dcol].NumberValue = (int)tsa.mark;
                        sheet.Range[irow, dcol].NumberFormat = "0";
                        dcol = dcol + 1;


                    }

                    ////end4

                }//end each student
                irow = irow + 1;


            }


            //////////////////savedexcel/////////////////////
            sheet.AllocatedRange.AutoFitColumns();
            wb.SaveToFile(dirst, ExcelVersion.Version2013  );
                             MessageBox.Show("excel生成好了"); 

        }            //end  score
















    }//endclass
}
