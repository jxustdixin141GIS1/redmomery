using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class ROOT_INFODAL
	{
		public int AddNew(ROOT_INFO model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO ROOT_INFO(ROOT_ID,ROOT_NAME,ROOT_PWD,REAL_NAME,TELPHONE,STATUS,CREATTIME,UPTIME) VALUES (@ROOT_ID,@ROOT_NAME,@ROOT_PWD,@REAL_NAME,@TELPHONE,@STATUS,@CREATTIME,@UPTIME);SELECT @@identity"
				,new SqlParameter("@ROOT_ID", model.ROOT_ID)
				,new SqlParameter("@ROOT_NAME", model.ROOT_NAME)
				,new SqlParameter("@ROOT_PWD", model.ROOT_PWD)
				,new SqlParameter("@REAL_NAME", model.REAL_NAME)
				,new SqlParameter("@TELPHONE", model.TELPHONE)
				,new SqlParameter("@STATUS", model.STATUS)
				,new SqlParameter("@CREATTIME", model.CREATTIME)
				,new SqlParameter("@UPTIME", model.UPTIME)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM ROOT_INFO WHERE ROOT_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(ROOT_INFO model)
		{
			string sql = "UPDATE ROOT_INFO SET ROOT_ID=@ROOT_ID,ROOT_NAME=@ROOT_NAME,ROOT_PWD=@ROOT_PWD,REAL_NAME=@REAL_NAME,TELPHONE=@TELPHONE,STATUS=@STATUS,CREATTIME=@CREATTIME,UPTIME=@UPTIME WHERE ROOT_ID=@ROOT_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ROOT_ID", model.ROOT_ID)
				, new SqlParameter("@ROOT_NAME", model.ROOT_NAME)
				, new SqlParameter("@ROOT_PWD", model.ROOT_PWD)
				, new SqlParameter("@REAL_NAME", model.REAL_NAME)
				, new SqlParameter("@TELPHONE", model.TELPHONE)
				, new SqlParameter("@STATUS", model.STATUS)
				, new SqlParameter("@CREATTIME", model.CREATTIME)
				, new SqlParameter("@UPTIME", model.UPTIME)
			);
			return rows > 0;
		}

		public ROOT_INFO Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM ROOT_INFO WHERE ROOT_ID=@ROOT_ID", new SqlParameter("@ROOT_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			ROOT_INFO model = ToModel(row);
			return model;
		}

		private static ROOT_INFO ToModel(DataRow row)
		{
			ROOT_INFO model = new ROOT_INFO();
			model.ROOT_ID = (int)row["ROOT_ID"];
			model.ROOT_NAME = (string)row["ROOT_NAME"];
			model.ROOT_PWD = (string)row["ROOT_PWD"];
			model.REAL_NAME = (string)row["REAL_NAME"];
			model.TELPHONE = (int)row["TELPHONE"];
			model.STATUS = (int)row["STATUS"];
			model.CREATTIME = (DateTime)row["CREATTIME"];
			model.UPTIME = (DateTime)row["UPTIME"];
			return model;
		}

		public IEnumerable<ROOT_INFO> ListAll()
		{
			List<ROOT_INFO> list = new List<ROOT_INFO>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM ROOT_INFO");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
