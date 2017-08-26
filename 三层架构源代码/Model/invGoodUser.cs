using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	partial class invGoodUser
	{
		public int LUID { get; set; }
		public int RUID { get; set; }
		public DateTime ctime { get; set; }
		public string message { get; set; }
		public int isinv { get; set; }
	}
}
