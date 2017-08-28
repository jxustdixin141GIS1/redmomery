using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	partial class UATIMeettable
	{
		public int UID { get; set; }
		public int meetID { get; set; }
		public DateTime dataTime { get; set; }
		public string contentApply { get; set; }
		public int state { get; set; }
		public int dealUID { get; set; }
	}
}
