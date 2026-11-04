using JREST.Models;
using Microsoft.EntityFrameworkCore;

namespace JREST.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<HousingApplication> HousingApplications { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}