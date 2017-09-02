using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;
namespace redmomery.DAL
{
    public partial class View_MultimessageDAL
    {
        public List<View_Multimessage> getBytime(int GID, DateTime dtime)
        {
            List<View_Multimessage> list = new List<View_Multimessage>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM View_Multimessage where (TGID = @TGID  and  Ftime >= @Ftime  )    order by Ftime  ", new SqlParameter("@TGID", GID), new SqlParameter("@Ftime", dtime));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
    }
}
