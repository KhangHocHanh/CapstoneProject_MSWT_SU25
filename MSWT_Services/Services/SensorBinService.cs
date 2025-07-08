using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;

namespace MSWT_Services.Services
{
    public class SensorBinService : ISensorBinService
    {
        private readonly ISensorBinRepository _sensorBinRepository;
        public SensorBinService(ISensorBinRepository sensorBinRepository ) 
        {
            _sensorBinRepository = sensorBinRepository;
        }

        public async Task AddSensorBin(SensorBin sensorBin)
        {
            await _sensorBinRepository.AddAsync(sensorBin);
        }

        public async Task DeleteSensorBin(string id)
        {
            await _sensorBinRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SensorBin>> GetAllSensorBins()
        {
            return await _sensorBinRepository.GetAllAsync();
        }

        public async Task<SensorBin> GetSensorBinById(string id)
        {
            return await _sensorBinRepository.GetByIdAsync(id);
        }

        public async Task UpdateSensorBin(SensorBin sensorBin)
        {
            await _sensorBinRepository.UpdateAsync(sensorBin);
        }
        // Implement methods for CRUD operations on SensorBin
    }
}
