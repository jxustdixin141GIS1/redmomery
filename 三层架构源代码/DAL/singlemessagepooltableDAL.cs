using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class singlemessagepooltableDAL
	{
		public int AddNew(singlemessagepooltable model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO singlemessagepooltable(FUID,TUID,Ftime,Context,isread,MD5) VALUES (@FUID,@TUID,@Ftime,@Context,@isread,@MD5);SELECT @@identity"
				,new SqlParameter("@FUID", model.FUID)
				,new SqlParameter("@TUID", model.TUID)
				,new SqlParameter("@Ftime", model.Ftime)
				,new SqlParameter("@Context", model.Context)
				,new SqlParameter("@isread", model.isread)
				,new SqlParameter("@MD5", model.MD5)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM singlemessagepooltable WHERE FUID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(singlemessagepooltable model)
		{
			string sql = "UPDATE singlemessagepooltable SET FUID=@FUID,TUID=@TUID,Ftime=@Ftime,Context=@Context,isread=@isread,MD5=@MD5 WHERE FUID=@FUID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@FUID", model.FUID)
				, new SqlParameter("@TUID", model.TUID)
				, new SqlParameter("@Ftime", model.Ftime)
				, new SqlParameter("@Context", model.Context)
				, new SqlParameter("@isread", model.isread)
				, new SqlParameter("@MD5", model.MD5)
			);
			return rows > 0;
		}

		public singlemessagepooltable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM singlemessagepooltable WHERE FUID=@FUID", new SqlParameter("@FUID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			singlemessagepooltable model = ToModel(row);
			return model;
		}

		private static singlemessagepooltable ToModel(DataRow row)
		{
			singlemessagepooltable model = new singlemessagepooltable();
			model.FUID = (int)row["FUID"];
			model.TUID = (int)row["TUID"];
			model.Ftime = (DateTime)row["Ftime"];
			model.Context = (string)row["Context"];
			model.isread = (int)row["isread"];
			model.MD5 = (string)row["MD5"];
			return model;
		}

		public IEnumerable<singlemessagepooltable> ListAll()
		{
			List<singlemessagepooltable> list = new List<singlemessagepooltable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM singlemessagepooltable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
