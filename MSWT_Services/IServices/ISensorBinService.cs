using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Services.IServices
{
    public interface ISensorBinService
    {
        #region CRUD Category
        Task<IEnumerable<SensorBin>> GetAllSensorBins();
        Task<SensorBin> GetSensorBinById(string id);
        Task AddSensorBin(SensorBin sensorBin);
        Task UpdateSensorBin(SensorBin sensorBin);
        Task DeleteSensorBin(string id);
        #endregion
    }
}
