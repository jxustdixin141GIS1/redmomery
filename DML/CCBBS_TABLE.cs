using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class CCBBS_TABLE
	{
		public object ID { get; set; }
		public int C_ID { get; set; }
		public int U_ID { get; set; }
		public DateTime CC_TIME { get; set; }
		public object Context { get; set; }
		public int is_delete { get; set; }
		public int n_y { get; set; }
	}
}