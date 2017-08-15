using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public partial class SHAPE_TABLE
	{
		public int ID { get; set; }
		public int U_ID { get; set; }
		public int TYPE { get; set; }
		public int M_ID { get; set; }
		public int T_ID { get; set; }
		public string shape_tablename { get; set; }
		public string Name { get; set; }
		public object description { get; set; }
		public DateTime C_TIME { get; set; }
		public DateTime U_TIME { get; set; }
	}
}
