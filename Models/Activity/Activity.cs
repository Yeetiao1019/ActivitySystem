using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class Activity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }
        [Required]
        [DisplayName("活動名稱")]
        public string ActivityName { get; set; }
        [Required]
        [DisplayName("活動地點")]
        public string Location { get; set; }
        [Required]
        [DisplayName("活動內容")]
        public string Description { get; set; }
        [Required]
        [DisplayName("活動開始時間")]
        [Column(TypeName = "datetime2")]
        public DateTime ActivityStartTime { get; set; }
        [Required]
        [DisplayName("活動結束時間")]
        [Column(TypeName = "datetime2")]
        public DateTime ActivityEndTime { get; set; }
        [Required]
        [DisplayName("報名開始時間")]
        [Column(TypeName = "datetime2")]
        public DateTime ActivitySignUpStartTime { get; set; }
        [Required]
        [DisplayName("報名結束時間")]
        [Column(TypeName = "datetime2")]
        public DateTime ActivitySignUpEndTime { get; set; }
        [DisplayName("開放報名人數")]
        public int EnrollCount { get; set; }
        [DisplayName("已報名人數")]
        public int AlreadyEnrollCount { get; set; }

        public ActivityImage ActivityImage { get; set; }
    }
}
