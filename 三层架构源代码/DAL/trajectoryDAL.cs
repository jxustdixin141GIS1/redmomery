using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class trajectoryDAL
	{
		public int AddNew(trajectory model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO trajectory(T_time,Timetext,Local,context,x,y,TID,MID) VALUES (@T_time,@Timetext,@Local,@context,@x,@y,@TID,@MID);SELECT @@identity"
				,new SqlParameter("@T_time", model.T_time)
				,new SqlParameter("@Timetext", model.Timetext)
				,new SqlParameter("@Local", model.Local)
				,new SqlParameter("@context", model.context)
				,new SqlParameter("@x", model.x)
				,new SqlParameter("@y", model.y)
				,new SqlParameter("@TID", model.TID)
				,new SqlParameter("@MID", model.MID)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM trajectory WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(trajectory model)
		{
			string sql = "UPDATE trajectory SET T_time=@T_time,Timetext=@Timetext,Local=@Local,context=@context,x=@x,y=@y,TID=@TID,MID=@MID WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@T_time", model.T_time)
				, new SqlParameter("@Timetext", model.Timetext)
				, new SqlParameter("@Local", model.Local)
				, new SqlParameter("@context", model.context)
				, new SqlParameter("@x", model.x)
				, new SqlParameter("@y", model.y)
				, new SqlParameter("@TID", model.TID)
				, new SqlParameter("@MID", model.MID)
			);
			return rows > 0;
		}

		public trajectory Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM trajectory WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			trajectory model = ToModel(row);
			return model;
		}

		private static trajectory ToModel(DataRow row)
		{
			trajectory model = new trajectory();
			model.ID = (object)row["ID"];
			model.T_time = (DateTime)row["T_time"];
			model.Timetext = (string)row["Timetext"];
			model.Local = (string)row["Local"];
			model.context = (string)row["context"];
			model.x = (string)row["x"];
			model.y = (string)row["y"];
			model.TID = (string)row["TID"];
			model.MID = (string)row["MID"];
			return model;
		}

		public IEnumerable<trajectory> ListAll()
		{
			List<trajectory> list = new List<trajectory>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM trajectory");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
