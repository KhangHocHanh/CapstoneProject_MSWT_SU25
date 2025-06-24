using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class RequestStatusHelper
    {
        public static string ToStringStatus(RequestStatusEnum status)
        {
            return status switch
            {
                RequestStatusEnum.DaGui => "Đã gửi",
                RequestStatusEnum.DaXuLy => "Đã xử lý",
                RequestStatusEnum.DaHuy => "Đã hủy",
                _ => "Không xác định"
            };
        }

        public static RequestStatusEnum ToEnum(string status)
        {
            return status switch
            {
                "Đã gửi" => RequestStatusEnum.DaGui,
                "Đã xử lý" => RequestStatusEnum.DaXuLy,
                "Đã hủy" => RequestStatusEnum.DaHuy,
                _ => throw new ArgumentException("Trạng thái không hợp lệ", nameof(status))
            };
        }

    }
}
