using ActivitySystem.Filters;
using ActivitySystem.Models;
using ActivitySystem.Services;
using ActivitySystem.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ActivitySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActivityRepository _activityRepository;
        private readonly IWebHostEnvironment _env;
        private readonly IEnrollRepository _enrollRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IActivityRepository activityRepository,
            IWebHostEnvironment env, UserManager<ApplicationUser> userManager,
            IEnrollRepository enrollRepository)
        {
            _logger = logger;
            this._activityRepository = activityRepository;
            this._env = env;
            this._userManager = userManager;
            this._enrollRepository = enrollRepository;
        }

        public IActionResult Index()
        {
            var activities = _activityRepository.GetActivities();

            return View(new ActivityListViewModel
            {
                Activities = activities
            });
        }

        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ActivityFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                IFormFile UploadImageFile = ActivityImageUploadService.UploadedFile(model, _env);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //取得當前登入的使用者 id
                var mapperConfig = new MapperConfiguration(cfg =>
                cfg.CreateMap<ActivityFormViewModel, Models.Activity>()
                .ForMember(a => a.ActivityImage, opt => opt.Ignore())); //註冊 Model 的對映

                var mapper = mapperConfig.CreateMapper();
                var activity = mapper.Map<Models.Activity>(model);
                activity.CreateUser = userId;

                ActivityImage ActivityImageModel = new ActivityImage
                {
                    ImageFileName = model.ActivityImageFileName,
                    UploadTime = DateTime.Now
                };

                _activityRepository.AddActivityWithImage(activity, ActivityImageModel);
                TempData["Message"] = "新增成功！";

                return View("Detail", activity);
            }

            return BadRequest();
        }

        public IActionResult Detail(int activityId)
        {
            if (activityId <= 0)
            {
                return NotFound();
            }

            var activity = _activityRepository.GetActivityById(activityId);
            activity.AlreadyEnrollCount = _enrollRepository.GetEnrollQtyByActivityId(activityId);

            var createUser = _userManager.FindByIdAsync(activity.CreateUser);   //用 userId 取 UserName
            ViewData["CreateUserName"] = createUser.Result.FullName;

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userAlreadyEnrollInActivity = _enrollRepository.GetEnrollByActivityIdAndUserId(activityId, currentUserId);
            if(userAlreadyEnrollInActivity != null)     // 若該活動的報名資料有當前登入使用者，則不顯示報名按鈕
            {
                ViewData["EnrollBtn"] = false;
            }
            else
            {
                ViewData["EnrollBtn"] = true;
            }

            if (activity.CreateUser != currentUserId)   // 若當前登入使用者非活動建立者，則不顯示編輯按鈕
            {
                ViewData["EditDelBtn"] = false;
            }
            else
            {
                ViewData["EditDelBtn"] = true;
            }

            return View(activity);
        }

        [Authorize]
        [TypeFilter(typeof(ActivityCreateUserAuthAttribute))]
        public IActionResult Edit(int activityId)
        {
            if (activityId <= 0)
            {
                return NotFound();
            }

            return View(_activityRepository.GetActivityById(activityId));
        }

        [HttpPost]
        [TypeFilter(typeof(ActivityCreateUserAuthAttribute))]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.Activity activity)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //取得當前登入的使用者 id
                activity.UpdateUser = userId;
                activity.UpdateTime = DateTime.Now;

                _activityRepository.UpdateActivity(activity);
                TempData["Message"] = "修改成功！";
            }

            return View("Detail", _activityRepository.GetActivityById(activity.ActivityId));
        }

        [Authorize]
        public IActionResult Delete(int? activityId)
        {
            if (activityId == null)
            {
                return NotFound();
            }
            var activity = _activityRepository.GetActivityById(activityId);

            return View(activity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int activityId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _activityRepository.DeleteActivityById(activityId);
                    TempData["Message"] = "刪除成功！";

                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        public IActionResult Enroll()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Enroll(int activityId)
        {
            try
            {
                var activity = _activityRepository.GetActivityById(activityId);
                if(activity != null)
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //取得當前登入的使用者 id
                    Enroll enroll = new Enroll()
                    {
                        ActivityId = activityId,
                        ApplicationUserId = userId
                    };

                    _enrollRepository.AddEnroll(enroll);

                    TempData["Message"] = "報名成功！";

                    return RedirectToAction("Enroll");
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return BadRequest();
        }

        public IActionResult Search(string activityName)
        {
            activityName = activityName == null ? "" : activityName;       // 避免 null 值而無法正常撈到資料
            ViewData["activityName"] = activityName;

            return View(new ActivityListViewModel
            {
                Activities = _activityRepository.GetActivitiesByName(activityName)
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return base.View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
