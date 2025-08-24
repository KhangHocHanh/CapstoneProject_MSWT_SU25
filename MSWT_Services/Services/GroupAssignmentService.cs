using AutoMapper;
using MSWT_BussinessObject.Model;
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
    public class GroupAssignmentService : IGroupAssignmentService
    {
        private readonly IGroupAssignmentRepository _groupAssignmentRepository;
        private readonly IMapper _mapper;

        public GroupAssignmentService(IGroupAssignmentRepository groupAssignmentRepository, IMapper mapper)
        {
            _groupAssignmentRepository = groupAssignmentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupAssignmentResponse>> GetAllGroupAssignments()
        {
            var groupAssignments = await _groupAssignmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GroupAssignmentResponse>>(groupAssignments);
        }

        public async Task<GroupAssignmentResponse> GetGroupAssignmentById(string id) 
        {
            var groupAssignment = await _groupAssignmentRepository.GetByIdAsync(id);
            if (groupAssignment == null) return null;

            return _mapper.Map<GroupAssignmentResponse>(groupAssignment);
        }
    }
}
