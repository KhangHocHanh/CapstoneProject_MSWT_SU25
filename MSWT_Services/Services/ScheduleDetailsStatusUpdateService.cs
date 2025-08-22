//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using MSWT_Services.IServices;
//using Microsoft.Extensions.DependencyInjection;

//public class ScheduleDetailsStatusUpdateService : BackgroundService
//{
//    private readonly ILogger<ScheduleDetailsStatusUpdateService> _logger;
//    private readonly IServiceProvider _serviceProvider;
//    private readonly TimeSpan _interval = TimeSpan.FromMinutes(1); // check every minute

//    public ScheduleDetailsStatusUpdateService(ILogger<ScheduleDetailsStatusUpdateService> logger, IServiceProvider serviceProvider)
//    {
//        _logger = logger;
//        _serviceProvider = serviceProvider;
//    }

//    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//    {
//        _logger.LogInformation("ScheduleDetailsStatusUpdateService is starting.");

//        while (!stoppingToken.IsCancellationRequested)
//        {
//            try
//            {
//                using var scope = _serviceProvider.CreateScope();
//                var scheduleDetailsService = scope.ServiceProvider.GetRequiredService<IScheduleDetailsService>();

//                await scheduleDetailsService.UpdateScheduleDetailStatusesAsync();

//                _logger.LogInformation("Checked and updated schedule statuses at: {time}", DateTimeOffset.Now);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError(ex, "Error while updating scheduleDetails statuses");
//            }

//            await Task.Delay(_interval, stoppingToken);
//        }

//        _logger.LogInformation("ScheduleDetailsStatusUpdateService is stopping.");
//    }
//}
