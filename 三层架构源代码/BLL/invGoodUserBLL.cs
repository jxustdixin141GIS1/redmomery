using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class invGoodUserBLL
	{
		public int AddNew(invGoodUser model)
		{
			return new invGoodUserDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new invGoodUserDAL().Delete(id);
		}

		public bool Update(invGoodUser model)
		{
			return new invGoodUserDAL().Update(model);
		}

		public invGoodUser Get(int id)
		{
			return new invGoodUserDAL().Get(id);
		}

		public IEnumerable<invGoodUser> ListAll()
		{
			return new invGoodUserDAL().ListAll();
		}
	}
}
