using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Reservations.Persistance
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReservationDbContext>
    {
        public ReservationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../Reservations.Api/appsettings.Development.json").Build();
            var builder = new DbContextOptionsBuilder<ReservationDbContext>();
            var connectionString = configuration.GetConnectionString("Default");
            builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            return new ReservationDbContext(builder.Options);
        }
    }
}
