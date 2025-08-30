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
    public class BuildingRepository : GenericRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Building> GetByIdAsync(string id)
        {
            return await _context.Buildings
        .Include(f => f.Areas)

        .FirstOrDefaultAsync(f => f.BuildingId == id);
        }

        public async Task AddAsync(Building floor)
        {
            _context.AddAsync(floor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var floor = await _context.Buildings.FindAsync(id);
            if (floor != null)
            {
                _context.Buildings.Remove(floor);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Building>> IBuildingRepository.GetAllAsync()
        {
            return await _context.Buildings
        .Include(f => f.Areas)
        .OrderBy(f => f.BuildingName)
        .ToListAsync();
        }

        public async Task UpdateAsync(Building floor)
        {
            _context.Buildings.Update(floor);
            await _context.SaveChangesAsync();
        }
    }
}
