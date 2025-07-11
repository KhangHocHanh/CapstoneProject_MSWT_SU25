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

    }
}
