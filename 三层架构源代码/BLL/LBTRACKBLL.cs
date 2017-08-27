using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class LBTRACKBLL
	{
		public int AddNew(LBTRACK model)
		{
			return new LBTRACKDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new LBTRACKDAL().Delete(id);
		}

		public bool Update(LBTRACK model)
		{
			return new LBTRACKDAL().Update(model);
		}

		public LBTRACK Get(int id)
		{
			return new LBTRACKDAL().Get(id);
		}

		public IEnumerable<LBTRACK> ListAll()
		{
			return new LBTRACKDAL().ListAll();
		}
	}
}
