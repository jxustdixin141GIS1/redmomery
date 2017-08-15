using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class WAR_INFODAL
	{
		public int AddNew(WAR_INFO model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO WAR_INFO(WAR_ID,WAR_NAME,WAR_TIME,WAR_ADDRESS,WAR_LOCX,WAR_LOCY,WAR_INTRO) VALUES (@WAR_ID,@WAR_NAME,@WAR_TIME,@WAR_ADDRESS,@WAR_LOCX,@WAR_LOCY,@WAR_INTRO);SELECT @@identity"
				,new SqlParameter("@WAR_ID", model.WAR_ID)
				,new SqlParameter("@WAR_NAME", model.WAR_NAME)
				,new SqlParameter("@WAR_TIME", model.WAR_TIME)
				,new SqlParameter("@WAR_ADDRESS", model.WAR_ADDRESS)
				,new SqlParameter("@WAR_LOCX", model.WAR_LOCX)
				,new SqlParameter("@WAR_LOCY", model.WAR_LOCY)
				,new SqlParameter("@WAR_INTRO", model.WAR_INTRO)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM WAR_INFO WHERE WAR_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(WAR_INFO model)
		{
			string sql = "UPDATE WAR_INFO SET WAR_ID=@WAR_ID,WAR_NAME=@WAR_NAME,WAR_TIME=@WAR_TIME,WAR_ADDRESS=@WAR_ADDRESS,WAR_LOCX=@WAR_LOCX,WAR_LOCY=@WAR_LOCY,WAR_INTRO=@WAR_INTRO WHERE WAR_ID=@WAR_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@WAR_ID", model.WAR_ID)
				, new SqlParameter("@WAR_NAME", model.WAR_NAME)
				, new SqlParameter("@WAR_TIME", model.WAR_TIME)
				, new SqlParameter("@WAR_ADDRESS", model.WAR_ADDRESS)
				, new SqlParameter("@WAR_LOCX", model.WAR_LOCX)
				, new SqlParameter("@WAR_LOCY", model.WAR_LOCY)
				, new SqlParameter("@WAR_INTRO", model.WAR_INTRO)
			);
			return rows > 0;
		}

		public WAR_INFO Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM WAR_INFO WHERE WAR_ID=@WAR_ID", new SqlParameter("@WAR_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			WAR_INFO model = ToModel(row);
			return model;
		}

		private static WAR_INFO ToModel(DataRow row)
		{
			WAR_INFO model = new WAR_INFO();
			model.WAR_ID = (int)row["WAR_ID"];
			model.WAR_NAME = (string)row["WAR_NAME"];
			model.WAR_TIME = (DateTime)row["WAR_TIME"];
			model.WAR_ADDRESS = (string)row["WAR_ADDRESS"];
			model.WAR_LOCX = (object)row["WAR_LOCX"];
			model.WAR_LOCY = (object)row["WAR_LOCY"];
			model.WAR_INTRO = (object)row["WAR_INTRO"];
			return model;
		}

		public IEnumerable<WAR_INFO> ListAll()
		{
			List<WAR_INFO> list = new List<WAR_INFO>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM WAR_INFO");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
