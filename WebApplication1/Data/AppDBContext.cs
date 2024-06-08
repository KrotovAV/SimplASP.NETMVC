using Microsoft.EntityFrameworkCore;
using WebApplication1.Entity;

namespace WebApplication1.Data
{
    //    dotnet ef migrations add InitialMigration 
    //    dotnet ef database update
    public class AppDBContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }
        public AppDBContext()
        {

        }
        public AppDBContext(DbContextOptions dbc) : base(dbc)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .Build();

            optionsBuilder.UseLazyLoadingProxies().
                    UseSqlServer(config.GetConnectionString("Connection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
