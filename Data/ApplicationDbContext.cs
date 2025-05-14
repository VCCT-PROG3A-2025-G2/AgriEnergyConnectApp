using AgriEnergyConnectApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AgriEnergyConnectApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
