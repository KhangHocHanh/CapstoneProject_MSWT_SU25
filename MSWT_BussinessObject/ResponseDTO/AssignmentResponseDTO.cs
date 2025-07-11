using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_BussinessObject.ResponseDTO
{
    public class AssignmentResponseDTO
    {
        public string AssignmentId { get; set; } = null!;

        public string? AssigmentName { get; set; }

        public string? Description { get; set; }

        public string? TimesPerDay { get; set; }

        public string? Status { get; set; }

    }
}
