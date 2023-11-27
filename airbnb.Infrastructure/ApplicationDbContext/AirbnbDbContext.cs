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
    public DbSet<Comment> Comments { get; set; }
    public DbSet<RoomComment> RoomComments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Rooms)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Reservations)
            .WithOne(re => re.Room)
            .HasForeignKey(re => re.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reservation>()
            .HasOne(re => re.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(re => re.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomComment>()
            .HasOne(rc => rc.Comment)
            .WithMany(c => c.RoomComments)
            .HasForeignKey(rc => rc.CommentId)
            .OnDelete(DeleteBehavior.NoAction); 

    }

}
