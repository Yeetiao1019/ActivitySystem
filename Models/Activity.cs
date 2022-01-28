using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        [DisplayName("活動名稱")]
        public string ActivityName { get; set; }
        [DisplayName("活動地點")]
        public string Location { get; set; }
        [DisplayName("活動內容")]
        public string Description { get; set; }
        [DisplayName("活動開始時間")]
        public DateTime ActivityStartTime { get; set; }
        [DisplayName("活動結束時間")]
        public DateTime ActivityEndTime { get; set; }
        [DisplayName("報名開始時間")]
        public DateTime ActivitySignUpStartTime { get; set; }
        [DisplayName("報名結束時間")]
        public DateTime ActivitySignUpEndTime { get; set; }
        [DisplayName("開放報名人數")]
        public int EnrollCount{ get; set; }
        [DisplayName("已報名人數")]
        public int AlreadyEnrollCount { get; set; }
        public Organizer Organizer { get; set; }
    }
}
