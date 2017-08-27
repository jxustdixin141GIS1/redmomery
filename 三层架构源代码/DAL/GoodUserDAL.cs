using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class GoodUserDAL
	{
		public int AddNew(GoodUser model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO GoodUser(LUID,RUID,groupname,state) VALUES (@LUID,@RUID,@groupname,@state);SELECT @@identity"
				,new SqlParameter("@LUID", model.LUID)
				,new SqlParameter("@RUID", model.RUID)
				,new SqlParameter("@groupname", model.groupname)
				,new SqlParameter("@state", model.state)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM GoodUser WHERE LUID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(GoodUser model)
		{
			string sql = "UPDATE GoodUser SET LUID=@LUID,RUID=@RUID,groupname=@groupname,state=@state WHERE LUID=@LUID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@LUID", model.LUID)
				, new SqlParameter("@RUID", model.RUID)
				, new SqlParameter("@groupname", model.groupname)
				, new SqlParameter("@state", model.state)
			);
			return rows > 0;
		}

		public GoodUser Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GoodUser WHERE LUID=@LUID", new SqlParameter("@LUID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			GoodUser model = ToModel(row);
			return model;
		}

		private static GoodUser ToModel(DataRow row)
		{
			GoodUser model = new GoodUser();
			model.LUID = (int)row["LUID"];
			model.RUID = (int)row["RUID"];
			model.groupname = (string)row["groupname"];
			model.state = (int)row["state"];
			return model;
		}

		public IEnumerable<GoodUser> ListAll()
		{
			List<GoodUser> list = new List<GoodUser>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GoodUser");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
