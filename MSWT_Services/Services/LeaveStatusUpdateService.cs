using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSWT_BussinessObject.Model;
using MSWT_BussinessObject.Enum;
using static MSWT_BussinessObject.Enum.Enum;
using MSWT_Services;
using Microsoft.Extensions.Logging;
using MSWT_Services.IServices;

public class LeaveStatusUpdateService : BackgroundService
{
    private readonly ILogger<LeaveStatusUpdateService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(1); // check every 5 minutes

    public LeaveStatusUpdateService(ILogger<LeaveStatusUpdateService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("LeaveStatusUpdateService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var leavesService = scope.ServiceProvider.GetRequiredService<ILeaveService>();

                await leavesService.UpdateUsersOnLeaveAsync();

                _logger.LogInformation("Checked and updated leaves statuses at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating leaves statuses");
            }

            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("LeaveStatusUpdateService is stopping.");
    }
}
