using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using redmomery.DAL;
using redmomery.Model;
using redmomery.command;
namespace redMomery.Controllers
{
    public class CommandServeController : Controller
    {
        //
        // GET: /CommandServe/

        public ActionResult Index()
        {
            return View();
            
        }
        public ActionResult GetXYByAddess(string address)
        {
            baiduGeocodingaddress result = redmomery.command.Geocodingcommand.getGeocodingByAddressobject(address);
            return Json(result);
        }
        public ActionResult GetAddessByXY(string lng, string lat)
        {
            baiduGeocodingXY addess = redmomery.command.Geocodingcommand.getGeocodingByXYobject(lng, lat);
            return Json(addess);
        }
        public ActionResult Readmodel(string LBID)
        {
            if (LBID == null) return null;
            string[] lbs = LBID.Replace("\"", "").Split(',');

            LB_INFODAL lb = new LB_INFODAL();
            List<redmomery.Model.LB_INFO> temp = new List<redmomery.Model.LB_INFO>();

            for (int i = 0; i < lbs.Length; i++)
            {
                if (lbs[i] != "")
                {
                    temp.Add(lb.Get(int.Parse(lbs[i])));
                }
            }

            return Json(temp);
        }
        public  ActionResult Login(string Name,string password)
        {
            //登录功能
            USER_INFODAL udal=new USER_INFODAL();
            USER_INFO userinfo = udal.get(Name, password);
            Session["userinfo"] = userinfo;
            return Json(redmomery.Common.SerializerHelper.SerializeToString(userinfo));
        }

        

    }
}
