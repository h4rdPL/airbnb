using airbnb.Domain.Enum;

namespace airbnb.Domain.Models
{
    public class Room
    {
        public int Id { get; set; }
        public HomeType HomeType { get; set; }
        public int TotalOccupancy { get; set; }
        public int TotalBedrooms { get; set; }
        public int TotalBathrooms { get; set; }
        public int Summary { get; set; }
        public Address Address { get; set; }
        public Amenities Amenities { get; set; }
        public int Price { get; set; }
        public DateTime PublishedAt { get; set; }
        public List<Reservation> Reservations { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<RoomComment> RoomComments { get; set; }
    }
}
