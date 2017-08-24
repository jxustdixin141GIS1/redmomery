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
				"INSERT INTO Echowall(text,FTime,T_ID,LBID,M_ID) VALUES (@text,@FTime,@T_ID,@LBID,@M_ID);SELECT @@identity"
				,new SqlParameter("@text", model.text)
				,new SqlParameter("@FTime", model.FTime)
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@LBID", model.LBID)
				,new SqlParameter("@M_ID", model.M_ID)
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
			string sql = "UPDATE Echowall SET text=@text,FTime=@FTime,T_ID=@T_ID,LBID=@LBID,M_ID=@M_ID WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@text", model.text)
				, new SqlParameter("@FTime", model.FTime)
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@LBID", model.LBID)
				, new SqlParameter("@M_ID", model.M_ID)
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
			model.T_ID = (int)(row["T_ID"].ToString()==""?0:int.Parse(row["T_ID"].ToString()));
			model.LBID = (int)(row["LBID"].ToString()==""?0:int.Parse(row["LBID"].ToString()));
			model.M_ID = (int)(row["M_ID"].ToString()==""?0:int.Parse(row["M_ID"].ToString()));
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
