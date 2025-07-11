using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace MSWT_BussinessObject.Model
{
    public class SmartTrashDbContextFactory : IDesignTimeDbContextFactory<SmartTrashBinandCleaningStaffManagementContext>
    {
        public SmartTrashBinandCleaningStaffManagementContext CreateDbContext(string[] args)
        {
            // DI CHUYỂN lên thư mục chứa MSWT_API để lấy appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MSWT_API"))
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SmartTrashBinandCleaningStaffManagementContext>();
            var connectionString = configuration.GetConnectionString("DB");

            optionsBuilder.UseSqlServer(connectionString);

            return new SmartTrashBinandCleaningStaffManagementContext(optionsBuilder.Options);
        }
    }
}
