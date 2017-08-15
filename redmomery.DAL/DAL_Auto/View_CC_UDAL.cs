using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_CC_UDAL
	{
		public int AddNew(View_CC_U model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO View_CC_U(CCID,C_ID,U_ID,CC_TIME,Context,is_delete,n_y,USER_NETNAME,USER_IMG) VALUES (@CCID,@C_ID,@U_ID,@CC_TIME,@Context,@is_delete,@n_y,@USER_NETNAME,@USER_IMG);SELECT @@identity"
				,new SqlParameter("@CCID", model.CCID)
				,new SqlParameter("@C_ID", model.C_ID)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@CC_TIME", model.CC_TIME)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@is_delete", model.is_delete)
				,new SqlParameter("@n_y", model.n_y)
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				,new SqlParameter("@USER_IMG", model.USER_IMG)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_CC_U WHERE CCID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_CC_U model)
		{
			string sql = "UPDATE View_CC_U SET CCID=@CCID,C_ID=@C_ID,U_ID=@U_ID,CC_TIME=@CC_TIME,Context=@Context,is_delete=@is_delete,n_y=@n_y,USER_NETNAME=@USER_NETNAME,USER_IMG=@USER_IMG WHERE CCID=@CCID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@CCID", model.CCID)
				, new SqlParameter("@C_ID", model.C_ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@CC_TIME", model.CC_TIME)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@is_delete", model.is_delete)
				, new SqlParameter("@n_y", model.n_y)
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@USER_IMG", model.USER_IMG)
			);
			return rows > 0;
		}

		public View_CC_U Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_CC_U WHERE CCID=@CCID", new SqlParameter("@CCID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_CC_U model = ToModel(row);
			return model;
		}

		private static View_CC_U ToModel(DataRow row)
		{
			View_CC_U model = new View_CC_U();
			model.CCID = (object)row["CCID"];
			model.C_ID = (int)row["C_ID"];
			model.U_ID = (int)row["U_ID"];
			model.CC_TIME = (DateTime)row["CC_TIME"];
			model.Context = (object)row["Context"];
			model.is_delete = (int)row["is_delete"];
			model.n_y = (int)row["n_y"];
			model.USER_NETNAME = (string)row["USER_NETNAME"];
            model.USER_IMG = (string)row["USER_IMG"].ToString() == "" ? "" : (string)row["USER_IMG"]; 
			return model;
		}

		public IEnumerable<View_CC_U> ListAll()
		{
			List<View_CC_U> list = new List<View_CC_U>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_CC_U");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
