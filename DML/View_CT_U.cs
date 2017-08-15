using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
   public	partial class View_CT_U
	{
		public int C_ID { get; set; }
		public int T_ID { get; set; }
		public int U_ID { get; set; }
		public DateTime F_TIME { get; set; }
		public object Context { get; set; }
		public int n_c { get; set; }
		public int is_delete { get; set; }
		public int n_y { get; set; }
		public string USER_NETNAME { get; set; }
		public string USER_IMG { get; set; }
	}
}
