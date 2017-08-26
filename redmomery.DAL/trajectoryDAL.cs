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
    }
}
