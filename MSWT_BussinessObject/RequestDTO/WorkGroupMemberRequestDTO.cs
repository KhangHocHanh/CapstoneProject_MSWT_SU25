using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class WorkGroupMemberRequestDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public List<string> UserIds { get; set; }
    }
}
