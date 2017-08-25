using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLRedmomery
{
    public class Program
    {
        static void Main(string[] args)
        {
            example();
        }
        public static void example()
        {
            Console.WriteLine(DateTime.ParseExact("9999-12-30-", "yyyy-MM-dd-", null));

            NLPIR_ICTCLAS_C nlpir = new NLPIR_ICTCLAS_C();
            //string s2 = "ICTCLAS在国内973专家组组织的评测中活动获得了第一名，在第一届国际中文处理研究机构SigHan组织的评测中都获得了多项第一名。陈增辉";
            string s1 = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\github\team\redmomery\调试\新建文本文档.txt");
            int count = nlpir.GetParagraphProcessAWordCount(s1);
            result_t[] results = nlpir.ParagraphProcessAW(count);
          //  int i = 1;
            byte[] bytes = Encoding.Default.GetBytes(s1);
            foreach (result_t r in results)
            {
                string sWhichDic = "";
                switch (r.word_type)
                {
                    case 0:
                        sWhichDic = "核心词典";
                        break;
                    case 1:
                        sWhichDic = "用户词典";
                        break;
                    case 2:
                        sWhichDic = "专业词典";
                        break;
                    default:
                        break;
                }
                Console.Write(Encoding.Default.GetString(bytes, r.start, r.length)+"/"+r.sPos);
                
            }
            Console.WriteLine("下面开始测试关键词提取功能");
            string keys = nlpir.KeyExtractGetKeyWords(s1, 10, true);
            Console.WriteLine(keys);
            Console.WriteLine("测试发现新词功能：");
            string newwords = nlpir.NWFGetNewWords(s1);
            Console.WriteLine(newwords);
            Console.WriteLine(nlpir.NWFResult2UserDict());
            Console.WriteLine("下面测试批量发现新词");
            nlpir.NWFBatch_Start();
            nlpir.NWFBatch_AddMem(s1);
            nlpir.NWFBatch_Complete();
            Console.WriteLine(nlpir.NWFBatch_GetResult());
            Console.WriteLine(nlpir.NWFResult2UserDict());
            Console.WriteLine("下面测试实体抽取功能");
            long handle = nlpir.DEParseDocE(s1, "mgc#ngd", true, nFuncRequired.ALL_REQUIRED);
            string res = nlpir.DEGetResult(handle);
            Console.WriteLine(res);
            Console.WriteLine(nlpir.DEGetSentimentScore(handle));
            Console.Read();

        }
    }
}
