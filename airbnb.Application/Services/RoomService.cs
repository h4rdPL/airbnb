using airbnb.Application.Common.Interfaces;
using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;

namespace airbnb.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }


        public async Task<Room> CreateOffer(CreateRoomOfferRequest offerRequest)
        {
            try
            {
                var response = await _roomRepository.CreateOffer(offerRequest);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("En error occured while trying to invoke repository", ex); 
            }
        }
    }
}
