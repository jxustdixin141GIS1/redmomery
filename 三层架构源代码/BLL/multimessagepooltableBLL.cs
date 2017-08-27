using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class multimessagepooltableBLL
	{
		public int AddNew(multimessagepooltable model)
		{
			return new multimessagepooltableDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new multimessagepooltableDAL().Delete(id);
		}

		public bool Update(multimessagepooltable model)
		{
			return new multimessagepooltableDAL().Update(model);
		}

		public multimessagepooltable Get(int id)
		{
			return new multimessagepooltableDAL().Get(id);
		}

		public IEnumerable<multimessagepooltable> ListAll()
		{
			return new multimessagepooltableDAL().ListAll();
		}
	}
}
