using airbnb.Contracts.RoomsOffer;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomService
    {
        Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offerRequest);
    }
}
