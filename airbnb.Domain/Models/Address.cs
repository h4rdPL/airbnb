namespace airbnb.Domain.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string ZIPCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int ApartmentNumber { get; set; }
    }
}
