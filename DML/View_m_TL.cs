using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class View_m_TL
	{
		public string USER_NETNAME { get; set; }
		public int U_ID { get; set; }
		public int M_ID { get; set; }
		public int ID { get; set; }
		public DateTime F_TIME { get; set; }
		public int N_RESPONSE { get; set; }
		public string TITLE { get; set; }
		public object Context { get; set; }
		public int N_YES { get; set; }
		public int Authonrity { get; set; }
		public string B_TITLE { get; set; }
		public object B_describe { get; set; }
		public int N_TITLE_T { get; set; }
		public string T_key { get; set; }
	}
}
