using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
namespace redmomery.DAL
{
	public  partial class SqlHelper
	{
        public static readonly string connstr =System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString().Trim();
        public static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["conStr"].ToString().Trim();
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());


        public  static SqlParameter[] getsqlpsFromlist(List<SqlParameter> listparsql)
        {
            SqlParameter[] sqls = new SqlParameter[listparsql.Count];
            for (int i = 0; i < sqls.Length; i++)
            {
                sqls[i] = (SqlParameter)listparsql[i];
            }
            return sqls;
        }
        #region//ExecteNonQuery����

        /// <summary>
        ///ִ��һ������Ҫ����ֵ��SqlCommand���ͨ��ָ��ר�õ������ַ�����
        /// ʹ�ò���������ʽ�ṩ�����б� 
        /// </summary>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQuery(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //ͨ��PrePareCommand����������������뵽SqlCommand�Ĳ���������
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                //���SqlCommand�еĲ����б�
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        ///ִ��һ������Ҫ����ֵ��SqlCommand���ͨ��ָ��ר�õ������ַ�����
        /// ʹ�ò���������ʽ�ṩ�����б� 
        /// </summary>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQuery(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(connectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        ///�洢����ר��
        /// </summary>
        /// <param name="cmdText">�洢���̵�����</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQueryProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        ///Sql���ר��
        /// </summary>
        /// <param name="cmdText">T_Sql���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ����ֵ��ʾ��SqlCommand����ִ�к�Ӱ�������</returns>
        public static int ExecteNonQueryText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecteNonQuery(CommandType.Text, cmdText, commandParameters);
        }

        #endregion
        #region//GetTable����

        /// <summary>
        /// ִ��һ�����ؽ������SqlCommand��ͨ��һ���Ѿ����ڵ����ݿ�����
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <param name="connecttionString">һ�����е����ݿ�����</param>
        /// <param name="cmdTye">SqlCommand��������</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTable(string connecttionString, CommandType cmdTye, string cmdText, SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(connecttionString))
            {
                PrepareCommand(cmd, conn, null, cmdTye, cmdText, commandParameters);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
            }
            DataTableCollection table = ds.Tables;
            return table;
        }

        /// <summary>
        /// ִ��һ�����ؽ������SqlCommand��ͨ��һ���Ѿ����ڵ����ݿ�����
        /// ʹ�ò��������ṩ����
        /// </summary>
        /// <param name="cmdTye">SqlCommand��������</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTable(CommandType cmdTye, string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(cmdTye, cmdText, commandParameters);
        }


        /// <summary>
        /// �洢����ר��
        /// </summary>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTableProducts(string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// Sql���ר��
        /// </summary>
        /// <param name="cmdText"> T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������(DataTableCollection)��ʾ��ѯ�õ������ݼ�</returns>
        public static DataTableCollection GetTableText(string cmdText, SqlParameter[] commandParameters)
        {
            return GetTable(CommandType.Text, cmdText, commandParameters);
        }
        #endregion


        /// <summary>
        /// Ϊִ������׼������
        /// </summary>
        /// <param name="cmd">SqlCommand ����</param>
        /// <param name="conn">�Ѿ����ڵ����ݿ�����</param>
        /// <param name="trans">���ݿ����ﴦ��</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">Command text��T-SQL��� ���� Select * from Products</param>
        /// <param name="cmdParms">���ش�����������</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            //�ж����ݿ�����״̬
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            //�ж��Ƿ���Ҫ���ﴦ��
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static SqlDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connectionString);
            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        #region//ExecuteDataSet����

        /// <summary>
        /// return a dataset
        /// </summary>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                return ds;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// ����һ��DataSet
        /// </summary>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// ����һ��DataSet
        /// </summary>
        /// <param name="cmdText">�洢���̵�����</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// ����һ��DataSet
        /// </summary>

        /// <param name="cmdText">T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>return a dataset</returns>
        public static DataSet ExecuteDataSetText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataSet(connectionString, CommandType.Text, cmdText, commandParameters);
        }


        public static DataView ExecuteDataSet(string connectionString, string sortExpression, string direction, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                da.SelectCommand = cmd;
                da.Fill(ds);
                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = sortExpression + " " + direction;
                return dv;
            }
            catch
            {
                conn.Close();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion


        #region // ExecuteScalar����


        /// <summary>
        /// ���ص�һ�еĵ�һ��
        /// </summary>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������</returns>
        public static object ExecuteScalar(CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, cmdType, cmdText, commandParameters);
        }

        /// <summary>
        /// ���ص�һ�еĵ�һ�д洢����ר��
        /// </summary>
        /// <param name="cmdText">�洢���̵�����</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������</returns>
        public static object ExecuteScalarProducts(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, CommandType.StoredProcedure, cmdText, commandParameters);
        }

        /// <summary>
        /// ���ص�һ�еĵ�һ��Sql���ר��
        /// </summary>
        /// <param name="cmdText">�� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>����һ������</returns>
        public static object ExecuteScalarText(string cmdText, params SqlParameter[] commandParameters)
        {
            return ExecuteScalar(SqlHelper.connectionString, CommandType.Text, cmdText, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a SqlCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">һ����Ч�����ݿ������ַ���</param>
        /// <param name="cmdType">SqlCommand�������� (�洢���̣� T-SQL��䣬 �ȵȡ�)</param>
        /// <param name="cmdText">�洢���̵����ֻ��� T-SQL ���</param>
        /// <param name="commandParameters">��������ʽ�ṩSqlCommand�������õ��Ĳ����б�</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(SqlConnection connection, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        #endregion


        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params SqlParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];
            if (cachedParms == null)
                return null;
            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();
            return clonedParms;
        }


        /// <summary>
        /// ����Ƿ����
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <returns>bool���</returns>
        public static bool Exists(string strSql)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, strSql, null));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// ����Ƿ����
        /// </summary>
        /// <param name="strSql">Sql���</param>
        /// <param name="cmdParms">����</param>
        /// <returns>bool���</returns>
        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            int cmdresult = Convert.ToInt32(ExecuteScalar(connectionString, CommandType.Text, strSql, cmdParms));
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
	}
}
