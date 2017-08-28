using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class UATIMeettableDAL
	{
		public int AddNew(UATIMeettable model)
		{
            object obj = SqlHelper.ExecuteNonQuery(
				"INSERT INTO UATIMeettable(UID,meetID,dataTime,contentApply,state,dealUID) VALUES (@UID,@meetID,@dataTime,@contentApply,@state,@dealUID);"
				,new SqlParameter("@UID", model.UID)
				,new SqlParameter("@meetID", model.meetID)
				,new SqlParameter("@dataTime", model.dataTime)
				,new SqlParameter("@contentApply", model.contentApply)
				,new SqlParameter("@state", model.state)
				,new SqlParameter("@dealUID", model.dealUID)
			);
			return Convert.ToInt32(obj);
		}

        public bool Delete(int UID,int MeetID)
		{
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM UATIMeettable WHERE (UID=@UID AND  meetID=@meetID)"
                , new SqlParameter("@UID", UID)
                , new SqlParameter("@meetID",MeetID));
			return rows > 0;
		}

		public bool Update(UATIMeettable model)
		{
            string sql = "UPDATE UATIMeettable SET UID=@UID,meetID=@meetID,dataTime=@dataTime,contentApply=@contentApply,state=@state,dealUID=@dealUID WHERE (UID=@UID AND  meetID=@meetID)";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@UID", model.UID)
				, new SqlParameter("@meetID", model.meetID)
				, new SqlParameter("@dataTime", model.dataTime)
				, new SqlParameter("@contentApply", model.contentApply)
				, new SqlParameter("@state", model.state)
				, new SqlParameter("@dealUID", model.dealUID)
			);
			return rows > 0;
		}
        /// <summary>
        /// 这个方法已经过时，这里不推荐使用（严禁使用）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
		public UATIMeettable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM UATIMeettable WHERE UID=@UID", new SqlParameter("@UID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			UATIMeettable model = ToModel(row);
			return model;
		}

		private static UATIMeettable ToModel(DataRow row)
		{
			UATIMeettable model = new UATIMeettable();
			model.UID = (int)row["UID"];
			model.meetID = (int)row["meetID"];
			model.dataTime = (DateTime)row["dataTime"];
			model.contentApply = (string)row["contentApply"];
			model.state = (int)row["state"];
			model.dealUID = (int)row["dealUID"];
			return model;
		}

		public IEnumerable<UATIMeettable> ListAll()
		{
			List<UATIMeettable> list = new List<UATIMeettable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM UATIMeettable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
