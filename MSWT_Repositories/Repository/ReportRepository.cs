using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;

namespace MSWT_Repositories.Repository
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Report> GetByIdAsync(string id)
        {
            return await _context.Reports.FindAsync(id);
        }

        public async Task AddAsync(Report report)
        {
            _context.AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
            }
        }

        async Task<IEnumerable<Report>> IReportRepository.GetAllAsync()
        {
            return await _context.Reports
                .Include(r => r.User).ToListAsync();
        }

        public async Task UpdateAsync(Report report)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Report>> GetAllWithUserAndRoleAsync()
        {
            return await _context.Reports
                .Include(r => r.User)
                    .ThenInclude(u => u.Role)
                .ToListAsync();
        }

    }
}
