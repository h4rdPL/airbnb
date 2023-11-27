using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Enum;
using airbnb.Domain.Models;

namespace airbnb.Tests.Fixtures
{
    public class RoomFixture
    {
        public static CreateRoomOfferRequest CreateRoomRequestFixture() =>
            new CreateRoomOfferRequest
            {
                HomeType = HomeType.House,
                TotalOccupancy = 2,
                TotalBedrooms = 1,
                TotalBathrooms = 1,
                Summary = 300,
                Address = new Address
                {
                    ZIPCode = "12345",
                    City = "Example City",
                    Street = "Main Street",
                    ApartmentNumber = 101
                },
                Amenities = new Amenities
                {
                    HasTV = true,
                    HasKitchen = true,
                    HasHeating = true,
                    HasInternet = true,
                    HasAirCon = true
                },
                Price = 100,
                PublishedAt = DateTime.UtcNow
            };

        public static CreateRoomOfferResponse CreateRoomResponseFixture() =>
        new CreateRoomOfferResponse
        {
            Id = 1,
            HomeType = HomeType.House,
            TotalOccupancy = 2,
            TotalBedrooms = 1,
            TotalBathrooms = 1,
            Summary = 300,
            Address = new Address
            {
                ZIPCode = "12345",
                City = "Example City",
                Street = "Main Street",
                ApartmentNumber = 101
            },
            Amenities = new Amenities
            {
                HasTV = true,
                HasKitchen = true,
                HasHeating = true,
                HasInternet = true,
                HasAirCon = true
            },
            Price = 100,
            PublishedAt = DateTime.UtcNow
        };
        public static Room CreateRoomFixture() =>
        new Room
      {
          Id = 1,
          HomeType = HomeType.House,
          TotalOccupancy = 2,
          TotalBedrooms = 1,
          TotalBathrooms = 1,
          Summary = 300,
          Address = new Address
          {
              ZIPCode = "12345",
              City = "Example City",
              Street = "Main Street",
              ApartmentNumber = 101
          },
          Amenities = new Amenities
          {
              HasTV = true,
              HasKitchen = true,
              HasHeating = true,
              HasInternet = true,
              HasAirCon = true
          },
          Price = 100,
          PublishedAt = DateTime.UtcNow
      };
    };

}
