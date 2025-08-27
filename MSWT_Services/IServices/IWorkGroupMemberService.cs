using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface IWorkGroupMemberService
    {
        Task<WorkGroupMemberResponse> GetWorkGroupMemberById(string id);
        Task<(string? SupervisorUserId, List<WorkGroupMemberResponse> Members)> GetSupervisorAndMembersByWorkGroupIdAsync(string workGroupId);
        Task<IEnumerable<WorkGroupMemberResponse>> GetMembersByWorkGroupIdAsync(string workGroupId);
        Task<IEnumerable<WorkGroupMemberResponse>> GetAllWorkGroup();
        Task<WorkerGroup> CreateWorkerGroupWithMembersAsync(WorkGroupMemberRequestDTO request);
        Task<WorkGroupMemberResponseDTO> GetMemberByIdAsync(string id);
        Task<IEnumerable<WorkGroupMemberResponseDTO>> GetAllMembersAsync();
        Task<IEnumerable<WorkGroupMemberResponseDTO>> GetMembersByGroupIdAsync(string groupId);
        Task<WorkGroupMemberResponseDTO> UpdateMemberAsync(string id, UpdateWorkGroupMemberRequest request);
        Task<bool> DeleteMemberAsync(string id);
    }
}
