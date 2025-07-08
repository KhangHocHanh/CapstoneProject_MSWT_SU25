using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.Enum
{
    public class Enum
    {
        // Request Status Enum
        public enum RequestStatusEnum
        {
            DaGui,      // Đã gửi
            DaXuLy,     // Đã xử lý
            DaHuy       // Đã hủy
        }
        // Role Status Enum
        public enum RoleStatus
        {
            DangHoatDong,// Đang hoạt động
            NgungHoatDong // Ngưng hoạt động
        }
        public enum SensorStatus
        {
            DangHoatDong,// Đang hoạt động
            NgungHoatDong // Ngưng hoạt động
        }
        public enum SensorBinStatus
        {
            DangHoatDong,// Đang hoạt động
            NgungHoatDong // Ngưng hoạt động
        }

        public enum TrashbinStatus
        {
            DangHoatDong,// Đang hoạt động
            NgungHoatDong // Ngưng hoạt động
        }
        /// <summary>
        /// Loại nghỉ phép do nhân viên chọn.
        /// </summary>
        public enum LeaveTypeEnum
        {
            /// <summary>Nghỉ do ốm đau, bệnh tật.</summary>
            NghiBenh = 1,

            /// <summary>Nghỉ phép hằng năm.</summary>
            NghiPhep = 2,

            /// <summary>Nghỉ việc cá nhân (việc nhà, đám tiệc, …).</summary>
            NghiViecCaNhan = 3
        }

        /// <summary>
        /// Trạng thái phê duyệt đơn nghỉ.
        /// </summary>
        public enum ApprovalStatusEnum
        {
            ChuaDuyet = 0,
            DaDuyet = 1,
            TuChoi = 2
        }

        /// <summary>Loại báo cáo.</summary>
        public enum ReportTypeEnum
        {
            SuCo = 1,
            NhanVien = 2
        }

        /// <summary>Mức độ ưu tiên.</summary>
        public enum PriorityReportEnum
        {
            Cao = 1,
            TrungBinh = 2,
            Thap = 3
        }

        /// <summary>Trạng thái xử lý báo cáo.</summary>
        public enum ReportStatus
        {
            DaGui =1, // Đã gửi
            DangXuLy = 2, // Đang xử lý
            DaHoanThanh = 3, // Đã hoàn thành
        }

        /// <summary>Trạng thái Chi tiết công việc</summary>
        public enum ScheduleDetailsStatus
        {
            SapToi = 1, // Sắp tới
            DangLam = 2, // Đang làm
            HoanTat = 3, // Hoàn tất
            ChuaHoanTat = 4, // Chưa hoàn tất
        }
        /// <summary>Trạng thái người dùng.</summary>
        public enum UserStatusEnum
        {
            ChuaXacThuc = 0, // Chưa xác thực
            NghiPhep = 1, // Đang nghỉ phép
            Trong = 2, // Đang trống lịch
            DaCoLich = 3, // Đã có lịch
            ThoiViec = 4 // Thôi việc
        }

        public enum ScheduleStatus
        {
            HangNgay = 1,
            DotXuat = 2,
        }

        public enum ShiftStatus
        {
            IsDeleted = 1,
            IsNotDeleted = 2,
        }

        public enum AreaStatus
        {
            Assigned = 1,
            NotAssigned = 2,
        }

        public enum RestroomStatus
        {
            HoatDong = 1,
            BaoTri = 2,
        }
    }
}
