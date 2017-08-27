using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class LBTRACKDAL
	{
		public int AddNew(LBTRACK model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO LBTRACK(T_time,Timetext,Local,context,x,y,LBID,isCurrent,name) VALUES (@T_time,@Timetext,@Local,@context,@x,@y,@LBID,@isCurrent,@name);SELECT @@identity"
				,new SqlParameter("@T_time", model.T_time)
				,new SqlParameter("@Timetext", model.Timetext)
				,new SqlParameter("@Local", model.Local)
				,new SqlParameter("@context", model.context)
				,new SqlParameter("@x", model.x)
				,new SqlParameter("@y", model.y)
				,new SqlParameter("@LBID", model.LBID)
				,new SqlParameter("@isCurrent", model.isCurrent)
				,new SqlParameter("@name", model.name)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM LBTRACK WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(LBTRACK model)
		{
			string sql = "UPDATE LBTRACK SET T_time=@T_time,Timetext=@Timetext,Local=@Local,context=@context,x=@x,y=@y,LBID=@LBID,isCurrent=@isCurrent,name=@name WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@T_time", model.T_time)
				, new SqlParameter("@Timetext", model.Timetext)
				, new SqlParameter("@Local", model.Local)
				, new SqlParameter("@context", model.context)
				, new SqlParameter("@x", model.x)
				, new SqlParameter("@y", model.y)
				, new SqlParameter("@LBID", model.LBID)
				, new SqlParameter("@isCurrent", model.isCurrent)
				, new SqlParameter("@name", model.name)
			);
			return rows > 0;
		}

		public LBTRACK Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM LBTRACK WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			LBTRACK model = ToModel(row);
			return model;
		}

		private static LBTRACK ToModel(DataRow row)
		{
			LBTRACK model = new LBTRACK();
			model.ID = (int)row["ID"];
			model.T_time = (DateTime)row["T_time"];
			model.Timetext = (string)row["Timetext"];
			model.Local = (string)row["Local"];
			model.context = (string)row["context"];
			model.x = (string)row["x"];
			model.y = (string)row["y"];
			model.LBID = (int)row["LBID"];
			model.isCurrent = (int)row["isCurrent"];
			model.name = (string)row["name"];
			return model;
		}

		public IEnumerable<LBTRACK> ListAll()
		{
			List<LBTRACK> list = new List<LBTRACK>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM LBTRACK");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
