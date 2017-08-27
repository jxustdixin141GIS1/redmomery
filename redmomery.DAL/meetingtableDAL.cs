using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;
namespace redmomery.DAL
{
    public partial class meetingtableDAL
    {
        public List<meetingtable> getListByLBID(string UID)
        {
            List<meetingtable> list = new List<meetingtable>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM meetingtable where UID=@UID", new SqlParameter("@UID",UID));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
    }
}
