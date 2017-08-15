using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class PUBLIC_RECORD
	{
		public int PUB_ID { get; set; }
		public string PUB_ADDRESS { get; set; }
		public string IP { get; set; }
		public int STATE { get; set; }
		public DateTime CREATTIME { get; set; }
	}
}
