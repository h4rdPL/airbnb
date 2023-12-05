using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Enum;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomRepository
    {
        Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offer);
        Task<MakeReservationResponse> MakeReservation(MakeReservationRequest newReservation);
        Task<Room> GetRoomById(int id);
        int CalculateStayDuration(DateTime startDate, DateTime endDate);
        Task<bool> CancelReservation(int reservationId);
        Task<List<ListOfRoomsResponse>> GetAllRoms();
        Task<bool> Remove(int roomId);
        Task<List<Room>> GetRoomsByHomeType(HomeType homeType);
    }
}
