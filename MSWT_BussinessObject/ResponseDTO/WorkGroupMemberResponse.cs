using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class WorkGroupMemberResponse
    {
        public string WorkGroupMemberId { get; set; } = null!;

        public string? WorkGroupId { get; set; }

        public string? UserId { get; set; }

        public string? RoleId { get; set; }

        public DateTime? JoinedAt { get; set; }

        public DateTime? LeftAt { get; set; }
    }
}
