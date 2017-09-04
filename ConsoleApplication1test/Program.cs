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
using redmomery.Common;
using System.Net;
using System.Data.Spatial;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.InteropServices;
using NLRedmomery;
using System.Net.Mail;  
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
            //邓小平，为人表

           // List<track> list = new List<track>();
           // trackDAL dal = new trackDAL();
           // list = dal.ListAll() as List<track>;
           // for (int i = 0; i < list.Count; i++)
           // {
           //     dal.Update(list[i]);
           //     Console.Write(i.ToString()+"\\");
           // }
           // Console.WriteLine();
           // Console.WriteLine("开始处理轨迹表");
           // List<trajectory> tlist = new List<trajectory>();
           // trajectoryDAL tdal = new trajectoryDAL();
           // tlist = tdal.ListAll() as List<trajectory>;
           // for (int i = 0; i < tlist.Count; i++)
           // {
           //     trajectory temp = tlist[i];
           //     LB_INFO lb = (new LB_INFODAL()).get(temp.LBID);
           //     temp.name = lb.LBname;
           //     tdal.Update(temp);
           //     Console.Write(i.ToString() + "\\");
           // }
           //// Commands cl1 = new Commands();
           // //获取当前用户的
           //cl1.testgeogecoding();
            #region
            ////这一部分用来处理大部分的时间概念
            //List<trajectory> t = new List<trajectory>();
            //trajectoryDAL dal = new trajectoryDAL();
            //DateTime dt = DateTime.Parse("1943/03/21");

            //Console.WriteLine(dt.ToString());
            //Console.WriteLine(DateTime.Now.ToString());
            //t = dal.getBytime(dt);
            //for (int i = 0; i < t.Count; i++)
            //{
            //    Console.WriteLine(SerializerHelper.SerializeToString(t[i]));   
            //}
            //DateTime temp = DateTime.Now;
            //for (int i = 0; i < 10; i++)
            //{
            //    List<GroupUser> gu = (new GroupUserDAL()).getGusers((3).ToString());
            //    Console.WriteLine(redmomery.Common.SerializerHelper.SerializeToString(gu));
            //    Console.WriteLine();
            //    Console.WriteLine("测试用户发送消息");
            //    multimessagepooltable newmeessage = new multimessagepooltable();
            //    newmeessage.FUID = 2;
            //    newmeessage.TGID = 3;
            //    if (i == 5)
            //    {
            //        temp = DateTime.Now;
            //    }
            //    newmeessage.context = "这个是专门用来测试对应的网络在线聊天功能的" + DateTime.Now.ToString().ToString();
            //    newmeessage.Ftime = DateTime.Now;
            //    newmeessage.MD5 = MD5Helper.EncryptString(redmomery.Common.SerializerHelper.SerializeToString(newmeessage));
            //    multimessagepooltableDAL dals = new multimessagepooltableDAL();
            //    dals.AddNew(newmeessage);
            //    Thread.Sleep(1000);
            //}
            //Console.WriteLine("开始读取聊天记录");
            //List<multimessagepooltable> list = new List<multimessagepooltable>();
            //multimessagepooltableDAL dal = new multimessagepooltableDAL();
            //Console.WriteLine(temp.ToString());
            //list = dal.getBytime(3,temp);
            //Console.WriteLine(redmomery.Common.SerializerHelper.SerializeToString(list));


           // List<LB_INFO> lists = (new LB_INFODAL()).Listall() as List<LB_INFO>;

            //Console.WriteLine(long.MaxValue.ToString().Length);
            //Console.WriteLine((DateTime.Now.ToString()).Length);
          //string LBtext = redmomery.command.createlog.readTextFrompath(@"D:\qq缓存\新建文本文档.txt");



          //List<trajectory> list = LbTextParse.parseLbstored(-1,LBtext);          //object temp = LBText.parsetext(LBtext);
          //List<Text_result> initlist = LBText.mergeresult((List<Text_result>)temp);
          //temp = LBText.ChangeCp(initlist);
          //temp = LBText.Removevilable((List<Time_result>)temp);
          //List<Time_result> show = temp as List<Time_result>;
          //List<Time_result> show6 = LBText.ExtractTime(show); show = show6;
          //List<Time_result> show4 = LBText.Removevilable(show); show = show4;
          //List<Time_result> show5 = LBText.reckonTime(show); show = show5;
          //List<T_LocalText> show7 = LBText.ExtractLocalName(show);
          //List<Res_T_LocalText> show8 = LBText.ExtractContent(show7);
          //List<Res_T_LocalText> show9 = LBText.mergeLocal(show8);
          //List<Text_trcajectory> show10 = LBText.uniquelocal(show9);
          //for (int i = 0; i < show10.Count; i++)
          //{
          //    Console.WriteLine("时间：" + (show10[i].time == null ? "null" : show10[i].time));
          //    Console.WriteLine("地点：" + show10[i].address);
          //    if (show10[i].xy != null)
          //    {
          //        Console.WriteLine("x:" + show10[i].xy.lng + "  " + "y:" + show10[i].xy.lat);
          //    }
          //    else
          //    {
          //        Console.WriteLine("x:  null" + "  " + "y: null");
          //    }
          //    Console.WriteLine("内容：" + show10[i].context);
          //    Console.WriteLine("危险级：" + show10[i].iscurent);
          //    Console.WriteLine();
          //}
           //List<track> tlist = new List<track>();
           //StringBuilder sb = new StringBuilder();
           //for (int i = 0; i < list.Count; i++)
           //{
           //    track newt = new track();
           //    newt.EID = 178 + i;
           //    newt.Timetext = list[i].Timetext;
           //    newt.name = "周恩来";
           //    newt.heroID = "2周恩来";
           //    newt.Local = list[i].Local;
           //    newt.x = list[i].x;
           //    newt.y = list[i].y;
           //    newt.context = list[i].context;
           //    newt.img = 173 + i;
           //    tlist.Add(newt);
           //}
           ////批量上传
           //trackDAL dal = new trackDAL();
           //int count = 0;
           //for (int i = 0; i < tlist.Count; i++)
           //{
           //    count += dal.AddNew(tlist[i]);
           //    Console.Write(i.ToString());
            //}
            #endregion
            Console.WriteLine("程序结束");
            Console.Read();
        }
        public void f()
        {
            List<trajectory> list = new List<trajectory>();
            trajectoryDAL dal = new trajectoryDAL();
            list = dal.getByLBID((141).ToString());
            //dal.AddNew(model);
            //dal.Update(model);
            for (int i = 0; i < list.Count; i++)
            {
                trajectory model = list[i];
                LBTRACK mo = new LBTRACK();
                mo.ID = int.Parse(model.ID.ToString());
                mo.T_time = model.T_time;
                mo.Timetext = model.Timetext;
                mo.Local = model.Local;
                mo.context = model.context;
                mo.x = model.x;
                mo.y = model.y;
                mo.LBID = model.LBID;//.....
                mo.isCurrent = model.isCurrent;
                LB_INFO lb = new LB_INFO();
                LB_INFODAL lbdal = new LB_INFODAL();
                lb = lbdal.get(mo.LBID);
                mo.name = lb.LBname;
                LBTRACKDAL tdal = new LBTRACKDAL();
                tdal.AddNew(mo);
            }
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
            return distance;
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
