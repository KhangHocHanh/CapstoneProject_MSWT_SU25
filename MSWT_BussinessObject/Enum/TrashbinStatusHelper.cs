using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class TrashbinStatusHelper
    {
        public static string ToDisplayString(this TrashbinStatus status)
        {
            return status switch
            {
                TrashbinStatus.DangHoatDong => "Hoạt động",
                TrashbinStatus.NgungHoatDong => "Ngưng hoạt động",
                _ => "Không xác định"
            };
        }
    }
}
