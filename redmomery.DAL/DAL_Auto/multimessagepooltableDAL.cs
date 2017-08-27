using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class multimessagepooltableDAL
	{
		public int AddNew(multimessagepooltable model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO multimessagepooltable(FUID,TGID,Ftime,context,Rnum,Snum,MD5) VALUES (@FUID,@TGID,@Ftime,@context,@Rnum,@Snum,@MD5);SELECT @@identity"
				,new SqlParameter("@FUID", model.FUID)
				,new SqlParameter("@TGID", model.TGID)
				,new SqlParameter("@Ftime", model.Ftime)
				,new SqlParameter("@context", model.context)
				,new SqlParameter("@Rnum", model.Rnum)
				,new SqlParameter("@Snum", model.Snum)
				,new SqlParameter("@MD5", model.MD5)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM multimessagepooltable WHERE FUID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(multimessagepooltable model)
		{
			string sql = "UPDATE multimessagepooltable SET FUID=@FUID,TGID=@TGID,Ftime=@Ftime,context=@context,Rnum=@Rnum,Snum=@Snum,MD5=@MD5 WHERE FUID=@FUID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@FUID", model.FUID)
				, new SqlParameter("@TGID", model.TGID)
				, new SqlParameter("@Ftime", model.Ftime)
				, new SqlParameter("@context", model.context)
				, new SqlParameter("@Rnum", model.Rnum)
				, new SqlParameter("@Snum", model.Snum)
				, new SqlParameter("@MD5", model.MD5)
			);
			return rows > 0;
		}

		public multimessagepooltable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM multimessagepooltable WHERE FUID=@FUID", new SqlParameter("@FUID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			multimessagepooltable model = ToModel(row);
			return model;
		}

		private static multimessagepooltable ToModel(DataRow row)
		{
			multimessagepooltable model = new multimessagepooltable();
			model.FUID = (int)row["FUID"];
			model.TGID = (int)row["TGID"];
			model.Ftime = (DateTime)row["Ftime"];
			model.context = (string)row["context"];
			model.Rnum = (int)row["Rnum"];
			model.Snum = (int)row["Snum"];
			model.MD5 = (string)row["MD5"];
			return model;
		}

		public IEnumerable<multimessagepooltable> ListAll()
		{
			List<multimessagepooltable> list = new List<multimessagepooltable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM multimessagepooltable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
