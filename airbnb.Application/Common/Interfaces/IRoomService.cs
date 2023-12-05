using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Enum;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomService
    {
        Task<bool> CancelReservation(int reservationId);
        Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offerRequest);
        Task<List<ListOfRoomsResponse>> GetAllRooms();
        Task<List<ListOfRoomsResponse>> GetRoomsByHomeType(HomeType homeType);
        Task<MakeReservationResponse> MakeReservation(MakeReservationRequest reservationRequest);
        Task<bool> RemoveRoomById(int roomId);
    }
}
