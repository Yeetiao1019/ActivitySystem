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
    public class ShowActivityCarousel : ViewComponent
    {
        private readonly IActivityRepository _activityRepository;
        private readonly AppDbContext _appDbContext;

        public ShowActivityCarousel(IActivityRepository activityRepository, AppDbContext appDbContext)
        {
            _activityRepository = activityRepository;
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activities = await _appDbContext.Activities.FromSqlRaw("EXECUTE dbo.GetTheNewestActivities").ToListAsync();

            return View(new ActivityListViewModel
            {
                Activities = activities
            });
        }
    }
}
