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
    public class SensorRepository: GenericRepository<Sensor>, ISensorRepository
    {
        public SensorRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Sensor> GetByIdAsync(string id)
        {
            return await _context.Sensors.FindAsync(id);
        }

        public async Task AddAsync(Sensor Sensor)
        {
            _context.AddAsync(Sensor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var Sensor = await _context.Sensors.FindAsync(id);
            if (Sensor != null)
            {
                _context.Sensors.Remove(Sensor);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Sensor>> ISensorRepository.GetAllAsync()
        {
            return await _context.Sensors.ToListAsync();
        }

        public async Task UpdateAsync(Sensor Sensor)
        {
            _context.Sensors.Update(Sensor);
            await _context.SaveChangesAsync();
        }
    }
}
