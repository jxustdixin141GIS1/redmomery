using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class FILE_TABLEDAL
	{
		public int AddNew(FILE_TABLE model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO FILE_TABLE(U_ID,M_ID,T_ID,url,Name,N_View,Keyvalues) VALUES (@U_ID,@M_ID,@T_ID,@url,@Name,@N_View,@Keyvalues);SELECT @@identity"
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@M_ID", model.M_ID)
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@url", model.url)
				,new SqlParameter("@Name", model.Name)
				,new SqlParameter("@N_View", model.N_View)
				,new SqlParameter("@Keyvalues", model.Keyvalues)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM FILE_TABLE WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(FILE_TABLE model)
		{
			string sql = "UPDATE FILE_TABLE SET U_ID=@U_ID,M_ID=@M_ID,T_ID=@T_ID,url=@url,Name=@Name,N_View=@N_View,Keyvalues=@Keyvalues WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@M_ID", model.M_ID)
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@url", model.url)
				, new SqlParameter("@Name", model.Name)
				, new SqlParameter("@N_View", model.N_View)
				, new SqlParameter("@Keyvalues", model.Keyvalues)
			);
			return rows > 0;
		}

		public FILE_TABLE Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM FILE_TABLE WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			FILE_TABLE model = ToModel(row);
			return model;
		}

		private static FILE_TABLE ToModel(DataRow row)
		{
			FILE_TABLE model = new FILE_TABLE();
			model.ID = (int)row["ID"];
			model.U_ID = (int)row["U_ID"];
			model.M_ID = (int)row["M_ID"];
			model.T_ID = (int)row["T_ID"];
			model.url = (string)row["url"];
			model.Name = (string)row["Name"];
			model.N_View = (int)row["N_View"];
			model.Keyvalues = (string)row["Keyvalues"];
			return model;
		}

		public IEnumerable<FILE_TABLE> ListAll()
		{
			List<FILE_TABLE> list = new List<FILE_TABLE>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM FILE_TABLE");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
