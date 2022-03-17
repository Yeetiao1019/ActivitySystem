using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public interface IActivityRepository
    {
        IEnumerable<Activity> GetActivities();
        IEnumerable<Activity> GetActivitiesByName(string activityName);
        Task<IEnumerable<Activity>> GetPopularActivitiesAsync(int count);
        Task<IEnumerable<Activity>> GetTheNewestActivitiesAsync(int count);
        Activity GetActivityById(int? activityId);
        bool AddActivity(Activity activity);
        bool AddActivityWithImage(Activity activity, ActivityImage activityImage);
        bool DeleteActivity(Activity activity);
        bool DeleteActivityById(int activityId);
        bool UpdateActivity(Activity activity);
    }
}
