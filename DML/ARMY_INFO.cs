using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class ARMY_INFO
	{
		public int ARMY_ID { get; set; }
		public string ARMY_NAME { get; set; }
		public DateTime ARMY_TIME { get; set; }
		public string ARMY_ADDRESS { get; set; }
		public object ARMY_LOCX { get; set; }
		public object ARMY_LOCY { get; set; }
		public object ARMY_INTRO { get; set; }
	}
}
