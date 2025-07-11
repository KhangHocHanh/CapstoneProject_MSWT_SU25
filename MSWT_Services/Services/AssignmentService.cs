using AutoMapper;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.RequestDTO;
using MSWT_BussinessObject.ResponseDTO;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Services.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IAssignmentRepository _assignmentRepository;
        private readonly IMapper _mapper;
        public AssignmentService(IAssignmentRepository assignmentRepository, IMapper mapper)
        {
            _assignmentRepository = assignmentRepository;
            _mapper = mapper;
        }
        public async Task<AssignmentResponseDTO> CreateAssignmentAsync(AssignmentRequestDTO request)
        {
            var assignment = _mapper.Map<Assignment>(request);
            assignment.AssignmentId = Guid.NewGuid().ToString();

            await _assignmentRepository.AddAsync(assignment);

            return _mapper.Map<AssignmentResponseDTO>(assignment);
        }

        public async Task DeleteAssigment(string id)
        {
            await _assignmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AssignmentResponseDTO>> GetAllAssigments()
        {
            var assignments = await _assignmentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AssignmentResponseDTO>>(assignments);
        }

        public async Task<AssignmentResponseDTO> GetAssignmentById(string id)
        {
            var assignment = await _assignmentRepository.GetByIdAsync(id);
            if (assignment == null)
                throw new Exception("Assignment not found.");

            return _mapper.Map<AssignmentResponseDTO>(assignment);
        }


        public async Task UpdateAssigment(Assignment assignment)
        {
            await _assignmentRepository.UpdateAsync(assignment);
        }
    }
}
