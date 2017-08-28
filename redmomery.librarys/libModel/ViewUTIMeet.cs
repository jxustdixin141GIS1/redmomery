using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redmomery.librarys.model
{
    public class ViewUTIMeet//主要用来进行数据的展示工作
    {
        //活动信息
        public int MeetID;
        public DateTime Ttime { get; set; }
        public string local { get; set; }
        public string contentTitle { get; set; }
        public string context { get; set; }
        public int vnum { get; set; }
        public int isCheck { get; set; }
        public object lng { get; set; }
        public object lat { get; set; }
        //创建人信息
        public string USER_EMEIL { get; set; }
        public string USER_NETNAME { get; set; }
        public string USER_PHONE { get; set; }
        public string USER_IMG { get; set; }
       //表示归属
        public int state { get; set; }
    }
}