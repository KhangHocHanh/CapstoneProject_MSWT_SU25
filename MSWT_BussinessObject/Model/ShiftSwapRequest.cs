using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.Model
{
    public partial class ShiftSwapRequest
    {
        public Guid SwapRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public string RequesterId { get; set; } = null!;
        public string? TargetUserId { get; set; }
        public string? TargetUserPhone { get; set; } 
        public string? Status { get; set; } = "Đã gửi";
        public DateTime? ConfirmedDate { get; set; }
        public string? Reason { get; set; }
        public bool? SwapExecuted { get; set; } = false;
        public int Month { get; set; }
        public int Year { get; set; }

        public virtual User? Requester { get; set; }
        public virtual User? TargetUser { get; set; }

    }
}
