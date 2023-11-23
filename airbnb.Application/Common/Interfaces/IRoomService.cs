using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomService
    {
        Task<bool> CancelReservation(int reservationId);
        Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offerRequest);
        Task<MakeReservationResponse> MakeReservation(MakeReservationRequest reservationRequest);
    }
}
