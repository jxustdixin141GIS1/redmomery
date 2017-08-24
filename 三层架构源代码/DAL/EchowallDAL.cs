using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class EchowallDAL
	{
		public int AddNew(Echowall model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO Echowall(text,FTime) VALUES (@text,@FTime);SELECT @@identity"
				,new SqlParameter("@text", model.text)
				,new SqlParameter("@FTime", model.FTime)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM Echowall WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(Echowall model)
		{
			string sql = "UPDATE Echowall SET text=@text,FTime=@FTime WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@text", model.text)
				, new SqlParameter("@FTime", model.FTime)
			);
			return rows > 0;
		}

		public Echowall Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM Echowall WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			Echowall model = ToModel(row);
			return model;
		}

		private static Echowall ToModel(DataRow row)
		{
			Echowall model = new Echowall();
			model.ID = (object)row["ID"];
			model.text = (string)row["text"];
			model.FTime = (DateTime)row["FTime"];
			return model;
		}

		public IEnumerable<Echowall> ListAll()
		{
			List<Echowall> list = new List<Echowall>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM Echowall");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
