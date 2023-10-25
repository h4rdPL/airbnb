namespace airbnb.Domain
{
    public class Review
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ReviewDescription { get; set; }
        public int ReviewStarsNumber { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
