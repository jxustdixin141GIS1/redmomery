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
            return View();
        }
        public ActionResult datetests()
        {  
                 return View();
        }
        #region 常用的网络
        #endregion
    }
}
