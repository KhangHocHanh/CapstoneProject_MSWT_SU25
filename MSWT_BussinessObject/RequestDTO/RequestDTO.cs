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





        #region RequestDTO

        public class RequestCreateDto
        {
            public string? Description { get; set; }
            public string? Location { get; set; }
            public DateOnly? RequestDate { get; set; }
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
            [Required(ErrorMessage = "RoleName không được để trống")]
            public string RoleName { get; set; } = null!;
            public string? Description { get; set; }
        }
        #endregion

        #region LeaveDTO
        public class LeaveCreateDto
        {
            public LeaveTypeEnum LeaveType { get; set; }

            public DateOnly StartDate { get; set; }

            public DateOnly EndDate { get; set; }

            public string? Reason { get; set; }
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
    }
}
