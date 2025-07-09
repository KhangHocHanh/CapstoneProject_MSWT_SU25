using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class SensorBinStatusHelper
    {
        public static string ToDisplayString(this SensorBinStatus status)
        {
            return status switch
            {
                SensorBinStatus.DangHoatDong => "Hoạt động",
                SensorBinStatus.NgungHoatDong => "Ngưng hoạt động",
                _ => "Không xác định"
            };
        }
    }
}
