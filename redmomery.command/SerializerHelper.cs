
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace redmomery.Common
{
    public static class SerializerHelper
    {
        public static  string SerializeToString(object obj)
        {
            return (new temp()).SerializeToString(obj);
        }     
    }
    class temp : System.Web.Mvc.Controller
    {
        public string SerializeToString(object obj)
        {
            return Json(obj).ToString();
        }   
    }
}