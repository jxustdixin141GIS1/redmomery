using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class ARMY_INFODAL
	{
		public int AddNew(ARMY_INFO model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO ARMY_INFO(ARMY_ID,ARMY_NAME,ARMY_TIME,ARMY_ADDRESS,ARMY_LOCX,ARMY_LOCY,ARMY_INTRO) VALUES (@ARMY_ID,@ARMY_NAME,@ARMY_TIME,@ARMY_ADDRESS,@ARMY_LOCX,@ARMY_LOCY,@ARMY_INTRO);SELECT @@identity"
				,new SqlParameter("@ARMY_ID", model.ARMY_ID)
				,new SqlParameter("@ARMY_NAME", model.ARMY_NAME)
				,new SqlParameter("@ARMY_TIME", model.ARMY_TIME)
				,new SqlParameter("@ARMY_ADDRESS", model.ARMY_ADDRESS)
				,new SqlParameter("@ARMY_LOCX", model.ARMY_LOCX)
				,new SqlParameter("@ARMY_LOCY", model.ARMY_LOCY)
				,new SqlParameter("@ARMY_INTRO", model.ARMY_INTRO)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM ARMY_INFO WHERE ARMY_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(ARMY_INFO model)
		{
			string sql = "UPDATE ARMY_INFO SET ARMY_ID=@ARMY_ID,ARMY_NAME=@ARMY_NAME,ARMY_TIME=@ARMY_TIME,ARMY_ADDRESS=@ARMY_ADDRESS,ARMY_LOCX=@ARMY_LOCX,ARMY_LOCY=@ARMY_LOCY,ARMY_INTRO=@ARMY_INTRO WHERE ARMY_ID=@ARMY_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ARMY_ID", model.ARMY_ID)
				, new SqlParameter("@ARMY_NAME", model.ARMY_NAME)
				, new SqlParameter("@ARMY_TIME", model.ARMY_TIME)
				, new SqlParameter("@ARMY_ADDRESS", model.ARMY_ADDRESS)
				, new SqlParameter("@ARMY_LOCX", model.ARMY_LOCX)
				, new SqlParameter("@ARMY_LOCY", model.ARMY_LOCY)
				, new SqlParameter("@ARMY_INTRO", model.ARMY_INTRO)
			);
			return rows > 0;
		}

		public ARMY_INFO Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM ARMY_INFO WHERE ARMY_ID=@ARMY_ID", new SqlParameter("@ARMY_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			ARMY_INFO model = ToModel(row);
			return model;
		}

		private static ARMY_INFO ToModel(DataRow row)
		{
			ARMY_INFO model = new ARMY_INFO();
			model.ARMY_ID = (int)row["ARMY_ID"];
			model.ARMY_NAME = (string)row["ARMY_NAME"];
			model.ARMY_TIME = (DateTime)row["ARMY_TIME"];
			model.ARMY_ADDRESS = (string)row["ARMY_ADDRESS"];
			model.ARMY_LOCX = (object)row["ARMY_LOCX"];
			model.ARMY_LOCY = (object)row["ARMY_LOCY"];
			model.ARMY_INTRO = (object)row["ARMY_INTRO"];
			return model;
		}

		public IEnumerable<ARMY_INFO> ListAll()
		{
			List<ARMY_INFO> list = new List<ARMY_INFO>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM ARMY_INFO");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
