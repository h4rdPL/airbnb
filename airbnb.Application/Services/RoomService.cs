using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Enum;
using AutoMapper;

namespace airbnb.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Cancel reservation service
        /// </summary>
        /// <param name="reservationId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> CancelReservation(int reservationId)
        {
            var result = await _roomRepository.CancelReservation(reservationId);
            return result;
        }

        /// <summary>
        /// Create room for user
        /// </summary>
        /// <param name="offerRequest">Roon offer request</param>
        /// <returns>Room Response</returns>
        /// <exception cref="Exception"></exception>
        public async Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offerRequest)
        {
            try
            {
                var response = await _roomRepository.CreateOffer(offerRequest);

                var mappedResponse = _mapper.Map<CreateRoomOfferResponse>(response);

                return mappedResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw new Exception("An error occurred while processing the room offer. Please try again later.", ex);
            }
        }

        /// <summary>
        /// Get List of all rooms
        /// </summary>
        /// <returns>List<Room></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<ListOfRoomsResponse>> GetAllRooms()
        {
            try
            {
                var result = await _roomRepository.GetAllRoms();
                return result;
            } catch (Exception ex)
            {
                throw new Exception("En error occured while invoke repository", ex);
            }  
        }

        /// <summary>
        /// Gets a list of rooms based on the specified home type.
        /// </summary>
        /// <param name="homeType">The type of home to filter rooms by.</param>
        /// <returns>A list of rooms matching the specified home type.</returns>
        public async Task<List<ListOfRoomsResponse>> GetRoomsByHomeType(HomeType homeType)
        {
            try
            {
                var rooms = await _roomRepository.GetRoomsByHomeType(homeType);

                var response = rooms.Select(room => new ListOfRoomsResponse(
                    HomeType: room.HomeType,
                    TotalOccupancy: room.TotalOccupancy,
                    TotalBedrooms: room.TotalBedrooms,
                    TotalBathrooms: room.TotalBathrooms,
                    Summary: room.Summary,
                    Address: room.Address,
                    Amenities: room.Amenities,
                    Price: room.Price,
                    PublishedAt: room.PublishedAt
                )).ToList();

                return response;
            }
            catch (Exception ex)
            {
                // Możesz tutaj obsłużyć błędy biznesowe lub rzucić bardziej konkretne wyjątki
                throw new Exception($"An error occurred in the business logic: {ex.Message}");
            }
        }

        /// <summary>
        /// Nake Room reservation
        /// </summary>
        /// <param name="reservationRequest"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<MakeReservationResponse> MakeReservation(MakeReservationRequest reservationRequest)
        {
            try
            {
                var response = await _roomRepository.MakeReservation(reservationRequest);

                var mappedResponse = _mapper.Map<MakeReservationResponse>(response);

                return mappedResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while making a reservation: {ex}");
                throw new Exception("An error occurred while making a reservation :C", ex);
            }
        }

        /// <summary>
        /// Invoke repository
        /// </summary>
        /// <param name="roomId">single room id</param>
        /// <returns>true or false</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> RemoveRoomById(int roomId)
        {
            try
            {
                var response = await _roomRepository.Remove(roomId);
                return response;
            } catch (Exception ex)
            {
                throw new Exception("An error occured while tru to invoke the repository", ex);
            }
        }
    }
}
