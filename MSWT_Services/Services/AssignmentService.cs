using MSWT_BussinessObject.Model;
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
        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }
        public async Task AddAssigment(Assignment assignment)
        {
            await _assignmentRepository.AddAsync(assignment);
        }

        public async Task DeleteAssigment(string id)
        {
            await _assignmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Assignment>> GetAllAssigments()
        {
            return await _assignmentRepository.GetAllAsync();
        }

        public async Task<Assignment> GetAssigmentById(string id)
        {
            return await _assignmentRepository.GetByIdAsync(id);
        }

        public async Task UpdateAssigment(Assignment assignment)
        {
            await _assignmentRepository.UpdateAsync(assignment);
        }
    }
}
