using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
namespace redmomery.command
{
      public   class Geocodingcommand
    {
        public static string[] getGecodingByAddress(string nedadresss)
        {
            //获取指定的地址，开始进行查询
            string ak = "&ak=" + "WqQgeC4x8uBKhnrkUZVs0kDbgtl7eUMM";
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
    }
}
