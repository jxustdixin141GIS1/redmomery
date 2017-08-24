using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLRedmomery;
namespace redmomery.librarys
{
    class LbTextParse
    {
       
    }
    public class LBText
    {
        public static List<string> Extractbookname(string s1)
        {
            List<string> bookname = new List<string>();
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i].ToString() == "》")
                {
                    int starts = s1.LastIndexOf("《", i);
                    if (starts >= 0)
                    {
                        StringBuilder st = new StringBuilder();
                        st.Append(s1.Substring(starts, i - starts + 1));
                        bookname.Add(st.ToString());
                    }
                }

            }
            return bookname;

        }
        public static List<Text_result> parseText(string text)
        {
            List<string> bookname = Extractbookname(text);
            List<Text_result> resl = new List<Text_result>();
            Text_result[] results = null;
            NLPIR_ICTCLAS_C nlpir = new NLPIR_ICTCLAS_C();

            for (int i = 0; i < bookname.Count; i++)
            {
                nlpir.AddUserWord(bookname[i] + "\t" + "n");
            }
            int count = nlpir.GetParagraphProcessAWordCount(text);
            result_t[] res = nlpir.ParagraphProcessAW(count);
            byte[] bytes = System.Text.Encoding.Default.GetBytes(text);
            //下面将对应的数据进行转换
            results = new Text_result[count];
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = new Text_result();
                results[i].text = Encoding.Default.GetString(bytes, res[i].start, res[i].length);
                results[i].res = res[i];
            }
            resl.AddRange(results);
            return resl;
        }
        public static List<T_LocalText> timeExtract(List<Text_result> initlist)
        {
            List<T_LocalText> t_linit1 = new List<T_LocalText>();
            //数据的格式改变
            //1、更改部分词语的词性
            string[] tmid = redmomery.command.createlog.readTextFrompath(@"..\时间中词.txt").Split(',', '，');
            for (int i = 0; i < initlist.Count; i++)
            {
                for (int j = 0; j < tmid.Length; j++)
                {
                    if (initlist[i].text == tmid[j])
                    {
                        //如何在这两个词中有一个是时间词，则表示这两个都是时间词
                        if (initlist[i - 1].res.sPos == "t" || initlist[i + 1].res.sPos == "t")
                        {
                            initlist[i - 1].res.sPos = "t";
                            initlist[i + 1].res.sPos = "t";
                        }
                    }
                    if (initlist[i].res.sPos == "t")
                    {
                        bool text = (initlist[i].text.IndexOf("后") >= 0) || (initlist[i].text.IndexOf("同") >= 0);
                        if (text)
                        {
                            initlist[i].res.sPos = initlist[i].res.sPos == "t" ? "tc" : initlist[i].res.sPos;
                        }
                    }
                }
            }
            //开始将程序进行提取,使用堆栈的方式
            List<Text_result> temp = new List<Text_result>();
            for (int i = 0; i < initlist.Count; i++)
            {
                temp.Add(initlist[i]);
                if (i == initlist.Count - 1)
                {
                    Text_result[] ttemp = new Text_result[temp.Count];
                    for (int j = 0; j < ttemp.Length; j++)
                    {
                        ttemp[j] = temp[j];
                    }
                    T_LocalText nt = new T_LocalText();
                    nt.Time = ttemp[0].res.sPos == "t" ? ttemp[0] : null;
                    for (int j = 0; j < ttemp.Length; j++)
                    {
                        nt.res.Add(ttemp[j]);
                    }
                    nt.local = ExtractLocal(nt.res);
                    nt.iscurrent = 1;
                    t_linit1.Add(nt);
                    temp = new List<Text_result>();//在从新添加对应的程序
                    break;
                }
                else
                {
                    if (initlist[i].text == "。" || initlist[i].text == "\r\n")
                    {
                        if (initlist[i + 1].res.sPos == "t" || i + 1 == initlist.Count - 1)//表示一种结束
                        {
                            Text_result[] ttemp = new Text_result[temp.Count];
                            for (int j = 0; j < ttemp.Length; j++)
                            {
                                ttemp[j] = temp[j];
                            }
                            T_LocalText nt = new T_LocalText();
                            nt.Time = ttemp[0].res.sPos == "t" ? ttemp[0] : null;
                            for (int j = 0; j < ttemp.Length; j++)
                            {
                                nt.res.Add(ttemp[j]);
                            }
                            nt.local = ExtractLocal(nt.res);
                            nt.iscurrent = 1;
                            t_linit1.Add(nt);
                            temp = new List<Text_result>();//在从新添加对应的程序
                        }
                        else
                        { //若是碰到类似与 从 表示之后的程序为
                            if (initlist[i + 1].text == "从" && initlist[i + 2].res.sPos == "t")
                            {
                                Text_result[] ttemp = new Text_result[temp.Count];
                                for (int j = 0; j < ttemp.Length; j++)
                                {
                                    ttemp[j] = temp[j];
                                }
                                T_LocalText nt = new T_LocalText();
                                nt.Time = ttemp[0].res.sPos == "t" ? ttemp[0] : null;
                                for (int j = 0; j < ttemp.Length; j++)
                                {
                                    nt.res.Add(ttemp[j]);
                                }
                                nt.local = ExtractLocal(nt.res);
                                nt.iscurrent = 1;
                                t_linit1.Add(nt);
                                temp = new List<Text_result>();//在从新添加对应的程序

                            }
                        }
                    }
                    else
                    {

                    }
                }
            }

            //下面开始进行二次时间的确定,从文本中抽取

            if (t_linit1[0].Time == null)
            {
                T_LocalText ttemp = t_linit1[0];
                t_linit1[0].Time = t_linit1[1].Time;
                for (int j = 0; j < ttemp.res.Count; j++)
                {
                    if (ttemp.res[j].res.sPos == "t")
                    {
                        t_linit1[0].Time = ttemp.res[j];
                        t_linit1[0].iscurrent = 2;
                        break;
                    }
                }
                t_linit1[0].iscurrent = 2;
            }

            for (int i = 0; i < t_linit1.Count; i++)
            {
                T_LocalText ttemp = t_linit1[i];
                if (ttemp.Time != null)
                {
                    continue;
                }
                else
                {
                    for (int j = 0; j < ttemp.res.Count; j++)
                    {
                        if (ttemp.res[j].res.sPos == "t")
                        {
                            t_linit1[i].Time = ttemp.res[j];
                            t_linit1[i].iscurrent = 2;
                            break;
                        }
                    }
                }
                t_linit1[i].Time.text = t_linit1[i].Time.text.Replace("春", "3月");
                t_linit1[i].Time.text = t_linit1[i].Time.text.Replace("夏", "6月");
                t_linit1[i].Time.text = t_linit1[i].Time.text.Replace("秋", "9月");
                t_linit1[i].Time.text = t_linit1[i].Time.text.Replace("冬", "12月");
            }


            //进行时间的推算，简单来说就是，进行时间的年份推算
            for (int i = 0; i < t_linit1.Count; i++)
            {
                T_LocalText ttemp = t_linit1[i];
                if (ttemp.Time.text.IndexOf("年") >= 0)
                {
                    if (ttemp.Time.text.IndexOf("月") >= 0)
                    {

                    }
                    else
                    {
                        if (ttemp.Time.text.IndexOf("日") >= 0)
                        {

                        }
                        else
                        {
                            //没有日期的限制,需要二次处理信息，也就找到一个有着时间的时间词作为补偿
                            for (int j = i; j >= 0; j--)
                            {
                                if (t_linit1[j].Time.text.IndexOf("年") >= 0)
                                {
                                    t_linit1[i].Time = t_linit1[j].Time;
                                    t_linit1[i].iscurrent = 3;
                                    break;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //表示没有年份
                    for (int j = i; j >= 0; j--)
                    {
                        int ytemp = t_linit1[j].Time.text.IndexOf("年");
                        if (ytemp >= 0)
                        {
                            t_linit1[i].Time.text = t_linit1[j].Time.text.Substring(0, ytemp + 1) + t_linit1[i].Time.text;
                            t_linit1[i].iscurrent = 4;
                            break;
                        }
                    }
                }
            }
            //进行地点的词语匹配，这里就是假定指定的对象不会发生移动
            for (int i = 0; i < t_linit1.Count; i++)
            {
                if (t_linit1[i].local.Count == 0)
                {
                    if (i == 0)
                    {
                        for (int j = 0; j < t_linit1.Count; j++)
                        {
                            if (t_linit1[j].local.Count > 0)
                            {
                                t_linit1[i].local = t_linit1[j].local;
                                t_linit1[i].iscurrent = 5;
                            }
                        }
                    }
                    else
                    {
                        if (t_linit1[i].local.Count == 0)
                        {
                            for (int j = 0; j < i; j++)
                            {
                                if (t_linit1[j].local.Count > 0)
                                {
                                    t_linit1[i].local = t_linit1[j].local;
                                    t_linit1[i].iscurrent = 5;
                                }
                            }
                        }
                    }
                }
            }
            //下面进行方法的二次处理将文本提取出来

            return t_linit1;
        }
        public static List<Text_result> ExtractLocal(List<Text_result> inittext)
        {
            List<Text_result> result = new List<Text_result>();
            for (int i = 0; i < inittext.Count; i++)
            {
                if (inittext[i].res.sPos == "ns" || inittext[i].res.sPos == "nsf")
                {
                    result.Add(inittext[i]);
                }
            }
            return result;
        }
        public static List<Res_T_LocalText> ConvertToRes(List<T_LocalText> t_linit1)
        {
            List<Res_T_LocalText> result = new List<Res_T_LocalText>();
            for (int i = 0; i < t_linit1.Count; i++)
            {
                Res_T_LocalText newres = new Res_T_LocalText();
                newres.time = t_linit1[i].Time.text;
                for (int j = 0; j < t_linit1[i].local.Count; j++)
                {
                    newres.local.Add(t_linit1[i].local[j].text);
                }
                StringBuilder sb = new StringBuilder();
                for (int j = 0; j < t_linit1[i].res.Count; j++)
                {
                    sb.Append(t_linit1[i].res[j].text);
                }
                newres.context = sb.ToString();
                newres.iscurrent = t_linit1[i].iscurrent;
                result.Add(newres);
            }
            return result;
        }
    }

}
namespace redmomery.librarys
{

    public class Time_result
    {
        public Text_result time = new Text_result();//若为null开头非时间词
        public List<Text_result> timelist = new List<Text_result>();//表示表示这个时间段，所对应的时间词切分结果
    }
    public class T_LocalText
    {
        public Text_result Time;//表示时间
        public List<Text_result> local = new List<Text_result>();//表示地点
        public List<Text_result> res = new List<Text_result>();
        public int iscurrent = 0;
    }
    public class Res_T_LocalText
    {
        public string time;//时间
        public List<string> local = new List<string>();//地点
        public string context;//内容
        public int iscurrent = 0;
    }
}