using airbnb.Domain.Enum;
using airbnb.Domain.Models;

namespace airbnb.Contracts.RoomsOffer
{
    public record struct CreateRoomOfferResponse(
        int Id,
        int UserId,
        HomeType HomeType,
        int TotalOccupancy,
        int TotalBedrooms,
        int TotalBathrooms,
        int Summary,
        Address Address,
        Amenities Amenities,
        int Price,
        DateTime PublishedAt
    );
}
