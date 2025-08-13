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
using ClosedXML.Excel;
using MSWT_BussinessObject.ResponseDTO;
using static MSWT_BussinessObject.ResponseDTO.ResponseDTO;

namespace MSWT_Services.Services
{
    public class AttendanceRecordService : IAttendanceRecordService
    {
        private readonly IAttendanceRecordRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;
        private readonly IScheduleDetailsRepository _scheduleDetailRepo;

        public AttendanceRecordService(IAttendanceRecordRepository repo, IUserRepository userRepo, IMapper mapper, IScheduleDetailsRepository scheduleDetailRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
            _mapper = mapper;
            _scheduleDetailRepo = scheduleDetailRepo;
        }

        public async Task<(bool IsSuccess, string Message)> CheckInAsync(string userId)
        {
            var today = DateOnly.FromDateTime(TimeHelper.GetNowInVietnamTime());
            var now = TimeHelper.GetNowInVietnamTime();

            // Lấy ca làm hôm nay từ ScheduleDetail
            var scheduleDetail = await _scheduleDetailRepo
                .GetByWorkerAndDateAsync(userId, today);

            if (scheduleDetail == null)
                return (false, "Hôm nay bạn không có ca làm.");

            var startTime = scheduleDetail.StartTime.Value; // TimeOnly
            var currentTime = TimeOnly.FromDateTime(now);

            var record = await _repo.GetByUserAndDateAsync(userId, today);
            if (record == null)
                return (false, "Không tìm thấy dữ liệu chấm công hôm nay.");

            if (record.Status != "Chưa check in")
                return (false, "Bạn đã check in.");

            // Tính mốc thời gian
            var startAllow = startTime.AddMinutes(-30);
            var lateThreshold = startTime.AddMinutes(30);
            var maxAllow = startTime.AddHours(1);

            if (currentTime < startAllow || currentTime > maxAllow)
            {
                record.Status = "Vắng không phép";
                record.Note = "Không check in đúng giờ";
                record.UpdatedAt = now;
                await _repo.UpdateAsync(record);
                return (false, "Bạn đã quá giờ check in, bị tính vắng không phép.");
            }

            if (currentTime <= lateThreshold)
                record.Note = "Đúng giờ";
            else
                record.Note = "Đi trễ";

            record.CheckInTime = now;
            record.Status = "Chưa check out";
            record.UpdatedAt = now;

            await _repo.UpdateAsync(record);
            return (true, "Check in thành công.");
        }

        public async Task<(bool IsSuccess, string Message)> CheckOutAsync(string userId)
        {
            var today = DateOnly.FromDateTime(TimeHelper.GetNowInVietnamTime());
            var now = TimeHelper.GetNowInVietnamTime();

            var scheduleDetail = await _scheduleDetailRepo
                .GetByWorkerAndDateAsync(userId, today);

            if (scheduleDetail == null)
                return (false, "Hôm nay bạn không có ca làm.");

            var endTime = scheduleDetail.EndTime.Value; // TimeOnly
            var currentTime = TimeOnly.FromDateTime(TimeHelper.GetNowInVietnamTime());

            var record = await _repo.GetByUserAndDateAsync(userId, today);
            if (record == null || record.Status == "Chưa check in")
                return (false, "Bạn chưa check in.");

            if (record.CheckOutTime != null)
                return (false, "Bạn đã check out.");

            if (currentTime < endTime)
                return (false, "Chưa hết ca làm.");

            record.CheckOutTime = now;
            record.Status = "Hoàn thành";
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
        public async Task GenerateDailyRecordsAsync()
        {
            var today = DateOnly.FromDateTime(TimeHelper.GetNowInVietnamTime());
            var now = TimeHelper.GetNowInVietnamTime();

            var allUsers = await _userRepo.GetAllAsync();

            foreach (var user in allUsers)
            {
                if (!await _repo.ExistsByUserAndDateAsync(user.UserId, today))
                {
                    var record = new AttendanceRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        EmployeeId = user.UserId,
                        AttendanceDate = today,
                        Status = "Chưa check in",
                        CreatedAt = now,
                        UpdatedAt = now
                    };
                    await _repo.CreateAsync(record);
                }
            }
        }
        public async Task<(bool IsSuccess, string Message)> CreateMonthlyAttendanceRecordsAsync(CreateMonthlyAttendanceRequest request)
        {
            if (await _repo.HasMonthlyAttendanceRecordsAsync(request.Year, request.Month))
                return (false, $"Lịch điểm danh tháng {request.Month}/{request.Year} đã tồn tại.");

            var now = TimeHelper.GetNowInVietnamTime();
            var users = await _userRepo.GetAllAsync();
            var targetUsers = users.Where(u => u.RoleId == "RL03" || u.RoleId == "RL04" && u.Status != "Chưa xác thực").ToList();

            var daysInMonth = DateTime.DaysInMonth(request.Year, request.Month);
            var records = new List<AttendanceRecord>();

            for (int day = 1; day <= daysInMonth; day++)
            {
                var date = new DateOnly(request.Year, request.Month, day);
                if (request.NgayNghi.Contains(date) || date.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                foreach (var user in targetUsers)
                {
                    records.Add(new AttendanceRecord
                    {
                        Id = Guid.NewGuid().ToString(),
                        EmployeeId = user.UserId,
                        AttendanceDate = date,
                        Status = "Chưa check in",
                        CreatedAt = now,
                        UpdatedAt = now
                    });
                }
            }

            foreach (var record in records)
            {
                await _repo.CreateAsync(record);
            }

            return (true, $"Tạo lịch điểm danh tháng {request.Month}/{request.Year} thành công cho {targetUsers.Count} nhân viên.");
        }
        public async Task<(bool IsSuccess, string Message)> AddNewEmployeeAttendanceAsync(AddNewEmployeeAttendanceRequest request)
        {
            var user = await _userRepo.GetByIdAsync(request.UserId);
            if (user == null)
                return (false, "Không tìm thấy nhân viên.");

            if (!(user.RoleId == "RL03" || user.RoleId == "RL04") || user.Status == "Chưa xác thực")
                return (false, "Nhân viên không thuộc đối tượng cần tạo lịch.");

            var year = request.StartDate.Year;
            var month = request.StartDate.Month;

            if (!await _repo.HasMonthlyAttendanceRecordsAsync(year, month))
                return (false, "Tháng này chưa được tạo lịch tổng, không thể bổ sung.");

            var now = TimeHelper.GetNowInVietnamTime();
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var records = new List<AttendanceRecord>();

            for (var day = request.StartDate.Day; day <= daysInMonth; day++)
            {
                var date = new DateOnly(year, month, day);
                if (request.NgayNghi.Contains(date) || date.DayOfWeek == DayOfWeek.Sunday)
                    continue;

                if (await _repo.ExistsByUserAndDateAsync(user.UserId, date))
                    continue;

                records.Add(new AttendanceRecord
                {
                    Id = Guid.NewGuid().ToString(),
                    EmployeeId = user.UserId,
                    AttendanceDate = date,
                    Status = "Chưa check in",
                    CreatedAt = now,
                    UpdatedAt = now
                });
            }

            foreach (var record in records)
            {
                await _repo.CreateAsync(record);
            }

            return (true, $"Bổ sung lịch điểm danh cho nhân viên {user.UserName} từ {request.StartDate:dd/MM/yyyy} thành công.");
        }
        public async Task<byte[]> ExportMonthlyAttendanceAsync(int year, int month)
        {
            var records = await _repo.GetRecordsByMonthAsync(year, month);

            using var workbook = new ClosedXML.Excel.XLWorkbook();
            var worksheet = workbook.Worksheets.Add($"DiemDanh_{month:D2}_{year}");

            worksheet.Cell(1, 1).Value = $"Báo cáo điểm danh tháng {month:D2}/{year}";
            worksheet.Range(1, 1, 1, 7).Merge().Style.Font.SetBold().Font.FontSize = 16;

            worksheet.Cell(2, 1).Value = "STT";
            worksheet.Cell(2, 2).Value = "Mã NV";
            worksheet.Cell(2, 3).Value = "Tên NV";
            worksheet.Cell(2, 4).Value = "Ngày";
            worksheet.Cell(2, 5).Value = "Trạng thái";
            worksheet.Cell(2, 6).Value = "Ghi chú";
            worksheet.Cell(2, 7).Value = "CheckIn";
            worksheet.Cell(2, 8).Value = "CheckOut";

            int row = 3;
            int stt = 1;
            foreach (var r in records)
            {
                worksheet.Cell(row, 1).Value = stt++;
                worksheet.Cell(row, 2).Value = r.EmployeeId;
                worksheet.Cell(row, 3).Value = r.User?.UserName;
                worksheet.Cell(row, 4).Value = r.AttendanceDate.Value.ToString("dd/MM/yyyy");
                worksheet.Cell(row, 5).Value = r.Status;
                worksheet.Cell(row, 6).Value = r.Note;
                worksheet.Cell(row, 7).Value = r.CheckInTime?.ToString("HH:mm");
                worksheet.Cell(row, 8).Value = r.CheckOutTime?.ToString("HH:mm");
                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public async Task<IEnumerable<AttendanceRecordResponseDTO>> GetAllAttendanceRecordWithFullName()
        {
           var records = await _repo.GetAllWithUsersAsync();
           return _mapper.Map<IEnumerable<AttendanceRecordResponseDTO>>(records);
        }
    }
}
