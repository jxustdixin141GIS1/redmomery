using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class SHAPE_TABLEDAL
	{
		public int AddNew(SHAPE_TABLE model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO SHAPE_TABLE(U_ID,TYPE,M_ID,T_ID,shape_tablename,Name,description,C_TIME,U_TIME) VALUES (@U_ID,@TYPE,@M_ID,@T_ID,@shape_tablename,@Name,@description,@C_TIME,@U_TIME);SELECT @@identity"
				,new SqlParameter("@U_ID", model.U_ID)
				,new SqlParameter("@TYPE", model.TYPE)
				,new SqlParameter("@M_ID", model.M_ID)
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@shape_tablename", model.shape_tablename)
				,new SqlParameter("@Name", model.Name)
				,new SqlParameter("@description", model.description)
				,new SqlParameter("@C_TIME", model.C_TIME)
				,new SqlParameter("@U_TIME", model.U_TIME)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM SHAPE_TABLE WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(SHAPE_TABLE model)
		{
			string sql = "UPDATE SHAPE_TABLE SET U_ID=@U_ID,TYPE=@TYPE,M_ID=@M_ID,T_ID=@T_ID,shape_tablename=@shape_tablename,Name=@Name,description=@description,C_TIME=@C_TIME,U_TIME=@U_TIME WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@U_ID", model.U_ID)
				, new SqlParameter("@TYPE", model.TYPE)
				, new SqlParameter("@M_ID", model.M_ID)
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@shape_tablename", model.shape_tablename)
				, new SqlParameter("@Name", model.Name)
				, new SqlParameter("@description", model.description)
				, new SqlParameter("@C_TIME", model.C_TIME)
				, new SqlParameter("@U_TIME", model.U_TIME)
			);
			return rows > 0;
		}

		public SHAPE_TABLE Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM SHAPE_TABLE WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			SHAPE_TABLE model = ToModel(row);
			return model;
		}

		private static SHAPE_TABLE ToModel(DataRow row)
		{
			SHAPE_TABLE model = new SHAPE_TABLE();
			model.ID = (int)row["ID"];
			model.U_ID = (int)row["U_ID"];
			model.TYPE = (int)row["TYPE"];
			model.M_ID = (int)row["M_ID"];
			model.T_ID = (int)row["T_ID"];
			model.shape_tablename = (string)row["shape_tablename"];
			model.Name = (string)row["Name"];
			model.description = (object)row["description"];
			model.C_TIME = (DateTime)row["C_TIME"];
			model.U_TIME = (DateTime)row["U_TIME"];
			return model;
		}

		public IEnumerable<SHAPE_TABLE> ListAll()
		{
			List<SHAPE_TABLE> list = new List<SHAPE_TABLE>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM SHAPE_TABLE");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
