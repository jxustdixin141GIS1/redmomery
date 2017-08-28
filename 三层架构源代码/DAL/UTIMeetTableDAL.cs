using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class UTIMeetTableDAL
	{
		public int AddNew(UTIMeetTable model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO UTIMeetTable(UID,MeetID,state) VALUES (@UID,@MeetID,@state);SELECT @@identity"
				,new SqlParameter("@UID", model.UID)
				,new SqlParameter("@MeetID", model.MeetID)
				,new SqlParameter("@state", model.state)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM UTIMeetTable WHERE UID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(UTIMeetTable model)
		{
			string sql = "UPDATE UTIMeetTable SET UID=@UID,MeetID=@MeetID,state=@state WHERE UID=@UID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@UID", model.UID)
				, new SqlParameter("@MeetID", model.MeetID)
				, new SqlParameter("@state", model.state)
			);
			return rows > 0;
		}

		public UTIMeetTable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM UTIMeetTable WHERE UID=@UID", new SqlParameter("@UID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			UTIMeetTable model = ToModel(row);
			return model;
		}

		private static UTIMeetTable ToModel(DataRow row)
		{
			UTIMeetTable model = new UTIMeetTable();
			model.UID = (int)row["UID"];
			model.MeetID = (int)row["MeetID"];
			model.state = (int)row["state"];
			return model;
		}

		public IEnumerable<UTIMeetTable> ListAll()
		{
			List<UTIMeetTable> list = new List<UTIMeetTable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM UTIMeetTable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
