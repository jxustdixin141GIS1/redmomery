using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class sysdiagrams
	{
		public string name { get; set; }
		public int principal_id { get; set; }
		public int diagram_id { get; set; }
		public int version { get; set; }
		public object definition { get; set; }
	}
}
