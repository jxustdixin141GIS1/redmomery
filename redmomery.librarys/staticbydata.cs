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

namespace redmomery.librarys
{
    public  class staticbydata
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
            string tempstrings = "";
            List<redmomery.Model.LB_INFO> lbs = ((new redmomery.DAL.LB_INFODAL()).Listall()) as List<redmomery.Model.LB_INFO>;
            List<redmomery.Model.objectkeyname> citystatic = new List<objectkeyname>();
            for (int i = 0; i < lbs.Count; i++)
            {
                LB_INFO lb = lbs[i];
                string addres =redmomery.command.Geocodingcommand.getAdressnameByXy(lb.X.ToString(), lb.Y.ToString());//拿到对应的城市代码
                addres = addres.Replace("\"", ""); 
                string[] temp = addres.Split('{', '}', ',', ':');
                string citycode = string.Empty;
                string cityname = string.Empty;
                string province = string.Empty;
                string country = string.Empty;
                string countrycode = string.Empty;
                string lng = string.Empty;
                string lat = string.Empty;
                //信息获取
                #region
                for (int j = 0; j < temp.Length; j++)
                {
                    if (temp[j].ToString() == "cityCode")
                    {
                        citycode = temp[j + 1].ToString();
                    }
                    if (temp[j].ToString() == "city")
                    {
                        cityname = temp[j + 1].ToString();
                    }
                    if (temp[j].ToString() == "province")
                    {
                        province = temp[j + 1].ToString();
                    }
                    if (temp[j].ToString() == "country")
                    {
                        country = temp[j + 1].ToString();
                    }
                    if (temp[j].ToString() == "country_code")
                    {
                        countrycode = temp[j + 1].ToString();
                    }
                }
                //下面开始获取经纬度
                staticinfotable citytemp = (new redmomery.DAL.staticinfotableDAL()).GetbycityCode(citycode);
                if (citytemp == null)
                {
                    citytemp = new staticinfotable();
                    //表示没有需要添加
                    citytemp.citycode = citycode;
                    citytemp.cityname = cityname;
                    citytemp.country = country;
                    citytemp.countrycode = countrycode;
                    string[] xy = redmomery.command.Geocodingcommand.getGecodingByAddress(cityname);
                    lng = xy[0]; lat = xy[1];
                    citytemp.lng = lng==null?"":lng;
                    citytemp.lat = lat==null?"":lat;
                    citytemp.province = province;
                    if (citytemp.citycode.Length >1)
                    {
                        (new redmomery.DAL.staticinfotableDAL()).AddNew(citytemp);
                    }
                }
                #endregion
                //数据统计
                   //进行数据判别
                bool isexist = false;
                for (int j = 0; j < citystatic.Count; j++)
                {
                    if (citystatic[i].Key.ToString() == cityname)
                    {
                        citystatic[i].Value = (int)(citystatic[i].Value) + 1;
                        isexist = true;
                        break;
                    }
                }
                if (!isexist)
                {
                    objectkeyname newobject = new objectkeyname();
                    if (cityname != null && cityname != "")
                    {
                        newobject.Key = cityname;
                        newobject.Value = 1;
                    }
                }
                
                Console.Write(i.ToString());
                Console.Write("\r");
            }
            //开始生成第一个变量 cityinfo data
            

            //下面开始创建对应的数据表
            string cityinfo = redmomery.Common.SerializerHelper.SerializeToString(citystatic);
            string context = "var staticinfo=" + cityinfo + ";";
            string path = @"D:\题库系统\github\team\redmomery\redmomery.server\resource\staticfile";
            redmomery.command.createlog.createtxt(context, path, "cityinfo.js");
            redmomery.command.createlog.createlogs(tempstrings);
        }
    }
}
