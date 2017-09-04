using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class trackDAL
	{
		public int AddNew(track model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO track(EID,Timetext,Local,context,x,y,heroID,name,img,location) VALUES (@EID,@Timetext,@Local,@context,@x,@y,@heroID,@name,@img,@location);SELECT @@identity"
				,new SqlParameter("@EID", model.EID)
				,new SqlParameter("@Timetext", model.Timetext)
				,new SqlParameter("@Local", model.Local)
				,new SqlParameter("@context", model.context)
				,new SqlParameter("@x", model.x)
				,new SqlParameter("@y", model.y)
				,new SqlParameter("@heroID", model.heroID)
				,new SqlParameter("@name", model.name)
				,new SqlParameter("@img", model.img)
				,new SqlParameter("@location", model.location)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM track WHERE EID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(track model)
		{
			string sql = "UPDATE track SET EID=@EID,Timetext=@Timetext,Local=@Local,context=@context,x=@x,y=@y,heroID=@heroID,name=@name,img=@img,location=@location WHERE EID=@EID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@EID", model.EID)
				, new SqlParameter("@Timetext", model.Timetext)
				, new SqlParameter("@Local", model.Local)
				, new SqlParameter("@context", model.context)
				, new SqlParameter("@x", model.x)
				, new SqlParameter("@y", model.y)
				, new SqlParameter("@heroID", model.heroID)
				, new SqlParameter("@name", model.name)
				, new SqlParameter("@img", model.img)
				, new SqlParameter("@location", model.location)
			);
			return rows > 0;
		}

		public track Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM track WHERE EID=@EID", new SqlParameter("@EID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			track model = ToModel(row);
			return model;
		}

		private static track ToModel(DataRow row)
		{
			track model = new track();
			model.EID = (int)row["EID"];
			model.Timetext = (string)row["Timetext"];
			model.Local = (string)row["Local"];
			model.context = (string)row["context"];
			model.x = (object)row["x"];
			model.y = (object)row["y"];
			model.heroID = (string)row["heroID"];
			model.name = (string)row["name"];
			model.img = (int)row["img"];
			model.location = (object)row["location"];
			return model;
		}

		public IEnumerable<track> ListAll()
		{
			List<track> list = new List<track>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM track");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
