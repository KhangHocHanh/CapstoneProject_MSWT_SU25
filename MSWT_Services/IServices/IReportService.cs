using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Services.IServices
{
    public interface IReportService
    {
        #region CRUD Category
        Task<IEnumerable<Report>> GetAllReports();
        Task<Report> GetReportById(string id);
        Task AddReport(Report report);
        Task UpdateReport(Report report);
        Task DeleteReport(string id);
        #endregion
    }
}
