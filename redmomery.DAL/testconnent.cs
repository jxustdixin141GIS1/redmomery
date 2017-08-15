using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.SqlTypes;
using redMomery.Conn;
using DML;

namespace redmomery.DAL
{
    public class testconnent
    {
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
        #region 老兵
        public List<DML.老兵> GetMOdel(string Lb_INFOID)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from redmomery.dbo.老兵 ");
            strsql.Append("where LBID=@LBID");
            SqlParameter[] sqlparamet = new SqlParameter[] { new SqlParameter("@LBID", SqlDbType.BigInt) };
            sqlparamet[0].Value = Lb_INFOID;
            string constr = SqlHelper.GetConstr;
            DataTableCollection dcollect = SqlHelper.GetTable(constr, CommandType.Text, strsql.ToString(), sqlparamet);
            List<DML.老兵> result = readerToLB_INfo(dcollect);
            if (result == null) return null;
            return result;
        }
        public List<DML.老兵> readerToLB_INfo(DataTableCollection readers)
        {

            List<DML.老兵> modellist = new List<老兵>();
            for (int i = 0; i < readers.Count; i++)
            {
                DataTable dt = readers[i];
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow reader = dt.Rows[j];
                    老兵 lbinfo = new 老兵();
                    if (reader["ID"] != null)
                    {
                        lbinfo.ID = (int)reader["ID"];
                    }
                    else
                    {
                        lbinfo.ID = -1;//表示没有获取带ID
                    }
                    if (reader["LBID"] != null)
                        lbinfo.LBID = reader["LBID"].ToString();
                    if (reader["LBjob"] != null)
                        lbinfo.LBjob = reader["LBjob"].ToString();
                    if (reader["LBlife"] != null)
                        lbinfo.LBlife = reader["LBlife"].ToString();
                    if (reader["LBname"] != null)
                        lbinfo.LBname = reader["LBname"].ToString();
                    if (reader["LBPhoto"] != null)
                    {
                        lbinfo.LBPhoto = (string)(reader["LBPhoto"].ToString());
                    }
                    if (reader["LBsex"] != null)
                        lbinfo.LBsex = reader["LBsex"].ToString();
                    if (reader["X"] != null && (reader["X"].ToString()) != "")
                        lbinfo.X = float.Parse(reader["X"].ToString());
                    if (reader["Y"] != null && (reader["Y"].ToString()) != "")
                        lbinfo.Y = float.Parse(reader["Y"].ToString());
                    if (reader["designation"] != null)
                        lbinfo.designation = reader["designation"].ToString();
                    if (reader["LBexperience"] != null)
                        lbinfo.LBexperience = reader["LBexperience"].ToString();
                    if (reader["LBdomicile"] != null)
                        lbinfo.LBdomicile = reader["LBdomicile"].ToString();
                    if (reader["LBbirthday"] != null)
                        lbinfo.LBbirthday = reader["LBbirthday"].ToString();
                    modellist.Add(lbinfo);
                }
            }
            if (modellist.Count <= 0) return null;

            return modellist;

        }
        public bool UPdateMOdel(string uptable, ArrayList upfield_value, ArrayList conditions)
        {
            //进行拼接字符串
            ArrayList sqlpar = new ArrayList();
            string sql = "update redmomery.dbo."+uptable+" set ";
            for (int i = 0; i < upfield_value.Count; i++)
            {
                condition ups = (condition)upfield_value[i];
                sql += ups.conditionname + " = @" + ups.conditionname;
                if (i == upfield_value.Count - 1)
                {
                    sql += "";
                }
                else
                {
                    sql += ",";
                }
                SqlParameter sqlp = new SqlParameter("@" + ups.conditionname, ups.type);
                sqlp.Value = ups.conditionvalue;
                sqlpar.Add(sqlp);
            }
            sql += "  where (";

            //条件拼接
            for (int i = 0; i < conditions.Count; i++)
            {
                condition ups = (condition)conditions[i];
                sql += ups.conditionname + " = @" + ups.conditionname;
                if (i == conditions.Count - 1)
                {
                    sql += ")";
                }
                else
                {
                    sql += ",";
                }
                SqlParameter sqlp = new SqlParameter("@" + ups.conditionname, ups.type);
                sqlp.Value = ups.conditionvalue;
                sqlpar.Add(sqlp);
            }

            SqlParameter[] sqlps = new SqlParameter[sqlpar.Count];
            for (int i = 0; i < sqlpar.Count; i++)
            {
                sqlps[i] = (SqlParameter)sqlpar[i];
            }
            try
            {
                int i = SqlHelper.ExecteNonQueryText(sql, sqlps);
                return i > 0 ? true : false;
            }
            catch (Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Message + " ;;" + ex.Source.ToString() + " ;; " + ex.Data.ToString() + "对象：" + ex.StackTrace.ToString() + "::" + ex.TargetSite.ToString());
            }
            return false;
        }
        public bool updatemodel(老兵 l1)
        {
            //开始字段的拼接
            string sql = "update redmomery.dbo.老兵 " +
                         " set LBID=@LBID , LBname=@LBname , LBjob=@LBjob , LBsex=@LBsex , LBbirthday =@LBbirthday , LBdomicile=@LBdomicile , designation=@designation , LBexperience=@LBexperience , LBlife=@LBlife , LBPhoto=@LBPhoto , X=@X , Y=@Y " +
                           " where ID=@ID ";

            SqlParameter[] sqlpars = new SqlParameter[] { 
              new SqlParameter("@ID",l1.ID),
              new SqlParameter("@LBID",l1.LBID),
              new SqlParameter("@designation",l1.designation),
              new SqlParameter("@LBexperience",l1.LBexperience),
              new SqlParameter("@X",l1.X),
              new SqlParameter("@Y",l1.Y),
              new SqlParameter("@LBname" ,l1.LBname),
              new SqlParameter("@LBjob",l1.LBjob),
              new SqlParameter("@LBsex",l1.LBsex),
              new SqlParameter("@LBbirthday",l1.LBbirthday),
              new SqlParameter("@LBdomicile",l1.LBdomicile),
              new SqlParameter("@LBlife",l1.LBlife),
              new SqlParameter("@LBPhoto",l1.LBPhoto)
            };

            try
            {
                int i = SqlHelper.ExecteNonQueryText(sql, sqlpars);
                return i > 0 ? true : false;
            }
            catch (Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Message + " ;;" + ex.Source.ToString() + " ;; " + ex.Data.ToString() + "对象：" + ex.StackTrace.ToString() + "::" + ex.TargetSite.ToString());
            }
            return false;
        }
        public int updatemodels(List<老兵> list1)
        {
            int isexcute = 0;
            //这个就是进行数据的多条数据进行更新  50条以内，若是大于50条就取出50
            string sql = " update redmomery.dbo.老兵 set ";
            string set1 = " LBID= CASE ID ",
                   set2 = " LBname= CASE ID ",
                   set3 = " LBjob = CASE ID ",
                   set4 = " LBsex= CASE ID ",
                   set5 = " LBbirthday= CASE ID ",
                   set6 = " LBdomicile= CASE ID ",
                   set7 = " designation= CASE ID ",
                   set8 = " LBexperience = CASE ID ",
                   set9 = " LBlife = CASE ID ",
                   set10 = " LBPhoto = CASE ID ",
                   set11 = " X = CASE ID ",
                   set12 = " Y = CASE ID ",
                   where1 = " where ID in (";
            //开始进行字符串的拼接
            List<SqlParameter> sqlp = new List<SqlParameter>();
            for (int i = 0; i < list1.Count; i++)
            {
                老兵 l1 = (老兵)list1[i];
                #region  参数声明
                //声明参数为                                              变量声明                                         添加到变量列表中
                string temp1 = "@LBID" + i.ToString(); SqlParameter sqlp1 = new SqlParameter(temp1, l1.LBID); sqlp.Add(sqlp1);
                string temp2 = "@LBname" + i.ToString(); SqlParameter sqlp2 = new SqlParameter(temp2, l1.LBname); sqlp.Add(sqlp2);
                string temp3 = "@LBjob" + i.ToString(); SqlParameter sqlp3 = new SqlParameter(temp3, l1.LBjob); sqlp.Add(sqlp3);
                string temp4 = "@LBsex" + i.ToString(); SqlParameter sqlp4 = new SqlParameter(temp4, l1.LBsex); sqlp.Add(sqlp4);
                string temp5 = "@LBbirthday" + i.ToString(); SqlParameter sqlp5 = new SqlParameter(temp5, l1.LBbirthday); sqlp.Add(sqlp5);
                string temp6 = "@LBdomicile" + i.ToString(); SqlParameter sqlp6 = new SqlParameter(temp6, l1.LBdomicile); sqlp.Add(sqlp6);
                string temp7 = "@designation" + i.ToString(); SqlParameter sqlp7 = new SqlParameter(temp7, l1.designation); sqlp.Add(sqlp7);
                string temp8 = "@LBexperience" + i.ToString(); SqlParameter sqlp8 = new SqlParameter(temp8, l1.LBexperience); sqlp.Add(sqlp8);
                string temp9 = "@LBlife" + i.ToString(); SqlParameter sqlp9 = new SqlParameter(temp9, l1.LBlife); sqlp.Add(sqlp9);
                string temp10 = "@LBPhoto" + i.ToString(); SqlParameter sqlp10 = new SqlParameter(temp10, l1.LBPhoto); sqlp.Add(sqlp10);
                string temp11 = "@X" + i.ToString(); SqlParameter sqlp11 = new SqlParameter(temp11, l1.X); sqlp.Add(sqlp11);
                string temp12 = "@Y" + i.ToString(); SqlParameter sqlp12 = new SqlParameter(temp12, l1.Y); sqlp.Add(sqlp12);
                string wheretemp11 = "@ID" + i.ToString(); SqlParameter sqlpwhere1 = new SqlParameter(wheretemp11, l1.ID); sqlp.Add(sqlpwhere1);
                #endregion
                //下面开始拼接字符串，规则
                #region  案例规则
                /*
                 * UPDATE categories 
                   SET display_order = CASE id 
                   WHEN 1 THEN 3 
                   WHEN 2 THEN 4 
                   WHEN 3 THEN 5 
                   END, 
                   title = CASE id 
                   WHEN 1 THEN 'New Title 1' 
                   WHEN 2 THEN 'New Title 2' 
                   WHEN 3 THEN 'New Title 3' 
                   END 
                   WHERE id IN (1,2,3) 
                 * */
                #endregion
                //具体拼接方式
                #region  代码拼接区域
                set1 += " WHEN  " + wheretemp11 + " THEN " + temp1 + " ";
                set2 += " WHEN  " + wheretemp11 + " THEN " + temp2 + " ";
                set3 += " WHEN  " + wheretemp11 + " THEN " + temp3 + " ";
                set4 += " WHEN  " + wheretemp11 + " THEN " + temp4 + " ";
                set5 += " WHEN  " + wheretemp11 + " THEN " + temp5 + " ";
                set6 += " WHEN  " + wheretemp11 + " THEN " + temp6 + " ";
                set7 += " WHEN  " + wheretemp11 + " THEN " + temp7 + " ";
                set8 += " WHEN  " + wheretemp11 + " THEN " + temp8 + " ";
                set9 += " WHEN  " + wheretemp11 + " THEN " + temp9 + " ";
                set10 += " WHEN  " + wheretemp11 + " THEN " + temp10 + " ";
                set11 += " WHEN  " + wheretemp11 + " THEN " + temp11 + " ";
                set12 += " WHEN  " + wheretemp11 + " THEN " + temp12 + " ";
                if (i % 50 == 0)
                {
                    where1 += " " + wheretemp11 + " ";
                }
                else
                {
                    where1 += " , " + wheretemp11 + " ";
                }

                #endregion
                if ((i + 1) % 50 == 0)
                {
                    #region  开始执行代码
                    set1 += " END ,";
                    set2 += " END ,";
                    set3 += " END ,";
                    set4 += " END ,";
                    set5 += " END ,";
                    set6 += " END ,";
                    set7 += " END ,";
                    set8 += " END ,";
                    set9 += " END ,";
                    set10 += " END ,";
                    set11 += " END ,";
                    set12 += " END ";
                    where1 += " )";
                    sql += set1 + set2 + set3 + set4 + set5 + set6 + set7 + set8 + set9 + set10 + set11 + set12 + where1;
                    //下面就是执行的代码
                    try
                    {
                        SqlParameter[] sqlss = getsqlpsFromlist(sqlp);
                        int count = SqlHelper.ExecteNonQueryText(sql, sqlss);
                        isexcute += count;
                        sqlp.Clear();//清空参数列表
                        sql = " update redmomery.dbo.老兵 set ";
                        set1 = " LBID= CASE ID ";
                        set2 = " LBname= CASE ID ";
                        set3 = " LBjob = CASE ID ";
                        set4 = " LBsex= CASE ID ";
                        set5 = " LBbirthday= CASE ID ";
                        set6 = " LBdomicile= CASE ID ";
                        set7 = " designation= CASE ID ";
                        set8 = " LBexperience = CASE ID ";
                        set9 = " LBlife = CASE ID ";
                        set10 = " LBPhoto = CASE ID ";
                        set11 = " X = CASE ID ";
                        set12 = " Y = CASE ID ";
                        where1 = " where ID in (";
                    }
                    catch (Exception ex)
                    {
                        redmomery.command.createlog.createlogs(ex.Message + " ;;" + ex.Source.ToString() + " ;; " + ex.Data.ToString() + "对象：" + ex.StackTrace.ToString() + "::" + ex.TargetSite.ToString());
                    }
                    #endregion
                }
            }
            if (list1.Count % 50.0 > 0)
            {
                #region  开始执行代码
                set1 += " END ,";
                set2 += " END ,";
                set3 += " END ,";
                set4 += " END ,";
                set5 += " END ,";
                set6 += " END ,";
                set7 += " END ,";
                set8 += " END ,";
                set9 += " END ,";
                set10 += " END ,";
                set11 += " END ,";
                set12 += " END ";
                where1 = " )";
                sql += set1 + set2 + set3 + set3 + set4 + set5 + set6 + set7 + set8 + set9 + set10 + set11 + set12 + where1;
                //下面就是执行的代码
                try
                {
                    int count = SqlHelper.ExecteNonQueryText(sql, getsqlpsFromlist(sqlp));
                    isexcute += count;
                    sqlp = new List<SqlParameter>();
                }
                catch (Exception ex)
                {
                    redmomery.command.createlog.createlogs(ex.Message + "  " + ex.Source.ToString() + "  " + ex.Data.ToString() + "对象：" + ex.StackTrace.ToString());

                }
                #endregion
            }
            return isexcute;
        }

        #endregion
        #region  LB_INFO
        public int InsertModels(List<LB_INFO> list1)
        {
            //开始
            int isexcute = 0;
            string sql = "insert into redmomery.dbo.LB_INFO  (LB_ID,LB_NAME,LB_SEX ,LB_JOB,LB_BIRTHDAY,LB_DESIGNATION ,LB_ADDRESS ,LB_EXPERIENCE,LB_LIFE,LB_LOCX,LB_LOCY ,LB_IMGPTH) values ";
            List<SqlParameter> sqlp = new List<SqlParameter>();
            string temp1 = "";
            string temp2 = "";
            string temp4 = "";
            string temp3 = "";
            string temp5 = "";
            string temp7 = "";
            string temp6 = "";
            string temp8 = "";
            string temp9 = "";
            string temp11 = "";
            string temp12 = "";
            string temp10 = "";
            for (int i = 0; i < list1.Count; i++)
            {
                #region 参数
                LB_INFO l1 = list1[i];
                temp1 = "@LBID" + i.ToString(); SqlParameter sqlp1 = new SqlParameter(temp1, l1.LB_ID); sqlp.Add(sqlp1);
                temp2 = "@LBname" + i.ToString(); SqlParameter sqlp2 = new SqlParameter(temp2, l1.LB_NAME); sqlp.Add(sqlp2);
                temp3 = "@LBjob" + i.ToString(); SqlParameter sqlp3 = new SqlParameter(temp3, l1.LB_JOB); sqlp.Add(sqlp3);
                temp4 = "@LBsex" + i.ToString(); SqlParameter sqlp4 = new SqlParameter(temp4, l1.LB_SEX); sqlp.Add(sqlp4);
                temp5 = "@LBbirthday" + i.ToString();
                SqlParameter sqlp5 = new SqlParameter(temp5, SqlDbType.DateTime);
                sqlp5.Value = l1.LB_BIRTHDAY.Value;
                sqlp.Add(sqlp5);
                temp6 = "@LBdomicile" + i.ToString(); SqlParameter sqlp6 = new SqlParameter(temp6, l1.LB_ADDRESS); sqlp.Add(sqlp6);
                temp7 = "@designation" + i.ToString(); SqlParameter sqlp7 = new SqlParameter(temp7, l1.LB_DESIGNATION); sqlp.Add(sqlp7);
                temp8 = "@LBexperience" + i.ToString(); SqlParameter sqlp8 = new SqlParameter(temp8, l1.LB_EXPERIENCE); sqlp.Add(sqlp8);
                temp9 = "@LBlife" + i.ToString(); SqlParameter sqlp9 = new SqlParameter(temp9, l1.LB_LIFE); sqlp.Add(sqlp9);
                temp10 = "@LBPhoto" + i.ToString(); SqlParameter sqlp10 = new SqlParameter(temp10, l1.LB_IMGPTH); sqlp.Add(sqlp10);
                temp11 = "@X" + i.ToString(); SqlParameter sqlp11 = new SqlParameter(temp11, l1.LB_LOCX); sqlp.Add(sqlp11);
                temp12 = "@Y" + i.ToString(); SqlParameter sqlp12 = new SqlParameter(temp12, l1.LB_LOCY); sqlp.Add(sqlp12);
                #endregion
                #region 案例
                /*
                 * insert into persons 
                    (id_p, lastname , firstName, city )
                    values
                    (200,'haha' , 'deng' , 'shenzhen'),
                    (201,'haha2' , 'deng' , 'GD'),
                    (202,'haha3' , 'deng' , 'Beijing');
                 * */
                #endregion

                if ((i + 1) % 50 == 0)
                {
                    #region 拼接
                    sql += "(" + temp1 + "," +
                               temp2 + "," +
                               temp4 + "," +
                               temp3 + "," +
                               temp5 + "," +
                               temp7 + "," +
                               temp6 + "," +
                               temp8 + "," +
                               temp9 + "," +
                               temp11 + "," +
                               temp12 + "," +
                               temp10 + ")";
                    #endregion
                    #region 执行

                    try
                    {
                        SqlParameter[] sqlss = getsqlpsFromlist(sqlp);
                        int count = SqlHelper.ExecteNonQueryText(sql, sqlss);
                        isexcute += count;
                        sqlp.Clear();//清空参数列表
                        sql = "insert into redmomery.dbo.LB_INFO  (LB_ID,LB_NAME,LB_SEX ,LB_JOB,LB_BIRTHDAY,LB_DESIGNATION ,LB_ADDRESS ,LB_EXPERIENCE,LB_LIFE,LB_LOCX,LB_LOCY ,LB_IMGPTH) values ";
                    }

                    catch (Exception ex)
                    {
                        redmomery.command.createlog.createlogs(ex.Message + " ;;" + ex.Source.ToString() + " ;; " + ex.Data.ToString() + "对象：" + ex.StackTrace.ToString() + "::" + ex.TargetSite.ToString());
                    }
                    #endregion
                }
                else
                {
                    #region
                    if (i != list1.Count - 1)
                    {
                        sql += "(" + temp1 + "," +
                                     temp2 + "," +
                                     temp4 + "," +
                                     temp3 + "," +
                                     temp5 + "," +
                                     temp7 + "," +
                                     temp6 + "," +
                                     temp8 + "," +
                                     temp9 + "," +
                                     temp11 + "," +
                                     temp12 + "," +
                                     temp10 + ") , ";
                    }
                    else
                    {
                        sql += "(" + temp1 + "," +
                                     temp2 + "," +
                                     temp4 + "," +
                                     temp3 + "," +
                                     temp5 + "," +
                                     temp7 + "," +
                                     temp6 + "," +
                                     temp8 + "," +
                                     temp9 + "," +
                                     temp11 + "," +
                                     temp12 + "," +
                                     temp10 + ")";
                        #region
                        try
                        {
                            SqlParameter[] sqlss = getsqlpsFromlist(sqlp);
                            int count = SqlHelper.ExecteNonQueryText(sql, sqlss);
                            isexcute += count;
                            sqlp.Clear();//清空参数列表
                            sql = "insert into redmomery.dbo.LB_INFO  (LB_ID,LB_NAME,LB_SEX ,LB_JOB,LB_BIRTHDAY,LB_DESIGNATION ,LB_ADDRESS ,LB_EXPERIENCE,LB_LIFE,LB_LOCX,LB_LOCY ,LB_IMGPTH) values ";
                        }

                        catch (Exception ex)
                        {
                            redmomery.command.createlog.createlogs(ex.Message + " ;;" + ex.Source.ToString() + " ;; " + ex.Data.ToString() + "对象：" + ex.StackTrace.ToString() + "::" + ex.TargetSite.ToString());
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            return isexcute;
        }

        public bool InsertModel(LB_INFO l1)
        {
            string sql = "insert into redmomery.dbo.LB_INFO  (LB_NAME,LB_SEX ,LB_JOB,LB_BIRTHDAY,LB_DESIGNATION ,LB_ADDRESS ,LB_EXPERIENCE,LB_LIFE,LB_LOCX,LB_LOCY ,LB_IMGPTH) values " +
                "(@LB_NAME,@LB_SEX ,@LB_JOB,@LB_BIRTHDAY,@LB_DESIGNATION ,@LB_ADDRESS ,@LB_EXPERIENCE,@LB_LIFE,@LB_LOCX,@LB_LOCY ,@LB_IMGPTH)";
            List<SqlParameter> sqlp = new List<SqlParameter>();
          //  string temp1 = "@LB_ID"; SqlParameter sqlp1 = new SqlParameter(temp1, l1.LB_ID); sqlp.Add(sqlp1);
            string temp2 = "@LB_NAME"; SqlParameter sqlp2 = new SqlParameter(temp2, l1.LB_NAME); sqlp.Add(sqlp2);
            string temp3 = "@LB_SEX"; SqlParameter sqlp4 = new SqlParameter(temp3, l1.LB_SEX); sqlp.Add(sqlp4);
            string temp4 = "@LB_JOB"; SqlParameter sqlp3 = new SqlParameter(temp4, l1.LB_JOB); sqlp.Add(sqlp3);
            string temp5 = "@LB_BIRTHDAY";
            SqlParameter sqlp5 = new SqlParameter(temp5, SqlDbType.DateTime);
            sqlp5.Value = l1.LB_BIRTHDAY.Value;
            sqlp.Add(sqlp5);
            string temp6 = "@LB_ADDRESS"; SqlParameter sqlp6 = new SqlParameter(temp6, l1.LB_ADDRESS); sqlp.Add(sqlp6);
            string temp7 = "@LB_DESIGNATION"; SqlParameter sqlp7 = new SqlParameter(temp7, l1.LB_DESIGNATION); sqlp.Add(sqlp7);
            string temp8 = "@LB_EXPERIENCE"; SqlParameter sqlp8 = new SqlParameter(temp8, l1.LB_EXPERIENCE); sqlp.Add(sqlp8);
            string temp9 = "@LB_LIFE"; SqlParameter sqlp9 = new SqlParameter(temp9, l1.LB_LIFE); sqlp.Add(sqlp9);
            string temp10 = "@LB_IMGPTH"; SqlParameter sqlp10 = new SqlParameter(temp10, l1.LB_IMGPTH); sqlp.Add(sqlp10);
            string temp11 = "@LB_LOCX"; SqlParameter sqlp11 = new SqlParameter(temp11, l1.LB_LOCX); sqlp.Add(sqlp11);
            string temp12 = "@LB_LOCY"; SqlParameter sqlp12 = new SqlParameter(temp12, l1.LB_LOCY); sqlp.Add(sqlp12);

            #region
            try
            {
                SqlParameter[] sqlss = getsqlpsFromlist(sqlp);
                int count = SqlHelper.ExecteNonQueryText(sql, sqlss);
                return true;
            }

            catch (Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Message + " ;;" + ex.Source.ToString() + " ;; " + ex.Data.ToString() + "对象：" + ex.StackTrace.ToString() + "::" + ex.TargetSite.ToString());
            }
            #endregion


            return false;
        }
        #endregion
    }
}
