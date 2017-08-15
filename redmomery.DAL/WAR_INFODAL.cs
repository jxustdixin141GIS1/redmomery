using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class WAR_INFODAL
	{
		public int addNew(WAR_INFO model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(WAR_INFO model)
		{
            return Update(model);
		}

		public WAR_INFO get(int id)
		{
            return Get(id);
		}
		public IEnumerable<WAR_INFO> Listall()
		{
            return ListAll();
		}
	}
}
