using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface ISensorRepository : IGenericRepository<Sensor>
    {
        #region CRUD Category
        Task<IEnumerable<Sensor>> GetAllAsync();
        Task<Sensor> GetByIdAsync(string id);
        Task AddAsync(Sensor sensor);
        Task DeleteAsync(string id);
        Task UpdateAsync(Sensor sensor);
        #endregion
    }
}
