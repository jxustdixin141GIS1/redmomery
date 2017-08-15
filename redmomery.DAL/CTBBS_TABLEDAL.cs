using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class CTBBS_TABLEDAL
    {
        /// <summary>
        /// 帖子的ID
        /// </summary>
        public List<CTBBS_TABLE> getByT_ID(int T_ID)
        {
            List<CTBBS_TABLE> result = new List<CTBBS_TABLE>();
            string sql = "select * from CTBBS_TABLE where T_ID=@T_ID  order by ID ";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@T_ID",T_ID));
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        //-----------------------------------自动生成代码----------------------------
        #region
        public int addNew(CTBBS_TABLE model)
		{
            return AddNew(model);
		}
       
		public bool update(CTBBS_TABLE model)
		{
            return Update(model);
		}

		public CTBBS_TABLE get(int id)
		{
            return Get(id);
		}
		public IEnumerable<CTBBS_TABLE> Listall()
		{
            return ListAll();
		}
        #endregion

        public bool addC_N(int c_id)
        {
            int yn = 1;
            int count = 0;
            string sql = "exec dbo.add_C_N @c_id,@yn";
            count = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@c_id", c_id), new SqlParameter("@yn", yn));
            if (count != 0)
                return true;
            return false;
        }
        public bool addc_y(int c_id)
        {
            int yn = 2;
            int count = 0;
            string sql = "exec dbo.add_C_N @c_id,@yn";
            count = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@c_id", c_id), new SqlParameter("@yn", yn));
            if (count != 0)
                return true;
            return false;
        }
        public bool clear(int id)
        {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM CTBBS_TABLE WHERE ID = @id", new SqlParameter("@id", id));
            return rows > 0;
        }
        public bool delete(int id)
        {
            int rows = SqlHelper.ExecuteNonQuery("update CTBBS_TABLE  set is_delete = 3 WHERE ID = @id", new SqlParameter("@id", id));
            return rows > 0;
        }
	}
}
