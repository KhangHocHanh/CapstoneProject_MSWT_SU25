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
    public class SensorBinRepository : GenericRepository<SensorBin>, ISensorBinRepository
    {
        public SensorBinRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddAsync(SensorBin sensorBin)
        {
            _context.AddAsync(sensorBin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var sensorBin = await _context.SensorBins.FindAsync(id);
            if (sensorBin != null)
            {
                _context.SensorBins.Remove(sensorBin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SensorBin> GetByIdAsync(string id)
        {
            return await _context.SensorBins
                .Include(s => s.Bin)
                .Include(s => s.Sensor)
                .FirstOrDefaultAsync(s => s.SensorId == id);
        }

        async Task<IEnumerable<SensorBin>> ISensorBinRepository.GetAllAsync()
        {
            return await _context.SensorBins.Include(b => b.Bin).ToListAsync();
        }

        public async Task UpdateAsync(SensorBin sensorBin)
        {
            _context.SensorBins.Update(sensorBin);
            await _context.SaveChangesAsync();
        }
    }
}
