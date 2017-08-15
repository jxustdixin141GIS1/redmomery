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
using Newtonsoft.Json;
using System.Net;
using System.Data.Spatial;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Types;
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
               //1、下面为程序自行的统计函数
            string example = " {\"status\":0,\"result\":{\"location\":{\"lng\":121.96829223632793,\"lat\":37.16608424486709},\"formatted_address\":\"山东省威海市文登市\",\"business\":\"米山\",\"addressComponent\":{\"country\":\"中国\",\"country_code\":0,\"province\":\"山东省\",\"city\":\"威海市\",\"district\":\"文登市\",\"adcode\":\"371081\",\"street\":\"\",\"street_number\":\"\",\"direction\":\"\",\"distance\":\"\"},\"pois\":[],\"roads\":[],\"poiRegions\":[],\"sematic_description\":\"\",\"cityCode\":175}}";


            // testgeogecoding();
            Console.Read();
          
        }
    }
    public class staticbydata 
    {
        public void createcityinfo()
        {
            string path = @"D:\题库系统\github\team\redmomery\插件库\echart文件测试--统计网页\百度城市代码.txt";
            string contenxt = redmomery.command.createlog.readTextFrompath(path);
            string[] city = contenxt.Split('\n');
            Console.Write("程序切割完成");
            StringBuilder sbuding = new StringBuilder();//进行json格式数据文件的建立
            StringBuilder citycoding = new StringBuilder();
            sbuding.Append("var codecity_C =");
            sbuding.Append("[");
            for (int i = 0; i < city.Length - 1; i++)
            {
                string[] temp = city[i].Trim().Split(' ');//出去前后空格，并进行字符串的切分，后面为城市名
                string[] lnglat = redmomery.command.Geocodingcommand.getGecodingByAddress(temp[1]);
                Console.Write(i.ToString()); Console.Write("\r");
                sbuding.Append("{'code':" + temp[0] + "," + "'cityname':\"" + temp[1] + "\"" + ",'coordination'" + ":" + "[" + lnglat[0] + "," + lnglat[1] + "]" + "}");
                if (i < city.Length - 2)
                {
                    sbuding.Append(",");
                }
            }
            sbuding.Append("]");
            sbuding.Append(";");
            redmomery.command.createlog.createtxt(sbuding.ToString(), "cityTocode.js");
        }
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
            string url = "http://api.map.baidu.com/geocoder/v2/?location="+lat+","+lng+"&output=json&pois=1&ak=" + "WqQgeC4x8uBKhnrkUZVs0kDbgtl7eUMM";
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            return result;
        }
        public static void staticdistributionbycity()
        {
            List<redmomery.Model.LB_INFO> lbs = ((new redmomery.DAL.LB_INFODAL()).Listall()) as List<redmomery.Model.LB_INFO>;
            for (int i = 0; i < lbs.Count; i++)
            {
                LB_INFO lb = lbs[i];
                string addres = Commands.getAdressnameByXy(lb.X.ToString(), lb.Y.ToString());
            }
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
