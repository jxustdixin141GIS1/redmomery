using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class USER_INFOBLL
	{
		public int AddNew(USER_INFO model)
		{
			return new USER_INFODAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new USER_INFODAL().Delete(id);
		}

		public bool Update(USER_INFO model)
		{
			return new USER_INFODAL().Update(model);
		}

		public USER_INFO Get(int id)
		{
			return new USER_INFODAL().Get(id);
		}

		public IEnumerable<USER_INFO> ListAll()
		{
			return new USER_INFODAL().ListAll();
		}
	}
}
