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
       public List<trajectory> getByLBID(string LBID)
       { 
        
			DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM trajectory WHERE LBID=@LBID", new SqlParameter("@LBID", LBID));
            List<trajectory> list = new List<trajectory>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];
			trajectory model = ToModel(row);
            list.Add(model);

            }
			return list;
		}

       }
}
