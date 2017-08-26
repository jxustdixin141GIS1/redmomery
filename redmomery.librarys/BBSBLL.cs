using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using redmomery.Model;
using redmomery.DAL;
using System.Data.Spatial;
using System.Data.SqlTypes;
namespace redmomery.librarys
{
    public abstract partial class BBSBLL
    {
        /// <summary>
        /// 提交回复评论功能
        /// </summary>
        /// <param name="M_ID">模块ID</param>
        /// <param name="T_ID">帖子的ID</param>
        /// <param name="C_ID">评论的ID</param>
        /// <param name="U_ID">用户ID</param>
        /// <param name="comment">内容</param>
        /// <returns></returns>
        public static bool postcommentofComment(int M_ID, int T_ID, int C_ID, int U_ID, string comment)
        {
            //这个业务层类主要为是进行论坛的开发
            //提交评论，更新回复评论表
            CCBBS_TABLE c1 = new CCBBS_TABLE();
            c1.C_ID = C_ID;
            c1.CC_TIME = DateTime.Now;
            c1.Context = comment;
            c1.U_ID = U_ID;
            object dal = new CCBBS_TABLEDAL();
            int isadd = ((CCBBS_TABLEDAL)dal).AddNew(c1);
            //开始更新当前帖子的回复数量
            dal = new CTBBS_TABLEDAL();
            bool isad = ((CTBBS_TABLEDAL)dal).addC_N(c1.C_ID);
            if (isadd > 0 && isad)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 主题帖的回复
        /// </summary>
        /// <param name="M_ID">模块ID</param>
        /// <param name="T_ID">帖子ID</param>
        /// <param name="U_ID">用户ID</param>
        /// <param name="comment">回复内容</param>
        /// <returns></returns>
        public static bool postcommentoftitle(int M_ID, int T_ID, int U_ID, string comment)
        {
            //当前评论对象主体
            CTBBS_TABLE c1 = new CTBBS_TABLE();
            c1.T_ID = T_ID;
            c1.U_ID = U_ID;
            c1.Context = comment;
            object dal = new CTBBS_TABLEDAL();
            int count = ((CTBBS_TABLEDAL)dal).AddNew(c1);
            //开始更新当前帖子的评论数 title_reponse
            dal = new BBSTITLE_TABLEDAL();
            if (count > 0 && ((BBSTITLE_TABLEDAL)dal).add_comNUM(T_ID))
                return true;
            return false;
        }
        public static int PostCommentByTID(Model.CTBBS_TABLE CT)
        {
            CTBBS_TABLEDAL dal = new CTBBS_TABLEDAL();
            int CID = dal.addNew(CT);
            return CID;
        }
        /// <summary>
        /// 发布帖子
        /// </summary>
        /// <param name="M_ID">模块ID</param>
        /// <param name="U_ID">发布人ID</param>
        /// <param name="comment">内容</param>
        /// <param name="titlename">帖子的名称</param>
        /// <param name="authonrity">帖子的权限</param>
        /// <returns></returns>
        public static bool createtile(int M_ID, int U_ID, string comment, string titlename, int authonrity = 10)
        {
            //提交标题贴中心问题
            BBSTITLE_TABLE t1 = new BBSTITLE_TABLE();
            t1.M_ID = M_ID;//模块ID
            t1.U_ID = U_ID;//用户ID
            t1.TITLE = titlename;//贴子标题
            t1.F_TIME = DateTime.Now;//发帖时间
            t1.Context = comment;//帖子内容
            t1.N_RESPONSE = 0;//回复数
            t1.N_YES = 0;//点赞
            t1.is_pass = 1;//是否审核通过
            t1.pass_TIME = DateTime.ParseExact("9999/12/31", "yyyy/MM/dd", null);//当前的贴子有效时间
            t1.Authonrity = 10;//10表示正常普通贴,可以后期进行更改，这个位置直接关系到是不是置顶贴
            object dal = new BBSTITLE_TABLEDAL();
            ((BBSTITLE_TABLEDAL)dal).AddNew(t1);
            //开始增加对应模块的标题数
            dal = new moduleBBS_TableDAL();
            ((moduleBBS_TableDAL)dal).add_N_T(M_ID);
            return false;
        }
        /// <summary>
        /// 创建模块
        /// </summary>
        /// <param name="U_ID">创建人ID</param>
        /// <param name="title">模块主题名</param>
        /// <param name="description">模块描述</param>
        /// <returns></returns>
        public static bool CreateModuless(int U_ID, string title, string description)
        {
            //开始创建一个论坛主题模块
            //注意这里面需要注意的是，所有的模块的ID必须是为正数
            moduleBBS_Table newm1 = new moduleBBS_Table();
            newm1.U_ID = U_ID;
            newm1.N_TITLE_T = 0;
            newm1.B_describe = description;
            newm1.C_TIME = DateTime.Now;
            newm1.U_TIME = DateTime.Now;
            return false;
        }

        #region  论坛展示功能
        #region  论坛搜索功能



        //----------------------模块列表进行搜索--------------
        /// <summary>
        /// 获取模块的展示页面,主要页面展示
        /// </summary>
        /// <returns></returns>


        public static List<Model.View_m_L> getALLList_moduless()
        {
            List<Model.View_m_L> result = new List<View_m_L>();
            //开始获取页面列表
            object dal = new View_m_LDAL();
            //获取全部的列表
            List<Model.View_m_L> temp = ((View_m_LDAL)dal).ListAll() as List<Model.View_m_L>;
            //进行排序，目前是第一个版本，需要在后期进行处理
            result = modelrecomment.recommendModulessList(temp);
            return result;
        }
        //完成按照帖子名进行查找
        public static List<Model.View_m_L> getM_LByName(string name)
        {
            List<Model.View_m_L> result = new List<View_m_L>();
            //开始获取页面列表
            object dal = new View_m_LDAL();
            //获取全部的列表
            List<Model.View_m_L> temp = ((View_m_LDAL)dal).ListAll() as List<Model.View_m_L>;
            //进行排序，目前是第一个版本，需要在后期进行处理,这是第二阶段的功能
            result = modelrecomment.recommendModulessList(temp);
            return result;
        }

        //--------------------模块列表搜索结束-----------------
        #endregion
        #endregion
        #region  LB_INFO故事
        //---------------------------------LB_INFO故事论坛的故事展现--------------------------
        /// <summary>
        /// 按照帖子标题ID获取
        /// </summary>
        /// <param name="T_ID"></param>
        /// <returns></returns>
        public static List<LB_INFO> show_LB_INFO_Story(int T_ID)
        {
            List<LB_INFO> lb = new List<LB_INFO>();
            object dal = new DAL.LB_INFODAL();
            lb = ((DAL.LB_INFODAL)dal).getByTitle(T_ID);
            return lb;
        }
        //--------------------------创建LB_INFO论坛----------------------
        //-----------------------如何完成帖子和老兵信息的关联-------------
        /// <summary>
        /// 这个是根据老兵的故事信息自动生成对应的帖子,并将帖子与之关联
        /// </summary>
        /// <param name="lb">老兵对象</param>
        /// <param name="U_ID">创建老兵用户ID</param>
        public static LB_INFO addlBtotitle(LB_INFO lb, int U_ID)
        {
            //--------------------------------------根据老兵的信息直接创建一个合适帖子-----------------------------
            BBSTITLE_TABLEDAL titledal = new BBSTITLE_TABLEDAL();
            //创建一个帖子
            int M_Id = -1;//这是老兵模块的初始ID值
            BBSTITLE_TABLE newt1 = new BBSTITLE_TABLE();
            newt1.M_ID = M_Id;
            newt1.U_ID = U_ID;
            newt1.T_key = lb.LBname;
            newt1.TITLE = lb.LBname;
            newt1.Context = lb.LBexperience;
            newt1.is_pass = 1;
            newt1.N_RESPONSE = 0;
            newt1.N_YES = 0;
            newt1.pass_TIME = DateTime.ParseExact("9999/12/31", "yyyy/MM/dd", null);
            newt1.F_TIME = DateTime.Now;
            newt1.Authonrity = 10;
            newt1.MD5 = redmomery.Common.MD5Helper.EncryptString(newt1.Authonrity + newt1.F_TIME.ToString() + newt1.M_ID.ToString() + newt1.Context + newt1.TITLE);
            //创建管理对应的帖子完毕
            try
            {
                int count = titledal.addNew(newt1);
                redmomery.command.createlog.createlogs(count.ToString());
                newt1 = titledal.getByMD5(newt1.MD5);
                lb.T_ID = newt1.ID;
                redmomery.command.createlog.createlogs(count.ToString() + ":" + lb.T_ID.ToString());
            }
            catch (Exception ex)
            {
                redmomery.command.createlog.createlogs(ex.Message + "\n\r" + ex.StackTrace.ToString());
            }
            return lb;
        }
        /// <summary>
        /// 根据信息创建老兵对象
        /// </summary>
        /// <returns></returns>
        private static LB_INFO CreateLBInFo(string LBName, string LBjob, string LBsex, string Birthday, string Domicicile, string designation, string lbExperirence, string lblife, string LBPoto, float? x, float? y)
        {
            //开始根据这些信息创建字段
            LB_INFO lb = new LB_INFO();
            lb.LBname = LBName;
            lb.LBjob = LBjob;
            lb.LBsex = LBsex;
            lb.LBbirthday = Birthday;
            lb.LBdomicile = Domicicile;
            lb.designation = designation;
            lb.LBexperience = lbExperirence;
            lb.LBlife = lblife;
            lb.LBPhoto = LBPoto;
            lb.X = x;
            lb.Y = y;
            if ((x.ToString() == "" && y.ToString() == "") || (x < 0 && y < 0))
            {
                lb = getGecoding(lb);
            }
            //创建几何对象
            if ((lb.X.ToString() != "" && lb.X.ToString() != "") && ((float)(lb.X) >= 0 && (float)(lb.X) >= 0))
            {
                string locationpoint = "POINT(" + lb.X.ToString() + " " + lb.Y.ToString() + ")";
                string localpoint =  locationpoint;//SqlGeography.STPointFromText(pars, 4326);
                lb.Location = localpoint;
            }
            return lb;
        }

        /// <summary>
        /// 创建lb与帖子的关连方法
        /// </summary>
        /// <param name="U_ID"></param>
        /// <param name="LBName"></param>
        /// <param name="LBjob"></param>
        /// <param name="LBsex"></param>
        /// <param name="Birthday"></param>
        /// <param name="Domicicile"></param>
        /// <param name="designation"></param>
        /// <param name="lbExperirence"></param>
        /// <param name="lblife"></param>
        /// <param name="LBPoto"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static LB_INFO CreateLBandTitle(int U_ID, string LBName, string LBjob, string LBsex, string Birthday, string Domicicile, string designation, string lbExperirence, string lblife, string LBPoto, float? x = null, float? y = null)
        {
            LB_INFO LB = new LB_INFO();

            LB = CreateLBInFo(LBName, LBjob, LBsex, Birthday, Domicicile, designation, lbExperirence, lblife, LBPoto, x, y);

            LB = addlBtotitle(LB, U_ID);

            if (LB.T_ID == 0)
            {
                redmomery.command.createlog.createlogs("创建帖子失败");
            }
            else
            {
                LB_INFODAL dal = new LB_INFODAL();
                try
                {
                    int ID = dal.addNew(LB);
                    LB = dal.get(ID);
                    staticbydata.AddCityLBByLB(LB);
                }
                catch (Exception ex)
                {
                    BBSTITLE_TABLEDAL dals = new BBSTITLE_TABLEDAL();
                    staticbydata.DeleteCityLBByLB(LB);
                    dals.delete(LB.ID);
                    LB = null;
                }
            }
            return LB;
        }
        //--------------------------------LB_INFO故事论坛评论加载-------------------------------------------------

        //----------------------------------------根据老兵的ID，获取帖子，评论，评论的回复-------------------------
        public static LB_INFO getLB_INFOByID(int ID)
        {
            LB_INFODAL dal = new LB_INFODAL();
            return dal.get(ID);
        }
        public static BBSTITLE_TABLE getTitleID(int Id)
        {
            BBSTITLE_TABLEDAL dal = new BBSTITLE_TABLEDAL();
            return dal.get(Id);
        }
        public static View_T_U getTitleViewID(int T_ID)
        {
            View_T_UDAL dal = new View_T_UDAL();
            return dal.Get(T_ID);
        }
        public static List<CTBBS_TABLE> getConmentByTitleID(int T_ID)
        {
            CTBBS_TABLEDAL dal = new CTBBS_TABLEDAL();
            return dal.getByT_ID(T_ID);
        }
        public static List<CCBBS_TABLE> getccByC_ID(int C_ID)
        {
            CCBBS_TABLEDAL dal = new CCBBS_TABLEDAL();
            return dal.getByC_ID(C_ID);
        }
        public static List<LB_INFO> getLB_INFOByName(string name)
        {
            LB_INFODAL dal = new LB_INFODAL();
            return dal.GetByName(name);
        }
        public static List<LB_INFO> GetLB_INFOByTID(int T_ID)
        {
            LB_INFODAL dal = new LB_INFODAL();
            return dal.getByTitle(T_ID);
        }
        public static List<View_CT_U> GetCLgetTID(int TID)
        {
            View_CT_UDAL dal = new View_CT_UDAL();
            return dal.getByTID(TID);
        }
        public static bool DeleteCommentByCID(int CID)
        {
            CTBBS_TABLEDAL dal = new CTBBS_TABLEDAL();
            return dal.delete(CID);
        }
        //--------------------------------LB_INFO故事论坛评论提交模块-------------------------
        #endregion
        public static LB_INFO getGecoding(LB_INFO lb1)
        {
            //获取指定的地址，开始进行查询
            string[] xy = new string[2];
            xy[0] = null;
            xy[1] = null;
            xy = redmomery.command.Geocodingcommand.getGecodingByAddress(lb1.LBdomicile);
            //下面开始针对这个模型进行更新
            if (xy[0] != null && xy[0] != "" && xy[1] != null && xy[1] != "")
            {
                lb1.X = float.Parse(xy[0]);
                lb1.Y = float.Parse(xy[1]);
            }
            return lb1;
        }
    }


}
