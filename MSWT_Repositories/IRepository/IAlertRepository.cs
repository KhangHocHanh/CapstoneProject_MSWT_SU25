using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_Repositories.IRepository
{
    public interface IAlertRepository : IGenericRepository<Alert>
    {
        #region CRUD Alert
        Task<IEnumerable<Alert>> GetAllAsync();
        Task<Alert> GetByIdAsync(string id);
        Task AddAsync(Alert alert);
        Task DeleteAsync(string id);
        Task UpdateAsync(Alert alert);
        Task<IEnumerable<Alert>> GetAlertsByUserId(string userId);
        #endregion
    }
}
