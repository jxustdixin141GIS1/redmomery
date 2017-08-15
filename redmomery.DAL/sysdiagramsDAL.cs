using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class sysdiagramsDAL
	{
		public int addNew(sysdiagrams model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
              return   Delete(id);
		}

		public bool update(sysdiagrams model)
		{
            return Update(model);
		}

		public sysdiagrams get(int id)
		{
            return Get(id);
		}
		
		public IEnumerable<sysdiagrams> Listall()
		{
            return ListAll();
		}
	}
}
