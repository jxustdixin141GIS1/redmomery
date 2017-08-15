using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
  public partial class USER_INFO
	{
		public int USER_ID { get; set; }
		public string USER_NAME { get; set; }
		public string USER_SEX { get; set; }
		public string USER_JOB { get; set; }
		public DateTime USER_BIRTHDAY { get; set; }
		public string USER_ADDRESS { get; set; }
		public string USER_PHONE { get; set; }
		public string USER_EMEIL { get; set; }
		public string USER_NETNAME { get; set; }
		public string USER_IMG { get; set; }
		public string USER_PSWD { get; set; }
		public string MD5 { get; set; }
	}
}
