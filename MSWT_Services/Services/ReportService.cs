using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;

namespace MSWT_Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task AddReport(Report report)
        {
            await _reportRepository.AddAsync(report);
        }

        public async Task DeleteReport(string id)
        {
            await _reportRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Report>> GetAllReports()
        {
            return await _reportRepository.GetAllAsync();
        }

        public async Task<Report> GetReportById(string id)
        {
            return await _reportRepository.GetByIdAsync(id);
        }

        public async Task UpdateReport(Report report)
        {
            await _reportRepository.UpdateAsync(report);
        }
        public async Task<IEnumerable<Report>> GetReportsByUserId(string userId)
        {
            var allReports = await _reportRepository.GetAllAsync();
            return allReports.Where(r => r.UserId == userId).OrderByDescending(r => r.Date); // nếu có CreatedDate
        }
        public async Task<List<Report>> GetAllReportsWithUserAndRole()
        {
            var reports = await _reportRepository.GetAllWithUserAndRoleAsync();
            return reports.ToList();
        }

    }
}
