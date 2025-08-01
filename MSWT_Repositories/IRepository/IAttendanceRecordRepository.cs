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
        Task<List<AttendanceRecord>> GetRecordsByUserIdAsync(string userId);
        Task<List<AttendanceRecord>> GetAllWithUsersAsync();

        Task<AttendanceRecord?> GetByUserAndDateAsync(string userId, DateOnly date);
        Task<AttendanceRecord?> GetByIdAsync(string id);
        Task<List<AttendanceRecord>> GetAllByDateAsync(DateOnly date);
    }
}
