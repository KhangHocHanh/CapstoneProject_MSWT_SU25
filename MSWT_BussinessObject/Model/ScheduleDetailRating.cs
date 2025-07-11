using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.Model
{
    public class ScheduleDetailRating
    {
        public string ScheduleDetailRatingId { get; set; } = Guid.NewGuid().ToString();

        public string ScheduleDetailId { get; set; } = null!;
        public string RatedByUserId { get; set; } = null!;

        public DateTime RatedAt { get; set; } = DateTime.UtcNow;
        public DateTime RatingDate { get; set; } = DateTime.UtcNow.Date;

        public int RatingValue { get; set; }
        public string? Comment { get; set; }

        public virtual ScheduleDetail? ScheduleDetail { get; set; }
    }
}
