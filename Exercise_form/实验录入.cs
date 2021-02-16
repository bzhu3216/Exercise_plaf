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
using System.IO;

namespace Exercise_form
{
    public partial class shiya : Form
    {
        param pp;
        List<V_tea_course> lvtc = null;
        int cid = -1;
        String fileDirectory = "";
        String attaDirectory = "";
        List<exp_q> LTF = new List<exp_q>();
        public shiya()
        {
            InitializeComponent();

        }
        public shiya(param p)
        {
            InitializeComponent();
            pp = p;
            lvtc = pp.ltea_c;
        }



        private void splitContainer1_Panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }


        private void button5_Click_1(object sender, EventArgs e)
        {
            ////////////////////////////
            fileDirectory = "";

            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Word97-2003 files(*.doc)|*.doc|Word2007-2010 files (*.docx)|*.docx|All files (*.*)|*.*";

            dialog.Title = "Select a DOC file";

            dialog.Multiselect = false;
            dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\Data");

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)

            {

                try

                {
                    //Load DOC document from file.
                    this.docViewer1.LoadFromFile(dialog.FileName);
                    fileDirectory = dialog.FileName;
                    button1.Enabled = true;
                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }


            ///////////////////////////
        }

        private void toppanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void shiya_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = lvtc;
            comboBox1.ValueMember = "CourseName";
            comboBox1.Text = "";
            comboBox2.Items.Clear();
            // comboBox3.Items.Clear();
            checkedListBox1.Items.Clear();
            comboBox4.Items.Clear();


        }
        private void loaditems()
        {
            LTF.Clear();
            listBox1.Items.Clear();
            var questionQuery3 = from o in pp.context.exp_q
                                  where (o.courseid == cid)
                                  orderby  o.con
                                  select o;

            if (questionQuery3.Count<exp_q>() > 0)
            {
                LTF = questionQuery3.ToList<exp_q>();
            }
            foreach (exp_q dd in LTF)
            {
                listBox1.Items.Add(dd.exname );

            }

            
            


            }

        private void loadcom()
        {
            comboBox2.Items.Clear();
            checkedListBox1.Items.Clear();
            comboBox4.Items.Clear();
            int numc = lvtc[comboBox1.SelectedIndex].numcontent;
            int numo = lvtc[comboBox1.SelectedIndex].numobjective;
            int numd = lvtc[comboBox1.SelectedIndex].diff;
            cid = (int)lvtc[comboBox1.SelectedIndex].couseid;
            for (int i = 1; i <= numc; i++)
            {
                comboBox2.Items.Add(i.ToString());
            }
            for (int i = 1; i <= numo; i++)
            {
                //comboBox3.Items.Add(i.ToString());
                checkedListBox1.Items.Add(i.ToString());
            }

            for (int i = 1; i <= numd; i++)
            {
                comboBox4.Items.Add(i.ToString());
            }



        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            loadcom();
            loaditems();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool flagupdata = checkBox1.Checked;      
             Exercise_form.ServiceReference1.exp_q mcq = new exp_q();
            //
            if (flagupdata)
            {
                Exercise_form.ServiceReference1.exp_q mcq2 = (exp_q)LTF[listBox1.SelectedIndex];
                // if (!comboBox2.Text.Equals("") && !comboBox4.Text.Equals("") && !fileDirectory.Equals("") && !textBox2.Text.Equals(""))
                if (!comboBox2.Text.Equals("") && !comboBox4.Text.Equals("")&& !(mcq2==null)&& !textBox2.Text.Equals(""))
                {
                    mcq2.con = Convert.ToInt16(comboBox2.Text);
                    mcq2.diff = Convert.ToInt16(comboBox4.Text);
                   // mcq.courseid = cid;
                    int objectivecount = checkedListBox1.CheckedItems.Count;
                    string strobj = "";
                    for (int i = 0; i < objectivecount; i++) { strobj = strobj + "|" + checkedListBox1.CheckedItems[i].ToString(); }
                    mcq2.objective = strobj;
                    mcq2.exname = textBox2.Text;
                    //上传实验内容和附件
                    long intLength = 0; //获取文件内容的长度                 
                    byte[] bytContent = null; //定义内容数组                                   

                    //建立要输入的文件流 
                    System.IO.FileStream fs = null;
                    //建立二进制读取 
                    System.IO.BinaryReader br = null;
                    long intLength2 = 0; //获取文件内容的长度                 
                    byte[] bytContent2 = null; //定义内容数组                                   

                    //建立要输入的文件流 
                    System.IO.FileStream fs2 = null;
                    //建立二进制读取 
                    System.IO.BinaryReader br2 = null;
                    try
                    {
                        if (!attaDirectory.Equals(""))
                        {
                            fs2 = new FileStream(attaDirectory, System.IO.FileMode.Open);
                            br2 = new BinaryReader((Stream)fs2);
                            intLength2 = fs2.Length;
                            bytContent2 = new byte[intLength2];
                            bytContent2 = br2.ReadBytes((int)intLength2);

                            mcq2.attachment = bytContent2;
                            String[] fnames = attaDirectory.Split('\\');
                            String filenamestr = fnames[fnames.Length - 1];
                            mcq2.attachmentname  = filenamestr;



                        }
                        if (!fileDirectory.Equals(""))
                        { 
                            fs = new FileStream(fileDirectory, System.IO.FileMode.Open);
                        br = new BinaryReader((Stream)fs);
                        intLength = fs.Length;
                        bytContent = new byte[intLength];
                        bytContent = br.ReadBytes((int)intLength);
                        mcq2.expdoc = bytContent;
                         String[] fnames = fileDirectory.Split('\\');
                        String filenamestr = fnames[fnames.Length - 1] ;
                            mcq2.docfilename = filenamestr;
                        //        MessageBox.Show(fileDirectory+"   "+ filenamestr);
                        if ((intLength + intLength2) < pp.maxsize)
                        {
                            pp.context.UpdateObject(mcq2);
                            pp.context.SaveChanges();
                            textBox2.Text = "";
                            button1.Enabled = false;

                        }
                        }
                        loaditems();
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show("文件打开或保存异常！" + ee.Message);
                    }
                    //

                }
                else
                    MessageBox.Show("输入不完整！");

                return;
            }




            //
            
                     
            if(!comboBox2.Text.Equals("")&& !comboBox4.Text.Equals("")&& !fileDirectory.Equals("")&&!textBox2.Text.Equals(""))
            {  
            mcq.con = Convert.ToInt16(comboBox2.Text);
            mcq.diff = Convert.ToInt16(comboBox4.Text);
                mcq.courseid = cid;
            int objectivecount = checkedListBox1.CheckedItems.Count;
            string strobj = "";
            for (int i = 0; i < objectivecount; i++) { strobj = strobj + "|" + checkedListBox1.CheckedItems[i].ToString(); }
                mcq.objective = strobj;
                mcq.exname = textBox2.Text;
                //上传实验内容和附件
                long intLength = 0; //获取文件内容的长度                 
                byte[] bytContent = null; //定义内容数组                                   
                
                //建立要输入的文件流 
                System.IO.FileStream fs = null;
                //建立二进制读取 
                System.IO.BinaryReader br = null;
                long intLength2 = 0; //获取文件内容的长度                 
                byte[] bytContent2 = null; //定义内容数组                                   

                //建立要输入的文件流 
                System.IO.FileStream fs2 = null;
                //建立二进制读取 
                System.IO.BinaryReader br2 = null;
                try
                { if (!attaDirectory.Equals(""))
                    {
                        fs2 = new FileStream(attaDirectory, System.IO.FileMode.Open);
                        br2 = new BinaryReader((Stream)fs2);
                        intLength2 = fs2.Length;
                        bytContent2 = new byte[intLength2];
                        bytContent2 = br2.ReadBytes((int)intLength2);                      
                       
                            mcq.attachment = bytContent2;
                        
                        String[] fnames2 = attaDirectory.Split('\\');
                        String filenamestr2 = fnames2[fnames2.Length - 1];
                        mcq.attachmentname = filenamestr2;


                    }
                    fs = new FileStream(fileDirectory, System.IO.FileMode.Open);
                    br = new BinaryReader((Stream)fs);
                    intLength = fs.Length;
                    bytContent = new byte[intLength];
                    bytContent = br.ReadBytes((int) intLength);
                    mcq.expdoc = bytContent;
                    String[] fnames = fileDirectory.Split('\\');
                    String filenamestr = fnames[fnames.Length - 1];
                    mcq.docfilename = filenamestr;
                    if ((intLength +intLength2) < pp.maxsize)
                    {
                        pp.context.AddToexp_q(mcq);                       
                        pp.context.SaveChanges();
                        textBox2.Text = "";
                        button1.Enabled = false;
                        
                    }

                    loaditems();
                }
                catch(Exception ee)
                {
                    MessageBox.Show("文件打开或保存异常！"+ee.Message);
                }
                //

            }
            else
                MessageBox.Show("输入不完整！");

        }

        private void docViewer1_DocumentOpened(object sender, EventArgs args)
        {
           
        }

        private void docViewer1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            attaDirectory = "";
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "RAR files (*.rar)|*.rar";

            dialog.Title = "如果需要请上传附件";

            dialog.Multiselect = false;
            dialog.InitialDirectory = System.IO.Path.GetFullPath(@"..\..\..\..\..\..\Data");

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)

            {

                try

                {
                    //Load DOC document from file.
                    attaDirectory = dialog.FileName;

                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             textBox2.Text= "";
            int lindex = listBox1.SelectedIndex;
            if (lindex >= 0)
            {
                try
                {
                    docViewer1.LoadFromStream(new MemoryStream((((exp_q)LTF[lindex]).expdoc)), Spire.Doc.FileFormat.Docx);
                }
                catch
                {
                    try { docViewer1.LoadFromStream(new MemoryStream((((exp_q)LTF[lindex]).expdoc)), Spire.Doc.FileFormat.Doc); }
                    catch { MessageBox.Show("文件格式不对"); }

                }
            
            }
            comboBox2.Text = ((exp_q)LTF[lindex]).con.ToString();
            comboBox4.Text=((exp_q)LTF[lindex]).diff.ToString();
            textBox1.Text = ((exp_q)LTF[lindex]).idexp.ToString();
            String strobjs = ((exp_q)LTF[lindex]).objective;
            String[]  strobj = strobjs.Split('|');

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);

            }

                for (int i = 0; i < strobj.Length; i++)
            {
                if (!strobj[i].Equals(""))
                    checkedListBox1.SetItemChecked(int.Parse(strobj[i])-1,true);
                         

            }





        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) button1.Enabled = true;

        }
    }
}
