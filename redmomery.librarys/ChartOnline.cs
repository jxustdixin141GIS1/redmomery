using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using redmomery.librarys;
using redmomery.Model;
using redmomery.DAL;
using redmomery.command;
using redmomery.Common;
using redmomery.librarys.model;
using redmomery.librarys.libModel;
namespace redmomery.librarys
{
    public class ChartOnline
    {
        public List<multimessagepooltable> getmeesage(int GID)
        {
            multimessagepooltableDAL dal = new multimessagepooltableDAL();
            List<multimessagepooltable> list = dal.getByGID(GID);
            return list;
        }
        public bool PostMessageG(USER_INFO user, int TGID, string context)
        {
            multimessagepooltable newmessage = new multimessagepooltable();
            newmessage.context = context;
            newmessage.FUID = user.USER_ID;
            newmessage.TGID = TGID;
            newmessage.Rnum = 10;//可以选择
            newmessage.Ftime = DateTime.Now;
            multimessagepooltableDAL dal = new multimessagepooltableDAL();
          int count=  dal.AddNew(newmessage);
          return (count > 0);
        }
        public List<GroupUser> GetUserG(int GID)
        {
            List<GroupUser> list = new List<GroupUser>();
            return list;
        }
    }
}
//在线交流部分开始进行书写


namespace redmomery.librarys
{
    public partial class ChartOnlinelib
    {
       //当用户创建一个活动信息时 ,创建成功就返回活动信息，失败就返回空对象
        public static meetingtable Usertakeon(USER_INFO user,string local,string content,string suject,DateTime meetTime)
        {
            chartgrouptable cg = CreateGroup(user, suject, suject);
            meetingtable mt=CreateMeet(user.USER_ID.ToString(),local,content,suject,meetTime,cg );
            if (mt!=null)
            {
                return mt;
            }
            else
            {
               return  null;
            }
        }
        //获取指定用户的参与的活动列表
        public static List<ViewUTIMeet> GetmeetList(USER_INFO user)
        {
            //获取用户和列表之间的关系
            List<UTIMeetTable> list = new List<UTIMeetTable>();
            UTIMeetTableDAL udal = new UTIMeetTableDAL();
            list = udal.getByUID(user.USER_ID);
            List<ViewUTIMeet> result = new List<ViewUTIMeet>();
            for (int i = 0; i < list.Count; i++)
            {
                //添加表示

                ViewUTIMeet newView = new ViewUTIMeet();
                newView.MeetID = list[i].MeetID;
                newView.USER_NETNAME = user.USER_NETNAME;
                newView.USER_EMEIL = user.USER_EMEIL;
                newView.USER_IMG = user.USER_IMG;
                newView.USER_PHONE = user.USER_PHONE;
                newView.state = list[i].state;
                //获取活动信息
                meetingtableDAL mdal = new meetingtableDAL();
                meetingtable meet = mdal.Get(list[i].MeetID);
                newView.lat = meet.lat;
                newView.lng = meet.lng;
                newView.local = meet.local;
                newView.vnum = meet.vnum;
                newView.contentTitle = meet.contentTitle;
                newView.context = meet.context;
                newView.meetTime = meet.meetTime;
                newView.isCheck = meet.isCheck;
                newView.lng = meet.lng;
                newView.lat = meet.lat;
                if (list[i].state == 1)//表示这个不是用户自己创建的，需要从数据库中找到相对应的记录
                {
                    USER_INFODAL uidal = new USER_INFODAL();
                    USER_INFO newuser = uidal.get(meet.UID);
                    newView.USER_NETNAME = newuser.USER_NETNAME;
                    newView.USER_PHONE = newuser.USER_PHONE;
                    newView.USER_IMG = newuser.USER_IMG;
                    newView.USER_EMEIL = newuser.USER_EMEIL;
                }
                //这里表示信息填充完毕
                if (meet.isCheck == 0)
                {
                    result.Add(newView);
                }
            }
            return result;
        }
      //用户申请参加活动模块
        public static UATIMeettable applyTImeet(int meetID,int UID,string contentapply)
        {
            
            UATIMeettableDAL dal = new UATIMeettableDAL();
            List<UATIMeettable> newUTIs = dal.getByUIDMID(UID, meetID);
            if (newUTIs == null)
            {
                //表示这个用户没有提交过申请
                UATIMeettable newUTl = new UATIMeettable();
                newUTl.UID = UID;
                newUTl.meetID = meetID;
                newUTl.dataTime = DateTime.Now;
                newUTl.contentApply = contentapply;
                newUTl.state = 2;//表示待处理
                newUTl.dealUID = -1;//表示这个是系统的一个机器人，为了维护数据库的唯一性
                int cout = dal.AddNew(newUTl);
                if (cout > 0)
                {
                    newUTIs = dal.getByUIDMID(UID, meetID);
                    if (newUTIs.Count > 0)
                    {
                        return newUTIs[0];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;//表示失败
                }
            }
            else
            { 
               //表示已经找到了，统计被拒绝的次数
                int count = 0;
                for (int i = 0; i < newUTIs.Count; i++)
                {
                    if (newUTIs[i].state == 1)
                    {
                        count++;
                    }
                }
                if (count >5)
                {
                    UATIMeettable newUTI = new UATIMeettable();
                    newUTI.state = 3;
                    return newUTI;
                }
                else
                {
                    UATIMeettable newUai = new UATIMeettable();
                    newUai.dealUID = -1;
                    newUai.dataTime = DateTime.Now;
                    newUai.contentApply = contentapply;
                    newUai.meetID = meetID;
                    newUai.UID = UID;
                    newUai.state = 2;
                  int i= dal.AddNew(newUai);
                  return i > 0 ? newUai : null;
                }

            }
         
        }
      //获取当前用户能够管理的活动空间的申请
        public static List<redmomery.librarys.libModel.ViewmanageMeetApply> getlistManageapply(USER_INFO user)
        {
            List<ViewmanageMeetApply> result = new List<ViewmanageMeetApply>();//作为结果进行输出
            //准备的对象
            UTIMeetTableDAL utmdal = new UTIMeetTableDAL();
            meetingtableDAL mdal = new meetingtableDAL();
            USER_INFODAL udal = new USER_INFODAL();
            UATIMeettableDAL uadal = new UATIMeettableDAL();
             //1、获取当前信息为管理员的数据库表格
         
            List<UTIMeetTable> Utlsit = utmdal.getmanageByUID(user.USER_ID);
            //2、开始按照对应的管理进行查询管理的活动申请情况
            for (int i = 0; Utlsit!=null&&i < Utlsit.Count; i++)
            {
               meetingtable managermeet = mdal.Get(Utlsit[i].MeetID);
                //2.1 开始转存活动信息            
                //获取待审核用户的信息
                  //开始获取这个活动没有处理的需求信息,开始按照对应的活动的ID进行查询
                List<UATIMeettable> ualist=uadal.getByMID(Utlsit[i].MeetID) as List<UATIMeettable>;
                for (int j = 0; j < ualist.Count; j++)
                {
                    UATIMeettable uamodel = ualist[j];
                   
                    if (ualist[j].state == 2)//只对待处理的进行处理
                    {
                        USER_INFO Uuser = udal.get(uamodel.UID);
                        ViewmanageMeetApply newmanager = new ViewmanageMeetApply();
                        newmanager.MID = managermeet.ID;
                        newmanager.GID = managermeet.GID;
                        newmanager.local = managermeet.local;
                        newmanager.meetTime = managermeet.meetTime;
                        newmanager.contentTitle = managermeet.contentTitle;
                        newmanager.context = managermeet.context;
                        newmanager.vnum = managermeet.vnum;
                        //开始进行转存待处理用户的个人信息
                        newmanager.USER_ID = Uuser.USER_ID;
                        newmanager.USER_NETNAME = Uuser.USER_NETNAME;
                        newmanager.dataTime = uamodel.dataTime;
                        newmanager.contentApply = uamodel.contentApply;
                        //处理情况
                        newmanager.state = uamodel.state;
                        newmanager.dealUID = uamodel.dealUID;
                      //添加到对应的输出列表中
                        result.Add(newmanager);
                    }
                }

            }

            return result;
        }
      //对于当前用户进行处理的意见
        public static bool dealwithUp(int dealUID, int MeetID, int state, int UserID)
        {
           
            UATIMeettableDAL dal = new UATIMeettableDAL();
            List<UATIMeettable> uamodels = dal.getByUIDMID(dealUID, MeetID);

            for (int i = 0; i < uamodels.Count; i++)
            {
                if (uamodels[i].state == 2)
                {
                    uamodels[i].state = state;
                    dal.Update(uamodels[i]);
                    if (state == 0)//表示同意
                    {
                        //将这个用户和对应的活动组绑定，以及和对应的聊天组绑定
                        UTIMeetTable utmodel = new UTIMeetTable();
                        utmodel.UID = uamodels[i].dealUID;
                        utmodel.MeetID = uamodels[i].meetID;
                        utmodel.state = 1;
                        UTIMeetTableDAL utdal = new UTIMeetTableDAL();
                        //在进行数据更新前需要进行数据的比较首先判断用户有没有绑定到群组
                        UTIMeetTable checkUT = utdal.Get(utmodel.UID, utmodel.MeetID);
                        if (checkUT == null)
                        {
                            int count = utdal.AddNew(utmodel);
                        }
                        else
                        {
                            //不进行任何处理
                        }
                        //添加完成之后，就删除所有的数据库中的内容
                        dal.Delete(dealUID,MeetID);
                        return true;
                    }
                    else
                    { 
                      //同时进行状态处理
                        List<UATIMeettable> listss = dal.getByUIDMID(dealUID,MeetID);
                        if (listss.Count >= 5)
                        {
                            dal.Delete(dealUID, MeetID); 
                        for (int ii = 0; ii < 5; ii++)
                        {
                            listss[i].state = 1;
                            dal.AddNew(listss[ii]);
                        }
                        return true;
                      }
                    }
                }
            }

            return false;
        }
    }
    partial class ChartOnlinelib
    {
        #region 创建聊天群主，
        //创建群组 ,返回群组的编号
        public static chartgrouptable CreateGroup(USER_INFO user,string description,string groupName)
        {
           chartgrouptableDAL dal=new chartgrouptableDAL();
           chartgrouptable cg = new chartgrouptable();
            cg.Ctime = DateTime.Now;
            cg.groupName = groupName;
            cg.description = description;
            cg.UID =user.USER_ID;
            cg.vnum = 0;
           int index = dal.AddNew(cg);
            //创建用户成功之后，应该讲用户绑定到对应的群组中
            cg=dal.Get(index);
            if (cg != null)
            {
                GroupUser gu = new GroupUser();
                GroupUserDAL gdal = new GroupUserDAL();
                gu.UID = user.USER_ID;
                gu.state = 0;
                gu.GroupID = cg.ID;
                gu.groupname = user.USER_NETNAME;
                if (checkExsit(gu.UID, gu.GroupID) == null)//用来检测是否已经存在
                {
                    int ind = gdal.AddNew(gu);
                    if (ind >= 0)
                    {
                        cg.vnum = 1;
                    }
                }
            }
            return cg;
        }
        //创建活动对象 返回活动对象
        public static meetingtable CreateMeet(string UID, string local, string content,string suject,DateTime meetTime, chartgrouptable cg)
        {
            meetingtable newmeet = new meetingtable();
            newmeet.UID = int.Parse(UID);
            newmeet.Ttime = DateTime.Now;
            newmeet.local = local;
            newmeet.context = content;
            newmeet.contentTitle = suject;
            newmeet.isCheck = 0;
            newmeet.meetTime = meetTime;
            newmeet.GID = cg.ID;
            baiduGeocodingaddress xy = redmomery.command.Geocodingcommand.getGeocodingByAddressobject(newmeet.local);
            if (xy.status == 0 && xy.result != null & xy.result.location != null)
            {
                newmeet.lng = xy.result.location.lng;
                newmeet.lat = xy.result.location.lat;
            }
            //将创建的活动列表添加到数据库中
            meetingtableDAL mdal = new meetingtableDAL();
            int meid = mdal.AddNew(newmeet);
            newmeet = mdal.Get(meid);
            if(newmeet!=null)
            {
               //开始将用户的ID和其创建的活动进行组合
                UTIMeetTable um = new UTIMeetTable();
                um.MeetID = newmeet.ID;
                um.UID = int.Parse(UID.ToString());
                um.state = 0;
                UTIMeetTableDAL udal = new UTIMeetTableDAL();
              int i=  udal.AddNew(um);
              if (i >= 1)
              {
                  newmeet.vnum = 1;
              }
              mdal.Update(newmeet);
              newmeet = mdal.Get(newmeet.ID);
            }      
            return newmeet;
        }
        //绑定活动和群组
        public static bool BingCgAndMt(chartgrouptable cg,meetingtable mt)
        {
          mt.GID=cg.ID;
          meetingtableDAL dal = new meetingtableDAL();
          bool isok=dal.Update(mt);
          return isok;
        }
        private static GroupUser checkExsit(int UID, int GID)
        {
            GroupUserDAL dal = new GroupUserDAL();
            GroupUser gu = dal.getGroupUserBy(UID.ToString(),GID.ToString());
            return gu;
        }
       
        #endregion
    }

}

