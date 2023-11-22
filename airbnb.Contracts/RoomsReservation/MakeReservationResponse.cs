namespace airbnb.Contracts.RoomsReservation
{
    public record struct MakeReservationResponse(
            int UserId,
            int RoomId,
            DateTime StartDate,
            DateTime EndDate,
            int FinalPrice,
            int ReservedGuests
        );
}
