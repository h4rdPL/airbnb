using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomService
    {
        Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offerRequest);
        Task<MakeReservationResponse> MakeReservation(MakeReservationRequest reservationRequest);
    }
}
