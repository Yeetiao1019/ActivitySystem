using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class EnrollRepository : IEnrollRepository
    {
        private readonly AppDbContext _appDbContext;
        public EnrollRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public bool AddEnroll(Enroll enroll)
        {
            var activity = _appDbContext.Activities.FirstOrDefault(a => a.ActivityId == enroll.ActivityId);
            var userAlreadyEnrollInActivity = GetEnrollByActivityIdAndUserId(enroll.ActivityId, enroll.ApplicationUserId);
            var alreadyEnrollQty = GetEnrollQtyByActivityId(enroll.ActivityId);

            try
            {
                if(alreadyEnrollQty >= activity.EnrollCount)     // 已報名人數小於報名人數才可執行
                {
                    throw new Exception("此活動報名人數已滿");
                }
                else if(userAlreadyEnrollInActivity != null)
                {
                    throw new Exception("您已報名此活動");
                }
                else
                {
                    _appDbContext.Enrolls.Add(enroll);
                    var count = _appDbContext.SaveChanges();

                    return count > 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool DeleteEnrollByActivityIdAndUserId(int activityId, string userId)
        {
            var enroll = _appDbContext.Enrolls
                .FirstOrDefault(e => e.ActivityId == activityId && e.ApplicationUserId == userId);

            if(enroll != null)
            {
                _appDbContext.Enrolls.Remove(enroll);
                var count = _appDbContext.SaveChanges();

                return count > 0;
            }

            return false;
        }

        public int GetEnrollQtyByActivityId(int activityId)
        {
            return _appDbContext.Enrolls.Where(e => e.ActivityId == activityId).Count();
        }

        public Enroll GetEnrollByActivityIdAndUserId(int activityId, string userId)
        {
            return _appDbContext.Enrolls
                .FirstOrDefault(e => e.ActivityId == activityId && e.ApplicationUserId == userId);
        }

        public IEnumerable<Enroll> GetEnrollsByUserId(string userId)
        {
            try
            {
                var enrolls = _appDbContext.Enrolls.Where(e => e.ApplicationUserId == userId).ToList();
                if (enrolls != null)
                {
                    foreach (var enroll in enrolls)
                    {
                        var activity = _appDbContext.Activities.FirstOrDefault(a => a.ActivityId == enroll.ActivityId);
                        enroll.Activity = activity;
                    }
                }

                return enrolls;
            }
            catch (Exception)
            {
                throw new ArgumentNullException("報名資料搜尋失敗");
            }
        }
    }
}
