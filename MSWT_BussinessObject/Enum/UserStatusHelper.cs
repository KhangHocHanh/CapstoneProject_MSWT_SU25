using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class UserStatusHelper
    {
        public static string ToStringStatus(UserStatusEnum status)
        {
            return status switch
            {
                UserStatusEnum.ChuaXacThuc => "Chưa xác thực",
                UserStatusEnum.NghiPhep=> "Đang nghỉ phép",
                UserStatusEnum.Trong => "Đang trống lịch",
                UserStatusEnum.DaCoLich => "Đã có lịch",
                UserStatusEnum.ThoiViec => "Đã thôi việc",
                _ => "Không xác định"
            };
        }

        public static UserStatusEnum ToEnum(string status)
        {
            status = status.ToLower().Trim(); // thêm dòng này để chuẩn hóa
            return status switch
            {
                "chưa xác thực" => UserStatusEnum.ChuaXacThuc,
                "đang nghỉ phép" => UserStatusEnum.NghiPhep,
                "đang trống lịch" => UserStatusEnum.Trong,
                "đã có lịch" => UserStatusEnum.DaCoLich,
                "đã thôi việc" => UserStatusEnum.ThoiViec,
                _ => throw new ArgumentException("Trạng thái không hợp lệ", nameof(status))
            };
        }

    }
}
