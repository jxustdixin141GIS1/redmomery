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
        #region 对变量进行声明
        private static bool _Init = false;
        private static bool _KeyExtractInit = false;
        private static bool _NWIStart = false;
        private static bool _NWIComplete = false;
        private const string rootDir =        @"..\..\NLPIR\";
        private const string libpath =        @"..\..\NLPIR\bin-win64\";
        private const string datapath =       @"..\..\NLPIR\Data\";
        private const string NLPIRPath=       @"..\..\NLPIR\bin-win64\NLPIR.dll";
        private const string KeyExtractPath = @"..\..\NLPIR\bin-win64\KeyExtract.dll";
        #endregion
        #region 非函数程序
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
                    case "xs":return "微博会话分隔符";
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
        #endregion
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
            JudgeInit();
            if (!_NWIStart)
            {
                throw new Exception("未启动新词识别!");
            }
        }
        private static void JudgeNWIComplete()
        {
            JudgeInit();
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
        public result_t[] ParagraphProcessA(string sParagraph,bool bUserdir=true)
        {
            JudgeInit();
            int nCount = 0;
            IntPtr intpre = NLPIR_ParagraphProcessA(sParagraph, out nCount,bUserdir);
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
        public string GetKeyWords(string sLine,int nMaxKeyLimit=50,bool bWeightOut=false)
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
        public string GetFileKeyWords(string sFilename, int nMaxKeyLimit = 50, bool bWeightOut = false)
        {
            JudgeKeyExtractInit();
            IntPtr intpre = KeyExtract_GetFileKeyWords(sFilename, nMaxKeyLimit, bWeightOut);
            return Marshal.PtrToStringAnsi(intpre);
        }
         /// <summary>
         /// 导入用户自定的关键词词典
         /// </summary>
         /// <param name="sFileName">词典名称</param>
         /// <param name="bOverwrite">是否重写</param>
         /// <returns>1</returns>
        public uint KeyExtractImportUserDict(string sFileName, bool bOverwrite = false)
        {
            JudgeKeyExtractInit();
            return KeyExtract_ImportUserDict(sFileName, bOverwrite);
        }
         /// <summary>
         /// 向关键词词典中添加词语
         /// </summary>
         /// <param name="sWord">注意事项参见NLPIR的官方帮助文档，本程序已经应汇总对应的帮助文档</param>
         /// <returns></returns>
        public int KeyExtractAddUserWord(string sWord)
        {
            JudgeKeyExtractInit();
            return KeyExtract_AddUserWord(sWord);
        }
         /// <summary>
         /// 清除用户词典库
         /// </summary>
         /// <returns></returns>
        public int KeyExtractCleanUserWord()
        {
            JudgeKeyExtractInit();
            return KeyExtract_CleanUserWord();
        }
        /// <summary>
        /// 保存用户词典库
        /// </summary>
        /// <returns></returns>
        public int KeyExtractSaveTheUsrDic()
        {
            JudgeKeyExtractInit();
            return KeyExtract_SaveTheUsrDic();
        }
        /// <summary>
        /// 删除用户词
        /// </summary>
        /// <returns></returns>
        public int KeyExtractDelUsrWord(string sWord)
        {
            JudgeKeyExtractInit();
            return KeyExtract_DelUsrWord(sWord);
        }
         /// <summary>
         /// 导入停用词列表
         /// </summary>
         /// <param name="sFilename"></param>
         /// <param name="sPOSBlacklist"></param>
         /// <returns></returns>
        public uint KeyExtractImportKeyBlackList(string sFilename, string sPOSBlacklist)
        {
            JudgeKeyExtractInit();
            return KeyExtract_ImportKeyBlackList(sFilename,sPOSBlacklist);
        }
         //-----------------------------下面为批量处理方法----------
        public int KeyExtractBatch_Start()
        {
            JudgeKeyExtractInit();
            return KeyExtract_Batch_Start();
        }

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
        private static extern IntPtr NLPIR_ParagraphProcessA(string sParagraph, out int nResultCount,bool bUseeDir=true);
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
        [DllImport(NLPIRPath,CharSet=CharSet.Ansi,CallingConvention=CallingConvention.Cdecl,EntryPoint="NLPIR_AddUserWord")]
        private static  extern int NLPIR_AddUserWord(string sWord);

        //保存用户词典
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_SaveTheUsrDic")]
        private static extern int NLPIR_SaveTheUsrDic();

        //删除用户词
        [DllImport(NLPIRPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_DelUsrWord")]
        private static extern int NLPIR_DelUsrWord(string sWord);
        #endregion

        #region KeyExtract
        //初始化关键词提取
        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_Init")]
        private static extern bool KeyExtract_Init(string sDatapath,int encode=(int)NLPIR_CODE.GBK_CODE);
        //退出关键词提取
        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_Exit")]
        private static extern bool KeyExtract_Exit();
        //从文本中提取关键词
        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_GetKeyWords")]
        private static extern IntPtr KeyExtract_GetKeyWords(string sLine, int nMaxKeyLimit = 50, bool bWeightOut = false);

        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_GetFileKeyWords")]
        private static extern IntPtr KeyExtract_GetFileKeyWords(string sFilename, int nMaxKeyLimit = 50, bool bWeightOut = false);
        //导入关键词用户自定义的词典
        [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_ImportUserDict")]
        private static extern uint KeyExtract_ImportUserDict(string sFileName, bool bOverwrite = false);
        //向词典中添加词语
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_AddUserWord")]
        private static extern int KeyExtract_AddUserWord(string sWord);
        //清除关键词库
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_CleanUserWord")]
         private static extern int KeyExtract_CleanUserWord();
        //保存关键词库
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_SaveTheUsrDic")]
         private static extern int KeyExtract_SaveTheUsrDic();
        //删除词语
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_DelUsrWord")]
         private static extern int KeyExtract_DelUsrWord(string sWord);
        //导入停用词
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_DelUsrWord")]
         private static extern uint KeyExtract_ImportKeyBlackList(string sFilename, string sPOSBlacklist);

        ///下面为批量处理方法
        //启动关键词识别
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "eyExtract_Batch_Start")]
         private static extern int KeyExtract_Batch_Start();
        //添加关键词匹配文件
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_Batch_AddFile")]
         private static extern uint KeyExtract_Batch_AddFile(string sFilename);
        //天剑关键词匹配字符串
         [DllImport(KeyExtractPath, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "KeyExtract_Batch_AddMem")]
         private static extern uint KeyExtract_Batch_AddMem(string sText);
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
}