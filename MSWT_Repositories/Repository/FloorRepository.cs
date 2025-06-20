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
    public class FloorRepository : GenericRepository<Floor>, IFloorRepository
    {
        public FloorRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Floor> GetByIdAsync(string id)
        {
            return await _context.Floors.FindAsync(id);
        }

        public async Task AddAsync(Floor floor)
        {
            _context.AddAsync(floor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var floor = await _context.Floors.FindAsync(id);
            if (floor != null)
            {
                _context.Floors.Remove(floor);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Floor>> IFloorRepository.GetAllAsync()
        {
            return await _context.Floors.ToListAsync();
        }

        public async Task UpdateAsync(Floor floor)
        {
            _context.Floors.Update(floor);
            await _context.SaveChangesAsync();
        }
    }
}
