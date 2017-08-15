using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class WAR_INFO
	{
		public int WAR_ID { get; set; }
		public string WAR_NAME { get; set; }
		public DateTime WAR_TIME { get; set; }
		public string WAR_ADDRESS { get; set; }
		public object WAR_LOCX { get; set; }
		public object WAR_LOCY { get; set; }
		public object WAR_INTRO { get; set; }
	}
}
