using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class cityLBBLL
	{
		public int AddNew(cityLB model)
		{
			return new cityLBDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new cityLBDAL().Delete(id);
		}

		public bool Update(cityLB model)
		{
			return new cityLBDAL().Update(model);
		}

		public cityLB Get(int id)
		{
			return new cityLBDAL().Get(id);
		}

		public IEnumerable<cityLB> ListAll()
		{
			return new cityLBDAL().ListAll();
		}
	}
}
