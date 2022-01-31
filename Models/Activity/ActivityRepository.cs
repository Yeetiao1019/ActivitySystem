using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrganizerRepository organizer;

        public ActivityRepository(AppDbContext appDbContext, IOrganizerRepository organizer)
        {
            this._appDbContext = appDbContext;
            this.organizer = organizer;
        }
        public bool AddActivity(Activity activity)
        {
            _appDbContext.Activities.Add(activity);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }

        public bool DeleteActivityById(int activityId)
        {
            var activity = _appDbContext.Activities.FirstOrDefault(a => a.ActivityId == activityId);
            if (activity == null)
            {
                return false;
            }
            _appDbContext.Activities.Remove(activity);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }

        public IEnumerable<Activity> GetActivities()
        {
            return _appDbContext.Activities.ToList();
        }

        public IEnumerable<Activity> GetActivitiesByName(string activityName)
        {
            if(activityName == null)
            {
                return _appDbContext.Activities.ToList();
            }

            return _appDbContext.Activities.Where(a => a.ActivityName.Contains(activityName)).ToList();
        }

        public Activity GetActivityById(int activityId)
        {
            return _appDbContext.Activities.FirstOrDefault(a => a.ActivityId == activityId);
        }

        public bool UpdateActivity(Activity activity)
        {
             _appDbContext.Activities.Update(activity) ;
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }
    }
}
