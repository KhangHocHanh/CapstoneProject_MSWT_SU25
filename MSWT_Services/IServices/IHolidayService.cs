using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IHolidayService
    {
        Task<bool> IsHolidayAsync(DateTime date);
        Task<List<DateTime>> FilterOutHolidaysAsync(IEnumerable<DateTime> dates);
    }
}
