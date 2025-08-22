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
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        public RoomRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Room?> GetByIdAsync(string id)
        {
            return await _context.Rooms
                .Include(r => r.Area)

                .FirstOrDefaultAsync(r => r.RoomId == id);
        }


        public async Task AddAsync(Room room)
        {
            _context.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var restroom = await _context.Rooms.FindAsync(id);
            if (restroom != null)
            {
                _context.Rooms.Remove(restroom);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Room>> IRoomRepository.GetAllAsync()
        {
            return await _context.Rooms
                .Include(r => r.Area)

                .ToListAsync();
        }

        public async Task UpdateAsync(Room restroom)
        {
            _context.Rooms.Update(restroom);
            await _context.SaveChangesAsync();
        }
    }
}
