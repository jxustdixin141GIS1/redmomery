using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class invGoodUserDAL
	{
		public int AddNew(invGoodUser model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO invGoodUser(LUID,RUID,ctime,message,isinv) VALUES (@LUID,@RUID,@ctime,@message,@isinv);SELECT @@identity"
				,new SqlParameter("@LUID", model.LUID)
				,new SqlParameter("@RUID", model.RUID)
				,new SqlParameter("@ctime", model.ctime)
				,new SqlParameter("@message", model.message)
				,new SqlParameter("@isinv", model.isinv)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM invGoodUser WHERE LUID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(invGoodUser model)
		{
			string sql = "UPDATE invGoodUser SET LUID=@LUID,RUID=@RUID,ctime=@ctime,message=@message,isinv=@isinv WHERE LUID=@LUID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@LUID", model.LUID)
				, new SqlParameter("@RUID", model.RUID)
				, new SqlParameter("@ctime", model.ctime)
				, new SqlParameter("@message", model.message)
				, new SqlParameter("@isinv", model.isinv)
			);
			return rows > 0;
		}

		public invGoodUser Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM invGoodUser WHERE LUID=@LUID", new SqlParameter("@LUID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			invGoodUser model = ToModel(row);
			return model;
		}

		private static invGoodUser ToModel(DataRow row)
		{
			invGoodUser model = new invGoodUser();
			model.LUID = (int)row["LUID"];
			model.RUID = (int)row["RUID"];
			model.ctime = (DateTime)row["ctime"];
			model.message = (string)row["message"];
			model.isinv = (int)row["isinv"];
			return model;
		}

		public IEnumerable<invGoodUser> ListAll()
		{
			List<invGoodUser> list = new List<invGoodUser>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM invGoodUser");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
