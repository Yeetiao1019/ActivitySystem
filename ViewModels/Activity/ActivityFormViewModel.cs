using ActivitySystem.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.ViewModels
{
    public class ActivityFormViewModel
    {
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
        public DateTime ActivityStartTime { get; set; }
        [Required]
        [DisplayName("活動結束時間")]
        public DateTime ActivityEndTime { get; set; }
        [Required]
        [DisplayName("報名開始時間")]
        public DateTime ActivitySignUpStartTime { get; set; }
        [Required]
        [DisplayName("報名結束時間")]
        public DateTime ActivitySignUpEndTime { get; set; }
        [DisplayName("開放報名人數")]
        public int EnrollCount { get; set; }
        [DisplayName("封面圖片")]
        public IFormFile ActivityImage { get; set; }
        public string ActivityImageFileName { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
