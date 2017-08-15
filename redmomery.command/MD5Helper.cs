using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace  redmomery.Common
{
    public static class MD5Helper
    {
        /// <summary>
        /// 字符串MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncryptString(string str)
        {
            using (MD5 md5String = MD5.Create())
            {
                StringBuilder sb = new StringBuilder();
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                byte[] md5Encrypt = md5String.ComputeHash(bytes);
                for (int i = 0; i < md5Encrypt.Length; i++)
                {
                    sb.Append(md5Encrypt[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 文件加密
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string EncryptFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (MD5 md5Encrypt = MD5.Create())
                {
                    byte[] encryptBytes = md5Encrypt.ComputeHash(fs);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < encryptBytes.Length; i++)
                    {
                        sb.Append(encryptBytes[i].ToString("x2"));
                    }
                    return sb.ToString();
                }
            }
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static String GetStreamMD5(Stream stream)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            arrbytHashValue = oMD5Hasher.ComputeHash(stream); //计算指定Stream 对象的哈希值
            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = System.BitConverter.ToString(arrbytHashValue);
            //替换-
            strHashData = strHashData.Replace("-", "");
            strResult = strHashData;
            return strResult;
        }
    }
}
