using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services
{
    public static class TimeHelper
    {
        public static DateTime ConvertUtcToVietnamTime(DateTime utcTime)
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, vietnamTimeZone);
        }

        public static DateTime GetNowInVietnamTime()
        {
            return ConvertUtcToVietnamTime(DateTime.UtcNow);
        }
    }
}
