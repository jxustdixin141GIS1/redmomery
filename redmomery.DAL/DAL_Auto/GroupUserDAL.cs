using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class GroupUserDAL
	{
		public int AddNew(GroupUser model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO GroupUser(UID,GroupID,groupname) VALUES (@UID,@GroupID,@groupname);SELECT @@identity"
				,new SqlParameter("@UID", model.UID)
				,new SqlParameter("@GroupID", model.GroupID)
				,new SqlParameter("@groupname", model.groupname)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM GroupUser WHERE UID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(GroupUser model)
		{
			string sql = "UPDATE GroupUser SET UID=@UID,GroupID=@GroupID,groupname=@groupname WHERE UID=@UID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@UID", model.UID)
				, new SqlParameter("@GroupID", model.GroupID)
				, new SqlParameter("@groupname", model.groupname)
			);
			return rows > 0;
		}

		public GroupUser Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GroupUser WHERE UID=@UID", new SqlParameter("@UID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			GroupUser model = ToModel(row);
			return model;
		}

		private static GroupUser ToModel(DataRow row)
		{
			GroupUser model = new GroupUser();
			model.UID = (int)row["UID"];
			model.GroupID = (int)row["GroupID"];
			model.groupname = (string)row["groupname"];
			return model;
		}

		public IEnumerable<GroupUser> ListAll()
		{
			List<GroupUser> list = new List<GroupUser>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GroupUser");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
