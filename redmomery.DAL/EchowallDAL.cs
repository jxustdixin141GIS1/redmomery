using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
  public partial  class EchowallDAL
    {
      public bool DelByLBID(string LBID)
      {
          int rows = SqlHelper.ExecuteNonQuery("DELETE FROM Echowall WHERE LBID = @LBID", new SqlParameter("@LBID", LBID));
          return rows > 0;
      }
    }
}
