using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class ARMY_INFODAL
	{
		public int addNew(ARMY_INFO model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(ARMY_INFO model)
		{
            return Update(model);
		}

		public ARMY_INFO get(int id)
		{
            return Get(id);
		}

		public IEnumerable<ARMY_INFO> Listall()
		{
            return ListAll();
		}
	}
}
