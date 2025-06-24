using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class LeaveStatusHelper
    {
        public static string ToVietnamese(this LeaveTypeEnum type)
        {
            return type switch
            {
                LeaveTypeEnum.NghiBenh => "Nghỉ bệnh",
                LeaveTypeEnum.NghiPhep => "Nghỉ phép",
                LeaveTypeEnum.NghiViecCaNhan => "Nghỉ việc cá nhân",
                _ => "Không xác định"
            };
        }

        public static string ToVietnamese(this ApprovalStatusEnum status)
        {
            return status switch
            {
                ApprovalStatusEnum.ChuaDuyet => "Chưa duyệt",
                ApprovalStatusEnum.DaDuyet => "Đã duyệt",
                ApprovalStatusEnum.TuChoi => "Từ chối",
                _ => "Không xác định"
            };
        }
    }
}
