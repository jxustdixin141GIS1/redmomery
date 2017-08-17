using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class moduleBBS_TableDAL
    {
        #region
       public int addNew(moduleBBS_Table model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

      
		public bool update(moduleBBS_Table model)
		{
            return Update(model);
		}

		public moduleBBS_Table get(int id)
		{
            return Get(id);
		}
		public IEnumerable<moduleBBS_Table> Listall()
		{
            return ListAll();
		}
        #endregion
        //---------------------------------------------下面为按照需求进行创建---------------------------------
        public bool add_N_T(int ID)
        {
            string sql = "upate moduleBBS_Table set N_TITLE_T=N_TITLE_T+1 where ID=@T_ID";
            int rows = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@T_ID", ID));
            return rows > 0;
        }
        public moduleBBS_Table getByMD5(string MD5)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM moduleBBS_Table WHERE MD5=@ID", new SqlParameter("@ID", MD5));
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            moduleBBS_Table model = ToModel(row);
            return model;
        }

    }
}
