using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using redmomery.Model;
using redmomery.DAL;
using redmomery.librarys;
using System.IO;
namespace redMomery.Controllers
{
    public class UserController : Controller//这个服务主要用来进行用户的注册功能测试
    {
        //
        // GET: /User/Login

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult register()
        {
            return View();
        }
        public ActionResult loginuser()
        {
            return View();
        }
        public ActionResult Login()
        { 
           //专门由于用户主持的
            string result = "";
            string username = HttpContext.Request["username"].ToString();
            string psw = HttpContext.Request["password"].ToString();
            USER_INFODAL dal=new USER_INFODAL();
            USER_INFO user=dal.get(username,psw);
            if (user != null)
            {
                Session["user"] = user;
                ViewData.Add("userimg", "~/" + user.USER_IMG);
                ViewData.Add("username", user.USER_NETNAME);
                USER_INFO pageuser = new USER_INFO();
                pageuser.USER_NETNAME = user.USER_NETNAME;
                pageuser.USER_SEX = user.USER_SEX;
                pageuser.USER_IMG = user.USER_IMG;
                pageuser.USER_ID = user.USER_ID;
                return Json(pageuser);
            }
            else
             {
                 result = "登录失败请检查密码";
                 return Json(result);
            }
        }
        public ActionResult RegisterUser()
        {
            string resultss = "";
            try
            {
                USER_INFO users = new USER_INFO();
                HttpContext.Response.ContentType = "application/json:charset=utf-8";

                string USER_NAME = HttpContext.Request["usernamec"].ToString();
                string USER_SEX = HttpContext.Request["sexc"].ToString();
                string USER_JOB = HttpContext.Request["jobc"].ToString();
                string USER_BIRTHDAY = HttpContext.Request["userbirthday"].ToString();
                string USER_ADDRESS = HttpContext.Request["addressec"].ToString();
                string USER_PHONE = HttpContext.Request["photoc"].ToString();
                string USER_EMEIL = HttpContext.Request["Emailc"].ToString();
                string USER_NETNAME = HttpContext.Request["Netnamec"].ToString();
                string USER_PSWD = HttpContext.Request["passwordc"].ToString();
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
                //开始绑定用户列表
                string USER_IMG = filelist[0].url;
                string usractivate=System.Configuration.ConfigurationManager.AppSettings["activationurl"].ToString();
                users=  Userlib.RegisterUser(usractivate, USER_NAME, USER_SEX, USER_JOB, USER_BIRTHDAY, USER_ADDRESS, USER_PHONE, USER_EMEIL, USER_NETNAME, USER_PSWD, USER_IMG);
                if (users != null)
                {
                    users.USER_NAME = null;
                    users.USER_PSWD = null;
                    users.USER_ADDRESS = null;
                    users.USER_BIRTHDAY = null;
                }
                return Json(users);
            }
            catch(Exception ex)
            {
                resultss = "失败:";
                resultss += ex.Data.ToString() + ";" + ex.HResult.ToString() + ";" + ex.Source.ToString() + ";" + ex.Message.ToString() + ";"+ex.StackTrace.ToString();
            }

            return Json(resultss);
        }
        public ActionResult activation(string md5)
        {
            string md5s = md5;
            //开始执行激活指令
            USER_INFO user = (new USER_INFODAL()).getByMD5(md5);
            user.ISPASS = 0;
            if ((new USER_INFODAL()).update(user))
            {
                return View();
            }
            else
            {
                Response.Redirect("/User/activafault");
            }
            return null;
        }
        public ActionResult activafault()
        {
            return View();
        }
        public ActionResult SendEmail(string subject,string username,string psw,string sender,string redcive,string content)
        {
            
                string result = "";
                try
                {
                redmomery.command.PageMail pageMail = new redmomery.command.PageMail();
                pageMail.ishtml = false;
                pageMail.password = "chenliu0904";
                pageMail.userName = "18720728252";
                pageMail.senderAddress = "18720728252@163.com";
                pageMail.reciveaddres.Add("3045316072@qq.com");
                pageMail.servehf = "smtp.163.com";
                pageMail.port = 25;
                pageMail.subject = "测试网络";
                pageMail.content = "这个邮件是我自己发给自己，用阿里测试自己的邮箱服务器的状态的";
                pageMail.Sends();
            }
            catch(Exception ex)
            {
                result += ex.Source.ToString();
                result += ex.TargetSite.ToString();
                result += ex.Message.ToString();
            }
            return  Json(result);
        }
    }
}
