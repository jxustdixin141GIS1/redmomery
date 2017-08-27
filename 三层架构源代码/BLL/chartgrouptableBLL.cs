using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class chartgrouptableBLL
	{
		public int AddNew(chartgrouptable model)
		{
			return new chartgrouptableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new chartgrouptableDAL().Delete(id);
		}

		public bool Update(chartgrouptable model)
		{
			return new chartgrouptableDAL().Update(model);
		}

		public chartgrouptable Get(int id)
		{
			return new chartgrouptableDAL().Get(id);
		}

		public IEnumerable<chartgrouptable> ListAll()
		{
			return new chartgrouptableDAL().ListAll();
		}
	}
}
