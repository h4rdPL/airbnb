using airbnb.Domain.Enum;
using airbnb.Domain.Models;

namespace airbnb.Contracts.RoomsOffer
{
    public record struct CreateRoomOfferResponse(
        int Id,
        HomeType HomeType,
        int TotalOcupancy,
        int TotalBedroom,
        int TotalBathrooms,
        int Summary,
        Address Address,
        Amenities Amenities,
        int Price, 
        DateTime PublishedAt
        );
}
