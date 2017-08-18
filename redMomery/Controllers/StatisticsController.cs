using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace redMomery.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult cityLb()
        {
            return View();
        }
        public ActionResult GetCityLb()
        {
            List<redmomery.Model.cityLB> list = (new redmomery.DAL.cityLBDAL()).ListAll() as List<redmomery.Model.cityLB>;
            return Json(list);
        }
        public ActionResult GetCityinfo()
        {
            List<redmomery.Model.staticinfotable> list = (new redmomery.DAL.staticinfotableDAL()).ListAll() as List<redmomery.Model.staticinfotable>;
            return Json(list);
        }
    }
}
