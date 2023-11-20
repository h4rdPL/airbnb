using airbnb.Contracts.RoomsOffer;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomRepository
    {
        Task<CreateRoomOfferResponse> CreateOffer(CreateRoomOfferRequest offer);
    }
}
