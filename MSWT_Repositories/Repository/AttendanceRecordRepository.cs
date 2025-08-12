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
    public class AttendanceRecordRepository : GenericRepository<AttendanceRecord>, IAttendanceRecordRepository
    {
        private readonly SmartTrashBinandCleaningStaffManagementContext _context;

        public AttendanceRecordRepository(SmartTrashBinandCleaningStaffManagementContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AttendanceRecord?> GetTodayAttendanceAsync(string userId, DateOnly date)
        {
            return await _context.AttendanceRecords
                .FirstOrDefaultAsync(ar => ar.EmployeeId == userId && ar.AttendanceDate == date);
        }

        public async Task<List<AttendanceRecord>> GetRecordsByUserIdAsync(string userId)
        {
            return await _context.AttendanceRecords
                .Where(ar => ar.EmployeeId == userId)
                .OrderByDescending(ar => ar.AttendanceDate)
                .ToListAsync();
        }

        public async Task<List<AttendanceRecord>> GetAllWithUsersAsync()
        {
            return await _context.AttendanceRecords
                .Include(ar => ar.User)
                .OrderByDescending(ar => ar.AttendanceDate)
                .ToListAsync();
        }
        public async Task<AttendanceRecord?> GetByUserAndDateAsync(string userId, DateOnly date)
        {
            return await _context.AttendanceRecords
                .FirstOrDefaultAsync(r =>
                    r.EmployeeId == userId &&
                    r.AttendanceDate == date);
        }
        public async Task<List<AttendanceRecord>> GetRecordsByDateAsync(DateOnly date)
        {
            return await _context.AttendanceRecords
                .Where(r => r.AttendanceDate == date)
                .ToListAsync();
        }

        public Task<AttendanceRecord?> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<AttendanceRecord>> GetAllByDateAsync(DateOnly date)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> ExistsByUserAndDateAsync(string userId, DateOnly date)
        {
            return await _context.AttendanceRecords
                .AnyAsync(r => r.EmployeeId == userId && r.AttendanceDate == date);
        }
        public async Task<bool> HasMonthlyAttendanceRecordsAsync(int year, int month)
        {
            return await _context.AttendanceRecords.AnyAsync(r => r.AttendanceDate.Value.Year == year && r.AttendanceDate.Value.Month == month);
        }
        public async Task<List<AttendanceRecord>> GetRecordsByMonthAsync(int year, int month)
        {
            return await _context.AttendanceRecords
                .Include(r => r.User)
                .Where(r => r.AttendanceDate.Value.Year == year && r.AttendanceDate.Value.Month == month)
                .OrderBy(r => r.EmployeeId).ThenBy(r => r.AttendanceDate)
                .ToListAsync();
        }

    }
}
