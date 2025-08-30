using MSWT_BussinessObject.Model;
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
        Task<List<GroupAssignment>> GetAllAsync();
        Task<GroupAssignment?> GetByIdAsync(string id);
        Task<GroupAssignment> CreateAsync(string name, string? description, List<string> assignmentIds);
        Task<GroupAssignment?> UpdateAsync(string id, string name, string? description, List<string> assignmentIds);
        Task<bool> DeleteAsync(string id);
    }
}
