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
namespace testtemp
{
    /// <summary>
    /// redmomeryserver 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    [Serializable]
    public class redmomeryserver : System.Web.Services.WebService
    {
        BBSTITLE_TABLEDAL dalTilte = new BBSTITLE_TABLEDAL();
        FILE_TABLEDAL dalFile = new FILE_TABLEDAL();
        //--------------------基础操作对象-------------------
        #region
        LB_INFODAL lbdal = new LB_INFODAL();
        #endregion
        //-------------------对象声明完成--------------------
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public List<string> GetXYByAddess(string address)
        {
            string[] xy = redmomery.command.Geocodingcommand.getGecodingByAddress(address);
            List<string> result = new List<string>();
            result.Add("lng:" + xy[0]);
            result.Add("lat:" + xy[1]);
            return result;
        }
        [WebMethod]
        public List<BBSTITLE_TABLE> CreateLBInfo()
        {
            List<BBSTITLE_TABLE> result = new List<BBSTITLE_TABLE>();
            return result;
        }
        [WebMethod]
        public List<string> getLBINfo(string LBname)
        {
            List<string> json = new List<string>();
            List<LB_INFO> result = new List<LB_INFO>();
            result = (List<LB_INFO>)lbdal.GetByName(LBname);
            for (int i = 0; i < result.Count; i++)
            {
                json.Add(redmomery.Common.SerializerHelper.SerializeToString(result[i]));
            }
            return json;
        }
        #region  文件上传
        [WebMethod]
        public void UploadLB()
        {
            int U_ID = 2;
            HttpContext.Current.Response.ContentType = "application/json:charset=utf-8";
            redmomery.Model.LB_INFO LB = new LB_INFO();
            string LBname = HttpContext.Current.Request["lbname"].ToString();
            string LBsex = HttpContext.Current.Request["lbsex"].ToString();
            string LBbirthday = HttpContext.Current.Request["lbbirthday"].ToString();
            string LBjob = HttpContext.Current.Request["lbwork"].ToString();
            string LBdecimal = HttpContext.Current.Request["lbaddress"].ToString();
            string LBdesignation = HttpContext.Current.Request["designation"].ToString();
            string LBlife = HttpContext.Current.Request["lblife"].ToString();
            string LBstory = HttpContext.Current.Request["lbstory"].ToString();
            #region  存储文件
            HttpFileCollection files = HttpContext.Current.Request.Files;
            List<FILE_TABLE> filelist = new List<FILE_TABLE>();
            for (int i = 0; i < files.Count; i++)
            {
                FILE_TABLE filet = new FILE_TABLE();
                HttpPostedFile file = files[i];
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
            HttpContext.Current.Response.Write(redmomery.Common.SerializerHelper.SerializeToString(LB));
            HttpContext.Current.Response.End();
        }
        [WebMethod]
        public void UploadFile()
        {
            HttpContext.Current.Response.ContentType = "application/json:charset=utf-8";
            string jsonCallbackFunName = HttpContext.Current.Request.Params["jsoncallback"].ToString();
            string strJson = "";
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string strFileName = HttpContext.Current.Request["filename"];
            byte[] b = new byte[files[0].ContentLength];
            System.IO.Stream fs = (System.IO.Stream)files[0].InputStream;
            fs.Read(b, 0, files[0].ContentLength);
            //定义一个内存流
            FileStream f = new FileStream(Server.MapPath("\\resource\\file") + "\\" + files[0].FileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(f);
            sw.Write(fs);
            sw.Close();
            f.Close();
            f = null;
            if (strJson == "")
            {
                strJson = "0";
            }
            HttpContext.Current.Response.Write(string.Format("{0}({1})", jsonCallbackFunName, strJson));
            HttpContext.Current.Response.End();
        }
        [WebMethod]
        public void uploadImage()
        {
            HttpContext.Current.Response.ContentType = "application/json:charset=utf-8";
            string strJson = "";
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string strFileName = HttpContext.Current.Request["lbphoto"];
            byte[] b = new byte[files[0].ContentLength];
            System.IO.Stream fs = (System.IO.Stream)files[0].InputStream;
            fs.Read(b, 0, files[0].ContentLength);
            //定义一个内存流
            FileStream f = new FileStream(Server.MapPath("\\resource\\file") + "\\" + files[0].FileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(f);
            bw.Write(b);
            bw.Close();
            f.Close();
            f = null;
            if (strJson == "")
            {
                strJson = "0";
            }
            List<string> json = new List<string>();
            HttpContext.Current.Response.Write("成功借宿");
            HttpContext.Current.Response.End();
        }
        public string storageFiles(HttpPostedFile flie)
        {
            string path = string.Empty;
            string filename = flie.FileName;
            byte[] b = new byte[flie.ContentLength];
            System.IO.Stream fs = flie.InputStream;
            fs.Read(b, 0, flie.ContentLength);
            //对于流进行MD5计算
            string MD5 = redmomery.Common.MD5Helper.GetStreamMD5(fs);
            //进行数据存储

            return path;
        }
        #endregion
        //--------------------------------论坛部分服务测试---------------------------------
        [WebMethod]
        public List<string> GetLBByTID(string T_ID)
        {
            List<string> json = new List<string>();
            List<LB_INFO> result = new List<LB_INFO>();
            result.AddRange(BBSBLL.GetLB_INFOByTID(int.Parse(T_ID)));
            for (int i = 0; i < result.Count; i++)
            {
                json.Add(redmomery.Common.SerializerHelper.SerializeToString(result[i]));
            }
            return json;
        }
        [WebMethod]
        public List<string> getTitle(string T_ID)
        {
            List<string> result = new List<string>();
            //T_ID若是为-1,表示错误
            View_T_U b_title = BBSBLL.getTitleViewID(int.Parse(T_ID));
            result.Add(redmomery.Common.SerializerHelper.SerializeToString(b_title));
            return result;
        }
        /// <summary>
        /// 这个用来得到的回复的帖子列表
        /// </summary>
        /// <param name="T_ID"></param>
        /// <returns></returns>
        [WebMethod]
        public List<string> getComentbyTitleId(string T_ID)
        {
            List<string> result = new List<string>();
            List<View_CT_U> CTs = BBSBLL.GetCLgetTID(int.Parse(T_ID));
            for (int i = 0; i < CTs.Count; i++)
            {
                result.Add(redmomery.Common.SerializerHelper.SerializeToString(CTs[i]));
            }
            return result;
        }
        [WebMethod]
        public List<string> addCommentByTID(string TID,string context)
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
            newct=dal.Get(CID);
            result.Add(redmomery.Common.SerializerHelper.SerializeToString(newct));
            return result;
        }
        [WebMethod]
        public List<string> deleteCommentByCID(string CID)
        {

            List<string> result = new List<string>();
            bool isdelte = BBSBLL.DeleteCommentByCID(int.Parse(CID));
            result.Add(isdelte.ToString());
            return result;
        }
        //--------------------------------论坛服务测试结束---------------------------------
        [WebMethod]
        public List<string> Bing(string keyword, string selectsite, int PageIndex)
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
            return list;// result;
        }
        [WebMethod]
        public string loadechowall()
        {
             List<Echowall> result=new List<Echowall>();
            string s=string.Empty;
            //try
            //{
               result = Echowalllib.getAllEchowall();
                return redmomery.Common.SerializerHelper.SerializeToString(result);
            //}
            //catch (Exception ex)
            //{
            //    redmomery.command.createlog.createlogs(ex.Source.ToString() + "\n\r" + ex.StackTrace.ToString() + "\n\r"+ex.Message);
            //}
            //return s;
        }
        [WebMethod]
        public string addechowall(string context)
        {
            string s = string.Empty;
            try
            {
                Echowall ec = Echowalllib.Addechowall(context);
                return redmomery.Common.SerializerHelper.SerializeToString(ec);
            }
            catch (Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Source.ToString() + "\n\r" + ex.StackTrace.ToString() + "\n\r");
            }
            return s;
        }
        [WebMethod]
        public string parseLbtext(string LBtext,string lbId)
        {
            return redmomery.Common.SerializerHelper.SerializeToString(LbTextParse.parseLbstored(int.Parse(lbId), LBtext));  
        }
        
        
    }
}
