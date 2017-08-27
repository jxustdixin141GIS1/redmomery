using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            
            return View();
            
        }
        #endregion
    }
}
