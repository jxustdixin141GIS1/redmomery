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
        public ActionResult testGetLB()
        {
            return View();
        }
        public ActionResult LBupload()
        {
            return View();
        }
        [WebMethod]
        public ActionResult GetLBByLBID(string sLBID)
        {
            List<LB_INFO> result = new List<LB_INFO>();
            LB_INFO LB = (new LB_INFODAL()).get(int.Parse(sLBID));
            result.Add(LB);
            return Json(result);
        }
        [WebMethod]
        public ActionResult GetLBByTID(string T_ID)
        {
            List<string> json = new List<string>();
            List<LB_INFO> result = new List<LB_INFO>();
            result.AddRange(BBSBLL.GetLB_INFOByTID(int.Parse(T_ID)));
            for (int i = 0; i < result.Count; i++)
            {
                json.Add(redmomery.Common.SerializerHelper.SerializeToString(result[i]));
            }
            return Json(result);
        }
        [WebMethod]
        public ActionResult getTitle(string T_ID)
        {
            List<string> result = new List<string>();
            //T_ID若是为-1,表示错误
            View_T_U b_title = BBSBLL.getTitleViewID(int.Parse(T_ID));
            result.Add(redmomery.Common.SerializerHelper.SerializeToString(b_title));
            return Json(result);
        }
        /// <summary>
        /// 这个用来得到的回复的帖子列表
        /// </summary>
        /// <param name="T_ID"></param>
        /// <returns></returns>
        [WebMethod]
        public ActionResult getComentbyTitleId(string T_ID)
        {
            List<string> result = new List<string>();
            List<View_CT_U> CTs = BBSBLL.GetCLgetTID(int.Parse(T_ID));
            for (int i = 0; i < CTs.Count; i++)
            {
                result.Add(redmomery.Common.SerializerHelper.SerializeToString(CTs[i]));
            }
            return Json(result);
        }
        [WebMethod]
        public ActionResult addCommentByTID(string TID, string context)
        {
            //这里就假装当前连接的用户ID为
            int UID = 1;
            CTBBS_TABLE newc = new CTBBS_TABLE();
            newc.F_TIME = DateTime.Now;
            newc.Context = context;
            newc.n_c = 0;
            newc.n_y = 0;
            newc.U_ID = UID;
            newc.is_delete = 0;
            newc.T_ID = int.Parse(TID);
            int CID = BBSBLL.PostCommentByTID(newc);
            List<string> result = new List<string>();
            View_CT_UDAL dal = new View_CT_UDAL();
            View_CT_U newct = new View_CT_U();
            newct = dal.Get(CID);
            result.Add(redmomery.Common.SerializerHelper.SerializeToString(newct));
            return Json(result);
        }
        [WebMethod]
        public ActionResult deleteCommentByCID(string CID)
        {

            List<string> result = new List<string>();
            bool isdelte = BBSBLL.DeleteCommentByCID(int.Parse(CID));
            result.Add(isdelte.ToString());
            return Json(result);
        }
        LB_INFODAL lbdal = new LB_INFODAL();
        [WebMethod]
        public ActionResult getLBINfo(string LBname)
        {
            List<string> json = new List<string>();
            List<LB_INFO> result = new List<LB_INFO>();
            result = (List<LB_INFO>)lbdal.GetByName(LBname);
            for (int i = 0; i < result.Count; i++)
            {
                json.Add(redmomery.Common.SerializerHelper.SerializeToString(result[i]));
            }
            return Json( json);
        }
    }
}
