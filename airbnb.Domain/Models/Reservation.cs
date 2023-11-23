namespace airbnb.Domain.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FinalPrice { get; set; }
        public int ReservedGuests { get; set; }
        public bool IsCancelled { get; set; }
    }
}
