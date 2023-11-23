using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
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


    }
}
