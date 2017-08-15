using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class PUBLIC_RECORDDAL
	{
		public int addNew(PUBLIC_RECORD model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(PUBLIC_RECORD model)
		{
            return Update(model);
		}

		public PUBLIC_RECORD get(int id)
		{
         return    Get(id);
		}
		public IEnumerable<PUBLIC_RECORD> Listall()
		{
            return ListAll();
		}
	}
}
