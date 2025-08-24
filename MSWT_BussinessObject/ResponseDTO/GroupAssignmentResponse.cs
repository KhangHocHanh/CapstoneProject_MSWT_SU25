using MSWT_BussinessObject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class GroupAssignmentResponse
    {
        public string GroupAssignmentId { get; set; } = null!;

        public string? AssignmentGroupName { get; set; }

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        //public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
    }
}
