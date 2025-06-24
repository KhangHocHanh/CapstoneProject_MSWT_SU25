using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.Enum
{
    public static class ReportStatusHelper
    {
        public static string ToVietnamese(this ReportTypeEnum type)
        {
            return type switch
            {
                ReportTypeEnum.NhanVien => "Báo cáo nhân viên",
                ReportTypeEnum.SuCo => "Báo cáo sự cố",
                _ => "Không xác định"
            };
        }

        public static string ToVietnamese(this PriorityReportEnum status)
        {
            return status switch
            {
                PriorityReportEnum.Cao => "Cao",
                PriorityReportEnum.TrungBinh => "Trung Bình",
                PriorityReportEnum.Thap => "Thấp",
                _ => "Không xác định"
            };
        }
        public static string ToVietnamese(this ReportStatus status)
        {
            return status switch
            {
                ReportStatus.DaGui => "Đã gửi",
                ReportStatus.DangXuLy => "Đang xử lý",
                ReportStatus.DaHoanThanh => "Đã hoàn thành",
                _ => "Không xác định"
            };
        }
        public static ReportStatus ToEnum(string status)
        {
            return status switch
            {
                "Đã gửi" => ReportStatus.DaGui,
                "Đang xử lý" => ReportStatus.DangXuLy,
                "Đã hoàn thành" => ReportStatus.DaHoanThanh,
                _ => throw new ArgumentException("Trạng thái không hợp lệ")
            };
        }

        public static bool CanUpdateStatus(ReportStatus current, ReportStatus target)
        {
            return (current, target) switch
            {
                (ReportStatus.DaGui, ReportStatus.DangXuLy) => true,
                (ReportStatus.DangXuLy, ReportStatus.DaHoanThanh) => true,
                _ => false
            };
        }
    }
}
