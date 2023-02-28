using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Reservations.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservationDbContext>, IDesignTimeDbContextFactory<SecurityDbContext>
    {
        ReservationDbContext IDesignTimeDbContextFactory<ReservationDbContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Reservations.Api/appsettings.Development.json").Build();
            var builder = new DbContextOptionsBuilder<ReservationDbContext>();
            var connectionString = configuration.GetConnectionString("ReservationDb");
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new ReservationDbContext(builder.Options);
        }

        SecurityDbContext IDesignTimeDbContextFactory<SecurityDbContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Reservations.Api/appsettings.Development.json").Build();
            var builder = new DbContextOptionsBuilder<SecurityDbContext>();
            var connectionString = configuration.GetConnectionString("SecurityDb");
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new SecurityDbContext(builder.Options);
        }
    }
}
