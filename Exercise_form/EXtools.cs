﻿using System;
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

namespace Exercise_form
{
    class EXtools
    {


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
        public static string  toSummary(exerL tel,Course cc,param p)
        {
            string strsumm = null;

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







                    }
                              
                              


                           
                            if (ed1.typeq == 1)
                            {
                                this.richTextBox2.Rtf = null;
                                var q12 = from o in pp.context.TFQues
                                          where o.id == ed1.qid
                                          select o;
                                TFQues mcq = q12.First<TFQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                richTextBox1.Text = "";
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                // this.richTextBox2.AppendText(richTextBox1.Rtf);
                                //get student answervar


                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
                                          select o;

                                studAnsw tsa = null;
                                String key1 = "Question not being attemped"; ;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    tsa = q13.First<studAnsw>();
                                    if (tsa.answ2 == true) key1 = "True";
                                    if (tsa.answ2 == false) key1 = "False";
                                }
                                this.richTextBox2.AppendText("\n(" + key1 + ")_____________________________\n");
                                // this.richTextBox2.AppendText("\n_____________________________\n");
                                // Section s = doc.AddSection();
                                Paragraph para1 = s.AddParagraph();
                                para1.AppendRTF(richTextBox2.Rtf);

                            }

                            if (ed1.typeq == 2)
                            {



                            }

                            if (ed1.typeq == 3)
                            {
                                this.richTextBox2.Rtf = null;

                                var q12 = from o in pp.context.SQues
                                          where o.id == ed1.qid
                                          select o;
                                SQues mcq = q12.First<SQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                //this.richTextBox2.AppendText(richTextBox1.Rtf );
                                //get student answervar
                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
                                          select o;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    studAnsw tsa = q13.First<studAnsw>();
                                    //  String key1 = null;
                                    // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                    //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                    Byte[] mybyte = tsa.answ3;
                                    // MessageBox.Show(mybyte.Length.ToString());
                                    System.IO.MemoryStream ms = null;
                                    if (mybyte != null)
                                        ms = new System.IO.MemoryStream(mybyte);
                                    Image im = Image.FromStream(ms);
                                    int w = im.Size.Width;
                                    int h = im.Size.Height;
                                    //Section s = doc.AddSection();
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);
                                    //  para1.AppendPicture(im); 
                                    Paragraph para2 = s.AddParagraph();
                                    DocPicture picture = para2.AppendPicture(im);
                                    //设置图片大小         

                                    if (w < 450)
                                    {
                                        picture.Width = w;
                                        picture.Height = h;
                                    }
                                    else
                                    {
                                        picture.Width = 450;
                                        picture.Height = h * 450 / w;
                                        if (h * 450 / w > 450) picture.Height = 450;

                                    }
                                }
                                else
                                {
                                    this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                    this.richTextBox2.AppendText("\n_____________________________\n");
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);

                                }

                            }
                            //////////////////////////////////////end3
                            if (ed1.typeq == 4)
                            {
                                this.richTextBox2.Rtf = null;

                                var q12 = from o in pp.context.AQues
                                          where o.id == ed1.qid
                                          select o;
                                AQues mcq = q12.First<AQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                //this.richTextBox2.AppendText(richTextBox1.Rtf );
                                //get student answervar
                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
                                          select o;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    studAnsw tsa = q13.First<studAnsw>();
                                    //  String key1 = null;
                                    // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                    //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                    Byte[] mybyte = tsa.answ3;
                                    // MessageBox.Show(mybyte.Length.ToString());
                                    System.IO.MemoryStream ms = null;
                                    if (mybyte != null)
                                        ms = new System.IO.MemoryStream(mybyte);
                                    Image im = Image.FromStream(ms);
                                    int w = im.Size.Width;
                                    int h = im.Size.Height;
                                    //Section s = doc.AddSection();
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);
                                    //  para1.AppendPicture(im); 
                                    Paragraph para2 = s.AddParagraph();
                                    DocPicture picture = para2.AppendPicture(im);
                                    //设置图片大小         

                                    if (w < 450)
                                    {
                                        picture.Width = w;
                                        picture.Height = h;
                                    }
                                    else
                                    {
                                        picture.Width = 450;
                                        picture.Height = h * 450 / w;
                                        if (h * 450 / w > 450) picture.Height = 450;

                                    }
                                }
                                else
                                {
                                    this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                    this.richTextBox2.AppendText("\n_____________________________\n");
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);

                                }

                            }

                            ////end4

                        }

                        //////////////////savedoc//////////////////////

                        String stsavepath = null;
                        stsavepath = dirsave + @"/" + vst.stid + vst.stname + ".docx";

                        // richTextBox2.SaveFile(saveFileDialog1.FileName);
                        try
                        {

                            doc.SaveToFile(stsavepath, FileFormat.Docx2013);
                        }
                        catch (Exception Err)
                        {
                            MessageBox.Show("WORD文件保存操作失败！" + Err.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }//end eachstudent

                    MessageBox.Show("文档生成结束！");




                }
            }






            return strsumm;
        }



        ///

        ///exerl to word
        ///
        public static void toword(exerL tel)
        {/*


            saveFileDialog1.DefaultExt = ".docx";
            saveFileDialog1.Filter = "Word file|*.docx";
            String dirsave = null;
            List<View_student> tlvst = null;
            exerL tel1 = tel;

            var q5 = from o in pp.context.View_student
                     where o.cid == tci.classid
                     select o;

            if (q5.Count<View_student>() > 0) tlvst = q5.ToList<View_student>();
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
                    dirsave = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));

                    foreach (View_student vst in tlvst)
                    {

                        richTextBox1.Text = "";
                        this.richTextBox2.Text = "";
                        led = q11.ToList<exerDetail>();
                        Document doc = new Document();
                        Section s = doc.AddSection();
                        led = q11.ToList<exerDetail>();
                        foreach (exerDetail ed1 in led)
                        {
                            if (ed1.typeq == 0)
                            {
                                this.richTextBox2.Rtf = null;
                                var q12 = from o in pp.context.mchoiceQues
                                          where o.id == ed1.qid
                                          select o;
                                mchoiceQues mcq = q12.First<mchoiceQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                richTextBox1.Text = "";
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
                                          select o;
                                studAnsw tsa = null;
                                String key1 = "Question not being attemped"; ;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    tsa = q13.First<studAnsw>();
                                    if (tsa.answ1 == 0) key1 = "A";
                                    if (tsa.answ1 == 1) key1 = "B";
                                    if (tsa.answ1 == 2) key1 = "C";
                                    if (tsa.answ1 == 3) key1 = "D";
                                }
                                this.richTextBox2.AppendText("\n(" + key1 + ")_____________________________\n");
                                // this.richTextBox2.AppendText("_____________________________\n");

                                Paragraph para1 = s.AddParagraph();
                                para1.AppendRTF(richTextBox2.Rtf);


                            }
                            if (ed1.typeq == 1)
                            {
                                this.richTextBox2.Rtf = null;
                                var q12 = from o in pp.context.TFQues
                                          where o.id == ed1.qid
                                          select o;
                                TFQues mcq = q12.First<TFQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                richTextBox1.Text = "";
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                // this.richTextBox2.AppendText(richTextBox1.Rtf);
                                //get student answervar


                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
                                          select o;

                                studAnsw tsa = null;
                                String key1 = "Question not being attemped"; ;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    tsa = q13.First<studAnsw>();
                                    if (tsa.answ2 == true) key1 = "True";
                                    if (tsa.answ2 == false) key1 = "False";
                                }
                                this.richTextBox2.AppendText("\n(" + key1 + ")_____________________________\n");
                                // this.richTextBox2.AppendText("\n_____________________________\n");
                                // Section s = doc.AddSection();
                                Paragraph para1 = s.AddParagraph();
                                para1.AppendRTF(richTextBox2.Rtf);

                            }

                            if (ed1.typeq == 2)
                            {



                            }

                            if (ed1.typeq == 3)
                            {
                                this.richTextBox2.Rtf = null;

                                var q12 = from o in pp.context.SQues
                                          where o.id == ed1.qid
                                          select o;
                                SQues mcq = q12.First<SQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                //this.richTextBox2.AppendText(richTextBox1.Rtf );
                                //get student answervar
                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
                                          select o;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    studAnsw tsa = q13.First<studAnsw>();
                                    //  String key1 = null;
                                    // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                    //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                    Byte[] mybyte = tsa.answ3;
                                    // MessageBox.Show(mybyte.Length.ToString());
                                    System.IO.MemoryStream ms = null;
                                    if (mybyte != null)
                                        ms = new System.IO.MemoryStream(mybyte);
                                    Image im = Image.FromStream(ms);
                                    int w = im.Size.Width;
                                    int h = im.Size.Height;
                                    //Section s = doc.AddSection();
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);
                                    //  para1.AppendPicture(im); 
                                    Paragraph para2 = s.AddParagraph();
                                    DocPicture picture = para2.AppendPicture(im);
                                    //设置图片大小         

                                    if (w < 450)
                                    {
                                        picture.Width = w;
                                        picture.Height = h;
                                    }
                                    else
                                    {
                                        picture.Width = 450;
                                        picture.Height = h * 450 / w;
                                        if (h * 450 / w > 450) picture.Height = 450;

                                    }
                                }
                                else
                                {
                                    this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                    this.richTextBox2.AppendText("\n_____________________________\n");
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);

                                }

                            }
                            //////////////////////////////////////end3
                            if (ed1.typeq == 4)
                            {
                                this.richTextBox2.Rtf = null;

                                var q12 = from o in pp.context.AQues
                                          where o.id == ed1.qid
                                          select o;
                                AQues mcq = q12.First<AQues>();
                                System.IO.MemoryStream mstream = new System.IO.MemoryStream(mcq.question, false);
                                this.richTextBox2.AppendText("\n_____________________________\n");
                                this.richTextBox2.LoadFile(mstream, RichTextBoxStreamType.RichText);
                                //this.richTextBox2.AppendText(richTextBox1.Rtf );
                                //get student answervar
                                var q13 = from o in pp.context.studAnsw
                                          where o.stid == vst.stid && o.did == ed1.id
                                          select o;
                                if (q13.Count<studAnsw>() > 0)
                                {
                                    studAnsw tsa = q13.First<studAnsw>();
                                    //  String key1 = null;
                                    // System.IO.MemoryStream mstream2 = new System.IO.MemoryStream(tsa.answ3 , false);
                                    //this.richTextBox1.LoadFile(mstream2, RichTextBoxStreamType.RichText);
                                    Byte[] mybyte = tsa.answ3;
                                    // MessageBox.Show(mybyte.Length.ToString());
                                    System.IO.MemoryStream ms = null;
                                    if (mybyte != null)
                                        ms = new System.IO.MemoryStream(mybyte);
                                    Image im = Image.FromStream(ms);
                                    int w = im.Size.Width;
                                    int h = im.Size.Height;
                                    //Section s = doc.AddSection();
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);
                                    //  para1.AppendPicture(im); 
                                    Paragraph para2 = s.AddParagraph();
                                    DocPicture picture = para2.AppendPicture(im);
                                    //设置图片大小         

                                    if (w < 450)
                                    {
                                        picture.Width = w;
                                        picture.Height = h;
                                    }
                                    else
                                    {
                                        picture.Width = 450;
                                        picture.Height = h * 450 / w;
                                        if (h * 450 / w > 450) picture.Height = 450;

                                    }
                                }
                                else
                                {
                                    this.richTextBox2.AppendText("\n(" + "Question not being attemped" + ")");
                                    this.richTextBox2.AppendText("\n_____________________________\n");
                                    Paragraph para1 = s.AddParagraph();
                                    para1.AppendRTF(richTextBox2.Rtf);

                                }

                            }

                            ////end4

                        }

                        //////////////////savedoc//////////////////////

                        String stsavepath = null;
                        stsavepath = dirsave + @"/" + vst.stid + vst.stname + ".docx";

                        // richTextBox2.SaveFile(saveFileDialog1.FileName);
                        try
                        {

                            doc.SaveToFile(stsavepath, FileFormat.Docx2013);
                        }
                        catch (Exception Err)
                        {
                            MessageBox.Show("WORD文件保存操作失败！" + Err.Message, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }//end eachstudent

                    MessageBox.Show("文档生成结束！");




                }
            }

*/
        }

        










    }//endclass
}
