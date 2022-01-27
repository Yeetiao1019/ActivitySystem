using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public DateTime ActivityStartTime { get; set; }
        public DateTime ActivityEndTime { get; set; }
        public DateTime ActivitySignUpStartTime { get; set; }
        public DateTime ActivitySignUpEndTime { get; set; }
        public int EnrollCount{ get; set; }
        public int AlreadyEnrollCount { get; set; }
        public Organizer Organizer { get; set; }
    }
}
