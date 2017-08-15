using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class SHAPE_TABLEDAL
	{
		public int addNew(SHAPE_TABLE model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(SHAPE_TABLE model)
		{
            return Update(model);
		}

		public SHAPE_TABLE get(int id)
		{
            return Get(id);
		}

		public IEnumerable<SHAPE_TABLE> Listall()
		{
            return ListAll();
		}
	}
}
