using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace redMomery.Controllers
{
    public class UserController : Controller//这个服务主要用来进行用户的注册功能测试
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

    }
}
