using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_m_LDAL
	{
		public int AddNew(View_m_L model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO View_m_L(USER_NETNAME,B_TITLE,B_describe,U_ID,N_TITLE_T,M_Key,C_TIME) VALUES (@USER_NETNAME,@B_TITLE,@B_describe,@U_ID,@N_TITLE_T,@M_Key,@C_TIME);SELECT @@identity"
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				,new SqlParameter("@B_TITLE", model.B_TITLE)
				,new SqlParameter("@B_describe", model.B_describe)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@N_TITLE_T", model.N_TITLE_T)
				,new SqlParameter("@M_Key", model.M_Key)
				,new SqlParameter("@C_TIME", model.C_TIME)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_m_L WHERE USER_NETNAME = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_m_L model)
		{
			string sql = "UPDATE View_m_L SET USER_NETNAME=@USER_NETNAME,B_TITLE=@B_TITLE,B_describe=@B_describe,U_ID=@U_ID,N_TITLE_T=@N_TITLE_T,M_Key=@M_Key,C_TIME=@C_TIME WHERE USER_NETNAME=@USER_NETNAME";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@B_TITLE", model.B_TITLE)
				, new SqlParameter("@B_describe", model.B_describe)
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@N_TITLE_T", model.N_TITLE_T)
				, new SqlParameter("@M_Key", model.M_Key)
				, new SqlParameter("@C_TIME", model.C_TIME)
			);
			return rows > 0;
		}

		public View_m_L Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_m_L WHERE USER_NETNAME=@USER_NETNAME", new SqlParameter("@USER_NETNAME", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_m_L model = ToModel(row);
			return model;
		}

		private static View_m_L ToModel(DataRow row)
		{
			View_m_L model = new View_m_L();
			model.USER_NETNAME = (string)row["USER_NETNAME"];
			model.B_TITLE = (string)row["B_TITLE"];
			model.B_describe = (object)row["B_describe"];
			model.ID = (int)row["ID"];
			model.U_ID = (int)row["U_ID"];
			model.N_TITLE_T = (int)row["N_TITLE_T"];
			model.M_Key = (string)row["M_Key"];
			model.C_TIME = (DateTime)row["C_TIME"];
			return model;
		}

		public IEnumerable<View_m_L> ListAll()
		{
			List<View_m_L> list = new List<View_m_L>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_m_L");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
