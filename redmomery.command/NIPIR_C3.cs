using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Example
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

    /// <summary>
    /// 分词类。
    /// </summary>
    public class NLPIR
    {
        #region 对变量进行声明
        private static bool _Init = false;
        private static bool _NWIStart = false;
        private static bool _NWIComplete = false;
        private const string rootDir = @".\";
        #endregion

        #region 对函数进行声明和包装
        #region 预判断
        private static void JudgeInit()
        {
            if (!_Init) throw new Exception("未进行初始化！");
        }

        private static void JudgeNWIStart()
        {
            JudgeInit();
            if (!_NWIStart) throw new Exception("未启动新词识别！");
        }

        private static void JudgeNWIComplete()
        {
            JudgeInit();
            if (!_NWIComplete) throw new Exception("未结束新词识别！");
        }
        #endregion

        #region 初始化、退出
        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="sInitDirPath">Data所在目录。</param>
        /// <param name="encoding">编码类型。</param>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Init")]
        private static extern bool NLPIR_Init(string sInitDirPath, int encoding = (int)NLPIR_CODE.GBK_CODE);
        /// <summary>
        /// 初始化，编码类型为GBK_CODE。
        /// </summary>
        /// <param name="sInitDirPath">Data所在目录。</param>
        /// <returns>是否执行成功。</returns>
        public static bool Init(string sInitDirPath)
        {

            _Init = NLPIR_Init(sInitDirPath);
            return _Init;
        }
        /// <summary>
        /// 初始化。
        /// </summary>
        /// <param name="sInitDirPath">Data所在目录。</param>
        /// <param name="encoding">编码类型。</param>
        /// <returns>是否执行成功。</returns>
        public static bool Init(string sInitDirPath, NLPIR_CODE encoding)
        {
            _Init = NLPIR_Init(sInitDirPath, (int)encoding);
            return _Init;
        }

        /// <summary>
        /// 退出并释放资源。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Exit")]
        private static extern bool NLPIR_Exit();
        /// <summary>
        /// 退出并释放资源。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        public static bool Exit()
        {
            _Init = false;
            return NLPIR_Exit();
        }
        #endregion

        #region 分词操作
        /// <summary>
        /// 处理文本内容。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <param name="bPOStagged">是否进行词性标注。</param>
        /// <returns>处理结果。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcess")]
        private static extern IntPtr NLPIR_ParagraphProcess(string sParagraph, int bPOStagged = 1);
        /// <summary>
        /// 处理文本内容。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <param name="bPOStagged">是否进行词性标注。</param>
        /// <returns>处理结果。</returns>
        public static string ParagraphProcess(string sParagraph, bool bPOStagged)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_ParagraphProcess(sParagraph, bPOStagged ? 1 : 0);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// 处理文本文件。
        /// </summary>
        /// <param name="sSrcFilename">源文件。</param>
        /// <param name="sDestFilename">目标文件。</param>
        /// <param name="bPOStagged">是否进行词性标注。</param>
        /// <returns>执行成功返回处理速度；否则返回0。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FileProcess")]
        private static extern double NLPIR_FileProcess(
            string sSrcFilename, string sDestFilename, int bPOStagged = 1);
        /// <summary>
        /// 处理文本文件。
        /// </summary>
        /// <param name="sSrcFilename">源文件。</param>
        /// <param name="sDestFilename">目标文件。</param>
        /// <param name="bPOStagged">是否进行词性标注。</param>
        /// <returns>执行成功返回处理速度；否则返回0。</returns>
        public static double FileProcess(string sSrcFilename, string sDestFilename, bool bPOStagged)
        {
            JudgeInit();
            return NLPIR_FileProcess(sSrcFilename, sDestFilename, bPOStagged ? 1 : 0);
        }

        /// <summary>
        /// 处理文本内容，获取分词数。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <returns>分词数。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetParagraphProcessAWordCount")]
        private static extern int NLPIR_GetParagraphProcessAWordCount(string sParagraph);
        /// <summary>
        /// 处理文本内容，获取分词数。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <returns>分词数。</returns>
        public static int GetParagraphProcessAWordCount(string sParagraph)
        {
            JudgeInit();
            return NLPIR_GetParagraphProcessAWordCount(sParagraph);
        }

        /// <summary>
        /// 处理文本内容。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <param name="nResultCount">分词数。</param>
        /// <returns>分词结果数组。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessA")]
        private static extern IntPtr NLPIR_ParagraphProcessA(string sParagraph, out int nResultCount);
        /// <summary>
        /// 处理文本内容。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <returns>分词结果数组。</returns>
        public static result_t[] ParagraphProcessA(string sParagraph)
        {
            JudgeInit();
            int nCount = 0;
            IntPtr intPtr = NLPIR_ParagraphProcessA(sParagraph, out nCount);
            result_t[] results = new result_t[nCount];
            for (int i = 0; i < nCount; i++, intPtr = new IntPtr(
                intPtr.ToInt32() + Marshal.SizeOf(typeof(result_t))))
            {
                results[i] = (result_t)Marshal.PtrToStructure(intPtr, typeof(result_t));
            }
            return results;
        }

        /// <summary>
        /// 处理文本内容。
        /// </summary>
        /// <param name="nCount">分词数。</param>
        /// <param name="results">分词结果数组。</param>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessAW")]
        private static extern void NLPIR_ParagraphProcessAW(
            int nCount, [Out, MarshalAs(UnmanagedType.LPArray)] result_t[] result);
        /// <summary>
        /// 处理文本内容。
        /// </summary>
        /// <param name="nCount">分词数。</param>
        /// <returns>分词结果数组。</returns>
        public static result_t[] ParagraphProcessAW(int nCount)
        {
            JudgeInit();
            result_t[] results = new result_t[nCount];
            NLPIR_ParagraphProcessAW(nCount, results);
            return results;
        }
        #endregion

        #region 用户自定义词操作
        /// <summary>
        /// 导入用户自定义词典。
        /// 经测试没有写到磁盘，下次启动程序时需重新导入，即使调用NLPIR_SaveTheUsrDic。
        /// </summary>
        /// <param name="sFilename">用户自定义词典文件名（文本文件）。</param>
        /// <returns>用户自定义词数。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ImportUserDict")]
        private static extern int NLPIR_ImportUserDict(string sFilename);
        /// <summary>
        /// 导入用户自定义词典。
        /// 经测试没有写到磁盘，下次启动程序时需重新导入，即使调用NLPIR_SaveTheUsrDic。
        /// </summary>
        /// <param name="sFilename">用户自定义词典文件名（文本文件）。</param>
        /// <returns>用户自定义词数。</returns>
        public static int ImportUserDict(string sFilename)
        {
            JudgeInit();
            return NLPIR_ImportUserDict(sFilename);
        }

        /// <summary>
        /// 添加用户自定义词，格式为词+空格+词性，例“在国内 kkk”，不指定词性，默认为n。
        /// 若要下次启动程序时仍然有效，需执行NLPIR_SaveTheUsrDic。
        /// </summary>
        /// <param name="sWord">用户自定义词。</param>
        /// <returns>执行成功返回1；否则返回0。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_AddUserWord")]
        private static extern int NLPIR_AddUserWord(string sWord);
        /// <summary>
        /// 添加用户自定义词，格式为词+空格+词性，例“在国内 kkk”，不指定词性，默认为n。
        /// 若要下次启动程序时仍然有效，需执行SaveTheUsrDic。
        /// </summary>
        /// <param name="sWord">用户自定义词。</param>
        /// <returns>是否执行成功。</returns>
        public static bool AddUserWord(string sWord)
        {
            JudgeInit();
            return NLPIR_AddUserWord(sWord) == 1;
        }

        /// <summary>
        /// 删除用户自定义词，不能指定词性。
        /// 若要下次启动程序时仍然有效，需执行NLPIR_SaveTheUsrDic。
        /// </summary>
        /// <param name="sWord">用户自定义词。</param>
        /// <returns>执行成功返回用户自定义词句柄；否则返回-1。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_DelUsrWord")]
        private static extern int NLPIR_DelUsrWord(string sWord);
        /// <summary>
        /// 删除用户自定义词，不能指定词性。
        /// 若要下次启动程序时仍然有效，需执行SaveTheUsrDic。
        /// </summary>
        /// <param name="sWord">用户自定义词。</param>
        /// <returns>是否执行成功。</returns>
        public static bool DelUsrWord(string sWord)
        {
            JudgeInit();
            return NLPIR_DelUsrWord(sWord) != -1;
        }

        /// <summary>
        /// 保存用户自定义词到磁盘。
        /// </summary>
        /// <returns>执行成功返回1；否则返回0。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_SaveTheUsrDic")]
        private static extern int NLPIR_SaveTheUsrDic();
        /// <summary>
        /// 保存用户自定义词到磁盘。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        public static bool SaveTheUsrDic()
        {
            JudgeInit();
            return NLPIR_SaveTheUsrDic() == 1;
        }
        #endregion

        #region 新词操作
        /// <summary>
        /// 启动新词识别。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_Start")]
        private static extern bool NLPIR_NWI_Start();
        /// <summary>
        /// 启动新词识别。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        public static bool NWI_Start()
        {
            JudgeInit();
            _NWIStart = NLPIR_NWI_Start();
            // 此处不能用_NWIComplete = ！_NWIStart。
            if (_NWIStart) _NWIComplete = false;
            return _NWIStart;
        }

        /// <summary>
        /// 新词识别添加内容结束，需要在运行NLPIR_NWI_Start()之后才有效。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_Complete")]
        private static extern bool NLPIR_NWI_Complete();
        /// <summary>
        /// 新词识别添加内容结束，需要在运行NWI_Start()之后才有效。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        public static bool NWI_Complete()
        {
            JudgeNWIStart();
            _NWIStart = false;
            _NWIComplete = NLPIR_NWI_Complete();
            return _NWIComplete;
        }

        /// <summary>
        /// 往新词识别系统中添加待识别新词的文本文件，可反复添加，需要在运行NLPIR_NWI_Start()之后才有效。
        /// </summary>
        /// <param name="sFilename">文本文件名。</param>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_AddFile")]
        private static extern bool NLPIR_NWI_AddFile(string sFilename);
        /// <summary>
        /// 往新词识别系统中添加待识别新词的文本文件，可反复添加，需要在运行NWI_Start()之后才有效。
        /// </summary>
        /// <param name="sFilename">文本文件名。</param>
        /// <returns>是否执行成功。</returns>
        public static bool NWI_AddFile(string sFilename)
        {
            JudgeNWIStart();
            return NLPIR_NWI_AddFile(sFilename);
        }

        /// <summary>
        /// 往新词识别系统中添加待识别新词的文本内容，可反复添加，需要在运行NLPIR_NWI_Start()之后才有效。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_AddMem")]
        private static extern bool NLPIR_NWI_AddMem(string sParagraph);
        /// <summary>
        /// 往新词识别系统中添加待识别新词的文本内容，可反复添加，需要在运行NWI_Start()之后才有效。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <returns>是否执行成功。</returns>
        public static bool NWI_AddMem(string sParagraph)
        {
            JudgeNWIStart();
            return NLPIR_NWI_AddMem(sParagraph);
        }

        /// <summary>
        /// 获取新词识别的结果，需要在运行NLPIR_NWI_Complete()之后才有效。
        /// </summary>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_GetResult")]
        private static extern IntPtr NLPIR_NWI_GetResult(bool bWeightOut = false);
        /// <summary>
        /// 获取新词识别的结果，需要在运行NWI_Complete()之后才有效。
        /// </summary>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>是否执行成功。</returns>
        public static string NWI_GetResult(bool bWeightOut)
        {
            JudgeNWIComplete();
            IntPtr intPtr = NLPIR_NWI_GetResult(bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// 将新词识别结果导入到用户词典中，需要在运行NLPIR_NWI_Complete()之后才有效。
        /// 经测试该函数会自动将结果写入磁盘，无需执行SaveTheUsrDic。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_Result2UserDict")]
        private static extern bool NLPIR_NWI_Result2UserDict();
        /// <summary>
        /// 将新词识别结果导入到用户词典中，需要在运行NWI_Complete()之后才有效。
        /// 经测试该函数会自动将结果写入磁盘，无需执行SaveTheUsrDic。
        /// </summary>
        /// <returns>是否执行成功。</returns>
        public static bool NWI_Result2UserDict()
        {
            JudgeNWIComplete();
            return NLPIR_NWI_Result2UserDict();
        }
        #endregion

        #region 直接获取关键词或新词。
        /// <summary>
        /// 获取文本关键字。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <param name="nMaxKeyLimit">关键字最大数，实际输出关键字数为nMaxKeyLimit+1。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>关键字列表。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetKeyWords")]
        private static extern IntPtr NLPIR_GetKeyWords(
            string sParagraph, int nMaxKeyLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// 获取文本关键字。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <param name="nMaxKeyLimit">关键字最大数。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>关键字列表。</returns>
        public static string GetKeyWords(string sParagraph, int nMaxKeyLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetKeyWords(sParagraph, nMaxKeyLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// 获取文本文件关键字。
        /// </summary>
        /// <param name="sFilename">文本文件名。</param>
        /// <param name="nMaxKeyLimit">关键字最大数，实际输出关键字数为nMaxKeyLimit+1。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>关键字列表。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetFileKeyWords")]
        private static extern IntPtr NLPIR_GetFileKeyWords(
            string sFilename, int nMaxKeyLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// 获取文本文件关键字。
        /// </summary>
        /// <param name="sFilename">文本文件名。</param>
        /// <param name="nMaxKeyLimit">关键字最大数。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>关键字列表。</returns>
        public static string GetFileKeyWords(string sFilename, int nMaxKeyLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetFileKeyWords(sFilename, nMaxKeyLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// 获取文本新词。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <param name="nMaxNewLimit">新词最大数，实际输出新词数为nMaxNewLimit+1。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>新词列表。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetNewWords")]
        private static extern IntPtr NLPIR_GetNewWords(
            string sParagraph, int nMaxNewLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// 获取文本新词。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <param name="nMaxNewLimit">新词最大数。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>新词列表。</returns>
        public static string GetNewWords(string sParagraph, int nMaxNewLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetNewWords(sParagraph, nMaxNewLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// 获取文本文件新词。
        /// </summary>
        /// <param name="sFilename">文本文件名。</param>
        /// <param name="nMaxNewLimit">新词最大数，实际输出新词数为nMaxNewLimit+1。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>新词列表。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetFileNewWords")]
        private static extern IntPtr NLPIR_GetFileNewWords(
            string sFilename, int nMaxNewLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// 获取文本文件新词。
        /// </summary>
        /// <param name="sFilename">文本文件名。</param>
        /// <param name="nMaxNewLimit">新词最大数。</param>
        /// <param name="bWeightOut">是否输出权值。</param>
        /// <returns>新词列表。</returns>
        public static string GetFileNewWords(string sFilename, int nMaxNewLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetFileNewWords(sFilename, nMaxNewLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }
        #endregion

        #region 其他
        /// <summary>
        /// 设置标注集。
        /// </summary>
        /// <param name="nPOSmap">标注集。</param>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_SetPOSmap")]
        private static extern void NLPIR_SetPOSmap(int nPOSmap);
        /// <summary>
        /// 设置标注集。
        /// </summary>
        /// <param name="nPOSmap">标注集。</param>
        public static void SetPOSmap(NLPIR_MAP nPOSmap)
        {
            JudgeInit();
            NLPIR_SetPOSmap((int)nPOSmap);
        }

        /// <summary>
        /// 从文本提取指纹信息。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <returns>执行成功返回指纹信息；否则返回0。</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FingerPrint")]
        private static extern ulong NLPIR_FingerPrint(string sParagraph);
        /// <summary>
        /// 从文本提取指纹信息。
        /// </summary>
        /// <param name="sParagraph">文本内容。</param>
        /// <returns>执行成功返回指纹信息；否则返回0。</returns>
        public static ulong FingerPrint(string sParagraph)
        {
            JudgeInit();
            return NLPIR_FingerPrint(sParagraph);
        }
        #endregion
        #endregion

        /// <summary>
        /// 用法举例。
        /// </summary>
        public static void Example()
        {
            #region Init
            string s1 = "ICTCLAS在国内973专家组组织的评测中活动获得了第一名，在第一届国际中文处理研究机构SigHan组织的评测中都获得了多项第一名。";
            string s2 = File.ReadAllText(@"D:\题库系统\redMomery\调试\新建文本文档.txt", Encoding.Default);
          //  string s2 = File.ReadAllText(rootDir + @"test\test.TXT", Encoding.Default);
            bool bSuc = false;
            string ss = "";
            string str = "";
            int nTemp = 0;
            double dTemp = 0;
            int count = 0;
            Init(rootDir);
            Console.WriteLine("Init ICTCLAS success!");
            #endregion

            #region GetParagraphProcessAWordCount,ParagraphProcessAW
            count = GetParagraphProcessAWordCount(s1);//先得到结果的词数
            result_t[] results = ParagraphProcessAW(count);//获取结果存到客户的内存中
            int i = 1;
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
                Console.WriteLine(r.sPos + "," + Encoding.Default.GetString(bytes, r.start, r.length));
                Console.WriteLine("No.{0}:start:{1},length:{2},POS_ID:{3}\n" +
                    "Word_ID:{4},UserDefine:{5},Weight:{6}",
                    i++, r.start, r.length, r.POS_id, r.word_ID, sWhichDic, r.weight);
            }
            #endregion

            #region ParagraphProcessA
            results = ParagraphProcessA(s2);
            #endregion

            #region ParagraphProcess
            str = ParagraphProcess(s2, true);
            Console.WriteLine(str);
            #endregion

            #region FileProcess
            dTemp = FileProcess(rootDir + @"test\test.TXT",
                rootDir + @"test\test-result.TXT", true);
            #endregion

            #region AddUserWord,DelUsrWord,SaveTheUsrDic
            Console.WriteLine("insert user dic:");
            ss = Console.ReadLine();
            while (ss[0] != 'q' && ss[0] != 'Q')
            {
                //用户词典中添加词
                bSuc = AddUserWord(ss);
                str = ParagraphProcess(s1, false);
                Console.WriteLine(str);
                bSuc = SaveTheUsrDic();

                //删除用户词典中的词
                Console.WriteLine("delete usr dic:");
                ss = Console.ReadLine();
                bSuc = DelUsrWord(ss);
                str = ParagraphProcess(s1, true);
                Console.WriteLine(str);
                bSuc = SaveTheUsrDic();

                Console.WriteLine("insert user dic:");
                ss = Console.ReadLine();
            }
            #endregion

            #region ImportUserDict
            nTemp = ImportUserDict(rootDir + @"test\UserWords.txt");
            str = ParagraphProcess(s1, true);
            Console.WriteLine(str);
            #endregion

            #region GetKeyWords
            str = GetKeyWords(s1, 5, true);
            Console.WriteLine(str);
            str = GetKeyWords(s1, 5, false);
            Console.WriteLine(str);
            #endregion

            #region GetFileKeyWords
            str = GetFileKeyWords(rootDir + @"test\test.TXT", 6, true);
            Console.WriteLine(str);
            #endregion

            #region GetNewWords
            str = GetNewWords(s2, 5, true);
            Console.WriteLine(str);
            str = GetNewWords(s2, 5, false);
            Console.WriteLine(str);
            #endregion

            #region GetFileNewWords
            str = GetFileNewWords(rootDir + @"test\test.TXT", 3, true);
            Console.WriteLine(str);
            #endregion

            #region NWI_Start,NWI_AddFile,NWI_AddMem,NWI_Complete,NWI_GetResult,NWI_Result2UserDict
            bSuc = NWI_Start();
            bSuc = NWI_AddFile(rootDir + @"test\test.TXT");
            bSuc = NWI_AddMem(s1);
            bSuc = NWI_Complete();
            str = NWI_GetResult(false);
            Console.WriteLine("新词识别结果:");
            Console.WriteLine(str);
            str = NWI_GetResult(true);
            Console.WriteLine("新词识别结果:");
            Console.WriteLine(str);
            bSuc = NWI_Result2UserDict();
            #endregion

            #region FingerPrint
            ulong ulTemp = FingerPrint(s1);
            #endregion

            #region SetPOSmap
            SetPOSmap(NLPIR_MAP.ICT_POS_MAP_FIRST);
            SetPOSmap(NLPIR_MAP.ICT_POS_MAP_SECOND);
            SetPOSmap(NLPIR_MAP.PKU_POS_MAP_FIRST);
            SetPOSmap(NLPIR_MAP.PKU_POS_MAP_SECOND);
            #endregion

            #region Exit
            DelUsrWord("屌丝");
            DelUsrWord("网民");
            DelUsrWord("解构");
            DelUsrWord("屌丝文化");
            DelUsrWord("阿Q");
            DelUsrWord("网络亚文化");
            DelUsrWord("群体自嘲");
            DelUsrWord("身份卑微");
            DelUsrWord("爆红网络");
            DelUsrWord("贴吧");
            DelUsrWord("富帅");
            bSuc = SaveTheUsrDic();
            str = ParagraphProcess(s2, true);
            bSuc = Exit();
            #endregion
        }
    }

    class MainClass
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            NLPIR.Example();
        }
    }
}
