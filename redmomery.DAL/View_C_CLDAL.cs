using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class View_C_CLDAL
	{
		public int addNew(View_C_CL model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(View_C_CL model)
		{
            return Update(model);
		}

		public View_C_CL get(int id)
		{
            return Get(id);
		}
		public IEnumerable<View_C_CL> Listall()
		{
            return ListAll();
		}
	}
}
