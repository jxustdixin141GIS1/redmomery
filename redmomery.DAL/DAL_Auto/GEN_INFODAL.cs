using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class GEN_INFODAL
	{
		public int AddNew(GEN_INFO model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO GEN_INFO(GEN_ID,GEN_NAME,GEN_BIRTH,GEN_EVTIME,GEN_ADDRESS,GEN_LOCX,GEN_LOCY,GEN_INTRO) VALUES (@GEN_ID,@GEN_NAME,@GEN_BIRTH,@GEN_EVTIME,@GEN_ADDRESS,@GEN_LOCX,@GEN_LOCY,@GEN_INTRO);SELECT @@identity"
				,new SqlParameter("@GEN_ID", model.GEN_ID)
				,new SqlParameter("@GEN_NAME", model.GEN_NAME)
				,new SqlParameter("@GEN_BIRTH", model.GEN_BIRTH)
				,new SqlParameter("@GEN_EVTIME", model.GEN_EVTIME)
				,new SqlParameter("@GEN_ADDRESS", model.GEN_ADDRESS)
				,new SqlParameter("@GEN_LOCX", model.GEN_LOCX)
				,new SqlParameter("@GEN_LOCY", model.GEN_LOCY)
				,new SqlParameter("@GEN_INTRO", model.GEN_INTRO)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM GEN_INFO WHERE GEN_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(GEN_INFO model)
		{
			string sql = "UPDATE GEN_INFO SET GEN_ID=@GEN_ID,GEN_NAME=@GEN_NAME,GEN_BIRTH=@GEN_BIRTH,GEN_EVTIME=@GEN_EVTIME,GEN_ADDRESS=@GEN_ADDRESS,GEN_LOCX=@GEN_LOCX,GEN_LOCY=@GEN_LOCY,GEN_INTRO=@GEN_INTRO WHERE GEN_ID=@GEN_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@GEN_ID", model.GEN_ID)
				, new SqlParameter("@GEN_NAME", model.GEN_NAME)
				, new SqlParameter("@GEN_BIRTH", model.GEN_BIRTH)
				, new SqlParameter("@GEN_EVTIME", model.GEN_EVTIME)
				, new SqlParameter("@GEN_ADDRESS", model.GEN_ADDRESS)
				, new SqlParameter("@GEN_LOCX", model.GEN_LOCX)
				, new SqlParameter("@GEN_LOCY", model.GEN_LOCY)
				, new SqlParameter("@GEN_INTRO", model.GEN_INTRO)
			);
			return rows > 0;
		}

		public GEN_INFO Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GEN_INFO WHERE GEN_ID=@GEN_ID", new SqlParameter("@GEN_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			GEN_INFO model = ToModel(row);
			return model;
		}

		private static GEN_INFO ToModel(DataRow row)
		{
			GEN_INFO model = new GEN_INFO();
			model.GEN_ID = (int)row["GEN_ID"];
			model.GEN_NAME = (string)row["GEN_NAME"];
			model.GEN_BIRTH = (DateTime)row["GEN_BIRTH"];
			model.GEN_EVTIME = (DateTime)row["GEN_EVTIME"];
			model.GEN_ADDRESS = (string)row["GEN_ADDRESS"];
			model.GEN_LOCX = (object)row["GEN_LOCX"];
			model.GEN_LOCY = (object)row["GEN_LOCY"];
			model.GEN_INTRO = (object)row["GEN_INTRO"];
			return model;
		}

		public IEnumerable<GEN_INFO> ListAll()
		{
			List<GEN_INFO> list = new List<GEN_INFO>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM GEN_INFO");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
