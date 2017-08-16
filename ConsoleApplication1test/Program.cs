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
            staticbydata.staticdistributionbycity();
            
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
        //这个方法，暂时还是有问题，中午吃饭是在进行更改
        public static void staticdistributionbycity()
        {
            List<redmomery.Model.LB_INFO> lbs = ((new redmomery.DAL.LB_INFODAL()).Listall()) as List<redmomery.Model.LB_INFO>;
            List<redmomery.Model.objectkeyname> citystatic = new List<objectkeyname>();
            for (int i = 0; i < lbs.Count; i++)
            {
                LB_INFO lb = lbs[i];
                string addres = Commands.getAdressnameByXy(lb.X.ToString(), lb.Y.ToString());//拿到对应的城市代码
                addres = addres.Replace("\"", "");
                string[] temp = addres.Split('{', '}', ',', ':');
               // objectkeyname ts = new objectkeyname();//ts.key:code名称，ts.value:数据统计
                //下面进行数据统计
                for (int j = 0; j < temp.Length; j++)
                {
                    if (temp[j].ToString() == "cityCode")
                    {
                        string citycode = temp[j + 1].ToString();
                        bool isexsit = false;
                        //下面开始进行判别是否存在
                        for (int t = 0; t < citystatic.Count; t++)
                        {      
                            if (citystatic[t].Key.ToString() == citycode)
                            {
                                citystatic[t].Value = ((int)(citystatic[t].Value)) + 1;
                                isexsit = true;
                            }
                        }
                        if (!isexsit)
                        {
                            objectkeyname newkey = new objectkeyname();
                            newkey.Key = citycode;
                            newkey.Value = 1;
                            citystatic.Add(newkey);
                        }
                    }
                }
                Console.Write(i.ToString());
                Console.Write("\r");
            }
            //下面为提取

        }
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
            string url = "http://api.map.baidu.com/geocoder/v2/?location="+lat+","+lng+"&output=json&pois=1&ak=" + "WqQgeC4x8uBKhnrkUZVs0kDbgtl7eUMM";
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
