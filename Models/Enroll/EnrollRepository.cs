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
            _appDbContext.Enrolls.Add(enroll);
            var count = _appDbContext.SaveChanges();

            return count > 0;
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

        public Enroll GetEnrollByActivityIdAndUserId(int activityId, string userId)
        {
            return _appDbContext.Enrolls
                .FirstOrDefault(e => e.ActivityId == activityId && e.ApplicationUserId == userId);
        }

        public IEnumerable<Enroll> GetEnrollsByUserId(string userId)
        {
            return _appDbContext.Enrolls.Where(e => e.ApplicationUserId == userId);
        }
    }
}
