using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class BBSCONTENTSDAL
	{
		public int AddNew(BBSCONTENTS model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO BBSCONTENTS(BSS_ID,CONNAME,CONREXT,WRITEDATE,WRITEID,REPLYID,CRTIME,KINDID,KINDNAME) VALUES (@BSS_ID,@CONNAME,@CONREXT,@WRITEDATE,@WRITEID,@REPLYID,@CRTIME,@KINDID,@KINDNAME);SELECT @@identity"
				,new SqlParameter("@BSS_ID", model.BSS_ID)
				,new SqlParameter("@CONNAME", model.CONNAME)
				,new SqlParameter("@CONREXT", model.CONREXT)
				,new SqlParameter("@WRITEDATE", model.WRITEDATE)
				,new SqlParameter("@WRITEID", model.WRITEID)
				,new SqlParameter("@REPLYID", model.REPLYID)
				,new SqlParameter("@CRTIME", model.CRTIME)
				,new SqlParameter("@KINDID", model.KINDID)
				,new SqlParameter("@KINDNAME", model.KINDNAME)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM BBSCONTENTS WHERE BSS_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(BBSCONTENTS model)
		{
			string sql = "UPDATE BBSCONTENTS SET BSS_ID=@BSS_ID,CONNAME=@CONNAME,CONREXT=@CONREXT,WRITEDATE=@WRITEDATE,WRITEID=@WRITEID,REPLYID=@REPLYID,CRTIME=@CRTIME,KINDID=@KINDID,KINDNAME=@KINDNAME WHERE BSS_ID=@BSS_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@BSS_ID", model.BSS_ID)
				, new SqlParameter("@CONNAME", model.CONNAME)
				, new SqlParameter("@CONREXT", model.CONREXT)
				, new SqlParameter("@WRITEDATE", model.WRITEDATE)
				, new SqlParameter("@WRITEID", model.WRITEID)
				, new SqlParameter("@REPLYID", model.REPLYID)
				, new SqlParameter("@CRTIME", model.CRTIME)
				, new SqlParameter("@KINDID", model.KINDID)
				, new SqlParameter("@KINDNAME", model.KINDNAME)
			);
			return rows > 0;
		}

		public BBSCONTENTS Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM BBSCONTENTS WHERE BSS_ID=@BSS_ID", new SqlParameter("@BSS_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			BBSCONTENTS model = ToModel(row);
			return model;
		}

		private static BBSCONTENTS ToModel(DataRow row)
		{
			BBSCONTENTS model = new BBSCONTENTS();
			model.BSS_ID = (int)row["BSS_ID"];
			model.CONNAME = (object)row["CONNAME"];
			model.CONREXT = (object)row["CONREXT"];
			model.WRITEDATE = (DateTime)row["WRITEDATE"];
			model.WRITEID = (int)row["WRITEID"];
			model.REPLYID = (int)row["REPLYID"];
			model.CRTIME = (DateTime)row["CRTIME"];
			model.KINDID = (int)row["KINDID"];
			model.KINDNAME = (string)row["KINDNAME"];
			return model;
		}

		public IEnumerable<BBSCONTENTS> ListAll()
		{
			List<BBSCONTENTS> list = new List<BBSCONTENTS>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM BBSCONTENTS");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
