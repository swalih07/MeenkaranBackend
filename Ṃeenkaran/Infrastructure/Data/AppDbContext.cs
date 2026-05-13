using Microsoft.EntityFrameworkCore;
using Ṃeenkaran.Domain.Entities.User;
using Ṃeenkaran.Domain.Entities.Admin;

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
        public DbSet<ActiveFishingFeed> ActiveFishingFeeds { get; set; }
        public DbSet<GuideBooking> GuideBookings { get; set; }
        public DbSet<GuideAvailability> GuideAvailabilities { get; set; }
        public DbSet<TripBookingRequest> TripBookingRequests { get; set; }
        public DbSet<CatchPost> CatchPosts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<GuideReview> GuideReviews { get; set; }
        public DbSet<GuideFeedback> GuideFeedbacks { get; set; }
        public DbSet<CommunityPost> CommunityPosts { get; set; }
        public DbSet<CommunityPostReport> CommunityPostReports { get; set; }
        public DbSet<FishingSpotReview> FishingSpotReviews { get; set; }
        public DbSet<SafetyAlert> SafetyAlerts { get; set; }
        public DbSet<SystemErrorLog> SystemErrorLogs { get; set; }
        public DbSet<SuspiciousLoginAttempt> SuspiciousLoginAttempts { get; set; }
        public DbSet<GuidePayment> GuidePayments { get; set; }
        public DbSet<PlatformPaymentSetting> PlatformPaymentSettings { get; set; }
        public DbSet<TripBooking> TripBookings => Set<TripBooking>();
        public DbSet<GuidePayout> GuidePayouts => Set<GuidePayout>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GuideBooking>()
                .HasOne(b => b.Guide)
                .WithMany()
                .HasForeignKey(b => b.GuideId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GuideBooking>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TripBookingRequest>()
                .HasOne(b => b.Guide)
                .WithMany()
                .HasForeignKey(b => b.GuideId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TripBookingRequest>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}