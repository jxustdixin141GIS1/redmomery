using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class singlemessagepooltableBLL
	{
		public int AddNew(singlemessagepooltable model)
		{
			return new singlemessagepooltableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new singlemessagepooltableDAL().Delete(id);
		}

		public bool Update(singlemessagepooltable model)
		{
			return new singlemessagepooltableDAL().Update(model);
		}

		public singlemessagepooltable Get(int id)
		{
			return new singlemessagepooltableDAL().Get(id);
		}

		public IEnumerable<singlemessagepooltable> ListAll()
		{
			return new singlemessagepooltableDAL().ListAll();
		}
	}
}
