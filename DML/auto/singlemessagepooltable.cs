using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	partial class singlemessagepooltable
	{
		public int FUID { get; set; }
		public int TUID { get; set; }
		public DateTime Ftime { get; set; }
		public string Context { get; set; }
		public int isread { get; set; }
		public string MD5 { get; set; }
	}
}
