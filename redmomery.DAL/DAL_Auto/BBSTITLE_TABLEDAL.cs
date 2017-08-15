using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class BBSTITLE_TABLEDAL
	{
		public int AddNew(BBSTITLE_TABLE model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO BBSTITLE_TABLE(M_ID,U_ID,F_TIME,N_RESPONSE,TITLE,Context,N_YES,is_pass,pass_TIME,Authonrity,T_key,MD5) VALUES (@M_ID,@U_ID,@F_TIME,@N_RESPONSE,@TITLE,@Context,@N_YES,@is_pass,@pass_TIME,@Authonrity,@T_key,@MD5);SELECT @@identity"
				,new SqlParameter("@M_ID", model.M_ID)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@F_TIME", model.F_TIME)
				,new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				,new SqlParameter("@TITLE", model.TITLE)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@N_YES", model.N_YES)
				,new SqlParameter("@is_pass", model.is_pass)
				,new SqlParameter("@pass_TIME", model.pass_TIME)
				,new SqlParameter("@Authonrity", model.Authonrity)
				,new SqlParameter("@T_key", model.T_key)
				,new SqlParameter("@MD5", model.MD5)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM BBSTITLE_TABLE WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(BBSTITLE_TABLE model)
		{
			string sql = "UPDATE BBSTITLE_TABLE SET M_ID=@M_ID,U_ID=@U_ID,F_TIME=@F_TIME,N_RESPONSE=@N_RESPONSE,TITLE=@TITLE,Context=@Context,N_YES=@N_YES,is_pass=@is_pass,pass_TIME=@pass_TIME,Authonrity=@Authonrity,T_key=@T_key,MD5=@MD5 WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@M_ID", model.M_ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@F_TIME", model.F_TIME)
				, new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				, new SqlParameter("@TITLE", model.TITLE)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@N_YES", model.N_YES)
				, new SqlParameter("@is_pass", model.is_pass)
				, new SqlParameter("@pass_TIME", model.pass_TIME)
				, new SqlParameter("@Authonrity", model.Authonrity)
				, new SqlParameter("@T_key", model.T_key)
				, new SqlParameter("@MD5", model.MD5)
			);
			return rows > 0;
		}

		public BBSTITLE_TABLE Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM BBSTITLE_TABLE WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			BBSTITLE_TABLE model = ToModel(row);
			return model;
		}

		private static BBSTITLE_TABLE ToModel(DataRow row)
		{
			BBSTITLE_TABLE model = new BBSTITLE_TABLE();
			model.ID = (int)row["ID"];
			model.M_ID = (int)row["M_ID"];
			model.U_ID = (int)row["U_ID"];
			model.F_TIME = (DateTime)row["F_TIME"];
			model.N_RESPONSE = (int)row["N_RESPONSE"];
			model.TITLE = (string)row["TITLE"];
			model.Context = (object)row["Context"];
			model.N_YES = (int)row["N_YES"];
			model.is_pass = (int)row["is_pass"];
			model.pass_TIME = (DateTime)row["pass_TIME"];
			model.Authonrity = (int)row["Authonrity"];
			model.T_key = (string)row["T_key"];
			model.MD5 = (string)row["MD5"];
			return model;
		}

		public IEnumerable<BBSTITLE_TABLE> ListAll()
		{
			List<BBSTITLE_TABLE> list = new List<BBSTITLE_TABLE>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM BBSTITLE_TABLE");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
