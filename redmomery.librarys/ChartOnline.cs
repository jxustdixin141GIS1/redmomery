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
namespace redmomery.librarys
{
    public class ChartOnline
    {
        

    }
}
namespace redmomery.librarys
{
    public partial class ChartOnlinelib
    {
       //当用户创建一个活动信息时 ,创建成功就返回活动信息
        public static meetingtable Usertakeon(USER_INFO user,string local,string content,string suject)
        {
           meetingtable mt=CreateMeet(user.USER_ID.ToString(),local,content,suject );
            chartgrouptable cg=CreateGroup(user,suject,suject);
            if (BingCgAndMt(cg, mt))
            {
                return mt;
            }
            else
            {
               return  null;
            }
        }

    }
    partial class ChartOnlinelib
    { 
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
                int ind= gdal.AddNew(gu);
                if (ind >= 0)
                {
                    cg.vnum = 1;
                }
            }
            return cg;
        }
        //创建活动对象 返回活动对象
        public static meetingtable CreateMeet(string UID, string local, string content,string suject)
        {
            meetingtable newmeet = new meetingtable();
            newmeet.UID = int.Parse(UID);
            newmeet.Ttime = DateTime.Now;
            newmeet.local = local;
            newmeet.context = content;
            newmeet.contentTitle = suject;
            baiduGeocodingaddress xy = redmomery.command.Geocodingcommand.getGeocodingByAddressobject(newmeet.local);
            if (xy.status == 0 && xy.result != null & xy.result.location != null)
            {
                newmeet.lng = xy.result.location.lng;
                newmeet.lat = xy.result.location.lat;
            }
            newmeet.vnum = 1;
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
    }

}

