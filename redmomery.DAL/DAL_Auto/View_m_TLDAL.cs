using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_m_TLDAL
	{
		public int AddNew(View_m_TL model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO View_m_TL(USER_NETNAME,U_ID,M_ID,F_TIME,N_RESPONSE,TITLE,Context,N_YES,Authonrity,B_TITLE,B_describe,N_TITLE_T,T_key) VALUES (@USER_NETNAME,@U_ID,@M_ID,@F_TIME,@N_RESPONSE,@TITLE,@Context,@N_YES,@Authonrity,@B_TITLE,@B_describe,@N_TITLE_T,@T_key);SELECT @@identity"
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@M_ID", model.M_ID)
				,new SqlParameter("@F_TIME", model.F_TIME)
				,new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				,new SqlParameter("@TITLE", model.TITLE)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@N_YES", model.N_YES)
				,new SqlParameter("@Authonrity", model.Authonrity)
				,new SqlParameter("@B_TITLE", model.B_TITLE)
				,new SqlParameter("@B_describe", model.B_describe)
				,new SqlParameter("@N_TITLE_T", model.N_TITLE_T)
				,new SqlParameter("@T_key", model.T_key)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_m_TL WHERE USER_NETNAME = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_m_TL model)
		{
			string sql = "UPDATE View_m_TL SET USER_NETNAME=@USER_NETNAME,U_ID=@U_ID,M_ID=@M_ID,F_TIME=@F_TIME,N_RESPONSE=@N_RESPONSE,TITLE=@TITLE,Context=@Context,N_YES=@N_YES,Authonrity=@Authonrity,B_TITLE=@B_TITLE,B_describe=@B_describe,N_TITLE_T=@N_TITLE_T,T_key=@T_key WHERE USER_NETNAME=@USER_NETNAME";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@M_ID", model.M_ID)
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@F_TIME", model.F_TIME)
				, new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				, new SqlParameter("@TITLE", model.TITLE)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@N_YES", model.N_YES)
				, new SqlParameter("@Authonrity", model.Authonrity)
				, new SqlParameter("@B_TITLE", model.B_TITLE)
				, new SqlParameter("@B_describe", model.B_describe)
				, new SqlParameter("@N_TITLE_T", model.N_TITLE_T)
				, new SqlParameter("@T_key", model.T_key)
			);
			return rows > 0;
		}

		public View_m_TL Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_m_TL WHERE USER_NETNAME=@USER_NETNAME", new SqlParameter("@USER_NETNAME", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_m_TL model = ToModel(row);
			return model;
		}

		private static View_m_TL ToModel(DataRow row)
		{
			View_m_TL model = new View_m_TL();
			model.USER_NETNAME = (string)row["USER_NETNAME"];
			model.U_ID = (int)row["U_ID"];
			model.M_ID = (int)row["M_ID"];
			model.ID = (int)row["ID"];
			model.F_TIME = (DateTime)row["F_TIME"];
			model.N_RESPONSE = (int)row["N_RESPONSE"];
			model.TITLE = (string)row["TITLE"];
			model.Context = (object)row["Context"];
			model.N_YES = (int)row["N_YES"];
			model.Authonrity = (int)row["Authonrity"];
			model.B_TITLE = (string)row["B_TITLE"];
			model.B_describe = (object)row["B_describe"];
			model.N_TITLE_T = (int)row["N_TITLE_T"];
			model.T_key = (string)row["T_key"];
			return model;
		}

		public IEnumerable<View_m_TL> ListAll()
		{
			List<View_m_TL> list = new List<View_m_TL>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_m_TL");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
