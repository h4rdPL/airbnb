using airbnb.Domain.Models;

namespace airbnb.Tests.Fixtures
{
    public static class ReservationFixture
    {
        internal static Reservation CreateReservation() =>
        new Reservation
        {
            Id = 1,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(2),
            FinalPrice = 400,
            ReservedGuests = 1,
            IsCancelled = false,

        };
    }
}
