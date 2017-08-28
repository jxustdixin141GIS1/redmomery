using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	partial class meetingtable
	{
		public int ID { get; set; }
		public int UID { get; set; }
		public int GID { get; set; }
		public DateTime Ttime { get; set; }
		public string local { get; set; }
		public string contentTitle { get; set; }
		public string context { get; set; }
		public DateTime meetTime { get; set; }
		public int vnum { get; set; }
		public int isCheck { get; set; }
		public object lng { get; set; }
		public object lat { get; set; }
	}
}
