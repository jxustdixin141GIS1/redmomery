using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;
namespace redmomery.DAL
{
   public partial  class trajectoryDAL
    {
       public bool deleteByLBID(string LBID)
       {
           int rows = SqlHelper.ExecuteNonQuery("DELETE FROM trajectory WHERE LBID= @LBID", new SqlParameter("@LBID", LBID));
           return rows > 0;
       }
       public trajectory getByLBID(string LBID)
       { 
        
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM trajectory WHERE LBID=@LBID", new SqlParameter("@LBID", id));
			if (dt.Rows.Count > 1)
			{
				throw new Exception("more than 1 row was found");
			}
			if (dt.Rows.Count <= 0)
			{
				return null;
			}
			DataRow row = dt.Rows[0];
			trajectory model = ToModel(row);
			return model;
		}

       }
    }
}
