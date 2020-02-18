using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Exercise_student
{
    class Piczip
    {


        public static bool CompressImage(string sFile, string dFile, int flag = 90, int size = 120, bool sfsc = true)
        {
            Image iSource = Image.FromFile(sFile);
            System.Drawing.Imaging.ImageFormat tFormat = iSource.RawFormat;
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(sFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                FileInfo firstFileInfo2 = new FileInfo(dFile);
                if (firstFileInfo2 != null) firstFileInfo2.Delete();

                firstFileInfo.CopyTo(dFile);
                return true;
            }

            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            System.Drawing.Imaging.EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                    FileInfo fi = new FileInfo(dFile);
                  //  MessageBox.Show(fi.Length.ToString());
                    if (fi.Length > 1024 * size)
                    {
                        if (flag > 20) flag = flag - 20;
                        else
                            flag = 5;
                        CompressImage(sFile, dFile, flag, size, false);
                    }
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }
        ////////////////////

        public static  System.IO.MemoryStream CompressImage2(string sFile,  int flag, int size = 300, bool sfsc = true)
        {
            System.IO.FileStream fs = new System.IO.FileStream(sFile, FileMode.Open, FileAccess.Read);
            System.IO.MemoryStream fs2 = new System.IO.MemoryStream();
            Image iSource = Image.FromFile(sFile);
            System.Drawing.Imaging.ImageFormat tFormat = iSource.RawFormat;
            //如果是第一次调用，原始图像的大小小于要压缩的大小，则直接复制文件，并且返回true
            FileInfo firstFileInfo = new FileInfo(sFile);
            if (sfsc == true && firstFileInfo.Length < size * 1024)
            {
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                fs.Seek(0, SeekOrigin.Begin);
                fs2.Write(bytes,0, bytes.Length );
                //firstFileInfo.CopyTo(dFile);
                return fs2;
            }

            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            System.Drawing.Imaging.EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                   
                    ob.Save(fs2, jpegICIinfo, ep);//dFile是压缩后的新路径
                  //  FileInfo fi = new FileInfo(dFile);
                   // if (fs.Length > 1024 * size)
                  //  {
                  //      flag = flag - 10;
                    //    CompressImage2(sFile,  flag, size, false);
                   // }
                }
                else
                {
                    ob.Save(fs2, tFormat);
                }
                return fs2;
            }
            catch
            {
                return null;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }


        ////////////////////////


    }
}
