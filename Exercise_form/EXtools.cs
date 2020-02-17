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
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using System.Diagnostics;
//using Exercise_plaf.Servicestu;
using System.Windows.Forms;
using Exercise_form.ServiceReference1;

namespace Exercise_form
{
    class EXtools
    {


        ///////////////start score

        private static void   toScore(classinfo tci, exerL tel)
        {
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

        }











        //end  score
















    }//endclass
}
