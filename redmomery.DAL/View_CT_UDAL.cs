using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
  public partial class View_CT_UDAL
	{
      public List<View_CT_U> getByTID(int TID)
      {
          string sql = "SELECT * FROM View_CT_U WHERE T_ID=@T_ID  and  is_delete < 3";
          SqlParameter par = new SqlParameter("@T_ID", TID);
          List<View_CT_U> list = new List<View_CT_U>();
          DataTable dt = SqlHelper.ExecuteDataTable(sql,par);
          foreach (DataRow row in dt.Rows)
          {
              list.Add(ToModel(row));
          }
          return list;
      }
	}
}
