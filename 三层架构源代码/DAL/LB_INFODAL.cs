using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class LB_INFODAL
	{
		public int AddNew(LB_INFO model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO LB_INFO(T_ID,LBname,LBjob,LBsex,LBbirthday,LBdomicile,designation,LBexperience,LBlife,LBPhoto,LBdelete,X,Y,Location) VALUES (@T_ID,@LBname,@LBjob,@LBsex,@LBbirthday,@LBdomicile,@designation,@LBexperience,@LBlife,@LBPhoto,@LBdelete,@X,@Y,@Location);SELECT @@identity"
				,new SqlParameter("@T_ID", model.T_ID)
				,new SqlParameter("@LBname", model.LBname)
				,new SqlParameter("@LBjob", model.LBjob)
				,new SqlParameter("@LBsex", model.LBsex)
				,new SqlParameter("@LBbirthday", model.LBbirthday)
				,new SqlParameter("@LBdomicile", model.LBdomicile)
				,new SqlParameter("@designation", model.designation)
				,new SqlParameter("@LBexperience", model.LBexperience)
				,new SqlParameter("@LBlife", model.LBlife)
				,new SqlParameter("@LBPhoto", model.LBPhoto)
				,new SqlParameter("@LBdelete", model.LBdelete)
				,new SqlParameter("@X", model.X)
				,new SqlParameter("@Y", model.Y)
				,new SqlParameter("@Location", model.Location)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM LB_INFO WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(LB_INFO model)
		{
			string sql = "UPDATE LB_INFO SET T_ID=@T_ID,LBname=@LBname,LBjob=@LBjob,LBsex=@LBsex,LBbirthday=@LBbirthday,LBdomicile=@LBdomicile,designation=@designation,LBexperience=@LBexperience,LBlife=@LBlife,LBPhoto=@LBPhoto,LBdelete=@LBdelete,X=@X,Y=@Y,Location=@Location WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@T_ID", model.T_ID)
				, new SqlParameter("@LBname", model.LBname)
				, new SqlParameter("@LBjob", model.LBjob)
				, new SqlParameter("@LBsex", model.LBsex)
				, new SqlParameter("@LBbirthday", model.LBbirthday)
				, new SqlParameter("@LBdomicile", model.LBdomicile)
				, new SqlParameter("@designation", model.designation)
				, new SqlParameter("@LBexperience", model.LBexperience)
				, new SqlParameter("@LBlife", model.LBlife)
				, new SqlParameter("@LBPhoto", model.LBPhoto)
				, new SqlParameter("@LBdelete", model.LBdelete)
				, new SqlParameter("@X", model.X)
				, new SqlParameter("@Y", model.Y)
				, new SqlParameter("@Location", model.Location)
			);
			return rows > 0;
		}

		public LB_INFO Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM LB_INFO WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			LB_INFO model = ToModel(row);
			return model;
		}

		private static LB_INFO ToModel(DataRow row)
		{
			LB_INFO model = new LB_INFO();
			model.ID = (int)row["ID"];
			model.T_ID = (int)row["T_ID"];
			model.LBname = (string)row["LBname"];
			model.LBjob = (string)row["LBjob"];
			model.LBsex = (string)row["LBsex"];
			model.LBbirthday = (string)row["LBbirthday"];
			model.LBdomicile = (string)row["LBdomicile"];
			model.designation = (string)row["designation"];
			model.LBexperience = (string)row["LBexperience"];
			model.LBlife = (object)row["LBlife"];
			model.LBPhoto = (string)row["LBPhoto"];
			model.LBdelete = (int)row["LBdelete"];
			model.X = (object)row["X"];
			model.Y = (object)row["Y"];
			model.Location = (object)row["Location"];
			return model;
		}

		public IEnumerable<LB_INFO> ListAll()
		{
			List<LB_INFO> list = new List<LB_INFO>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM LB_INFO");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
