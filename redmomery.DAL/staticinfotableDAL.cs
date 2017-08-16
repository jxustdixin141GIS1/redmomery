using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;
namespace redmomery.DAL
{
   public partial class staticinfotableDAL
    {
       public redmomery.Model.staticinfotable GetbycityCode(string citycode)
       {
           DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM staticinfotable WHERE citycode=@ID", new SqlParameter("@ID", citycode));
           if (dt.Rows.Count > 1)
           {
               throw new Exception("more than 1 row was found");
           }
           if (dt.Rows.Count <= 0)
           {
               return null;
           }
           DataRow row = dt.Rows[0];
           staticinfotable model = ToModel(row);
           return model;
       }
       public redmomery.Model.staticinfotable GetbycityName(string cityName)
       {
           DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM staticinfotable WHERE cityname=@ID", new SqlParameter("@ID", cityName));
           if (dt.Rows.Count > 1)
           {
               throw new Exception("more than 1 row was found");
           }
           if (dt.Rows.Count <= 0)
           {
               return null;
           }
           DataRow row = dt.Rows[0];
           staticinfotable model = ToModel(row);
           return model;
       }
    }
}
