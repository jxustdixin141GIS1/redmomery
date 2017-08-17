using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using redmomery.Model;

namespace redmomery.DAL
{
     public 	partial class BBSTITLE_TABLEDAL
     {
         public BBSTITLE_TABLE getByMD5(string Md5)
         {
             DataTable dt = SqlHelper.ExecuteDataTable("SELECT * FROM BBSTITLE_TABLE WHERE MD5=@ID", new SqlParameter("@ID", Md5));
             if (dt.Rows.Count > 1)
             {
                 throw new Exception("more than 1 row was found");
             }
             if (dt.Rows.Count <= 0)
             {
                 return null;
             }
             DataRow row = dt.Rows[0];
             BBSTITLE_TABLE model = ToModel(row);
             return model;
         
         }
         //-------------------------自动生成
         #region 自动生成
         public int addNew(BBSTITLE_TABLE model)
         {
             return AddNew(model);
         }

         public bool delete(int id)
         {
             return Delete(id);
         }

         public bool update(BBSTITLE_TABLE model)
         {
             return Update(model);
         }

         public BBSTITLE_TABLE get(int id)
         {
             return Get(id);
         }

         public IEnumerable<BBSTITLE_TABLE> Listall()
         {
             return ListAll();
         }
       #endregion
         //更新回复数
        public  bool add_comNUM(int T_ID)
        {
            string sql = "upate BBSTITLE_TABLE set N_RESPONSE=N_RESPONSE+1 where ID=@T_ID";
            int rows = SqlHelper.ExecuteNonQuery(sql,new SqlParameter("@T_ID",T_ID));
            return rows > 0;
        }
         //更新点赞数
        public  bool add_Y(int T_ID)
        {
            string sql = "upate BBSTITLE_TABLE set N_YES=N_YES+1 where ID=@T_ID";
            int rows = SqlHelper.ExecuteNonQuery(sql, new SqlParameter("@T_ID", T_ID));
            return rows > 0;
        }

         /// <summary>
         /// //////////////////////////////////////////////////////这个清除有待于处理和扩充，现在暂时就是这样
         /// </summary>
         /// <param name="id"></param>
         /// <returns></returns>
        public bool Clear(int id)
        {
            int rows = SqlHelper.ExecuteNonQuery("DELETE FROM BBSTITLE_TABLE WHERE ID = @id and is_pass=0", new SqlParameter("@id", id));
            return rows > 0;
        }
	}
}
