using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class USER_INFODAL
	{
		public int AddNew(USER_INFO model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO USER_INFO(USER_NAME,USER_SEX,USER_JOB,USER_BIRTHDAY,USER_ADDRESS,USER_PHONE,USER_EMEIL,USER_NETNAME,USER_IMG,USER_PSWD,ISPASS,MD5) VALUES (@USER_NAME,@USER_SEX,@USER_JOB,@USER_BIRTHDAY,@USER_ADDRESS,@USER_PHONE,@USER_EMEIL,@USER_NETNAME,@USER_IMG,@USER_PSWD,@ISPASS,@MD5);SELECT @@identity"
				
				,new SqlParameter("@USER_NAME", model.USER_NAME)
				,new SqlParameter("@USER_SEX", model.USER_SEX)
				,new SqlParameter("@USER_JOB", model.USER_JOB)
				,new SqlParameter("@USER_BIRTHDAY", model.USER_BIRTHDAY)
				,new SqlParameter("@USER_ADDRESS", model.USER_ADDRESS)
				,new SqlParameter("@USER_PHONE", model.USER_PHONE)
				,new SqlParameter("@USER_EMEIL", model.USER_EMEIL)
				,new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				,new SqlParameter("@USER_IMG", model.USER_IMG)
				,new SqlParameter("@USER_PSWD", model.USER_PSWD)
				,new SqlParameter("@ISPASS", model.ISPASS)
				,new SqlParameter("@MD5", model.MD5)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM USER_INFO WHERE USER_ID = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(USER_INFO model)
		{
			string sql = "UPDATE USER_INFO SET         USER_NAME=@USER_NAME,USER_SEX=@USER_SEX,USER_JOB=@USER_JOB,USER_BIRTHDAY=@USER_BIRTHDAY,USER_ADDRESS=@USER_ADDRESS,USER_PHONE=@USER_PHONE,USER_EMEIL=@USER_EMEIL,USER_NETNAME=@USER_NETNAME,USER_IMG=@USER_IMG,USER_PSWD=@USER_PSWD,ISPASS=@ISPASS,MD5=@MD5 WHERE USER_ID=@USER_ID";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@USER_ID", model.USER_ID)
				, new SqlParameter("@USER_NAME", model.USER_NAME)
				, new SqlParameter("@USER_SEX", model.USER_SEX)
				, new SqlParameter("@USER_JOB", model.USER_JOB)
				, new SqlParameter("@USER_BIRTHDAY", model.USER_BIRTHDAY)
				, new SqlParameter("@USER_ADDRESS", model.USER_ADDRESS)
				, new SqlParameter("@USER_PHONE", model.USER_PHONE)
				, new SqlParameter("@USER_EMEIL", model.USER_EMEIL)
				, new SqlParameter("@USER_NETNAME", model.USER_NETNAME)
				, new SqlParameter("@USER_IMG", model.USER_IMG)
				, new SqlParameter("@USER_PSWD", model.USER_PSWD)
				, new SqlParameter("@ISPASS", model.ISPASS)
				, new SqlParameter("@MD5", model.MD5)
			);
			return rows > 0;
		}

		public USER_INFO Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM USER_INFO WHERE USER_ID=@USER_ID", new SqlParameter("@USER_ID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			USER_INFO model = ToModel(row);
			return model;
		}

		private static USER_INFO ToModel(DataRow row)
		{
			USER_INFO model = new USER_INFO();
			model.USER_ID = (int)(row["USER_ID"]==null?0:row["USER_ID"].ToString()==""?0:int.Parse(row["USER_ID"].ToString()));
			model.USER_NAME = (string)(row["USER_NAME"]==null?"":row["USER_NAME"].ToString());
			model.USER_SEX = (string)(row["USER_SEX"]==null?"":row["USER_SEX"].ToString());
			model.USER_JOB = (string)(row["USER_JOB"]==null?"":row["USER_JOB"].ToString());
			model.USER_BIRTHDAY = (string)(row["USER_BIRTHDAY"]==null?"":row["USER_BIRTHDAY"].ToString());
			model.USER_ADDRESS = (string)(row["USER_ADDRESS"]==null?"":row["USER_ADDRESS"].ToString());
			model.USER_PHONE = (string)(row["USER_PHONE"]==null?"":row["USER_PHONE"].ToString());
			model.USER_EMEIL = (string)(row["USER_EMEIL"]==null?"":row["USER_EMEIL"].ToString());
			model.USER_NETNAME = (string)(row["USER_NETNAME"]==null?"":row["USER_NETNAME"].ToString());
			model.USER_IMG = (string)(row["USER_IMG"]==null?"":row["USER_IMG"].ToString());
			model.USER_PSWD = (string)(row["USER_PSWD"]==null?"":row["USER_PSWD"].ToString());
			model.ISPASS = (int)(row["ISPASS"]==null?3:row["ISPASS"].ToString()==""?3:int.Parse(row["ISPASS"].ToString()));
			model.MD5 = (string)(row["MD5"]==null?"":row["MD5"].ToString());
			return model;
		}
		public IEnumerable<USER_INFO> ListAll()
		{
			List<USER_INFO> list = new List<USER_INFO>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM USER_INFO");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
