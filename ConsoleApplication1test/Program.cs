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
using redmomery.command;
using System.Net;
using System.Data.Spatial;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
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
            baiduGeocodingaddress bas = redmomery.command.Geocodingcommand.getGeocodingByAddressobject("中国");
            Console.WriteLine("中国" + ":" + bas.result.level);
            bas = redmomery.command.Geocodingcommand.getGeocodingByAddressobject("中国江苏省");
            Console.WriteLine("中国江苏省" + ":" + bas.result.level);
            bas = redmomery.command.Geocodingcommand.getGeocodingByAddressobject("中国江苏省徐州市");
            Console.WriteLine("中国江苏省徐州市" + ":" + bas.result.level);
            bas = redmomery.command.Geocodingcommand.getGeocodingByAddressobject("中国江苏省徐州市沛县");
            Console.WriteLine("中国江苏省徐州市沛县" + ":" + bas.result.level);
            bas = redmomery.command.Geocodingcommand.getGeocodingByAddressobject("中国江苏省徐州市沛县魏庙镇");
            Console.WriteLine("中国江苏省徐州市沛县魏庙镇" + ":" + bas.result.level);
            bas = redmomery.command.Geocodingcommand.getGeocodingByAddressobject("中国江苏省徐州市沛县魏庙镇义河村");
            Console.WriteLine("中国江苏省徐州市沛县魏庙镇义河村" + ":" + bas.result.level);
            bas = redmomery.command.Geocodingcommand.getGeocodingByAddressobject("中国江苏省徐州市沛县魏庙镇义河村200号");
            Console.WriteLine("中国江苏省徐州市沛县魏庙镇义河村200号" + ":" + bas.result.level);
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
            ////staticbydata.staticdistributionbycity();
            string s1 = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\github\team\redmomery\调试\新建文本文档.txt").Replace("\n\r", "").Replace("\r\n", "");
            Console.WriteLine("进行聚类分析");
            object temp = LBText.parseText(s1);
            temp = LBText.timeExtract((List<Text_result>)temp);
            temp = LBText.ConvertToRes((List<T_LocalText>)temp);
            //开始进行组合
            List<Res_T_LocalText> timeinit1 = temp as List<Res_T_LocalText>;

           //地名唯一化处理

            //先批量处理地址
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
            double Gx = 0, Gy = 0, G=0;
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
            Gcenter.lng =float.Parse( Gx.ToString());
            Gcenter.lat =float.Parse( Gy.ToString());
           
            //下面开始删除地名点  坐标如下 级别最高为准  距离最近为准  级别越高 坐标越准
            for (int i = 0; i < res_t.Count; i++)
            {
                //首先进行级别判断 找到级别最高的
                Res_t_locals localtemp = new Res_t_locals();
                for (int j = 0; j < res_t[i].locals.Count; j++)
                {
                    baiduGeocodingaddress ba = res_t[i].locals[j].local;

                    if (levelscore(localtemp.local) > levelscore(ba))
                    {
                        res_t[i].locals.Remove(res_t[i].locals[j]);
                    }
                    else if (levelscore(localtemp.local) == levelscore(ba))
                    {
                        localtemp = res_t[i].locals[j];
                    }
                    else
                    {
                        res_t[i].locals.Remove(localtemp);
                        localtemp = res_t[i].locals[j];
                    }
                }

                //下面开始计算距离最近为准
                Res_t_locals localtemps = new Res_t_locals();


            }



            Console.Read();

        }

        public static  double levelscore(baiduGeocodingaddress ba)
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
        public static float distancestatic(baiduGeocodingaddress ba, baiducoordinate Gcenter)
        {
            float distance = float.MaxValue;
            distance = (ba.result.location.lng - Gcenter.lng) * (ba.result.location.lng - Gcenter.lng) + (ba.result.location.lat - Gcenter.lat) * (ba.result.location.lat - Gcenter.lat);
        }
        //中间临时建立的对象，这里需要对此进行进一步的划分 
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
            newt1.MD5 = redmomery.Common.MD5Helper.EncryptString(newt1.Authonrity + newt1.F_TIME.ToString() + newt1.M_ID.ToString() + newt1.Context + newt1.TITLE);
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
                    result[i].Location = lt.localtiontoWKT(result[i]);
                }
                Console.Write((i + 1).ToString() + "/");
            }
            //现在开始逐行更改
            int count = 0;
            Console.WriteLine("正在提交数据");
            for (int i = 0; i < result.Count; i++)
            {
                count += lt.update(result[i] as LB_INFO) ? 1 : -1;
                Console.Write((i + 1).ToString() + "/");
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
                //  baiduGeocodingXY ba = redmomery.command.Geocodingcommand.getGeocodingByXYobject(lb1.X.ToString(), lb1.Y.ToString());
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
            address += nedadresss;
            url = url + address + output + ak;
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            baiduGeocodingaddress ba = redmomery.Common.SerializerHelper.DeserializeToObject<baiduGeocodingaddress>(html);
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
