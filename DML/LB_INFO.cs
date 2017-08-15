using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
namespace redmomery.Model
{
	public partial class LB_INFO
	{
		public int ID { get; set; }
		public int T_ID { get; set; }
		public string LBname { get; set; }
		public string LBjob { get; set; }
		public string LBsex { get; set; }
		public string LBbirthday { get; set; }
		public string LBdomicile { get; set; }
		public object designation { get; set; }
		public object LBexperience { get; set; }
		public object LBlife { get; set; }
		public string LBPhoto { get; set; }
		public object X { get; set; }
		public object Y { get; set; }
         [JsonIgnore]
		public SqlGeography Location { get; set; }
	}
}
