using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class GEN_INFODAL
	{
		public int addNew(GEN_INFO model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(GEN_INFO model)
		{
            return Update(model);
		}

		public GEN_INFO get(int id)
		{
            return Get(id);
		}

	
		public IEnumerable<GEN_INFO> Listall()
		{
            return ListAll();
		}
	}
}
