using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class cityLBDAL
	{
		public int AddNew(cityLB model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO cityLB(LBID,CityName,CityCode) VALUES (@LBID,@CityName,@CityCode);SELECT @@identity"
				,new SqlParameter("@LBID", model.LBID)
				,new SqlParameter("@CityName", model.CityName)
				,new SqlParameter("@CityCode", model.CityCode)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM cityLB WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(cityLB model)
		{
			string sql = "UPDATE cityLB SET LBID=@LBID,CityName=@CityName,CityCode=@CityCode WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@LBID", model.LBID)
				, new SqlParameter("@CityName", model.CityName)
				, new SqlParameter("@CityCode", model.CityCode)
			);
			return rows > 0;
		}

		public cityLB Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM cityLB WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			cityLB model = ToModel(row);
			return model;
		}

		private static cityLB ToModel(DataRow row)
		{
			cityLB model = new cityLB();
			model.ID = (int)row["ID"];
			model.LBID = (int)row["LBID"];
			model.CityName = (string)row["CityName"];
			model.CityCode = (string)row["CityCode"];
			return model;
		}

		public IEnumerable<cityLB> ListAll()
		{
			List<cityLB> list = new List<cityLB>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM cityLB");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
