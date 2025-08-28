using AutoMapper;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Repositories.Repository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.Services
{
    public class WorkerGroupService : IWorkerGroupService
    {
        private readonly IWorkGroupMemberRepository _workGroupMemberRepository;
        private readonly IWorkerGroupRepository _workerGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public WorkerGroupService(IWorkGroupMemberRepository workGroupMemberRepository, IMapper mapper, IWorkerGroupRepository workerGroupRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _workGroupMemberRepository = workGroupMemberRepository;
            _mapper = mapper;
            _workerGroupRepository = workerGroupRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<WorkerGroupResponse> GetWorkerGroupById(string id)
        {
            var workGroup = await _workerGroupRepository.GetByIdAsync(id);
            if (workGroup == null) return null;

            return _mapper.Map<WorkerGroupResponse>(workGroup);
        }

        public async Task<IEnumerable<WorkerGroupResponse>> GetAllWorkerGroups()
        {
            var workGroups = await _workerGroupRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WorkerGroupResponse>>(workGroups);
        }

        public async Task<ResponseDTO.WorkerGroupResponseDTO> CreateWorkerGroupAsync(RequestDTO.CreateWorkerGroupRequest request)
        {
            // Create new WorkerGroup
            var workerGroup = new WorkerGroup
            {
                WorkerGroupId = Guid.NewGuid().ToString(),
                WorkerGroupName = request.WorkerGroupName,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _workerGroupRepository.AddAsync(workerGroup);

            // Add members to the group
            if (request.MemberUserIds?.Any() == true)
            {
                // Verify all users are available (not in any other group)
                var unavailableUsers = await GetUnavailableUsersAsync(request.MemberUserIds);
                if (unavailableUsers.Any())
                {
                    throw new InvalidOperationException($"Some users are already in other groups: {string.Join(", ", unavailableUsers)}");
                }

                foreach (var userId in request.MemberUserIds)
                {
                    var member = new WorkGroupMember
                    {
                        WorkGroupMemberId = Guid.NewGuid().ToString(),
                        WorkGroupId = workerGroup.WorkerGroupId,
                        UserId = userId,
                        JoinedAt = DateTime.UtcNow
                    };

                    await _workGroupMemberRepository.AddAsync(member);
                }
            }

            // Return the created group with members
            return await GetWorkerGroupByIdAsync(workerGroup.WorkerGroupId);
        }

        public async Task<ResponseDTO.WorkerGroupResponseDTO> GetWorkerGroupByIdAsync(string id)
        {
            var workerGroup = await _workerGroupRepository.GetByIdAsync(id);
            if (workerGroup == null)
                return null;

            var members = await _workGroupMemberRepository.GetByWorkGroupIdAsync(id);

            return new WorkerGroupResponseDTO
            {
                WorkerGroupId = workerGroup.WorkerGroupId,
                WorkerGroupName = workerGroup.WorkerGroupName,
                Description = workerGroup.Description,
                CreatedAt = workerGroup.CreatedAt,
                Members = members.Select(m => new WorkGroupMemberResponseDTO
                {
                    WorkGroupMemberId = m.WorkGroupMemberId,
                    WorkGroupId = m.WorkGroupId,
                    UserId = m.UserId,
                    RoleId = m.RoleId,
                    JoinedAt = m.JoinedAt,
                    LeftAt = m.LeftAt,
                    UserName = m.User?.UserName,
                    UserEmail = m.User?.Email
                }).ToList()
            };
        }

        public async Task<IEnumerable<ResponseDTO.WorkerGroupResponseDTO>> GetAllWorkerGroupsAsync()
        {
            var workerGroups = await _workerGroupRepository.GetAllAsync();
            var responses = new List<WorkerGroupResponseDTO>();

            foreach (var group in workerGroups)
            {
                var groupResponse = await GetWorkerGroupByIdAsync(group.WorkerGroupId);
                responses.Add(groupResponse);
            }

            return responses;
        }

        public async Task<ResponseDTO.WorkerGroupResponseDTO> UpdateWorkerGroupAsync(string id, RequestDTO.UpdateWorkerGroupRequest request)
        {
            var workerGroup = await _workerGroupRepository.GetByIdAsync(id);
            if (workerGroup == null)
                return null;

            workerGroup.WorkerGroupName = request.WorkerGroupName;
            workerGroup.Description = request.Description;

            await _workerGroupRepository.UpdateAsync(workerGroup);

            return await GetWorkerGroupByIdAsync(id);
        }

        public async Task<bool> DeleteWorkerGroupAsync(string id)
        {
            // First, remove all members from the group
            var members = await _workGroupMemberRepository.GetByWorkGroupIdAsync(id);
            foreach (var member in members)
            {
                await _workGroupMemberRepository.DeleteAsync(member.WorkGroupMemberId);
            }

            // Then delete the group
            await _workerGroupRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<ResponseDTO.AvailableUserResponse>> GetAvailableUsersAsync()
        {
            // Get all users
            var allUsers = await _userRepository.GetAllAsync();

            // Get all users who are currently in groups (active members)
            var allMembers = await _workGroupMemberRepository.GetAllAsync();
            var usersInGroups = allMembers
                .Where(m => m.LeftAt == null) // Only active members
                .Select(m => m.UserId)
                .ToHashSet();

            // Filter out users who are already in groups
            var availableUsers = allUsers
       .Where(u => (u.RoleId == "RL03" || u.RoleId == "RL04") && !usersInGroups.Contains(u.UserId));

            return availableUsers.Select(u => new AvailableUserResponse
            {
                UserId = u.UserId,
                UserName = u.UserName,
                Email = u.Email
            });
        }

        public async Task<bool> AddMembersToGroupAsync(string groupId, RequestDTO.AddMembersToGroupRequest request)
        {
            // Verify group exists
            var group = await _workerGroupRepository.GetByIdAsync(groupId);
            if (group == null)
                return false;

            // Verify all users are available
            var unavailableUsers = await GetUnavailableUsersAsync(request.UserIds);
            if (unavailableUsers.Any())
            {
                throw new InvalidOperationException($"Some users are already in other groups: {string.Join(", ", unavailableUsers)}");
            }

            // Add members
            foreach (var userId in request.UserIds)
            {
                var member = new WorkGroupMember
                {
                    WorkGroupMemberId = Guid.NewGuid().ToString(),
                    WorkGroupId = groupId,
                    UserId = userId,
                    JoinedAt = DateTime.UtcNow
                };

                await _workGroupMemberRepository.AddAsync(member);
            }

            return true;
        }

        public async Task<bool> RemoveMemberFromGroupAsync(string groupId, string userId)
        {
            var members = await _workGroupMemberRepository.GetByWorkGroupIdAsync(groupId);
            var member = members.FirstOrDefault(m => m.UserId == userId && m.LeftAt == null);

            if (member == null)
                return false;

            member.LeftAt = DateTime.UtcNow;
            await _workGroupMemberRepository.UpdateAsync(member);

            return true;
        }
        private async Task<List<string>> GetUnavailableUsersAsync(IEnumerable<string> userIds)
        {
            var allMembers = await _workGroupMemberRepository.GetAllAsync();
            var usersInGroups = allMembers
                .Where(m => m.LeftAt == null && userIds.Contains(m.UserId))
                .Select(m => m.UserId)
                .ToList();

            return usersInGroups;
        }
    }
}
