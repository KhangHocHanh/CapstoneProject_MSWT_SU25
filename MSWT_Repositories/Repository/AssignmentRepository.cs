using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;

namespace MSWT_Repositories.Repository
{
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Assignment> GetByIdAsync(string id)
        {
            return await _context.Assignments.FindAsync(id);
        }

        public async Task AddAsync(Assignment assignment)
        {
            _context.AddAsync(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment != null)
            {
                _context.Assignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Assignment>> IAssignmentRepository.GetAllAsync()
        {
            return await _context.Assignments.ToListAsync();
        }

        public async Task UpdateAsync(string assignmentId, Assignment assignment)
        {
            if (assignmentId != assignment.AssignmentId)
            {
                throw new ArgumentException("Không tìm thấy công việc để chỉnh sửa");
            }
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Assignment>> GetByGroupAssignmentIdAsync(string groupAssignmentId)
        {
            return await _context.Assignments
                .Where(a => a.GroupAssignmentId == groupAssignmentId)
                .ToListAsync();
        }
    }
}
