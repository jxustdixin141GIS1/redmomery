using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class BBSCONTENTSDAL
	{
		public int addNew(BBSCONTENTS model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(BBSCONTENTS model)
		{
            return Update(model);
		}

		public BBSCONTENTS get(int id)
		{
            return Get(id);
		}

	
		public IEnumerable<BBSCONTENTS> Listall()
		{
            return ListAll();
		}


	}
}
