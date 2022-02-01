using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class ActivityImageRepository : IActivityImageRepository
    {
        private readonly AppDbContext _appDbContext;
        public ActivityImageRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public bool AddActivityImage(ActivityImage activityImage)
        {
            _appDbContext.ActivityImages.Add(activityImage);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }

        public bool DeleteActivityImageById(int activityImageId)
        {
            var activityImage = _appDbContext.ActivityImages.FirstOrDefault(ai => ai.ActivityImageId == activityImageId);
            if (activityImage == null)
            {
                return false;
            }
            _appDbContext.ActivityImages.Remove(activityImage);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }

        public ActivityImage GetActivityImageByActivityId(int activityId)
        {
            if (activityId == 0)
            {
                throw new Exception("ActivityId is 0.");
            }

            return _appDbContext.ActivityImages.FirstOrDefault(ai => ai.ActivityId == activityId);
        }

        public bool UpdateActivityImage(ActivityImage activityImage)
        {
            _appDbContext.ActivityImages.Update(activityImage);
            var count = _appDbContext.SaveChanges();

            return count > 0;
        }
    }
}
