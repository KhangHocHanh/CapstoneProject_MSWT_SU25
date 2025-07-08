using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class SensorStatusHelper
    {
        public static string ToDisplayString(this SensorStatus status)
        {
            return status switch
            {
                SensorStatus.DangHoatDong => "Hoạt động",
                SensorStatus.NgungHoatDong => "Ngưng hoạt động",
                _ => "Không xác định"
            };
        }
    }
}
