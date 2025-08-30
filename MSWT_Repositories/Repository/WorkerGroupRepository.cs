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
    public class WorkerGroupRepository : GenericRepository<WorkerGroup>, IWorkerGroupRepository
    {
        public WorkerGroupRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<WorkerGroup> GetByIdAsync(string id)
        {
            return await _context.WorkerGroups

        .FirstOrDefaultAsync(f => f.WorkerGroupId == id);
        }

        public async Task AddAsync(WorkerGroup workerGroup)
        {
            _context.AddAsync(workerGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var workerGroup = await _context.WorkerGroups.FindAsync(id);
            if (workerGroup != null)
            {
                _context.WorkerGroups.Remove(workerGroup);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<WorkerGroup>> IWorkerGroupRepository.GetAllAsync()
        {
            return await _context.WorkerGroups
                .Include(m => m.WorkGroupMembers)
                .ThenInclude(u => u.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
        }

        public async Task UpdateAsync(WorkerGroup workerGroup)
        {
            _context.WorkerGroups.Update(workerGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<WorkerGroup> GetByIdWithMembersAsync(string id)
        {
            return await _context.WorkerGroups
                .Include(wg => wg.WorkGroupMembers)
                    .ThenInclude(wgm => wgm.User)
                .FirstOrDefaultAsync(f => f.WorkerGroupId == id);
        }

        public async Task<IEnumerable<WorkerGroup>> GetAllWithMembersAsync()
        {
            return await _context.WorkerGroups
                 .Include(wg => wg.WorkGroupMembers)
                     .ThenInclude(wgm => wgm.User)
                 .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
