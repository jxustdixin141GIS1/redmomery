
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using redmomery.librarys;
using redmomery.command;
namespace redMomery.Controllers
{
    public class LBController : Controller
    {
        //
        // GET: /LB/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LB()
        {
            return View();
        }
        public ActionResult parseText(string sText)
        {
            return View();
        }
    }
}

