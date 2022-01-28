using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public interface IActivityRepository
    {
        IEnumerable<Activity> GetActivities();
        Activity GetActivityById(int activityId);
        bool AddActivity(Activity activity);
        bool DeleteActivityById(int activityId);
        bool UpdateActivity(Activity activity);
    }
}
