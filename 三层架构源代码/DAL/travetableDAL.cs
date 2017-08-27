using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class travetableDAL
	{
		public int AddNew(travetable model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO travetable(Utime,UID,meetID,meetsubject,context,local,Vtime,lng,lat,point,isOK,nOK,nY) VALUES (@Utime,@UID,@meetID,@meetsubject,@context,@local,@Vtime,@lng,@lat,@point,@isOK,@nOK,@nY);SELECT @@identity"
				,new SqlParameter("@Utime", model.Utime)
				,new SqlParameter("@UID", model.UID)
				,new SqlParameter("@meetID", model.meetID)
				,new SqlParameter("@meetsubject", model.meetsubject)
				,new SqlParameter("@context", model.context)
				,new SqlParameter("@local", model.local)
				,new SqlParameter("@Vtime", model.Vtime)
				,new SqlParameter("@lng", model.lng)
				,new SqlParameter("@lat", model.lat)
				,new SqlParameter("@point", model.point)
				,new SqlParameter("@isOK", model.isOK)
				,new SqlParameter("@nOK", model.nOK)
				,new SqlParameter("@nY", model.nY)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM travetable WHERE ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(travetable model)
		{
			string sql = "UPDATE travetable SET Utime=@Utime,UID=@UID,meetID=@meetID,meetsubject=@meetsubject,context=@context,local=@local,Vtime=@Vtime,lng=@lng,lat=@lat,point=@point,isOK=@isOK,nOK=@nOK,nY=@nY WHERE ID=@ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@ID", model.ID)
				, new SqlParameter("@Utime", model.Utime)
				, new SqlParameter("@UID", model.UID)
				, new SqlParameter("@meetID", model.meetID)
				, new SqlParameter("@meetsubject", model.meetsubject)
				, new SqlParameter("@context", model.context)
				, new SqlParameter("@local", model.local)
				, new SqlParameter("@Vtime", model.Vtime)
				, new SqlParameter("@lng", model.lng)
				, new SqlParameter("@lat", model.lat)
				, new SqlParameter("@point", model.point)
				, new SqlParameter("@isOK", model.isOK)
				, new SqlParameter("@nOK", model.nOK)
				, new SqlParameter("@nY", model.nY)
			);
			return rows > 0;
		}

		public travetable Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM travetable WHERE ID=@ID", new SqlParameter("@ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			travetable model = ToModel(row);
			return model;
		}

		private static travetable ToModel(DataRow row)
		{
			travetable model = new travetable();
			model.ID = (int)row["ID"];
			model.Utime = (DateTime)row["Utime"];
			model.UID = (int)row["UID"];
			model.meetID = (int)row["meetID"];
			model.meetsubject = (string)row["meetsubject"];
			model.context = (string)row["context"];
			model.local = (string)row["local"];
			model.Vtime = (string)row["Vtime"];
			model.lng = (object)row["lng"];
			model.lat = (object)row["lat"];
			model.point = (object)row["point"];
			model.isOK = (int)row["isOK"];
			model.nOK = (int)row["nOK"];
			model.nY = (int)row["nY"];
			return model;
		}

		public IEnumerable<travetable> ListAll()
		{
			List<travetable> list = new List<travetable>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM travetable");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
