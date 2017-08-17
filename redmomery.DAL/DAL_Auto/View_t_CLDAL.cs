using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_t_CLDAL
	{
		public int AddNew(View_t_CL model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO View_t_CL(Expr3,url,Name,Expr4,Expr5,USER_NETNAME,T_ID,U_ID,F_TIME,Context,n_c,n_y,Expr2,Expr6,M_ID,Keyvalues,N_RESPONSE,Expr7,N_YES,TITLE,T_key) VALUES (@Expr3,@url,@Name,@Expr4,@Expr5,@USER_NETNAME,@T_ID,@U_ID,@F_TIME,@Context,@n_c,@n_y,@Expr2,@Expr6,@M_ID,@Keyvalues,@N_RESPONSE,@Expr7,@N_YES,@TITLE,@T_key);SELECT @@identity"
				,new SqlParameter("@Expr3", model.Expr3)
				,new SqlParameter("@url", model.url)
				,new SqlParameter("@Name", model.Name)
				,new SqlParameter("@Expr4", model.Expr4)
				,new SqlParameter("@Expr5", model.Expr5)
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@F_TIME", model.F_TIME)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@n_c", model.n_c)
				,new SqlParameter("@n_y", model.n_y)
				,new SqlParameter("@Expr2", model.Expr2)
				,new SqlParameter("@Expr6", model.Expr6)
				,new SqlParameter("@M_ID", model.M_ID)
				,new SqlParameter("@Keyvalues", model.Keyvalues)
				,new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				,new SqlParameter("@Expr7", model.Expr7)
				,new SqlParameter("@N_YES", model.N_YES)
				,new SqlParameter("@TITLE", model.TITLE)
				,new SqlParameter("@T_key", model.T_key)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_t_CL WHERE Expr3 = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_t_CL model)
		{
			string sql = "UPDATE View_t_CL SET Expr3=@Expr3,url=@url,Name=@Name,Expr4=@Expr4,Expr5=@Expr5,USER_NETNAME=@USER_NETNAME,T_ID=@T_ID,U_ID=@U_ID,F_TIME=@F_TIME,Context=@Context,n_c=@n_c,n_y=@n_y,Expr2=@Expr2,Expr6=@Expr6,M_ID=@M_ID,Keyvalues=@Keyvalues,N_RESPONSE=@N_RESPONSE,Expr7=@Expr7,N_YES=@N_YES,TITLE=@TITLE,T_key=@T_key WHERE Expr3=@Expr3";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@Expr3", model.Expr3)
				, new SqlParameter("@url", model.url)
				, new SqlParameter("@Name", model.Name)
				, new SqlParameter("@Expr4", model.Expr4)
				, new SqlParameter("@Expr5", model.Expr5)
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@F_TIME", model.F_TIME)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@n_c", model.n_c)
				, new SqlParameter("@n_y", model.n_y)
				, new SqlParameter("@Expr2", model.Expr2)
				, new SqlParameter("@Expr6", model.Expr6)
				, new SqlParameter("@M_ID", model.M_ID)
				, new SqlParameter("@Keyvalues", model.Keyvalues)
				, new SqlParameter("@N_RESPONSE", model.N_RESPONSE)
				, new SqlParameter("@Expr7", model.Expr7)
				, new SqlParameter("@N_YES", model.N_YES)
				, new SqlParameter("@TITLE", model.TITLE)
				, new SqlParameter("@T_key", model.T_key)
			);
			return rows > 0;
		}

		public View_t_CL Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_t_CL WHERE Expr3=@Expr3", new SqlParameter("@Expr3", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_t_CL model = ToModel(row);
			return model;
		}

		private static View_t_CL ToModel(DataRow row)
		{
			View_t_CL model = new View_t_CL();
			model.Expr3 = (int)row["Expr3"];
			model.url = (string)row["url"];
			model.Name = (string)row["Name"];
			model.Expr4 = (int)row["Expr4"];
			model.Expr5 = (int)row["Expr5"];
			model.USER_NETNAME = (string)row["USER_NETNAME"];
			model.ID = (int)row["ID"];
			model.T_ID = (int)row["T_ID"];
			model.U_ID = (int)row["U_ID"];
			model.F_TIME = (DateTime)row["F_TIME"];
			model.Context = (object)row["Context"];
			model.n_c = (int)row["n_c"];
			model.n_y = (int)row["n_y"];
			model.Expr2 = (int)row["Expr2"];
			model.Expr6 = (int)row["Expr6"];
			model.M_ID = (int)row["M_ID"];
			model.Keyvalues = (string)row["Keyvalues"];
			model.N_RESPONSE = (int)row["N_RESPONSE"];
			model.Expr7 = (object)row["Expr7"];
			model.N_YES = (int)row["N_YES"];
			model.TITLE = (string)row["TITLE"];
			model.T_key = (string)row["T_key"];
			return model;
		}

		public IEnumerable<View_t_CL> ListAll()
		{
			List<View_t_CL> list = new List<View_t_CL>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_t_CL");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
