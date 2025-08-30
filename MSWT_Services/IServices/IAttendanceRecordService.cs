using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWT_BussinessObject.Model;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.IServices
{
    public interface IAttendanceRecordService
    {
        Task<(bool IsSuccess, string Message)> CheckInAsync(string userId);
        Task<(bool IsSuccess, string Message)> CheckOutAsync(string userId);
        Task<List<AttendanceRecord>> GetAttendanceRecordsByUserAsync(string userId);
        Task<List<AttendanceRecord>> GetAllAttendanceRecordsAsync();
        Task<IEnumerable<AttendanceRecordResponseDTO>> GetRecordsByDateAsync(DateOnly date);
        Task GenerateDailyRecordsAsync();
        Task<(bool IsSuccess, string Message)> CreateMonthlyAttendanceRecordsAsync(CreateMonthlyAttendanceRequest request);
        Task<(bool IsSuccess, string Message)> AddNewEmployeeAttendanceAsync(AddNewEmployeeAttendanceRequest request);
        Task<byte[]> ExportMonthlyAttendanceAsync(int year, int month);
        Task<IEnumerable<AttendanceRecordResponseDTO>> GetAllAttendanceRecordWithFullName();

        Task<bool> CheckInAsync(string supervisorId, string employeeId, string shift); // shift: "Morning" or "Afternoon"
                                                                                       // SCBS.Services/Interfaces/IAttendanceService.cs
        Task<List<User>> GetEmployeesAsync(string supervisorId);
        Task<bool> SaveAttendancesAsync(string supervisorId, List<string> presentEmployeeIds, string shift);

    }
}
