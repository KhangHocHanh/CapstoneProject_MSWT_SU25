using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class AlertStatusHelper
    {
        public static string ToDisplayString(this AlertStatus status)
        {
            return status switch
            {
                AlertStatus.ChuaXuLy => "Cần được xử lý",
                AlertStatus.DaXuLy => "Đã xử lý",
                AlertStatus.DaHuy => "Đã hủy",
                _ => "Không xác định"
            };
        }
    }
}
