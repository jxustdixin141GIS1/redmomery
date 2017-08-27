using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	partial class trajectory
	{
		public object ID { get; set; }
		public DateTime T_time { get; set; }
		public string Timetext { get; set; }
		public string Local { get; set; }
		public string context { get; set; }
		public string x { get; set; }
		public string y { get; set; }
		public int LBID { get; set; }
		public int isCurrent { get; set; }
	}
}
