using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Services.IServices
{
    public interface ISensorService
    {
        #region CRUD Category
        Task<IEnumerable<Sensor>> GetAllSensors();
        Task<Sensor> GetSensorById(string id);
        Task AddSensor(Sensor sensor);
        Task UpdateSensor(Sensor sensor);
        Task DeleteSensor(string id);
        #endregion
    }
}
