using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Repositories.Repository
{
    public class TrashBinRepository : GenericRepository<TrashBin>, ITrashBinRepository
    {
        public TrashBinRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<TrashBin> GetByIdAsync(string id)
        {
            return await _context.TrashBins.FindAsync(id);
        }

        public async Task AddAsync(TrashBin TrashBin)
        {
            _context.AddAsync(TrashBin);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var TrashBin = await _context.TrashBins.FindAsync(id);
            if (TrashBin != null)
            {
                _context.TrashBins.Remove(TrashBin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TrashBin>> GetAllAsync()
        {
            return await _context.TrashBins
                .Include(t => t.Area)
                .OrderBy(t => t.Location)
                .ToListAsync();
        }

        public async Task UpdateAsync(TrashBin TrashBin)
        {
            _context.TrashBins.Update(TrashBin);
            await _context.SaveChangesAsync();
        }
        public async Task<List<TrashBinWithSensorDTO>> GetTrashBinsWithSensorsAsync()
        {
            var result = await _context.SensorBins
       .Include(sb => sb.Bin)
       .Include(sb => sb.Sensor)
       .GroupBy(sb => sb.Bin.TrashBinId)
       .Select(g => new TrashBinWithSensorDTO
       {
           TrashBinId = g.Key,
           Status = g.First().Bin.Status,
           AreaId = g.First().Bin.AreaId,
           Location = g.First().Bin.Location,
           Image = g.First().Bin.Image,
           RestroomId = g.First().Bin.RestroomId,
           Sensors = g.Select(sb => new SensorInfoDTO
           {
               SensorId = sb.Sensor.SensorId,
               SensorName = sb.Sensor.SensorName
           }).ToList()
       })
       .ToListAsync();

            return result;
        }
    }
}
