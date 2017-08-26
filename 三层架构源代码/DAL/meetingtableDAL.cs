using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class meetingtableDAL
	{
		public int AddNew(meetingtable model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO meetingtable(UID,GID,Ttime,local,context,vnum,isCheck,lng,lat) VALUES (@UID,@GID,@Ttime,@local,@context,@vnum,@isCheck,@lng,@lat);SELECT @@identity"
				,new SqlParameter("@UID", model.UID)
				,new SqlParameter("@GID", model.GID)
				,new SqlParameter("@Ttime", model.Ttime)
				,new SqlParameter("@local", model.local)
				,new SqlParameter("@context", model.context)
				,new SqlParameter("@vnum", model.vnum)
				,new SqlParameter("@isCheck", model.isCheck)
				,new SqlParameter("@lng", model.lng)
				,new SqlParameter("@lat", model.lat)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM meetingtable WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(meetingtable model)
		{
			string sql = "UPDATE meetingtable SET UID=@UID,GID=@GID,Ttime=@Ttime,local=@local,context=@context,vnum=@vnum,isCheck=@isCheck,lng=@lng,lat=@lat WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@UID", model.UID)
				, new SqlParameter("@GID", model.GID)
				, new SqlParameter("@Ttime", model.Ttime)
				, new SqlParameter("@local", model.local)
				, new SqlParameter("@context", model.context)
				, new SqlParameter("@vnum", model.vnum)
				, new SqlParameter("@isCheck", model.isCheck)
				, new SqlParameter("@lng", model.lng)
				, new SqlParameter("@lat", model.lat)
			);
			return rows > 0;
		}

		public meetingtable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM meetingtable WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			meetingtable model = ToModel(row);
			return model;
		}

		private static meetingtable ToModel(DataRow row)
		{
			meetingtable model = new meetingtable();
			model.ID = (int)row["ID"];
			model.UID = (int)row["UID"];
			model.GID = (int)row["GID"];
			model.Ttime = (DateTime)row["Ttime"];
			model.local = (string)row["local"];
			model.context = (string)row["context"];
			model.vnum = (int)row["vnum"];
			model.isCheck = (int)row["isCheck"];
			model.lng = (object)row["lng"];
			model.lat = (object)row["lat"];
			return model;
		}

		public IEnumerable<meetingtable> ListAll()
		{
			List<meetingtable> list = new List<meetingtable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM meetingtable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
