using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class MockActivityImageRepository : IActivityImageRepository
    {
        public bool AddActivityImage(ActivityImage activityImage)
        {
            throw new NotImplementedException();
        }

        public bool DeleteActivityImageById(int activityImageId)
        {
            throw new NotImplementedException();
        }

        public ActivityImage GetActivityImageByActivityId(int activityId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateActivityImage(ActivityImage activityImage)
        {
            throw new NotImplementedException();
        }
    }
}
