using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace redmomery.Model
{
	public   partial class staticinfotable
	{
		public int ID { get; set; }
		public string citycode { get; set; }
		public string cityname { get; set; }
		public string province { get; set; }
		public string country { get; set; }
		public string countrycode { get; set; }
		public string lng { get; set; }
		public string lat { get; set; }
	}
}
