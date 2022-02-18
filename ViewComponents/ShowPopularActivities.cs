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
    public class ShowPopularActivities : ViewComponent
    {
        private readonly IActivityRepository _activityRepository;

        public ShowPopularActivities(IActivityRepository activityRepository)
        {
            this._activityRepository = activityRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new ActivityListViewModel
            {
                Activities = await _activityRepository.GetPopularActivitiesAsync(5)
            });
        }
    }
}
