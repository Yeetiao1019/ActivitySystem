using ActivitySystem.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ActivitySystem.Filters
{
    /// <summary>
    /// 判斷當前登入使用者是否為 Activity 的 CreateUser
    /// </summary>
    public class ActivityCreateUserAuthAttribute : ActionFilterAttribute, IActionFilter
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreateUserAuthAttribute(IActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository ?? throw new ArgumentNullException(nameof(activityRepository)); ;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)       // Action 執行前
        {
            if (context.ActionArguments.ContainsKey("activityId"))      // 判斷傳入 Action 是否有 activityId 參數
            {
                var activityId = (int)context.ActionArguments["activityId"];
                var activity = _activityRepository.GetActivityById(activityId);
                var claimsPrincipal = context.HttpContext.User as ClaimsPrincipal;
                var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

                if(activity != null && userId != null)
                {
                    if(activity.CreateUser != userId)
                    {
                        throw new UnauthorizedAccessException("無權限");
                    }
                }
            }
        }
    }
}
