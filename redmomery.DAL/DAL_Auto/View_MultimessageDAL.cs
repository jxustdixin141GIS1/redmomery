using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class View_MultimessageDAL
	{
        //public int AddNew(View_Multimessage model)
        //{
        //    object obj = SqlHelper.ExecuteScalar(
        //        "INSERT INTO View_Multimessage(FUID,TGID,Ftime,context,Rnum,Snum,MD5,USER_NETNAME,USER_IMG) VALUES (@FUID,@TGID,@Ftime,@context,@Rnum,@Snum,@MD5,@USER_NETNAME,@USER_IMG);SELECT @@identity"
        //        ,new SqlParameter("@FUID", model.FUID)
        //        ,new SqlParameter("@TGID", model.TGID)
        //        ,new SqlParameter("@Ftime", model.Ftime)
        //        ,new SqlParameter("@context", model.context)
        //        ,new SqlParameter("@Rnum", model.Rnum)
        //        ,new SqlParameter("@Snum", model.Snum)
        //        ,new SqlParameter("@MD5", model.MD5)
        //        ,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
        //        ,new SqlParameter("@USER_IMG", model.USER_IMG)
        //    );
        //    return Convert.ToInt32(obj);
        //}
        //视图不需要数据的添加
		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM View_Multimessage WHERE FUID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(View_Multimessage model)
		{
			string sql = "UPDATE View_Multimessage SET FUID=@FUID,TGID=@TGID,Ftime=@Ftime,context=@context,Rnum=@Rnum,Snum=@Snum,MD5=@MD5,USER_NETNAME=@USER_NETNAME,USER_IMG=@USER_IMG WHERE FUID=@FUID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@FUID", model.FUID)
				, new SqlParameter("@TGID", model.TGID)
				, new SqlParameter("@Ftime", model.Ftime)
				, new SqlParameter("@context", model.context)
				, new SqlParameter("@Rnum", model.Rnum)
				, new SqlParameter("@Snum", model.Snum)
				, new SqlParameter("@MD5", model.MD5)
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@USER_IMG", model.USER_IMG)
			);
			return rows > 0;
		}

		public View_Multimessage Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_Multimessage WHERE FUID=@FUID", new SqlParameter("@FUID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			View_Multimessage model = ToModel(row);
			return model;
		}

		private static View_Multimessage ToModel(DataRow row)
		{
			View_Multimessage model = new View_Multimessage();
			model.FUID = (int)row["FUID"];
			model.TGID = (int)row["TGID"];
			model.Ftime = (DateTime)row["Ftime"];
			model.context = (string)row["context"];
			model.Rnum = (int)row["Rnum"];
			model.Snum = (int)row["Snum"];
			model.MD5 = (string)row["MD5"];
			model.USER_NETNAME = (string)row["USER_NETNAME"];
			model.USER_IMG = (string)row["USER_IMG"];
			return model;
		}

		public IEnumerable<View_Multimessage> ListAll()
		{
			List<View_Multimessage> list = new List<View_Multimessage>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_Multimessage");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
