using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace redMomery.Models
{
    public class MessageTemp
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public string IsRead { get; set; }
        public string CreateTime { get; set; }
        public string FromType { get; set; }
        public int OtherId { get; set; }
        public string OtherName { get; set; }
    }
}