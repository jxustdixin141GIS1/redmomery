using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class staticinfotableDAL
	{
		public int AddNew(staticinfotable model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO staticinfotable(citycode,cityname,province,country,countrycode,lng,lat) VALUES (@citycode,@cityname,@province,@country,@countrycode,@lng,@lat);SELECT @@identity"
				,new SqlParameter("@citycode", model.citycode)
				,new SqlParameter("@cityname", model.cityname)
				,new SqlParameter("@province", model.province)
				,new SqlParameter("@country", model.country)
				,new SqlParameter("@countrycode", model.countrycode)
				,new SqlParameter("@lng", model.lng)
				,new SqlParameter("@lat", model.lat)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM staticinfotable WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(staticinfotable model)
		{
			string sql = "UPDATE staticinfotable SET citycode=@citycode,cityname=@cityname,province=@province,country=@country,countrycode=@countrycode,lng=@lng,lat=@lat WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@citycode", model.citycode)
				, new SqlParameter("@cityname", model.cityname)
				, new SqlParameter("@province", model.province)
				, new SqlParameter("@country", model.country)
				, new SqlParameter("@countrycode", model.countrycode)
				, new SqlParameter("@lng", model.lng)
				, new SqlParameter("@lat", model.lat)
			);
			return rows > 0;
		}

		public staticinfotable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM staticinfotable WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			staticinfotable model = ToModel(row);
			return model;
		}

		private static staticinfotable ToModel(DataRow row)
		{
			staticinfotable model = new staticinfotable();
			model.ID = (int)row["ID"];
			model.citycode = (string)row["citycode"];
			model.cityname = (string)row["cityname"];
			model.province = (string)row["province"];
			model.country = (string)row["country"];
			model.countrycode = (string)row["countrycode"];
			model.lng = (string)row["lng"];
			model.lat = (string)row["lat"];
			return model;
		}

		public IEnumerable<staticinfotable> ListAll()
		{
			List<staticinfotable> list = new List<staticinfotable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM staticinfotable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
