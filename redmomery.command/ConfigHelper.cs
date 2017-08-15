using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  redmomery.Common
{
    public class ConfigHelper
    {
        public static string GetAppSettings(string key)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains(key))
                return ConfigurationManager.AppSettings[key].ToString();
            return string.Empty;
        }
    }
}
