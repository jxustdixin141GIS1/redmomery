using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class View_MultimessageBLL
	{
		public int AddNew(View_Multimessage model)
		{
			return new View_MultimessageDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new View_MultimessageDAL().Delete(id);
		}

		public bool Update(View_Multimessage model)
		{
			return new View_MultimessageDAL().Update(model);
		}

		public View_Multimessage Get(int id)
		{
			return new View_MultimessageDAL().Get(id);
		}

		public IEnumerable<View_Multimessage> ListAll()
		{
			return new View_MultimessageDAL().ListAll();
		}
	}
}
