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

            return View(new ActivityIndexViewModel
            {
                Activities = activities
            });
        }

        public IActionResult Add()
        {


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return base.View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
