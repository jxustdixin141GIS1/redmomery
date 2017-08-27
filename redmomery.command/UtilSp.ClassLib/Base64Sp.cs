using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilSp.ClassLib
{
    public class Base64Sp
    {
        /// <summary>
        /// To base 64 string.
        /// </summary>
        /// <param name="sourceStr">sourceStr</param>
        /// <param name="encoding">Encoding is null,use default encoding.</param>
        /// <returns></returns>
        public static string tobase64Str(string sourceStr,Encoding encoding=null)
        {
            if (encoding==null)
            {
                encoding = Encoding.Default;
            }
            byte[] bytes = encoding.GetBytes(sourceStr);
            string base64Str = Convert.ToBase64String(bytes);
            return base64Str;
        }
    }
}
