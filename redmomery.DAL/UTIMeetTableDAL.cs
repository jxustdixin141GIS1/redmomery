using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using redmomery.Model;
namespace redmomery.DAL
{
   public partial   class UTIMeetTableDAL
    {
       public List<UTIMeetTable> getByUID(int UID)
       {
           List<UTIMeetTable> list = new List<UTIMeetTable>();
           DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM UTIMeetTable where UID=@UID",new SqlParameter("@UID",UID));
           foreach (DataRow row in dt.Rows)
           {
               list.Add(ToModel(row));
           }
           return list;
       }
    }
}
