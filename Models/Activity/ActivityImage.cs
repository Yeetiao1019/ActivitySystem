using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActivitySystem.Models
{
    public class ActivityImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityImageId { get; set; }
        public string ImageFileName { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime UploadTime { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}
