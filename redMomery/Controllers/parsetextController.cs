using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using redmomery.command;
using redmomery;
using redmomery.Common;
using redmomery.DAL;
using redmomery.librarys;
using redmomery.Model;
using System.IO;
using Ivony.Html;
using Ivony.Html.Parser;
namespace redMomery.Controllers
{
    public class parsetextController : Controller
    {
        //
        // GET: /parsetext/

        public ActionResult Index()
        {
            return View();
        }
        [WebMethod]
        public ActionResult parseLbtext(string LBtext, string lbId)
        {
            return Json(LbTextParse.parseLbstored(int.Parse(lbId), LBtext));
        }
        [WebMethod]
        public ActionResult parseText(string LBText)
        {
            return Json( LbTextParse.parseText(LBText));
        }
        [WebMethod]
        public ActionResult ExtractLocalname(string Text)
        {
            return Json( LbTextParse.getLocalFromText(Text));
        }
        [WebMethod]
        public ActionResult KeyWord(string text)
        {
            return Json(LBText.getKeyWordFromText(text));
        }
    }
}
