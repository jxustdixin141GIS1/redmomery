using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class trackBLL
	{
		public int AddNew(track model)
		{
			return new trackDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new trackDAL().Delete(id);
		}

		public bool Update(track model)
		{
			return new trackDAL().Update(model);
		}

		public track Get(int id)
		{
			return new trackDAL().Get(id);
		}

		public IEnumerable<track> ListAll()
		{
			return new trackDAL().ListAll();
		}
	}
}
