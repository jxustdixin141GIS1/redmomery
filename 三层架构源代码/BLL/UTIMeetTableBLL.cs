using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class UTIMeetTableBLL
	{
		public int AddNew(UTIMeetTable model)
		{
			return new UTIMeetTableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new UTIMeetTableDAL().Delete(id);
		}

		public bool Update(UTIMeetTable model)
		{
			return new UTIMeetTableDAL().Update(model);
		}

		public UTIMeetTable Get(int id)
		{
			return new UTIMeetTableDAL().Get(id);
		}

		public IEnumerable<UTIMeetTable> ListAll()
		{
			return new UTIMeetTableDAL().ListAll();
		}
	}
}
