using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UtilSp.ClassLib
{
    public class MailSp
    {
        #region isMail Function
        public static bool isMail(string mailAddressStr)
        {
            Regex reg = new Regex(RegexSp.mail);
            return reg.IsMatch(mailAddressStr);
        }
        #endregion
    }
}
