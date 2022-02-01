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
        Activity GetActivityById(int activityId);
        bool AddActivity(Activity activity);
        bool AddActivityWithImage(Activity activity, ActivityImage activityImage);
        bool DeleteActivityById(int activityId);
        bool UpdateActivity(Activity activity);
    }
}
