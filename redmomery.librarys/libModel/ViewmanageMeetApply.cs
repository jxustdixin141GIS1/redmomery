using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redmomery.librarys.libModel
{
   public  class ViewmanageMeetApply
    {
       //管理的群组信息
        public int MID { get; set; }
        public int GID { get; set; }
        public string local { get; set; }
        public string contentTitle { get; set; }
        public string context { get; set; }
        public DateTime meetTime { get; set; }
        public int vnum { get; set; }
       //待审审核用户信息
        public int USER_ID { get; set; }//审核用户的ID
        public string USER_NETNAME { get; set; }//审核用户的名称
       //审核申请内容
        public DateTime dataTime { get; set; }//申请时间
        public string contentApply { get; set; }//申请留言
       //处理情况
        public int state { get; set; }
        public int dealUID { get; set; }
    }
}
