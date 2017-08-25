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
    public class LBbxController : Controller
    {
        //
        // GET: /LBbx/
        BBSTITLE_TABLEDAL dalTilte = new BBSTITLE_TABLEDAL();
        FILE_TABLEDAL dalFile = new FILE_TABLEDAL();
        public ActionResult LBbx()
        {
            USER_INFO userinfo = (USER_INFO)Session["userinfo"];

            return View();
        }
        [WebMethod]
        public void UploadLB()
        {
            USER_INFO userinfo = (USER_INFO)Session["userinfo"];
          //  int U_ID = userinfo.USER_ID;
            int U_ID = 2;
            HttpContext.Response.ContentType = "application/json:charset=utf-8";
            redmomery.Model.LB_INFO LB = new LB_INFO();
            string LBname = HttpContext.Request["lbname"].ToString();
            string LBsex = HttpContext.Request["lbsex"].ToString();
            string LBbirthday = HttpContext.Request["lbbirthday"].ToString();
            string LBjob = HttpContext.Request["lbwork"].ToString();
            string LBdecimal = HttpContext.Request["lbaddress"].ToString();
            string LBdesignation = HttpContext.Request["designation"].ToString();
            string LBlife = HttpContext.Request["lblife"].ToString();
            string LBstory = HttpContext.Request["lbstory"].ToString();
            #region  存储文件
            HttpFileCollectionBase files = HttpContext.Request.Files;
            List<FILE_TABLE> filelist = new List<FILE_TABLE>();
            for (int i = 0; i < files.Count; i++)
            {
                FILE_TABLE filet = new FILE_TABLE();
                HttpPostedFileBase file = files[i];
                string path = string.Empty;
                string filename = file.FileName;
                byte[] b = new byte[file.ContentLength];
                System.IO.Stream fs = file.InputStream;
                fs.Read(b, 0, file.ContentLength);
                //对于流进行MD5计算
                string MD5 = redmomery.Common.MD5Helper.GetStreamMD5(fs);
                string filepath = "\\resource\\file\\" + MD5 + filename;
                filet.Name = filename;
                filet.url = filepath; //Server.MapPath(filepath);
                filet.Keyvalues = "测试文件";
                //开始进行存储
                string storage = Server.MapPath(filepath);
                FileStream f = new FileStream(storage, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                BinaryWriter bw = new BinaryWriter(f);
                bw.Write(b);
                bw.Close();
                f.Close();
                filelist.Add(filet);
            }
            #endregion
            //开始进行帖子创建 lb信息默认 -1 当前为测试 2,表示为测试员成立代码

            LB = BBSBLL.CreateLBandTitle(U_ID, LBname, LBjob, LBsex, LBbirthday, LBdecimal, LBdesignation, LBstory, LBlife, filelist[0].url, -1000000, -1000000);

            BBSTITLE_TABLE title = dalTilte.get(LB.T_ID);
            //开始将文件添加到数据库
            for (int i = 0; i < filelist.Count; i++)
            {
                filelist[i].U_ID = U_ID;
                filelist[i].T_ID = LB.T_ID;
                filelist[i].M_ID = title.M_ID;
                filelist[i].N_View = 0;
                dalFile.addNew(filelist[i]);
            }
            HttpContext.Response.Write(redmomery.Common.SerializerHelper.SerializeToString(LB));
            HttpContext.Response.End();
        }
        public ActionResult LBupload()
        {
            return View();
        }

        [WebMethod]
        public ActionResult GetLBByLBID(string sLBID)
        {
            List<string> json = new List<string>();
            List<LB_INFO> result = new List<LB_INFO>();
            result.Add((new LB_INFODAL()).get(int.Parse(sLBID)));
            return Json(result);
        }


    }
}
