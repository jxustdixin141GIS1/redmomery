using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
	public  partial class View_m_TLDAL
	{


        //---------------------------帖子搜索------------------------------------
           //---------------------------------帖子搜索（交叉搜索）----------------------------

        public List<Model.View_m_TL> getByModulessIdAndTitleName(string Username, string[] titleNames)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_TL WHERE USER_NETNAME like @USER_NETNAME and ( ";
            SqlParameter parmember = new SqlParameter("@USER_NETNAME", Username);
            parmembers.Add(parmember);
            string connections = "  or  ";
            for (int i = 0; i < titleNames.Length; i++)
            {
                if (i == titleNames.Length - 1)
                {
                    string conditions = "TITLE like  @T_key" + i.ToString();
                    parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
                else
                {
                    string conditions = "TITLE like  @T_key" + i.ToString() + connections;
                    parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
            }
            sql += ")";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, SqlHelper.getsqlpsFromlist(parmembers));
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照模块的ID   和帖子名进行搜索
        /// </summary>
        public List<Model.View_m_TL> getByModulessIdAndTitleName(int M_ID, string[] titleNames)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_TL WHERE M_ID = @M_ID and (";
            SqlParameter parmember = new SqlParameter("@M_ID", M_ID);
            parmembers.Add(parmember);
            string connections = "  or  ";
            for (int i = 0; i < titleNames.Length; i++)
            {
                if (i == titleNames.Length - 1)
                {
                    string conditions = "TITLE like  @T_key" + i.ToString();
                    parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
                else
                {
                    string conditions = "TITLE like  @T_key" + i.ToString() + connections;
                     parmember = new SqlParameter("@T_key" + i.ToString(), "%" + titleNames[i] + "%");
                    parmembers.Add(parmember);
                    sql += conditions;
                }
            }
            sql += ")";
            DataTable dt = SqlHelper.ExecuteDataTable(sql, SqlHelper.getsqlpsFromlist(parmembers));

            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照模块的ID   和发帖人进行搜索
        /// </summary>
        public List<Model.View_m_TL> getByMIDandUserName(int M_ID, string USER_NETNAME)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_TL WHERE M_ID = @M_ID and USER_NETNAME like @USER_NETNAME";
            SqlParameter parmember = new SqlParameter("@M_ID", M_ID);
            SqlParameter par = new SqlParameter("@USER_NETNAME", "%" + USER_NETNAME + "%");
            DataTable dt = SqlHelper.ExecuteDataTable(sql, par,parmember);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
           //---------------------------------帖子搜索（交叉搜索）结束-------------------------
           //---------------------------------帖子搜索（不进行交叉搜索）----------------------
        /// <summary>
        /// 按照模块的ID的进行搜索
        /// </summary>
        public List<Model.View_m_TL> getByModulessId(int M_ID)
        {
            List<Model.View_m_TL> result = new List<View_m_TL>();
            //sql语句
            string sql = "SELECT * FROM View_m_TL WHERE M_ID = @T_key";
            SqlParameter par = new SqlParameter("@T_key", M_ID);
            DataTable dt = SqlHelper.ExecuteDataTable(sql, par);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照帖子名进行搜索
        /// </summary>
        public List<Model.View_m_TL> getByUserNames(string USER_NETNAME)
        {
            List<Model.View_m_TL> result = new List<View_m_TL>();
            //sql语句
            string sql = "SELECT * FROM View_m_TL WHERE USER_NETNAME like @T_key";
            SqlParameter par = new SqlParameter("@T_key", "%" + USER_NETNAME + "%");
            DataTable dt = SqlHelper.ExecuteDataTable(sql, par);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        /// <summary>
        /// 按照帖子名进行检索
        /// </summary>
        public List<Model.View_m_TL> getByTitleNames(string titleName)
        {
            List<Model.View_m_TL> result = new List<View_m_TL>();
            //sql语句
            string sql = "SELECT * FROM View_m_TL WHERE TITLE like @T_key";
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
        public List<Model.View_m_TL> getByTitleNames_and(string[] titleNames)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_TL WHERE ";
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
        public List<Model.View_m_TL> getByTitleNames_or(string[] titleNames)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_TL WHERE ";
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
        public List<View_m_TL> getByKeyValue(string KeyValuae)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            string sql = "SELECT * FROM View_m_TL WHERE T_key like @T_key";
            SqlParameter par = new SqlParameter("@T_key", "%" + KeyValuae + "%");
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
        public List<View_m_TL> getByKeyValues_and(string[] KeyValues)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_TL WHERE ";
            string connections = "  and  ";
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
        /// <summary>
        /// 按照多关键字进行检索 方式 or
        /// </summary>
        public List<View_m_TL> getByKeyValues_or(string[] KeyValues)
        {
            List<View_m_TL> result = new List<View_m_TL>();
            List<SqlParameter> parmembers = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_TL WHERE ";
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
        //--------------------------------------结束帖子搜索（不进行交叉搜索）--------------------
        //--------------------------帖子搜索结束-----------------------------

		public int addNew(View_m_TL model)
		{
            return AddNew(model);
		}

		public bool delete(int id)
		{
            return Delete(id);
		}

		public bool update(View_m_TL model)
		{
            return Update(model);
		}

		public View_m_TL get(int id)
		{
            return Get(id);
		}
		public IEnumerable<View_m_TL> Listall()
		{
            return ListAll();
		}
	}
}
