using airbnb.Domain.Enum;
using airbnb.Domain.Models;

namespace airbnb.Contracts.RoomsOffer
{
    public record struct ListOfRoomsResponse(
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
