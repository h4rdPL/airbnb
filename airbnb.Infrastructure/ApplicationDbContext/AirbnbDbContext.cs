using airbnb.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace airbnb.Infrastructure.ApplicationDbContext;

public class AirbnbDbContext : DbContext
{
    public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the relationship between User and Room
        modelBuilder.Entity<User>()
            .HasMany(u => u.Rooms)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Define the relationship between Room and Reservation
        modelBuilder.Entity<Room>()
            .HasMany(r => r.Reservations)
            .WithOne(re => re.Room)
            .HasForeignKey(re => re.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        // Define the relationship between User and Reservation without cascading delete
        modelBuilder.Entity<Reservation>()
            .HasOne(re => re.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(re => re.UserId)
            .OnDelete(DeleteBehavior.Restrict); // or .OnDelete(DeleteBehavior.NoAction)

    }
}
