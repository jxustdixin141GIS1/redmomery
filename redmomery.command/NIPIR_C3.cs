using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Example
{
    /// <summary>
    /// ��ע�����͡�
    /// </summary>
    public enum NLPIR_MAP
    {
        /// <summary>
        /// ������һ����ע����
        /// </summary>
        ICT_POS_MAP_FIRST = 1,

        /// <summary>
        /// ������������ע����
        /// </summary>
        ICT_POS_MAP_SECOND = 0,

        /// <summary>
        /// ����һ����ע����
        /// </summary>
        PKU_POS_MAP_FIRST = 3,

        /// <summary>
        /// ���������ע����
        /// </summary>
        PKU_POS_MAP_SECOND = 2
    }

    /// <summary>
    /// �������͡�
    /// </summary>
    public enum NLPIR_CODE
    {
        /// <summary>
        /// GBK���롣
        /// </summary>
        GBK_CODE = 0,

        /// <summary>
        /// UTF8���롣
        /// </summary>
        UTF8_CODE = 1,

        /// <summary>
        /// BIG5���롣
        /// </summary>
        BIG5_CODE = 2,

        /// <summary>
        /// GBK���룬������������֡�
        /// </summary>
        GBK_FANTI_CODE = 3
    }

    /// <summary>
    /// �ִʽ���ṹ�塣
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct result_t
    {
        /// <summary>
        /// ��������������еĿ�ʼλ�á�
        /// </summary>
        public int start;

        /// <summary>
        /// ����ĳ��ȡ�
        /// </summary>
        public int length;

        /// <summary>
        /// ����IDֵ�����Կ��ٵĻ�ȡ���Ա�
        /// </summary>
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string sPos;

        /// <summary>
        /// ���Ա�ע�ı�š�
        /// </summary>
        public int POS_id;

        /// <summary>
        /// �ôʵ��ڲ�ID�ţ������δ��¼�ʣ����0����-1��
        /// </summary>
        public int word_ID;

        /// <summary>
        /// �����û��ʵ䣬1���û��ʵ��еĴʣ�0���û��ʵ��еĴʡ�
        /// </summary>
        public int word_type;

        /// <summary>
        /// Ȩֵ��
        /// </summary>
        public int weight;
    }

    /// <summary>
    /// �ִ��ࡣ
    /// </summary>
    public class NLPIR
    {
        #region �Ա�����������
        private static bool _Init = false;
        private static bool _NWIStart = false;
        private static bool _NWIComplete = false;
        private const string rootDir = @".\";
        #endregion

        #region �Ժ������������Ͱ�װ
        #region Ԥ�ж�
        private static void JudgeInit()
        {
            if (!_Init) throw new Exception("δ���г�ʼ����");
        }

        private static void JudgeNWIStart()
        {
            JudgeInit();
            if (!_NWIStart) throw new Exception("δ�����´�ʶ��");
        }

        private static void JudgeNWIComplete()
        {
            JudgeInit();
            if (!_NWIComplete) throw new Exception("δ�����´�ʶ��");
        }
        #endregion

        #region ��ʼ�����˳�
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="sInitDirPath">Data����Ŀ¼��</param>
        /// <param name="encoding">�������͡�</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Init")]
        private static extern bool NLPIR_Init(string sInitDirPath, int encoding = (int)NLPIR_CODE.GBK_CODE);
        /// <summary>
        /// ��ʼ������������ΪGBK_CODE��
        /// </summary>
        /// <param name="sInitDirPath">Data����Ŀ¼��</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool Init(string sInitDirPath)
        {

            _Init = NLPIR_Init(sInitDirPath);
            return _Init;
        }
        /// <summary>
        /// ��ʼ����
        /// </summary>
        /// <param name="sInitDirPath">Data����Ŀ¼��</param>
        /// <param name="encoding">�������͡�</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool Init(string sInitDirPath, NLPIR_CODE encoding)
        {
            _Init = NLPIR_Init(sInitDirPath, (int)encoding);
            return _Init;
        }

        /// <summary>
        /// �˳����ͷ���Դ��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_Exit")]
        private static extern bool NLPIR_Exit();
        /// <summary>
        /// �˳����ͷ���Դ��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool Exit()
        {
            _Init = false;
            return NLPIR_Exit();
        }
        #endregion

        #region �ִʲ���
        /// <summary>
        /// �����ı����ݡ�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <param name="bPOStagged">�Ƿ���д��Ա�ע��</param>
        /// <returns>��������</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcess")]
        private static extern IntPtr NLPIR_ParagraphProcess(string sParagraph, int bPOStagged = 1);
        /// <summary>
        /// �����ı����ݡ�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <param name="bPOStagged">�Ƿ���д��Ա�ע��</param>
        /// <returns>��������</returns>
        public static string ParagraphProcess(string sParagraph, bool bPOStagged)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_ParagraphProcess(sParagraph, bPOStagged ? 1 : 0);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// �����ı��ļ���
        /// </summary>
        /// <param name="sSrcFilename">Դ�ļ���</param>
        /// <param name="sDestFilename">Ŀ���ļ���</param>
        /// <param name="bPOStagged">�Ƿ���д��Ա�ע��</param>
        /// <returns>ִ�гɹ����ش����ٶȣ����򷵻�0��</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FileProcess")]
        private static extern double NLPIR_FileProcess(
            string sSrcFilename, string sDestFilename, int bPOStagged = 1);
        /// <summary>
        /// �����ı��ļ���
        /// </summary>
        /// <param name="sSrcFilename">Դ�ļ���</param>
        /// <param name="sDestFilename">Ŀ���ļ���</param>
        /// <param name="bPOStagged">�Ƿ���д��Ա�ע��</param>
        /// <returns>ִ�гɹ����ش����ٶȣ����򷵻�0��</returns>
        public static double FileProcess(string sSrcFilename, string sDestFilename, bool bPOStagged)
        {
            JudgeInit();
            return NLPIR_FileProcess(sSrcFilename, sDestFilename, bPOStagged ? 1 : 0);
        }

        /// <summary>
        /// �����ı����ݣ���ȡ�ִ�����
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <returns>�ִ�����</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetParagraphProcessAWordCount")]
        private static extern int NLPIR_GetParagraphProcessAWordCount(string sParagraph);
        /// <summary>
        /// �����ı����ݣ���ȡ�ִ�����
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <returns>�ִ�����</returns>
        public static int GetParagraphProcessAWordCount(string sParagraph)
        {
            JudgeInit();
            return NLPIR_GetParagraphProcessAWordCount(sParagraph);
        }

        /// <summary>
        /// �����ı����ݡ�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <param name="nResultCount">�ִ�����</param>
        /// <returns>�ִʽ�����顣</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessA")]
        private static extern IntPtr NLPIR_ParagraphProcessA(string sParagraph, out int nResultCount);
        /// <summary>
        /// �����ı����ݡ�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <returns>�ִʽ�����顣</returns>
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
        /// �����ı����ݡ�
        /// </summary>
        /// <param name="nCount">�ִ�����</param>
        /// <param name="results">�ִʽ�����顣</param>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ParagraphProcessAW")]
        private static extern void NLPIR_ParagraphProcessAW(
            int nCount, [Out, MarshalAs(UnmanagedType.LPArray)] result_t[] result);
        /// <summary>
        /// �����ı����ݡ�
        /// </summary>
        /// <param name="nCount">�ִ�����</param>
        /// <returns>�ִʽ�����顣</returns>
        public static result_t[] ParagraphProcessAW(int nCount)
        {
            JudgeInit();
            result_t[] results = new result_t[nCount];
            NLPIR_ParagraphProcessAW(nCount, results);
            return results;
        }
        #endregion

        #region �û��Զ���ʲ���
        /// <summary>
        /// �����û��Զ���ʵ䡣
        /// ������û��д�����̣��´���������ʱ�����µ��룬��ʹ����NLPIR_SaveTheUsrDic��
        /// </summary>
        /// <param name="sFilename">�û��Զ���ʵ��ļ������ı��ļ�����</param>
        /// <returns>�û��Զ��������</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_ImportUserDict")]
        private static extern int NLPIR_ImportUserDict(string sFilename);
        /// <summary>
        /// �����û��Զ���ʵ䡣
        /// ������û��д�����̣��´���������ʱ�����µ��룬��ʹ����NLPIR_SaveTheUsrDic��
        /// </summary>
        /// <param name="sFilename">�û��Զ���ʵ��ļ������ı��ļ�����</param>
        /// <returns>�û��Զ��������</returns>
        public static int ImportUserDict(string sFilename)
        {
            JudgeInit();
            return NLPIR_ImportUserDict(sFilename);
        }

        /// <summary>
        /// ����û��Զ���ʣ���ʽΪ��+�ո�+���ԣ������ڹ��� kkk������ָ�����ԣ�Ĭ��Ϊn��
        /// ��Ҫ�´���������ʱ��Ȼ��Ч����ִ��NLPIR_SaveTheUsrDic��
        /// </summary>
        /// <param name="sWord">�û��Զ���ʡ�</param>
        /// <returns>ִ�гɹ�����1�����򷵻�0��</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_AddUserWord")]
        private static extern int NLPIR_AddUserWord(string sWord);
        /// <summary>
        /// ����û��Զ���ʣ���ʽΪ��+�ո�+���ԣ������ڹ��� kkk������ָ�����ԣ�Ĭ��Ϊn��
        /// ��Ҫ�´���������ʱ��Ȼ��Ч����ִ��SaveTheUsrDic��
        /// </summary>
        /// <param name="sWord">�û��Զ���ʡ�</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool AddUserWord(string sWord)
        {
            JudgeInit();
            return NLPIR_AddUserWord(sWord) == 1;
        }

        /// <summary>
        /// ɾ���û��Զ���ʣ�����ָ�����ԡ�
        /// ��Ҫ�´���������ʱ��Ȼ��Ч����ִ��NLPIR_SaveTheUsrDic��
        /// </summary>
        /// <param name="sWord">�û��Զ���ʡ�</param>
        /// <returns>ִ�гɹ������û��Զ���ʾ�������򷵻�-1��</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_DelUsrWord")]
        private static extern int NLPIR_DelUsrWord(string sWord);
        /// <summary>
        /// ɾ���û��Զ���ʣ�����ָ�����ԡ�
        /// ��Ҫ�´���������ʱ��Ȼ��Ч����ִ��SaveTheUsrDic��
        /// </summary>
        /// <param name="sWord">�û��Զ���ʡ�</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool DelUsrWord(string sWord)
        {
            JudgeInit();
            return NLPIR_DelUsrWord(sWord) != -1;
        }

        /// <summary>
        /// �����û��Զ���ʵ����̡�
        /// </summary>
        /// <returns>ִ�гɹ�����1�����򷵻�0��</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_SaveTheUsrDic")]
        private static extern int NLPIR_SaveTheUsrDic();
        /// <summary>
        /// �����û��Զ���ʵ����̡�
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool SaveTheUsrDic()
        {
            JudgeInit();
            return NLPIR_SaveTheUsrDic() == 1;
        }
        #endregion

        #region �´ʲ���
        /// <summary>
        /// �����´�ʶ��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_Start")]
        private static extern bool NLPIR_NWI_Start();
        /// <summary>
        /// �����´�ʶ��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool NWI_Start()
        {
            JudgeInit();
            _NWIStart = NLPIR_NWI_Start();
            // �˴�������_NWIComplete = ��_NWIStart��
            if (_NWIStart) _NWIComplete = false;
            return _NWIStart;
        }

        /// <summary>
        /// �´�ʶ��������ݽ�������Ҫ������NLPIR_NWI_Start()֮�����Ч��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_Complete")]
        private static extern bool NLPIR_NWI_Complete();
        /// <summary>
        /// �´�ʶ��������ݽ�������Ҫ������NWI_Start()֮�����Ч��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool NWI_Complete()
        {
            JudgeNWIStart();
            _NWIStart = false;
            _NWIComplete = NLPIR_NWI_Complete();
            return _NWIComplete;
        }

        /// <summary>
        /// ���´�ʶ��ϵͳ����Ӵ�ʶ���´ʵ��ı��ļ����ɷ�����ӣ���Ҫ������NLPIR_NWI_Start()֮�����Ч��
        /// </summary>
        /// <param name="sFilename">�ı��ļ�����</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_AddFile")]
        private static extern bool NLPIR_NWI_AddFile(string sFilename);
        /// <summary>
        /// ���´�ʶ��ϵͳ����Ӵ�ʶ���´ʵ��ı��ļ����ɷ�����ӣ���Ҫ������NWI_Start()֮�����Ч��
        /// </summary>
        /// <param name="sFilename">�ı��ļ�����</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool NWI_AddFile(string sFilename)
        {
            JudgeNWIStart();
            return NLPIR_NWI_AddFile(sFilename);
        }

        /// <summary>
        /// ���´�ʶ��ϵͳ����Ӵ�ʶ���´ʵ��ı����ݣ��ɷ�����ӣ���Ҫ������NLPIR_NWI_Start()֮�����Ч��
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_AddMem")]
        private static extern bool NLPIR_NWI_AddMem(string sParagraph);
        /// <summary>
        /// ���´�ʶ��ϵͳ����Ӵ�ʶ���´ʵ��ı����ݣ��ɷ�����ӣ���Ҫ������NWI_Start()֮�����Ч��
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool NWI_AddMem(string sParagraph)
        {
            JudgeNWIStart();
            return NLPIR_NWI_AddMem(sParagraph);
        }

        /// <summary>
        /// ��ȡ�´�ʶ��Ľ������Ҫ������NLPIR_NWI_Complete()֮�����Ч��
        /// </summary>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_GetResult")]
        private static extern IntPtr NLPIR_NWI_GetResult(bool bWeightOut = false);
        /// <summary>
        /// ��ȡ�´�ʶ��Ľ������Ҫ������NWI_Complete()֮�����Ч��
        /// </summary>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static string NWI_GetResult(bool bWeightOut)
        {
            JudgeNWIComplete();
            IntPtr intPtr = NLPIR_NWI_GetResult(bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// ���´�ʶ�������뵽�û��ʵ��У���Ҫ������NLPIR_NWI_Complete()֮�����Ч��
        /// �����Ըú������Զ������д����̣�����ִ��SaveTheUsrDic��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_NWI_Result2UserDict")]
        private static extern bool NLPIR_NWI_Result2UserDict();
        /// <summary>
        /// ���´�ʶ�������뵽�û��ʵ��У���Ҫ������NWI_Complete()֮�����Ч��
        /// �����Ըú������Զ������д����̣�����ִ��SaveTheUsrDic��
        /// </summary>
        /// <returns>�Ƿ�ִ�гɹ���</returns>
        public static bool NWI_Result2UserDict()
        {
            JudgeNWIComplete();
            return NLPIR_NWI_Result2UserDict();
        }
        #endregion

        #region ֱ�ӻ�ȡ�ؼ��ʻ��´ʡ�
        /// <summary>
        /// ��ȡ�ı��ؼ��֡�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <param name="nMaxKeyLimit">�ؼ����������ʵ������ؼ�����ΪnMaxKeyLimit+1��</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�ؼ����б�</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetKeyWords")]
        private static extern IntPtr NLPIR_GetKeyWords(
            string sParagraph, int nMaxKeyLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// ��ȡ�ı��ؼ��֡�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <param name="nMaxKeyLimit">�ؼ����������</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�ؼ����б�</returns>
        public static string GetKeyWords(string sParagraph, int nMaxKeyLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetKeyWords(sParagraph, nMaxKeyLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// ��ȡ�ı��ļ��ؼ��֡�
        /// </summary>
        /// <param name="sFilename">�ı��ļ�����</param>
        /// <param name="nMaxKeyLimit">�ؼ����������ʵ������ؼ�����ΪnMaxKeyLimit+1��</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�ؼ����б�</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetFileKeyWords")]
        private static extern IntPtr NLPIR_GetFileKeyWords(
            string sFilename, int nMaxKeyLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// ��ȡ�ı��ļ��ؼ��֡�
        /// </summary>
        /// <param name="sFilename">�ı��ļ�����</param>
        /// <param name="nMaxKeyLimit">�ؼ����������</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�ؼ����б�</returns>
        public static string GetFileKeyWords(string sFilename, int nMaxKeyLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetFileKeyWords(sFilename, nMaxKeyLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// ��ȡ�ı��´ʡ�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <param name="nMaxNewLimit">�´��������ʵ������´���ΪnMaxNewLimit+1��</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�´��б�</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetNewWords")]
        private static extern IntPtr NLPIR_GetNewWords(
            string sParagraph, int nMaxNewLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// ��ȡ�ı��´ʡ�
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <param name="nMaxNewLimit">�´��������</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�´��б�</returns>
        public static string GetNewWords(string sParagraph, int nMaxNewLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetNewWords(sParagraph, nMaxNewLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }

        /// <summary>
        /// ��ȡ�ı��ļ��´ʡ�
        /// </summary>
        /// <param name="sFilename">�ı��ļ�����</param>
        /// <param name="nMaxNewLimit">�´��������ʵ������´���ΪnMaxNewLimit+1��</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�´��б�</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_GetFileNewWords")]
        private static extern IntPtr NLPIR_GetFileNewWords(
            string sFilename, int nMaxNewLimit = 50, bool bWeightOut = false);
        /// <summary>
        /// ��ȡ�ı��ļ��´ʡ�
        /// </summary>
        /// <param name="sFilename">�ı��ļ�����</param>
        /// <param name="nMaxNewLimit">�´��������</param>
        /// <param name="bWeightOut">�Ƿ����Ȩֵ��</param>
        /// <returns>�´��б�</returns>
        public static string GetFileNewWords(string sFilename, int nMaxNewLimit, bool bWeightOut)
        {
            JudgeInit();
            IntPtr intPtr = NLPIR_GetFileNewWords(sFilename, nMaxNewLimit - 1, bWeightOut);
            return Marshal.PtrToStringAnsi(intPtr);
        }
        #endregion

        #region ����
        /// <summary>
        /// ���ñ�ע����
        /// </summary>
        /// <param name="nPOSmap">��ע����</param>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_SetPOSmap")]
        private static extern void NLPIR_SetPOSmap(int nPOSmap);
        /// <summary>
        /// ���ñ�ע����
        /// </summary>
        /// <param name="nPOSmap">��ע����</param>
        public static void SetPOSmap(NLPIR_MAP nPOSmap)
        {
            JudgeInit();
            NLPIR_SetPOSmap((int)nPOSmap);
        }

        /// <summary>
        /// ���ı���ȡָ����Ϣ��
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <returns>ִ�гɹ�����ָ����Ϣ�����򷵻�0��</returns>
        [DllImport("NLPIR.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl, EntryPoint = "NLPIR_FingerPrint")]
        private static extern ulong NLPIR_FingerPrint(string sParagraph);
        /// <summary>
        /// ���ı���ȡָ����Ϣ��
        /// </summary>
        /// <param name="sParagraph">�ı����ݡ�</param>
        /// <returns>ִ�гɹ�����ָ����Ϣ�����򷵻�0��</returns>
        public static ulong FingerPrint(string sParagraph)
        {
            JudgeInit();
            return NLPIR_FingerPrint(sParagraph);
        }
        #endregion
        #endregion

        /// <summary>
        /// �÷�������
        /// </summary>
        public static void Example()
        {
            #region Init
            string s1 = "ICTCLAS�ڹ���973ר������֯�������л����˵�һ�����ڵ�һ��������Ĵ����о�����SigHan��֯�������ж�����˶����һ����";
            string s2 = File.ReadAllText(@"D:\���ϵͳ\redMomery\����\�½��ı��ĵ�.txt", Encoding.Default);
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
            count = GetParagraphProcessAWordCount(s1);//�ȵõ�����Ĵ���
            result_t[] results = ParagraphProcessAW(count);//��ȡ����浽�ͻ����ڴ���
            int i = 1;
            byte[] bytes = Encoding.Default.GetBytes(s1);
            foreach (result_t r in results)
            {
                string sWhichDic = "";
                switch (r.word_type)
                {
                    case 0:
                        sWhichDic = "���Ĵʵ�";
                        break;
                    case 1:
                        sWhichDic = "�û��ʵ�";
                        break;
                    case 2:
                        sWhichDic = "רҵ�ʵ�";
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
                //�û��ʵ�����Ӵ�
                bSuc = AddUserWord(ss);
                str = ParagraphProcess(s1, false);
                Console.WriteLine(str);
                bSuc = SaveTheUsrDic();

                //ɾ���û��ʵ��еĴ�
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
            Console.WriteLine("�´�ʶ����:");
            Console.WriteLine(str);
            str = NWI_GetResult(true);
            Console.WriteLine("�´�ʶ����:");
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
            DelUsrWord("��˿");
            DelUsrWord("����");
            DelUsrWord("�⹹");
            DelUsrWord("��˿�Ļ�");
            DelUsrWord("��Q");
            DelUsrWord("�������Ļ�");
            DelUsrWord("Ⱥ���Գ�");
            DelUsrWord("��ݱ�΢");
            DelUsrWord("��������");
            DelUsrWord("����");
            DelUsrWord("��˧");
            bSuc = SaveTheUsrDic();
            str = ParagraphProcess(s2, true);
            bSuc = Exit();
            #endregion
        }
    }

    class MainClass
    {
        /// <summary>
        /// Ӧ�ó��������ڵ㡣
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            NLPIR.Example();
        }
    }
}
