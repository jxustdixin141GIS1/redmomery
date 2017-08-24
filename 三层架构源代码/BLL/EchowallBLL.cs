using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class EchowallBLL
	{
		public int AddNew(Echowall model)
		{
			return new EchowallDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new EchowallDAL().Delete(id);
		}

		public bool Update(Echowall model)
		{
			return new EchowallDAL().Update(model);
		}

		public Echowall Get(int id)
		{
			return new EchowallDAL().Get(id);
		}

		public IEnumerable<Echowall> ListAll()
		{
			return new EchowallDAL().ListAll();
		}
	}
}
