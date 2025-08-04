using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.Enum.Enum;

namespace MSWT_BussinessObject.RequestDTO
{
    public class RequestDTO
    {
        public class LoginRequestDTO
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public string Password { get; set; }
        }

        public class AlertRequestDTO
        {
            [Required(ErrorMessage = "Id thùng rác không được để trống")]
            public string? TrashBinId { get; set; }
        }



        #region RequestDTO

        public class RequestCreateDto
        {
            [Required(ErrorMessage = "Mô tả không được để trống")]
            public string? Description { get; set; }
            [Required(ErrorMessage = "Vị trí không được để trống")]
            public string? Location { get; set; }
            public DateTime? RequestDate { get; set; }
        }
        public class RequestUpdateStatusDto
        {
            public string RequestId { get; set; } = null!;
            public RequestStatusEnum Status { get; set; }
        }

        #endregion



        #region RoleDTO
        public class RoleCreateDto
        {
            [Required(ErrorMessage = "Tên vai trò không được để trống")]
            public string RoleName { get; set; } = null!;
            public string? Description { get; set; }
        }
        public class ReportWithRoleDto
        {
            public string ReportId { get; set; }
            public string? ReportType { get; set; }
            public string? Description { get; set; }
            public string? Status { get; set; }
            public string? ReportName { get; set; }
            public DateOnly? Date { get; set; }
            public string? Image { get; set; }
            public string? Priority { get; set; }

            public string? UserId { get; set; }
            public string? UserName { get; set; }
            public string? FullName { get; set; }

            public string? RoleName { get; set; } // <-- Lấy từ User.Role.RoleName
        }

        #endregion

        #region LeaveDTO
        public class LeaveCreateDto : IValidatableObject
        {
            [Required(ErrorMessage = "Loại nghỉ phép không được để trống.")]
            [EnumDataType(typeof(LeaveTypeEnum), ErrorMessage = "Loại nghỉ phép không hợp lệ.")]
            public LeaveTypeEnum LeaveType { get; set; }
            [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
            public DateOnly StartDate { get; set; }
            [Required(ErrorMessage = "Ngày bắt đầu không được để trống.")]
            public DateOnly EndDate { get; set; }
            [Required(ErrorMessage = "Lý do không được để trống.")]
            [MinLength(1, ErrorMessage = "Lý do không được để trống.")]
            public string? Reason { get; set; }
            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (StartDate > EndDate)
                {
                    yield return new ValidationResult(
                        "Ngày bắt đầu phải nhỏ hơn hoặc bằng ngày kết thúc.",
                        new[] { nameof(StartDate), nameof(EndDate) }
                    );
                }

                if (string.IsNullOrWhiteSpace(Reason))
                {
                    yield return new ValidationResult(
                        "Lý do không được để trống hoặc chỉ chứa khoảng trắng.",
                        new[] { nameof(Reason) }
                    );
                }

            }

        }
        public class LeaveApprovalDto
        {
            public ApprovalStatusEnum ApprovalStatus { get; set; }
            public string? Note { get; set; }
        }
        #endregion
        #region ReportDTO
        public class ReportCreateDto
        {
            public string Description { get; set; } = null!;
            public string ReportName { get; set; } = null!;
            public string? Image { get; set; }
            public PriorityReportEnum Priority { get; set; }
        }
        public class ReportCreateDtoWithType : ReportCreateDto
        {
            public ReportTypeEnum ReportType { get; set; }
        }
        public class ReportUpdateStatusDto
        {
            public ReportStatus NewStatus { get; set; }
        }
        #endregion
        #region UserDTO
        public class UserCreateDto
        {
            [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Mật khẩu không được để trống")]
            public string Password { get; set; }

            [Required(ErrorMessage = "Họ và tên không được để trống")]
            public string FullName { get; set; }

            [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
            [Required(ErrorMessage = "Số điện thoại không được để trống")]
            public string Phone { get; set; }

            public string? Email { get; set; }
            public string? Address { get; set; }
            public string? Image { get; set; }

            [Required(ErrorMessage = "Vai trò không được để trống")]
            public string RoleId { get; set; }
        }
        public class UserStatusUpdateDto
        {
            public string Note { get; set; } // Lưu lý do nghỉ việc nếu cần
        }
        public class UserUpdateProfileDto
        {
            [Required(ErrorMessage = "Họ và tên không được để trống")]
            public string FullName { get; set; }

            [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
            public string Phone { get; set; }

            [EmailAddress(ErrorMessage = "Email không hợp lệ")]
            public string? Email { get; set; }

            public string? Address { get; set; }
            public string? Image { get; set; }
        }

        public class ChangePasswordDto
        {
            [Required(ErrorMessage = "Mật khẩu cũ không được để trống")]
            public string OldPassword { get; set; } = null!;
            [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
            public string NewPassword { get; set; } = null!;
            [Required(ErrorMessage = "Xác nhận mật khẩu mới không được để trống")]  
            public string ConfirmNewPassword { get; set; } = null!;
        }


        #endregion

        #region SensorDTO
        public class SensorCreateDto
        {
            [Required(ErrorMessage = "Tên cảm biến không được để trống")]
            public string? SensorName { get; set; } = null!;
        }

        #endregion

        #region SensorBinDTO
        public class SensorBinCreateDto
        {
            [Required(ErrorMessage = "ID cảm biến không được để trống")]
            public string SensorId { get; set; } = null!;
            [Required(ErrorMessage = "ID thùng rác không được để trống")]
            public string BinId { get; set; } = null!;
        }

        #endregion

        #region TrashbinDTO
        public class TrashbinCreateDto
        {
            public string? AreaId { get; set; }
            [Required(ErrorMessage = "Vị trí thùng rác không được để trống")]
            public string? Location { get; set; }

            public string? Image { get; set; }

            public string? RestroomId { get; set; }
        }

        #endregion

        public class ScheduleDetailRatingCreateDTO
        {
            public string ScheduleDetailId { get; set; } = null!;
            public int RatingValue { get; set; }
            public string? Comment { get; set; }
        }
        public class ShiftSwapRequestCreateDTO
        {
            public DateTime RequesterScheduleDate { get; set; }
            public string TargetPhoneNumber { get; set; } = null!;
            public DateTime TargetScheduleDate { get; set; }
            public string? Reason { get; set; }
        }
        public class ShiftSwapRequestDTO
        {
            public string ToUserId { get; set; } = null!;
            public string? TargetUserPhone { get; set; } = null!;
            public string? Reason { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }
        #region AttendanceRecordDTO
        public class AttendanceRecordDTO
        {
            public string Id { get; set; }
            public string EmployeeId { get; set; } = null!;
            public DateOnly AttendanceDate { get; set; }
            public DateTime? CheckInTime { get; set; }
            public DateTime? CheckOutTime { get; set; }
            public string? Status { get; set; }
            public string? Note { get; set; }
        }

        public class CreateAttendanceRequest
        {
            public string EmployeeId { get; set; } = null!;
            public DateOnly AttendanceDate { get; set; }
        }

        public class CheckInRequest
        {
            public string EmployeeId { get; set; } = null!;
        }

        public class CheckOutRequest
        {
            public string EmployeeId { get; set; } = null!;
        }

        #endregion

    }
}
