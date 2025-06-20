using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        #region CRUD Request
        Task<IEnumerable<Request>> GetAllAsync();
        Task<Request> GetByIdAsync(string id);
        Task AddAsync(Request request);
        Task DeleteAsync(string id);
        Task UpdateAsync(Request request);
        #endregion
    }
}
