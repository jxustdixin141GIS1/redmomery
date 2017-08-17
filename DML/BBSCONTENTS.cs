using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class BBSCONTENTS
	{
		public int BSS_ID { get; set; }
		public object CONNAME { get; set; }
		public object CONREXT { get; set; }
		public DateTime WRITEDATE { get; set; }
		public int WRITEID { get; set; }
		public int REPLYID { get; set; }
		public DateTime CRTIME { get; set; }
		public int KINDID { get; set; }
		public string KINDNAME { get; set; }
	}
}
