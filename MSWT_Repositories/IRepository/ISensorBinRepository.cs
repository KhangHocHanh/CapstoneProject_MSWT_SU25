using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface ISensorBinRepository : IGenericRepository<SensorBin>
    {
        #region CRUD Category
        Task<IEnumerable<SensorBin>> GetAllAsync();
        Task<SensorBin> GetByIdAsync(string id);
        Task AddAsync(SensorBin sensorBin);
        Task DeleteAsync(string id);
        Task UpdateAsync(SensorBin sensorBin);
        #endregion
    }
}
