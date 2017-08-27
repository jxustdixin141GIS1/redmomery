using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class travetableBLL
	{
		public int AddNew(travetable model)
		{
			return new travetableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new travetableDAL().Delete(id);
		}

		public bool Update(travetable model)
		{
			return new travetableDAL().Update(model);
		}

		public travetable Get(int id)
		{
			return new travetableDAL().Get(id);
		}

		public IEnumerable<travetable> ListAll()
		{
			return new travetableDAL().ListAll();
		}
	}
}
