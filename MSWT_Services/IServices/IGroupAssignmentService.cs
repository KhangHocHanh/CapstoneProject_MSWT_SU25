using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IGroupAssignmentService
    {
        Task<IEnumerable<GroupAssignmentResponse>> GetAllGroupAssignments();
        Task<GroupAssignmentResponse> GetGroupAssignmentById(string id);
    }
}
