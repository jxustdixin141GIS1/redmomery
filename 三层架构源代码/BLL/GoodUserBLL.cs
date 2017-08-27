using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class GoodUserBLL
	{
		public int AddNew(GoodUser model)
		{
			return new GoodUserDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new GoodUserDAL().Delete(id);
		}

		public bool Update(GoodUser model)
		{
			return new GoodUserDAL().Update(model);
		}

		public GoodUser Get(int id)
		{
			return new GoodUserDAL().Get(id);
		}

		public IEnumerable<GoodUser> ListAll()
		{
			return new GoodUserDAL().ListAll();
		}
	}
}
