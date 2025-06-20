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
    public class ShiftRepository : GenericRepository<Shift>, IShiftRepository
    {
        public ShiftRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Shift> GetByIdAsync(string id)
        {
            return await _context.Shifts.FindAsync(id);
        }

        public async Task AddAsync(Shift shift)
        {
            _context.AddAsync(shift);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Shift>> IShiftRepository.GetAllAsync()
        {
            return await _context.Shifts.ToListAsync();
        }

        public async Task UpdateAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }
    }
}
