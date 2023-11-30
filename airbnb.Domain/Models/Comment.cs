namespace airbnb.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserFirstName { get; set; }
        public string CommentValue { get; set; }
        public int NumberOfStars { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public List<RoomComment> RoomComments{ get; set; }
    }
}
