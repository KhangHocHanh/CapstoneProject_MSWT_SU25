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
    public class LeafRepository : GenericRepository<Leaf>, ILeafRepository
    {
        public LeafRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Leaf> GetByIdAsync(string id)
        {
            return await _context.Leaves.FindAsync(id);
        }

        public async Task AddAsync(Leaf Leaf)
        {
            _context.AddAsync(Leaf);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var Leaf = await _context.Leaves.FindAsync(id);
            if (Leaf != null)
            {
                _context.Leaves.Remove(Leaf);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Leaf>> ILeafRepository.GetAllAsync()
        {
            return await _context.Leaves
                .Include(u => u.Worker)
                .OrderByDescending(u => u.RequestDate)
                .ToListAsync();
        }

        public async Task UpdateAsync(Leaf Leaf)
        {
            _context.Leaves.Update(Leaf);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Leaf>> GetLeavesByUserId(string userId)
        {
            return await _context.Leaves
                .Where(l => l.WorkerId == userId)
                .OrderByDescending(l => l.StartDate)
                .ToListAsync();
        }

    }
}
