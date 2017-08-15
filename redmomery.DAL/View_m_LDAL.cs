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
        #region �Զ�����
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
        /**************ǰ��Ϊ�����Զ����ɵĴ���************************
         * 
         * ���ڼ򵥵���ɾ�Ĳ���룬�Ǽ򵥵ģ������ҾͲ��ò��ù��߽����Զ�����
         * 
         * ************�������Լ�������ƵĴ���***********************
         * */
        /// <summary>
        /// ͨ���������в���
        /// </summary>
        /// <param name="U_ID">����ID</param>
        /// <returns></returns>
        public List<View_m_L> ListByU_ID(int[] U_ID)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            for (int i = 0; i < U_ID.Length; i++)
            {
                //����������ϳɲ�ѯ���
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
        /// �������ӱ�����в���
        /// </summary>
        /// <param name="Title">����ؼ���</param>
        /// <returns></returns>
        public List<View_m_L> ListByB_Title_or(string[] Title)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            string connection = "  OR  ";
            for (int i = 0; i < Title.Length; i++)
            {
                //����������ϳɲ�ѯ���
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
                //����������ϳɲ�ѯ���
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
        /// ����ģ��ؼ��ֽ��м���������ģʽΪOR
        /// </summary>
        /// <param name="Title">�ؼ�������</param>
        /// <returns></returns>
        public List<View_m_L> ListByM_Key_or(string[] Title)
        {
            List<View_m_L> list = new List<View_m_L>();
            List<SqlParameter> sqlps = new List<SqlParameter>();
            string sql = "SELECT * FROM View_m_L where (";
            string connection = "  OR  ";
            for (int i = 0; i < Title.Length; i++)
            {
                //����������ϳɲ�ѯ���
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
        /// ����ģ��ؼ��ֽ��м���������ģʽΪAnd
        /// </summary>
        /// <param name="Title">�ؼ�������</param>
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
                //����������ϳɲ�ѯ���
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
        /// ����ģ�鴴���˵��������д���
        /// </summary>
        /// <param name="USER_NETNAME"></param>
        /// <returns></returns>
        public List<View_m_L> getByUserNames(string USER_NETNAME)
        {
            List<Model.View_m_L> result = new List<View_m_L>();
            //sql���
            string sql = "SELECT * FROM View_m_L WHERE USER_NETNAME like @T_key";
            SqlParameter par = new SqlParameter("@T_key", "%" + USER_NETNAME + "%");
            DataTable dt = SqlHelper.ExecuteDataTable(sql, par);
            foreach (DataRow row in dt.Rows)
            {
                result.Add(ToModel(row));
            }
            return result;
        }
        #region  ���õ������Ǵ���
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
