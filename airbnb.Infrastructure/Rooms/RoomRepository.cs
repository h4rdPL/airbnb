using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace airbnb.Infrastructure.Rooms
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AirbnbDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoomRepository(AirbnbDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offer)
        {
            try
            {
                var myIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (myIdClaim == null)
                {
                    Console.WriteLine("Nie znaleziono Claima o typie NameIdentifier.");
                    throw new Exception("Missing NameIdentifier claim.");
                }

                var userId = int.Parse(myIdClaim);
                Console.WriteLine(userId);


                var newOffer = new Room
                {
                    UserId = userId,
                    HomeType = offer.HomeType,
                    TotalOccupancy = offer.TotalOccupancy,
                    TotalBedrooms = offer.TotalBedrooms,
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
                        HasTV = offer.Amenities.HasTV,
                        HasAirCon = offer.Amenities.HasAirCon,
                        HasHeating = offer.Amenities.HasHeating,
                        HasInternet = offer.Amenities.HasInternet,
                        HasKitchen = offer.Amenities.HasKitchen,
                    },
                    Price = offer.Price,
                    PublishedAt = offer.PublishedAt.ToLocalTime()
                };

                _context.Rooms.Add(newOffer);
                await _context.SaveChangesAsync();

                return new CreateRoomOfferResponse(newOffer.Id, newOffer.UserId, newOffer.HomeType, newOffer.TotalOccupancy, newOffer.TotalBedrooms, newOffer.TotalBathrooms, newOffer.Summary, newOffer.Address, newOffer.Amenities, newOffer.Price, newOffer.PublishedAt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("An error occurred while processing the room offer. Please try again later.", ex);
            }
        }

        public async Task<MakeReservationResponse> MakeReservation(MakeReservationRequest newReservation)
        {
            try
            {
                var myIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var userId = int.Parse(myIdClaim);

                var reservation = new Reservation
                {
                    UserId = userId,
                    RoomId = newReservation.RoomId,
                    StartDate = newReservation.StartDate,
                    EndDate = newReservation.EndDate,
                    FinalPrice = 400,
                    ReservedGuests = newReservation.ReservedGuests
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return new MakeReservationResponse(userId, reservation.UserId, reservation.StartDate, reservation.EndDate, reservation.FinalPrice, reservation.ReservedGuests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while making a reservation: {ex}");
                throw;
            }
        }


    }
}
