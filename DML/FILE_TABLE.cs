using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class FILE_TABLE
	{
		public int ID { get; set; }
		public int U_ID { get; set; }
		public int M_ID { get; set; }
		public int T_ID { get; set; }
		public string url { get; set; }
		public string Name { get; set; }
		public int N_View { get; set; }
		public string Keyvalues { get; set; }
	}
}
