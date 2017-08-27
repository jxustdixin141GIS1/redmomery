using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace redmomery.DAL
{
	partial class SqlHelper
	{
	 

		public static int ExecuteNonQuery(string cmdText, params SqlParameter[] parameters)
		{
			using (SqlConnection conn = new SqlConnection(connstr))
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = cmdText;
					cmd.Parameters.AddRange(parameters);
					return cmd.ExecuteNonQuery();
				}
			}
		}

		public static object ExecuteScalar(string cmdText, params SqlParameter[] parameters)
		{
			using (SqlConnection conn = new SqlConnection(connstr))
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = cmdText;
					cmd.Parameters.AddRange(parameters);
					return cmd.ExecuteScalar();
				}
			}
		}

		public static DataTable ExecuteDataTable(string cmdText, params SqlParameter[] parameters)
		{
			using (SqlConnection conn = new SqlConnection(connstr))
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = cmdText;
					cmd.Parameters.AddRange(parameters);
					using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
					{
						DataTable dt = new DataTable();
						adapter.Fill(dt);
						return dt;
					}
				}
			}
		}

		public static SqlDataReader ExecuteDataReader(string cmdText, params SqlParameter[] parameters)
		{
			using (SqlConnection conn = new SqlConnection(connstr))
			{
				conn.Open();
				using (SqlCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = cmdText;
					cmd.Parameters.AddRange(parameters);
					return cmd.ExecuteReader(CommandBehavior.CloseConnection);
				}
			}
		}
		public static object FromDbValue(object value)
		{
			if (value == DBNull.Value)
			{
				return null;
			}
			else
			{
				return value;
			}
		}
		public static object ToDbValue(object value)
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			else
			{
				return value;
			}
		}
	}
}
