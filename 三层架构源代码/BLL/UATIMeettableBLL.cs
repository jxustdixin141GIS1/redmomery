using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class UATIMeettableBLL
	{
		public int AddNew(UATIMeettable model)
		{
			return new UATIMeettableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new UATIMeettableDAL().Delete(id);
		}

		public bool Update(UATIMeettable model)
		{
			return new UATIMeettableDAL().Update(model);
		}

		public UATIMeettable Get(int id)
		{
			return new UATIMeettableDAL().Get(id);
		}

		public IEnumerable<UATIMeettable> ListAll()
		{
			return new UATIMeettableDAL().ListAll();
		}
	}
}
