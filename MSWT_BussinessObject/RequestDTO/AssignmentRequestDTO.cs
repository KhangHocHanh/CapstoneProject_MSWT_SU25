using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.RequestDTO
{
    public class AssignmentRequestDTO
    {
        public string? AssigmentName { get; set; }

        public string? Description { get; set; }

        public string? TimesPerDay { get; set; }

        public string? Status { get; set; }
    }
}
