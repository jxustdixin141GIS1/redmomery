using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using redmomery.Model;
using System.Web.Services;
using System.Web.Http;
using System.IO;
using redmomery.DAL;
using redmomery.librarys;
namespace redMomery.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/
        #region 网页
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult cityLb()
        {
            return View();
        }
        public ActionResult LBsexstatistics()
        {
            return View();
        }
        public ActionResult provinceLB()
        {
            return View();
        }
        public ActionResult KeyWordExample()
        {
            return View();
        }
        #endregion


        #region 老兵城市分布
        public ActionResult GetLbsexstatstic()
        { 
           //数据库中获取性别信息
            List<redmomery.Model.LB_INFO> list = new List<LB_INFO>();
            list = (new redmomery.DAL.LB_INFODAL()).Listall() as List<redmomery.Model.LB_INFO>;
            int sexnan = 0, sexnv = 0;
            for (int i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                if(temp.LBsex.Trim()=="男")
                {
                    sexnan = sexnan + 1;
                }
                if (temp.LBsex.Trim() == "女")
                {
                    sexnv = sexnv + 1;
                }
            }
            //表示数据统计完成
            List<objectkeyname> res = new List<objectkeyname>();
            objectkeyname man = new objectkeyname();
            man.Key = "男";man.Value = sexnan;
            res.Add(man);
            objectkeyname women = new objectkeyname();
            women.Key = "女"; women.Value = sexnv;
            res.Add(women);
            return Json(res);
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
        #endregion
        
    }
}
