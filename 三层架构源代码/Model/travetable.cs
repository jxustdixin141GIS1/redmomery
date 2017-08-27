using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	partial class travetable
	{
		public int ID { get; set; }
		public DateTime Utime { get; set; }
		public int UID { get; set; }
		public int meetID { get; set; }
		public string meetsubject { get; set; }
		public string context { get; set; }
		public string local { get; set; }
		public string Vtime { get; set; }
		public object lng { get; set; }
		public object lat { get; set; }
		public object point { get; set; }
		public int isOK { get; set; }
		public int nOK { get; set; }
		public int nY { get; set; }
	}
}
