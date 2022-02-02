using ActivitySystem.Models;
using ActivitySystem.Services;
using ActivitySystem.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IActivityRepository _activityRepository;
        private readonly IWebHostEnvironment _env;

        public HomeController(ILogger<HomeController> logger, IActivityRepository activityRepository, IWebHostEnvironment env)
        {
            _logger = logger;
            this._activityRepository = activityRepository;
            this._env = env;
        }

        public IActionResult Index()
        {
            var activities = _activityRepository.GetActivities();

            return View(new ActivityListViewModel
            {
                Activities = activities
            });
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ActivityFormViewModel model)
        {
            Models.Activity ActivityModel = new Models.Activity();
            if (ModelState.IsValid)
            {
                IFormFile UploadImageFile = ActivityImageUploadService.UploadedFile(model, _env);

                ActivityModel = new Models.Activity
                {
                    ActivityName = model.ActivityName,
                    Description = model.Description,
                    Location = model.Location,
                    ActivitySignUpStartTime = model.ActivitySignUpStartTime,
                    ActivitySignUpEndTime = model.ActivitySignUpEndTime,
                    ActivityStartTime = model.ActivityStartTime,
                    ActivityEndTime = model.ActivityEndTime
                };

                ActivityImage ActivityImageModel = new ActivityImage 
                { 
                    ImageFileName = model.ActivityImageFileName,
                    UploadTime = DateTime.Now
                };

                _activityRepository.AddActivityWithImage(ActivityModel, ActivityImageModel);
                TempData["Message"] = "新增成功！";
            }

            return View("Detail", ActivityModel);
        }

        public IActionResult Detail(int activityId)
        {
            if (activityId <= 0)
            {
                return NotFound();
            }

            return View(_activityRepository.GetActivityById(activityId));
        }

        public IActionResult Edit(int activityId)
        {
            if (activityId <= 0)
            {
                return NotFound();
            }

            return View(_activityRepository.GetActivityById(activityId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ActivitySystem.Models.Activity activity)
        {
            if (ModelState.IsValid)
            {
                _activityRepository.UpdateActivity(activity);
                TempData["Message"] = "修改成功！";
            }

            return View("Detail", _activityRepository.GetActivityById(activity.ActivityId));
        }

        public IActionResult Delete (int? activityId)
        {
            if(activityId == null)
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
