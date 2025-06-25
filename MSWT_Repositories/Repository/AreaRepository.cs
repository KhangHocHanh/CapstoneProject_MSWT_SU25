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
    public class AreaRepository : GenericRepository<Area>, IAreaRepository
    {
        public AreaRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Area> GetByIdAsync(string id)
        {
            return await _context.Areas.FindAsync(id);
        }

        public async Task AddAsync(Area area)
        {
            _context.AddAsync(area);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var area = await _context.Areas.FindAsync(id);
            if (area != null)
            {
                _context.Areas.Remove(area);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Area>> IAreaRepository.GetAllAsync()
        {
            return await _context.Areas
        .Include(f => f.Floor)
        .ToListAsync();
        }

        public async Task UpdateAsync(Area area)
        {
            _context.Areas.Update(area);
            await _context.SaveChangesAsync();
        }
    }
}
