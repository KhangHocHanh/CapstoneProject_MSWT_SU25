using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class RoleStatusHelper
    {
        public static string ToDisplayString(this RoleStatus status)
        {
            return status switch
            {
                RoleStatus.DangHoatDong => "Hoạt động",
                RoleStatus.NgungHoatDong => "Ngưng hoạt động",
                _ => "Không xác định"
            };
        }
    }
}
