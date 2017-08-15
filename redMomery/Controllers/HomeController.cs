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
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        
        public ActionResult Index()
        {
            
            USER_INFO userinfo = new USER_INFO();
            USER_INFODAL udal = new USER_INFODAL();
            userinfo = udal.Get(1);
            Session["userinfo"] = userinfo;
            return View();
        }
        public ActionResult datetests()
        {  
       
            USER_INFO userinfo = (USER_INFO)Session["userinfo"];
            if (userinfo != null)
            {
                redmomery.command.createlog.createlogs(Session["userinfo"].ToString());
            }
                 return View();
        }
        #region 常用的网络
        #endregion
    }
}
