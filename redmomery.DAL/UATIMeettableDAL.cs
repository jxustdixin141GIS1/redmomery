using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
   public partial class UATIMeettableDAL
    {
       public List<UATIMeettable> getByUIDMID(int UID,int MID)
       {
           List<UATIMeettable> list = new List<UATIMeettable>();
           DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM UATIMeettable  where  (UID=@UID AND  meetID=@meetID)"
               , new SqlParameter("@UID",UID)
               , new SqlParameter("@meetID",MID));
           foreach (DataRow row in dt.Rows)
           {
               list.Add(ToModel(row));
           }
           return list;
       }
       public List<UATIMeettable> getByMID(int MID)
       {
           List<UATIMeettable> list = new List<UATIMeettable>();
           DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM UATIMeettable  where   meetID=@meetID"
               , new SqlParameter("@meetID", MID));
           foreach (DataRow row in dt.Rows)
           {
               list.Add(ToModel(row));
           }
           return list;
       }
    }
}
