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
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc;
using System.Drawing;

namespace Exercise_form
{
    class EXtools
    {
        private System.Windows.Forms.SaveFileDialog saveFileDialog1 = new SaveFileDialog();

        ///////////////start score

        public static void toScore(classinfo tci, exerL tel, string dirst, param p,int tobj )
        {

            Workbook wb = new Workbook();
            //清除默认的工作表
            wb.Worksheets.Clear();
            //添加一个工作表并指定表名
            Worksheet sheet = wb.Worksheets.Add("score");
          
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
           
            sheet.Range[2, 2].Text = "序号";
            sheet.Range[3, 2].Text = "分值";
            sheet.Range[4, 2].Text = "指标";

            int irow = 5;
            int icol = 1;
            if (tlvst ==null) { MessageBox.Show("班级没有学生");return; }
            foreach (View_student vst in tlvst)
            {
                List<int> listobj = new List<int>(tobj + 1) ;
                for (int i = 0; i <= tobj; i++) { listobj.Add(0); }
                for (int i = 0; i <= tobj; i++) { listobj[i] = 0; }
                List<int> listobj2 = new List<int>(tobj + 1);
                for (int i = 0; i <= tobj; i++) { listobj2.Add(0); }
                for (int i = 0; i <= tobj; i++) { listobj2[i] = 0; }
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

                        if(irow==5)
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
                        listobj[(int)mcq.objective] = listobj[(int)mcq.objective] + (int)sheet.Range[irow, dcol].NumberValue ;
                        listobj2[(int)mcq.objective] = listobj2[(int)mcq.objective] + (int)ed1.score ;


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

                        if (irow == 5)
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
                        listobj[(int)mcq.objective] = listobj[(int)mcq.objective] + (int)sheet.Range[irow, dcol].NumberValue;
                        listobj2[(int)mcq.objective] = listobj2[(int)mcq.objective] + (int)ed1.score;
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
                        if (irow == 5)
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
                        listobj[(int)mcq.objective] = listobj[(int)mcq.objective] + (int)sheet.Range[irow, dcol].NumberValue;
                        listobj2[(int)mcq.objective] = listobj2[(int)mcq.objective] + (int)ed1.score;
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
                        if (irow == 5)
                        {
                            //write head
                            sheet.Range[2, dcol].NumberValue = n_aq;
                            sheet.Range[2, dcol].NumberFormat = "0";
                            sheet.Range[3, dcol].NumberValue = (int)ed1.score;
                            sheet.Range[3, dcol].NumberFormat = "0";
                            sheet.Range[4, dcol].NumberValue = (int)mcq.objective;
                            sheet.Range[4, dcol].NumberFormat = "0";
                            n_aq = n_aq + 1;

                        }
                        if (tsa == null)
                            sheet.Range[irow, dcol].NumberValue = 0;
                        else
                            sheet.Range[irow, dcol].NumberValue = (int)tsa.mark;
                        sheet.Range[irow, dcol].NumberFormat = "0";
                        listobj[(int)mcq.objective] = listobj[(int)mcq.objective] + (int)sheet.Range[irow, dcol].NumberValue;
                        listobj2[(int)mcq.objective] = listobj2[(int)mcq.objective] + (int)ed1.score;
                        dcol = dcol + 1;


                    }

                    ////end4

                }//end each student
                 ///////////////////////////处理目标///
                dcol++;
                for (int i = 1; i <= tobj; i++)
                {

                    if (irow == 5)
                    {
                        sheet.Range[4, dcol].NumberValue = i;
                        sheet.Range[2 ,dcol].Text  = "目标";
                        sheet.Range[3, dcol].NumberValue = listobj2[i];
                        sheet.Range[3, dcol].NumberFormat = "0";
                    }

                    sheet.Range[irow, dcol].NumberValue = listobj[i];
                    sheet.Range[irow, dcol].NumberFormat = "0";
                    dcol++;

                }
                irow = irow + 1;

            }



      



            //////////////////savedexcel/////////////////////
            sheet.AllocatedRange.AutoFitColumns();
            wb.SaveToFile(dirst, ExcelVersion.Version2013  );
                             MessageBox.Show("excel生成好了");

        }            //end  score


        ///exerl to summy
        ///
        public static string  toSummary(exerL tel,V_tea_course  cc,param p)
        {
            StringBuilder st = new StringBuilder(); ;

            param pp = p;
            exerL tel1 = tel;


            List<int> numofquestion = new List<int>(5);
            for (int i = 0; i < 5; i++) numofquestion.Add(0);
            List<int> totalscoreofques = new List<int>(5);
            for (int i = 0; i < 5; i++) totalscoreofques.Add(0);
            //////
            List<int> numofcon= new List<int>(cc.numcontent);
            for (int i = 0; i < cc.numcontent; i++) numofcon.Add(0);
            List<int> totalscoreofcon = new List<int>(cc.numcontent);
            for (int i = 0; i < cc.numcontent; i++) totalscoreofcon.Add(0);
            ///
            List<int> objectiveofcon = new List<int>(cc.numobjective);
            for (int i = 0; i < cc.numobjective; i++) objectiveofcon.Add(0);
            List<int> objectivescoreofofcon = new List<int>(cc.numobjective);
            for (int i = 0; i < cc.numobjective; i++) objectivescoreofofcon.Add(0);
            //
            List<int> diffof = new List<int>(cc.diff);
            for (int i = 0; i < cc.diff; i++) diffof.Add(0);
            List<int> diffscore = new List<int>(cc.diff);
            for (int i = 0; i < cc.diff; i++) diffscore.Add(0);

///////////////////////////        



            List<exerDetail> led = null;
            var q11 = from o in pp.context.exerDetail
                      where o.lid == tel1.id
                      orderby o.typeq
                      select o;

            if (q11 != null)
            {                      
                        led = q11.ToList<exerDetail>();
                      
                        foreach (exerDetail ed1 in led)
                        {
                    if (ed1.typeq == 0)
                    {
                        var q12 = from o in pp.context.mchoiceQues
                                  where o.id == ed1.qid
                                  select o;
                        mchoiceQues mcq = q12.First<mchoiceQues>();

                        numofquestion[0] = numofquestion[0] + 1;
                        totalscoreofques[0] = totalscoreofques[0] +(int) ed1.score ;
                        numofcon[(int)mcq.con - 1] = numofcon[(int)mcq.con - 1] + 1;
                        totalscoreofcon[(int)mcq.con - 1] = totalscoreofcon[(int)mcq.con - 1] + (int)ed1.score;
                        objectiveofcon[(int)mcq.objective - 1] = objectiveofcon[(int)mcq.objective - 1] + 1;
                        objectivescoreofofcon[(int)mcq.objective - 1] = objectivescoreofofcon[(int)mcq.objective - 1] + (int)ed1.score;
                        diffof[(int)mcq.objective - 1] = diffof[(int)mcq.objective - 1] + 1;
                        diffscore[(int)mcq.objective - 1]= diffscore[(int)mcq.objective - 1] + (int)ed1.score;


                    }
                      //////////////////   


                           
                            if (ed1.typeq == 1)
                            {
                               
                                var q12 = from o in pp.context.TFQues
                                          where o.id == ed1.qid
                                          select o;
                                TFQues mcq = q12.First<TFQues>();
                        numofquestion[1] = numofquestion[1] + 1;
                        totalscoreofques[1] = totalscoreofques[1] + (int)ed1.score;
                        numofcon[(int)mcq.con - 1] = numofcon[(int)mcq.con - 1] + 1;
                        totalscoreofcon[(int)mcq.con - 1] = totalscoreofcon[(int)mcq.con - 1] + (int)ed1.score;
                        objectiveofcon[(int)mcq.objective - 1] = objectiveofcon[(int)mcq.objective - 1] + 1;
                        objectivescoreofofcon[(int)mcq.objective - 1] = objectivescoreofofcon[(int)mcq.objective - 1] + (int)ed1.score;
                        diffof[(int)mcq.objective - 1] = diffof[(int)mcq.objective - 1] + 1;
                        diffscore[(int)mcq.objective - 1] = diffscore[(int)mcq.objective - 1] + (int)ed1.score;


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
                             
                        numofquestion[3] = numofquestion[3] + 1;
                        totalscoreofques[3] = totalscoreofques[3] + (int)ed1.score;
                        numofcon[(int)mcq.con - 1] = numofcon[(int)mcq.con - 1] + 1;
                        totalscoreofcon[(int)mcq.con - 1] = totalscoreofcon[(int)mcq.con - 1] + (int)ed1.score;
                        objectiveofcon[(int)mcq.objective - 1] = objectiveofcon[(int)mcq.objective - 1] + 1;
                        objectivescoreofofcon[(int)mcq.objective - 1] = objectivescoreofofcon[(int)mcq.objective - 1] + (int)ed1.score;
                        diffof[(int)mcq.objective - 1] = diffof[(int)mcq.objective - 1] + 1;
                        diffscore[(int)mcq.objective - 1] = diffscore[(int)mcq.objective - 1] + (int)ed1.score;



                    }
                            //////////////////////////////////////end3
                            if (ed1.typeq == 4)
                            {
                                

                                var q12 = from o in pp.context.AQues
                                          where o.id == ed1.qid
                                          select o;
                                AQues mcq = q12.First<AQues>();
                        numofquestion[4] = numofquestion[4] + 1;
                        totalscoreofques[4] = totalscoreofques[4] + (int)ed1.score;
                        numofcon[(int)mcq.con - 1] = numofcon[(int)mcq.con - 1] + 1;
                        totalscoreofcon[(int)mcq.con - 1] = totalscoreofcon[(int)mcq.con - 1] + (int)ed1.score;
                        objectiveofcon[(int)mcq.objective - 1] = objectiveofcon[(int)mcq.objective - 1] + 1;
                        objectivescoreofofcon[(int)mcq.objective - 1] = objectivescoreofofcon[(int)mcq.objective - 1] + (int)ed1.score;
                        diffof[(int)mcq.objective - 1] = diffof[(int)mcq.objective - 1] + 1;
                        diffscore[(int)mcq.objective - 1] = diffscore[(int)mcq.objective - 1] + (int)ed1.score;

                    }

                            ////end4

                        }

                //////////////////tostring//////////////////////

                st.AppendLine(tel.name + ":");
                st.AppendLine("选择题（" + numofquestion[0] + ")个     共（" + totalscoreofques[0] + ")分");
                st.AppendLine("判断题（" + numofquestion[1] + ")个     共（" + totalscoreofques[1] + ")分");
                st.AppendLine("填空题（" + numofquestion[2] + ")个     共（" + totalscoreofques[2] + ")分");
                st.AppendLine("简答题（" + numofquestion[3] + ")个     共（" + totalscoreofques[3] + ")分");
                st.AppendLine("分析题（" + numofquestion[4] + ")个     共（" + totalscoreofques[4] + ")分");
                //////////////////////////
                st.AppendLine("_________________________________________________");
                int j = 0;
                foreach (int ll in numofcon)
                {
                    st.AppendLine("第（"+(j+1)+")部分内容有（"+ numofcon[j]+")个题目共（" + totalscoreofcon[j]+"）分");
                    j++;
                }
                ///////////////////
                st.AppendLine("_________________________________________________");
                int i = 0;
                foreach (int jll in objectiveofcon)
                {
                    st.AppendLine("目标（" + (i + 1) + ")有（" + objectiveofcon[i] + ")个题目共（" + objectivescoreofofcon[i] + "）分");
                    i++;
                }
                //////////////////////////
                st.AppendLine("_________________________________________________");
                i = 0;
                foreach (int kk in diffof)
                {
                    st.AppendLine("难度（" + (i + 1) + ")有（" + diffof[i] + ")个题目共（" + diffscore[i] + "）分");
                    i++;
                }
                st.AppendLine("_________________________________________________");





                ////////////////////////////


            }


            return st.ToString();




           
        }



        ///

        ///exerl to word
        ///
        public  void toword(param p)
        {
            param pp = null;
            pp = p;


            saveFileDialog1.DefaultExt = ".docx";
            saveFileDialog1.Filter = "Word file|*.docx";
            String dirsave = null;
         
            exerL tel1 = pp.elword ;
            bool needkey = pp.keyneed;
            V_tea_course cc = pp.vdlword;
            List<int> numofquestion = new List<int>(5);
            for (int i = 0; i < 5; i++) numofquestion.Add(0);
            

            List<exerDetail> led = null;
            var q11 = from o in pp.context.exerDetail
                      where o.lid == tel1.id
                      orderby o.typeq
                      select o;
            if (q11 != null)
            {

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = saveFileDialog1.FileName.ToString();
                    dirsave = localFilePath;
                    led = q11.ToList<exerDetail>();
                    Spire.Doc.Document doc = new Document();
                    ParagraphStyle style1 = new ParagraphStyle(doc);
                    style1.Name = "titleStyle";
                    style1.CharacterFormat.Bold = true;
                    style1.CharacterFormat.TextColor = Color.Purple;
                    style1.CharacterFormat.FontName = "宋体";
                    style1.CharacterFormat.FontSize = 24f;
                    doc.Styles.Add(style1);
                    Section s = doc.AddSection();
                    Paragraph para2 = s.AddParagraph();
                    para2.AppendText(tel1.name);
                    para2.ApplyStyle("titleStyle");
                    led = q11.ToList<exerDetail>();
                    foreach (exerDetail ed1 in led)
                    {
                        if (ed1.typeq == 0)
                        {

                            var q12 = from o in pp.context.mchoiceQues
                                      where o.id == ed1.qid
                                      select o;
                            mchoiceQues mcq = q12.First<mchoiceQues>();
                            string keya = "";
                            if (mcq.answ == 1) keya = "A";
                            if (mcq.answ == 2) keya = "B";
                            if (mcq.answ == 3) keya = "C";
                            if (mcq.answ == 4) keya = "D";
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            byte[] a = mstream.ToArray();
                            // string sb = System.text.encoding.default.getstring(a);
                            string sb = System.Text.Encoding.Default.GetString(a);
                            var q13 = from o in pp.context.studAnsw
                                      where o.did == ed1.id
                                      select o;
                            string keystr = "";
                            DataObject myDataObject = new DataObject();
                            if (needkey)
                            {
                                keystr = "答案(" + keya + ")题号(" + mcq.id + ")章节(" + mcq.con + ")目标(" + mcq.objective + ")\n";
                                myDataObject.SetData(DataFormats.Rtf, keystr);
                                myDataObject.GetData(DataFormats.Rtf);
                            }
                            Paragraph para1 = s.AddParagraph();
                            if (numofquestion[0] == 0)
                            {
                                Paragraph para3 = s.AddParagraph();
                                para3.AppendText("一.选择题");
                            }
                            numofquestion[0] = numofquestion[0] + 1;
                            sb = numofquestion[0] + ". " + sb;
                            para1.AppendRTF(sb);
                            if(needkey)
                            para1.AppendRTF(myDataObject.GetData(DataFormats.Rtf).ToString());
                            TextSelection[] selections = doc.FindAllPattern(new System.Text.RegularExpressions.Regex("."));
                            TextRange range = null;
                            foreach (TextSelection selection in selections)
                            {
                                range = selection.GetAsOneRange();
                                Font ft = range.CharacterFormat.Font;
                                Font newft = new Font(ft.SystemFontName, 10f, ft.Style);
                                range.CharacterFormat.Font = newft;
                            }

                        }
                        if (ed1.typeq == 1)
                        {
                            var q12 = from o in pp.context.TFQues
                                      where o.id == ed1.qid
                                      select o;
                            TFQues mcq = q12.First<TFQues>();
                            string keya = "";
                            if ((bool)mcq.answ) keya = "True";
                            if (!(bool)mcq.answ) keya = "False";
                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            byte[] a = mstream.ToArray();
                            string sb = System.Text.Encoding.Default.GetString(a);
                            var q13 = from o in pp.context.studAnsw
                                      where o.did == ed1.id
                                      select o;
                            string keystr = "";
                            DataObject myDataObject = new DataObject();
                            if (needkey)
                            {
                                keystr = "答案(" + keya + ")题号(" + mcq.id + ")章节(" + mcq.con + ")目标(" + mcq.objective + ")\n";
                                myDataObject.SetData(DataFormats.Rtf, keystr);
                                myDataObject.GetData(DataFormats.Rtf);
                            }
                            Paragraph para1 = s.AddParagraph();
                            if (numofquestion[1] == 0)
                            {
                                Paragraph para3 = s.AddParagraph();
                                para3.AppendText("二.判断题");
                            }
                            numofquestion[1] = numofquestion[1] + 1;
                            sb = numofquestion[1] + ". " + sb;
                            para1.AppendRTF(sb);
                            if (needkey)
                                para1.AppendRTF(myDataObject.GetData(DataFormats.Rtf).ToString());
                            TextSelection[] selections = doc.FindAllPattern(new System.Text.RegularExpressions.Regex("."));
                            TextRange range = null;
                            foreach (TextSelection selection in selections)
                            {
                                range = selection.GetAsOneRange();
                                Font ft = range.CharacterFormat.Font;
                                Font newft = new Font(ft.SystemFontName, 10f, ft.Style);
                                range.CharacterFormat.Font = newft;
                            }

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
                            string keya = "";

                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            byte[] a = mstream.ToArray();
                            string sb = System.Text.Encoding.Default.GetString(a);
                            System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(mcq.answ, false);
                            byte[] b = mstream2.ToArray();
                            keya = System.Text.Encoding.Default.GetString(b);
                            var q13 = from o in pp.context.studAnsw
                                      where o.did == ed1.id
                                      select o;
                            string keystr = "";
                            DataObject myDataObject = new DataObject();
                            if (needkey)
                            {
                                keystr = "题号(" + mcq.id + ")章节(" + mcq.con + ")目标(" + mcq.objective + ")\n";
                                myDataObject.SetData(DataFormats.Rtf, keystr);
                                myDataObject.GetData(DataFormats.Rtf);
                            }
                            Paragraph para1 = s.AddParagraph();
                            if (numofquestion[3] == 0)
                            {
                                Paragraph para3 = s.AddParagraph();
                                para3.AppendText("三.简答题");
                            }
                            numofquestion[3] = numofquestion[3] + 1;
                            sb = numofquestion[3] + ". " + sb;
                            para1.AppendRTF(sb);
                            if (needkey)
                            {
                                para1.AppendRTF(keya);
                                para1.AppendRTF(myDataObject.GetData(DataFormats.Rtf).ToString());
                            }
                            TextSelection[] selections = doc.FindAllPattern(new System.Text.RegularExpressions.Regex("."));
                            TextRange range = null;
                            foreach (TextSelection selection in selections)
                            {
                                range = selection.GetAsOneRange();
                                Font ft = range.CharacterFormat.Font;
                                Font newft = new Font(ft.SystemFontName, 10f, ft.Style);
                                range.CharacterFormat.Font = newft;
                            }

                        }
                        //////////////////////////////////////end3

                        if (ed1.typeq == 4)
                        {
                            var q12 = from o in pp.context.AQues
                                      where o.id == ed1.qid
                                      select o;
                            AQues mcq = q12.First<AQues>();
                            string keya = "";

                            System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                            byte[] a = mstream.ToArray();
                            string sb = System.Text.Encoding.Default.GetString(a);
                            System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(mcq.answ, false);
                            byte[] b = mstream2.ToArray();
                            keya = System.Text.Encoding.Default.GetString(b);
                            var q13 = from o in pp.context.studAnsw
                                      where o.did == ed1.id
                                      select o;
                            string keystr = "";
                            DataObject myDataObject = new DataObject();
                            if (needkey)
                            {
                                keystr = "题号(" + mcq.id + ")章节(" + mcq.con + ")目标(" + mcq.objective + ")\n";
                                myDataObject.SetData(DataFormats.Rtf, keystr);
                                myDataObject.GetData(DataFormats.Rtf);
                            }
                            Paragraph para1 = s.AddParagraph();
                            if (numofquestion[4] == 0)
                            {
                                Paragraph para3 = s.AddParagraph();
                                para3.AppendText("三.简答题");
                            }
                            numofquestion[4] = numofquestion[4] + 1;
                            sb = numofquestion[4] + ". " + sb;
                            para1.AppendRTF(sb);
                            if (needkey)
                            {
                                para1.AppendRTF(keya);
                                para1.AppendRTF(myDataObject.GetData(DataFormats.Rtf).ToString());
                            }
                            TextSelection[] selections = doc.FindAllPattern(new System.Text.RegularExpressions.Regex("."));
                            TextRange range = null;
                            foreach (TextSelection selection in selections)
                            {
                                range = selection.GetAsOneRange();
                                Font ft = range.CharacterFormat.Font;
                                Font newft = new Font(ft.SystemFontName, 10f, ft.Style);
                                range.CharacterFormat.Font = newft;
                            }


                        }
               
                       ////end4

                   
                  
                        //////////////////savedoc//////////////////////


                        // richTextBox2.SaveFile(saveFileDialog1.FileName);
                        try
                        {

                            doc.SaveToFile(dirsave, Spire.Doc.FileFormat.Docx2013);
                        }
                        catch (Exception Err)
                        {
                            MessageBox.Show("WORD文件保存操作失败！" + Err.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }//end eachstudent

                    MessageBox.Show("文档生成结束！");




                }
            }


        }



        /////////////////////////////////////////////
        public List<featurehelp>  checkfeature(int type, param p)
        {
            List<featurehelp> result = new List<featurehelp>();
            var q1 = from o in p.context.featurehelp
                     where  o.type1 == type
                     select o;
            if (q1.Count() >0) result = q1.ToList<featurehelp>() ;


                    return result;

        }

        ////////////////////////////////////////////

        public static float degreeofsimilarity(string a,string b)
        {
            string cstr = null;
            string dstr = null;
            if (a.Length < b.Length)
            { cstr = a;dstr = b; }
            else
            { cstr = b; dstr = a; }
             float result=0;
            int la = cstr.Length;
            int lb = cstr.Length;
            int sum = 0;
            for (int i = 0; i < la; i++)
            {

                if (dstr.Contains(cstr[i])) sum = sum + 1;

            }

            result = sum / la;
            return result;

        }





        ///////////////////////////////////////////




    }//endclass
}
