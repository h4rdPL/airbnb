namespace airbnb.Domain.Models
{
    public class RoomComment
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}