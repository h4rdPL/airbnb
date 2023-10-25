using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airbnb.Domain
{
    public class Offer
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public int GuestNumberAvaliable { get; set; }
        public string FlatElements { get; set; }
        public float PriceForNight { get; set; }
        public string Benefits { get; set; }
        public string Description { get; set; }
        public string Amenities { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
        public int BookingId { get; set; }
        public Booking Booking { get; set; }
    }

    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public List<User> User { get; set; }
    }
}
