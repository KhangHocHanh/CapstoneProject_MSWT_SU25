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
        public class WorkGroupMemberRepository : GenericRepository<WorkGroupMember>, IWorkGroupMemberRepository
        {
            public WorkGroupMemberRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
            {
                _context = context;
            }

            public async Task<WorkGroupMember> GetByIdAsync(string id)
            {
                return await _context.WorkGroupMembers

            .FirstOrDefaultAsync(f => f.WorkGroupMemberId == id);
            }

            public async Task AddAsync(WorkGroupMember workGroupMember)
            {
                _context.AddAsync(workGroupMember);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(string id)
            {
                var workGroupMember = await _context.WorkGroupMembers.FindAsync(id);
                if (workGroupMember != null)
                {
                    _context.WorkGroupMembers.Remove(workGroupMember);
                    await _context.SaveChangesAsync();
                }
            }

            async Task<IEnumerable<WorkGroupMember>> IWorkGroupMemberRepository.GetAllAsync()
            {
                return await _context.WorkGroupMembers

            .ToListAsync();
            }

            public async Task UpdateAsync(WorkGroupMember workGroupMember)
            {
                _context.WorkGroupMembers.Update(workGroupMember);
                await _context.SaveChangesAsync();
            }

            public async Task<IEnumerable<WorkGroupMember>> GetByWorkGroupIdAsync(string workGroupId)
            {
                return await _context.WorkGroupMembers
                    .Where(m => m.WorkGroupId == workGroupId)
                    .ToListAsync();
            }   
        }
}