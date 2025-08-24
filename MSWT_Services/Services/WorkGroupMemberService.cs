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

namespace MSWT_Services.Services
{
    public class WorkGroupMemberService : IWorkGroupMemberService
    {
        private readonly IWorkGroupMemberRepository _workGroupMemberRepository;
        private readonly IWorkerGroupRepository _workerGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkGroupMemberService(IWorkGroupMemberRepository workGroupMemberRepository, IMapper mapper, IWorkerGroupRepository workerGroupRepository, IUnitOfWork unitOfWork)
        {
            _workGroupMemberRepository = workGroupMemberRepository;
            _mapper = mapper;
            _workerGroupRepository = workerGroupRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<WorkGroupMemberResponse> GetWorkGroupMemberById(string id)
        {
            var workGroupMember = await _workGroupMemberRepository.GetByIdAsync(id);
            if (workGroupMember == null) return null;

            return _mapper.Map<WorkGroupMemberResponse>(workGroupMember);
        }

        public async Task<(string? SupervisorUserId, List<WorkGroupMemberResponse> Members)>
    GetSupervisorAndMembersByWorkGroupIdAsync(string workGroupId)
        {
            var members = await _workGroupMemberRepository.GetByWorkGroupIdAsync(workGroupId);

            if (members == null || !members.Any())
                throw new Exception("No members found for the given WorkGroupId.");

            // map entities -> responses (FullName comes from User.FullName via profile)
            var memberResponses = members.Where(m => !string.IsNullOrEmpty(m.UserId))
    .Select(m => new WorkGroupMemberResponse
    {
        WorkGroupMemberId = m.WorkGroupMemberId,
        WorkGroupId = m.WorkGroupId,
        UserId = m.UserId,
        RoleId = m.RoleId,
        JoinedAt = m.JoinedAt,
        LeftAt = m.LeftAt,
        FullName = m.User?.FullName
    }).ToList();
            var supervisor = memberResponses.FirstOrDefault(m => m.RoleId == "RL03");

            return (supervisor?.UserId, memberResponses);
        }



        // ✅ Return full mapped response DTO list
        public async Task<IEnumerable<WorkGroupMemberResponse>> GetMembersByWorkGroupIdAsync(string workGroupId)
        {
            var members = await _workGroupMemberRepository.GetByWorkGroupIdAsync(workGroupId);
            return _mapper.Map<IEnumerable<WorkGroupMemberResponse>>(members);
        }

        public async Task<IEnumerable<WorkGroupMemberResponse>> GetAllWorkGroup()
        {
            var members = await _workGroupMemberRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<WorkGroupMemberResponse>>(members);
        }

        public async Task<WorkerGroup> CreateWorkerGroupWithMembersAsync(WorkGroupMemberRequestDTO request)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var group = new WorkerGroup
                {
                    WorkerGroupId = Guid.NewGuid().ToString(),
                    WorkerGroupName = request.Name,
                    Description = request.Description,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.WorkerGroupRepository.AddAsync(group);

                bool supervisorAdded = false;

                foreach (var userId in request.UserIds)
                {
                    var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
                    if (user == null)
                        throw new Exception($"User {userId} not found.");

                    // use the user's actual role
                    var roleId = user.RoleId;

                    if (roleId == "RL03")
                    {
                        if (supervisorAdded)
                            throw new Exception("Only one supervisor (RL03) can be added to a worker group.");

                        supervisorAdded = true;
                    }

                    var member = new WorkGroupMember
                    {
                        WorkGroupMemberId = Guid.NewGuid().ToString(),
                        WorkGroupId = group.WorkerGroupId,
                        UserId = user.UserId,
                        RoleId = roleId,
                        JoinedAt = DateTime.Now
                    };

                    await _unitOfWork.WorkGroupMemberRepository.AddAsync(member);
                }

                await _unitOfWork.CommitAsync();
                return group;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }




    }
}
