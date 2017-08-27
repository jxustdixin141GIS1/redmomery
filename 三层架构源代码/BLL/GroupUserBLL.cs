using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class GroupUserBLL
	{
		public int AddNew(GroupUser model)
		{
			return new GroupUserDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new GroupUserDAL().Delete(id);
		}

		public bool Update(GroupUser model)
		{
			return new GroupUserDAL().Update(model);
		}

		public GroupUser Get(int id)
		{
			return new GroupUserDAL().Get(id);
		}

		public IEnumerable<GroupUser> ListAll()
		{
			return new GroupUserDAL().ListAll();
		}
	}
}
