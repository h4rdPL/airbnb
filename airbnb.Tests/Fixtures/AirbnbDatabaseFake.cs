using airbnb.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace airbnb.Tests.Fixtures
{
    public class AirbnbDatabaseFake
    {
        public AirbnbDbContext Context { get; }

        public AirbnbDatabaseFake()
        {
            var builder = new DbContextOptionsBuilder<AirbnbDbContext>();
            builder.UseInMemoryDatabase("airbnb");
            var dbContextOptions = builder.Options;
            Context = new AirbnbDbContext(dbContextOptions);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }
    }

}
