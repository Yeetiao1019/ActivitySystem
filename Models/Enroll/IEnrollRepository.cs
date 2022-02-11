using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public interface IEnrollRepository
    {
        Enroll GetEnrollByActivityIdAndUserId(int activityId, string userId);
        IEnumerable<Enroll> GetEnrollsByUserId(string userId);
        bool AddEnroll(Enroll enroll);
        bool DeleteEnrollByActivityIdAndUserId(int activityId, string userId);        
    }
}
