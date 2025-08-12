using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidayService(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public async Task<bool> IsHolidayAsync(DateTime date)
        {
            var holidays = await _holidayRepository.GetAllHolidaysAsync();
            return holidays.Contains(date.Date);
        }

        public async Task<List<DateTime>> FilterOutHolidaysAsync(IEnumerable<DateTime> dates)
        {
            var holidays = await _holidayRepository.GetAllHolidaysAsync();
            return dates.Where(d => !holidays.Contains(d.Date)).ToList();
        }
    }
}
