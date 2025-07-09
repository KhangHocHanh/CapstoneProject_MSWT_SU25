using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface IReportService
    {
        #region CRUD Category
        Task<IEnumerable<ReportWithUserNameDTO>> GetAllReports();
        Task<Report> GetReportById(string id);
        Task AddReport(Report report);
        Task UpdateReport(Report report);
        Task DeleteReport(string id);
        Task<IEnumerable<Report>> GetReportsByUserId(string userId);
        Task<List<Report>> GetAllReportsWithUserAndRole();

        #endregion
    }
}
