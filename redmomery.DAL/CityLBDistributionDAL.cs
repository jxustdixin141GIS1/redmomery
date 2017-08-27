using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;
namespace redmomery.DAL
{
     public partial  class CityLBDistributionDAL
    {
         public static CityLBDistribution getModelbyCityCode(string CityCode)
         {
             DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CityLBDistribution WHERE CityName=@CityName", new SqlParameter("@CityName", CityCode));
             if (dt.Rows.Count > 1)
             {
                 throw new Exception("more than 1 row was found");
             }
             if (dt.Rows.Count <= 0)
             {
                 return null;
             }
             DataRow row = dt.Rows[0];
             CityLBDistribution model = ToModel(row);
             return model;
         }
         public static CityLBDistribution getModelbyCityName(string CityName)
         {
             DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM CityLBDistribution WHERE CityName=@CityName", new SqlParameter("@CityName", CityName));
             if (dt.Rows.Count > 1)
             {
                 throw new Exception("more than 1 row was found");
             }
             if (dt.Rows.Count <= 0)
             {
                 return null;
             }
             DataRow row = dt.Rows[0];
             CityLBDistribution model = ToModel(row);
             return model;
         } 
    }
}
