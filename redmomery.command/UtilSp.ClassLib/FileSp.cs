//////////////////////////////////////////////////////////////////////////
//Version 1.1.2
//////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Windows.Forms;

namespace UtilSp.ClassLib
{
    //public class FileSp
    //{
    //    #region exception static Property
    //    private static Exception exception_ = null;
    //    public static Exception exception_pro
    //    {
    //        get
    //        {
    //            return exception_;
    //        }
    //        set
    //        {
    //            exception_ = value;
    //        }
    //    }
    //    #endregion

    //    #region Get path or name
    //    public static string getOpenDialogFileName(string title = "",
    //                                            string filter = "All Files|*.*",
    //                                            int filterIndex = 1,
    //                                            string iniDirectory = "")
    //    {
    //        string fileName = "";
    //        OpenFileDialog ofd = new OpenFileDialog();
    //        ofd.Title = title; ;
    //        ofd.Filter = filter;
    //        ofd.FilterIndex = filterIndex;
    //        ofd.InitialDirectory = iniDirectory;
    //        if (DialogResult.OK == ofd.ShowDialog())
    //        {
    //            fileName = ofd.FileName;
    //        }

    //        return fileName;
    //    }

    //    public static string getSaveDialogFileName(string title = "",
    //                                                string filter = "All Files|*.*",
    //                                                int filterIndex = 1,
    //                                                string iniDirectory = "")
    //    {
    //        string fileName = "";
    //        SaveFileDialog sfd = new SaveFileDialog();
    //        sfd.Title = title;
    //        sfd.Filter = filter;
    //        sfd.FilterIndex = filterIndex;
    //        sfd.InitialDirectory = iniDirectory;
    //        if (DialogResult.OK == sfd.ShowDialog())
    //        {
    //            fileName = sfd.FileName;
    //        }
    //        return fileName;
    //    }

    //    /// <summary>
    //    /// Get folder filePath.
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string getFolderPath()
    //    {
    //        string folderPath = "";
    //        FolderBrowserDialog fbd = new FolderBrowserDialog();
    //        DialogResult DialogRlt = fbd.ShowDialog();
    //        if (DialogRlt == DialogResult.OK)
    //        {
    //            folderPath = fbd.SelectedPath;
    //        }
    //        return folderPath;
    //    }
    //    #endregion

    //    #region Operate file data

    //    #region calculateFileLineCount Function
    //    public static int calculateFileLineCount(string filePath)
    //    {
    //        try
    //        {
    //            exception_pro = null;
    //            FileStream fsSrc = File.OpenRead(filePath);
    //            StreamReader srSrc = new StreamReader(fsSrc, Encoding.Default);
    //            int lineCount = 0;
    //            while (srSrc.ReadLine() != null)
    //            {
    //                lineCount++;
    //            }
    //            fsSrc.Close();
    //            srSrc.Close();
    //            return lineCount;
    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return 0;
    //        }
    //    }
    //    #endregion

    //    public static MemoryStream fileStreamToMemoryStream(string filePath)
    //    {
    //        FileStream fsSrc = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    //        MemoryStream msDest = new MemoryStream();
    //        msDest.SetLength(fsSrc.Length - fsSrc.Position);
    //        fsSrc.Read(msDest.GetBuffer(), 0, (int)(fsSrc.Length - fsSrc.Position));
    //        fsSrc.Close();
    //        return msDest;
    //    }

    //    /// <summary>
    //    /// Read all lines from file.
    //    /// </summary>
    //    /// <param name="fileName"></param>
    //    /// <param name="isReturnList">true:return line list.false:return lines string.</param>
    //    /// <returns></returns>
    //    public static object readAllLineStrFromFile(string fileName, bool isReturnList = false)
    //    {
    //        if (!File.Exists(fileName))
    //        {
    //            return null;
    //        }
    //        string allLineStr = "";
    //        List<string> lineList = new List<string>();
    //        FileStream fsSrc = File.OpenRead(fileName);
    //        StreamReader srSrc = new StreamReader(fsSrc, Encoding.Default);
    //        while (!srSrc.EndOfStream)
    //        {
    //            string lineStrTemp = srSrc.ReadLine();
    //            if (isReturnList)
    //            {
    //                lineList.Add(lineStrTemp);
    //            }
    //            else
    //            {
    //                allLineStr += lineStrTemp + "\n";
    //            }
    //        }
    //        fsSrc.Close();
    //        srSrc.Close();
    //        if (isReturnList)
    //        {
    //            return lineList;
    //        }
    //        else
    //        {
    //            return allLineStr;
    //        }
    //    }

    //    #region readOneLineStrFromFile
    //    /// <summary>
    //    /// Read one line string from file
    //    /// </summary>
    //    /// <param name="filePath"></param>
    //    /// <param name="lineNo">begin 0</param>
    //    /// <returns></returns>
    //    public static string readOneLineStrFromFile(string filePath, int lineNo)
    //    {
    //        try
    //        {

    //            string lineStr = "";
    //            FileStream fsSrc = File.OpenRead(filePath);
    //            StreamReader srSrc = new StreamReader(fsSrc, Encoding.Default);
    //            string lineStrTemp = srSrc.ReadLine();
    //            int iLineNoTmp = 0;
    //            while (lineStrTemp != null)
    //            {
    //                if (iLineNoTmp == lineNo)
    //                {
    //                    break;
    //                }
    //                lineStrTemp = srSrc.ReadLine();
    //                iLineNoTmp++;
    //            }
    //            lineStr = lineStrTemp;
    //            fsSrc.Close();
    //            srSrc.Close();
    //            return lineStr;
    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return null;
    //        }
    //    }
    //    #endregion



    //    #region readFileBytes Function
    //    public static byte[] readFileBytes(string filePath)
    //    {
    //        exception_pro = null;
    //        try
    //        {
    //            if (File.Exists(filePath))
    //            {
    //                FileInfo fi = new FileInfo(filePath);
    //                long len = fi.Length;
    //                FileStream fs = new FileStream(filePath, FileMode.Open);
    //                byte[] buffer = new byte[len];
    //                fs.Read(buffer, 0, (int)len);
    //                fs.Close();
    //                return buffer;
    //            }
    //            else
    //            {
    //                return null;
    //            }
    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return null;
    //        }
    //    }

    //    #region readSpecifyPositionFileData Function
    //    /// <summary>
    //    /// Read specify position data from file.
    //    /// </summary>
    //    /// <param name="filePath"></param>
    //    /// <param name="position"></param>
    //    /// <param name="length"></param>
    //    /// <param name="isReturnString">True:return string.False:return bytes.</param>
    //    /// <param name="encoding">null:use default encoding.notNull:use encoding</param>
    //    /// <returns></returns>
    //    public static object readSpecifyPositionFileData(string filePath,
    //                                                     int position,
    //                                                     int length,
    //                                                     bool isReturnString = true,
    //                                                      Encoding encoding = null)
    //    {
    //        try
    //        {
    //            exception_pro = null;
    //            FileStream reader = new FileStream(filePath, FileMode.Open, FileAccess.Read);
    //            reader.Position = position;
    //            byte[] data = new byte[length];
    //            reader.Read(data, 0, length);
    //            reader.Close();
    //            if (isReturnString)
    //            {
    //                encoding = (encoding == null) ? Encoding.Default : encoding;
    //                string destData = encoding.GetString(data);
    //                return destData;
    //            }
    //            else
    //            {
    //                return data;
    //            }

    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return null;
    //        }
    //    }
    //    #endregion

    //    #endregion

    //    #region WriteStringToFile Function
    //    public static bool writeStringToFile(string filePath, string strSource, bool isNew = false)
    //    {
    //        try
    //        {
    //            FileStream fs = new FileStream(filePath, isNew ? FileMode.Create : FileMode.OpenOrCreate, FileAccess.Write);
    //            StreamWriter m_streamWriter = new StreamWriter(fs);
    //            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
    //            m_streamWriter.WriteLine(strSource);
    //            m_streamWriter.Flush();
    //            m_streamWriter.Close();
    //            fs.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return false;
    //        }
    //    }
    //    #endregion

    //    #region writeBytesToFile Function
    //    public static bool writeBytesToFile(string filePath, byte[] bytes, bool isNew = false)
    //    {
    //        try
    //        {
    //            exception_pro = null;
    //            FileStream fs = new FileStream(filePath, isNew ? FileMode.Create : FileMode.OpenOrCreate, FileAccess.Write);
    //            fs.Write(bytes, 0, bytes.Length);
    //            fs.Close();
    //            return true;
    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return false;
    //        }
    //    }
    //    #endregion

    //    #endregion

    //    #region Operate folder or file



    //    public static bool createDirectory(string strDir)
    //    {
    //        try
    //        {
    //            if (!Directory.Exists(strDir))
    //            {
    //                Directory.CreateDirectory(strDir);
    //            }
    //            return true;
    //        }
    //        catch (Exception ex)
    //        {
    //            exception_pro = ex;
    //            return false;
    //        }

    //    }

    //    #region CopyFolder or CopyDirectory
    //    /// <summary>
    //    /// When sourceFolder is folder filePath,copy folder.When sourceFolder is directory,copy directory.
    //    /// </summary>
    //    /// <param name="sourceFolder"></param>
    //    /// <param name="destFolder"></param>
    //    public static bool copyFolderOrDirectory(string sourceFolder, string destFolder)
    //    {
    //        try
    //        {
    //            string folderName = sourceFolder.Substring(sourceFolder.LastIndexOf("\\") + 1);
    //            string desfolderdir = destFolder + "\\" + folderName;
    //            if (destFolder.LastIndexOf("\\") == (destFolder.Length - 1))
    //            {
    //                desfolderdir = destFolder + folderName;
    //            }
    //            string[] filenames = Directory.GetFileSystemEntries(sourceFolder);

    //            foreach (string file in filenames)// 遍历所有的文件和目录
    //            {
    //                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
    //                {

    //                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
    //                    if (!Directory.Exists(currentdir))
    //                    {
    //                        Directory.CreateDirectory(currentdir);
    //                    }

    //                    copyFolderOrDirectory(file, desfolderdir);
    //                }

    //                else // 否则直接copy文件
    //                {
    //                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

    //                    srcfileName = desfolderdir + "\\" + srcfileName;


    //                    if (!Directory.Exists(desfolderdir))
    //                    {
    //                        Directory.CreateDirectory(desfolderdir);
    //                    }


    //                    File.Copy(file, srcfileName);
    //                }
    //            }//foreach 
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }//function end 
    //    #endregion

    //    public static bool deleteExistFile(string path)
    //    {
    //        try
    //        {
    //            if (File.Exists(path))
    //            {
    //                File.Delete(path);
    //                if (File.Exists(path))
    //                {
    //                    return false;
    //                }
    //            }
    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }

    //    #region GetAllFileName Function
    //    public static List<string> fetchFileNameList(string folderPath, List<string> extensionList = null, bool hasExtension = true)
    //    {
    //        try
    //        {
    //            List<string> listFileName = new List<string>();
    //            foreach (string fileName in Directory.GetFiles(folderPath))
    //            {
    //                if (extensionList != null && extensionList.Count >= 1)
    //                {
    //                    string extension = Path.GetExtension(fileName);
    //                    if (extension.Trim().Length > 1)
    //                    {
    //                        extension = extension.Substring(1);
    //                    }
    //                    var queryResult =
    //                        from n in extensionList
    //                        where extension == n
    //                        select n;
    //                    if (queryResult == null || queryResult.Count() <= 0)
    //                    {
    //                        continue;
    //                    }
    //                }

    //                if (!hasExtension)
    //                {
    //                    listFileName.Add(Path.GetFileNameWithoutExtension(fileName));
    //                }
    //                else
    //                {
    //                    listFileName.Add(fileName);
    //                }
    //            }
    //            return listFileName;
    //        }
    //        catch (Exception)
    //        {
    //            // throw;
    //            return null;
    //        }

    //    }
    //    #endregion

    //    /// <summary>
    //    /// Only remove files in the folder.Not Remove child folder.Not remove files in the child folder.
    //    /// </summary>
    //    /// <param name="folderPath"></param>
    //    /// <returns></returns>
    //    public static bool removeFirstFloorFilesInFolder(string folderPath)
    //    {
    //        try
    //        {
    //            foreach (string str in System.IO.Directory.GetFiles(folderPath))
    //            {
    //                FileInfo info = new FileInfo(str);
    //                info.Attributes = FileAttributes.Normal;
    //                info.Delete();
    //            }
    //            return true;
    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// Remove all files and folders in the folder.
    //    /// </summary>
    //    /// <param name="folderPath"></param>
    //    /// <param name="needRemoveRootFolder"></param>
    //    public static bool removeAllFilesAndFolders(string folderPath, bool needRemoveRootFolder = false)
    //    {
    //        try
    //        {
    //            DirectoryInfo theFolder = new DirectoryInfo(folderPath);
    //            if (needRemoveRootFolder)
    //            {
    //                theFolder.Delete(true);
    //                return true;
    //            }
    //            else
    //            {
    //                DirectoryInfo[] dirInfo = theFolder.GetDirectories();
    //                foreach (DirectoryInfo NextFolder in dirInfo)
    //                    NextFolder.Delete(true);
    //            }
    //            removeFirstFloorFilesInFolder(folderPath);
    //            return true;
    //        }

    //        catch (System.Exception ex)
    //        {
    //            exception_pro = ex;
    //            return false;
    //        }
    //    }

    //    public static void removeAllocateFolder(string rootFolder, string allocateFolder)
    //    {
    //        DirectoryInfo theFolder = new DirectoryInfo(rootFolder);
    //        DirectoryInfo[] folders = theFolder.GetDirectories(allocateFolder, SearchOption.AllDirectories);
    //        foreach (DirectoryInfo folder in folders)
    //        {
    //            folder.Delete(true);
    //        }
    //    }

    //    public static List<DirectoryInfo> fetchFolderList(string folderPath)
    //    {
    //        try
    //        {
    //            List<DirectoryInfo> ListFolderDirInfo = new List<DirectoryInfo>();
    //            DirectoryInfo theFolder = new DirectoryInfo(folderPath);
    //            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
    //            foreach (DirectoryInfo NextFolder in dirInfo)
    //            {
    //                ListFolderDirInfo.Add(NextFolder);
    //            }

    //            return ListFolderDirInfo;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }



    //    #endregion
    //}
}
