using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.Repository
{
    public class HolidayRepository : GenericRepository<Holiday>, IHolidayRepository
    {
        public HolidayRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<DateTime>> GetAllHolidaysAsync()
        {
            return await _context.Holidays
                .Select(h => h.Date)
                .ToListAsync();
        }
    }
}
