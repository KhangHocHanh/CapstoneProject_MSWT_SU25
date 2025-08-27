using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IHolidayProvider
    {
        bool IsHoliday(DateTime date);
        IReadOnlyCollection<DateTime> GetHolidays(int year);
    }
}
