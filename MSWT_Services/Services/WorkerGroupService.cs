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
    public class WorkerGroupService : IWorkerGroupService
    {
        private readonly IWorkGroupMemberRepository _workGroupMemberRepository;
        private readonly IWorkerGroupRepository _workerGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public WorkerGroupService(IWorkGroupMemberRepository workGroupMemberRepository, IMapper mapper, IWorkerGroupRepository workerGroupRepository, IUnitOfWork unitOfWork)
        {
            _workGroupMemberRepository = workGroupMemberRepository;
            _mapper = mapper;
            _workerGroupRepository = workerGroupRepository;
            _unitOfWork = unitOfWork;
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
    }
}
