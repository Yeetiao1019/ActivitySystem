using ActivitySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.ViewModels
{
    public class ActivityIndexViewModel
    {
        public IEnumerable<Activity> Activities { get; set; }
    }
}
