using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

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
            public string? FullName { get; set; }
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
            public string BinId { get; set; } = null!;
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

        public class TrashBinWithSensorDTO
        {
            public string TrashBinId { get; set; } = null!;
            public string? Status { get; set; }
            public string? AreaId { get; set; }
            public string? Location { get; set; }
            public string? Image { get; set; }
            public string? RestroomId { get; set; }

            public List<SensorInfoDTO> Sensors { get; set; } = new();
        }
        public class SensorInfoDTO
        {
            public string SensorId { get; set; } = null!;
            public string? SensorName { get; set; } = null!;

        }
        public class AlertTrashBinDTO
        {
            public string AlertId { get; set; } = null!;

            public string? TrashBinId { get; set; }

            public DateTime TimeSend { get; set; }

            public DateTime? ResolvedAt { get; set; }

            public string? UserId { get; set; }

            public string? Status { get; set; }
            public string? AreaName { get; set; }
        }
        public class LeafDTO
        {
            public string LeaveId { get; set; } = null!;
            public string WorkerId { get; set; } = null!;
            public string FullName { get; set; } = null!;
            public string LeaveType { get; set; } = null!;
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
            public int TotalDays { get; set; }
            public string? Reason { get; set; }
            public DateOnly RequestDate { get; set; }
            public string ApprovalStatus { get; set; } = null!;
            public string? ApprovedBy { get; set; }
            public DateOnly? ApprovalDate { get; set; }
            public string? Note { get; set; }
            
        }
        public class AttendanceRecordResponseDTO
        {
            public string Id { get; set; } = null!;
            public string? EmployeeId { get; set; }
            public string? FullName { get; set; }
            public DateOnly? AttendanceDate { get; set; }
            public DateTime? CheckInTime { get; set; }
            public DateTime? CheckOutTime { get; set; }
            public string? Status { get; set; }
            public string? Note { get; set; }
            public DateTime? CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
        public class RequestResponseDTO
        {
            public string RequestId { get; set; } = null!;

            public string? WorkerId { get; set; }
            public string? WorkerName { get; set; }

            public string? Description { get; set; }

            public string? Status { get; set; }

            public DateTime? RequestDate { get; set; }

            public DateTime? ResolveDate { get; set; }

            public string? Location { get; set; }

            public string? SupervisorId { get; set; }

            public string? TrashBinId { get; set; }
        }
        public class AreaWithBuildingNameResponseDTO
        {
            public string AreaId { get; set; } = null!;

            public string? BuildingId { get; set; }

            public string? Description { get; set; }

            public string? Status { get; set; }

            public string? AreaName { get; set; }
            public string? BuildingName { get; set; }

        }

        #region WorkerGroup
        public class WorkerGroupResponseDTO
        {
            public string WorkerGroupId { get; set; } = null!;
            public string? WorkerGroupName { get; set; }
            public string? Description { get; set; }
            public DateTime? CreatedAt { get; set; }
            public List<WorkGroupMemberResponseDTO>? Members { get; set; }
        }

        public class WorkGroupMemberResponseDTO
        {
            public string WorkGroupMemberId { get; set; } = null!;
            public string? WorkGroupId { get; set; }
            public string? UserId { get; set; }
            public string? RoleId { get; set; }
            public DateTime? JoinedAt { get; set; }
            public DateTime? LeftAt { get; set; }
            public string? UserName { get; set; } // From User navigation property
            public string? UserEmail { get; set; } // From User navigation property
        }

        public class AvailableUserResponse
        {
            public string UserId { get; set; } = null!;
            public string? UserName { get; set; }
            public string? Email { get; set; }
            // Add other user properties as needed
        }
        #endregion
    }
}
