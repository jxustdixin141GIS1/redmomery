using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class View_t_CLDAL
    {
       



        //---------------------------帖子搜索-------------------------------
        /// <summary>
        /// 按照帖子名进行检索
        /// </summary>
        public List<Model.View_t_CL> getByTitleNames(string titleName)
        {
            List<Model.View_t_CL> result = new List<View_t_CL>();
            //sql语句
            string sql = "SELECT * FROM View_t_CL WHERE TITLE like @T_key";
            SqlParameter par = new SqlParameter("@T_key", "%" + titleName + "%");
            DataTable dt = SqlHelper.ExecuteDataTable(sql, par);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照帖子名进行搜索  方法 and
        /// </summary>
        public List<Model.View_t_CL> getByTitleNames_and(string[] titleNames)
        {
            List<View_t_CL> result = new List<View_t_CL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_t_CL WHERE ";
            string connections = "  and  ";
            for (int i = 0; i < titleNames.Length; i++)
            {
                if (i == titleNames.Length - 1)
                {
                    string conditions = "TITLE like  @T_key" + i.ToString();
                    SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
                else
                {
                    string conditions = "TITLE like  @T_key" + i.ToString() + connections;
                    SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
            }
            DataTable dt = SqlHelper.ExecuteDataTable(sql, SqlHelper.getsqlpsFromlist(parmembers));

            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照帖子名进行搜索  方法 or
        /// </summary>
        public List<Model.View_t_CL> getByTitleNames_or(string[] titleNames)
        {
            List<View_t_CL> result = new List<View_t_CL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_t_CL WHERE ";
            string connections = "  or  ";
            for (int i = 0; i < titleNames.Length; i++)
            {
                if (i == titleNames.Length - 1)
                {
                    string conditions = "TITLE like  @T_key" + i.ToString();
                    SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
                else
                {
                    string conditions = "TITLE like  @T_key" + i.ToString() + connections;
                    SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
            }
            DataTable dt = SqlHelper.ExecuteDataTable(sql, SqlHelper.getsqlpsFromlist(parmembers));

            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照单个关键字进行检索之后的代码
        /// </summary>
        public List<View_t_CL> getByKeyValue(string KeyValuae)
        {
            List<View_t_CL> result = new List<View_t_CL>();
            string sql = "SELECT * FROM View_t_CL WHERE T_key like @T_key";
            SqlParameter par = new SqlParameter("@T_key","%"+KeyValuae+"%");
            DataTable dt = SqlHelper.ExecuteDataTable(sql, par);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照多关键字进行检索 方式 and 
        /// </summary>
        public List<View_t_CL> getByKeyValues_and(string[] KeyValues)
        {
            List<View_t_CL> result = new List<View_t_CL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_t_CL WHERE ";
            string connections = "  and  ";
              for (int i = 0; i < KeyValues.Length;i++ )
              {
                  if (i == KeyValues.Length - 1)
                  {
                      string conditions = "T_key like  @T_key" + i.ToString() ;
                      SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + KeyValues[i] + "%");
                      parmembers.Add(parmember);
                      sql += conditions;
                  }
                  else
                  {
                      string conditions = "T_key like  @T_key" + i.ToString()+connections;
                      SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + KeyValues[i] + "%");
                      parmembers.Add(parmember);
                      sql += conditions;
                  }
              }
              DataTable dt = SqlHelper.ExecuteDataTable(sql, SqlHelper.getsqlpsFromlist(parmembers));

              foreach (DataRow row in dt.Rows)
              {
                  result.Add(ToModel(row));
              }
              return result;
        }
        /// <summary>
        /// 按照多关键字进行检索 方式 or
        /// </summary>
        public List<View_t_CL> getByKeyValues_or(string[] KeyValues)
        {
            List<View_t_CL> result = new List<View_t_CL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_t_CL WHERE ";
            string connections = "  or  ";
            for (int i = 0; i < KeyValues.Length; i++)
            {
                if (i == KeyValues.Length - 1)
                {
                    string conditions = "T_key like  @T_key" + i.ToString();
                    SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + KeyValues[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
                else
                {
                    string conditions = "T_key like  @T_key" + i.ToString() + connections;
                    SqlParameter parmember = new SqlParameter("@T_key" + i.ToString(), "%" + KeyValues[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
            }
            DataTable dt = SqlHelper.ExecuteDataTable(sql, SqlHelper.getsqlpsFromlist(parmembers));

            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        //--------------------------帖子搜索结束-----------------------------


        //-----------------------------逻辑代码结束--------------------------------------

       //------------------------------代码自动生成区域----------------------------------
        public int addNew(View_t_CL model)
        {
            return AddNew(model);
        }

        public bool delete(int id)
        {
            return Delete(id);
        }

        public bool update(View_t_CL model)
        {
            return Update(model);
        }

        public View_t_CL get(int id)
        {
            return Get(id);
        }

        public IEnumerable<View_t_CL> Listall()
        {
            return ListAll();
        }
    }
}
