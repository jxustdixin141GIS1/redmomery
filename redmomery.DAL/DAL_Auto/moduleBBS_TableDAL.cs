using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class moduleBBS_TableDAL
	{
		public int AddNew(moduleBBS_Table model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO moduleBBS_Table(U_ID,B_TITLE,B_describe,C_TIME,U_TIME,IS_pass,N_TITLE_T,M_Key,MD5) VALUES (@U_ID,@B_TITLE,@B_describe,@C_TIME,@U_TIME,@IS_pass,@N_TITLE_T,@M_Key,@MD5);SELECT @@identity"
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@B_TITLE", model.B_TITLE)
				,new SqlParameter("@B_describe", model.B_describe)
				,new SqlParameter("@C_TIME", model.C_TIME)
				,new SqlParameter("@U_TIME", model.U_TIME)
				,new SqlParameter("@IS_pass", model.IS_pass)
				,new SqlParameter("@N_TITLE_T", model.N_TITLE_T)
				,new SqlParameter("@M_Key", model.M_Key)
				,new SqlParameter("@MD5", model.MD5)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM moduleBBS_Table WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(moduleBBS_Table model)
		{
			string sql = "UPDATE moduleBBS_Table SET U_ID=@U_ID,B_TITLE=@B_TITLE,B_describe=@B_describe,C_TIME=@C_TIME,U_TIME=@U_TIME,IS_pass=@IS_pass,N_TITLE_T=@N_TITLE_T,M_Key=@M_Key,MD5=@MD5 WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@B_TITLE", model.B_TITLE)
				, new SqlParameter("@B_describe", model.B_describe)
				, new SqlParameter("@C_TIME", model.C_TIME)
				, new SqlParameter("@U_TIME", model.U_TIME)
				, new SqlParameter("@IS_pass", model.IS_pass)
				, new SqlParameter("@N_TITLE_T", model.N_TITLE_T)
				, new SqlParameter("@M_Key", model.M_Key)
				, new SqlParameter("@MD5", model.MD5)
			);
			return rows > 0;
		}

		public moduleBBS_Table Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM moduleBBS_Table WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			moduleBBS_Table model = ToModel(row);
			return model;
		}

		private static moduleBBS_Table ToModel(DataRow row)
		{
			moduleBBS_Table model = new moduleBBS_Table();
			model.ID = (int)row["ID"];
			model.U_ID = (int)row["U_ID"];
			model.B_TITLE = (string)row["B_TITLE"];
			model.B_describe = (object)row["B_describe"];
			model.C_TIME = (DateTime)row["C_TIME"];
			model.U_TIME = (DateTime)row["U_TIME"];
			model.IS_pass = (int)row["IS_pass"];
			model.N_TITLE_T = (int)row["N_TITLE_T"];
			model.M_Key = (string)row["M_Key"];
			model.MD5 = (string)row["MD5"];
			return model;
		}

		public IEnumerable<moduleBBS_Table> ListAll()
		{
			List<moduleBBS_Table> list = new List<moduleBBS_Table>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM moduleBBS_Table");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
