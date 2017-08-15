using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class CCBBS_TABLEDAL
	{
        //-------------------------------------------逻辑代码-----------------------------------------
        public List<CCBBS_TABLE> getByC_ID(int C_ID)
        {
           string sql="SELECT * FROM CCBBS_TABLE WHERE C_ID=@ID";
            List<CCBBS_TABLE> list = new List<CCBBS_TABLE>();
            DataTable dt = SqlHelper.ExecuteDataTable(sql, new SqlParameter("@ID", C_ID));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }




        //-------------------------------------------自动生成的代码------------------------------------
		public int addNew(CCBBS_TABLE model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(CCBBS_TABLE model)
		{
            return Update(model);
		}

		public CCBBS_TABLE get(int id)
		{
            return Get(id);
		}

	

		public IEnumerable<CCBBS_TABLE> Listall()
		{
            return ListAll();
		}
	}
}
