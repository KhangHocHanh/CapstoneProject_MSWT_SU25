using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class ResponseDTO
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public ResponseDTO(int status, string? message, object? data = null)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public class ReportWithUserNameDTO
        {
            public string ReportId { get; set; } = null!;
            public string? ReportType { get; set; }
            public string? Description { get; set; }
            public string? Status { get; set; }
            public string? ReportName { get; set; }
            public string? UserId { get; set; }
            public DateTime? CreatedAt { get; set; }
            public string? Image { get; set; }
            public string? Priority { get; set; }
            public DateTime? ResolvedAt { get; set; }

            // Chỉ lấy UserName
            public string? UserName { get; set; }
        }

        public class TrashbinWithAreaNameDTO
        {
            public string TrashBinId { get; set; } = null!;
            public string? AreaId { get; set; }
            public string? Description { get; set; }
            public string? Status { get; set; }
            public string? TrashBinName { get; set; }
            public string? AreaName { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }

        }
        public class AlertResponseDTO
        {
            public string AlertId { get; set; } = null!;

            public string? TrashBinId { get; set; }

            public DateTime? TimeSend { get; set; }

            public DateTime? ResolvedAt { get; set; }

            public string? UserId { get; set; }

            public string? Status { get; set; }
        }

        public class UserWithRoleDTO
        {
            public string UserId { get; set; } = null!;

            public string? UserName { get; set; }

            public string? Password { get; set; }

            public string? FullName { get; set; }

            public string? Email { get; set; }

            public string? Phone { get; set; }

            public DateOnly? CreateAt { get; set; }

            public string? Status { get; set; }

            public string? Address { get; set; }

            public string? Image { get; set; }

            public string? RoleId { get; set; }

            public double? Rating { get; set; }

            public string? ReasonForLeave { get; set; }
            public string? RoleName { get; set; }

            public string? Description { get; set; }
        }
        public class SensorMeasurementDto
        {
            public string SensorId { get; set; } = null!;
            public float FillLevel { get; set; }
        }
        public class ShiftSwapRespondDTO
        {
            public Guid RequestId { get; set; }
            public bool IsAccepted { get; set; }
        }
        public class ShiftSwapResponseDTO
        {
            public Guid SwapRequestId { get; set; }
            public DateTime RequestDate { get; set; }
            public string RequesterId { get; set; }
            public string TargetUserId { get; set; } = string.Empty;
            public string TargetUserPhone { get; set; } = string.Empty;
            public string RequesterScheduleDetailId { get; set; } = string.Empty;
            public string TargetScheduleDetailId { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public DateTime? ConfirmedDate { get; set; }
            public string Reason { get; set; } = string.Empty;
            public bool? SwapExecuted { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
        }

    }
}
