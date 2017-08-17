using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_C_CLDAL
	{
		public int AddNew(View_C_CL model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO View_C_CL(T_ID,C_ID,U_ID,CC_TIME,Context,n_y,USER_NETNAME) VALUES (@T_ID,@C_ID,@U_ID,@CC_TIME,@Context,@n_y,@USER_NETNAME);SELECT @@identity"
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@C_ID", model.C_ID)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@CC_TIME", model.CC_TIME)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@n_y", model.n_y)
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_C_CL WHERE T_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_C_CL model)
		{
			string sql = "UPDATE View_C_CL SET T_ID=@T_ID,C_ID=@C_ID,U_ID=@U_ID,CC_TIME=@CC_TIME,Context=@Context,n_y=@n_y,USER_NETNAME=@USER_NETNAME WHERE T_ID=@T_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@C_ID", model.C_ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@CC_TIME", model.CC_TIME)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@n_y", model.n_y)
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
			);
			return rows > 0;
		}

		public View_C_CL Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_C_CL WHERE T_ID=@T_ID", new SqlParameter("@T_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_C_CL model = ToModel(row);
			return model;
		}

		private static View_C_CL ToModel(DataRow row)
		{
			View_C_CL model = new View_C_CL();
			model.T_ID = (int)row["T_ID"];
			model.ID = (object)row["ID"];
			model.C_ID = (int)row["C_ID"];
			model.U_ID = (int)row["U_ID"];
			model.CC_TIME = (DateTime)row["CC_TIME"];
			model.Context = (object)row["Context"];
			model.n_y = (int)row["n_y"];
			model.USER_NETNAME = (string)row["USER_NETNAME"];
			return model;
		}

		public IEnumerable<View_C_CL> ListAll()
		{
			List<View_C_CL> list = new List<View_C_CL>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_C_CL");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
