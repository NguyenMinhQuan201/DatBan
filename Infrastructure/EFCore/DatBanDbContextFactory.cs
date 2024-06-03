using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructureta2.EF
{
    public class DatBanDbContextFactory : IDesignTimeDbContextFactory<DatBanDbContext>
    {
        public DatBanDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DatBanDb");


            var optionsBuilder = new DbContextOptionsBuilder<DatBanDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DatBanDbContext(optionsBuilder.Options);
        }
    }
}
