using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;
using MSWT_Services.IServices;
using static MSWT_BussinessObject.RequestDTO.RequestDTO;

namespace MSWT_Services.Services
{
    public class AttendanceRecordService : IAttendanceRecordService
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public AttendanceRecordService(IAttendanceRecordRepository repo, IUserRepository userRepo, IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<(bool IsSuccess, string Message)> CheckInAsync(string userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            var now = DateTime.Now;

            var existing = await _repo.GetByUserAndDateAsync(userId, today);
            if (existing != null)
            {
                return (false, "Bạn đã check in hôm nay rồi.");
            }

            // Điều kiện đi trễ
            TimeOnly checkInTime = TimeOnly.FromDateTime(now);
            string note = null;

            // Sáng: 05:00 -> 13:00
            if (checkInTime > new TimeOnly(5, 0) && checkInTime < new TimeOnly(13, 0))
            {
                if (checkInTime > new TimeOnly(5, 15)) // ví dụ quy định đi trễ nếu sau 5h15 sáng
                {
                    note = "Đi trễ";
                }
            }
            // Chiều: 13:00 -> 21:00
            else if (checkInTime > new TimeOnly(13, 0) && checkInTime < new TimeOnly(21, 0))
            {
                if (checkInTime > new TimeOnly(13, 15)) // ví dụ quy định đi trễ nếu sau 13h15 chiều
                {
                    note = "Đi trễ";
                }
            }

            var record = new AttendanceRecord
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId = userId,
                AttendanceDate = today,
                CheckInTime = now,
                Status = "Chưa check out",
                Note = note,
                CreatedAt = now,
                UpdatedAt = now
            };

            await _repo.CreateAsync(record);
            return (true, "Check in thành công.");
        }

        public async Task<(bool IsSuccess, string Message)> CheckOutAsync(string userId)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var record = await _repo.GetByUserAndDateAsync(userId, today);

            if (record == null)
                return (false, "Bạn chưa check in hôm nay.");

            if (record.CheckOutTime != null)
                return (false, "Bạn đã check out rồi.");

            var now = DateTime.Now;
            var hour = now.TimeOfDay;

            // Phân ca dựa vào thời gian CheckIn
            if (record.CheckInTime.HasValue)
            {
                var checkInHour = record.CheckInTime.Value.TimeOfDay;

                // Nếu thuộc ca 1 thì yêu cầu check out sau 13:00
                if (checkInHour >= TimeSpan.FromHours(5) && checkInHour < TimeSpan.FromHours(13))
                {
                    if (hour < TimeSpan.FromHours(13))
                    {
                        return (false, "Bạn chỉ có thể check out sau 13:00 cho ca sáng.");
                    }
                }
                // Nếu thuộc ca 2 thì yêu cầu check out sau 21:00
                else if (checkInHour >= TimeSpan.FromHours(13))
                {
                    if (hour < TimeSpan.FromHours(21))
                    {
                        return (false, "Bạn chỉ có thể check out sau 21:00 cho ca chiều.");
                    }
                }
                else
                {
                    return (false, "Không xác định được ca làm việc.");
                }
            }
            else
            {
                return (false, "Không có thời gian check in.");
            }

            record.CheckOutTime = now;
            record.Status = "Đã điểm danh xong";
            record.UpdatedAt = now;

            await _repo.UpdateAsync(record);
            return (true, "Check out thành công.");
        }
        public async Task<List<AttendanceRecord>> GetAttendanceRecordsByUserAsync(string userId)
        {
            return await _repo.GetRecordsByUserIdAsync(userId);
        }

        public async Task<List<AttendanceRecord>> GetAllAttendanceRecordsAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<List<AttendanceRecord>> GetRecordsByDateAsync(DateOnly date)
        {
            return await _repo.GetAllByDateAsync(date);
        }

        
    }
}
