using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class CTBBS_TABLE
	{
		public int ID { get; set; }
		public int T_ID { get; set; }
		public int U_ID { get; set; }
		public DateTime F_TIME { get; set; }
		public object Context { get; set; }
		public int n_c { get; set; }
		public int is_delete { get; set; }
		public int n_y { get; set; }
	}
}
