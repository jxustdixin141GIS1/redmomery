using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_T_UDAL
	{
		public int AddNew(View_T_U model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO View_T_U(T_ID,M_ID,F_TIME,U_ID,N_RESPONSE,TITLE,Context,N_YES,pass_TIME,Authonrity,T_key,MD5,is_pass,USER_NETNAME,USER_IMG) VALUES (@T_ID,@M_ID,@F_TIME,@U_ID,@N_RESPONSE,@TITLE,@Context,@N_YES,@pass_TIME,@Authonrity,@T_key,@MD5,@is_pass,@USER_NETNAME,@USER_IMG);SELECT @@identity"
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@M_ID", model.M_ID)
				,new SqlParameter("@F_TIME", model.F_TIME)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				,new SqlParameter("@TITLE", model.TITLE)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@N_YES", model.N_YES)
				,new SqlParameter("@pass_TIME", model.pass_TIME)
				,new SqlParameter("@Authonrity", model.Authonrity)
				,new SqlParameter("@T_key", model.T_key)
				,new SqlParameter("@MD5", model.MD5)
				,new SqlParameter("@is_pass", model.is_pass)
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				,new SqlParameter("@USER_IMG", model.USER_IMG)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_T_U WHERE T_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_T_U model)
		{
			string sql = "UPDATE View_T_U SET T_ID=@T_ID,M_ID=@M_ID,F_TIME=@F_TIME,U_ID=@U_ID,N_RESPONSE=@N_RESPONSE,TITLE=@TITLE,Context=@Context,N_YES=@N_YES,pass_TIME=@pass_TIME,Authonrity=@Authonrity,T_key=@T_key,MD5=@MD5,is_pass=@is_pass,USER_NETNAME=@USER_NETNAME,USER_IMG=@USER_IMG WHERE T_ID=@T_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@M_ID", model.M_ID)
				, new SqlParameter("@F_TIME", model.F_TIME)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				, new SqlParameter("@TITLE", model.TITLE)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@N_YES", model.N_YES)
				, new SqlParameter("@pass_TIME", model.pass_TIME)
				, new SqlParameter("@Authonrity", model.Authonrity)
				, new SqlParameter("@T_key", model.T_key)
				, new SqlParameter("@MD5", model.MD5)
				, new SqlParameter("@is_pass", model.is_pass)
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@USER_IMG", model.USER_IMG)
			);
			return rows > 0;
		}

		public View_T_U Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_T_U WHERE T_ID=@T_ID", new SqlParameter("@T_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_T_U model = ToModel(row);
			return model;
		}

		private static View_T_U ToModel(DataRow row)
		{
			View_T_U model = new View_T_U();
			model.T_ID = (int)row["T_ID"];
			model.M_ID = (int)row["M_ID"];
			model.F_TIME = (DateTime)row["F_TIME"];
			model.U_ID = (int)row["U_ID"];
			model.N_RESPONSE = (int)row["N_RESPONSE"];
			model.TITLE = (string)row["TITLE"];
			model.Context = (object)row["Context"];
			model.N_YES = (int)row["N_YES"];
			model.pass_TIME = (DateTime)row["pass_TIME"];
			model.Authonrity = (int)row["Authonrity"];
			model.T_key = (string)row["T_key"];
            model.MD5 = row["MD5"].ToString() == "" ? "" : (string)row["MD5"];
			model.is_pass = (int)row["is_pass"];
			model.USER_NETNAME = (string)row["USER_NETNAME"];
            model.USER_IMG = (string)row["USER_IMG"].ToString() == "" ? "" : (string)row["USER_IMG"]; 
			return model;
		}

		public IEnumerable<View_T_U> ListAll()
		{
			List<View_T_U> list = new List<View_T_U>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_T_U");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
