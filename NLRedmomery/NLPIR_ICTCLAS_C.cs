using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Configuration;

namespace NLRedmomery
{
   
}
namespace NLRedmomery
{
    public partial class NLPIR_ICTCLAS_C
    {


        #region NLPIR_ICTCLA
        #region 对函数库进行声明和包装
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

        #region 初始化、退出
        //初始化
        [DllImport((libpath + "NLPIR.dll"), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Init")]
        private static extern bool NLPIR_Init(string sInitDirPath, int encoding = (int)NLPIR_CODE.GBK_CODE);
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
        [DllImport((libpath + "NLPIR.dll"), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Exit")]
        private static extern bool NLPIR_Exit();
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
        #region 分词操作
        [DllImport((libpath + "NLPIR.dll"), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcess")]
        private static extern IntPtr NLPIR_ParagraphProcess(string sParagraph, int bPOStagged = 1);
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

        [DllImport((libpath + "NLPIR.dll"), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FileProcess")]
        private static extern double NLPIR_FileProcess(string sSrcFilename, string sDestFilename, int bPOStagged = 1);
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

        [DllImport((libpath + "NLPIR.dll"), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetParagraphProcessAWordCount")]
        private static extern int NLPIR_GetParagraphProcessAWordCount(string sParagraph);
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

        [DllImport((libpath + "NLPIR.dll"), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessA")]
        private static extern IntPtr NLPIR_ParagraphProcessA(string sParagraph, out int nResultCount);
        /// <summary>
        /// 处理文本内容 并获取结果词组
        /// </summary>
        /// <param name="sParagraph">待处理的文本内容</param>
        /// <returns>分词结果数组</returns>
        public result_t[] ParagraphProcessA(string sParagraph)
        {
            JudgeInit();
            int nCount = 0;
            IntPtr intpre = NLPIR_ParagraphProcessA(sParagraph, out nCount);
            result_t[] result = new result_t[nCount];
            for (int i = 0; i < nCount; i++, intpre = new IntPtr(intpre.ToInt32() + Marshal.SizeOf(typeof(result_t))))
            {
                result[i] = (result_t)Marshal.PtrToStructure(intpre, typeof(result_t));
            }
            return result;
        }

        [DllImport((libpath + "NLPIR.dll"), CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessAW")]
        private static extern void NLPIR_ParagraphProcessAW(int nCount, [Out, MarshalAs(UnmanagedType.LPArray)] result_t[] result);

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
        #endregion

        #region 用户自定义词操作

        #endregion
        #endregion
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