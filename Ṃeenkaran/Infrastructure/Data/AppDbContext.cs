using Microsoft.EntityFrameworkCore;
using Ṃeenkaran.Domain.Entities.User;

namespace Ṃeenkaran.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FishingSpot> FishingSpots { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<GuidePackage> GuidePackages { get; set; }
    }
}