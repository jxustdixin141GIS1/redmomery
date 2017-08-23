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
       

        #region 程序运行维护变量
        private static bool _Init = false;
        private static bool _KeyExtractInit = false;
        private static bool _NewWordFinderInit = false;
        private static bool _DEInit = false;
        private static bool _NWIStart = false;
        private static bool _NWIComplete = false;

        #endregion
        #region 注意凡是在这个范围内的方法被标记为private 即时表示这个方法已经被放弃，不能够继续使用
        #region 非函数程序
        public NLPIR_ICTCLAS_C()
        {
            //这个方法开始针对环境进行初始化
            Init();
            KeyExtractInit();
            NewWordFinderInit();
            DEInit();
        }

        /// <summary>
        /// 词性标注集
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string nominal(string code)
        {
            switch (code.ToLower().Trim())
            {
                #region 名词
                case "n": return "名词";
                case "nr": return "人名";
                case "nr1": return "汉语姓氏";
                case "nr2": return "汉语名字";
                case "nri": return "日语人名";
                case "nrf": return "音译人名";
                case "ns": return "地名";
                case "nsf": return "音译地名";
                case "nt": return "机构团体名";
                case "nz": return "其他专名";
                case "nl": return "名词性惯用语";
                case "ng": return "名词性语素";
                #endregion
                #region 时间词
                case "t": return "时间词";
                case "tg": return "时间词语素";
                #endregion
                #region 处所词
                case "s": return "处所词";
                #endregion
                #region 方位词
                case "f": return "方位词";
                #endregion
                #region 动词
                case "v": return "动词";
                case "vd": return "名动词";
                case "vn": return "动词“是”";
                case "vyon": return "动词“有”";
                case "vf": return "趋向动词";
                case "vx": return "形式动词";
                case "vi": return "不及物动词";
                case "vl": return "动词性惯用语";
                case "vg": return "动词性语素";
                #endregion
                #region 形容词
                case "a": return "形容词";
                case "ad": return "副形词";
                case "an": return "名形词";
                case "ag": return "形容词性语素";
                case "al": return "形容词性惯用词";
                #endregion
                #region 区别词
                case "b": return "区别词";
                case "bl": return "区别词惯用词";
                #endregion
                #region 状态词
                case "z": return "状态词";
                #endregion
                #region 代词
                case "r": return "代词";
                case "rr": return "人称代词";
                case "rz": return "指示代词";
                case "rzt": return "时间指示代词";
                case "rzs": return "处所指示代词";
                case "rzv": return "谓词指示代词";
                case "ry": return "疑问代词";
                case "ryt": return "时间疑问代词";
                case "rys": return "处所疑问代词";
                case "ryy": return "谓词性疑问代词";
                case "rg": return "代词性语素";
                #endregion
                #region 数词
                case "m": return "数词";
                case "mq": return "数量词";
                #endregion
                #region 量词
                case "q": return "量词";
                case "qv": return "动量词";
                case "qt": return "时量词";
                #endregion
                #region 副词
                case "d": return "副词";
                #endregion
                #region 介词
                case "p": return "介词";
                case "pba": return "介词“把”";
                case "pbei": return "介词“被”";
                #endregion
                #region 连词
                case "c": return "连词";
                case "cc": return "并列连词";
                #endregion
                #region 助词
                case "u": return "着";
                case "uzhe": return "喽";
                case "uguo": return "过";
                case "ude1": return "的 底";
                case "ud2": return "地";
                case "ude3": return "得";
                case "usuo": return "所";
                case "udeng": return "等 等等 云云";
                case "uyy": return "一样 一般 似的 般";
                case "udh": return "的话";
                case "uls": return "来讲 来说 而言 说来";
                case "uzhi": return "之";
                case "ulian": return "连（“连小学生都会”）";
                #endregion
                #region 叹词
                case "e": return "叹词";
                #endregion
                #region 语气词
                case "y": return "语气词";
                #endregion
                #region 拟声词
                case "o": return "拟声词";
                #endregion
                #region 前缀
                case "h": return "前缀";
                #endregion
                #region 后缀
                case "k": return "后缀";
                #endregion
                #region 字符串
                case "x": return "字符串";
                case "xe": return "Email字符串";
                case "xs": return "微博会话分隔符";
                case "xm": return "表情符号";
                case "xu": return "网址url";
                #endregion
                #region 标点符号
                case "w": return "标点符号";
                case "wkz": return "左括号，全角：（ 〔  ［  ｛  《 【  〖 〈   半角：( [ { <";
                case "wky": return "右括号，全角：） 〕  ］ ｝ 》  】 〗 〉 半角： ) ] { >";
                case "wyz": return "左引号，全角：“ ‘ 『  ";
                case "wyy": return "右引号，全角：” ’ 』 ";
                case "wj": return "句号，全角：。";
                case "ww": return "问号，全角：？ 半角：?";
                case "wt": return "叹号，全角：！ 半角：!";
                case "wd": return "逗号，全角：， 半角：,";
                case "wf": return "分号，全角：； 半角： ;";
                case "wn": return "顿号，全角：、";
                case "wm": return "冒号，全角：： 半角： :";
                case "ws": return "省略号，全角：……  …";
                case "wp": return "破折号，全角：——   －－   ——－   半角：---  ----";
                case "wb": return "百分号千分号，全角：％ ‰   半角：%";
                case "wh": return "单位符号，全角：￥ ＄ ￡  °  ℃  半角：$";
                #endregion
                default: return "未知词";
            }
        }
        #endregion 数据库
        #region NLPIR库函数封装
        #region 预判断
        private static void JudgeInit()
        {
            if (!_Init)
            {
                throw new Exception("未进行初始化");
            }
        }
        private static void JudgeNWIStart()
        {
            JudgeNWFInit();
            if (!_NWIStart)
            {
                throw new Exception("未启动新词识别!");
            }
        }
        private static void JudgeNWIComplete()
        {
            JudgeNWFInit();
            if (!_NWIComplete)
            {
                throw new Exception("未结束新词识别！");
            }
        }
        #endregion
        #region 初始化，退出
        /// <summary>
        /// 初始化分词环境
        /// </summary>
        /// <param name="sInitDirPath">data所在的根目录</param>
        /// <param name="encoding">字符串的编码格式</param>
        /// <returns>返回初始结果</returns>
        public bool Init(string sInitDirPath, NLPIR_CODE encoding = NLPIR_CODE.UTF8_CODE)
        {
            _Init = NLPIR_Init(sInitDirPath, (int)encoding);
            return _Init;
        }
        public bool Init()
        {
            _Init = NLPIR_Init(rootDir);
            return _Init;
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public bool Exit()
        {
            _Init = false;
            return NLPIR_Exit();
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sFilename"></param>
        /// <param name="bOverwrite"></param>
        /// <returns></returns>
        public uint ImportUserDict(string sFilename, bool bOverwrite = true)
        {
            JudgeInit();
            return NLPIR_ImportUserDict(sFilename, bOverwrite);

        }
        /// <summary>
        /// 处理文本信息
        /// </summary>
        /// <param name="sParagraph">待处理的文本</param>
        /// <param name="bPOStagged">是否进行词性标注</param>
        /// <returns>处理之后文本</returns>
        public string ParagraphProcess(string sParagraph, int bPOStagged = 1)
        {
            JudgeInit();
            IntPtr result = NLPIR_ParagraphProcess(sParagraph, bPOStagged);
            return Marshal.PtrToStringAnsi(result);
        }
        /// <summary>
        /// 处理文本文件
        /// </summary>
        /// <param name="sSrcFilename">源文件</param>
        /// <param name="sDestFilename">目标文件</param>
        /// <param name="bPOStagged">是否进行词性标注</param>
        /// <returns>执行成功返回处理速度</returns>
        public double FileProcess(string sSrcFilename, string sDestFilename, bool bPOStagged)
        {
            JudgeInit();
            return NLPIR_FileProcess(sSrcFilename, sDestFilename, bPOStagged ? 1 : 0);
        }
        /// <summary>
        /// 处理文本内容，获取分词数。
        /// </summary>
        /// <param name="sParagraph">文本内容</param>
        /// <returns>分词数。</returns>
        public int GetParagraphProcessAWordCount(string sParagraph)
        {
            JudgeInit();
            return NLPIR_GetParagraphProcessAWordCount(sParagraph);
        }
        /// <summary>
        /// 处理文本内容 并获取结果词组
        /// </summary>
        /// <param name="sParagraph">待处理的文本内容</param>
        /// <param name="bUserdir">是否启动用户词典</param>
        /// <returns>分词结果数组</returns>
        public result_t[] ParagraphProcessA(string sParagraph, bool bUserdir = true)
        {
            JudgeInit();
            int nCount = 0;
            IntPtr intpre = NLPIR_ParagraphProcessA(sParagraph, out nCount, bUserdir);
            result_t[] result = new result_t[nCount];
            for (int i = 0; i < nCount; i++, intpre = new IntPtr(intpre.ToInt32() + Marshal.SizeOf(typeof(result_t))))
            {
                result[i] = (result_t)Marshal.PtrToStructure(intpre, typeof(result_t));
            }
            return result;
        }
        /// <summary>
        /// 处理文本内容
        /// </summary>
        /// <param name="nCount"></param>
        /// <returns>处理结果集</returns>
        public result_t[] ParagraphProcessAW(int nCount)
        {
            JudgeInit();
            result_t[] results = new result_t[nCount];
            NLPIR_ParagraphProcessAW(nCount, results);
            return results;
        }
        /// <summary>
        /// 向用户词典中添加词语
        ///  格式：添加词\t词性
        /// </summary>
        /// <param name="sWord">要添加的词语</param>
        /// <returns>1：成功  0：失败</returns>
        public int AddUserWord(string sWord)
        {
            JudgeInit();
            return NLPIR_AddUserWord(sWord);
        }
        /// <summary>
        /// 保存用户的词典
        /// </summary>
        /// <returns>1:成功 0:表示失败</returns>
        public int SaveTheUsrDic()
        {
            JudgeInit();
            return NLPIR_SaveTheUsrDic();
        }
        /// <summary>
        ///     删除某个词语从用户词典中
        /// </summary>
        /// <param name="sWord">需要删除的词语</param>
        /// <returns>-1：表示词语不存在于用户词典中 否者返回被删除的词语的句柄</returns>
        public int DelUsrWord(string sWord)
        {
            JudgeInit();
            return NLPIR_DelUsrWord(sWord);
        }
        /// <summary>
        /// 导入关键词黑名单
        /// </summary>
        /// <param name="sFilename">文件</param>
        /// <returns>成功黑名单个数</returns>
        public uint ImportKeyBlackList(string sFilename)
        {
            JudgeInit();
            return NLPIR_ImportKeyBlackList(sFilename);
        }
        /// <summary>
        /// 提取文章的指纹信息
        /// </summary>
        /// <param name="sLine">文章</param>
        /// <returns></returns>
        public uint FingerPrint(string sLine)
        {
            JudgeInit();
            return NLPIR_FingerPrint(sLine);
        }
        /// <summary>
        /// 设置标注集
        /// </summary>
        /// <param name="nPOSmap"></param>
        /// <returns>1：成功</returns>
        public int SetPOSmap(int nPOSmap)
        {
            JudgeInit();
            return NLPIR_SetPOSmap(nPOSmap);
        }
        /// <summary>
        /// 细分词语
        /// </summary>
        /// <param name="sLine"></param>
        /// <returns></returns>
        public string FinerSegment(string sLine)
        {
            JudgeInit();
            IntPtr intptr = NLPIR_FinerSegment(sLine);
            return Marshal.PtrToStringAnsi(intptr);
        }
        /// <summary>
        /// 获取英文原型
        /// </summary>
        /// <param name="sWord"></param>
        /// <returns></returns>
        public string GetEngWordOrign(string sWord)
        {
            JudgeInit();
            IntPtr intptr = NLPIR_GetEngWordOrign(sWord);
            return Marshal.PtrToStringAnsi(intptr);

        }
        /// <summary>
        /// 获取输入文本的词，词性，频统计结果，按照词频大小排序  来源字符串
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        public string WordFreqStat(string sText)
        {
            JudgeInit();
            IntPtr intptr = NLPIR_WordFreqStat(sText);
            return Marshal.PtrToStringAnsi(intptr);
        }
        /// <summary>
        /// 获取输入文本的词，词性，频统计结果 来源于文件
        /// </summary>
        /// <param name="sFilename"></param>
        /// <returns></returns>
        public string FileWordFreqStat(string sFilename)
        {
            JudgeInit();
            IntPtr intptr = NLPIR_FileWordFreqStat(sFilename);
            return Marshal.PtrToStringAnsi(intptr);
        }
        #endregion
        #region 关键词提取

        #region 预判断
        public static void JudgeKeyExtractInit()
        {
            if (!_KeyExtractInit)
            {
                throw new Exception("未进行关键词提取初始化");
            }
        }
        #endregion
        /// <summary>
        /// 初始化环境
        /// </summary>
        /// <param name="sInitdirpath"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public bool KeyExtractInit(string sInitdirpath, NLPIR_CODE encoding = NLPIR_CODE.GBK_CODE)
        {
            _KeyExtractInit = KeyExtract_Init(sInitdirpath, (int)encoding);
            return _KeyExtractInit;

        }
        /// <summary>
        /// 初始化变量
        /// </summary>
        /// <returns></returns>
        public bool KeyExtractInit()
        {
            _KeyExtractInit = KeyExtract_Init(rootDir);
            return _KeyExtractInit;
        }
        /// <summary>
        /// 退出关键词提取
        /// </summary>
        /// <returns></returns>
        public bool KeyExtractExit()
        {
            _KeyExtractInit = false;
            return KeyExtract_Exit();
        }
        /// <summary>
        /// 提取关键词
        /// </summary>
        /// <param name="sLine">需要提取的文本</param>
        /// <param name="nMaxKeyLimit">最大的关键词数</param>
        /// <param name="bWeightOut">是否输出最权重</param>
        /// <returns></returns>
        public string KeyExtractGetKeyWords(string sLine, int nMaxKeyLimit = 50, bool bWeightOut = false)
        {
            JudgeKeyExtractInit();
            IntPtr intpre = KeyExtract_GetKeyWords(sLine, nMaxKeyLimit, bWeightOut);
            return Marshal.PtrToStringAnsi(intpre);
        }
        /// <summary>
        /// 从指定的文件中提取关键词
        /// </summary>
        /// <param name="sFilename">对应的文件</param>
        /// <param name="nMaxKeyLimit">关键词最大数</param>
        /// <param name="bWeightOut">是否输出权重</param>
        /// <returns>关键词</returns>
        public string KeyExtractGetFileKeyWords(string sFilename, int nMaxKeyLimit = 50, bool bWeightOut = false)
        {
            JudgeKeyExtractInit();
            IntPtr intpre = KeyExtract_GetFileKeyWords(sFilename, nMaxKeyLimit, bWeightOut);
            return Marshal.PtrToStringAnsi(intpre);
        }

        #endregion
        #region  新词发现
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public bool NewWordFinderInit()
        {
            _NewWordFinderInit = NWF_Init(rootDir);
            return _NewWordFinderInit;
        }
        public bool NewWordFinderInit(string sRootdir, NLPIR_CODE coding = NLPIR_CODE.GBK_CODE)
        {
            _NewWordFinderInit = NWF_Init(sRootdir, (int)coding);
            return _NewWordFinderInit;
        }
        /// <summary>
        /// 退出新词发现模块
        /// </summary>
        /// <returns></returns>
        public bool NewWordFinderExit()
        {
            _NewWordFinderInit = false;
            return NWF_Exit();
        }
        /// <summary>
        /// 判断是否已经进行新词发现模块初始化
        /// </summary>
        public static void JudgeNWFInit()
        {
            if (!_NewWordFinderInit)
            {
                throw new Exception("新词发现模块没有初始化");
            }
        }
        /// <summary>
        /// 发现新词
        /// </summary>
        /// <param name="sLine">词源</param>
        /// <param name="nMaxKeyLimit">词数限制</param>
        /// <param name="bWeight">是否输出权重</param>
        /// <returns></returns>
        public string NWFGetNewWords(string sLine, int nMaxKeyLimit = 50, bool bWeight = false)
        {
            JudgeNWFInit();
            IntPtr intptr = NWF_GetNewWords(sLine, nMaxKeyLimit, bWeight);
            return Marshal.PtrToStringAnsi(intptr);
        }
        /// <summary>
        /// 从文件中寻找新词
        /// </summary>
        /// <param name="sTextFile"></param>
        /// <param name="nMaxKeyLimit"></param>
        /// <param name="bWeight"></param>
        /// <returns></returns>
        public string NWFGetFileNewWords(string sTextFile, int nMaxKeyLimit = 50, bool bWeight = false)
        {
            JudgeNWFInit();
            IntPtr intptr = NWF_GetFileNewWords(sTextFile, nMaxKeyLimit, bWeight);
            return Marshal.PtrToStringAnsi(intptr);
        }
        /// <summary>
        /// 将发现的新词写入用户词典
        /// </summary>
        /// <returns></returns>
        public uint NWFResult2UserDict()
        {
            JudgeNWFInit();
            return NWF_Result2UserDict();
        }
        /// <summary>
        /// 启动批量新词发现
        /// </summary>
        /// <returns></returns>
        public bool NWFBatch_Start()
        {
            JudgeNWFInit();
            _NWIStart = NWF_Batch_Start();
            return _NWIStart;
        }
        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="sFilename">语料文件</param>
        /// <returns></returns>
        public int NWFBatch_AddFile(string sFilename)
        {
            JudgeNWFInit();
            JudgeNWIStart();
            return NWF_Batch_AddFile(sFilename);
        }
        /// <summary>
        /// 添加待发现的新词内存
        /// </summary>
        /// <param name="sText"></param>
        /// <returns></returns>
        public bool NWFBatch_AddMem(string sText)
        {
            JudgeNWFInit();
            JudgeNWIStart();
            return NWF_Batch_AddMem(sText);
        }
        /// <summary>
        /// 完成语料添加，需要在star运行之后才有效
        /// </summary>
        /// <returns></returns>
        public bool NWFBatch_Complete()
        {
            JudgeNWFInit();
            JudgeNWIStart();
            _NWIStart = false;
            _NWIComplete = NWF_Batch_Complete();
            return _NWIComplete;
        }
        /// <summary>
        /// 从中获取分词结果
        /// </summary>
        /// <param name="bWeight"></param>
        /// <returns></returns>
        public string NWFBatch_GetResult(bool bWeight = false)
        {
            JudgeNWFInit();
            JudgeNWIComplete();
            IntPtr intptr = NWF_Batch_GetResult(bWeight);
            return Marshal.PtrToStringAnsi(intptr);
        }
        #endregion
        #region  实体抽取功能
        public bool DEInit(string srootDir, NLPIR_CODE encoding = NLPIR_CODE.GBK_CODE)
        {
            _DEInit = DE_Init(srootDir, (int)encoding) == 1 ? true : false;
            return _DEInit;
        }
        public bool DEInit()
        {
            int res = DE_Init(rootDir, (int)NLPIR_CODE.GBK_CODE);
            _DEInit = res == 1 ? true : false;
            return _DEInit;
        }
        public static void JudgeDEInit()
        {
            if (!_DEInit)
            {
                throw new Exception("实体抽取和情感判断没有进行初始化");
            }
        }

        public void DEExit()
        {
            _DEInit = false;
            DE_Exit();
        }

        public long DEParseDocE(string sText, string UserDefPos, bool bSummayNeeded = true, nFuncRequired funcrequired = nFuncRequired.ALL_REQUIRED)
        {
            JudgeDEInit();
            return DE_ParseDocE(sText, UserDefPos, bSummayNeeded, (uint)funcrequired);
        }
        public string DEGetResult(long handle, nDocExtractType docextractype = nDocExtractType.DOC_EXTRACT_TYPE_PERSON)
        {
            JudgeDEInit();
            IntPtr intptr = DE_GetResult(handle, (int)docextractype);
            return Marshal.PtrToStringAnsi(intptr);
        }

        public int DEGetSentimentScore(long handle)
        {
            JudgeDEInit();
            return DE_GetSentimentScore(handle);
        }


        public void DEReleaseHandle(long handle)
        {
            JudgeDEInit();
            DE_ReleaseHandle(handle);
        }
        public uint DEImportSentimentDict(string sFilename)
        {
            JudgeDEInit();
            return DE_ImportSentimentDict(sFilename);
        }
        public uint DEImportUserDict(string sFilename, bool bOverwite = true)
        {
            JudgeDEInit();
            return DE_ImportUserDict(sFilename, bOverwite);
        }
        public uint DEImportKeyBlackList(string sFilename)
        {
            JudgeDEInit();
            return DE_ImportKeyBlackList(sFilename);
        }
        public int DEComputeSentimentDoc(string sText)
        {
            JudgeDEInit();
            return DE_ComputeSentimentDoc(sText);
        }
        public string GetLastErrMsg()
        {
            JudgeDEInit();
            IntPtr intptr = DE_GetLastErrMsg();
            return Marshal.PtrToStringAnsi(intptr);
        }
        #endregion
        #endregion
    }
}
namespace NLRedmomery
{
    public partial class NLPIR_ICTCLAS_C
    {
        #region NLPIR_ICTCLA

        //初始化
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Init")]
        private static extern bool NLPIR_Init(string sInitDirPath, int encoding = (int)NLPIR_CODE.GBK_CODE);

        //退出
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Exit")]
        private static extern bool NLPIR_Exit();

        //导入用户词典
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Exit")]
        private static extern uint NLPIR_ImportUserDict(string sFilename, bool bOverwrite = true);

        //解析输入的文本
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcess")]
        private static extern IntPtr NLPIR_ParagraphProcess(string sParagraph, int bPOStagged = 1);

        //处理文本返回
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessA")]
        private static extern IntPtr NLPIR_ParagraphProcessA(string sParagraph, out int nResultCount, bool bUseeDir = true);

        //处理文本文件
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FileProcess")]
        private static extern double NLPIR_FileProcess(string sSrcFilename, string sDestFilename, int bPOStagged = 1);

        //处理文本，返回分词结果数目
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetParagraphProcessAWordCount")]
        private static extern int NLPIR_GetParagraphProcessAWordCount(string sParagraph);

        //处理文本
        [DllImport((NLPIRPath), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessAW")]
        private static extern void NLPIR_ParagraphProcessAW(int nCount, [Out, MarshalAs(UnmanagedType.LPArray)] result_t[] result);

        //向用户词点中添加用户自定义词语
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_AddUserWord")]
        private static extern int NLPIR_AddUserWord(string sWord);

        //保存用户词典
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_SaveTheUsrDic")]
        private static extern int NLPIR_SaveTheUsrDic();

        //删除用户词典
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_DelUsrWord")]
        private static extern int NLPIR_DelUsrWord(string sWord);

        //设置关键词黑名单
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ImportKeyBlackList")]
        private static extern uint NLPIR_ImportKeyBlackList(string sFilename);

        //提取出文章的指纹信息
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FingerPrint")]
        private static extern uint NLPIR_FingerPrint(string sLine);

        //设置标注集
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_SetPOSmap")]
        private static extern int NLPIR_SetPOSmap(int nPOSmap);

        //细分分词结果
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FinerSegment")]
        private static extern IntPtr NLPIR_FinerSegment(string sLine);

        //得到英文单词的原型
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetEngWordOrign")]
        private static extern IntPtr NLPIR_GetEngWordOrign(string sWord);

        //获取输入文本的词，词性，频统计结果，按照词频大小排序  来源字符串
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_WordFreqStat")]
        private static extern IntPtr NLPIR_WordFreqStat(string sText);

        //获取输入文本的词，词性，频统计结果 来源于文件
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FileWordFreqStat")]
        private static extern IntPtr NLPIR_FileWordFreqStat(string sFilename);
        #endregion
        #region KeyExtract
        //初始化关键词提取
        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_Init")]
        private static extern bool KeyExtract_Init(string sDatapath, int encode = (int)NLPIR_CODE.GBK_CODE);
        //退出关键词提取
        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_Exit")]
        private static extern bool KeyExtract_Exit();
        //从文本中提取关键词
        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_GetKeyWords")]
        private static extern IntPtr KeyExtract_GetKeyWords(string sLine, int nMaxKeyLimit = 50, bool bWeightOut = false);

        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_GetFileKeyWords")]
        private static extern IntPtr KeyExtract_GetFileKeyWords(string sFilename, int nMaxKeyLimit = 50, bool bWeightOut = false);

        #endregion
        #region 新词发现  NewWordFinder
        //初始化
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Init")]
        private static extern bool NWF_Init(string rootDir, int encoding = (int)NLPIR_CODE.GBK_CODE);
        //退出
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Exit")]
        private static extern bool NWF_Exit();
        //发现新词
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_GetNewWords")]
        private static extern IntPtr NWF_GetNewWords(string sLine, int nMaxKeyLimit = 50, bool bWeight = false);
        //发现文件中的新词
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_GetFileNewWords")]
        private static extern IntPtr NWF_GetFileNewWords(string sTextFile, int nMaxKeyLimit = 50, bool bWeight = false);
        //新词转化为用户词典
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Result2UserDict")]
        private static extern uint NWF_Result2UserDict();
        //批量识别新词启动
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Batch_Start")]
        private static extern bool NWF_Batch_Start();
        //批量识别新词添加文件
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Batch_AddFile")]
        private static extern int NWF_Batch_AddFile(string sFilename);
        //批量识别新词添加内存中的文件内容
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Batch_AddMem")]
        private static extern bool NWF_Batch_AddMem(string sText);
        //批量识别新词导入完成
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Batch_Complete")]
        private static extern bool NWF_Batch_Complete();
        //批量识别新词获取结果
        [DllImport(NewWordFinder, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NWF_Batch_GetResult")]
        private static extern IntPtr NWF_Batch_GetResult(bool bWeight = false);
        #endregion
        #region   DocExtractor
        //初始化
        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_Init")]
        private static extern int DE_Init(string sPath, int Encoding = (int)NLPIR_CODE.GBK_CODE);
        //退出
        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_Exit")]
        private static extern void DE_Exit();
        //系列文档抽取处理函数
        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_ParseDocE")]
        private static extern long DE_ParseDocE(string sText, string sUserDefPos, bool bSummaryNeeded = true, uint nFuncRequired = (uint)nFuncRequired.ALL_REQUIRED);
        //得到实体对象
        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_GetResult")]
        private static extern IntPtr DE_GetResult(long handle, int nDocExtractType = (int)nDocExtractType.DOC_EXTRACT_TYPE_PERSON);

        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_GetSentimentScore")]
        private static extern int DE_GetSentimentScore(long handle);

        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_ReleaseHandle")]
        private static extern void DE_ReleaseHandle(long handle);

        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_ImportSentimentDict")]
        private static extern uint DE_ImportSentimentDict(string sFilename);

        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_ImportUserDict")]
        private static extern uint DE_ImportUserDict(string sFilename, bool bOverwite = true);

        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_ImportKeyBlackList")]
        private static extern uint DE_ImportKeyBlackList(string sFilename);

        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_ComputeSentimentDoc")]
        private static extern int DE_ComputeSentimentDoc(string sText);

        [DllImport(DElib, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "DE_GetLastErrMsg")]
        private static extern IntPtr DE_GetLastErrMsg();
        #endregion
    }

}
namespace NLRedmomery
{
    /// <summary>
    /// 标注集类型。
    /// </summary>
    public enum NLPIR_MAP
    {
        /// <summary>
        /// 计算所一级标注集。
        /// </summary>
        ICT_POS_MAP_FIRST = 1,

        /// <summary>
        /// 计算所二级标注集。
        /// </summary>
        ICT_POS_MAP_SECOND = 0,

        /// <summary>
        /// 北大一级标注集。
        /// </summary>
        PKU_POS_MAP_FIRST = 3,

        /// <summary>
        /// 北大二级标注集。
        /// </summary>
        PKU_POS_MAP_SECOND = 2
    }
    /// <summary>
    /// 编码类型。
    /// </summary>
    public enum NLPIR_CODE
    {
        /// <summary>
        /// GBK编码。
        /// </summary>
        GBK_CODE = 0,

        /// <summary>
        /// UTF8编码。
        /// </summary>
        UTF8_CODE = 1,

        /// <summary>
        /// BIG5编码。
        /// </summary>
        BIG5_CODE = 2,

        /// <summary>
        /// GBK编码，里面包含繁体字。
        /// </summary>
        GBK_FANTI_CODE = 3
    }
    /// <summary>
    /// 分词结果结构体。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct result_t
    {
        /// <summary>
        /// 词语在输入句子中的开始位置。
        /// </summary>
        public int start;

        /// <summary>
        /// 词语的长度。
        /// </summary>
        public int length;

        /// <summary>
        /// 词性ID值，可以快速的获取词性表。
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string sPos;

        /// <summary>
        /// 词性标注的编号。
        /// </summary>
        public int POS_id;

        /// <summary>
        /// 该词的内部ID号，如果是未登录词，设成0或者-1。
        /// </summary>
        public int word_ID;

        /// <summary>
        /// 区分用户词典，1是用户词典中的词，0非用户词典中的词。
        /// </summary>
        public int word_type;

        /// <summary>
        /// 权值。
        /// </summary>
        public int weight;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct _tDocExtractResult
    {


    }
    public enum nFuncRequired
    {
        PERSON_REQUIRED = 0x0001,
        LOCATION_REQUIRED = 0x0002,
        ORGANIZATION_REQUIRED = 0x0004,
        KEYWORD_REQUIRED = 0x0008,
        AUTHOR_REQUIRED = 0x0010,
        MEDIA_REQUIRED = 0x0100,
        COUNTRY_REQUIRED = 0x0200,
        PROVINCE_REQUIRED = 0x0400,
        ABSTRACT_REQUIRED = 0x0800,
        SENTIWORD_REQUIRED = 0x1000,
        SENTIMENT_REQUIRED = 0x2000,
        USER_REQUIRED = 0x4000,
        HTML_REMOVER_REQUIRED = 0x8000,
        ALL_REQUIRED = 0xffff
    }
    public enum nDocExtractType
    {
        DOC_EXTRACT_TYPE_PERSON = 0,//输出的人名
        DOC_EXTRACT_TYPE_LOCATION = 1,//输出的地名
        DOC_EXTRACT_TYPE_ORGANIZATION = 2,//输出的机构名
        DOC_EXTRACT_TYPE_KEYWORD = 3,//输出的关键词
        DOC_EXTRACT_TYPE_AUTHOR = 4,//输出的文章作者
        DOC_EXTRACT_TYPE_MEDIA = 5,//输出的媒体
        DOC_EXTRACT_TYPE_COUNTRY = 6,//输出的文章对应的所在国别
        DOC_EXTRACT_TYPE_PROVINCE = 7,//输出的文章对应的所在省份
        DOC_EXTRACT_TYPE_ABSTRACT = 8,//输出文章的摘要
        DOC_EXTRACT_TYPE_POSITIVE = 9,//输出文章的正面情感词
        DOC_EXTRACT_TYPE_NEGATIVE = 10,//输出文章的副面情感词
        DOC_EXTRACT_TYPE_TEXT = 11,//输出文章去除网页等标签后的正文
        DOC_EXTRACT_TYPE_USER = 12,//用户自定义的词类，第一个自定义词
    }
}