using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class LB_INFODAL
	{
        public  List<LB_INFO> getByTitle(int T_ID)
        {
            List<LB_INFO> list = new List<LB_INFO>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM LB_INFO WHERE T_ID=@ID", new SqlParameter("@ID", T_ID));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LBName"></param>
        /// <returns>返回的是老兵列表</returns>
        public List<LB_INFO> GetByName(string LBName)
        {
            List<LB_INFO> list = new List<LB_INFO>();
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM LB_INFO  where LBname like @LBname", new SqlParameter("@LBname","%"+LBName+"%"));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
        //
        public string localtiontoWKT(LB_INFO lb)
        {
            if (lb.X.ToString() != "" && lb.Y.ToString() != "")
            {
                string locationpoint = "Point(" + lb.X.ToString() + " " + lb.Y.ToString() + ")";
                string localpoint = locationpoint.ToUpper();//"geometry::STGeomFromText('" + locationpoint.ToUpper() + "', 4326)";//SqlGeography.STPointFromText(pars, 4326);
                lb.Location = localpoint;
            }
            else
            {
                lb.Location = "null";
            }
            return lb.Location.ToString();
        }



        //-----------------自动生成的代码
        #region 自动生成的代码
        public int addNew(LB_INFO model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(LB_INFO model)
		{
            return Update(model);
		}

		public LB_INFO get(int id)
		{
            return Get(id);
		}

	
		public IEnumerable<LB_INFO> Listall()
		{
            return ListAll();
        }
        #endregion
        #region 废弃代码
        /// <summary>
        /// 这个方法其实是一个过时代码
        /// </summary>
        /// <param name="LB"></param>
        /// <returns></returns>
        public LB_INFO GetByModel(LB_INFO LB)
        {
            DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM LB_INFO WHERE T_ID=@T_ID and LBname=@LBname and LBdomicile=@LBdomicile and LBbirthday=@LBbirthday",
                new SqlParameter("@T_ID", LB.T_ID),
               new SqlParameter("@LBname", LB.LBname),
               new SqlParameter("@LBdomicile", LB.LBdomicile),
               new SqlParameter("@LBbirthday", LB.LBbirthday)
                );
            if (dt.Rows.Count > 1)
            {
                throw new Exception("more than 1 row was found");
            }
            if (dt.Rows.Count <= 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            LB_INFO model = ToModel(row);
            return model;
        }
        #endregion
    }
}
