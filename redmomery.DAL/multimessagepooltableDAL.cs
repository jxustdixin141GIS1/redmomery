using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;
namespace redmomery.DAL
{
    public partial class multimessagepooltableDAL
    {
        public List<multimessagepooltable> getByGID(int GID)
        {
            List<multimessagepooltable> list = new List<multimessagepooltable>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM multimessagepooltable where TGID = @TGID", new SqlParameter("@TGID",GID));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
    }
}
