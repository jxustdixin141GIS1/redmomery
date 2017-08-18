using System.Collections.Generic;
using System.Text;
using redmomery.Model;
using redmomery.DAL;

namespace redmomery.BLL
{
	partial class CityLBDistributionBLL
	{
		public int AddNew(CityLBDistribution model)
		{
			return new CityLBDistributionDAL().AddNew(model);
		}

		public bool Delete(int id)
		{
			return new CityLBDistributionDAL().Delete(id);
		}

		public bool Update(CityLBDistribution model)
		{
			return new CityLBDistributionDAL().Update(model);
		}

		public CityLBDistribution Get(int id)
		{
			return new CityLBDistributionDAL().Get(id);
		}

		public IEnumerable<CityLBDistribution> ListAll()
		{
			return new CityLBDistributionDAL().ListAll();
		}
	}
}
