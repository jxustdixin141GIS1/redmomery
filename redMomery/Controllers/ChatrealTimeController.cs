using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using redmomery.librarys;
using redmomery.Model;
using redmomery.DAL;
using redMomery.Models;
namespace redMomery.Controllers
{
    public class ChatrealTimeController : Controller
    {
        //
        // GET: /ChatrealTime/
        #region 网页
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMessage()
        {
            return View();
        }
        public ActionResult PublicCampaing()
        {
            return View();
        }
        #endregion
        #region  服务方法
        public ActionResult PostCampaign()
        {
            string local = HttpContext.Request["local"].ToString();
            string title = HttpContext.Request["Contentitle"].ToString();
            string content = HttpContext.Request["content"].ToString();
            DateTime contentTime =DateTime.Parse( HttpContext.Request["contentTime"].ToString());
            USER_INFO user = Session["user"] as USER_INFO;
            meetingtable mt=  ChartOnlinelib.Usertakeon(user,local,content,title,contentTime);
            chartgrouptable cg = (new redmomery.DAL.chartgrouptableDAL()).Get(mt.GID);
            //下面为群组分享链接
            return Json(mt);        
        }
        //查询到我自己创建的群组
        public ActionResult getMymeeting() 
        {
            //获取用户和列表之间的关系
            USER_INFO user = Session["user"] as USER_INFO;
            List<redmomery.librarys.model.ViewUTIMeet> result = ChartOnlinelib.GetmeetList(user);
            return Json(result);
        }
        
        #endregion
    }
}
