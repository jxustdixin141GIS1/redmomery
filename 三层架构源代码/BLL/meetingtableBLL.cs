using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class meetingtableBLL
	{
		public int AddNew(meetingtable model)
		{
			return new meetingtableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new meetingtableDAL().Delete(id);
		}

		public bool Update(meetingtable model)
		{
			return new meetingtableDAL().Update(model);
		}

		public meetingtable Get(int id)
		{
			return new meetingtableDAL().Get(id);
		}

		public IEnumerable<meetingtable> ListAll()
		{
			return new meetingtableDAL().ListAll();
		}
	}
}
