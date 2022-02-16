using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ActivitySystem.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// 自訂帳號管理的頁面
    /// </summary>
    public static partial class ManageNavPages
    {
        public static string Enroll => "Enroll";
        public static string EnrollNavClass(ViewContext viewContext) => PageNavClass(viewContext, Enroll);
    }
}
