using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using redmomery.DAL;
using redmomery.Model;
namespace redMomery.Controllers
{
    public class BBSController : Controller
    {
        //
        // GET: /BBS/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult postcommentofComment(string  M_ID, string  T_ID, string C_ID,string comment)
        {
            USER_INFO userinfo = (USER_INFO)Session["userinfo"];
            int U_ID = userinfo.USER_ID;
           
            return Json("回复成功");
        }
    }
}
