using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
namespace NLPIR_redmomery
{
    public static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.UtcNow.ToString());
            NLPIR_ICTCLAS_C nlpir = new NLPIR_ICTCLAS_C();
            string s1 = "ICTCLAS在国内973专家组组织的评测中活动获得了第一名，在第一届国际中文处理研究机构SigHan组织的评测中都获得了多项第一名。";
            s1 = redmomery.command.createlog.readTextFrompath(@"D:\题库系统\redMomery\redmomery\调试\新建文本文档.txt");
            if (nlpir.Init())
            {
                Console.WriteLine("初始化成功");
            }
            
            int count = nlpir.GetParagraphProcessAWordCount(s1);
            result_t[] results = nlpir.ParagraphProcessAW(count);
            int i = 1;
            byte[] bytes = Encoding.Default.GetBytes(s1);
            Console.WriteLine(DateTime.UtcNow.ToString());
           

            Console.Read();
        }
    }
}
