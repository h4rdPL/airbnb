using airbnb.Domain.Enum;
using airbnb.Domain.Models;

namespace airbnb.Contracts.RoomsOffer
{
    public record struct CreateRoomOfferRequest(
        HomeType HomeType,
        int TotalOcupancy,
        int TotalBedrooms,
        int TotalBathrooms,
        int Summary,
        Address Address,
        Amenities Amenities,
        int Price,
        DateTime PublishedAt
        );
}
