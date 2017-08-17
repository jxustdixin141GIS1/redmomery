using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class PUBLIC_RECORDDAL
	{
		public int AddNew(PUBLIC_RECORD model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO PUBLIC_RECORD(PUB_ID,PUB_ADDRESS,IP,STATE,CREATTIME) VALUES (@PUB_ID,@PUB_ADDRESS,@IP,@STATE,@CREATTIME);SELECT @@identity"
				,new SqlParameter("@PUB_ID", model.PUB_ID)
				,new SqlParameter("@PUB_ADDRESS", model.PUB_ADDRESS)
				,new SqlParameter("@IP", model.IP)
				,new SqlParameter("@STATE", model.STATE)
				,new SqlParameter("@CREATTIME", model.CREATTIME)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM PUBLIC_RECORD WHERE PUB_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(PUBLIC_RECORD model)
		{
			string sql = "UPDATE PUBLIC_RECORD SET PUB_ID=@PUB_ID,PUB_ADDRESS=@PUB_ADDRESS,IP=@IP,STATE=@STATE,CREATTIME=@CREATTIME WHERE PUB_ID=@PUB_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@PUB_ID", model.PUB_ID)
				, new SqlParameter("@PUB_ADDRESS", model.PUB_ADDRESS)
				, new SqlParameter("@IP", model.IP)
				, new SqlParameter("@STATE", model.STATE)
				, new SqlParameter("@CREATTIME", model.CREATTIME)
			);
			return rows > 0;
		}

		public PUBLIC_RECORD Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM PUBLIC_RECORD WHERE PUB_ID=@PUB_ID", new SqlParameter("@PUB_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			PUBLIC_RECORD model = ToModel(row);
			return model;
		}

		private static PUBLIC_RECORD ToModel(DataRow row)
		{
			PUBLIC_RECORD model = new PUBLIC_RECORD();
			model.PUB_ID = (int)row["PUB_ID"];
			model.PUB_ADDRESS = (string)row["PUB_ADDRESS"];
			model.IP = (string)row["IP"];
			model.STATE = (int)row["STATE"];
			model.CREATTIME = (DateTime)row["CREATTIME"];
			return model;
		}

		public IEnumerable<PUBLIC_RECORD> ListAll()
		{
			List<PUBLIC_RECORD> list = new List<PUBLIC_RECORD>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM PUBLIC_RECORD");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
