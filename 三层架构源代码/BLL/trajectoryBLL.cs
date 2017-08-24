using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class trajectoryBLL
	{
		public int AddNew(trajectory model)
		{
			return new trajectoryDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new trajectoryDAL().Delete(id);
		}

		public bool Update(trajectory model)
		{
			return new trajectoryDAL().Update(model);
		}

		public trajectory Get(int id)
		{
			return new trajectoryDAL().Get(id);
		}

		public IEnumerable<trajectory> ListAll()
		{
			return new trajectoryDAL().ListAll();
		}
	}
}
