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
    public class SensorService : ISensorService
    {
        private readonly ISensorRepository _SensorRepository;
        public SensorService(ISensorRepository SensorRepository)
        {
            _SensorRepository = SensorRepository;
        }

        public async Task AddSensor(Sensor Sensor)
        {
            await _SensorRepository.AddAsync(Sensor);
        }

        public async Task DeleteSensor(string id)
        {
            await _SensorRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Sensor>> GetAllSensors()
        {
            return await _SensorRepository.GetAllAsync();
        }

        public async Task<Sensor> GetSensorById(string id)
        {
            return await _SensorRepository.GetByIdAsync(id);
        }

        public async Task UpdateSensor(Sensor Sensor)
        {
            await _SensorRepository.UpdateAsync(Sensor);
        }
    }
}
