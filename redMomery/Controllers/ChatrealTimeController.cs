using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
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
        public ActionResult groupchart()
        {
            return View();
        }
        public ActionResult Userchargroup()
        {
            return View();
        }
        public ActionResult charexmap()
        {
            return View();
        }
        public ActionResult campaignlist()
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
            string img = HttpContext.Request["meetimg"].ToString(); 
            //下面开始进行数据的同步
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
                filet.url = "~" + filepath; //Server.MapPath(filepath);
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
            img = filelist[0].url;
            DateTime contentTime =DateTime.Parse( HttpContext.Request["contentTime"].ToString());
            USER_INFO user = Session["user"] as USER_INFO;
            meetingtable mt=  ChartOnlinelib.Usertakeon(user,local,content,title,contentTime,img);
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
        //专门用来获取指定群组的用户列表
        public ActionResult GetGroupUser(int GID)
        {
          
            List<pageGroupUser> list = ChartOnlineGroup.GetUserG(GID);
            return Json(list);
        }
        //获取当前用户的所获的用户的聊天群组
        public ActionResult Getgroup()
        {
            USER_INFO user = Session["user"] as USER_INFO;
            List<ViewGroup> list = ChartOnlineGroup.GetGroup(user.USER_ID.ToString());
            return Json(list);
        }
        //提交群组聊天
        public ActionResult PostGroupMessage(string TGID,string message)
        {
            USER_INFO user = Session["user"] as USER_INFO;
            if (ChartOnlineGroup.PostMessageG(user, int.Parse(TGID), message))
            {
                return Json(1);
            }
            else
            { 
             return Json(0);
            }
        }
        //获得当前群组的聊天记录
        public ActionResult GetgroupMessage(int GID, string dtimestring  )
        {
            
            DateTime dtime = DateTime.Parse(dtimestring);
            List<View_Multimessage> list = ChartOnlineGroup.getmeesage(GID, dtime);
            return Json(redmomery.Common.SerializerHelper.SerializeToString(list));
        }
        #endregion
    }
}
