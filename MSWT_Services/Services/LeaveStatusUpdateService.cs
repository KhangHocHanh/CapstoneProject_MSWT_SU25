using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.Enum;
using static MSWT_BussinessObject.Enum.Enum;
using MSWT_Services;

public class LeaveStatusUpdateService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public LeaveStatusUpdateService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await UpdateUserStatuses();
            await Task.Delay(TimeSpan.FromHours(24), stoppingToken); // chạy mỗi ngày
        }
    }

    private async Task UpdateUserStatuses()
    {
        using var scope = _serviceProvider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<SmartTrashBinandCleaningStaffManagementContext>();

        var today = DateOnly.FromDateTime(TimeHelper.GetNowInVietnamTime());

        // 1. Những đơn bắt đầu hôm nay → set trạng thái nghỉ phép
        var startingLeaves = await db.Leaves
            .Include(l => l.Worker)
            .Where(l => l.StartDate == today && l.ApprovalStatus == "Đã duyệt")
            .ToListAsync();

        foreach (var leave in startingLeaves)
        {
            if (leave.Worker != null)
            {
                leave.Worker.Status = UserStatusHelper.ToStringStatus(UserStatusEnum.NghiPhep);
            }
        }

        // 2. Những đơn kết thúc hôm qua → set trạng thái trống lịch
        var endingLeaves = await db.Leaves
            .Include(l => l.Worker)
            .Where(l => l.EndDate < today && l.ApprovalStatus == "Đã duyệt")
            .ToListAsync();

        foreach (var leave in endingLeaves)
        {
            if (leave.Worker != null)
            {
                leave.Worker.Status = UserStatusHelper.ToStringStatus(UserStatusEnum.HoatDong);
            }
        }

        await db.SaveChangesAsync();
    }
}
