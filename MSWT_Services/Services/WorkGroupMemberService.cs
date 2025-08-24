using AutoMapper;
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
        private readonly IMapper _mapper;

        public WorkGroupMemberService(IWorkGroupMemberRepository workGroupMemberRepository, IMapper mapper)
        {
            _workGroupMemberRepository = workGroupMemberRepository;
            _mapper = mapper;
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
    }
}
