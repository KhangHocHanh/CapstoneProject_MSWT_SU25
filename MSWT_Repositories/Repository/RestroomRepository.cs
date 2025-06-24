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
    public class RestroomRepository : GenericRepository<Restroom>, IRestroomRepository
    {
        public RestroomRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Restroom?> GetByIdAsync(string id)
        {
            return await _context.Restrooms
                .Include(r => r.Area)
                .Include(r => r.Floor)
                .FirstOrDefaultAsync(r => r.RestroomId == id);
        }


        public async Task AddAsync(Restroom restroom)
        {
            _context.AddAsync(restroom);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var restroom = await _context.Restrooms.FindAsync(id);
            if (restroom != null)
            {
                _context.Restrooms.Remove(restroom);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Restroom>> IRestroomRepository.GetAllAsync()
        {
            return await _context.Restrooms
                .Include(r => r.Area)
                .Include(r => r.Floor)
                .ToListAsync();
        }

        public async Task UpdateAsync(Restroom restroom)
        {
            _context.Restrooms.Update(restroom);
            await _context.SaveChangesAsync();
        }
    }
}
