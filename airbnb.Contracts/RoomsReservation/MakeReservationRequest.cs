namespace airbnb.Contracts.RoomsReservation
{
    public record struct MakeReservationRequest(
            int RoomId,
            DateTime StartDate,
            DateTime EndDate,
            int ReservedGuests
        );
}
