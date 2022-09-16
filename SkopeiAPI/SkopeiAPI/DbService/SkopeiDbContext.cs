using Microsoft.EntityFrameworkCore;
using SkopeiAPI.Models;

namespace SkopeiAPI.DbService
{
    // The DkopeiDbContext is the class which derives from the EF Core DbContext class
    // and therefore can interact with the database
    public class SkopeiDbContext : DbContext
    {
        public SkopeiDbContext(DbContextOptions<SkopeiDbContext> dbContextOptions) :
            base(dbContextOptions) 
        { 
        }

        // Instantiating the DbSets with their DB table names
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
