using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class staticinfotableBLL
	{
		public int AddNew(staticinfotable model)
		{
			return new staticinfotableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new staticinfotableDAL().Delete(id);
		}

		public bool Update(staticinfotable model)
		{
			return new staticinfotableDAL().Update(model);
		}

		public staticinfotable Get(int id)
		{
			return new staticinfotableDAL().Get(id);
		}

		public IEnumerable<staticinfotable> ListAll()
		{
			return new staticinfotableDAL().ListAll();
		}
	}
}
