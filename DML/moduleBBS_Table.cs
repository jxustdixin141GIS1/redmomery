using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class moduleBBS_Table
	{
		public int ID { get; set; }
		public int U_ID { get; set; }
		public string B_TITLE { get; set; }
		public object B_describe { get; set; }
		public DateTime C_TIME { get; set; }
		public DateTime U_TIME { get; set; }
		public int IS_pass { get; set; }
		public int N_TITLE_T { get; set; }
		public string M_Key { get; set; }
		public string MD5 { get; set; }
	}
}