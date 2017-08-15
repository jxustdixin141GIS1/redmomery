using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class ROOT_INFODAL
	{
		public int addNew(ROOT_INFO model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(ROOT_INFO model)
		{
            return Update(model);
		}

		public ROOT_INFO get(int id)
		{
            return Get(id);
		}

	

		public IEnumerable<ROOT_INFO> Listall()
		{
            return ListAll();
		}
	}
}
