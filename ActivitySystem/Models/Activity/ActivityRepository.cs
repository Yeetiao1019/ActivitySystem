using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IActivityImageRepository _activityImageRepository;

        public ActivityRepository(AppDbContext appDbContext, IActivityImageRepository activityImageRepository)
        {
            this._appDbContext = appDbContext;
            this._activityImageRepository = activityImageRepository;
        }
        public bool AddActivity(Activity activity)
        {
            _appDbContext.Activities.Add(activity);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }

        public bool AddActivityWithImage(Activity activity, ActivityImage activityImage)
        {
            _appDbContext.Activities.Add(activity);
            var count = _appDbContext.SaveChanges();

            if (count > 0)
            {
                activityImage.ActivityId = activity.ActivityId;
                _activityImageRepository.AddActivityImage(activityImage);

                count += _appDbContext.SaveChanges();
            }

            return count > 1;
        }

        public bool DeleteActivity(Activity activity)
        {
            try
            {
                _appDbContext.Activities.Remove(activity);
                var count = _appDbContext.SaveChanges();

                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
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
            var activities = _appDbContext.Activities.ToList();
            foreach (var activity in activities)    // 尋找已上傳的圖片 ID
            {
                var activityImageId = _appDbContext.ActivityImages.FirstOrDefault(ai => ai.ActivityId == activity.ActivityId).ActivityImageId;
                activity.ActivityImage.ActivityImageId = activityImageId;
            }

            return activities;
        }

        public IEnumerable<Activity> GetActivitiesByName(string activityName)
        {
            var activities = _appDbContext.Activities.Where(a => a.ActivityName.Contains(activityName)).ToList();

            foreach (var activity in activities)    // 尋找已上傳的圖片 ID
            {
                var activityImageId = _appDbContext.ActivityImages.FirstOrDefault(ai => ai.ActivityId == activity.ActivityId).ActivityImageId;
                activity.ActivityImage.ActivityImageId = activityImageId;
            }

            return activities;
        }
        public async Task<IEnumerable<Activity>> GetPopularActivitiesAsync(int count)
        {
            var SqlScript = $"EXECUTE dbo.GetTheNewestActivities @Count = {count}";
            var activities = await _appDbContext.Activities.FromSqlRaw(SqlScript).ToListAsync();

            return activities;
        }

        public async Task<IEnumerable<Activity>> GetTheNewestActivitiesAsync(int count)
        {
            string SqlScript = $"EXECUTE dbo.GetTheNewestActivities @Count = {count}";
            var activities = await _appDbContext.Activities.FromSqlRaw(SqlScript).ToListAsync();

            return activities;
        }

        public Activity GetActivityById(int? activityId)
        {
            if (activityId == null)
            {
                throw new ArgumentNullException();
            }

            var activity = _appDbContext.Activities.FirstOrDefault(a => a.ActivityId == activityId);
            var activityImageId = _appDbContext.ActivityImages.FirstOrDefault(ai => ai.ActivityId == activity.ActivityId).ActivityImageId;
            activity.ActivityImage.ActivityImageId = activityImageId;

            return activity;
        }

        public bool UpdateActivity(Activity activity)
        {
            _appDbContext.Activities.Update(activity);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }
    }
}
