using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	partial class sysdiagramsDAL
	{
		public int AddNew(sysdiagrams model)
		{
			object obj = SqlHelper.ExecuteScalar(
				"INSERT INTO sysdiagrams(name,principal_id,diagram_id,version,definition) VALUES (@name,@principal_id,@diagram_id,@version,@definition);SELECT @@identity"
				,new SqlParameter("@name", model.name)
				,new SqlParameter("@principal_id", model.principal_id)
				,new SqlParameter("@diagram_id", model.diagram_id)
				,new SqlParameter("@version", model.version)
				,new SqlParameter("@definition", model.definition)
			);
			return Convert.ToInt32(obj);
		}

		public bool Delete(int id)
		{
			int rows = SqlHelper.ExecuteNonQuery("DELETE FROM sysdiagrams WHERE name = @id", new SqlParameter("@id", id));
			return rows > 0;
		}

		public bool Update(sysdiagrams model)
		{
			string sql = "UPDATE sysdiagrams SET name=@name,principal_id=@principal_id,diagram_id=@diagram_id,version=@version,definition=@definition WHERE name=@name";
			int rows = SqlHelper.ExecuteNonQuery(sql
				, new SqlParameter("@name", model.name)
				, new SqlParameter("@principal_id", model.principal_id)
				, new SqlParameter("@diagram_id", model.diagram_id)
				, new SqlParameter("@version", model.version)
				, new SqlParameter("@definition", model.definition)
			);
			return rows > 0;
		}

		public sysdiagrams Get(int id)
		{
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM sysdiagrams WHERE name=@name", new SqlParameter("@name", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			sysdiagrams model = ToModel(row);
			return model;
		}

		private static sysdiagrams ToModel(DataRow row)
		{
			sysdiagrams model = new sysdiagrams();
			model.name = (string)row["name"];
			model.principal_id = (int)row["principal_id"];
			model.diagram_id = (int)row["diagram_id"];
			model.version = (int)row["version"];
			model.definition = (object)row["definition"];
			return model;
		}

		public IEnumerable<sysdiagrams> ListAll()
		{
			List<sysdiagrams> list = new List<sysdiagrams>();
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM sysdiagrams");
			foreach (DataRow row in dt.Rows)
			{
				list.Add(ToModel(row));
			}
			return list;
		}
	}
}
