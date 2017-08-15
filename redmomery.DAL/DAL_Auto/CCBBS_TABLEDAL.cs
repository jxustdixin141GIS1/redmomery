using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class CCBBS_TABLEDAL
	{
		public int AddNew(CCBBS_TABLE model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO CCBBS_TABLE(C_ID,U_ID,CC_TIME,Context,is_delete,n_y) VALUES (@C_ID,@U_ID,@CC_TIME,@Context,@is_delete,@n_y);SELECT @@identity"
				,new SqlParameter("@C_ID", model.C_ID)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@CC_TIME", model.CC_TIME)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@is_delete", model.is_delete)
				,new SqlParameter("@n_y", model.n_y)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM CCBBS_TABLE WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(CCBBS_TABLE model)
		{
			string sql = "UPDATE CCBBS_TABLE SET C_ID=@C_ID,U_ID=@U_ID,CC_TIME=@CC_TIME,Context=@Context,is_delete=@is_delete,n_y=@n_y WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@C_ID", model.C_ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@CC_TIME", model.CC_TIME)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@is_delete", model.is_delete)
				, new SqlParameter("@n_y", model.n_y)
			);
			return rows > 0;
		}

		public CCBBS_TABLE Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CCBBS_TABLE WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			CCBBS_TABLE model = ToModel(row);
			return model;
		}

		private static CCBBS_TABLE ToModel(DataRow row)
		{
			CCBBS_TABLE model = new CCBBS_TABLE();
			model.ID = (object)row["ID"];
			model.C_ID = (int)row["C_ID"];
			model.U_ID = (int)row["U_ID"];
			model.CC_TIME = (DateTime)row["CC_TIME"];
			model.Context = (object)row["Context"];
			model.is_delete = (int)row["is_delete"];
			model.n_y = (int)row["n_y"];
			return model;
		}

		public IEnumerable<CCBBS_TABLE> ListAll()
		{
			List<CCBBS_TABLE> list = new List<CCBBS_TABLE>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CCBBS_TABLE");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
