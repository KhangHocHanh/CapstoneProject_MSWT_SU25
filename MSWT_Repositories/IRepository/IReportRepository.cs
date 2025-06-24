using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        #region CRUD Category
        Task<IEnumerable<Report>> GetAllAsync();
        Task<Report> GetByIdAsync(string id);
        Task AddAsync(Report report);
        Task DeleteAsync(string id);
        Task UpdateAsync(Report report);
        #endregion
    }
}
