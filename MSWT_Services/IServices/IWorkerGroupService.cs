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
    public interface IWorkerGroupService
    {
        Task<WorkerGroupResponse> GetWorkerGroupById(string id);
        Task<IEnumerable<WorkerGroupResponse>> GetAllWorkerGroups();
        Task<WorkerGroupResponseDTO> CreateWorkerGroupAsync(CreateWorkerGroupRequest request);
        Task<WorkerGroupResponseDTO> GetWorkerGroupByIdAsync(string id);
        Task<IEnumerable<WorkerGroupResponseDTO>> GetAllWorkerGroupsAsync();
        Task<WorkerGroupResponseDTO> UpdateWorkerGroupAsync(string id, UpdateWorkerGroupRequest request);
        Task<bool> DeleteWorkerGroupAsync(string id);
        Task<IEnumerable<AvailableUserResponse>> GetAvailableUsersAsync();
        Task<bool> AddMembersToGroupAsync(string groupId, AddMembersToGroupRequest request);
        Task<bool> RemoveMemberFromGroupAsync(string groupId, string userId);
    }
}
