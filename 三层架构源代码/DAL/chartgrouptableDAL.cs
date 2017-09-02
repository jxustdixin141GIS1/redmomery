using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class chartgrouptableDAL
	{
		public int AddNew(chartgrouptable model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO chartgrouptable(UID,Ctime,groupName,description,img,vnum) VALUES (@UID,@Ctime,@groupName,@description,@img,@vnum);SELECT @@identity"
				,new SqlParameter("@UID", model.UID)
				,new SqlParameter("@Ctime", model.Ctime)
				,new SqlParameter("@groupName", model.groupName)
				,new SqlParameter("@description", model.description)
				,new SqlParameter("@img", model.img)
				,new SqlParameter("@vnum", model.vnum)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM chartgrouptable WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(chartgrouptable model)
		{
			string sql = "UPDATE chartgrouptable SET UID=@UID,Ctime=@Ctime,groupName=@groupName,description=@description,img=@img,vnum=@vnum WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@UID", model.UID)
				, new SqlParameter("@Ctime", model.Ctime)
				, new SqlParameter("@groupName", model.groupName)
				, new SqlParameter("@description", model.description)
				, new SqlParameter("@img", model.img)
				, new SqlParameter("@vnum", model.vnum)
			);
			return rows > 0;
		}

		public chartgrouptable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM chartgrouptable WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			chartgrouptable model = ToModel(row);
			return model;
		}

		private static chartgrouptable ToModel(DataRow row)
		{
			chartgrouptable model = new chartgrouptable();
			model.ID = (int)row["ID"];
			model.UID = (int)row["UID"];
			model.Ctime = (DateTime)row["Ctime"];
			model.groupName = (string)row["groupName"];
			model.description = (string)row["description"];
			model.img = (string)row["img"];
			model.vnum = (int)row["vnum"];
			return model;
		}

		public IEnumerable<chartgrouptable> ListAll()
		{
			List<chartgrouptable> list = new List<chartgrouptable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM chartgrouptable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
