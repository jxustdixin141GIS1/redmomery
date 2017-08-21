using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Services;
using NLPIR_redmomery;
namespace NIPIR_redmomery.server
{
    /// <summary>
    /// redmomertNLPIRwebpage 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public class redmomertNLPIRwebpage : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public List<string> Example()
        {
            NLPIR_ICTCLAS_C nlpir = new NLPIR_ICTCLAS_C();
            string s1 = "ICTCLAS在国内973专家组组织的评测中活动获得了第一名，在第一届国际中文处理研究机构SigHan组织的评测中都获得了多项第一名。";
            s1 = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\redMomery\redmomery\调试\新建文本文档.txt");
            if (nlpir.Init())
            {
                Console.WriteLine("初始化成功");
            }

            int count = nlpir.GetParagraphProcessAWordCount(s1);
            result_t[] results = nlpir.ParagraphProcessAW(count);
            byte[] bytes = Encoding.Default.GetBytes(s1);
            List<string> resultjson = new List<string>();
          resultjson.Add(s1);
            for (int i = 0; i < results.Length; i++)
            {
                StringBuilder str = new StringBuilder();
                str.Append("词语的名称：" + Encoding.Default.GetString(bytes, results[i].start, results[i].length) + ",");
                str.Append("词性：" + results[i].sPos + ",");
                str.Append("权值：" + results[i].weight + ";");
                resultjson.Add(str.ToString());
            }
            return resultjson;
        }
    }
}
