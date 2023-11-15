using airbnb.Domain.Enum;
using airbnb.Domain.Models;

namespace airbnb.Tests.Fixtures
{
    internal static class OfferFixture
    {
        internal static Room CreateTestRoom() => 
            new Room
            {
                Id = 999, 
                HomeType = HomeType.Villa, 
                TotalOccupancy = 1,
                TotalBedrooms = 3,
                TotalBathrooms = 4,
                Summary = 12,
                Address = new Address
                {
                    Id = 999, 
                    ZIPCode = "12-345",
                    City = "FictionalCity",
                    Street = "ImaginaryStreet",
                    ApartmentNumber = 42,
                },
                Amenities = new Amenities
                {
                    HasTV = true,
                    HasKitchen = true,
                    HasHeating = false,
                    HasInternet = true,
                    HasAirCon = true,
                },
                Price = 300,
                PublishedAt = DateTime.UtcNow,
                Reservations = new List<Reservation>
                {
                    new Reservation
                    {
                        Id = 1, 
                        UserId = 1, 
                        StartDate = DateTime.UtcNow.AddDays(5),
                        EndDate = DateTime.UtcNow.AddDays(10),
                        FinalPrice = 600,
                        ReservedGuests = 3,
                    },
                }
            };
    }
}
