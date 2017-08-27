using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLRedmomery;
using redmomery.Model;
using redmomery.Common;
using redmomery.command;
namespace redmomery.librarys
{
   public  class LbTextParse
    {
        public static List<trajectory> parseLbstored(LB_INFO lb)
        {
            List<trajectory> list = new List<trajectory>();
            List<Text_trcajectory> temp = LBText.parseText(lb.LBexperience.ToString());
            for (int j = 0; j < temp.Count; j++)
            {
                trajectory newtra = new trajectory();
                newtra.LBID = lb.ID;
                newtra.Local = temp[j].address == null ? "null" : temp[j].address;
                string temps = temp[j].time.IndexOf("年") >= 0 ?
                temp[j].time.Replace("9999-12-30-", "").Replace("年", "-").Replace("月", "-").Replace("日", "-").ToString() :
                temp[j].time.Replace("年", "-").Replace("月", "-").Replace("日", "-").ToString();
                DateTime dt = new DateTime();
                try
                {
                    dt = DateTime.ParseExact(temps, "yyyy-MM-dd-", null);
                }
                catch
                {
                    try
                    {
                        dt = DateTime.ParseExact(temps, "yyyy-MM-", null);
                    }
                    catch
                    {
                        try
                        {
                            dt = DateTime.ParseExact(temps, "yyyy-", null);
                        }
                        catch
                        {
                            dt = DateTime.ParseExact("9999-12-30-", "yyyy-MM-dd-", null);
                        }
                    }
                }
                newtra.T_time = dt;
                newtra.Timetext = temp[j].time;
                newtra.x = temp[j].xy.lng.ToString();
                newtra.y = temp[j].xy.lat.ToString();
                newtra.isCurrent = temp[j].iscurent;
                newtra.context = temp[j].context;
                list.Add(newtra);
            }
            return list;
        }
        public static List<trajectory> parseLbstored(int lbID,string lbtext)
        {
            
            List<trajectory> list = new List<trajectory>();
            List<Text_trcajectory> temp = LBText.parseText(lbtext);
            for (int j = 0; j < temp.Count; j++)
            {
                trajectory newtra = new trajectory();
                newtra.LBID = lbID;
                newtra.Local = temp[j].address == null ? "null" : temp[j].address;
                string temps = temp[j].time.IndexOf("年") >= 0 ?
                temp[j].time.Replace("9999-12-30-", "").Replace("年", "-").Replace("月", "-").Replace("日", "-").ToString() :
                temp[j].time.Replace("年", "-").Replace("月", "-").Replace("日", "-").ToString();
                DateTime dt = new DateTime();
                try
                {
                    dt = DateTime.ParseExact(temps, "yyyy-MM-dd-", null);
                }
                catch
                {
                    try
                    {
                        dt = DateTime.ParseExact(temps, "yyyy-MM-", null);
                    }
                    catch
                    {
                        try
                        {
                            dt = DateTime.ParseExact(temps, "yyyy-", null);
                        }
                        catch
                        {
                            dt = DateTime.ParseExact("9999-12-30-", "yyyy-MM-dd-", null);
                        }
                    }
                }
                newtra.T_time = dt;
                newtra.Timetext = temp[j].time;
                newtra.x = temp[j].xy.lng.ToString();
                newtra.y = temp[j].xy.lat.ToString();
                newtra.isCurrent = temp[j].iscurent;
                newtra.context = temp[j].context;
                list.Add(newtra);
            }
            return list;
        }
    }
    class LBText
    {
     public static List<Text_trcajectory> parseText(string text)
      {
          object temp = LBText.parsetext(text);
          temp = LBText.timeExtract((List<Text_result>)temp);
          temp = LBText.ConvertToRes((List<T_LocalText>)temp);
          //开始进行组合
          List<Res_T_LocalText> init = temp as List<Res_T_LocalText>;
          //下面主要处理地名
          List<Text_trcajectory> result = LBText.convertdeallocal(init);
          return result;
      }
     private   static List<string> Extractbookname(string s1)
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
     private static List<Text_result> parsetext(string text)
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
     private static List<T_LocalText> timeExtract(List<Text_result> initlist)
        {
            List<T_LocalText> t_linit1 = new List<T_LocalText>();
            //数据的格式改变
            //1、更改部分词语的词性
            string[] tmid = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\github\team\redmomery\NLRedmomery\bin\时间中词.txt").Split(',', '，');
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
            if (t_linit1.Count > 1)//若是只有一个数据就不要确定了
            {
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
                if (t_linit1[i].Time != null)
                {
                    t_linit1[i].Time.text = t_linit1[i].Time != null ? t_linit1[i].Time.text.Replace("春", "3月") : null;
                    t_linit1[i].Time.text = t_linit1[i].Time != null ? t_linit1[i].Time.text.Replace("夏", "6月") : null;
                    t_linit1[i].Time.text = t_linit1[i].Time != null ? t_linit1[i].Time.text.Replace("秋", "9月") : null;
                    t_linit1[i].Time.text = t_linit1[i].Time != null ? t_linit1[i].Time.text.Replace("冬", "12月") : null;
                }
                else
                {
                    t_linit1[i].Time=new Text_result();
                    t_linit1[i].Time.text = "9999-12-30-";
                }
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
     private static List<Text_result> ExtractLocal(List<Text_result> inittext)
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
     private static List<Res_T_LocalText> ConvertToRes(List<T_LocalText> t_linit1)
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
     private static List<Text_trcajectory> convertdeallocal(List<Res_T_LocalText> timeinit1)
        {

            List<Res_t_localtext> res_t = new List<Res_t_localtext>();
            for (int i = 0; i < timeinit1.Count; i++)
            {
                Res_t_localtext newres = new Res_t_localtext();
                newres.Time = timeinit1[i].time;
                for (int j = 0; j < timeinit1[i].local.Count; j++)
                {
                    //处理代码
                    baiduGeocodingaddress ba = redmomery.command.Geocodingcommand.getGeocodingByAddressobject(timeinit1[i].local[j]);
                    if (ba.status == 0 && ba.result != null)
                    {
                        Res_t_locals newloc = new Res_t_locals();
                        newloc.addressname = timeinit1[i].local[j];
                        newloc.local = ba;
                        newres.locals.Add(newloc);
                    }
                }
                newres.context = timeinit1[i].context;
                newres.iscurrent = timeinit1[i].iscurrent;
                res_t.Add(newres);
            }
            double Gx = 0, Gy = 0, G = 0;
            for (int i = 0; i < res_t.Count; i++)
            {
                for (int j = 0; j < res_t[i].locals.Count; j++)
                {
                    baiduGeocodingaddress ba = res_t[i].locals[j].local;
                    if (ba.status == 0 && ba.result != null)
                    {
                        double mi = 0;
                        switch (ba.result.level)
                        {
                            case "国家": mi = 0.2; break;
                            case "省": mi = 0.4; break;
                            case "城市": mi = 0.6; break;
                            case "区县": mi = 0.8; break;
                            case "村庄": mi = 1; break;
                            default: mi = 0; break;
                        }

                        Gx = Gx + ba.result.location.lng * mi;
                        Gy = Gy + ba.result.location.lat * mi;
                        G = mi + G;
                    }
                }
            }
            //计算坐标重心
            //统计国别

            Gy = Gy / G;
            Gx = Gx / G;
            baiducoordinate Gcenter = new baiducoordinate();
            Gcenter.lng = float.Parse(Gx.ToString());
            Gcenter.lat = float.Parse(Gy.ToString());
            List<Text_trcajectory> result = new List<Text_trcajectory>();
            //下面开始删除地名点  坐标如下 级别最高为准  距离最近为准  级别越高 坐标越准
            for (int i = 0; i < res_t.Count; i++)
            {
                //首先进行级别判断 找到级别最高的
                if (res_t[i].locals.Count > 0)
                {
                    Res_t_locals localtemp = res_t[i].locals[0];
                    List<Res_t_locals> reslocalstemp = new List<Res_t_locals>();
                    for (int j = 0; j < res_t[i].locals.Count; j++)
                    {
                        reslocalstemp.Add(res_t[i].locals[j]);
                    }
                    for (int j = 0; j < res_t[i].locals.Count; j++)
                    {
                        baiduGeocodingaddress ba = res_t[i].locals[j].local;

                        if (levelscore(localtemp.local) > levelscore(ba))
                        {
                            reslocalstemp.Remove(res_t[i].locals[j]);
                        }
                        else if (levelscore(localtemp.local) == levelscore(ba))
                        {
                            localtemp = res_t[i].locals[j];
                        }
                        else
                        {

                            reslocalstemp.Remove(localtemp);
                            localtemp = res_t[i].locals[j];
                        }
                    }
                    res_t[i].locals = reslocalstemp;
                    reslocalstemp = new List<Res_t_locals>();
                    for (int j = 0; j < res_t[i].locals.Count; j++)
                    {
                        reslocalstemp.Add(res_t[i].locals[j]);
                    }
                    //下面开始计算距离最近为准
                    Res_t_locals localtemps = new Res_t_locals();
                    for (int j = 0; j < res_t[i].locals.Count; j++)
                    {
                        baiduGeocodingaddress ba = res_t[i].locals[j].local;

                        if (distancestatic(localtemp.local, Gcenter) > distancestatic(ba, Gcenter))
                        {
                            reslocalstemp.Remove(res_t[i].locals[j]);
                        }
                        else if (distancestatic(localtemp.local, Gcenter) == distancestatic(ba, Gcenter))
                        {
                            localtemp = res_t[i].locals[j];
                        }
                        else
                        {
                            reslocalstemp.Remove(localtemp);
                            localtemp = res_t[i].locals[j];
                        }
                    }

                    res_t[i].locals = reslocalstemp;
                    Text_trcajectory newtra = new Text_trcajectory();
                    newtra.time = res_t[i].Time;
                    newtra.context = res_t[i].context;
                    if (res_t[i].locals.Count > 0)
                    {
                        newtra.address = res_t[i].locals[0].addressname;
                        if (res_t[i].locals[0].local.result != null)
                            newtra.xy = res_t[i].locals[0].local.result.location;
                    }
                    newtra.iscurent = res_t[i].iscurrent + 1;
                    result.Add(newtra);//添加数据
                }
                else
                {
                    Text_trcajectory newtra = new Text_trcajectory();
                    newtra.time = res_t[i].Time;
                    newtra.context = res_t[i].context;
                    if (res_t[i].locals.Count > 0)
                    {
                        newtra.address = res_t[i].locals[0].addressname;
                        if (res_t[i].locals[0].local.result != null)
                            newtra.xy = res_t[i].locals[0].local.result.location;
                    }
                    newtra.iscurent = res_t[i].iscurrent + 1;
                    result.Add(newtra); 
                }
            }
            return result;
        }

     private static double levelscore(baiduGeocodingaddress ba)
        {
            double mitemp = 0;
            switch (ba.result.level)
            {
                case "国家": mitemp = 0.2; break;
                case "省": mitemp = 0.4; break;
                case "城市": mitemp = 0.6; break;
                case "区县": mitemp = 0.8; break;
                case "村庄": mitemp = 1; break;
                default: mitemp = 0; break;
            }
            return mitemp;
        }
     private static float distancestatic(baiduGeocodingaddress ba, baiducoordinate Gcenter)
        {
            float distance = float.MaxValue;
            distance = (ba.result.location.lng - Gcenter.lng) * (ba.result.location.lng - Gcenter.lng) + (ba.result.location.lat - Gcenter.lat) * (ba.result.location.lat - Gcenter.lat);
            return distance;
        }
       
    }

}
namespace redmomery.librarys
{
    public class Text_trcajectory
    {
        public string time;
        public string address;
        public baiducoordinate xy = new baiducoordinate();
        public string context;
        public int iscurent;
    }
}
namespace redmomery.librarys
{
    class Time_result
    {
        public Text_result time = new Text_result();//若为null开头非时间词
        public List<Text_result> timelist = new List<Text_result>();//表示表示这个时间段，所对应的时间词切分结果
    }
    class T_LocalText//提取时间，和地点词  第二次处理
    {
        public Text_result Time;//表示时间
        public List<Text_result> local = new List<Text_result>();//表示地点
        public List<Text_result> res = new List<Text_result>();
        public int iscurrent = 0;
    }
     class Res_T_LocalText //提取时间和内容 去除对应对的分词属性之后结果 第三次处理
    {
        public string time;//时间
        public List<string> local = new List<string>();//地点 
        public string context;//内容
        public int iscurrent = 0;
    }
    class Res_t_localtext //给地点 进行坐标处理之后的第四次处理
    {
        public string Time;
        public List<Res_t_locals> locals = new List<Res_t_locals>();
        public string context;
        public int iscurrent = 0;
    }
    class Res_t_locals //地点赋值时间
    {
        public string addressname;
        public baiduGeocodingaddress local;
    }
    class Text_result//分词第一次处理
    {
        public string text;
        public result_t res;
    }

}