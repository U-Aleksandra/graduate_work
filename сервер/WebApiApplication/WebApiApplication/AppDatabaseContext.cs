using Microsoft.EntityFrameworkCore;
using WebApiApplication.Models;

namespace WebApiApplication
{
    public class AppDatabaseContext : DbContext
    {
        public AppDatabaseContext(DbContextOptions<AppDatabaseContext> options) 
            :base(options)
        { 
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Specialist> Specialists { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<NameService> NameServices { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<WorkSchedule> WorkSchedules { get; set; }
    }
}
