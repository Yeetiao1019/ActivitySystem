using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class Enroll
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollId { get; set; }
        [DataType("datetime2")]
        public DateTime EnrollTime { get; set; } = DateTime.Now;

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        


        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
