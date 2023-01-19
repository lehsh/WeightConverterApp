using Microsoft.EntityFrameworkCore;
using WeightConverterApp.Model.Entity;

namespace WeightConverterApp.Model
{
    public class WeightConverterDbContext: DbContext
    {
        public DbSet<KnownHost> KnownHosts { get; set; }
        public DbSet<Request> Requests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DbConnectionString"));
        }
    }
}
