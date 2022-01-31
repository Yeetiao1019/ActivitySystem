using ActivitySystem.Models;
using ActivitySystem.ViewModels;
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

        public HomeController(ILogger<HomeController> logger, IActivityRepository activityRepository)
        {
            _logger = logger;
            this._activityRepository = activityRepository;
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
        public IActionResult Add(Models.Activity activity)
        {
            if (ModelState.IsValid)
            {
                _activityRepository.AddActivity(activity);
                TempData["Message"] = "新增成功！";
            }

            return View("Detail", activity);
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
        public IActionResult Edit(ActivitySystem.Models.Activity activity)
        {
            if (ModelState.IsValid)
            {
                _activityRepository.UpdateActivity(activity);
                TempData["Message"] = "修改成功！";
            }

            return View("Detail", _activityRepository.GetActivityById(activity.ActivityId));
        }

        public IActionResult Search(string activityName)
        {
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
