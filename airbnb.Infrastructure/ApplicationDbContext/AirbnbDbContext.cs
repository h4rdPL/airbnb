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
}
