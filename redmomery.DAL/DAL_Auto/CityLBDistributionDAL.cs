using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class CityLBDistributionDAL
	{
		public int AddNew(CityLBDistribution model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO CityLBDistribution(CityName,CityCode,LBnum) VALUES (@CityName,@CityCode,@LBnum);SELECT @@identity"
				,new SqlParameter("@CityName", model.CityName)
				,new SqlParameter("@CityCode", model.CityCode)
				,new SqlParameter("@LBnum", model.LBnum)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM CityLBDistribution WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(CityLBDistribution model)
		{
			string sql = "UPDATE CityLBDistribution SET CityName=@CityName,CityCode=@CityCode,LBnum=@LBnum WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@CityName", model.CityName)
				, new SqlParameter("@CityCode", model.CityCode)
				, new SqlParameter("@LBnum", model.LBnum)
			);
			return rows > 0;
		}

		public CityLBDistribution Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CityLBDistribution WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			CityLBDistribution model = ToModel(row);
			return model;
		}

		private static CityLBDistribution ToModel(DataRow row)
		{
			CityLBDistribution model = new CityLBDistribution();
			model.ID = (int)row["ID"];
			model.CityName = (string)row["CityName"];
			model.CityCode = (string)row["CityCode"];
			model.LBnum = (int)row["LBnum"];
			return model;
		}

		public IEnumerable<CityLBDistribution> ListAll()
		{
			List<CityLBDistribution> list = new List<CityLBDistribution>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CityLBDistribution");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
