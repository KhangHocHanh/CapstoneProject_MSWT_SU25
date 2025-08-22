using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWT_Repositories.Repository
{
    public class GroupAssignmentRepository : GenericRepository<GroupAssignment>, IGroupAssignmentRepository
    {
        public GroupAssignmentRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<GroupAssignment> GetByIdAsync(string id)
        {
            return await _context.GroupAssignments

        .FirstOrDefaultAsync(f => f.GroupAssignmentId == id);
        }

        public async Task AddAsync(GroupAssignment groupAssignment)
        {
            _context.AddAsync(groupAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var groupAssignment = await _context.GroupAssignments.FindAsync(id);
            if (groupAssignment != null)
            {
                _context.GroupAssignments.Remove(groupAssignment);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<GroupAssignment>> IGroupAssignmentRepository.GetAllAsync()
        {
            return await _context.GroupAssignments
                .Include(m => m.Assignments)
                .ToListAsync();
        }

        public async Task UpdateAsync(GroupAssignment groupAssignment)
        {
            _context.GroupAssignments.Update(groupAssignment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GroupAssignment>> GetByAssignmentGroupIdAsync(string workGroupId)
        {
            var assignments = await _context.GroupAssignments
                .Include(m => m.Assignments)
                .Where(m => m.GroupAssignmentId == workGroupId)
                .ToListAsync();

            return assignments;
        }
    }
}
