using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NLRedmomery;
using redmomery.librarys;
namespace redmomery.server
{
    /// <summary>
    /// forongsong 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public class forongsong : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public string guiji()
        {
            string s1 = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\github\team\redmomery\调试\新建文本文档.txt").Replace("\n\r", "").Replace("\r\n", "");
            List<Text_result> initlist = LBText.parseText(s1);
            for (int i = 0; i < initlist.Count; i++)
            {
                Console.WriteLine(i.ToString() + "::" + initlist[i].text + ":" + initlist[i].res.sPos);
            }
            for (int i = 0; i < initlist.Count; i++)
            {
                Console.Write(initlist[i].text);
            }
            Console.WriteLine();
            List<T_LocalText> timeinit1 = LBText.timeExtract(initlist);

            List<Res_T_LocalText> result = LBText.ConvertToRes(timeinit1);

            return redmomery.Common.SerializerHelper.SerializeToString(result);
        }
        [WebMethod]
        public List<Res_T_LocalText> ParseText(string stext)
        {
            try
            {
                string s1 = stext.Replace("\n\r", "").Replace("\r\n", "");
                List<Text_result> initlist = LBText.parseText(s1);
                for (int i = 0; i < initlist.Count; i++)
                {
                    Console.WriteLine(i.ToString() + "::" + initlist[i].text + ":" + initlist[i].res.sPos);
                }
                for (int i = 0; i < initlist.Count; i++)
                {
                    Console.Write(initlist[i].text);
                }
                Console.WriteLine();
                List<T_LocalText> timeinit1 = LBText.timeExtract(initlist);

                List<Res_T_LocalText> result = LBText.ConvertToRes(timeinit1);

                return result;//redmomery.Common.SerializerHelper.SerializeToString(result);
            }
            catch (Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Source.ToString() + "\n\r" + ex.StackTrace.ToString() + "\n\r" + ex.Message);
            }
            return new List<Res_T_LocalText>();
        }
    }
}
