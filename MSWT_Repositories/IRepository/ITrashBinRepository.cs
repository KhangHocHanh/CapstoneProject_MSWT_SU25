using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Repositories.IRepository
{
    public interface ITrashBinRepository : IGenericRepository<TrashBin>
    {
        #region CRUD Category
        Task<IEnumerable<TrashBin>> GetAllAsync();
        Task<TrashBin> GetByIdAsync(string id);
        Task AddAsync(TrashBin TrashBin);
        Task DeleteAsync(string id);
        Task UpdateAsync(TrashBin TrashBin);
        Task<List<TrashBinWithSensorDTO>> GetTrashBinsWithSensorsAsync();
        #endregion
    }
}
