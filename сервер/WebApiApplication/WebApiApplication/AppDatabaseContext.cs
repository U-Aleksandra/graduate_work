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
    }
}
