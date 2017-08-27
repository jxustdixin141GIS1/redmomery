using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilSp.ClassLib
{
    public class RegexSp
    {
        public const string ip = @"^\d{0,3}\.\d{0,3}\.\d{0,3}\.\d{0,3}$";
        public const string mail = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    }
}
