using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_CT_UDAL
	{
		public int AddNew(View_CT_U model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO View_CT_U(C_ID,T_ID,U_ID,F_TIME,Context,n_c,is_delete,n_y,USER_NETNAME,USER_IMG) VALUES (@C_ID,@T_ID,@U_ID,@F_TIME,@Context,@n_c,@is_delete,@n_y,@USER_NETNAME,@USER_IMG);SELECT @@identity"
				,new SqlParameter("@C_ID", model.C_ID)
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@F_TIME", model.F_TIME)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@n_c", model.n_c)
				,new SqlParameter("@is_delete", model.is_delete)
				,new SqlParameter("@n_y", model.n_y)
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				,new SqlParameter("@USER_IMG", model.USER_IMG)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_CT_U WHERE C_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_CT_U model)
		{
			string sql = "UPDATE View_CT_U SET C_ID=@C_ID,T_ID=@T_ID,U_ID=@U_ID,F_TIME=@F_TIME,Context=@Context,n_c=@n_c,is_delete=@is_delete,n_y=@n_y,USER_NETNAME=@USER_NETNAME,USER_IMG=@USER_IMG WHERE C_ID=@C_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@C_ID", model.C_ID)
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@F_TIME", model.F_TIME)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@n_c", model.n_c)
				, new SqlParameter("@is_delete", model.is_delete)
				, new SqlParameter("@n_y", model.n_y)
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@USER_IMG", model.USER_IMG)
			);
			return rows > 0;
		}

		public View_CT_U Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_CT_U WHERE C_ID=@C_ID", new SqlParameter("@C_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_CT_U model = ToModel(row);
			return model;
		}

		private static View_CT_U ToModel(DataRow row)
		{
			View_CT_U model = new View_CT_U();
			model.C_ID = (int)row["C_ID"];
			model.T_ID = (int)row["T_ID"];
			model.U_ID = (int)row["U_ID"];
			model.F_TIME = (DateTime)row["F_TIME"];
			model.Context = (object)row["Context"];
			model.n_c = (int)row["n_c"];
			model.is_delete = (int)row["is_delete"];
			model.n_y = (int)row["n_y"];
			model.USER_NETNAME = (string)row["USER_NETNAME"];
            model.USER_IMG = (string)row["USER_IMG"].ToString() == "" ? "" : (string)row["USER_IMG"]; 
			return model;
		}

		public IEnumerable<View_CT_U> ListAll()
		{
			List<View_CT_U> list = new List<View_CT_U>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_CT_U");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
