using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class LB_INFOBLL
	{
		public int AddNew(LB_INFO model)
		{
			return new LB_INFODAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new LB_INFODAL().Delete(id);
		}

		public bool Update(LB_INFO model)
		{
			return new LB_INFODAL().Update(model);
		}

		public LB_INFO Get(int id)
		{
			return new LB_INFODAL().Get(id);
		}

		public IEnumerable<LB_INFO> ListAll()
		{
			return new LB_INFODAL().ListAll();
		}
	}
}
