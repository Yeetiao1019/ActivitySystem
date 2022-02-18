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
        private readonly AppDbContext _appDbContext;

        public ShowPopularActivities(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var activities = await _appDbContext.Activities.FromSqlRaw("EXECUTE dbo.GetPopularActivities").ToListAsync();

            return View(new ActivityListViewModel
            {
                Activities = activities
            });
        }
    }
}
