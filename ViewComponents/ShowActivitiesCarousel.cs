using ActivitySystem.Models;
using ActivitySystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.ViewComponents
{
    public class ShowActivitiesCarousel : ViewComponent
    {
        private readonly IActivityRepository _activityRepository;

        public ShowActivitiesCarousel(IActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new ActivityListViewModel
            {
                Activities = await _activityRepository.GetTheNewestActivitiesAsync(5)
            });
        }
    }
}
