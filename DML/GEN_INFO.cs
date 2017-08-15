using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class GEN_INFO
	{
		public int GEN_ID { get; set; }
		public string GEN_NAME { get; set; }
		public DateTime GEN_BIRTH { get; set; }
		public DateTime GEN_EVTIME { get; set; }
		public string GEN_ADDRESS { get; set; }
		public object GEN_LOCX { get; set; }
		public object GEN_LOCY { get; set; }
		public object GEN_INTRO { get; set; }
	}
}
