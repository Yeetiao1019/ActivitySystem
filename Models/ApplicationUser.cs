using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "用戶名稱")]
        public string FullName { get; set; }
    }
}
