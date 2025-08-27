using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services
{
    public class HolidayProvider : IHolidayProvider
    {
        // Company-observed extra days can be added here per year.
        // All dates are stored as Date (no time).
        private static readonly Dictionary<int, HashSet<DateTime>> _yearly = new()
        {
            // === 2025 (example) ===
            [2025] = new HashSet<DateTime>
        {
            // New Year (fixed)
            new DateTime(2025, 1, 1),

            // Tết 2025 (company-observed range)
            // Vietnamese New Year’s Eve + New Year day + subsequent official days
            new DateTime(2025, 1, 28), // New Year's Eve (observed)
            new DateTime(2025, 1, 29), // Vietnamese New Year’s Day (Tết)
            new DateTime(2025, 1, 30),
            new DateTime(2025, 1, 31),
            new DateTime(2025, 2, 1),
            new DateTime(2025, 2, 2),

            // Hùng Kings’ Commemoration Day 2025
            new DateTime(2025, 4, 7),

            // Reunification Day & Labor Day
            new DateTime(2025, 4, 30),
            new DateTime(2025, 5, 1),

            // National Day (Sep 2) + one adjacent day (Sep 1) as per policy
            new DateTime(2025, 9, 1),
            new DateTime(2025, 9, 2),
        }
        };

        public bool IsHoliday(DateTime date)
        {
            var d = date.Date;
            var y = d.Year;

            // Fixed-date holidays (apply to every year)
            if (d.Month == 4 && d.Day == 30) return true; // Reunification Day
            if (d.Month == 5 && d.Day == 1) return true; // Labor Day
            if (d.Month == 1 && d.Day == 1) return true; // New Year’s Day
            if (d.Month == 9 && d.Day == 2) return true; // National Day

            // Year-specific observed dates (Tết, Hùng Kings’ Day, extra National Day)
            return _yearly.TryGetValue(y, out var set) && set.Contains(d);
        }

        public IReadOnlyCollection<DateTime> GetHolidays(int year)
            => _yearly.TryGetValue(year, out var set)
                ? set.ToList().AsReadOnly()
                : Array.Empty<DateTime>();
    }
}
