using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;
using Microsoft.Extensions.Logging;

namespace airbnb.Infrastructure.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AirbnbDbContext _context;
        public RoomRepository(AirbnbDbContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateOffer(CreateRoomOfferRequest offer)
        {
            try
            {
                var newOffer = new Room
                {
                    HomeType = offer.HomeType,
                    TotalOccupancy = offer.TotalOcupancy,
                    TotalBedrooms = offer.TotalBathrooms,
                    TotalBathrooms = offer.TotalBathrooms,
                    Summary = offer.Summary,
                    Address = new Address
                    {
                        ZIPCode = offer.Address.ZIPCode,
                        Street = offer.Address.Street,
                        City = offer.Address.City,
                        ApartmentNumber = offer.Address.ApartmentNumber
                    },
                    Amenities = new Amenities
                    {
                        HasTV = true,
                        HasAirCon = true,
                        HasHeating = true,
                        HasInternet = true,
                        HasKitchen = true,
                    },
                    Price = offer.Price,
                    PublishedAt = offer.PublishedAt.ToLocalTime()
                };

                await _context.Rooms.AddAsync(newOffer);
                await _context.SaveChangesAsync();
                return newOffer;
            }
            catch (Exception ex)
            {
                // Return a more informative error message
                throw new Exception("An error occurred while processing the room offer. Please try again later.", ex);
            }
        }



    }
}
