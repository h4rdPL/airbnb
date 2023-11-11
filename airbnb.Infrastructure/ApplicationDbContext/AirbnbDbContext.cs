using airbnb.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace airbnb.Infrastructure.ApplicationDbContext;

public class AirbnbDbContext : DbContext
{
    public AirbnbDbContext(DbContextOptions<AirbnbDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
}
