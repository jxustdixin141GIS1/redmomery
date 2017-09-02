using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	partial class chartgrouptable
	{
		public int ID { get; set; }
		public int UID { get; set; }
		public DateTime Ctime { get; set; }
		public string groupName { get; set; }
		public string description { get; set; }
		public string img { get; set; }
		public int vnum { get; set; }
	}
}
