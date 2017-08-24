using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace redmomery.command
{
    public class Geocodingcommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nedadresss"></param>
        /// <returns>lng,lat</returns>
        public static string[] getGecodingByAddress(string nedadresss)
        {
            //获取指定的地址，开始进行查询
            string ak = "&ak=" + "WqQgeC4x8uBKhnrkUZVs0kDbgtl7eUMM";
            string url = "http://api.map.baidu.com/geocoder/v2/?";
            string address = "address=";
            string output = "&output=json";
            address += nedadresss;
            url = url + address + output + ak;
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            string[] xy = parsegeocoding(html);
            return xy;
        }
        private static string[] parsegeocoding(string anli)
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
        public static string getAdressnameByXy(string lng, string lat)
        {
            string result = string.Empty;
            string url = "http://api.map.baidu.com/geocoder/v2/?location=" + lat + "," + lng + "&output=json&pois=1&ak=" + "WqQgeC4x8uBKhnrkUZVs0kDbgtl7eUMM";
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            result = html;
            return result;
        }
        public static baiduGeocodingaddress getGeocodingByAddressobject(string nedadresss)
        {
            string ak = "&ak=" + Properties.Resources.redmomeryak.ToString();//"WqQgeC4x8uBKhnrkUZVs0kDbgtl7eUMM";
            string url = "http://api.map.baidu.com/geocoder/v2/?";
            string address = "address=";
            string output = "&output=json";
            address += nedadresss;
            url = url + address + output + ak;
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            baiduGeocodingaddress res = redmomery.Common.SerializerHelper.DeserializeToObject<baiduGeocodingaddress>(html);
            return res;
        }
        public static baiduGeocodingXY getGeocodingByXYobject(string lng, string lat)
        {
            string url = "http://api.map.baidu.com/geocoder/v2/?location=" + lat + "," + lng + "&output=json&pois=1&ak=" + Properties.Resources.redmomeryak.ToString();
            WebClient client = new WebClient();
            string html = UTF8Encoding.UTF8.GetString(client.DownloadData(url));
            baiduGeocodingXY result = redmomery.Common.SerializerHelper.DeserializeToObject<baiduGeocodingXY>(html);
            return result;
        }

    }



}
namespace redmomery.command
{
    public class baiduGeocodingaddress
    {
        public int status;
        public baiduresult result;
    }
    public class baiduresult
    {
        public baiducoordinate location;
        public int precise;
        public int confidence;
        public string level;
    }
    public class baiducoordinate
    {
        public float lng;
        public float lat;
    }
}
namespace redmomery.command
{
    public class baiduGeocodingXY
    {
        public string status;
        public baiduXYresult result;
    }
    public class baiduXYresult
    {
        public baiducoordinate location;
        public string formatted_address;
        public string business;
        public baiduaddressComponent addressComponent;
        public baiduposi[] pois;
        public string sematic_description;
        public int cityCode;
    }
    public class baiduaddressComponent
    {
        public string country;
        public int country_code;
        public string province;
        public string city;
        public string district;
        public string adcode;
        public string street;
        public string street_number;
        public string direction;
        public string distance;
    }
    public class baiduroad
    {
        public string name;
        public string distance;//
    }
    public class baiduposi
    {
        public string addr;// 地址信息  
        public string cp;// 数据来源  
        public string direction;//和当前坐标点的方向  
        public string distance;//离坐标点距离  
        public string name;//poi名称  
        public string poiType;// poi类型，如’ 办公大厦,商务大厦’  
        public baidupointxy point;// poi坐标{x,y}  
        public string tel;// 电话  
        public string uid;// poi唯一标识  
        public string zip;//邮编  
        public baiduparent_poi parent_poi;//
    }
    public class baidupointxy
    {
        public float x;
        public float y;
    }
    public class baiduparent_poi
    {
        public string name;
        public string tag;
        public string addr;
        public baidupointxy point;
        public string direction;
        public string distance;
        public string uid;

    }
}