using airbnb.Contracts.RoomsOffer;
using airbnb.Contracts.RoomsReservation;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomRepository
    {
        Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offer);
        Task<MakeReservationResponse> MakeReservation(MakeReservationRequest newReservation);
    }
}
