using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using redmomery.DAL;
using redmomery.Model;
namespace redMomery.Controllers
{
    public class uploadController : Controller
    {
        //
        // GET: /upload/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult summitForm()
        {
            
            USER_INFO userinfo = (USER_INFO)Session["userinfo"];
            return View();
        }
        [HttpPost]
        public ActionResult summitForm(object file)
        {
                
                return Json("成功");
        }
    }
     
}
