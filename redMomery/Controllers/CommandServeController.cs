using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using redmomery.DAL;
using redmomery.Model;
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
        [WebMethod]
        public ActionResult GetXYByAddess(string address)
        {
            string[] xy = redmomery.command.Geocodingcommand.getGecodingByAddress(address);
            List<string> result = new List<string>();
            result.Add("X:" + xy[0]);
            result.Add("Y：" + xy[1]);
            return Json(result);
        }

        [WebMethod]
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
        [WebMethod]
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
