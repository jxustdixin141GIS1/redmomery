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
        /// <summary>
        /// 这个方法只要的目的就是为了获取当前指定时间内的数据库资源化
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public List<multimessagepooltable> getBytime(int GID,DateTime dtime)
         { 
            List<multimessagepooltable> list = new List<multimessagepooltable>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM multimessagepooltable where (TGID = @TGID  and  Ftime >= @Ftime  )", new SqlParameter("@TGID", GID), new SqlParameter("@Ftime",dtime));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
    }
}
