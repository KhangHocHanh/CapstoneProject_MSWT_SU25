using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MSWT_Repositories.IRepository;
using MSWT_BussinessObject.Model;
using MSWT_Repositories.IRepository;

namespace MSWT_Services.Services
{
    public class UserStatusUpdateService : BackgroundService
    {
        private readonly ILogger<UserStatusUpdateService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromHours(1); // check every hour

        public UserStatusUpdateService(ILogger<UserStatusUpdateService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                        var scheduleDetailsRepository = scope.ServiceProvider.GetRequiredService<IScheduleDetailsRepository>();

                        var users = await userRepository.GetAllAsync();
                        var scheduleDetails = await scheduleDetailsRepository.GetAllAsync();

                        foreach (var user in users)
                        {
                            var isAssigned = scheduleDetails.Any(sd =>
                                sd.WorkerId == user.UserId ||
                                sd.SupervisorId == user.UserId);
                                //sd.BackupForUserId == user.UserId)

                            var newStatus = isAssigned ? "Yes" : "No";

                            if (user.IsAssigned != newStatus)
                            {
                                user.IsAssigned = newStatus;
                                await userRepository.UpdateAsync(user);
                                _logger.LogInformation($"Updated user {user.UserId} Status to: {newStatus}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while updating user status.");
                }

                await Task.Delay(_interval, stoppingToken);
            }
        }
    }
}
