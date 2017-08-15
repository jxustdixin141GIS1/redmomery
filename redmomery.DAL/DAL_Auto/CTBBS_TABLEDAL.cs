using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class CTBBS_TABLEDAL
	{
		public int AddNew(CTBBS_TABLE model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO CTBBS_TABLE(T_ID,U_ID,F_TIME,Context,n_c,is_delete,n_y) VALUES (@T_ID,@U_ID,@F_TIME,@Context,@n_c,@is_delete,@n_y);SELECT @@identity"
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@F_TIME", model.F_TIME)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@n_c", model.n_c)
				,new SqlParameter("@is_delete", model.is_delete)
				,new SqlParameter("@n_y", model.n_y)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM CTBBS_TABLE WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(CTBBS_TABLE model)
		{
			string sql = "UPDATE CTBBS_TABLE SET T_ID=@T_ID,U_ID=@U_ID,F_TIME=@F_TIME,Context=@Context,n_c=@n_c,is_delete=@is_delete,n_y=@n_y WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@F_TIME", model.F_TIME)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@n_c", model.n_c)
				, new SqlParameter("@is_delete", model.is_delete)
				, new SqlParameter("@n_y", model.n_y)
			);
			return rows > 0;
		}

		public CTBBS_TABLE Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CTBBS_TABLE WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			CTBBS_TABLE model = ToModel(row);
			return model;
		}

		private static CTBBS_TABLE ToModel(DataRow row)
		{
			CTBBS_TABLE model = new CTBBS_TABLE();
			model.ID = (int)row["ID"];
			model.T_ID = (int)row["T_ID"];
			model.U_ID = (int)row["U_ID"];
			model.F_TIME = (DateTime)row["F_TIME"];
			model.Context = (object)row["Context"];
			model.n_c = (int)row["n_c"];
			model.is_delete = (int)row["is_delete"];
			model.n_y = (int)row["n_y"];
			return model;
		}

		public IEnumerable<CTBBS_TABLE> ListAll()
		{
			List<CTBBS_TABLE> list = new List<CTBBS_TABLE>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CTBBS_TABLE");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
