using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;

namespace MSWT_Repositories.Repository
{
    public class ScheduleDetailRatingRepository : GenericRepository<ScheduleDetailRating>, IScheduleDetailRatingRepository
    {
        private readonly SmartTrashBinandCleaningStaffManagementContext _context;

        public ScheduleDetailRatingRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ScheduleDetailRating?> GetByScheduleDetailAndDateAsync(string scheduleDetailId, DateTime ratingDate)
        {
            return await _context.ScheduleDetailRatings
                .FirstOrDefaultAsync(r => r.ScheduleDetailId == scheduleDetailId && r.RatingDate == ratingDate.Date);
        }
    }

}
