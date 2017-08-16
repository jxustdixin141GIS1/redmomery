using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ivony.Html;
using Ivony.Html.Parser;
namespace redMomery.Controllers
{
    public class searchController : Controller
    {
        //
        // GET: /search/

        public ActionResult Index()
        {
            Bing("中国", "www.csdn.net/", 1);
            return View();
        }
         [HttpPost]
        public ActionResult Bing(string keyword, string selectsite, int PageIndex)
        {
            JumonyParser jumony = new JumonyParser();
            //如：
            var url = "http://cn.bing.com/search?q=" + keyword + "+site:" + selectsite + "&first=" + PageIndex + "1&FORM=PERE";
            var document = jumony.LoadDocument(url);
            var list = document.Find("#b_results .b_algo").ToList().Select(t => t.ToString()).ToList();

            var listli = document.Find("li.b_pag nav ul li");
            if (PageIndex > 0 && listli.Count() == 0)
                return null;

            if (listli.Count() > 1)
            {
                var text = document.Find("li.b_pag nav ul li").Last().InnerText();
                int npage = -1;
                if (text == "下一页")
                {
                    if (listli.Count() > 1)
                    {
                        var num = listli.ToList()[listli.Count() - 2].InnerText();
                        int.TryParse(num, out npage);
                    }
                }
                else
                    int.TryParse(text, out npage);
                if (npage <= PageIndex)
                    list = null;
            }
            return Json(list);
        }
    }
}
