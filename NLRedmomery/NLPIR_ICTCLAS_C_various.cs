using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;


namespace NLRedmomery
{
    public partial class NLPIR_ICTCLAS_C
    {
        #region 文件初始化地址变量
        private const string NLpath = @"D:\题库系统\github\team\redmomery\NLRedmomery\bin\Debug\";
        private const string rootDir =NLpath+ @"..\..\NLPIR\";
        private const string libpath = NLpath + @"..\..\NLPIR\bin-win64\";
        private const string datapath = NLpath + @"..\..\NLPIR\Data\";
        private const string sentimentData = NLpath + @"..\..\NLPIR\Data\SentimentNew";
        private const string NLPIRPath = NLpath + @"..\..\NLPIR\bin-win64\NLPIR.dll";
        private const string KeyExtractPath = NLpath + @"..\..\NLPIR\bin-win64\KeyExtract.dll";
        private const string NewWordFinder = NLpath + @"..\..\NLPIR\bin-win64\NewWordFinder.dll";
        private const string DElib = NLpath + @"..\..\NLPIR\bin-win64\DocExtractor.dll";
        public  const string userDir = NLpath + @"..\..\NLPIR\bin-win64\\output\NewTermlist.txt";
        #endregion

    }
}
