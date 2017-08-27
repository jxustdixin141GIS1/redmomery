using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using redmomery.DAL;
using redmomery.Model;
using System.Net;
using System.Data.Spatial;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
namespace ConsoleApplication1test
{
   public class abandon
    {
       //public static void parseJsonstrin(string example)
       //{
       //    //开始进行程序的解析,这里采用堆栈的方案处理
       //    example = example.Replace("\"", "");
       //    example = example.Replace(" ", "");
       //    List<redmomery.Model.objectkeyname> list = new List<objectkeyname>();
       //    List<object> temp = new List<object>();
       //    //首先提取括号中的内容
       //    string blift = "{", bright = "}", mmid = ":", bmid = ","; ;
       //    for (int i = 0; i < example.Length; )
       //    {
       //        if (example[i].ToString() == "{")
       //        {
       //            temp.Add(example[i]);
       //            i++;
       //            continue;
       //        }
       //        if (example[i].ToString() == ",")
       //        {
       //            i++;
       //            continue;
       //        }
       //        if (example[i].ToString() == ":")
       //        {
       //            i++;
       //            continue;
       //        }
       //        if (example[i].ToString() == "}")
       //        {
       //            i++;
       //            continue;
       //        }
       //        //若是读取的不是{},:就代表这是字符串字段
       //        //开始进行字符判别，

       //        if (example[i - 1].ToString() != ":")//若不是：就代表这是名称
       //        {
       //            //开始提取名称进行压入栈中
       //            int namend = example.IndexOf(':', i);
       //            string tempstring = string.Empty; if (namend == -1) { i++; continue; }
       //            tempstring = example.Substring(i, namend - i);
       //            temp.Add(tempstring);
       //        }
       //        if (example[i - 1].ToString() == ":")
       //        {
       //            //表示这是一个键值，若是键值的话，就需要判别是不是复合键值

       //        }
       //        i++;
       //    }
       //}
    }
   //public class PanGuC
   //{
   //    string TimeText = string.Empty;
   //    List<string> TimeList = new List<string>();
   //    //时间词典的生成 ，进行资料的拆分
   //   static int count_index= 0;
   //   int listline = 0;
   //   int templine = 0;
   //    public void createTimeDircr()
   //    {

   //        //时间词语：1800-2020年
   //        Console.WriteLine("开始进行日期生成");
   //        string year_ = "年";
   //        string month_ = "月";
   //        string day = "日";
   //        //开始构建对应的日期文件
   //        //从1800-2020年

   //        #region
   //        for (int y = 1800; y <= 2021; y++)
   //        {
   //            string yeartext = y.ToString() + "年";
   //            TimeList.Add(yeartext);
   //            TimeList.Add(N_TO_C_convert(yeartext, false));
   //            for (int m = 1; m <= 12; m++)
   //            {
   //                string monthtext = m.ToString() + "月";
   //                TimeList.Add(monthtext);
   //                TimeList.Add(yeartext + monthtext);                    
   //                for (int d = 1; d <= 31; d++)
   //                {
   //                    string daytext = d.ToString() + "日";
   //                    TimeList.Add(daytext);
   //                    TimeList.Add(yeartext + monthtext + daytext);
   //                    if ((m == 4 || m == 6 || m == 9 || m == 11) && d == 30)
   //                    {
   //                        break;
   //                    }
   //                    if (m == 2)
   //                    {
   //                        if ((y % 4 == 0 && y % 100 != 0) || (y % 400 == 0))
   //                        {
   //                            if (d == 29)
   //                            {
   //                                break;
   //                            }
   //                        }
   //                        else
   //                        {
   //                            if (d == 28)
   //                            {
   //                                break;
   //                            }
   //                        }
   //                    }
   //                }
   //            }
   //        }
   //        #endregion
   //        //开始进行数据输出,鉴于速度太慢所以采用多线程进行处理
   //        Console.WriteLine("日期生成结束,为了多线程进行二次处理,进行数据二次转存");
   //        int leng_lists = 50000;
   //        int count_thread = (TimeList.Count / leng_lists) + 1;
   //        List<string>[] Times=new List<string>[count_thread];
   //        int copyindex = 0;
   //        for (int i = 0; i < Times.Length; i++)
   //        {
   //            if (i == (Times.Length - 1))
   //            {
   //                Times[i] = TimeList.GetRange(copyindex, TimeList.Count - copyindex);
   //            }
   //            else
   //            {
   //                Times[i] = TimeList.GetRange(copyindex, leng_lists);
   //            }
   //            copyindex = copyindex + leng_lists;
   //        }
   //        int topiccount = TimeList.Count;
   //        TimeList = new List<string>();
   //        //开始进行线程处理,分别针对文件进行写入处理
   //        for (int i = 0; i < Times.Length; i++)
   //        {
   //            Thread t1 = new Thread(new ParameterizedThreadStart(Converto_TimeText));
   //            t1.Start(Times[i] as object);
   //        }
   //        while (listline != topiccount)
   //        {
   //            Console.Write(topiccount.ToString() + ":" + templine.ToString() +  " ,  " + listline.ToString()  + "\r");
   //        }
   //        Console.WriteLine(topiccount.ToString() + ":" + templine.ToString() + " ,  " + listline.ToString());
   //        Console.WriteLine();
   //        Console.WriteLine("进行数据的写入");

   //        Console.WriteLine(topiccount.ToString() + ":" + templine.ToString() +  " ,  " + listline.ToString() );
   //        Console.WriteLine();
   //        count_index = 0;
   //    }
   //    public  void Converto_TimeText(object Lists)
   //    {
   //        count_index++;
   //        int signindex = count_index;
   //        List<string> list = Lists as List<string>;
   //       // string temp=string.Empty;
   //        StringBuilder temp = new StringBuilder();
   //        for (int i = 0; i < list.Count; i++)
   //        { 
   //            temp.AppendLine(list[i].ToString());   
   //            templine++;
   //            if (i % 10000 == 0)
   //            {
   //                lock (this)
   //                {
   //                redmomery.command.createlog.createtxt(temp.ToString(), "公元时间词典纯数字");
   //                temp.Clear();
   //               }
   //            }
   //        }
   //        lock (this)
   //        {
   //            redmomery.command.createlog.createtxt(temp.ToString(), "公元时间词典");
   //            temp.Clear();
   //        }
   //        listline += list.Count;
   //    }
   //    public string N_Chinese_reflex(int nyear, bool Loma)
   //    {
   //        switch (nyear)
   //        {
   //            case 0: return "零";
   //            case 1: return Loma ? "元" : "一";//在中文中一月份还可以成为元月
   //            case 2: return "二";
   //            case 3: return "三";
   //            case 4: return "四";
   //            case 5: return "五";
   //            case 6: return "六";
   //            case 7: return "七";
   //            case 8: return "八";
   //            case 9: return "九";
   //            default: return "None";
   //        }
   //    }
   //    public int C_N_Chinese_reflex(string cyear)
   //    {
   //        switch (cyear)
   //        {
   //            case "零": return 0;
   //            case "一": return 1;
   //            case "元": return 1;
   //            case "二": return 2;
   //            case "三": return 3;
   //            case "四": return 4;
   //            case "五": return 5;
   //            case "六": return 6;
   //            case "七": return 7;
   //            case "八": return 8;
   //            case "九": return 9;
   //            default: return -1;
   //        }

   //    }
   //    public string N_TO_C_convert(string text, bool loma)
   //    {
   //        char[] temps = text.ToCharArray();
   //        for (int i = 0; i < temps.Length; i++)
   //        {
   //            if (char.IsNumber(temps[i]))
   //            {
   //                temps[i] = N_Chinese_reflex(int.Parse(temps[i].ToString()), loma).ToCharArray()[0];
   //            }
   //            else { }
   //        }
   //        return new string(temps);
   //    }
   //    public string mingguojinain(string cyear)
   //    {
   //        //这个方法用来处理民国纪元方法
   //        string result = string.Empty;
   //        int st = cyear.IndexOf("民国") + 2;
   //        if (st - 2 >= 0 && cyear.IndexOf("年", st) >= 0)
   //        {
   //            char[] chars = cyear.ToCharArray();
   //            int yend = cyear.IndexOf("年", st);
   //            bool isend = true;
   //            int whilestartindex = st;
   //            while (isend)
   //            {
   //                if (whilestartindex > yend)
   //                {
   //                    isend = false;
   //                }
   //                if (char.IsNumber(chars[whilestartindex]))
   //                {

   //                }
   //                else
   //                {
   //                    chars[whilestartindex] = C_N_Chinese_reflex(chars[whilestartindex].ToString()).ToString().ToCharArray()[0];
   //                }
   //                whilestartindex++;
   //            }
   //            //到这里已经完成民国的词语替换，全部转换为数字
   //            //下面就将对应的民国记年转换为公元纪年


   //            return result;
   //        }
   //        else
   //        {
   //            return cyear;
   //        }
   //    }

   //    //测试方法主入口
   //    public string example(string exampletext)
   //    {
   //        //盘古分词初始化
   //        string filename = @"D:\题库系统\redMomery\ConsoleApplication1test\Pangu\Release\PanGu.xml";
   //        PanGu.Segment.Init(filename);
   //        Segment segment = new Segment();
   //        MatchOptions options = new MatchOptions();

   //        ICollection<WordInfo> word = segment.DoSegment(exampletext);
   //        string result = "";
   //        result += "\n\r";
   //        int counts = 1;
   //        foreach (WordInfo item in word)
   //        {
   //            result += counts.ToString() + ":"; result += "\t";
   //            result += item.Word; result += "\t";
   //            result += item.OriginalWordType; result += "\t";
   //            result += item.Rank; result += "\t";
   //            result += GetChsPos(item.Pos);
   //                result += "\t";
   //            result += item.Position; result += "\t";
   //            result += item.WordType; result += "\t";
   //            result += item.Frequency; result += "\t";
   //            result += "\n\r";
   //            counts++;
   //        }

   //        return result;
   //    }

   //    //已经成功的方法
   //    public string GetChsPos(POS pos)
   //    {
   //        switch (pos)
   //        {
   //            case POS.POS_UNK:
   //                return "未知词性";
   //            case POS.POS_D_K:
   //                return "后接成分";
   //            case POS.POS_D_H:
   //                return "前接成分";
   //            case POS.POS_A_NZ:
   //                return "其他专名";
   //            case POS.POS_A_NX:
   //                return "外文字符";
   //            case POS.POS_A_NR:
   //                return "人名";
   //            case POS.POS_D_Z:
   //                return "状态词";
   //            case POS.POS_A_NT:
   //                return "机构团体";
   //            case POS.POS_A_NS:
   //                return "地名";
   //            case POS.POS_D_Y:
   //                return "语气词 语气语素";
   //            case POS.POS_D_X:
   //                return "非语素字";
   //            case POS.POS_D_W:
   //                return "标点符号";
   //            case POS.POS_D_T:
   //                return "时间词";
   //            case POS.POS_D_S:
   //                return "处所词";
   //            case POS.POS_D_V:
   //                return "动词 动语素";
   //            case POS.POS_D_U:
   //                return "助词 助语素";
   //            case POS.POS_D_R:
   //                return "代词 代语素";
   //            case POS.POS_A_Q:
   //                return "量词 量语素";
   //            case POS.POS_D_P:
   //                return "介词";
   //            case POS.POS_D_MQ:
   //                return "数量词";
   //            case POS.POS_A_M:
   //                return "数词 数语素";
   //            case POS.POS_D_O:
   //                return "拟声词";
   //            case POS.POS_D_N:
   //                return "名词 名语素";
   //            case POS.POS_D_F:
   //                return "方位词 方位语素";
   //            case POS.POS_D_E:
   //                return "叹词 叹语素";
   //            case POS.POS_D_L:
   //                return "习语";
   //            case POS.POS_D_I:
   //                return "成语";
   //            case POS.POS_D_D:
   //                return "副词 副语素";
   //            case POS.POS_D_C:
   //                return "连词 连语素";
   //            case POS.POS_D_B:
   //                return "区别词 区别语素";
   //            case POS.POS_D_A:
   //                return "形容词 形语素";
   //        }
   //        return "未知词性";
   //    }
   //}
}
