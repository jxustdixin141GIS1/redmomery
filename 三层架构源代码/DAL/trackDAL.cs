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
				"INSERT INTO track(EID,date,address,experience,X,Y,heroID) VALUES (@EID,@date,@address,@experience,@X,@Y,@heroID);SELECT @@identity"
				,new SqlParameter("@EID", model.EID)
				,new SqlParameter("@date", model.date)
				,new SqlParameter("@address", model.address)
				,new SqlParameter("@experience", model.experience)
				,new SqlParameter("@X", model.X)
				,new SqlParameter("@Y", model.Y)
				,new SqlParameter("@heroID", model.heroID)
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
			string sql = "UPDATE track SET EID=@EID,date=@date,address=@address,experience=@experience,X=@X,Y=@Y,heroID=@heroID WHERE EID=@EID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@EID", model.EID)
				, new SqlParameter("@date", model.date)
				, new SqlParameter("@address", model.address)
				, new SqlParameter("@experience", model.experience)
				, new SqlParameter("@X", model.X)
				, new SqlParameter("@Y", model.Y)
				, new SqlParameter("@heroID", model.heroID)
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
			model.date = (object)row["date"];
			model.address = (string)row["address"];
			model.experience = (string)row["experience"];
			model.X = (object)row["X"];
			model.Y = (object)row["Y"];
			model.heroID = (int)row["heroID"];
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
