using ActivitySystem.Models;
using ActivitySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.ViewComponents
{
    public class ShowActivityCarousel : ViewComponent
    {
        private readonly IActivityRepository _activityRepository;

        public ShowActivityCarousel(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activities = _activityRepository.GetActivities();

            return View(new ActivityListViewModel
            {
                Activities = activities
            });
        }
    }
}
