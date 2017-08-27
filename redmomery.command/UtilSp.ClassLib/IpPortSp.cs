using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UtilSp.ClassLib
{
    public class IpPortSp
    {
      
        #region fetchIP function
        public static string fetchIP(string str)
        {
            Regex ipReg = new Regex(@"\d{0,3}\.\d{0,3}\.\d{0,3}\.\d{0,3}");
            string ip = ipReg.Match(str).Value;
            return ip;
        }
        #endregion     

        #region isIP Function
        public static bool isIP(string ip)
        {
            Regex reg = new Regex(@"^\d{0,3}\.\d{0,3}\.\d{0,3}\.\d{0,3}$");
            return reg.IsMatch(ip);
        }
        #endregion

        #region isPort Function
        public static bool isPort(string port)
        {
            Regex reg = new Regex(@"^\d{1,5}$");
            return reg.IsMatch(port);
        }
        #endregion
    }
}
