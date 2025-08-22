using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.IServices
{
    public interface IWorkGroupMemberService
    {
        Task<WorkGroupMemberResponse> GetWorkGroupMemberById(string id);
        Task<(string? SupervisorUserId, List<WorkGroupMemberResponse> Members)> GetSupervisorAndMembersByWorkGroupIdAsync(string workGroupId);
        Task<IEnumerable<WorkGroupMemberResponse>> GetMembersByWorkGroupIdAsync(string workGroupId);
    }
}
