using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class View_m_LDAL
    {
        #region 自动生成
        public int addNew(View_m_L model)
        {
            return AddNew(model);
        }

        public bool delete(int id)
        {
            return Delete(id);
        }

        public bool update(View_m_L model)
        {
            return Update(model);
        }

        public View_m_L get(int id)
        {
            return Get(id);
        }
        public IEnumerable<View_m_L> Listall()
        {
            return ListAll();
        }
        
        #endregion
        /**************前面为工具自动生成的代码************************
         * 
         * 由于简单的增删改查代码，是简单的，所以我就采用采用工具进行自动生成
         * 
         * ************下面是自己进行设计的代码***********************
         * */
        /// <summary>
        /// 通过人名进行查找
        /// </summary>
        /// <param name="U_ID">人名ID</param>
        /// <returns></returns>
        public List<View_m_L> ListByU_ID(int[] U_ID)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            for (int i = 0; i < U_ID.Length; i++)
            {
                //进行批量组合成查询语句
                if(i==(U_ID.Length-1))
                {
                    sql += "U_ID = @U_ID" + i.ToString() + ") ";
                    SqlParameter sqlp = new SqlParameter("@U_ID" + i.ToString(), U_ID[i]);
                    sqlps.Add(sqlp);
                }
                else
                {
                  sql+="U_ID = @U_ID"+i.ToString()+"  OR  ";
                  SqlParameter sqlp = new SqlParameter("@U_ID" + i.ToString(), U_ID[i]);
                  sqlps.Add(sqlp);
                }
            }
            DataTable dt = SqlHelper.ExecuteDataTable(sql,getsqlpsFromlist(sqlps));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
        /// <summary>
        /// 按照帖子标题进行查找
        /// </summary>
        /// <param name="Title">标题关键字</param>
        /// <returns></returns>
        public List<View_m_L> ListByB_Title_or(string[] Title)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            string connection = "  OR  ";
            for (int i = 0; i < Title.Length; i++)
            {
                //进行批量组合成查询语句
                if (i == (Title.Length - 1))
                {
                    sql += "B_TITLE like @B_TITLE" + i.ToString() ;
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
                else
                {
                    sql += "B_TITLE like @B_TITLE" + i.ToString() + connection;
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
            }
            sql += ") ";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, getsqlpsFromlist(sqlps));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        
        }
        public List<View_m_L> ListByB_Title_and(string[] Title)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            string connection = "  AND  ";
            for (int i = 0; i < Title.Length; i++)
            {
                //进行批量组合成查询语句
                if (i == (Title.Length - 1))
                {
                    sql += "B_TITLE like @B_TITLE" + i.ToString() ;
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
                else
                {
                    sql += "B_TITLE like @B_TITLE" + i.ToString() + connection;
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
            }
            sql += ") ";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, getsqlpsFromlist(sqlps));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
        /// <summary>
        /// 按照模块关键字进行检索，检索模式为OR
        /// </summary>
        /// <param name="Title">关键字数组</param>
        /// <returns></returns>
        public List<View_m_L> ListByM_Key_or(string[] Title)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            string connection = "  OR  ";
            for (int i = 0; i < Title.Length; i++)
            {
                //进行批量组合成查询语句
                if (i == (Title.Length - 1))
                {
                    sql += "M_Key like @B_TITLE" + i.ToString();
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
                else
                {
                    sql += "M_Key like @B_TITLE" + i.ToString() + connection;
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
            }
            sql += ") ";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, getsqlpsFromlist(sqlps));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;

        }
        /// <summary>
        /// 按照模块关键字进行检索，检索模式为And
        /// </summary>
        /// <param name="Title">关键字数组</param>
        /// <returns></returns>
        /// 
        public List<View_m_L> ListByM_Key_and(string[] Title)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            string connection = "  AND  ";
            for (int i = 0; i < Title.Length; i++)
            {
                //进行批量组合成查询语句
                if (i == (Title.Length - 1))
                {
                    sql += "M_Key like @B_TITLE" + i.ToString();
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
                else
                {
                    sql += "M_Key like @B_TITLE" + i.ToString() + connection;
                    SqlParameter sqlp = new SqlParameter("@B_TITLE" + i.ToString(), "%" + Title[i] + "%");
                    sqlps.Add(sqlp);
                }
            }
            sql += ") ";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, getsqlpsFromlist(sqlps));
            foreach (DataRow row in dt.Rows)
            {
                list.Add(ToModel(row));
            }
            return list;
        }
        /// <summary>
        /// 按照模块创建人的姓名进行创建
        /// </summary>
        /// <param name="USER_NETNAME"></param>
        /// <returns></returns>
        public List<View_m_L> getByUserNames(string USER_NETNAME)
        {
            List<Model.View_m_L> result = new List<View_m_L>();
            //sql语句
            string sql = "SELECT * FROM View_m_L WHERE USER_NETNAME like @T_key";
            SqlParameter par = new SqlParameter("@T_key", "%" + USER_NETNAME + "%");
            DataTable dt = SqlHelper.ExecuteDataTable(sql, par);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        #region  常用的命令是代码
        private SqlParameter[] getsqlpsFromlist(List<SqlParameter> listparsql)
        {
            SqlParameter[] sqls = new SqlParameter[listparsql.Count];
            for (int i = 0; i < sqls.Length; i++)
            {
                sqls[i] = (SqlParameter)listparsql[i];
            }
            return sqls;
        }
        #endregion

    }
}
