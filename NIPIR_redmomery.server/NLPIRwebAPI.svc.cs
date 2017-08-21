using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using NLPIR_redmomery;
namespace NIPIR_redmomery.server
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class NLPIRwebAPI
    {
        static NLPIR_ICTCLAS_C nlpir = new NLPIR_ICTCLAS_C();
        // 要使用 HTTP GET，请添加 [WebGet] 特性。(默认 ResponseFormat 为 WebMessageFormat.Json)
        // 要创建返回 XML 的操作，
        //     请添加 [WebGet(ResponseFormat=WebMessageFormat.Xml)]，
        //     并在操作正文中包括以下行:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // 在此处添加操作实现
            return;
        }
        [OperationContract]
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
            byte[] bytes=Encoding.Default.GetBytes(s1);
            List<string> resultjson = new List<string>();
            for (int i = 0; i < results.Length; i++)
            {
                StringBuilder str = new StringBuilder();
                str.Append("词语的名称："+Encoding.Default.GetString(bytes,results[i].start,results[i].length)+",");
                str.Append("词性："+results[i].sPos+",");
                str.Append("权值：" + results[i].weight + ";");
                resultjson.Add(str.ToString());
            }
            return resultjson;
        }
        // 在此处添加更多操作并使用 [OperationContract] 标记它们
    }
}
