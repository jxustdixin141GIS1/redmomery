using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace redmomery.command
{
   public  class  createlog
    {
       public static void createlogs(string text)
      {
         //开始创建文件地址
          string path = @"D://题库系统//redMomery//调试//redmomerylog"+"_"+DateTime.Now.ToString("yyyy_MM_dd_hh_mm")+".txt";
          FileStream fs = new FileStream(path,FileMode.OpenOrCreate,FileAccess.ReadWrite);
          if (fs.Length > 0)
          {
              fs.Position = fs.Length;
          }
          StreamWriter sw = new StreamWriter(fs);
          string context = "\n\r" + DateTime.Now.ToString() + ":" + text + "\n\r";
          try
          {
              sw.WriteLine(context);
          }
          catch
          {

          }
          finally {
              sw.Close();
              fs.Close();
          }
      }
       public static void createtxt(string text,string filename)
       {
           //开始创建文件地址
           string path = @"D://题库系统//redMomery//调试//" + filename + ".txt";
           FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
           if (fs.Length > 0)
           {
               fs.Position = fs.Length;
           }
           StreamWriter sw = new StreamWriter(fs);
           string context = text + "\r\n";
           try
           {
               sw.WriteLine(context);
           }
           catch
           {

           }
           finally
           {
               sw.Close();
               fs.Close();
           }
       }

       public static string readTextFrompath(string path)
       {
           FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
           BinaryReader br = new BinaryReader(fs);
           string context = string.Empty;
           try
           {
               byte[] buffers;
               if (br.BaseStream.Length > int.MaxValue)
               {
                   //这个需要进行多次扩充数据才行
                   buffers = new byte[br.BaseStream.Length];
                   for (long i = 0; i < br.BaseStream.Length; i++)
                   {
                       buffers[i] = br.ReadByte();
                   }
               }
               else
               {
                   buffers = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length));
               }
               context = GetText(buffers);
           }
           catch
           {

           }
           finally
           {
               br.Close();
               fs.Close();
           }
           return context;
       }

       public static string GetText(byte[] buff)
       {
           string strReslut = string.Empty;
           if (buff.Length > 3)
           {
               if (buff[0] == 239 && buff[1] == 187 && buff[2] == 191)
               {// utf-8
                   strReslut = Encoding.UTF8.GetString(buff);
               }
               else if (buff[0] == 254 && buff[1] == 255)
               {// big endian unicode
                   strReslut = Encoding.BigEndianUnicode.GetString(buff);
               }
               else if (buff[0] == 255 && buff[1] == 254)
               {// unicode
                   strReslut = Encoding.Unicode.GetString(buff);
               }
               else if (isUtf8(buff))
               {// utf-8
                   strReslut = Encoding.UTF8.GetString(buff);
               }
               else
               {// ansi
                   strReslut = Encoding.Default.GetString(buff);
               }
           }
           return strReslut;
       }
       // 110XXXXX, 10XXXXXX
       // 1110XXXX, 10XXXXXX, 10XXXXXX
       // 11110XXX, 10XXXXXX, 10XXXXXX, 10XXXXXX
       private static bool isUtf8(byte[] buff)
       {
           for (int i = 0; i < buff.Length; i++)
           {
               if ((buff[i] & 0xE0) == 0xC0) // 110x xxxx 10xx xxxx
               {
                   if ((buff[i + 1] & 0x80) != 0x80)
                   {
                       return false;
                   }
               }
               else if ((buff[i] & 0xF0) == 0xE0) // 1110 xxxx 10xx xxxx 10xx xxxx
               {
                   if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80)
                   {
                       return false;
                   }
               }
               else if ((buff[i] & 0xF8) == 0xF0) // 1111 0xxx 10xx xxxx 10xx xxxx 10xx xxxx
               {
                   if ((buff[i + 1] & 0x80) != 0x80 || (buff[i + 2] & 0x80) != 0x80 || (buff[i + 3] & 0x80) != 0x80)
                   {
                       return false;
                   }
               }
           }
           return true;
       }
       // news.sohu.com
       private static bool isGBK(byte[] buff)
       {
           return false;
       }

    }
}
