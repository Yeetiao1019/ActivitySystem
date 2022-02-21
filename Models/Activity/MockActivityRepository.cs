using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class MockActivityRepository : IActivityRepository
    {
        public bool AddActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public bool AddActivityWithImage(Activity activity, ActivityImage activityImage)
        {
            throw new NotImplementedException();
        }

        public bool DeleteActivity(Activity activity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteActivityById(int activityId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Activity> GetActivities()
        {
            List<Activity> Activities = new List<Activity>
            {
                new Activity
                {
                    ActivityId = 1,
                    ActivitySignUpEndTime = new DateTime(2021,1,1),
                    ActivitySignUpStartTime = new DateTime(2021,1,5),
                    ActivityStartTime = new DateTime(2021,2,1),
                    ActivityEndTime = new DateTime(2021,2,5),
                    ActivityName = "測試活動001",
                    Description = "For testing",
                    ActivityImage = new ActivityImage
                    {
                        ActivityId =1,
                        ImageFileName = "test001.jpg",
                        ActivityImageId = 1,
                        UploadTime = new DateTime()
                    },
                    EnrollCount = 25,
                    Location = "Taipei, Taiwan",
                    CreateTime = new DateTime(2020, 12, 31),
                    CreateUser = "User00001"
                },
                new Activity
                {
                    ActivityId = 2,
                    ActivitySignUpEndTime = new DateTime(2021,2,1),
                    ActivitySignUpStartTime = new DateTime(2021,2,5),
                    ActivityStartTime = new DateTime(2021,3,1),
                    ActivityEndTime = new DateTime(2021,3,5),
                    ActivityName = "測試活動002",
                    Description = "For testing2",
                    ActivityImage = new ActivityImage
                    {
                        ActivityId =2,
                        ImageFileName = "test002.jpg",
                        ActivityImageId = 2,
                        UploadTime = new DateTime()
                    },
                    EnrollCount = 5,
                    Location = "Taipei, Taiwan",
                    CreateTime = new DateTime(2021, 12, 31),
                    CreateUser = "User00002"
                }
            };

            Activities[0].ActivityImage.Activity = Activities[0];
            Activities[1].ActivityImage.Activity = Activities[1];

            return Activities;
        }

        public async Task<IEnumerable<Activity>> GetActivitiesAsync()
        {
            List<Activity> Activities = new List<Activity>
            {
                new Activity
                {
                    ActivityId = 1,
                    ActivitySignUpEndTime = new DateTime(2021,1,1),
                    ActivitySignUpStartTime = new DateTime(2021,1,5),
                    ActivityStartTime = new DateTime(2021,2,1),
                    ActivityEndTime = new DateTime(2021,2,5),
                    ActivityName = "測試活動001",
                    Description = "For testing",
                    ActivityImage = new ActivityImage
                    {
                        ActivityId =1,
                        ImageFileName = "test001.jpg",
                        ActivityImageId = 1,
                        UploadTime = new DateTime()
                    },
                    EnrollCount = 25,
                    Location = "Taipei, Taiwan",
                    CreateTime = new DateTime(2020, 12, 31),
                    CreateUser = "User00001"
                },
                new Activity
                {
                    ActivityId = 2,
                    ActivitySignUpEndTime = new DateTime(2021,2,1),
                    ActivitySignUpStartTime = new DateTime(2021,2,5),
                    ActivityStartTime = new DateTime(2021,3,1),
                    ActivityEndTime = new DateTime(2021,3,5),
                    ActivityName = "測試活動002",
                    Description = "For testing2",
                    ActivityImage = new ActivityImage
                    {
                        ActivityId =2,
                        ImageFileName = "test002.jpg",
                        ActivityImageId = 2,
                        UploadTime = new DateTime()
                    },
                    EnrollCount = 5,
                    Location = "Taipei, Taiwan",
                    CreateTime = new DateTime(2021, 12, 31),
                    CreateUser = "User00002"
                }
            };

            return Activities;
        }

        public IEnumerable<Activity> GetActivitiesByName(string activityName)
        {
            throw new NotImplementedException();
        }

        public Activity GetActivityById(int? activityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Activity>> GetPopularActivitiesAsync(int count)
        {
            return GetActivitiesAsync();
        }

        public Task<IEnumerable<Activity>> GetTheNewestActivitiesAsync(int count)
        {
            return GetActivitiesAsync();
        }

        public bool UpdateActivity(Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
