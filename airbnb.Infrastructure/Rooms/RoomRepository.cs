using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Models;
using airbnb.Infrastructure.ApplicationDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        /// <summary>
        /// Calculate how many day guests will stay
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public int CalculateStayDuration(DateTime startDate, DateTime endDate)
        {
            TimeSpan stayDuration = endDate - startDate;
            Console.WriteLine(stayDuration.ToString());
            return stayDuration.Days;
        }


        /// <summary>
        /// Cancel reservation
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> CancelReservation(int reservationId)
        {
            try
            {
                var reservation = await _context.Reservations.FindAsync(reservationId);
                if (reservation == null && reservation.IsCancelled)
                {
                    return false;
                }

                reservation.IsCancelled = true;
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw new Exception("En error occured while trying to cancel reservation", ex);
            }

        }

        /// <summary>
        /// Create Room
        /// </summary>
        /// <param name="offer">Room offer</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offer)
        {
            try
            {
                var myIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (myIdClaim == null)
                {
                    throw new Exception("Missing NameIdentifier claim.");
                }

                var userId = int.Parse(myIdClaim);


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

        public async Task<List<ListOfRoomsResponse>> GetAllRoms()
        {
            try
            {
                var result = await _context.Rooms
                    .Include(r => r.Address)
                    .Include(r => r.Amenities)
                    .ToListAsync();

                var listOfRoomsResponse = result.Select(room => new ListOfRoomsResponse(
                    room.HomeType,
                    room.TotalOccupancy,
                    room.TotalBedrooms,
                    room.TotalBathrooms,
                    room.Summary,
                    room.Address,
                    room.Amenities,
                    room.Price,
                    room.PublishedAt
                )).ToList();

                return listOfRoomsResponse;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                throw new Exception("An error occurred while returning the list of rooms.", ex);
            }
        }

        /// <summary>
        /// Get single room by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Room> GetRoomById(int id)
        {
            var result = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            return result;
        }

        /// <summary>
        /// Create Reservation
        /// </summary>
        /// <param name="newReservation">Room reservation</param>
        /// <returns></returns>
        public async Task<MakeReservationResponse> MakeReservation(MakeReservationRequest newReservation)
        {
            try
            {

                var myIdClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var userId = int.Parse(myIdClaim);

                var room = GetRoomById(newReservation.RoomId);
                var numberOfDays = CalculateStayDuration(newReservation.StartDate, newReservation.EndDate);

                var totalPriceForRoom = room.Result.Price * numberOfDays * newReservation.ReservedGuests;
                if(totalPriceForRoom < 0)
                {
                    throw new Exception("An error occured while reservation room");
                }
                var reservation = new Reservation
                {
                    UserId = userId,
                    RoomId = newReservation.RoomId,
                    StartDate = newReservation.StartDate,
                    EndDate = newReservation.EndDate,
                    FinalPrice = totalPriceForRoom,
                    ReservedGuests = newReservation.ReservedGuests
                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return new MakeReservationResponse(userId, reservation.RoomId, reservation.StartDate, reservation.EndDate, reservation.FinalPrice, reservation.ReservedGuests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while making a reservation: {ex}");
                throw;
            }
        }


           
    }
}
