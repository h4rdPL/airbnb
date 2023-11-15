using airbnb.Contracts.RoomsOffer;
using airbnb.Domain.Models;

namespace airbnb.Application.Common.Interfaces
{
    public interface IRoomService
    {
        Task<Room> CreateOffer(CreateRoomOfferRequest offerRequest);
    }
}
