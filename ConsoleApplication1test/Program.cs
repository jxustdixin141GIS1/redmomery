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
using redmomery.librarys;
using Newtonsoft.Json;
using System.Net;
using System.Data.Spatial;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Types;
using NLRedmomery;
//using PanGu;
//using PanGu.Dict;
//using PanGu.Framework;
//using PanGu.HighLight;
//using PanGu.Match;
//using PanGu.Setting;

namespace ConsoleApplication1test
{
    class Program
    {
        static void Main(string[] args)
        {
            #region  前期废弃的代码
            //BBs_laobing m = new BBs_laobing();
            //Commands c1 = new Commands();
            //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString().Trim();
            //Console.WriteLine(connectionString);
            //c1.testgeogecoding();
            //// m.updata();
            //Console.WriteLine("全部修改成功");
            #endregion
            //现在正在使用的代码
            //staticbydata.staticdistributionbycity();
            string s1 = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\github\team\redmomery\调试\新建文本文档.txt").Replace("\n\r", "").Replace("\r\n", "");
            List<Text_result> initlist = LBText.parseText(s1);
            for (int i = 0; i < initlist.Count; i++)
            {
                Console.WriteLine(i.ToString()+"::"+initlist[i].text+":"+initlist[i].res.sPos);
            }
            for (int i = 0; i < initlist.Count; i++)
            {
                Console.Write( initlist[i].text );
            }
            Console.WriteLine();
            List<T_LocalText> timeinit1 = LBText.timeExtract(initlist); 

            //结果展示：
            string s = "";
            for (int i = 0; i < timeinit1.Count; i++)
            {
                T_LocalText temp = timeinit1[i];
              s+="时间：";
               s+=temp.Time == null ? "" : temp.Time.text;
               s += "\n\r";
               s+="地点:";
                for (int j = 0; j < temp.local.Count; j++)
                {
                    Text_result ttemp = temp.local[j];
                   s+=ttemp.text + "  ";
                }
                s += "\n\r";
                 s+="内容：";
                for (int j = 0; j < temp.res.Count; j++)
                {
                    Text_result ttemp = temp.res[j];
                    s+=ttemp.text;
                }
                s += "\n\r";
                s += "\n\r";
            }
            Console.WriteLine(s);
            redmomery.command.createlog.createlogs(s);
            //--------------------------------------------------下面开始针对时间顺序进行排列------------------------------
            List<T_LocalText> t_sort = new List<T_LocalText>();
            for (int i = 0; i < timeinit1.Count; i++)
            {
                T_LocalText temp = timeinit1[i];
            }
            //
            Console.Read();

        }
        //中间临时建立的对象，这里需要对此进行进一步的划分 
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
                results[i].res=res[i];
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
                for(int j=0;j<tmid.Length;j++)
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
                        if (text )
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
                T_LocalText ttemp=t_linit1[i];
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
                 t_linit1[i].Time.text= t_linit1[i].Time.text.Replace("春", "3月");
                 t_linit1[i].Time.text = t_linit1[i].Time.text.Replace("夏", "6月");
                 t_linit1[i].Time.text = t_linit1[i].Time.text.Replace("秋", "9月");
                 t_linit1[i].Time.text = t_linit1[i].Time.text.Replace("冬", "12月");
            }
           

            //进行时间的推算，简单来说就是，进行时间的年份推算
            for (int i = 0; i < t_linit1.Count; i++)
            {
                T_LocalText ttemp=t_linit1[i];
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
                    for (int j = i; j >=0 ; j--)
                    {
                        int ytemp=t_linit1[j].Time.text.IndexOf("年");
                        if (ytemp >= 0)
                        {
                            t_linit1[i].Time.text = t_linit1[j].Time.text.Substring(0,ytemp+1) + t_linit1[i].Time.text;
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
            
            return t_linit1;
        }
        public static  List<Text_result> ExtractLocal(List<Text_result> inittext)
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
    }

    public class Time_result
    {
        public Text_result time=new Text_result();//若为null开头非时间词
        public List<Text_result> timelist=new List<Text_result>();//表示表示这个时间段，所对应的时间词切分结果
        
    }
    public class T_LocalText
    {
        public Text_result Time;//表示时间
        public List<Text_result> local = new List<Text_result>();//表示地点
        public List<Text_result> res = new List<Text_result>();
        public int iscurrent = 0;
        public string outstring()
        {
            String s = "";
            for (int i = 0; i < res.Count; i++)
            {
                s += res[i].text;
            }
            return s;
        }
    }
    public class Res_T_LocalText
    {
        public string time;//时间
        public string local;//地点
        public string context;//内容
    }

    public class LbyL
    {
        public string name;//这个是暂时的字段，以后可以删除
        public string year;
        public string local;
        public string text;

    }

    public class TimeDict
    {

        public void createDict()
        { //这个方法用来生成现有的词典用来进行词语的解析和便捷


        }
    }
    public class BBs_laobing
    {
        //------------------------这个类主要的作用按照现有的老兵信息自动生成对应的帖子信息----------------
        LB_INFODAL lbdal = new LB_INFODAL();
        BBSTITLE_TABLEDAL titledal = new BBSTITLE_TABLEDAL();
        public void addlBtotitle(LB_INFO lb)
        {
            //创建一个帖子
            int M_Id = -1;//这是老兵模块的初始ID值
            int U_ID = 1;//这个是管理员值
            BBSTITLE_TABLE newt1 = new BBSTITLE_TABLE();
            newt1.M_ID = M_Id;
            newt1.U_ID = U_ID;
            newt1.T_key = lb.LBname;
            newt1.TITLE = lb.LBname;
            newt1.Context = lb.LBexperience;
            newt1.is_pass = 1;
            newt1.N_RESPONSE = 0;
            newt1.N_YES = 0;
            newt1.pass_TIME = DateTime.ParseExact("9999/12/31", "yyyy/MM/dd", null);
            newt1.F_TIME = DateTime.Now;
            newt1.Authonrity = 10;
            newt1.MD5 = redmomery.Common.MD5Helper.EncryptString(redmomery.Common.SerializerHelper.SerializeToString(newt1));
            try
            {
                int count = titledal.addNew(newt1);
                newt1 = titledal.getByMD5(newt1.MD5);
                lb.T_ID = newt1.ID;
                lbdal.update(lb);
            }
            catch (Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Message + "\n\r" + ex.StackTrace.ToString());
            }

        }
        public void updata()
        {
            List<LB_INFO> list = lbdal.Listall() as List<LB_INFO>;
            for (int i = 0; i < list.Count; i++)
            {

                addlBtotitle(list[i]);
                Console.Write((i + 1).ToString() + "/");
            }

        }
    }
    public class Commands
    {
        //这个方法主要是为了进行有坐标解算出地名
        public static string getAdressnameByXy(string lng, string lat)
        {
            string result = string.Empty;
            string url = "http://api.map.baidu.com/geocoder/v2/?location=" + lat + "," + lng + "&output=json&pois=1&ak=" + "WqQgeC4x8uBKhnrkUZVs0kDbgtl7eUMM";
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            result = html;
            return result;
        }


        //下面这个方法用来生成百度地图城市编码的city名字表，注意为json格式
        public void createjsonbycityid()
        {

        }
        //开始进行模型修改
        public void testgeogecoding()
        {
            //开始从指定的对象中获取地址，并将其保存会
            //也就是取出所有模型
            string[] lbs = new string[1000];
            //赋值 
            for (int i = 0; i < lbs.Length; i++)
            {
                lbs[i] = i.ToString();
            }

            redmomery.DAL.LB_INFODAL lt = new LB_INFODAL();

            List<LB_INFO> result = new List<LB_INFO>();
            Console.WriteLine("正在读取数据。。。。");
            List<LB_INFO> temp = (List<LB_INFO>)lt.ListAll();
            if (temp != null)
                result.AddRange(temp);
            Console.WriteLine("正在转换数据。。。");
            for (int i = 0; i < result.Count; i++)
            {
                if (i == 343)
                {
                    Console.WriteLine();
                }
                //进行获取数据库
                LB_INFO temp1 = getGecoding(result[i]);
                result[i] = temp1;
                //开始进行WKT的生成
                result[i].T_ID = -1;
                if (result[i].X.ToString() != "" && result[i].Y.ToString() != "")
                {
                    string locationpoint = "Point(" + result[i].X.ToString() + " " + result[i].Y.ToString() + ")";
                    SqlString parstring = new SqlString(locationpoint);
                    SqlChars pars = new SqlChars(parstring);
                    SqlGeography localpoint = SqlGeography.STPointFromText(pars, 4326);
                    result[i].Location = localpoint;
                }
                Console.Write((i + 1).ToString() + "/");
            }
            //现在开始逐行更改
            int count = 0;
            for (int i = 0; i < result.Count; i++)
            {
                // count += lt.update(result[i] as LB_INFO)?1:-1;

            }
            Console.WriteLine();
            Console.WriteLine("\n\r有{0}行受到了影响,共计{1}", count, result.Count.ToString());
        }
        #region
        //List<LB_INFO> listLB = new List<LB_INFO>();
        //for (int i = 0; i < result.Count; i++)
        //{
        //    LB_INFO lb = new LB_INFO();
        //    LB_INFO l1=result[i];
        //    //进行映射
        //    lb.LB_ID = int.Parse(l1.LBID);
        //    lb.LB_JOB = l1.LBjob;
        //    lb.LB_SEX = l1.LBsex;
        //    lb.LB_NAME = l1.LBname;
        //    lb.LB_LOCX =0;// l1.X;
        //    lb.LB_LOCY = 0;// l1.Y;
        //    lb.LB_IMGPTH = l1.LBPhoto;
        //    lb.LB_EXPERIENCE = l1.LBexperience;
        //    lb.LB_DESIGNATION = l1.designation;
        //    lb.LB_ADDRESS = l1.LBdomicile;
        //    lb.LB_LIFE = l1.LBlife;

        //    #region  时间
        //    try
        //    {
        //        string times = l1.LBbirthday.Trim();
        //        if (times.IndexOf("年") <= 0)
        //        {
        //            try
        //            {
        //                lb.LB_BIRTHDAY = DateTime.ParseExact(times, "yyyy/MM/dd", null);
        //            }
        //            catch
        //            {
        //                try
        //                {
        //                    lb.LB_BIRTHDAY = DateTime.ParseExact(times, "yyyy/MM", null);
        //                }
        //                catch
        //                {
        //                    lb.LB_BIRTHDAY = DateTime.ParseExact(times, "yyyy", null);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            try
        //            {
        //                lb.LB_BIRTHDAY = DateTime.ParseExact(times, "yyyy年MM月dd日", null);
        //            }
        //            catch
        //            {
        //                try
        //                {
        //                    lb.LB_BIRTHDAY = DateTime.ParseExact(times, "yyyy年MM月", null);
        //                }
        //                catch
        //                {
        //                    lb.LB_BIRTHDAY = DateTime.ParseExact(times, "yyyy年", null);
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        lb.LB_BIRTHDAY = DateTime.ParseExact("9999/12/31", "yyyy/MM/dd", null);
        //    }
        //    #endregion
        //   // listLB.Add(lb);
        //    Console.WriteLine((test.InsertModel(lb) ? "成功{0}" : "失败{0}"), (i + 1).ToString());
        //    redmomery.command.createlog.createlogs((test.InsertModel(lb) ? "成功" + (i + 1).ToString() + "" : "失败" + (i + 1).ToString() + ""));
        // Console.WriteLine(i.ToString());
        //}
        #endregion
        public LB_INFO getGecoding(LB_INFO lb1)
        {
            //获取指定的地址，开始进行查询
            string[] xy = new string[2];
            xy[0] = null;
            xy[1] = null;
            xy = getGecodingByAddress(lb1.LBdomicile);
            //下面开始针对这个模型进行更新
            if (xy[0] != null && xy[0] != "" && xy[1] != null && xy[1] != "")
            {
                lb1.X = float.Parse(xy[0]);
                lb1.Y = float.Parse(xy[1]);
            }
            return lb1;
        }
        public string[] getGecodingByAddress(string nedadresss)
        {
            //获取指定的地址，开始进行查询
            string ak = "&ak=" + Properties.Resources.ak;
            string url = "http://api.map.baidu.com/geocoder/v2/?";
            string address = "address=";
            string output = "&output=json";
            string callback = "&callback=showLocation";
            address += nedadresss;
            url = url + address + output + ak + callback;
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            string[] xy = parsegeocoding(html);
            return xy;
        }
        private string[] parsegeocoding(string anli)
        {
            string[] result = new string[2];
            //进行split切分按照：
            anli = anli.ToLower();
            anli = anli.Replace("\"", "");
            string[] c1s = anli.Split(',', '{', '}');
            //开始进行匹配 
            for (int i = 0; i < c1s.Length; i++)
            {
                if (c1s[i].IndexOf("lng") >= 0)
                {
                    string[] temp = c1s[i].Split(':');
                    result[0] = "";
                    if (temp[1] != null && temp[1] != "")
                    {
                        result[0] = temp[1];
                    }
                }
                if (c1s[i].IndexOf("lat") >= 0)
                {
                    string[] temp = c1s[i].Split(':');
                    result[1] = "";
                    if (temp[1] != null && temp[1] != "")
                    {
                        result[1] = temp[1];
                    }
                }
            }
            return result;
        }
    }
}
