using API_Authentication_BasicImplementation.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Authentication_BasicImplementation.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

       public DbSet<User> UserLoginDetails { get; set; }
    }
}
