﻿namespace airbnb.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? RepeatedPassword { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
