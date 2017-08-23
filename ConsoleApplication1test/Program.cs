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

            NLPIR_ICTCLAS_C nlpr = new NLPIR_ICTCLAS_C();


            string s1 = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\github\team\redmomery\调试\新建文本文档.txt");
            // NLRedmomery.Program.example();
            List<Text_result> initlist = LBText.parseText(s1);
            //---------------------进行对象整合：也就是按照时间进行词语且切分-------------------- 
            int start = 0;//切分的起点
            int hisstart = 0;
            int Next = 0;//切分的终点
            //这里假设当前面文章为简单介绍时，也同时进行切分
            List<Time_result> timeinit1 = new List<Time_result>();
            for (int i = 0; i < initlist.Count; i++)
            {
                Next = i;
                if (initlist[i].res.sPos == "t")
                {
                    if (initlist[i - 1].text == "~")
                    {

                    }
                    else
                    {
                        #region
                        Time_result newTimeresult = new Time_result();
                        //将start -  Next 放入到newTimeresult
                        for (int j = start; j < Next; j++)
                        {
                            newTimeresult.timelist.Add(initlist[j]);
                        }
                        if (initlist[start].res.sPos == "t")
                        {
                            newTimeresult.time = initlist[start];
                            hisstart = start;
                        }
                        else
                        {
                            if (initlist[hisstart].res.sPos == "t")
                            {
                                newTimeresult.time = initlist[hisstart];
                            }
                            else
                            {
                                newTimeresult.time = null;
                            }
                        }
                        timeinit1.Add(newTimeresult);
                        start = Next;
                        #endregion
                    }
                }
                if (i == initlist.Count - 1)
                {
                    Time_result newTimeresult = new Time_result();
                    //将start -  Next 放入到newTimeresult
                    for (int j = start; j < Next; j++)
                    {
                        newTimeresult.timelist.Add(initlist[j]);
                    }
                    if (initlist[start].res.sPos == "t")
                    {
                        newTimeresult.time = initlist[start];
                        hisstart = start;
                    }
                    else
                    {
                        if (initlist[hisstart].res.sPos == "t")
                        {
                            newTimeresult.time = initlist[hisstart];
                        }
                        else
                        {
                            newTimeresult.time = null;
                        }
                    }
                    timeinit1.Add(newTimeresult);
                    start = Next;
                
                }
            }
            
            //结果展示：
            for (int i = 0; i < timeinit1.Count; i++)
            {
                Time_result temp = timeinit1[i];
                Console.Write("时间：");
                Console.Write(temp.time == null ? "" : temp.time.text);
                Console.WriteLine();
                Console.Write("内容：");
                for (int j = 0; j < temp.timelist.Count; j++)
                {
                    Text_result ttemp = temp.timelist[j];
                    Console.Write(ttemp.text);
                }
                Console.WriteLine();
                Console.WriteLine();
            }







            Console.Read();

        }
        //中间临时建立的对象，这里需要对此进行进一步的划分 
    }
    public class LBText
    {
        public static List<Text_result> parseText(string text)
        {
            List<Text_result> resl = new List<Text_result>();
            Text_result[] results = null;
            NLPIR_ICTCLAS_C nlpir = new NLPIR_ICTCLAS_C();
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

    }



    public class Text_result
    {
        public string text;
        public result_t res;
    }
    public class Time_result
    {
        public Text_result time=new Text_result();//若为null开头非时间词
        public List<Text_result> timelist=new List<Text_result>();//表示表示这个时间段，所对应的时间词切分结果

    }
    public class T_LocalText
    {
        public result_t Time;//表示时间
        public List<result_t> local;//表示地点
        public string context;//表示事件
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
