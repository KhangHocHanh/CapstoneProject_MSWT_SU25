using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;

namespace MSWT_Repositories.IRepository
{
    public interface IAttendanceRecordRepository : IGenericRepository<AttendanceRecord>
    {
# region old
        Task<List<AttendanceRecord>> GetRecordsByUserIdAsync(string userId);
        Task<List<AttendanceRecord>> GetAllWithUsersAsync();

        Task<AttendanceRecord?> GetByUserAndDateAsync(string userId, DateOnly date);
        Task<AttendanceRecord?> GetByIdAsync(string id);
        Task<List<AttendanceRecord>> GetAllByDateAsync(DateOnly date);
        Task<bool> ExistsByUserAndDateAsync(string userId, DateOnly date);
        Task<bool> HasMonthlyAttendanceRecordsAsync(int year, int month);
        Task<List<AttendanceRecord>> GetRecordsByMonthAsync(int year, int month);
        #endregion
        Task<AttendanceRecord?> GetAttendanceAsync(string employeeId, DateOnly date);
        Task AddAttendanceAsync(AttendanceRecord record);
        Task UpdateAttendanceAsync(AttendanceRecord record);
        Task<List<WorkGroupMember>> GetGroupMembersBySupervisorAsync(string supervisorId);
        Task SaveChangesAsync();
        Task<List<User>> GetEmployeesBySupervisorAsync(string supervisorId);
    }
}
