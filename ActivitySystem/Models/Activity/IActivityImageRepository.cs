using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public interface IActivityImageRepository
    {
        ActivityImage GetActivityImageByActivityId(int activityId);
        bool AddActivityImage(ActivityImage activityImage);
        bool DeleteActivityImageById(int activityImageId);
        bool UpdateActivityImage(ActivityImage activityImage);
    }
}
