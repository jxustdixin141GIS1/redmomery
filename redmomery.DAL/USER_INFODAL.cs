using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class USER_INFODAL
	{
		public int addNew(USER_INFO model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(USER_INFO model)
		{
            return Update(model);
		}

		public USER_INFO get(int id)
		{
            return Get(id);
		}
		public IEnumerable<USER_INFO> Listall()
		{
            return ListAll();
		}
        public USER_INFO get(string name, string password)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("select * from USER_INFO where (USER_NETNAME=@USER_NETNAME and USER_PSWD=@USER_PSWD)", new SqlParameter("@USER_NETNAME", name), new SqlParameter("@USER_PSWD",password));
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            USER_INFO model = ToModel(row);
            return model;
        }
        public USER_INFO getByMD5(string MD5)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("select * from USER_INFO where MD5=@MD5", new SqlParameter("@MD5", MD5));
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            USER_INFO model = ToModel(row);
            return model;
        }
	}
}
