using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class ROOT_INFO
	{
		public int ROOT_ID { get; set; }
		public string ROOT_NAME { get; set; }
		public string ROOT_PWD { get; set; }
		public string REAL_NAME { get; set; }
		public int TELPHONE { get; set; }
		public int STATUS { get; set; }
		public DateTime CREATTIME { get; set; }
		public DateTime UPTIME { get; set; }
	}
}
